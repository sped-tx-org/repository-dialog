using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.VisualStudio.PlatformUI;

namespace RepositoryDialog.Shell
{
    /// <summary>
    /// Interaction logic for RepositoryDialog.xaml
    /// </summary>
    public partial class RepositoryDialog : DialogWindow
    {
        public static RepositoryDialog Create(Repository datacontext)
        {
            var dialog = new RepositoryDialog();
            dialog.MainGrid.DataContext = datacontext;
            return dialog;
        }

        internal Repository GetRepository() => (Repository)MainGrid.DataContext;

        public RepositoryDialog()
        {
            InitializeComponent();

            HasMaximizeButton = true;
            HasMinimizeButton = true;
            WindowStartupLocation = WindowStartupLocation.CenterOwner;

            RepositoryNameTextBox.Focus();
        }

        protected override void OnPreviewKeyDown(KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                DialogResult = false;
                Close();
            }
        }

        private void ApplyTargetFrameworkToProjectsCheckBox_Click(object sender, RoutedEventArgs e)
        {
            var current =(string) TargetFrameworkTextBox.GetValue(TextBox.TextProperty);
            var context = GetRepository();
            foreach(var project in context.Projects)
            {
                project.TargetFramework = current;
            }
        }

        private void ApplyNamespaceToProjectsCheckBox_Click(object sender, RoutedEventArgs e)
        {
            var current = (string)RootNamespaceTextBox.GetValue(TextBox.TextProperty);
            var context = GetRepository();
            foreach (var project in context.Projects)
            {
                project.RootNamespace = current;
            }
        }

        private void OnOKButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }
    

        private void OnCancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void ProjectsDataGrid_OnDeleteProjectClick(object sender, RoutedEventArgs e)
        {
            var context = GetRepository();
            var menuItem = sender as MenuItem;
            var contextMenu = menuItem.Parent as ContextMenu;
            var item = contextMenu.PlacementTarget as DataGrid;
            var project = item.SelectedCells[0].Item as Project;
            context.Projects.Remove(project);
        }

        private void ProjectsDataGrid_OnAddProjectClick(object sender, RoutedEventArgs e)
        {
            var project = new Project();
            var context = GetRepository();
            context.Projects.Add(project);

            
        }

        private void OnBrowseButton_Click(object sender, RoutedEventArgs e)
        {
            using (System.Windows.Forms.FolderBrowserDialog fbd = new System.Windows.Forms.FolderBrowserDialog())
            {
                fbd.Description = "Choose a directory for the repository folder.";
                fbd.RootFolder = Environment.SpecialFolder.MyComputer;
                fbd.ShowNewFolderButton = true;

                if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    OutputPathTextBox.SetValue(TextBox.TextProperty, fbd.SelectedPath);
                }
            }
        }

        private void RepositoryNameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var current = (string) RepositoryNameTextBox.GetValue(TextBox.TextProperty);

            SolutionNameTextBox.SetValue(TextBox.TextProperty, current);
        }

        private void DefaultButton_Click(object sender, RoutedEventArgs e)
        {
            var context = GetRepository();
            context.RepositoryName = "test-repository";
            context.SolutionName = "test-repository";
            context.OutputPath = "V:\\temp";
            context.RootNamespace = "My.Deep.RootNamespace";
            context.TargetFramework = "net48";
            context.Projects.Add(new Project { ProjectName = "Test.Repository.One", RootNamespace = "My.Deep.RootNamespace", TargetFramework = "net48", OutputType = OutputType.Library });
            context.Projects.Add(new Project { ProjectName = "Test.Repository.Two", RootNamespace = "My.Deep.RootNamespace", TargetFramework = "net48", OutputType = OutputType.Library });
            context.Projects.Add(new Project { ProjectName = "Test.Repository.Three", RootNamespace = "My.Deep.RootNamespace", TargetFramework = "net48", OutputType = OutputType.Library });
        }
    }
}

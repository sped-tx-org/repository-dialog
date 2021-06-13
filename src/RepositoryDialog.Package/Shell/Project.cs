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
using Repository.Services;

namespace RepositoryDialog.Shell
{

    /// <summary>
    /// Defines the <see cref="ProjectParameters" />.
    /// </summary>
    public class Project : ObservableObject
    {
        private OutputType _outputType;
        private ProjectType _projectType;
        private string _projectName;
        private string _rootNamespace;
        private string _targetFramework;

        /// <summary>
        /// Gets or sets the OutputType.
        /// </summary>
        public OutputType OutputType
        {
            get
            {
                return _outputType;
            }
            set
            {
                SetProperty(ref _outputType, value);
            }
        }

        /// <summary>
        /// Gets or sets the ProjectName.
        /// </summary>
        public string ProjectName
        {
            get
            {
                return _projectName;
            }
            set
            {
                SetProperty(ref _projectName, value);
            }
        }

        /// <summary>
        /// Gets or sets the RootNamespace.
        /// </summary>
        public string RootNamespace
        {
            get
            {
                return _rootNamespace;
            }
            set
            {
                SetProperty(ref _rootNamespace, value);
            }
        }

        /// <summary>
        /// Gets or sets the TargetFramework.
        /// </summary>
        public string TargetFramework
        {
            get
            {
                return _targetFramework;
            }
            set
            {
                SetProperty(ref _targetFramework, value);
            }
        }

        public ProjectType ProjectType
        {
            get
            {
                return _projectType;
            }
            set
            {
                SetProperty(ref _projectType, value);
            }
        }
    }
}

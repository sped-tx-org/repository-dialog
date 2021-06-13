using System.Collections.ObjectModel;

namespace RepositoryDialog.Shell
{

    public class Repository : ObservableObject
    {
        private string _repositoryName;
        private string _solutionName;
        private string _rootNamespace;
        private string _outputPath;
        private string _targetFramework;
        private ObservableCollection<Project> _projects = new ObservableCollection<Project>();

        public string RepositoryName
        {
            get
            {
                return _repositoryName;
            }
            set
            {
                SetProperty(ref _repositoryName, value);
            }
        }

        public string SolutionName
        {
            get
            {
                return _solutionName;
            }
            set
            {
                SetProperty(ref _solutionName, value);
            }
        }

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

        public string OutputPath
        {
            get
            {
                return _outputPath;
            }
            set
            {
                SetProperty(ref _outputPath, value);
            }
        }


        public ObservableCollection<Project> Projects
        {
            get
            {
                return _projects;
            }
            set
            {
                SetProperty(ref _projects, value);
            }
        }
    }
}

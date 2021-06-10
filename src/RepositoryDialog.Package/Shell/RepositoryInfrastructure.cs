namespace RepositoryDialog.Shell
{
    public class RepositoryInfrastructure
    {
        public RepositoryInfrastructure(Repository repository, RepositoryDialog dialog)
        {
            Repository = repository;
            Dialog = dialog;
        }

        public Repository Repository { get; }
        public RepositoryDialog Dialog { get; }
    }
}

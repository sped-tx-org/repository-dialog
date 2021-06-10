namespace RepositoryDialog.Shell
{
    public static class RepositoryFactory
    {
        public static RepositoryInfrastructure Create()
        {
            var repo = new Repository();
            var dialog = RepositoryDialog.Create(repo);
            return new RepositoryInfrastructure(repo, dialog);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RepositoryDialog.Shell;
using Microsoft.Extensions.DependencyInjection;
using Repository.Services;
using Task = System.Threading.Tasks.Task;
using Microsoft.VisualStudio.Shell;

namespace RepositoryDialog.Commands
{
    internal partial class CommandHandler
    {
        public override void OnExecuteNewRepositoryCommand(object sender, EventArgs e)
        {
            ThreadHelper.JoinableTaskFactory.Run(OnExecuteNewRepositoryCommandAsync);
        }

        public async Task OnExecuteNewRepositoryCommandAsync()
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

            var infra = Shell.RepositoryFactory.Create();
            if (infra.Dialog.ShowModal() == true)
            {
                var repository = infra.Repository;

                var generator = RepositoryDialog.RepositoryServices.Services.GetService<IRepositoryGenerator>();

                var settings = Repository.Services.RepositoryFactory.Settings(
                    repository.RepositoryName,
                    repository.SolutionName,
                    repository.OutputPath,
                    repository.RootNamespace,
                    repository.TargetFramework,
                    repository.Projects.ConvertAll());

                await generator.CreateRepositoryAsync(settings);

                Debug.Print("");
            }
        }
    }
}

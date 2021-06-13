using System;
using System.ComponentModel.Design;
using System.Runtime.InteropServices;
using System.Threading;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using RepositoryDialog.Commands;
using RepositoryDialog.Options;
using Task = System.Threading.Tasks.Task;

namespace RepositoryDialog.Packaging
{
    [PackageRegistration(UseManagedResourcesOnly = true, AllowsBackgroundLoading = true)]
    [ProvideMenuResource("Menus.ctmenu", 1)]
    [Guid(GuidSymbols.PackageString)]
    [ProvideOptionPage(typeof(OptionPage), "Repository Generator", "General", 0, 0, true)]
    public class RepositoryDialogPackage : AsyncPackage, IVsInstalledProduct
    {
        protected override async Task InitializeAsync(CancellationToken cancellationToken, IProgress<ServiceProgressData> progress)
        {
            await base.InitializeAsync(cancellationToken, progress);
            await JoinableTaskFactory.SwitchToMainThreadAsync(cancellationToken);

            var service = await GetServiceAsync(typeof(IMenuCommandService)) as OleMenuCommandService;
            var handler = new CommandHandler();
            CommandRegistrar.RegisterCommands(service, handler);
        }

        public int IdBmpSplash(out uint pIdBmp)
        {
            pIdBmp = 0U;
            return 0;
        }

        public int OfficialName(out string pbstrName)
        {
            pbstrName = "Repository Dialog Package";
            return 0;
        }

        public int ProductID(out string pbstrPID)
        {
            pbstrPID = "Repository Dialog";
            return 0;
        }

        public int ProductDetails(out string pbstrProductDetails)
        {
            pbstrProductDetails = "Generates a new repository.";
            return 0;
        }

        public int IdIcoLogoForAboutbox(out uint pIdIco)
        {
            pIdIco = 57077U;
            return 0;
        }
    }
}

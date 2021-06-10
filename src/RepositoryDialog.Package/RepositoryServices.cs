using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnvDTE;
using EnvDTE80;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.PlatformUI;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Repository.Services;
using Task = System.Threading.Tasks.Task;

namespace RepositoryDialog
{
    internal static class RepositoryServices
    {
        private static IServiceProvider _serviceProvider;

        public static IServiceProvider Services
        {
            get
            {
                if (_serviceProvider is null)
                {
                    _serviceProvider = CreateServiceCollection().BuildServiceProvider();
                }
                return _serviceProvider;
            }
        }

        private static IServiceCollection CreateServiceCollection()
        {
            IServiceCollection services = new ServiceCollection();
            services = services.AddSingleton<IRepositoryOpener, VsRepositoryOpener>();
            services = services.AddSingleton<IRepositoryDownloader, RepositoryDownloader>();
            services = services.AddSingleton<IRepositoryExpander, RepositoryExpander>();
            services = services.AddSingleton<IRepositoryFileSystem, RepositoryFileSystem>();
            services = services.AddSingleton<RepositoryDependencies>();
            services = services.AddSingleton<IRepositoryGenerator, RepositoryGenerator>();
            return services;
        }


        public static IEnumerable<IRepositoryProject> ConvertAll(this IEnumerable<RepositoryDialog.Shell.Project> source)
        {
            return source.Select(x => x.Convert());
        }

        public static IRepositoryProject Convert(this RepositoryDialog.Shell.Project source)
        {
            return RepositoryFactory.Project(
                source.ProjectName,
                source.RootNamespace,
                source.TargetFramework,
                source.OutputType.ToString());
        }
    }

    public class VsRepositoryOpener : IRepositoryOpener
    {
        private const uint AddToCurrent = (uint)__VSSLNOPENOPTIONS.SLNOPENOPT_AddToCurrent;

        public async Task CollapseSolutionAsync()
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();
            UIHierarchy solutionExplorer = DTE.ToolWindows.SolutionExplorer;
            if (solutionExplorer.UIHierarchyItems.Count <= 0)
                return;
            UIHierarchyItem rootNode = solutionExplorer.UIHierarchyItems.Item(1);
            await CollapseAsync(rootNode, solutionExplorer).ConfigureAwait(false);
            rootNode.Select(vsUISelectionType.vsUISelectionTypeSelect);
            rootNode.DTE.SuppressUI = false;
        }

        public async Task OpenRepositoryAsync(string solutionFilePath)
        {
            while (!File.Exists(solutionFilePath))
            {
                System.Threading.Thread.Sleep(1000);
                System.Diagnostics.Debug.Print($"Waiting for file {solutionFilePath} to exist");
            }

            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();
            Solution2.OpenSolutionFile(AddToCurrent, solutionFilePath);
            Solution4.EnsureSolutionIsLoaded((uint)__VSBSLFLAGS.VSBSLFLAGS_None);
            Solution2.SaveSolutionElement((uint)__VSSLNSAVEOPTIONS.SLNSAVEOPT_ForceSave, null, 0);
            await CollapseSolutionAsync().ConfigureAwait(false);
        }

        private async Task CollapseAsync(UIHierarchyItem item,  UIHierarchy solutionExplorer)
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

            foreach (UIHierarchyItem innerItem in item.UIHierarchyItems)
            {
                if (innerItem.UIHierarchyItems.Count > 0)
                {
                    await CollapseAsync(innerItem, solutionExplorer).ConfigureAwait(false);
                    if (innerItem.UIHierarchyItems.Expanded)
                    {
                        innerItem.UIHierarchyItems.Expanded = false;
                        if (innerItem.UIHierarchyItems.Expanded)
                        {
                            innerItem.Select(vsUISelectionType.vsUISelectionTypeSelect);
                            solutionExplorer.DoDefaultAction();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Gets the DTE.
        /// </summary>
        private static DTE2 DTE => GetService<SDTE, DTE2>();

        /// <summary>
        /// Gets the Solution2.
        /// </summary>
        private static IVsSolution2 Solution2 => GetService<SVsSolution, IVsSolution2>();

        /// <summary>
        /// Gets the Solution4.
        /// </summary>
        private static IVsSolution4 Solution4 => GetService<SVsSolution, IVsSolution4>();

        /// <summary>
        /// The GetService.
        /// </summary>
        /// <typeparam name="TService">.</typeparam>
        /// <typeparam name="TServiceInterface">.</typeparam>
        /// <returns>The <see cref="TServiceInterface"/>.</returns>
        private static TServiceInterface GetService<TService, TServiceInterface>()
        {
            return (TServiceInterface)Package.GetGlobalService(typeof(TService));
        }
    }
}

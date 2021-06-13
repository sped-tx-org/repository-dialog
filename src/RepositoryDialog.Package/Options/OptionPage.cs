using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnvDTE;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;

namespace RepositoryDialog.Options
{
    public class OptionPage : DialogPage
    {
        [Category("Repository Dialog")]
        [DisplayName("Repository Zip URL")]
        [Description("The zip file used by the repository generator.")]
        public string RepositoryZipUrl { get; set; } = "https://codeload.github.com/sped-mobi/arcade/zip/master";


        public static async Task<string> GetRepositoryZipUrlAsync()
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();
            var dte = GetService<SDTE, DTE>();
            var properties = dte.get_Properties("Repository Generator", "General");
            var property = properties.Item("RepositoryZipUrl");
            return property.Value as string;
        }

        private static TServiceInterface GetService<TService, TServiceInterface>()
        {
            return (TServiceInterface)Package.GetGlobalService(typeof(TService));
        }
    }
}

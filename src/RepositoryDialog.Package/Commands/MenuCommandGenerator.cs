//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RepositoryDialog.Commands
{
    using System;
    using System.ComponentModel.Design;
    using Microsoft.VisualStudio.Shell;
    
    internal partial class GuidSymbols
    {
        public const string PackageString = "AB8BD9B5-DB97-4DDC-9390-61B060CBA423";
        public const string PackageMenusString = "EA586EFE-CCF9-40BC-A7EA-2E9DB032DA99";
        public const string PackageGroupsString = "92D56DDA-8958-44F3-ADE7-61EC4594119B";
        public const string PackageCommandsString = "EF5F7C51-37E0-4574-831F-BF3215D6395C";
        public static Guid Package = new Guid(PackageString);
        public static Guid PackageMenus = new Guid(PackageMenusString);
        public static Guid PackageGroups = new Guid(PackageGroupsString);
        public static Guid PackageCommands = new Guid(PackageCommandsString);
    }
    /// <summary>
    /// Represents the <see cref="IDSymbols"/> class.
    /// </summary>
    internal partial class IDSymbols
    {
        public const int IDM_REPOSITORY = 257;
        public const int IDG_REPOSITORY = 17;
        public const int NewRepositoryCommand = 1;
    }
    /// <summary>
    /// Represents the <see cref="Commands"/> class.
    /// </summary>
    internal partial class Commands
    {
        public static CommandID NewRepositoryCommandCommandId = new CommandID(GuidSymbols.PackageCommands, IDSymbols.NewRepositoryCommand);
    }
    /// <summary>
    ///  Serves as the abstract base for classes that handle commands.
    /// </summary>
    internal abstract partial class AbstractCommandHandler
    {
        /// <summary>
        ///  Initializes a new instance of the <see cref="AbstractCommandHandler"/> class.
        /// </summary>
        protected AbstractCommandHandler()
        {
        }
        /// <summary>
        /// When overridden in a derived class, handles the NewRepositoryCommand command.
        /// </summary>
        /// <param name="sender">The <see cref="object"/> initiating the command.</param>
        /// <param name="e">The <see cref="EventArgs"/> that will ultimately handle the command.</param>
        public virtual void OnExecuteNewRepositoryCommand(object sender, EventArgs e)
        {
        }
    }
    /// <summary>
    ///  Entry point for handling commands.
    /// </summary>
    internal partial class CommandHandler : AbstractCommandHandler
    {
    }
    /// <summary>
    ///  Does the job no one else will do, and registers the commands.
    /// </summary>
    internal partial class CommandRegistrar
    {
        /// <summary>
        /// Registers the commands.
        /// </summary>
        /// <param name="service">the <see cref="OleMenuCommandService"/> that services the commands</param>
        /// <param name="handler">the <see cref="CommandHandler"/> class containing all of the handlers to the commands</param>
        public static void RegisterCommands(OleMenuCommandService service, CommandHandler handler)
        {
            RegisterCommand(service, Commands.NewRepositoryCommandCommandId, handler.OnExecuteNewRepositoryCommand);
        }
        /// <summary>
        /// Registers a command.
        /// </summary>
        /// <param name="service">the OleCommandService that services the commands</param>
        /// <param name="cmdId">the CommandID that identifies the command</param>
        /// <param name="commandHanlder">the EventHandler responsible for handling the command</param>
        private static void RegisterCommand(OleMenuCommandService service, CommandID cmdId, EventHandler commandHandler)
        {
            OleMenuCommand command = new OleMenuCommand(commandHandler, cmdId);
            service.AddCommand(command);
        }
    }
}


using System;
using AfterHope.Commands.Executors;

namespace AfterHope.Commands.Parsing.Syntax
{
    public interface ICommandSyntax
    {
        void DefineCommand<TCommandExecutor>(string commandName, string commandHelp) where TCommandExecutor : ICommandExecutor;

        void DefineHelpCommand<TSuperCommandExecutor>(string commandHelp) where TSuperCommandExecutor : ISuperCommandExecutor;

        void DefineDefaultCommand<TSuperCommandExecutor>() where TSuperCommandExecutor : ISuperCommandExecutor;

        string GetCommandName<TCommandExecutor>() where TCommandExecutor : ICommandExecutor;

        string GetCommandName(Type type);

        ICommandExecutor GetExecutor(string commandName);

        CommandInfo[] GetCommands();

        string HelpCommandName { get; }

        string DefaultCommandName { get; }
    }
}
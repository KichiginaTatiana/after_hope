using System;
using System.Collections.Generic;
using System.Linq;
using AfterHope.Commands.Executors;
using AfterHope.Commands.Executors.Factory;
using AfterHope.Commands.Parsing.Syntax.Exceptions;

namespace AfterHope.Commands.Parsing.Syntax
{
    public class CommandSyntax : ICommandSyntax
    {
        private readonly IDictionary<string, ICommandExecutor> nameToExecutor = new Dictionary<string, ICommandExecutor>();
        private readonly IDictionary<Type, string> executorToName = new Dictionary<Type, string>();
        private readonly IDictionary<string, ISuperCommandExecutor> nameToSuperExecutor = new Dictionary<string, ISuperCommandExecutor>();
        private readonly IDictionary<string, string> nameToHelp = new Dictionary<string, string>();
        private readonly HashSet<string> hiddenCommands = new HashSet<string>();
        private readonly IList<string> commandsInOrder = new List<string>();
        private readonly ICommandExecutorsFactory commandExecutorsFactory;
        private readonly ISuperCommandExecutorsFacotry superCommandExecutorsFacotry;
        private const string startCommandName = "start";

        public CommandSyntax(
            ICommandExecutorsFactory commandExecutorsFactory,
            ISuperCommandExecutorsFacotry superCommandExecutorsFacotry
            )
        {
            this.commandExecutorsFactory = commandExecutorsFactory;
            this.superCommandExecutorsFacotry = superCommandExecutorsFacotry;
            DefineHelpCommand<DefaultHelpSuperCommandExecutor>("help :)");
            DefineStartCommand<DefaultStartSuperCommandExecutor>(startCommandName);
            DefineDefaultCommand<DefaultSuperCommandExecutor>();
        }

        public void DefineCommand<TCommandExecutor>(string commandName, string commandHelp) where TCommandExecutor : ICommandExecutor
        {
            if (nameToExecutor.ContainsKey(commandName))// || nameToSuperExecutor.ContainsKey(commandName))
                throw new CommandAlreadyExistsException(commandName);

            executorToName[typeof(TCommandExecutor)] = commandName;
            nameToExecutor[commandName] = commandExecutorsFactory.Create<TCommandExecutor>();
            nameToHelp[commandName] = commandHelp;
            commandsInOrder.Add(commandName);
        }

        public void DefineHelpCommand<TSuperCommandExecutor>(string commandHelp) where TSuperCommandExecutor : ISuperCommandExecutor
        {
            DefineSuperCommand<TSuperCommandExecutor>(HelpCommandName, commandHelp, false);
        }

        public void DefineStartCommand<TSuperCommandExecutor>(string commandStart) where TSuperCommandExecutor : ISuperCommandExecutor
        {
            DefineSuperCommand<TSuperCommandExecutor>(startCommandName, commandStart, true);
        }

        public void DefineDefaultCommand<TSuperCommandExecutor>() where TSuperCommandExecutor : ISuperCommandExecutor
        {
            DefineSuperCommand<TSuperCommandExecutor>(DefaultCommandName, null, true);
        }

        public string GetCommandName<TCommandExecutor>() where TCommandExecutor : ICommandExecutor
        {
            return executorToName[typeof(TCommandExecutor)];
        }

        public string GetCommandName(Type type)
        {
            return executorToName[type];
        }

        private void DefineSuperCommand<TSuperCommandExecutor>(string commandName, string commandHelp, bool hidden)
            where TSuperCommandExecutor : ISuperCommandExecutor
        {
            if (nameToSuperExecutor.ContainsKey(commandName))
                commandsInOrder.Remove(commandName);

            nameToSuperExecutor[commandName] = superCommandExecutorsFacotry.Create<TSuperCommandExecutor>();
            nameToHelp[commandName] = commandHelp;
            commandsInOrder.Add(commandName);
            if (hidden)
                hiddenCommands.Add(commandName);
        }

        public ICommandExecutor GetExecutor(string commandName)
        {
            if (nameToSuperExecutor.ContainsKey(commandName))
                return nameToSuperExecutor[commandName].UseSyntax(this);

            if (!nameToExecutor.ContainsKey(commandName))
            {
                if (nameToSuperExecutor.ContainsKey(DefaultCommandName))
                    return nameToSuperExecutor[DefaultCommandName].UseSyntax(this);
                throw new CommandDoesNotExistException(commandName);
            }

            return nameToExecutor[commandName];
        }

        public CommandInfo[] GetCommands()
        {
            return commandsInOrder
                .Where(x => !hiddenCommands.Contains(x))
                .Select(x => new CommandInfo
                {
                    Name = x,
                    Help = nameToHelp[x],
                })
                .ToArray();
        }

        public string HelpCommandName { get; } = "help";

        public string DefaultCommandName { get; } = "default";
    }
}
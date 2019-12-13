using System;
using AfterHope.Commands.Parsing.Syntax;
using AfterHope.Storing;

namespace AfterHope.Commands.Executors
{
    public class ShowCommandExecutor : CommandExecutorBase, ICommandExecutor
    {
        public ShowCommandExecutor(IPersonRepository personRepository) : base(personRepository)
        {
        }

        public CommandResult Execute(Command command, CommandMeta meta, ICommandSyntax syntax)
        {
            switch (command.Args.Length)
            {
                case 1:
                    return ShowOne(command, meta, syntax);
                default:
                    throw new ArgumentException();
            }
        }

        private CommandResult ShowOne(Command command, CommandMeta meta, ICommandSyntax syntax)
        {
            var person = PersonRepository.Read(command.Args[0]);
            return CommandResult.AsSucceed($"{person.Name}", inlineMenu: CreateCommonMenu(syntax), update: true);
        }

        public bool AppliesTo(CommandType commandType) => commandType == CommandType.Show;
    }
}
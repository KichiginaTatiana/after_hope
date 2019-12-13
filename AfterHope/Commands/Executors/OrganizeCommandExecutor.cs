using System;
using AfterHope.Commands.Parsing.Syntax;
using AfterHope.Storing;

namespace AfterHope.Commands.Executors
{
    public class OrganizeCommandExecutor : CommandExecutorBase, ICommandExecutor
    {
        public OrganizeCommandExecutor(IPersonRepository personRepository) : base(personRepository)
        {
        }

        public CommandResult Execute(Command command, CommandMeta meta, ICommandSyntax syntax)
        {
            switch (command.Args.Length)
            {
                case 0:
                    return SelectLawsuit(meta, syntax);
                case 1:
                    return CreateDocument(command, meta, syntax);
                default:
                    throw new ArgumentException();
            }
        }

        private CommandResult CreateDocument(Command command, CommandMeta meta, ICommandSyntax syntax)
        {
            return CommandResult.AsSucceed("Выгрузка");
        }

        private CommandResult SelectLawsuit(CommandMeta meta, ICommandSyntax syntax) =>
            CommandResult.AsSucceed("Выберите дело",
                inlineMenu: CreateLawsuitsMenu(syntax, GetLawsuits(meta)),
                update: meta.FromInlineMenu);

        public bool AppliesTo(CommandType commandType) => commandType == CommandType.Organize;
    }
}
using AfterHope.Commands.Menus.Inline;
using AfterHope.Commands.Parsing.Syntax;

namespace AfterHope.Commands.Executors
{
    public class DefaultStartSuperCommandExecutor : ISuperCommandExecutor
    {
        public CommandResult Execute(Command command, CommandMeta meta, ICommandSyntax syntax) =>
            CommandResult.AsSucceed("Вы хотите организовать вечер писем узникам совести или написать одному из них?",
                inlineMenu: CreateMenu(syntax),
                update: meta.FromInlineMenu);

        public bool AppliesTo(CommandType commandType) => true;

        public ICommandExecutor UseSyntax(ICommandSyntax syntax)
        {
            this.syntax = syntax;
            return this;
        }

        private static InlineMenu CreateMenu(ICommandSyntax commandSyntax)
        {
            var organizeCommandName = commandSyntax.GetCommandName<OrganizeCommandExecutor>();
            var findCommandName = commandSyntax.GetCommandName<FindCommandExecutor>();

            var builder = InlineMenu.Build();
            builder.AddRow().WithCell("Организовать", organizeCommandName);
            builder.AddRow().WithCell("Узнать", findCommandName);
            return builder.Create();
        }

        private ICommandSyntax syntax;
    }
}
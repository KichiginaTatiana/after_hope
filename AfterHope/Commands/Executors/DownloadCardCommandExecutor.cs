﻿using AfterHope.Commands.Parsing.Syntax;
using AfterHope.Data.Repositories;

namespace AfterHope.Commands.Executors
{
    public class DownloadCardCommandExecutor : CommandExecutorBase, ICommandExecutor
    {
        public DownloadCardCommandExecutor(IPersonRepository personRepository,
            IPosterRepository posterRepository) : base(personRepository, posterRepository)
        {
        }

        public CommandResult Execute(Command command, CommandMeta meta, ICommandSyntax syntax)
        {
            return CommandResult.AsSucceed("Карточка");
        }

        public bool AppliesTo(CommandType commandType) => commandType == CommandType.DownloadCard;
    }
}
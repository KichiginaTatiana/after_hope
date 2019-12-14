using System;
using AfterHope.Commands.Parsing.Syntax;
using AfterHope.Data.Repositories;

namespace AfterHope.Commands.Executors
{
    public class DownloadPosterCommandExecutor : CommandExecutorBase, ICommandExecutor
    {
        public DownloadPosterCommandExecutor(IPersonRepository personRepository,
            IPosterRepository posterRepository) : base(personRepository, posterRepository)
        {
        }

        public CommandResult Execute(Command command, CommandMeta meta, ICommandSyntax syntax)
        {
            var posterId = command.Args[0];
            var poster = PosterRepository.Read(posterId);
            return CommandResult.AsSucceed(string.Empty, photoId: poster.FileId);
        }

        public bool AppliesTo(CommandType commandType) => commandType == CommandType.DownloadPoster;
    }
}
using System;
using System.Linq;
using AfterHope.Commands.Exceptions;

namespace AfterHope.Commands.Parser
{
    public class CommandParser : ICommandParser
    {
        public Command Parse(string command)
        {
            var commandParts = command.TrimStart('/').Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (commandParts.Length < 1)
                throw new IncorrectCommandException();

            var args = commandParts.Skip(1).ToArray();
            CommandType commandType;
            switch (commandParts[0])
            {
                case "organize":
                    commandType = CommandType.Organize;
                    break;
                case "find":
                    commandType = CommandType.Find;
                    break;
                case "help":
                    commandType = CommandType.Help;
                    break;
                case "show":
                    commandType = CommandType.Show;
                    break;
                case "downloadcard":
                    commandType = CommandType.DownloadCard;
                    break;
                case "downloadlist":
                    commandType = CommandType.DownloadList;
                    break;
                case "downloadposter":
                    commandType = CommandType.DownloadPoster;
                    break;
                case "add":
                    commandType = CommandType.Add;
                    break;
                case "letter":
                    commandType = CommandType.SendLetter;
                    break;
                case "process":
                    commandType = CommandType.ProcessLetter;
                    break;
                default:
                    commandType = CommandType.Unknown;
                    break;
            }

            return Command.Create(commandType, args);
        }
    }
}
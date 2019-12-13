using System;

namespace AfterHope.Commands.Parsing.Syntax.Exceptions
{
    public class CommandDoesNotExistException : Exception
    {
        public CommandDoesNotExistException(string commandName)
        {
            Commandname = commandName;
        }

        public string Commandname { get; set; }
    }
}
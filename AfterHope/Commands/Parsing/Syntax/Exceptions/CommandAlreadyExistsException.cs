using System;

namespace AfterHope.Commands.Parsing.Syntax.Exceptions
{
    public class CommandAlreadyExistsException : Exception
    {
        public CommandAlreadyExistsException(string commandName)
        {
            CommandName = commandName;

        }
        
        public string CommandName { get; set; }
    }
}
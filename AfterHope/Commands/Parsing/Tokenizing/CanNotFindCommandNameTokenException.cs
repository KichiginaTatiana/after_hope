using System;

namespace AfterHope.Commands.Parsing.Tokenizing
{
    public class CanNotFindCommandNameTokenException : Exception
    {
        public CanNotFindCommandNameTokenException(string commandLine) : base($"Can not find commandName token for commandLine: {commandLine}")
        {
        }
    }
}
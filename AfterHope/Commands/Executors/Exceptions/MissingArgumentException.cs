using System;

namespace AfterHope.Commands.Executors.Exceptions
{
    public class MissingArgumentException : Exception
    {
        public MissingArgumentException() {}

        public MissingArgumentException(string argName) : base($"Missing argument {argName}")
        {
            ArgName = argName;
        }

        public string ArgName { get; set; }
    }
}
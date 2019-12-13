using System;
using System.Linq;

namespace AfterHope.Commands.Parsing.Tokenizing
{
    public class Tokenizer : ITokenizer
    {
        public Tokens Tokenize(string commandLine)
        {
            var commandParts = commandLine.Split(new[] {' ', '_'}, StringSplitOptions.RemoveEmptyEntries);

            if (!commandParts[0].StartsWith("/"))
                throw new CanNotFindCommandNameTokenException(commandLine);

            var commandName = commandParts[0].Split(new[] {'@'}, StringSplitOptions.RemoveEmptyEntries)[0];

            return new Tokens
            {
                CommandName = commandName.TrimStart('/'),
                CommandArgs = commandParts.Skip(1).ToArray(),
            };
        }
    }
}
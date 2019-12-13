namespace AfterHope.Commands.Parsing.Tokenizing
{
    public interface ITokenizer
    {
         Tokens Tokenize(string commandLine);
    }
}
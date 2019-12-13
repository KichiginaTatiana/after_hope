namespace AfterHope.Commands.Exceptions
{
    public class IncorrectCommandException : System.Exception
    {
        public IncorrectCommandException() { }
        public IncorrectCommandException(string message) : base(message) { }
    }
}
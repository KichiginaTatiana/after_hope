namespace AfterHope.Commands.Executors.Factory
{
    public interface ICommandExecutorsFactory
    {
        ICommandExecutor Create<TCommandExecutor>() where TCommandExecutor : ICommandExecutor;
    }
}
namespace AfterHope.Commands.Executors.Factory
{
    public interface ISuperCommandExecutorsFacotry
    {
        ISuperCommandExecutor Create<TSuperCommandExecutor>() where TSuperCommandExecutor : ISuperCommandExecutor;
    }
}
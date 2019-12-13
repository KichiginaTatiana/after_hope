using System;
using System.Collections.Generic;
using System.Linq;

namespace AfterHope.Commands.Executors.Factory
{
    public class SuperCommandExecutorsFactory : ISuperCommandExecutorsFacotry
    {
        private readonly IDictionary<Type, ISuperCommandExecutor> typeToExecutor;

        public SuperCommandExecutorsFactory(ISuperCommandExecutor[] executors)
        {
            typeToExecutor = executors.ToDictionary(x => x.GetType());
        }

        public ISuperCommandExecutor Create<TSuperCommandExecutor>() where TSuperCommandExecutor : ISuperCommandExecutor
        {
            return typeToExecutor[typeof(TSuperCommandExecutor)];
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;

namespace AfterHope.Commands.Executors.Factory
{
    public class CommandExecutorsFactory : ICommandExecutorsFactory
    {
        private readonly IDictionary<Type, ICommandExecutor> typeToExecutor;

        public CommandExecutorsFactory(ICommandExecutor[] executors)
        {
            typeToExecutor = executors.ToDictionary(x => x.GetType());
        }

        public ICommandExecutor Create<TCommandExecutor>() where TCommandExecutor : ICommandExecutor
        {
            return typeToExecutor[typeof(TCommandExecutor)];
        }
    }
}
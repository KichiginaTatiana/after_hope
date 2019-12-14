using System.Reflection;
using AfterHope.Commands.Parsing.Syntax;
using AfterHope.Data.Repositories;
using AfterHope.Exporting;
using Grace.DependencyInjection;
using Grace.DependencyInjection.Lifestyle;
using LiteDB;

namespace AfterHope.Configuration
{
    public static class ContainerConfiguration
    {
        public static DependencyInjectionContainer Configure()
        {
            var container = new DependencyInjectionContainer();

            var repository = new LiteRepository(@"data\db");
            container.Configure(c => c.ExportInstance(repository).As<LiteRepository>());
            container.Configure(c => c.ExportAssembly(Assembly.GetExecutingAssembly()).ByInterfaces().Lifestyle.Custom(new SingletonLifestyle()));
            container.Configure(c => c.ExportAssembly(Assembly.GetAssembly(typeof(IPersonRepository))).ByInterfaces().Lifestyle.Custom(new SingletonLifestyle()));
            container.Configure(c => c.ExportAssembly(Assembly.GetAssembly(typeof(IPersonListExporter))).ByInterfaces().Lifestyle.Custom(new SingletonLifestyle()));

            var commandSyntax = CommandExecutorsConfiguration.Configure(container);
            container.Configure(c => c.ExportInstance(commandSyntax).As<ICommandSyntax>());
            
            return container;
        }
    }
}
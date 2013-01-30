using MusicStore.Api.CommandHandlers;
using StructureMap;
using System.Collections.Generic;
using System.Linq;

namespace MusicStore.Api.Command
{
    public class CommandBus : ICommandBus
    {
        private readonly IContainer Container;

        public CommandBus(IContainer container)
        {
            this.Container = container;
        }

        public void Send<T>(T Command) where T : ICommand
        {
            IList<ICommandHandler<T>> commandHandlers = Container.GetAllInstances<ICommandHandler<T>>();
            if (commandHandlers != null)
                commandHandlers.ToList().ForEach(p => p.Execute(Command));
        }
    }
}
using MusicStore.Api;

namespace MusicStore.UnitTest
{
    public class InMemoryCommandBus : ICommandBus
    {
        public InMemoryCommandBus()
        {
        }

        public ICommand Command { get; set; }

        public void Send<T>(T command) where T : ICommand
        {
            this.Command = command;
        }
    }
}
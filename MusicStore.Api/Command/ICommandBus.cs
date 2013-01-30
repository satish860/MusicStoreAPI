namespace MusicStore.Api
{
    public interface ICommandBus
    {
        void Send<T>(T Command) where T : ICommand;
    }
}
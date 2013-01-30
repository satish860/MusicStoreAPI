namespace MusicStore.Api.CommandHandlers
{
    public interface ICommandHandler<T> where T : ICommand
    {
        void Execute(T Command);
    }
}
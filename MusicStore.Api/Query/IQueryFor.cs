namespace MusicStore.Api.Query
{
    public interface IQueryFor<TInput, TOutput>
    {
        TOutput Execute(TInput input);
    }
}
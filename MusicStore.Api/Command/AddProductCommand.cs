namespace MusicStore.Api.Controllers
{
    public class AddProductCommand : ICommand
    {
        public string Categories { get; set; }

        public int Id { get; set; }

        public string Name { get; set; }
    }
}
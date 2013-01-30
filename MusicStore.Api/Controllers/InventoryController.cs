using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MusicStore.Api.Controllers
{
    public class InventoryController : ApiController
    {
        private ICommandBus commandBus;

        public InventoryController(ICommandBus bus)
        {
            this.commandBus = bus;
        }

        public HttpResponseMessage Post(AddProductCommand command)
        {
            commandBus.Send(command);
            HttpResponseMessage message = this.Request.CreateResponse(HttpStatusCode.Created);
            string link = Url.Link("DefaultApi", new { controller = "Product", Id = command.Id });
            message.Headers.Location = new Uri(link);
            return message;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MusicStore.Api.Controllers
{
    public class CartController : ApiController
    {
        private readonly ICommandBus bus;
        public CartController(ICommandBus Bus)
        {
            this.bus = Bus;
        }

        public HttpResponseMessage Post(CreateCartCommand command)
        {
            bus.Send(command);
            string link = Url.Link("DefaultApi", new { controller = "Cart", Id = command.Id });
            HttpResponseMessage message = this.Request.CreateResponse(HttpStatusCode.Created);
            message.Headers.Location = new Uri(link);
            return message;
        }

        public HttpResponseMessage PutCartCommand(AddProductToCartCommand command)
        {
            bus.Send(command);
            return this.Request.CreateResponse(HttpStatusCode.OK);
        }
    }

}

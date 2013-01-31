using MusicStore.Api.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MusicStore.Api.Controllers
{
    public class AdminUserController : ApiController
    {
        private static IList<AdminModel> AdminList = new List<AdminModel>();

        public AdminUserController()
        {
        }

        public HttpResponseMessage GetByUsername(string user)
        {
            if (AdminList.Any(p => p.UserName == user))
                return this.Request.CreateResponse(HttpStatusCode.OK);
            return this.Request.CreateErrorResponse(HttpStatusCode.NotFound, "not found");
        }

        public HttpResponseMessage Post(AdminModel model)
        {
            if (AdminList.Any(p => p.UserName == model.UserName))
            {
                return this.Request.CreateResponse(HttpStatusCode.Conflict, "User Name Already Exixt");
            }
            AdminList.Add(model);
            return this.Request.CreateResponse(HttpStatusCode.Created);
        }
    }
}
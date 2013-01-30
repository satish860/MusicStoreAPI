using MusicStore.Api.Model;
using MusicStore.Api.Query;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MusicStore.Api
{
    public class AdminAuthorizationMessageHandler : DelegatingHandler
    {
        private readonly IQueryFor<AdminModel, AdminModel> Query;

        public AdminAuthorizationMessageHandler(IQueryFor<AdminModel, AdminModel> Query)
        {
            this.Query = Query;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (request.Headers.Authorization == null)
                return Task.Factory.StartNew(() => { return new HttpResponseMessage(HttpStatusCode.Unauthorized); });
            AdminModel model = GetUserFromAuthorizationHeader(request.Headers.Authorization);
            if (model == null || this.Query.Execute(model) == null)
                return Task.Factory.StartNew(() => { return new HttpResponseMessage(HttpStatusCode.Unauthorized); });
            return base.SendAsync(request, cancellationToken);
        }

        private AdminModel GetUserFromAuthorizationHeader(AuthenticationHeaderValue authenticationHeaderValue)
        {
            try
            {
                string userNamePassword = Encoding.ASCII.GetString(Convert.FromBase64String(authenticationHeaderValue.Parameter));
                var userNamePasswordArray = userNamePassword.Split(':');
                return new AdminModel { UserName = userNamePasswordArray[0], Password = userNamePasswordArray[1] };
            }
            catch (Exception)
            {
                return null;
            }
        }

        //private AdminUserQuery GetUserFromAuthorizationHeader(AuthorizationHeaderValue
    }
}
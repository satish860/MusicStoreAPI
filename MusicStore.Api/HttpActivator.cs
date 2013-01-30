using StructureMap;
using System;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;

namespace MusicStore.Api
{
    public class HttpActivator : IHttpControllerActivator
    {
        private readonly IContainer structureMapContainer;

        public HttpActivator(IContainer container)
        {
            this.structureMapContainer = container;
        }

        public IHttpController Create(HttpRequestMessage request, HttpControllerDescriptor controllerDescriptor, Type controllerType)
        {
            IContainer container = this.structureMapContainer.GetNestedContainer();
            request.RegisterForDispose(container);
            return container.GetInstance(controllerType) as IHttpController;
        }
    }
}
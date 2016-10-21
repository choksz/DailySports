using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Dependencies;

namespace DailySports.DailySports.IOC
{
    public class UnityDependencyResolver : System.Web.Mvc.IDependencyResolver
    {
        readonly IUnityContainer _container;
        public UnityDependencyResolver(IUnityContainer container)
        {
            this._container = container;
        }
        public IDependencyScope BeginScope()
        {
            throw new NotImplementedException();
        }
        public void Dispose()
        {
            throw new NotImplementedException();
        }
        public object GetService(Type serviceType)
        {
            try
            {
                return _container.Resolve(serviceType);
            }
            catch
            {
                return null;
            }
        }
        public IEnumerable<object> GetServices(Type serviceType)
        {
            try
            {
                return _container.ResolveAll(serviceType);
            }
            catch
            {
                return new List<object>();
            }
        }
    }
}
using System; 
using System.Collections.Generic;
using System.Web.Http.Dependencies; 
using Microsoft.Practices.Unity; 

namespace Mundialito
{
    class IoCContainer : ScopeContainer, IDependencyResolver 
    { 
        public IoCContainer(IUnityContainer container) 
            : base(container) 
        { 
        } 
 
        public IDependencyScope BeginScope() 
        { 
            var child = Container.CreateChildContainer(); 
            return new ScopeContainer(child); 
        } 
    } 

    class ScopeContainer : IDependencyScope 
    { 
        protected IUnityContainer Container; 
 
        public ScopeContainer(IUnityContainer container) 
        { 
            if (container == null) 
            { 
                throw new ArgumentNullException("container"); 
            } 
            this.Container = container; 
        } 
 
        public object GetService(Type serviceType)
        {
            if (Container.IsRegistered(serviceType)) 
            { 
                return Container.Resolve(serviceType); 
            }
            return null;
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            if (Container.IsRegistered(serviceType)) 
            { 
                return Container.ResolveAll(serviceType); 
            }
            return new List<object>();
        }

        public void Dispose() 
        { 
            Container.Dispose(); 
        } 
    } 

   

}
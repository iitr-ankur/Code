using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using Microsoft.WindowsAzure.Mobile.Service;
using testcodefirstService.DataObjects;
using testcodefirstService.Models;

namespace testcodefirstService.Controllers
{
    public class ThreadController : TableController<Thread>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            testcodefirstContext context = new testcodefirstContext();
            DomainManager = new EntityDomainManager<Thread>(context, Request, Services);
        }

        // GET tables/Thread
        public IQueryable<Thread> GetAllThread()
        {
            return Query(); 
        }

        // GET tables/Thread/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<Thread> GetThread(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/Thread/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<Thread> PatchThread(string id, Delta<Thread> patch)
        {
             return UpdateAsync(id, patch);
        }

        // POST tables/Thread
        public async Task<IHttpActionResult> PostThread(Thread item)
        {
            Thread current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/Thread/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteThread(string id)
        {
             return DeleteAsync(id);
        }

    }
}
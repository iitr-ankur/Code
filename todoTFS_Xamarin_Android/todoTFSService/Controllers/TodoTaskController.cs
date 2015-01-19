using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using Microsoft.WindowsAzure.Mobile.Service;
using todoTFSService.DataObjects;
using todoTFSService.Models;

namespace todoTFSService.Controllers
{
    public class TodoTaskController : TableController<TodoTask>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            todoTFSContext context = new todoTFSContext();
            DomainManager = new EntityDomainManager<TodoTask>(context, Request, Services);
        }

        // GET tables/TodoTask
        public IQueryable<TodoTask> GetAllTodoTask()
        {
            return Query(); 
        }

        // GET tables/TodoTask/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<TodoTask> GetTodoTask(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/TodoTask/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<TodoTask> PatchTodoTask(string id, Delta<TodoTask> patch)
        {
             return UpdateAsync(id, patch);
        }

        // POST tables/TodoTask/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public async Task<IHttpActionResult> PostTodoTask(TodoTask item)
        {
            TodoTask current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/TodoTask/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteTodoTask(string id)
        {
             return DeleteAsync(id);
        }

    }
}
using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace TodoAzurePcl
{
	public interface IRepository<T> where T : class
	{
		Task<T> GetTaskAsync(string id);
		Task<List<T>> GetTasksAsync ();
		Task SaveTaskAsync (T item);
		Task DeleteTaskAsync (T item);
		Task SyncAsync();
	}
}


using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace TodoAzurePcl
{
	public interface IRepository<T> where T : class
	{
		Task<T> GetItemAsync(string id);
		Task<List<T>> GetAllItemsAsync ();
		Task SaveItemAsync (T item);
		Task DeleteItemAsync (T item);
		Task SyncItemsAsync();
	}
}


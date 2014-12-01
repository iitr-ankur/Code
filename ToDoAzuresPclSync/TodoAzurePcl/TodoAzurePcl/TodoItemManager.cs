using System;
using Microsoft.WindowsAzure.MobileServices;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.WindowsAzure.MobileServices.Sync;

namespace TodoAzurePcl
{
	public class TodoItemManager {

		IMobileServiceSyncTable<TodoItem> todoTable;
		IMobileServiceClient client;

		public TodoItemManager (IMobileServiceClient client, IMobileServiceSyncTable<TodoItem> todoTable) 
		{
			this.todoTable = todoTable;
			this.client = client;
		}

		public async Task<TodoItem> GetTaskAsync(string id)
		{
			try 
			{
				await SyncAsync();
				return await todoTable.LookupAsync(id);
			} 
			catch (MobileServiceInvalidOperationException msioe)
			{
				Debug.WriteLine(@"INVALID {0}", msioe.Message);
			}
			catch (Exception e) 
			{
				Debug.WriteLine(@"ERROR {0}", e.Message);
			}
			return null;
		}

		public async Task<List<TodoItem>> GetTasksAsync ()
		{
			try 
			{
				await SyncAsync();
				return new List<TodoItem> (await todoTable.ReadAsync());
			} 
			catch (MobileServiceInvalidOperationException msioe)
			{
				Debug.WriteLine(@"INVALID {0}", msioe.Message);
			}
			catch (Exception e) 
			{
				Debug.WriteLine(@"ERROR {0}", e.Message);
			}
			return null;
		}

		public async Task SaveTaskAsync (TodoItem item)
		{
			try {
				if (item.Id == null)
					await todoTable.InsertAsync (item);
				else
					await todoTable.UpdateAsync (item);
				
				await SyncAsync ();
			}
			catch (MobileServiceInvalidOperationException msioe)
			{
				Debug.WriteLine(@"INVALID {0}", msioe.Message);
			}
			catch (Exception e) 
			{
				Debug.WriteLine(@"ERROR {0}", e.Message);
			}
		}

		public async Task DeleteTaskAsync (TodoItem item)
		{
			try 
			{
				await todoTable.DeleteAsync(item);
				await SyncAsync();
			} 
			catch (MobileServiceInvalidOperationException msioe)
			{
				Debug.WriteLine(@"INVALID {0}", msioe.Message);
			}
			catch (Exception e) 
			{
				Debug.WriteLine(@"ERROR {0}", e.Message);
			}
		}

		public async Task SyncAsync()
		{
			try
			{
				await this.client.SyncContext.PushAsync();
				await this.todoTable.PullAsync("pullAll", todoTable.CreateQuery());
			}
			catch (MobileServiceInvalidOperationException e)
			{
				Debug.WriteLine(@"Sync Failed: {0}", e.Message);
			}
		}
	}
}


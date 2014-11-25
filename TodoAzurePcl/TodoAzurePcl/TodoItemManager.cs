using System;
using Microsoft.WindowsAzure.MobileServices;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace TodoAzurePcl
{
	public class TodoItemManager {

		IMobileServiceTable<TodoItem> todoTable;

		public TodoItemManager (IMobileServiceTable<TodoItem> todoTable) 
		{
			this.todoTable = todoTable;
		}

		public async Task<TodoItem> GetTaskAsync(string id)
		{
			try 
			{
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
			if (item.Id == null)
				await todoTable.InsertAsync(item);
			else
				await todoTable.UpdateAsync(item);
		}

		public async Task DeleteTaskAsync (TodoItem item)
		{
			try 
			{
				await todoTable.DeleteAsync(item);
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
	}
}


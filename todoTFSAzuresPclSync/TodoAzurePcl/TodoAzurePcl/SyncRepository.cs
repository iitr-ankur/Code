using System;
using Microsoft.WindowsAzure.MobileServices.Sync;
using Microsoft.WindowsAzure.MobileServices;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Diagnostics;

namespace TodoAzurePcl
{
	public class SyncRepository<T> : IRepository<T> where T : EntityData
	{
		IMobileServiceSyncTable<T> syncTable;
		IMobileServiceClient client;

		public SyncRepository (IMobileServiceClient client, IMobileServiceSyncTable<T> syncTable) 
		{
			this.syncTable = syncTable;
			this.client = client;
		}

		public async Task<T> GetItemAsync(string id)
		{
			try 
			{
				var item = await syncTable.LookupAsync(id);
				if(item == null)
				{
					await SyncItemsAsync();
					item = await syncTable.LookupAsync(id);
				}
				return item;
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

		public async Task<List<T>> GetAllItemsAsync ()
		{
			try 
			{
				SyncItemsAsync();
				return new List<T> (await syncTable.ReadAsync());
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

		public async Task SaveItemAsync (T item)
		{
			try {
				if (item.Id == null)
					await syncTable.InsertAsync (item);
				else
					await syncTable.UpdateAsync (item);

				SyncItemsAsync ();
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

		public async Task DeleteItemAsync (T item)
		{
			try 
			{
				await syncTable.DeleteAsync(item);
				SyncItemsAsync();
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

		public async Task SyncItemsAsync()
		{
			try
			{
				await this.client.SyncContext.PushAsync();
				await this.syncTable.PullAsync("pullAll", syncTable.CreateQuery());
			}
			catch (MobileServiceInvalidOperationException e)
			{
				Debug.WriteLine(@"Sync Failed: {0}", e.Message);
			}
		}
	}
}


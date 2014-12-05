using System;

namespace TodoAzurePcl
{
	using System;
	using Microsoft.WindowsAzure.MobileServices.Sync;
	using Microsoft.WindowsAzure.MobileServices;
	using System.Threading.Tasks;
	using System.Collections.Generic;
	using System.Diagnostics;

	namespace TodoAzurePcl
	{
		public class ThreadRepository
		{
			IMobileServiceSyncTable<Thread> threadsSyncTable;
			IMobileServiceClient client;
			string groupId;

			public ThreadRepository (IMobileServiceClient client, IMobileServiceSyncTable<Thread> threadsSyncTable, string groupId) 
			{
				this.threadsSyncTable = threadsSyncTable;
				this.client = client;
				this.groupId = groupId;
			}

			public async Task<Thread> GetTaskAsync(string id)
			{
				try 
				{
					var result = await threadsSyncTable.LookupAsync(id);
					SyncAsync();
					return result;
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

			public async Task<List<Thread>> GetTasksAsync ()
			{
				try 
				{
					var result = new List<Thread> (await threadsSyncTable.ReadAsync(threadsSyncTable.Where(p => p.GroupId == groupId)));
					SyncAsync();
					return result;
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

			public async Task SaveTaskAsync (Thread thread)
			{
				try {
					if (thread.Id == null)
						await threadsSyncTable.InsertAsync (thread);
					else
						await threadsSyncTable.UpdateAsync (thread);

					this.client.SyncContext.PushAsync();
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

			public async Task DeleteTaskAsync (Thread item)
			{
				try 
				{
					await threadsSyncTable.DeleteAsync(item);
					this.client.SyncContext.PushAsync();
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
					//await this.client.SyncContext.PushAsync();
					await this.threadsSyncTable.PullAsync("pullAll", threadsSyncTable.Where(p => p.GroupId == groupId));
				}
				catch (MobileServiceInvalidOperationException e)
				{
					Debug.WriteLine(@"Sync Failed: {0}", e.Message);
				}
				catch(Exception ex) {
					Debug.WriteLine ("Sync Failed: {0}", ex.ToString ());
				}
			}

			public async Task PurgeAsync()
			{
				this.threadsSyncTable.PurgeAsync (threadsSyncTable.Where (p => p.__createdAt != null).OrderBy (p => p.__createdAt).Take (3));
			}
		}
	}


}


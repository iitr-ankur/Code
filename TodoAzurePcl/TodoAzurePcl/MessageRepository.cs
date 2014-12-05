using System;
using System;
using Microsoft.WindowsAzure.MobileServices.Sync;
using Microsoft.WindowsAzure.MobileServices;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Diagnostics;

namespace TodoAzurePcl
{
	public class MessageRepository
	{
		IMobileServiceSyncTable<Message> messagesSyncTable;
		IMobileServiceClient client;

		public MessageRepository (IMobileServiceClient client, IMobileServiceSyncTable<Message> messagesSyncTable) 
		{
			this.messagesSyncTable = messagesSyncTable;
			this.client = client;
		}

		public async Task<List<Message>> GetTasksAsync (string threadId)
		{
			try 
			{
				var result = new List<Message> (await messagesSyncTable.ReadAsync(messagesSyncTable.Where(p => p.ThreadId == threadId)));
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

		public async Task SaveTaskAsync (Message message)
		{
			try {
				if (message.Id == null)
					await messagesSyncTable.InsertAsync (message);
				else
					await messagesSyncTable.UpdateAsync (message);
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

		public async Task DeleteTaskAsync (Message item)
		{
			try 
			{
				await messagesSyncTable.DeleteAsync(item);
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
				await this.messagesSyncTable.PullAsync("pullAll", messagesSyncTable.CreateQuery());
			}
			catch (MobileServiceInvalidOperationException e)
			{
				Debug.WriteLine(@"Sync Failed: {0}", e.Message);
			}
		}
	}
}


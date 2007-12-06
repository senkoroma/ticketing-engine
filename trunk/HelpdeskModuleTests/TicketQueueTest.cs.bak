using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using HelpdeskModule;
using System.Web.Security;
using System.Diagnostics;

namespace HelpdeskModuleTests
{
	[TestFixture]
	public class TicketQueueTest : AbstractDaoTest
	{
		#region ----------------  Test Setup  ----------------

		public static DateTime TestDate
		{
			get { return DateTime.Now; }
		}

		/// <summary>
		/// Creates shiny new queues that are not referenced / altered by other methods.
		/// </summary>
		/// <returns></returns>
		private TicketQueue NewQueue()
		{
			TicketQueue expected = new TicketQueue();
			expected.CreationDate = TestDate;
			expected.Creator = "nblevins";
			expected.Description = "This is my testing description of this Queue.";
			expected.IsActive = true;
			expected.Name = "Testing Queue";
			expected.Tickets = null;

			return expected;
		}

		/// <summary>
		/// Helper method to Select Queue from database by id, returns null if record is not found.
		/// </summary>
		private TicketQueue SelectQueueById(int TicketQueueId)
		{
			return HelpdeskService.GetQueue(TicketQueueId);
		}

		/// <summary>
		/// Helper method to insert values into the database.  If successful, it will set the appropriate row id.
		/// </summary>
		/// <param name="Module"></param>
		private void InsertQueueIntoDatabase(TicketQueue queue)
		{
			HelpdeskService.CreateQueue(queue);
		}

		protected override void TestInitialize() { }

		protected override void Cleanup() { }

		#endregion

		[Test]
		[Category("QueueTest")]
		public void CreateQueueTest()
		{
			TicketQueue temp = NewQueue();

			HelpdeskService.CreateQueue(temp);

			Trace.WriteLine(temp.QueueId);

			Assert.IsTrue(SelectQueueById(temp.QueueId) != null, "The internal selection query used to verify this test failed to return the a row.");
			Assert.IsTrue(temp.QueueId > 0, "The returned Id from the CreateQueue test did not return a value greater than 0.");
		}

		[Test]
		[Category("QueueTest")]
		public void UpdateQueueTest()
		{
			TicketQueue temp = NewQueue();
			InsertQueueIntoDatabase(temp);

			//Make sure the insertion worked smoothly.
			Assert.IsTrue(temp.Equals(SelectQueueById(temp.QueueId)), "The created queue and selected queue do not match.  Insertion or selection might have failed");

			//Change the values...
			temp.Name = "Testing Queue2";
			temp.Description = "New Description";
			temp.IsActive = false;
			temp.Tickets = null;
			temp.Creator = "blevinsn";
			temp.CreationDate = DateTime.Now;

			//Peform the update.
			HelpdeskService.UpdateQueue(temp);

			//Create a new instance of the module object and compare them...
			TicketQueue temp2 = SelectQueueById(temp.QueueId);
			//Make sure they match.
			Assert.IsTrue(temp.Equals(temp2), "The updated queue did not match equality with the prepared module values in the method.");
		}

		[Test]
		[Category("QueueTest")]
		public void DeleteQueueTest()
		{
			TicketQueue temp = NewQueue();

			InsertQueueIntoDatabase(temp);

			//Make sure the insert and select are working.
			Assert.IsTrue(SelectQueueById(temp.QueueId) != null, "The select query failed to return any results.");

			HelpdeskService.DeleteQueue(temp.QueueId);

			Assert.IsTrue(SelectQueueById(temp.QueueId) == null, "The selection returned a row, meaning that the delete statmen failed.");
		}

		[Test]
		[Category("QueueTest")]
		public void GetAllQueuesTest()
		{

			TicketQueueCollection CreatedSet = new TicketQueueCollection();

			//Select everything in the database.
			TicketQueueCollection PreSelectionSet = HelpdeskService.GetAllQueues();

			//Add the new items into the database and keep of collection of them for deletion later...
			for (int x = 0; x < 10; x++)
			{
				TicketQueue temp = NewQueue();

				HelpdeskService.CreateQueue(temp);
				CreatedSet.Add(temp);
			}

			//Get teh values of everything in teh datbase now that we have done some insertions.
			TicketQueueCollection PostSelectionSet = HelpdeskService.GetAllQueues();

			//Check their counts to make sure everything went into the database correctly.
			Assert.IsTrue((PreSelectionSet.Count + 10) == PostSelectionSet.Count);
		}

		[Test]
		[Category("QueueTest")]
		public void GetAllEnabledQueuesTest()
		{
			TicketQueueCollection CreatedSet = new TicketQueueCollection();

			//Select everything in teh database.
			TicketQueueCollection PreSelectionSet = HelpdeskService.GetAllEnabledQueues();

			//Add the new items into the database and keep of collection of them for deletion later...
			for (int x = 0; x < 10; x++)
			{
				TicketQueue temp = NewQueue();

				HelpdeskService.CreateQueue(temp);
				CreatedSet.Add(temp);
			}

			//Get teh values of everything in teh datbase now that we have done some insertions.
			TicketQueueCollection PostSelectionSet = HelpdeskService.GetAllEnabledQueues();

			//Check their counts to make sure everything went into the database correctly.
			Assert.IsTrue((PreSelectionSet.Count + 10) == PostSelectionSet.Count);
		}
	}
}

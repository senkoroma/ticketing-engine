using System;
using System.Collections.Generic;
using System.Text;
using HelpdeskModule;
using NUnit.Framework;
using System.Diagnostics;

namespace HelpdeskModuleTests
{
	[TestFixture]
	public class TicketTest : AbstractDaoTest
	{
		#region ----------------  Test Setup  ----------------

		/// <summary>
		/// Creates shiny new queues that are not referenced / altered by other methods.
		/// </summary>
		/// <returns></returns>
		private Ticket NewTicket()
		{
			//Make the complex objects and retunr their ID.
			TicketQueue que = new TicketQueue(0, "nblevins", "description", "QueueName", TestDate, true);
			HelpdeskService.CreateQueue(que);

			TicketCategory cat = new TicketCategory(0, "Category Name", "Category Description", true, que.QueueId);
			HelpdeskService.CreateCategory(cat);

			TicketModule mod = new TicketModule(0, que.QueueId, "Module Name", "Description", true);
			HelpdeskService.CreateModule(mod);

			Requestor req = new Requestor(0, "nbleivns", "Blevins", "444-444-4444", "asdf@asdf.com");
			HelpdeskService.CreateRequestor(req);

			TicketStatus stat = new TicketStatus(0, "Status Name", "Description", 1, true);
			HelpdeskService.CreateStatus(stat);


			//Set up the ticket...
			Ticket temp = new Ticket();
			temp.Category = cat;
			temp.CreationDate = TestDate;
			temp.Creator = "nblevins";
			temp.Description = "This is my ticket decription.";
			temp.DueDate = TestDate + new TimeSpan(2, 0, 0);
			temp.Module = mod;
			temp.Priority = TicketPriority.Medium;
			temp.Requestor = req;
			temp.Responses = new TicketResponseCollection();
			temp.Status = stat;
			temp.Assignment = new AssignmentCollection();
			temp.Queue = que;
			
			//Build the company...

			Company comp = new Company();
			comp.Address1 = "addy1";
			comp.Address2 = "addy2";
			comp.City = "some city";
			comp.ContactNumber1 = "444-444-4444";
			comp.ContactNumber2 = "322-333-3333";
			comp.Name = "New Company";
			comp.ParentId = 0;
			comp.State = "TN";
			comp.Website = "www.sworps.com";
			comp.Zip_Code = "33333-3333";

			Requestor MainReq = new Requestor();
			MainReq.ContactNumber = "555-555-5555";
			MainReq.Email = "asdf@asdf.com";
			MainReq.FirstName = "nathan 1";
			MainReq.LastName = "bleivns 1";
			HelpdeskService.CreateRequestor(MainReq);

			comp.MainContact = MainReq;

			Requestor SecReq = new Requestor();
			SecReq.ContactNumber = "555-222-5555";
			SecReq.Email = "2222@asdf.com";
			SecReq.FirstName = "nathan 2";
			SecReq.LastName = "bleivns 2";
			HelpdeskService.CreateRequestor(SecReq);

			comp.SecondaryContact = SecReq;

			temp.Company = comp;
			return temp;
		}

		/// <summary>
		/// Helper method to Select Ticket from database by id, returns null if record is not found.
		/// </summary>
		private Ticket SelectTicketById(int TicketId)
		{
			return HelpdeskService.GetTicket(TicketId, true);
		}

		/// <summary>
		/// Helper method to insert values into the database.  If successful, it will set the appropriate row id.
		/// </summary>
		/// <param name="Module"></param>
		private void InsertTicketIntoDatabase(Ticket ticket)
		{
			HelpdeskService.CreateTicket(ticket);
		}

		protected override void TestInitialize() { }

		protected override void Cleanup() { }

		#endregion

		//TODO:  Write unit tests for Load ALL DATA... 

		[Test]
		[Category("Ticket Test")]
		public void CreateTicketTest_BasicData()
		{
			Ticket temp = NewTicket();

			HelpdeskService.CreateTicket(temp);

			Trace.WriteLine(temp.TicketId);

			Assert.IsTrue(SelectTicketById(temp.TicketId) != null, "The internal selection query used to verify this test failed to return the a row.");
			Assert.IsTrue(temp.TicketId > 0, "The returned Id from the CreateTicket test did not return a value greater than 0.");
		}

		[Test]
		[Category("Ticket Tests")]
		public void UpdateTicketTest()
		{
			Ticket temp = NewTicket();
			InsertTicketIntoDatabase(temp);

			int TicketId = temp.TicketId;

			//Make sure the insertion worked smoothly.
			Assert.IsTrue(temp.Equals(SelectTicketById(TicketId)), "The created ticket and selected status do not match.  Insertion or selection might have failed");

			Ticket ChangedTicket = NewTicket();

			//Change the values...
			ChangedTicket.CreationDate = DateTime.Now;
			ChangedTicket.Creator = "new creator";
			ChangedTicket.DueDate = DateTime.Now;
			ChangedTicket.Priority = TicketPriority.High;

			temp = ChangedTicket;
			temp.TicketId = TicketId;

			//Peform the update.
			HelpdeskService.UpdateTicket(temp);

			//Create a new instance of the module object and compare them...
			Ticket temp2 = SelectTicketById(TicketId);
			//Make sure they match.
			Assert.IsTrue(temp.Equals(temp2), "The updated status did not match equality with the prepared module values in the method.");
		}

		[Test]
		[Category("Ticket Tests")]
		public void DeleteTicketTest()
		{
			Ticket temp = NewTicket();

			InsertTicketIntoDatabase(temp);

			//Make sure the insert and select are working.
			Assert.IsTrue(SelectTicketById(temp.TicketId) != null, "The select query failed to return any results.");

			HelpdeskService.DeleteTicket(temp.TicketId);

			Assert.IsTrue(SelectTicketById(temp.TicketId) == null, "The selection returned a row, meaning that the delete statmen failed.");
		}

		[Test]
		[Category("Ticket Test")]
		public void GetAllTicketsByQueueIdTest()
		{

			TicketCollection CreatedSet = new TicketCollection();

			//Select everything in the database.
			TicketCollection PreSelectionSet = HelpdeskService.GetTicketsByQueueId(1, true);

			//Add the new items into the database and keep of collection of them for deletion later...
			for (int x = 0; x < 10; x++)
			{
				Ticket temp = NewTicket();
				temp.Queue.QueueId = 1;

				HelpdeskService.CreateTicket(temp);
				CreatedSet.Add(temp);
			}

			//Get teh values of everything in teh datbase now that we have done some insertions.
			TicketCollection PostSelectionSet = HelpdeskService.GetTicketsByQueueId(1, true);

			//Check their counts to make sure everything went into the database correctly.
			Assert.IsTrue((PreSelectionSet.Count + 10) >= PostSelectionSet.Count);
		}

	}
}

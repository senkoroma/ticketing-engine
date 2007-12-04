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

		public static DateTime TestDate
		{
			get { return DateTime.Now; }
		}

		/// <summary>
		/// Creates shiny new queues that are not referenced / altered by other methods.
		/// </summary>
		/// <returns></returns>
		private Ticket NewTicket()
	    {
	        TicketCategory cat = new TicketCategory();
	        cat.TicketCategoryId = 1;

	        TicketModule mod = new TicketModule();
	        mod.TicketModuleId = 2;

			Requestor req = new Requestor();
			req.RequestorId = 1;

			TicketStatus stat = new TicketStatus();
			stat.TicketStatusId = 1;

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

		[Test]
		[Category("Ticket Test")]
		public void CreateTicketTest()
		{
			Ticket temp = NewTicket();

			HelpdeskService.CreateTicket(temp);

			Trace.WriteLine(temp.TicketId);

			Assert.IsTrue(SelectTicketById(temp.TicketId) != null, "The internal selection query used to verify this test failed to return the a row.");
			Assert.IsTrue(temp.TicketId > 0, "The returned Id from the CreateTicket test did not return a value greater than 0.");
		}

	}
}

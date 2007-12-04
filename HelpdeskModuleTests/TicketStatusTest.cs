using System;
using System.Collections.Generic;
using System.Text;
using HelpdeskModule;
using NUnit.Framework;
using System.Diagnostics;

namespace HelpdeskModuleTests
{
	[TestFixture]
	public class TicketStatusTest : AbstractDaoTest
	{
		#region ---------------  Test Setup  ---------------

		/// <summary>
		/// Helper method to Select Module from database by id, returns null if record is not found.
		/// </summary>
		private TicketStatus SelectStatusId(int TicketStatusId)
		{
			return HelpdeskService.GetStatusById(TicketStatusId);
		}

		/// <summary>
		/// Helper method to insert values into the database.  If successful, it will set teh appropriate row id.
		/// </summary>
		/// <param name="Module"></param>
		private void InsertStatusIntoDatabase(TicketStatus Status)
		{
			HelpdeskService.CreateStatus(Status);
		}

		/// <summary>
		/// Creates shiny new modules that are not referenced / altered by other methods.
		/// </summary>
		/// <returns></returns>
		private TicketStatus NewStatus()
		{
			//Initialize a basic module object.
			TicketStatus Status = new TicketStatus();
			Status.Description = "This is my status descrition";
			Status.IsActive = true;
			Status.Name = "Ticket Status Name";
			Status.StatusOrder = 1;

			return Status;
		}

		protected override void TestInitialize()
		{
		}

		protected override void Cleanup()
		{
		}

		#endregion

		/// <summary>
		/// Tests the insertions of the module data into the database.
		/// </summary>
		[Test]
		[Category("Status Tests")]
		public void CreateStatusTest()
		{
			TicketStatus temp = NewStatus();

			InsertStatusIntoDatabase(temp);

			//Make sure it returns a row id and that that record exists.
			Assert.IsTrue(temp.TicketStatusId > 0, "The TicketStatus insertion did not return a row id");

			Assert.IsTrue(SelectStatusId(temp.TicketStatusId) != null, "The internal selection query used to verify this test failed to return the a row.");
		}

		[Test]
		[Category("Status Tests")]
		public void EditStatusTest()
		{
			TicketStatus temp = NewStatus();
			InsertStatusIntoDatabase(temp);

			//Make sure the insertion worked smoothly.
			Assert.IsTrue(temp.Equals(SelectStatusId(temp.TicketStatusId)), "The created status and selected status do not match.  Insertion or selection might have failed");

			//Change the values...
			temp.Name = "New Module name!";
			temp.Description = "New Module Description";
			temp.IsActive = false;
			temp.StatusOrder = 3;

			//Peform the update.
			HelpdeskService.EditStatus(temp);

			//Create a new instance of the module object and compare them...
			TicketStatus temp2 = SelectStatusId(temp.TicketStatusId);
			//Make sure they match.
			Assert.IsTrue(temp.Equals(temp2), "The updated status did not match equality with the prepared module values in the method.");
		}

		[Test]
		[Category("Status Test")]
		public void GetAllStatusTest()
		{
			TicketStatusCollection tempCol = new TicketStatusCollection();

			//Create a new module, insert it into the database, and then insert it into the Module Collection.
			for (int x = 0; x < 10; x++)
			{
				TicketStatus tempStatus = NewStatus();
				InsertStatusIntoDatabase(tempStatus);
				tempCol.Add(tempStatus);
			}

			//Get all Modules...
			TicketStatusCollection tempCol2 = HelpdeskService.GetAllStatus();
			foreach (TicketStatus temp in tempCol)
			{
				Assert.IsTrue(tempCol2.Contains(temp), "The inserted collection and the retrived collection did not match in number or type.");
			}

		}

		[Test]
		[Category("Status Test")]
		public void SortOnTooLargeRequestedOrder()
		{
			TicketStatusCollection tempCol = HelpdeskService.GetAllStatus();

			int TooLargeStatusOrder = tempCol.Count + 15;
			int ExpectedStatusOrder = tempCol.Count + 1;

			TicketStatus temp = NewStatus();
			temp.StatusOrder = TooLargeStatusOrder;

			HelpdeskService.CreateStatus(temp);

			Trace.WriteLine(temp.StatusOrder);
			Assert.IsTrue(ExpectedStatusOrder == temp.StatusOrder);
		}

		[Test]
		[Category("Status Test")]
		public void SortOnMiddleRequestedOrder()
		{
			//Add a few status's to the database...
			TicketStatusCollection tempCol = new TicketStatusCollection();
			for (int x = 0; x < 10; x++)
			{
				TicketStatus tempStatus = NewStatus();
				InsertStatusIntoDatabase(tempStatus);
				tempCol.Add(tempStatus);
			}

			//Set up our test status.
			TicketStatus temp = NewStatus();
			temp.StatusOrder = 1;
			HelpdeskService.CreateStatus(temp);
			
			//Fetch all of the status'
			TicketStatusCollection tempCol2 = HelpdeskService.GetAllStatus();

			int MiddleStatusOrder = tempCol2.Count / 2;

			temp.StatusOrder = MiddleStatusOrder;

			HelpdeskService.EditStatus(temp);

			Assert.IsTrue(MiddleStatusOrder == temp.StatusOrder);
		}
	}
}

using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using HelpdeskModule;

namespace HelpdeskModuleTests
{
	[TestFixture]
	public class TicketResponseTest : AbstractDaoTest
	{
		#region ----------------  Test Setup  ----------------

		/// <summary>
		/// Creates shiny new queues that are not referenced / altered by other methods.
		/// </summary>
		/// <returns></returns>
		private TicketResponse NewResponse()
		{
			TicketResponse expected = new TicketResponse();
			expected.CreationDate = TestDate;
			expected.Creator = "nblevins";
			expected.MinsSpent = 33;
			expected.Response = "This is my response...";
			expected.TicketId = 1;

			return expected;
		}

		/// <summary>
		/// Helper method to Select Response from database by id, returns null if record is not found.
		/// </summary>
		private TicketResponse SelectResponseById(int TicketResponseId)
		{
			return HelpdeskService.GetResponse(TicketResponseId);
		}

		/// <summary>
		/// Helper method to insert values into the database.  If successful, it will set the appropriate row id.
		/// </summary>
		/// <param name="Category"></param>
		private void InsertResponseIntoDatabase(TicketResponse response)
		{
			HelpdeskService.CreateResponse(response);
		}

		protected override void TestInitialize() { }

		protected override void Cleanup() { }

		#endregion

		[Test]
		[Category("Response Test")]
		public void CreateResponseTest()
		{
			TicketResponse temp = NewResponse();

			HelpdeskService.CreateResponse(temp);

			Assert.IsTrue(SelectResponseById(temp.TicketResponseId) != null, "The internal selection query used to verify this test failed to return the a row.");
			Assert.IsTrue(temp.TicketResponseId > 0, "The returned Id from the CreateQueue test did not return a value greater than 0.");
		}

		[Test]
		[Category("Response Test")]
		public void EditResponseTest()
		{
			TicketResponse temp = NewResponse();
			InsertResponseIntoDatabase(temp);

			//Make sure the insertion worked smoothly.
			Assert.IsTrue(temp.Equals(SelectResponseById(temp.TicketResponseId)), "The created Category and selected Category do not match.  Insertion or selection might have failed");

			//Change the values...
			temp.CreationDate = DateTime.Now;
			temp.Creator = "me me me!";
			temp.MinsSpent = 22032;
			temp.Response = "new Response";
			temp.TicketId = 33;

			//Peform the update.
			HelpdeskService.UpdateResponse(temp);

			//Create a new instance of the Category object and compare them...
			TicketResponse temp2 = SelectResponseById(temp.TicketResponseId);
			//Make sure they match.
			Assert.IsTrue(temp.Equals(temp2), "The updated Category did not match equality with the prepared Category values in the method.");
		}

		[Test]
		[Category("Response Test")]
		public void DeleteResponseTest()
		{
			TicketResponse temp = NewResponse();

			InsertResponseIntoDatabase(temp);

			//Make sure the insert and select are working.
			Assert.IsTrue(SelectResponseById(temp.TicketResponseId) != null, "The select query failed to return any results.");

			HelpdeskService.DeleteResponse(temp.TicketResponseId);

			Assert.IsTrue(SelectResponseById(temp.TicketResponseId) == null, "The selection returned a row, meaning that the delete statmen failed.");
		}

		[Test]
		[Category("Response Test")]
		public void GetResponseByTicketIdTest()
		{
			TicketResponseCollection tempRespCol = new TicketResponseCollection();

			//Create a new module, insert it into the database, and then insert it into the Module Collection.
			for (int x = 0; x < 10; x++)
			{
				TicketResponse tempResp = NewResponse();
				tempResp.TicketId = 1;
				InsertResponseIntoDatabase(tempResp);
				tempRespCol.Add(tempResp);
			}

			//Get all Modules...
			TicketResponseCollection tempRespCol2 = HelpdeskService.GetResponsesByTicketId(1);
			foreach (TicketResponse temp in tempRespCol)
			{
				Assert.IsTrue(tempRespCol2.Contains(temp));
			}

			Assert.IsTrue(tempRespCol2.Count >= tempRespCol.Count);

		}

	}
}

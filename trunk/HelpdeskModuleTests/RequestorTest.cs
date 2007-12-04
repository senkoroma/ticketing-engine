using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using HelpdeskModule;
using System.Diagnostics;

namespace HelpdeskModuleTests
{
	[TestFixture]
	public class RequestorTest : AbstractDaoTest
	{

		#region ----------------  Test Setup  ----------------

		public static DateTime TestDate
		{
			get { return DateTime.Now; }
		}

		/// <summary>
		/// Creates shiny new requestor that are not referenced / altered by other methods.
		/// </summary>
		/// <returns></returns>
		private Requestor NewRequestor()
		{
			Requestor temp = new Requestor();
			temp.ContactNumber = "444-444-4444";
			temp.Email = "temp@temp.com";
			temp.FirstName = "Nathan";
			temp.LastName = "Blevins";
			
			return temp;
		}

		/// <summary>
		/// Helper method to Select Queue from database by id, returns null if record is not found.
		/// </summary>
		private Requestor SelectRequestorById(int RequestorId)
		{
			return HelpdeskService.GetRequestorById(RequestorId);
		}

		/// <summary>
		/// Helper method to insert values into the database.  If successful, it will set the appropriate row id.
		/// </summary>
		/// <param name="Module"></param>
		private void InsertRequestorIntoDatabase(Requestor requestor)
		{
			HelpdeskService.CreateRequestor(requestor);
		}

		protected override void TestInitialize() { }

		protected override void Cleanup() { }

		#endregion

		[Test]
		[Category("Requestor Test")]
		public void CreateRequestorTest()
		{
			Requestor temp = NewRequestor();

			HelpdeskService.CreateRequestor(temp);

			Trace.WriteLine(temp.RequestorId);

			Assert.IsTrue(SelectRequestorById(temp.RequestorId) != null, "The internal selection query used to verify this test failed to return the a row.");
			Assert.IsTrue(temp.RequestorId > 0, "The returned Id from the CreateQueue test did not return a value greater than 0.");
		}

		[Test]
		[Category("Requestor Test")]
		public void EditRequestorTest()
		{
			Requestor temp = NewRequestor();
			InsertRequestorIntoDatabase(temp);

			//Make sure the insertion worked smoothly.
			Assert.IsTrue(temp.Equals(SelectRequestorById(temp.RequestorId)), "The created queue and selected queue do not match.  Insertion or selection might have failed");

			//Change the values...
			temp.FirstName = "Nathan 2";
			temp.LastName = "Blevins 2";
			temp.ContactNumber = "423-432-4524";
			temp.Email = "email@dns.com";

			//Peform the update.
			HelpdeskService.EditRequestor(temp);

			//Create a new instance of the module object and compare them...
			Requestor temp2 = SelectRequestorById(temp.RequestorId);
			//Make sure they match.
			Assert.IsTrue(temp.Equals(temp2), "The updated queue did not match equality with the prepared module values in the method.");
		}

		[Test]
		[Category("Requestor Test")]
		public void GetAllRequestorTest()
		{
			RequestorCollection tempCol = new RequestorCollection();

			//Create a new Category, insert it into the database, and then insert it into the Category Collection.
			for (int x = 0; x < 10; x++)
			{
				Requestor temp = NewRequestor();
				InsertRequestorIntoDatabase(temp);
				tempCol.Add(temp);
			}

			//Get all Categorys...
			RequestorCollection tempCol2 = HelpdeskService.GetAllRequestor();
			foreach (Requestor temp in tempCol)
			{
				Assert.IsTrue(tempCol2.Contains(temp));
			}

		}
	}
}

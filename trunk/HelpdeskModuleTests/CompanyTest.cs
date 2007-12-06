using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using HelpdeskModule;
using System.Diagnostics;

namespace HelpdeskModuleTests
{
	[TestFixture]
	public class CompanyTest : AbstractDaoTest
	{
		#region ----------------  Test Setup  ----------------

		/// <summary>
		/// Creates shiny new queues that are not referenced / altered by other methods.
		/// </summary>
		/// <returns></returns>
		private Company NewCompany()
		{
			Company expected = new Company();
			expected.Address1 = "addy1";
			expected.Address2 = "addy2";
			expected.City = "some city";
			expected.ContactNumber1 = "444-444-4444";
			expected.ContactNumber2 = "322-333-3333";
			expected.Name = "New Company";
			expected.ParentId = 0;
			expected.State = "TN";
			expected.Website = "www.sworps.com";
			expected.Zip_Code = "33333-3333";

			//Complex objects...

			Requestor MainReq = new Requestor();
			MainReq.ContactNumber = "555-555-5555";
			MainReq.Email = "asdf@asdf.com";
			MainReq.FirstName = "nathan 1";
			MainReq.LastName = "bleivns 1";
			HelpdeskService.CreateRequestor(MainReq);

			expected.MainContact = MainReq;

			Requestor SecReq = new Requestor();
			SecReq.ContactNumber = "555-222-5555";
			SecReq.Email = "2222@asdf.com";
			SecReq.FirstName = "nathan 2";
			SecReq.LastName = "bleivns 2";
			HelpdeskService.CreateRequestor(SecReq);

			expected.SecondaryContact = SecReq;


			return expected;
		}

		/// <summary>
		/// Helper method to Select Queue from database by id, returns null if record is not found.
		/// </summary>
		private Company SelectCompanyById(int CompanyId)
		{
			return HelpdeskService.GetCompanyById(CompanyId);
		}

		/// <summary>
		/// Helper method to insert values into the database.  If successful, it will set the appropriate row id.
		/// </summary>
		/// <param name="Category"></param>
		private void InsertCompanyIntoDatabase(Company company)
		{
			HelpdeskService.CreateCompany(company);
		}

		protected override void TestInitialize() { }

		protected override void Cleanup() { }

		#endregion

		[Test]
		[Category("Company Test")]
		public void CreateCompanyTest()
		{
			Company temp = NewCompany();

			HelpdeskService.CreateCompany(temp);

			Trace.WriteLine(temp.CompanyId);

			Assert.IsTrue(SelectCompanyById(temp.CompanyId) != null, "The internal selection query used to verify this test failed to return the a row.");
			Assert.IsTrue(temp.CompanyId > 0, "The returned Id from the CreateQueue test did not return a value greater than 0.");
		}

		[Test]
		[Category("Company Test")]
		public void EditCompanyTest()
		{
			Company temp = NewCompany();
			InsertCompanyIntoDatabase(temp);

			//Make sure the insertion worked smoothly.
			Assert.IsTrue(temp.Equals(SelectCompanyById(temp.CompanyId)), "The created Category and selected Category do not match.  Insertion or selection might have failed");

			//Change the values...
			temp.Address1 = "--- ---- ----";
			temp.Address2 = "Addy 2 again!";
			temp.City = "newer city";
			temp.ContactNumber1 = "11";
			temp.ContactNumber2 = "11";
			temp.Name = "newer company name";
			temp.ParentId = 0;
			temp.State = "AK";
			temp.Website = "www.google.com";
			temp.Zip_Code = "55555";

			//Peform the update.
			HelpdeskService.UpdateCompany(temp);

			//Create a new instance of the Category object and compare them...
			Company temp2 = SelectCompanyById(temp.CompanyId);
			//Make sure they match.
			Assert.IsTrue(temp.Equals(temp2), "The updated Category did not match equality with the prepared Category values in the method.");
		}

	}
}

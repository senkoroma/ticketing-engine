using System;
using System.Collections.Generic;
using System.Text;
using HelpdeskModule;
using NUnit.Framework;

namespace HelpdeskModuleTests
{
	[TestFixture]
	public class ChargeCodeTest : AbstractDaoTest
	{

		#region --------------  Test Setup  --------------

		/// <summary>
		/// Helper method to Select Module from database by id, returns null if record is not found.
		/// </summary>
		private TicketChargeCode SelectChargeCodeById(int ChargeCodeId)
		{
			return HelpdeskService.GetChargeCodeById(ChargeCodeId);
		}

		/// <summary>
		/// Helper method to insert values into the database.  If successful, it will set teh appropriate row id.
		/// </summary>
		/// <param name="Module"></param>
		private void InsertChargeCodeIntoDatabase(TicketChargeCode ChargeCode)
		{
			HelpdeskService.CreateChargeCode(ChargeCode);
		}

		/// <summary>
		/// Creates shiny new modules that are not referenced / altered by other methods.
		/// </summary>
		/// <returns></returns>
		private TicketChargeCode NewChargeCode()
		{
			//Initialize a basic module object.
			TicketChargeCode ChargeCode = new TicketChargeCode();
			ChargeCode.ChargeCode = "1234-1524-AAtd-568d";
			ChargeCode.Description = "This is a dummy charge code.";
			ChargeCode.ExpirationDate = TestDate;
			ChargeCode.IsActive = true;

			return ChargeCode;
		}

		/// <summary>
		/// This method sets up the test itself with the proper vars.  This is called by the abstract class's 
		/// [TestFixtureStartUp] method.
		/// </summary>
		protected override void TestInitialize()
		{
		}

		protected override void Cleanup() { }

		#endregion

		[Test]
		[Category("ChargeCode Tests")]
		public void CreateModuleTest()
		{
			TicketChargeCode temp = NewChargeCode();

			InsertChargeCodeIntoDatabase(temp);

			//Make sure it returns a row id and that that record exists.
			Assert.IsTrue(temp.ChargeCodeId > 0, "The TicketModule insertion did not return a row id");

			Assert.IsTrue(SelectChargeCodeById(temp.ChargeCodeId) != null, "The internal selection query used to verify this test failed to return the a row.");
		}

		[Test]
		[Category("ChargeCode Tests")]
		public void EditModuleTest()
		{
			TicketChargeCode temp = NewChargeCode();
			InsertChargeCodeIntoDatabase(temp);

			//Make sure the insertion worked smoothly.
			Assert.IsTrue(temp.Equals(SelectChargeCodeById(temp.ChargeCodeId)), "The created module and selected module do not match.  Insertion or selection might have failed");

			//Change the values...
			temp.ChargeCode = "111-111-1111";
			temp.Description = "New ChargeCode Description";
			temp.IsActive = false;
			temp.ExpirationDate = DateTime.Now;

			//Peform the update.
			HelpdeskService.EditChargeCode(temp);

			//Create a new instance of the module object and compare them...
			TicketChargeCode temp2 = SelectChargeCodeById(temp.ChargeCodeId);
			//Make sure they match.
			Assert.IsTrue(temp.Equals(temp2), "The updated module did not match equality with the prepared module values in the method.");
		}

		[Test]
		[Category("ChargeCode Test")]
		public void GetAllChargeCodeTest()
		{
			TicketChargeCodeCollection tempChargeCodeCol = new TicketChargeCodeCollection();

			//Create a new ChargeCode, insert it into the database, and then insert it into the ChargeCode Collection.
			for (int x = 0; x < 10; x++)
			{
				TicketChargeCode tempCC = NewChargeCode();
				InsertChargeCodeIntoDatabase(tempCC);
				tempChargeCodeCol.Add(tempCC);
			}

			//Get all ChargeCodes...
			TicketChargeCodeCollection tempChargeCodeCol2 = HelpdeskService.GetAllChargeCode();
			foreach (TicketChargeCode temp in tempChargeCodeCol)
			{
				Assert.IsTrue(tempChargeCodeCol2.Contains(temp));
			}

		}

		[Test]
		[Category("ChargeCode Test")]
		public void GetChargeCodeByActiveTest()
		{
			TicketChargeCodeCollection tempChargeCodeCol = new TicketChargeCodeCollection();

			//Create a new ChargeCode, insert it into the database, and then insert it into the ChargeCode Collection.
			for (int x = 0; x < 10; x++)
			{
				TicketChargeCode tempMod = NewChargeCode();
				tempMod.IsActive = false;
				InsertChargeCodeIntoDatabase(tempMod);
				tempChargeCodeCol.Add(tempMod);
			}

			//Get all ChargeCodes...
			TicketChargeCodeCollection tempChargeCodeCol2 = HelpdeskService.GetAllChargeCodeByActive(false);
			foreach (TicketChargeCode temp in tempChargeCodeCol)
			{
				Assert.IsTrue(tempChargeCodeCol2.Contains(temp));
			}

			Assert.IsTrue(tempChargeCodeCol2.Count >= tempChargeCodeCol.Count);

		}
	}
}

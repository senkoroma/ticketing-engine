using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using HelpdeskModule;
using System.Diagnostics;

namespace HelpdeskModuleTests
{
	[TestFixture]
	public class TicketModuleTest : AbstractDaoTest
	{
		#region --------------  Test Setup  --------------

		/// <summary>
		/// Helper method to Select Module from database by id, returns null if record is not found.
		/// </summary>
		private TicketModule SelectModuleById(int TicketModuleId)
		{
			return HelpdeskService.GetModule(TicketModuleId);
		}
		
		/// <summary>
		/// Helper method to insert values into the database.  If successful, it will set teh appropriate row id.
		/// </summary>
		/// <param name="Module"></param>
		private void InsertModuleIntoDatabase(TicketModule Module)
		{
			HelpdeskService.CreateModule(Module);
		}

		/// <summary>
		/// Creates shiny new modules that are not referenced / altered by other methods.
		/// </summary>
		/// <returns></returns>
		private TicketModule NewModule()
		{
			//Initialize a basic module object.
			TicketModule Module = new TicketModule();
			Module.Description = "This is my testing description";
			Module.Name = "Module Name - Testing";
			Module.TicketQueueId = 1;
			Module.IsActive = true;

			return Module;
		}

		/// <summary>
		/// This method sets up the test itself with the proper vars.  This is called by the abstract class's 
		/// [TestFixtureStartUp] method.
		/// </summary>
		protected override void TestInitialize()
		{
		}


		/// <summary>
		/// This method tears down the test itself.  This is called by the abstract class's 
		/// [TestFixtureTearDown] method.  Transaction.Dispose() is always called in the abstract class's method.
		/// </summary>
		protected override void Cleanup()
		{
		}

		#endregion

		[Test]
		[Category("Module Tests")]
		public void CreateModuleTest()
		{
			TicketModule temp = NewModule();

			InsertModuleIntoDatabase(temp);

			//Make sure it returns a row id and that that record exists.
			Assert.IsTrue(temp.TicketModuleId > 0, "The TicketModule insertion did not return a row id");

			Assert.IsTrue(SelectModuleById(temp.TicketModuleId) != null, "The internal selection query used to verify this test failed to return the a row.");
		}

		[Test]
		[Category("Module Tests")]
		public void EditModuleTest()
		{
			TicketModule temp = NewModule();
			InsertModuleIntoDatabase(temp);

			//Make sure the insertion worked smoothly.
			Assert.IsTrue(temp.Equals(SelectModuleById(temp.TicketModuleId)), "The created module and selected module do not match.  Insertion or selection might have failed");

			//Change the values...
			temp.Name = "New Module name!";
			temp.Description = "New Module Description";
			temp.IsActive = false;
			temp.TicketQueueId = 3;

			//Peform the update.
			HelpdeskService.EditModule(temp);
			
			//Create a new instance of the module object and compare them...
			TicketModule temp2 = SelectModuleById(temp.TicketModuleId);
			//Make sure they match.
			Assert.IsTrue(temp.Equals(temp2), "The updated module did not match equality with the prepared module values in the method.");
		}

		[Test]
		[Category("Module Test")]
		public void GetAllModulesTest()
		{
			TicketModuleCollection tempModuleCol = new TicketModuleCollection();
			
			//Create a new module, insert it into the database, and then insert it into the Module Collection.
			for (int x = 0; x < 10; x++)
			{
				TicketModule tempMod = NewModule();
				InsertModuleIntoDatabase(tempMod);
				tempModuleCol.Add(tempMod);
			}

			//Get all Modules...
			TicketModuleCollection tempModuleCol2 = HelpdeskService.GetAllModules();
			foreach (TicketModule temp in tempModuleCol)
			{
				Assert.IsTrue(tempModuleCol2.Contains(temp));
			}

		}

		[Test]
		[Category("Module Test")]
		public void GetModulesByQueueIdTest()
		{
			TicketModuleCollection tempModuleCol = new TicketModuleCollection();

			//Create a new module, insert it into the database, and then insert it into the Module Collection.
			for (int x = 0; x < 10; x++)
			{
				TicketModule tempMod = NewModule();
				tempMod.TicketQueueId = 0;
				InsertModuleIntoDatabase(tempMod);
				tempModuleCol.Add(tempMod);
			}

			//Get all Modules...
			TicketModuleCollection tempModuleCol2 = HelpdeskService.GetModulesByQueueId(0);
			foreach (TicketModule temp in tempModuleCol)
			{
				Assert.IsTrue(tempModuleCol2.Contains(temp));
			}

			Assert.IsTrue(tempModuleCol2.Count == tempModuleCol.Count);

		}

	}
}

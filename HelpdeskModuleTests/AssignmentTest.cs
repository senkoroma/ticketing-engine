using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using HelpdeskModule;
using System.Diagnostics;

namespace HelpdeskModuleTests
{
	[TestFixture]
	public class AssignmentTest : AbstractDaoTest
	{

		#region --------------  Test Setup  --------------

		public static DateTime TestDate
		{
			get { return DateTime.Now; }
		}

		/// <summary>
		/// Helper method to Select Module from database by id, returns null if record is not found.
		/// </summary>
		private Assignment SelectAssignmentById(int AssignmentId)
		{
			return HelpdeskService.GetAssignmentById(AssignmentId);
		}

		/// <summary>
		/// Helper method to insert values into the database.  If successful, it will set teh appropriate row id.
		/// </summary>
		/// <param name="Module"></param>
		private void InsertAssignmentIntoDatabase(Assignment assignment)
		{
			HelpdeskService.CreateAssignment(assignment);
		}

		/// <summary>
		/// Creates shiny new assignment that are not referenced / altered by other methods.
		/// </summary>
		/// <returns></returns>
		private Assignment NewAssignment()
		{
			//Initialize a basic module object.
			Assignment temp = new Assignment();
			temp.AssignedTo = "nblevins(Assigned)";
			temp.AssignmentDate = TestDate;
			temp.Creator = "nblevins(Create)";
			temp.IsActive = true;
			temp.TicketId = 1;

			return temp;
		}

		/// <summary>
		/// This method sets up the test itself with the proper vars.  This is called by the abstract class's 
		/// [TestFixtureStartUp] method.
		/// </summary>
		protected override void TestInitialize()
		{
		}

		#endregion

		[Test]
		[Category("Assignment Tests")]
		public void CreateAssignmentTest()
		{
			Assignment temp = NewAssignment();

			InsertAssignmentIntoDatabase(temp);

			//Make sure it returns a row id and that that record exists.
			Assert.IsTrue(temp.AssignmentId > 0, "The TicketModule insertion did not return a row id");

			Assert.IsTrue(SelectAssignmentById(temp.AssignmentId) != null, "The internal selection query used to verify this test failed to return the a row.");
		}

		[Test]
		[Category("Assignment Tests")]
		public void UpdateAssignmentTest()
		{
			Assignment temp = NewAssignment();
			InsertAssignmentIntoDatabase(temp);

			//Make sure the insertion worked smoothly.
			Assert.IsTrue(temp.Equals(SelectAssignmentById(temp.AssignmentId)), "The created assignment and selected assignment do not match.  Insertion or selection might have failed");

			//Change the values...
			temp.AssignedTo = "nblevins(assigned";
			temp.AssignmentDate = TestDate;
			temp.IsActive = false;
			temp.TicketId = 1;
			temp.Creator = "blevinsn";

			//Peform the update.
			HelpdeskService.EditAssignment(temp);

			//Create a new instance of the module object and compare them...
			Assignment temp2 = SelectAssignmentById(temp.AssignmentId);
			//Make sure they match.
			Assert.IsTrue(temp.Equals(temp2), "The updated queue did not match equality with the prepared module values in the method.");
		}

		[Test]
		[Category("Assignment Tests")]
		public void GetAssignmentsByTicketIdTest()
		{
			AssignmentCollection tempAssignmentCol = new AssignmentCollection();

			//Create a new assignment, insert it into the database, and then insert it into the Assignment Collection.
			for (int x = 0; x < 10; x++)
			{
				Assignment temp = NewAssignment();
				temp.TicketId = 0;
				InsertAssignmentIntoDatabase(temp);
				tempAssignmentCol.Add(temp);
			}

			//Get all Assignments by that Ticket Id...
			AssignmentCollection tempAssignmentCol2 = HelpdeskService.GetAssignmentsByTicketId(0);
			foreach (Assignment temp in tempAssignmentCol)
			{
				Assert.IsTrue(tempAssignmentCol2.Contains(temp));
			}

			Assert.IsTrue(tempAssignmentCol2.Count == tempAssignmentCol.Count); 
		}

		[Test]
		[Category("Assignment Tests")]
		public void GetAssignmentsByUserTest()
		{
			AssignmentCollection tempAssignmentCol = new AssignmentCollection();

			//Create a new assignment, insert it into the database, and then insert it into the Assignment Collection.
			for (int x = 0; x < 10; x++)
			{
				Assignment temp = NewAssignment();
				temp.AssignedTo = "testing";
				InsertAssignmentIntoDatabase(temp);
				tempAssignmentCol.Add(temp);
			}

			//Get all Assignments by that Ticket Id...
			AssignmentCollection tempAssignmentCol2 = HelpdeskService.GetAssignmentsByUser("testing");

			foreach (Assignment temp in tempAssignmentCol)
			{
			   Assert.IsTrue(tempAssignmentCol2.Contains(temp), "The new assignment collection did not contain the same data as the original");
			}
			Assert.IsTrue(tempAssignmentCol2.Count >= tempAssignmentCol.Count); 
		}

		[Test]
		[Category("Assignment Tests")]
		public void GetAssignmentsByUserAndActiveTest()
		{
			AssignmentCollection tempAssignmentCol = new AssignmentCollection();

			//Create a new assignment, insert it into the database, and then insert it into the Assignment Collection.
			for (int x = 0; x < 10; x++)
			{
				Assignment temp = NewAssignment();
				temp.AssignedTo = "testing";
				temp.IsActive = true;
				InsertAssignmentIntoDatabase(temp);
				tempAssignmentCol.Add(temp);
			}

			//Get all Assignments by that Ticket Id...
			AssignmentCollection tempAssignmentCol2 = HelpdeskService.GetAssignmentByUserAndActive("testing", true);

			foreach (Assignment temp in tempAssignmentCol)
			{
				Assert.IsTrue(tempAssignmentCol2.Contains(temp), "The new assignment collection did not contain the same data as the original");
			}
			Assert.IsTrue(tempAssignmentCol2.Count >= tempAssignmentCol.Count);
		}

		protected override void Cleanup()
		{
		}

	}
}

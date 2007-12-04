using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using System.Diagnostics;
using HelpdeskModule;

namespace HelpdeskModuleTests
{
	[TestFixture]
	public class TicketCategoryTest : AbstractDaoTest
	{
		#region ----------------  Test Setup  ----------------

		/// <summary>
		/// Creates shiny new queues that are not referenced / altered by other methods.
		/// </summary>
		/// <returns></returns>
		private TicketCategory NewCategory()
		{
			TicketCategory expected = new TicketCategory();
			expected.Name = "Category Name";
			expected.Description = "This is my description of my category";
			expected.IsActive = true;
			expected.QueueId = 1;

			return expected;
		}

		/// <summary>
		/// Helper method to Select Queue from database by id, returns null if record is not found.
		/// </summary>
		private TicketCategory SelectCategoryById(int TicketCategoryId)
		{
			return HelpdeskService.GetCategoryById(TicketCategoryId);
		}

		/// <summary>
		/// Helper method to insert values into the database.  If successful, it will set the appropriate row id.
		/// </summary>
		/// <param name="Category"></param>
		private void InsertCategoryIntoDatabase(TicketCategory category)
		{
			HelpdeskService.CreateCategory(category);
		}

		protected override void TestInitialize() { }

		protected override void Cleanup() { }

		#endregion

		[Test]
		[Category("Category Test")]
		public void CreateCategoryTest()
		{
			TicketCategory temp = NewCategory();

			HelpdeskService.CreateCategory(temp);

			Trace.WriteLine(temp.TicketCategoryId);

			Assert.IsTrue(SelectCategoryById(temp.TicketCategoryId) != null, "The internal selection query used to verify this test failed to return the a row.");
			Assert.IsTrue(temp.TicketCategoryId > 0, "The returned Id from the CreateQueue test did not return a value greater than 0.");
		}

		[Test]
		[Category("Category Test")]
		public void EditCategoryTest()
		{
			TicketCategory temp = NewCategory();
			InsertCategoryIntoDatabase(temp);

			//Make sure the insertion worked smoothly.
			Assert.IsTrue(temp.Equals(SelectCategoryById(temp.TicketCategoryId)), "The created Category and selected Category do not match.  Insertion or selection might have failed");

			//Change the values...
			temp.Name = "New Category name!";
			temp.Description = "New Category Description";
			temp.IsActive = false;
			temp.QueueId = 3;

			//Peform the update.
			HelpdeskService.EditCategory(temp);

			//Create a new instance of the Category object and compare them...
			TicketCategory temp2 = SelectCategoryById(temp.TicketCategoryId);
			//Make sure they match.
			Assert.IsTrue(temp.Equals(temp2), "The updated Category did not match equality with the prepared Category values in the method.");
		}

		[Test]
		[Category("Category Test")]
		public void GetAllCategoryTest()
		{
			TicketCategoryCollection tempCategoryCol = new TicketCategoryCollection();

			//Create a new Category, insert it into the database, and then insert it into the Category Collection.
			for (int x = 0; x < 10; x++)
			{
				TicketCategory temp = NewCategory();
				InsertCategoryIntoDatabase(temp);
				tempCategoryCol.Add(temp);
			}

			//Get all Categorys...
			TicketCategoryCollection tempCategoryCol2 = HelpdeskService.GetAllCategory();
			foreach (TicketCategory temp in tempCategoryCol)
			{
				Assert.IsTrue(tempCategoryCol2.Contains(temp));
			}

		}
	}
}

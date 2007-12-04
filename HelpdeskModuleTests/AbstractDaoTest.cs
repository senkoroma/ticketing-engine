using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using System.Transactions;

namespace HelpdeskModuleTests
{

	[TestFixture]
	public abstract class AbstractDaoTest
	{
		public static DateTime TestDate
		{
			get { return DateTime.Now; }
		}

		private TransactionScope trans;

		protected TransactionOptions transactionOptions;

		protected abstract void TestInitialize();

		protected abstract void Cleanup();

		[TestFixtureSetUp]
		public void BaseSetup()
		{

			transactionOptions = new TransactionOptions();

			transactionOptions.IsolationLevel = IsolationLevel.Serializable;

			trans = new TransactionScope(TransactionScopeOption.Required, transactionOptions);

			TestInitialize();

		}

		[TestFixtureTearDown]
		public void BaseCleanup()
		{
			trans.Dispose();
			Cleanup();
		}

	}

}

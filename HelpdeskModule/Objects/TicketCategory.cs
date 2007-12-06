using System;
using System.Collections.Generic;
using System.Text;

namespace HelpdeskModule
{

    public class TicketCategory : IEquatable<TicketCategory>
	{
		#region ---------------  Fields and Properties  ---------------

		private string _Name;

		public string Name
		{
			get { return _Name; }
			set { _Name = value; }
		}
		private string _Description;

		public string Description
		{
			get { return _Description; }
			set { _Description = value; }
		}
		private int _TicketCategoryId;

		public int TicketCategoryId
		{
			get { return _TicketCategoryId; }
			set { _TicketCategoryId = value; }
		}

		private int _QueueId;

		public int QueueId
		{
			get { return _QueueId; }
			set { _QueueId = value; }
		}

		private bool _IsActive;

		public bool IsActive
		{
			get { return _IsActive; }
			set { _IsActive = value; }
		}

		#endregion

		#region IEquatable<TicketCategory> Members

		public bool Equals(TicketCategory other)
		{
			return other.Description == Description &&
				other.IsActive == IsActive &&
				other.Name == Name &&
				other.QueueId == QueueId;
		}

		#endregion

		#region  ---------------  Constructors  ---------------


		public TicketCategory() { }

		/// <summary>
		/// Loads a blank instance of the object, applying the supplied id.
		/// </summary>
		/// <param name="TicketCategoryId"></param>
		public TicketCategory(int uTicketCategoryId) { _TicketCategoryId = uTicketCategoryId; }

		public TicketCategory(int uTicketCategoryId, string uName, string uDescription,
			bool uIsActive, int uQueueId)
		{
			_TicketCategoryId = uTicketCategoryId;
			_Description = uDescription;
			_IsActive = uIsActive;
			_Name = uName;
			_QueueId = uQueueId;
		}

		#endregion

	}

	public class TicketCategoryCollection : List<TicketCategory> { };
}

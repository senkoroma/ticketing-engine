using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Security;

namespace HelpdeskModule
{
	/// <summary>
	/// Collection of Assignemnt objects, inherits from a basic generic list.
	/// </summary>
	public class AssignmentCollection : List<Assignment> { }

	/// <summary>
	/// Encapsulates the assignemnt data of a particular ticket to an individaul user.
	/// </summary>
    public class Assignment : IEquatable<Assignment>
	{

		#region ---------------  Fields and Properties  ---------------

		private int _TicketId;

		public int TicketId
		{
			get { return _TicketId; }
			set { _TicketId = value; }
		}
		private int _AssignmentId;

		public int AssignmentId
		{
			get { return _AssignmentId; }
			set { _AssignmentId = value; }
		}
		private string _Creator;

		public string Creator
		{
			get { return _Creator; }
			set { _Creator = value; }
		}
		private string _AssignedTo;

		public string AssignedTo
		{
			get { return _AssignedTo; }
			set { _AssignedTo = value; }
		}
		private DateTime _AssignmentDate;

		public DateTime AssignmentDate
		{
			get { return _AssignmentDate; }
			set { _AssignmentDate = value; }
		}

		private bool _IsActive;

		public bool IsActive
		{
			get { return _IsActive; }
			set { _IsActive = value; }
		}

		#endregion

		/// <summary>
		/// Basic constructor.
		/// </summary>
		public Assignment()
		{
		}

		#region IEquatable<Assignment> Members

		public bool Equals(Assignment other)
		{
			return other.AssignedTo == AssignedTo &&
				other.AssignmentDate.ToShortDateString() == AssignmentDate.ToShortDateString() &&
				other.Creator == Creator &&
				other.IsActive == IsActive &&
				other.TicketId == TicketId;
		}

		#endregion
	}
}

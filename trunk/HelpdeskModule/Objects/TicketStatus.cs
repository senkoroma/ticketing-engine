using System;
using System.Collections.Generic;
using System.Text;

namespace HelpdeskModule
{
	/// <summary>
	/// Indicates the response status of the ticket.
	/// </summary>
    public class TicketStatus : IEquatable<TicketStatus>
	{
		#region ---------------  Fields and Properties  ---------------

		private int _TicketStatusId;

		public int TicketStatusId
		{
			get { return _TicketStatusId; }
			set { _TicketStatusId = value; }
		}
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
		private int _StatusOrder;

		public int StatusOrder
		{
			get { return _StatusOrder; }
			set { _StatusOrder = value; }
		}

		private bool _IsActive;

		public bool IsActive
		{
			get { return _IsActive; }
			set { _IsActive = value; }
		}

		#endregion

		/// <summary>
		/// Basic constuctor.
		/// </summary>
		public TicketStatus()
		{
		}

		public TicketStatus(int uTicketStatusId)
		{
			_TicketStatusId = uTicketStatusId;
		}

		#region IEquatable<TicketStatus> Members

		public bool Equals(TicketStatus other)
		{
			return Name == other.Name && 
				Description == other.Description &&
				StatusOrder == other.StatusOrder &&
				IsActive == other.IsActive;
		}

		#endregion
	}
	
	/// <summary>
	/// A collection of TicketStatus objects, inherits from the generic List.
	/// </summary>
	public class TicketStatusCollection : List<TicketStatus> { };

}

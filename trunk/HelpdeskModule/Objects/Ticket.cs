using System;
using System.Collections.Generic;
using System.Text;

namespace HelpdeskModule
{
	/// <summary>
	/// A collection of Ticket objects.  This inherits from a basic List<> of Tickets.
	/// </summary>
	public class TicketCollection : List<Ticket> { }

	/// <summary>
	/// Class the represetns a Ticket within the Ticketing system.  
	/// </summary>
	public class Ticket
	{
		#region ---------------  Fields and Properties  ---------------

		private int _TicketId;

		public int TicketId
		{
			get { return _TicketId; }
			set { _TicketId = value; }
		}
		private TicketModule _Module;

		public TicketModule Module
		{
			get { return _Module; }
			set { _Module = value; }
		}
		private DateTime _CreationDate;

		public DateTime CreationDate
		{
			get { return _CreationDate; }
			set { _CreationDate = value; }
		}
		private TicketStatus _Status;

		public TicketStatus Status
		{
			get { return _Status; }
			set { _Status = value; }
		}
		private string _Description;

		public string Description
		{
			get { return _Description; }
			set { _Description = value; }
		}
		private string _Creator;

		public string Creator
		{
			get { return _Creator; }
			set { _Creator = value; }
		}

		private Requestor _Requestor;

		public Requestor Requestor
		{
			get { return _Requestor; }
			set { _Requestor = value; }
		}

		private TicketResponseCollection _Responses;

		public TicketResponseCollection Responses
		{
			get { return _Responses; }
			set { _Responses = value; }
		}
		private AssignmentCollection _Assignment;

		public AssignmentCollection Assignment
		{
			get { return _Assignment; }
			set { _Assignment = value; }
		}
		private TicketCategory _Category;

		public TicketCategory Category
		{
			get { return _Category; }
			set { _Category = value; }
		}

		private DateTime _DueDate;

		public DateTime DueDate
		{
			get { return _DueDate; }
			set { _DueDate = value; }
		}

		private TicketPriority _Priority;

		public TicketPriority Priority
		{
			get { return _Priority; }
			set { _Priority = value; }
		}



		#endregion

		/// <summary>
		/// Basic Constructor.
		/// </summary>
		public Ticket()
		{

		}
	}
}

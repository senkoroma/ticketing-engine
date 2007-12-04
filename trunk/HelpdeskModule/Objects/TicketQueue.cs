using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Security;

namespace HelpdeskModule
{
    public class TicketQueue : IEquatable<TicketQueue>
	{

		#region ---------------  Fields and Properties  ---------------

		private string _Name;

		public string Name
		{
			get { return _Name; }
			set { _Name = value; }
		}
		private int _QueueId;

		public int QueueId
		{
			get { return _QueueId; }
			set { _QueueId = value; }
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
		private DateTime _CreationDate;

		public DateTime CreationDate
		{
			get { return _CreationDate; }
			set { _CreationDate = value; }
		}

		private TicketCollection _Tickets;

		public TicketCollection Tickets
		{
			get { return _Tickets; }
			set { _Tickets = value; }
		}

		private bool _IsActive;

		public bool IsActive
		{
			get { return _IsActive; }
			set { _IsActive = value; }
		}

		#endregion

		/// <summary>
		/// Empty constructor for basic work.
		/// </summary>
		public TicketQueue()
		{
		}

		#region IEquatable<TicketQueue> Members

		public bool Equals(TicketQueue other)
		{
			return
				other.CreationDate.ToShortDateString() == CreationDate.ToShortDateString() &&
				other.Creator == Creator &&
				other.Description == Description &&
				other.IsActive == IsActive &&
				other.Name == Name &&
				other.QueueId == QueueId;

				//TODO:  May need to consider comparing tickets at a later date.
		}

		#endregion
	}

	public class TicketQueueCollection : List<TicketQueue> { }
}

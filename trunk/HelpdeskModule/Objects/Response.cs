using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Security;

namespace HelpdeskModule
{
	public class TicketResponseCollection : List<TicketResponse> { }

    public class TicketResponse
	{
		#region ---------------  Fields and Properties  ---------------

		private int _TicketId;

		public int TicketId
		{
			get { return _TicketId; }
			set { _TicketId = value; }
		}
		private string _Creator;

		public string Creator
		{
			get { return _Creator; }
			set { _Creator = value; }
		}
		private string _Response;

		public string Response
		{
			get { return _Response; }
			set { _Response = value; }
		}
		private DateTime _CreationDate;

		public DateTime CreationDate
		{
			get { return _CreationDate; }
			set { _CreationDate = value; }
		}
		private int _TicketResponseId;

		public int TicketResponseId
		{
			get { return _TicketResponseId; }
			set { _TicketResponseId = value; }
		}
		private int _MinsSpent;

		public int MinsSpent
		{
			get { return _MinsSpent; }
			set { _MinsSpent = value; }
		}

		#endregion
	}
}

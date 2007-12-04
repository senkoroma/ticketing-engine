using System;
using System.Collections.Generic;
using System.Text;

namespace HelpdeskModule
{
	/// <summary>
	/// A collection of TicketChargeCode 's.  Inherits from a basic list of TicketChargeCodes.
	/// </summary>
	public class TicketChargeCodeCollection : List<TicketChargeCode> { }

	/// <summary>
	/// Class that represents a charge code item that belongs to a ticket.  
	/// </summary>
	public class TicketChargeCode : IEquatable<TicketChargeCode>
	{

		#region ---------------  Fields and Properties  ---------------

		private int _ChargeCodeId;

		public int ChargeCodeId
		{
			get { return _ChargeCodeId; }
			set { _ChargeCodeId = value; }
		}

		private string _ChargeCode;

		public string ChargeCode
		{
			get { return _ChargeCode; }
			set { _ChargeCode = value; }
		}

		private string _Description;

		public string Description
		{
			get { return _Description; }
			set { _Description = value; }
		}

		private DateTime _ExpirationDate;

		public DateTime ExpirationDate
		{
			get { return _ExpirationDate; }
			set { _ExpirationDate = value; }
		}

		private bool _IsActive;

		public bool IsActive
		{
			get { return _IsActive; }
			set { _IsActive = value; }
		}

		#endregion

		#region IEquatable<TicketChargeCode> Members

		public bool Equals(TicketChargeCode other)
		{
			return other.ChargeCode == ChargeCode &&
				other.ChargeCodeId == ChargeCodeId &&
				other.Description == Description &&
				other.ExpirationDate.ToShortDateString() == ExpirationDate.ToShortDateString() &&
				other.IsActive == IsActive;
		}

		#endregion


		/// <summary>
		/// Basic construtor.
		/// </summary>
		public TicketChargeCode()
		{

		}

	}
}

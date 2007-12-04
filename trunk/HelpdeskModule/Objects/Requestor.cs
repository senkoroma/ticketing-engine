using System;
using System.Collections.Generic;
using System.Text;

namespace HelpdeskModule
{
	/// <summary>
	/// Collection of requesting users.  Inherits from a basic generic list.
	/// </summary>
	public class RequestorCollection : List<Requestor> { }

	/// <summary>
	/// Represents a requesting user in this system.  Requestors are not considered to be users
	/// of the system itself, but this class simply represents data about the requesting person.
	/// </summary>
    public class Requestor :IEquatable<Requestor>
	{
		#region ---------------  Fields and Properties  ---------------

		private string _FirstName;

		public string FirstName
		{
			get { return _FirstName; }
			set { _FirstName = value; }
		}
		private string _LastName;

		public string LastName
		{
			get { return _LastName; }
			set { _LastName = value; }
		}
		private string _Email;

		public string Email
		{
			get { return _Email; }
			set { _Email = value; }
		}
		private string _ContactNumber;

		public string ContactNumber
		{
			get { return _ContactNumber; }
			set { _ContactNumber = value; }
		}
		private int _RequestorId;

		public int RequestorId
		{
			get { return _RequestorId; }
			set { _RequestorId = value; }
		}

		#endregion

		/// <summary>
		/// Basic constructor.
		/// </summary>
		public Requestor()
		{
		}

		/// <summary>
		/// Loads a blank instance of the object, applying the supplied id.
		/// </summary>
		/// <param name="uRequestorId"></param>
		public Requestor(int uRequestorId)
		{
			_RequestorId = uRequestorId;
		}

		#region IEquatable<Requestor> Members

		public bool Equals(Requestor other)
		{
			return other.FirstName == FirstName &&
				other.LastName == LastName &&
				other.RequestorId == RequestorId &&
				other.ContactNumber == ContactNumber &&
				other.Email == Email;
		}

		#endregion
	}
}

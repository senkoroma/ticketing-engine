using System;
using System.Collections.Generic;
using System.Text;

namespace HelpdeskModule
{
	/// <summary>
	/// Class the represents a Company within the Ticketing system.  
	/// </summary>
	public class Company : IEquatable<Company>
	{
		#region ---------------  Fields and Properties  ---------------

		private int _CompanyId;

		public int CompanyId
		{
			get { return _CompanyId; }
			set { _CompanyId = value; }
		}

		private string _Name;

		public string Name
		{
			get { return _Name; }
			set { _Name = value; }
		}
		private string _Address1;

		public string Address1
		{
			get { return _Address1; }
			set { _Address1 = value; }
		}
		private string _Address2;

		public string Address2
		{
			get { return _Address2; }
			set { _Address2 = value; }
		}

		private string _City;

		public string City
		{
			get { return _City; }
			set { _City = value; }
		}

		private string _State;

		public string State
		{
			get { return _State; }
			set { _State = value; }
		}

		private string _Zip_Code;

		public string Zip_Code
		{
			get { return _Zip_Code; }
			set { _Zip_Code = value; }
		}

		private string _Website;

		public string Website
		{
			get { return _Website; }
			set { _Website = value; }
		}
		private Requestor _MainContact;

		public Requestor MainContact
		{
			get { return _MainContact; }
			set { _MainContact = value; }
		}
		private Requestor _SecondaryContact;

		public Requestor SecondaryContact
		{
			get { return _SecondaryContact; }
			set { _SecondaryContact = value; }
		}
		private int _ParentId;

		public int ParentId
		{
			get { return _ParentId; }
			set { _ParentId = value; }
		}
		private string _ContactNumber1;

		public string ContactNumber1
		{
			get { return _ContactNumber1; }
			set { _ContactNumber1 = value; }
		}
		private string _ContactNumber2;

		public string ContactNumber2
		{
			get { return _ContactNumber2; }
			set { _ContactNumber2 = value; }
		}

		#endregion

		#region IEquatable<Company> Members

		public bool Equals(Company other)
		{
			return other.Address1 == Address1 &&
			other.Address2 == Address2 &&
			other.City == City &&
			other.ContactNumber1 == ContactNumber1 &&
			other.ContactNumber2 == ContactNumber2 &&
			other.MainContact.Equals(MainContact) &&
			other.Name == Name &&
			other.ParentId == ParentId &&
			other.SecondaryContact.Equals(SecondaryContact) &&
			other.State == State &&
			other.Website == Website &&
			other.Zip_Code == Zip_Code;

		}

		#endregion
	}

	/// <summary>
	/// A collection of Company objects.  This inherits from a basic List<> of Company.
	/// </summary>
	public class CompanyCollection : List<Company>
	{ }
}

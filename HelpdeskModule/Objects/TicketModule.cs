using System;
using System.Collections.Generic;
using System.Text;

namespace HelpdeskModule
{
	public class TicketModuleCollection : List<TicketModule> { };

	public class TicketModule : IEquatable<TicketModule>
	{

		#region ---------------  Fields and Properties  ---------------

		private int _TicketQueueId;

		public int TicketQueueId
		{
			get { return _TicketQueueId; }
			set { _TicketQueueId = value; }
		}

		private int _TicketModuleId;

		public int TicketModuleId
		{
			get { return _TicketModuleId; }
			set { _TicketModuleId = value; }
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

		private bool _IsActive;

		public bool IsActive
		{
			get { return _IsActive; }
			set { _IsActive = value; }
		}

		#endregion

		#region  ---------------  Constructors  ---------------

		/// <summary>
		/// Basic Constructor
		/// </summary>
		public TicketModule()
		{

		}

		/// <summary>
		/// Loads a blank instance of the object, applying the supplied id.
		/// </summary>
		/// <param name="uTicketModuleId"></param>
		public TicketModule(int uTicketModuleId)
		{
			_TicketModuleId = uTicketModuleId;
		}

		public TicketModule(int uTicketModuleId, int uQueueId, 
			string uName, string uDescription, bool uIsActive)
		{
			_TicketModuleId = uTicketModuleId;
			_Description = uDescription;
			_Name = uName;
			_TicketQueueId = uQueueId;
			_IsActive = uIsActive;
		}

		#endregion

		#region IEquatable<TicketModule> Members

		public bool Equals(TicketModule other)
		{
			return other.Description == this.Description &&
				other.IsActive == this.IsActive &&
				other.Name == this.Name &&
				other.TicketModuleId == this.TicketModuleId &&
				other.TicketQueueId == this.TicketQueueId;
		}

		#endregion
	}
}

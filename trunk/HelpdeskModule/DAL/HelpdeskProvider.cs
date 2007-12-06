using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Security;

namespace HelpdeskModule
{
	/// <summary>
	/// Abstract class used to provide a pattern for any additional Providers that might be 
	/// attached to this system.  All new providers must inherit from this class.
	/// </summary>
	public abstract class HelpdeskProvider
	{
		#region -------------  Ticket Queues  -------------

		public abstract void CreateQueue(TicketQueue queue);

		public abstract void UpdateQueue(TicketQueue queue);

		public abstract TicketQueue GetQueue(int QueueId);

		public abstract void DeleteQueue(int QueueId);

		public abstract TicketQueueCollection GetAllQueues();

		public abstract TicketQueueCollection GetAllEnabledQueues();

		#endregion

		#region -------------  Tickets  -------------

		public abstract void CreateTicket(Ticket ticket);

		public abstract void UpdateTicket(Ticket ticket);

		public abstract Ticket GetTicket(int TicketId, bool IsBasicDataOnly);

		public abstract void DeleteTicket(int TicketId);

		public abstract TicketCollection GetTicketsByQueueId(int QueueId, bool IsBasicOnly);

		public abstract TicketCollection GetTicketsByAssignment(string user);

		public abstract Ticket MergeTickets(int MasterTicketId, int SubordinateTicketId);

		#endregion

		#region -------------  Responses  -------------

		public abstract void CreateResponse(TicketResponse response);

		public abstract void UpdateResponse(TicketResponse response);

		public abstract TicketResponse GetResponse(int ResponseId);

		public abstract void DeleteResponse(int ResponseId);

		public abstract TicketResponseCollection GetResponsesByTicketId(int TicketId);

		#endregion

		#region -------------  Assignment  -------------

		public abstract void CreateAssignment(Assignment assignment);

		public abstract void EditAssignment(Assignment assignment);

		public abstract AssignmentCollection GetAssignmentsByTicketId(int TicketId);

		public abstract AssignmentCollection GetAssignmentsByUser(string user);

		public abstract AssignmentCollection GetAssignmentByUserAndActive(string user, bool IsActive);

		public abstract Assignment GetAssignmentById(int AssignmentId);

		#endregion

		#region -------------  Ticket Module  -------------

		public abstract void CreateModule(TicketModule module);

		public abstract void EditModule(TicketModule module);

		public abstract TicketModuleCollection GetModulesByQueueId(int QueueId);

		public abstract TicketModule GetModule(int ModuleId);

		public abstract TicketModuleCollection GetAllModules();


		#endregion

		#region -------------  Ticket Status  -------------

		public abstract void CreateStatus(TicketStatus status);

		public abstract void EditStatus(TicketStatus status);

		public abstract TicketStatus GetStatusById(int TicketStatusId);

		public abstract TicketStatusCollection GetAllStatus();

		#endregion

		#region -------------  Ticket Category  -------------

		public abstract void CreateCategory(TicketCategory category);

		public abstract void EditCategory(TicketCategory category);

		public abstract TicketCategory GetCategoryById(int TicketCategoryid);

		public abstract TicketCategoryCollection GetAllCategory();

		#endregion

		#region -------------  Requestor  -------------

		public abstract void CreateRequestor(Requestor requestor);

		public abstract void EditRequestor(Requestor requestor);

		public abstract Requestor GetRequestorById(int RequestorId);

		public abstract RequestorCollection GetAllRequestor();

		#endregion

		#region -------------  Charge Code  -------------

		public abstract void CreateChargeCode(TicketChargeCode ChargeCode);

		public abstract void EditChargeCode(TicketChargeCode ChargeCode);

		public abstract TicketChargeCode GetChargeCodeById(int ChargeCodeId);

		public abstract TicketChargeCodeCollection GetAllChargeCode();

		public abstract TicketChargeCodeCollection GetAllChargeCodeByActive(bool IsActive);

		#endregion

		#region ------------- Company  -------------

		public abstract void CreateCompany(Company company);

		public abstract void UpdateCompany(Company company);

		public abstract Company GetCompanyById(int CompanyId);

		#endregion
	}
}

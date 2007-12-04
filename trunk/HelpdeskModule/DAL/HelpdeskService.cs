using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Security;

namespace HelpdeskModule
{
	/// <summary>
	/// Singleton method used to instantiate the providers for this system.  This work horse class 
	/// should be called from the client code, rather than any of the providers individually.
	/// </summary>
	public class HelpdeskService
	{
		#region ----------------  Singleton Methods / Properties  --------------

		private static HelpdeskProvider _provider = null;
		private static object _lock = new object();

		private static void LoadProviders()
		{
			// Avoid claiming lock if providers are already loaded
			if (_provider == null)
			{
				lock (_lock)
				{
					// Do this again to make sure _provider is still null
					if (_provider == null)
					{

						//Create an instance of the provider and set it to the _provider object...
						//Currently, this is hard-coded to load the SQLHelpdesk Provider.  In order to add
						//additional features / providers, look up "Creating Custom Providers" online and
						//modifiy this section to match (as well as the web.config).
						_provider = new SQLHelpdeskProvider();

						//If the provider is null, it managed to slip past our lock or did not instantiate
						//properly... Throw an new exeception.
						if (_provider == null)
							throw new Exception
								("Loaded Provider returned null during the instantiation process.");
					}
				}
			}
		}

		#endregion

		#region -------------  Static Methods and Properties (Public)  --------------------

		#region -------------  Ticket Queues  -------------

		public static void CreateQueue(TicketQueue queue) { LoadProviders(); _provider.CreateQueue(queue); }

		public static void UpdateQueue(TicketQueue queue) { LoadProviders(); _provider.UpdateQueue(queue); }

		public static TicketQueue GetQueue(int QueueId) { LoadProviders(); return _provider.GetQueue(QueueId); }

		public static void DeleteQueue(int QueueId) { LoadProviders(); _provider.DeleteQueue(QueueId); }

		public static TicketQueueCollection GetAllQueues() { LoadProviders(); return _provider.GetAllQueues(); }

		public static TicketQueueCollection GetAllEnabledQueues() { LoadProviders(); return _provider.GetAllEnabledQueues(); }

		#endregion

		#region -------------  Tickets  -------------

		public static void CreateTicket(Ticket ticket) { LoadProviders(); _provider.CreateTicket(ticket); }

		public static void UpdateTicket(Ticket ticket) { LoadProviders(); _provider.UpdateTicket(ticket); }

		public static Ticket GetTicket(int TicketId, bool IsBasicDataOnly) { LoadProviders(); return _provider.GetTicket(TicketId, IsBasicDataOnly); }

		public static void DeleteTicket(int TicketId) { LoadProviders(); _provider.DeleteTicket(TicketId); }

		public static TicketCollection GetTicketsByQueueId(int QueueId) { LoadProviders(); return _provider.GetTicketsByQueueId(QueueId); }

		public static TicketCollection GetTicketsByAssignment(string user) { LoadProviders(); return _provider.GetTicketsByAssignment(user); }

		#endregion

		#region -------------  Responses  -------------

		public static void CreateResponse(TicketResponse response) { LoadProviders(); _provider.CreateResponse(response); }

		public static void UpdateResponse(TicketResponse response) { LoadProviders(); _provider.UpdateResponse(response); }

		public static TicketResponse GetResponse(int ResponseId) { LoadProviders(); return _provider.GetResponse(ResponseId); }

		public static void DeleteResponse(int ResponseId) { LoadProviders(); _provider.DeleteResponse(ResponseId); }

		public static TicketResponseCollection GetResponsesByTicketId(int TicketId) { LoadProviders(); return _provider.GetResponsesByTicketId(TicketId); }

		#endregion

		#region -------------  Assignment  -------------

		public static void CreateAssignment(Assignment assignment) { LoadProviders(); _provider.CreateAssignment(assignment); }

		public static void EditAssignment(Assignment assignment) { LoadProviders(); _provider.EditAssignment(assignment); }

		public static AssignmentCollection GetAssignmentsByTicketId(int TicketId) { LoadProviders(); return _provider.GetAssignmentsByTicketId(TicketId); }

		public static AssignmentCollection GetAssignmentsByUser(string user) { LoadProviders(); return _provider.GetAssignmentsByUser(user); }

		public static AssignmentCollection GetAssignmentByUserAndActive(string user, bool IsActive) { LoadProviders(); return _provider.GetAssignmentByUserAndActive(user, IsActive); }

		public static Assignment GetAssignmentById(int AssignmentId) { LoadProviders(); return _provider.GetAssignmentById(AssignmentId); }

		#endregion

		#region -------------  Ticket Module  -------------

		public static void CreateModule(TicketModule module) { LoadProviders(); _provider.CreateModule(module); }

		public static void EditModule(TicketModule module) { LoadProviders(); _provider.EditModule(module); }

		public static TicketModuleCollection GetModulesByQueueId(int QueueId) { LoadProviders(); return _provider.GetModulesByQueueId(QueueId); }

		public static TicketModule GetModule(int ModuleId) { LoadProviders(); return _provider.GetModule(ModuleId); }

		public static TicketModuleCollection GetAllModules() { LoadProviders(); return _provider.GetAllModules(); }

		#endregion

		#region -------------  Ticket Status  -------------

		public static void CreateStatus(TicketStatus status) { LoadProviders(); _provider.CreateStatus(status); }

		public static void EditStatus(TicketStatus status) { LoadProviders(); _provider.EditStatus(status); }

		public static TicketStatus GetStatusById(int TicketStatusId) { LoadProviders(); return _provider.GetStatusById(TicketStatusId); }
		
		public static TicketStatusCollection GetAllStatus() { LoadProviders(); return _provider.GetAllStatus(); }

		#endregion

		#region -------------  Ticket Category  -------------

		public static void CreateCategory(TicketCategory category) { LoadProviders(); _provider.CreateCategory(category); }

		public static void EditCategory(TicketCategory category) { LoadProviders(); _provider.EditCategory(category); }

		public static TicketCategory GetCategoryById(int TicketCategoryid) { LoadProviders(); return _provider.GetCategoryById(TicketCategoryid);}
		
		public static TicketCategoryCollection GetAllCategory() { LoadProviders(); return _provider.GetAllCategory(); }

		#endregion

		#region -------------  Requestor  -------------

		public static void CreateRequestor(Requestor requestor) { LoadProviders(); _provider.CreateRequestor(requestor); }

		public static void EditRequestor(Requestor requestor) { LoadProviders(); _provider.EditRequestor(requestor); }

		public static Requestor GetRequestorById(int RequestorId) { LoadProviders(); return _provider.GetRequestorById(RequestorId); }

		public static RequestorCollection GetAllRequestor() { LoadProviders(); return _provider.GetAllRequestor(); }

		#endregion

		#region -------------  ChargeCode  -------------

		public static void CreateChargeCode(TicketChargeCode ChargeCode) { LoadProviders(); _provider.CreateChargeCode(ChargeCode); }

		public static void EditChargeCode(TicketChargeCode ChargeCode) { LoadProviders(); _provider.EditChargeCode(ChargeCode); }

		public static TicketChargeCode GetChargeCodeById(int ChargeCodeId) { LoadProviders(); return _provider.GetChargeCodeById(ChargeCodeId); }

		public static TicketChargeCodeCollection GetAllChargeCode() { LoadProviders(); return _provider.GetAllChargeCode(); }

		public static TicketChargeCodeCollection GetAllChargeCodeByActive(bool IsActive) { LoadProviders(); return _provider.GetAllChargeCodeByActive(IsActive); }

		#endregion


		#endregion

	}
}

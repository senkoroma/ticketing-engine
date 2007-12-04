using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Security;
using System.Data.SqlClient;
using System.Data;
using System.Diagnostics;

namespace HelpdeskModule
{
	/// <summary>
	/// SQL based provider for the Helpdesk system.  This class defines the actions to interact w/ a 
	/// SQL database.  All providers must inherit from the Helpdeskprovider.
	/// </summary>
	public class SQLHelpdeskProvider : HelpdeskProvider
	{

		#region -------------  Ticket Queues  -------------

		public override void CreateQueue(TicketQueue queue)
		{
			SqlParameter[] Params = new SqlParameter[] { 
				new SqlParameter("@Name", queue.Name), 
				new SqlParameter("@Description", queue.Description), 
				new SqlParameter("@Creator", queue.Creator), 
				new SqlParameter("@CreationDate", queue.CreationDate), 
				new SqlParameter("@IsActive", queue.IsActive)
			};

			queue.QueueId = DBHelper.ExecScalarSQL(
				@"INSERT INTO Queue ([name], description, creation_date, creator, is_active)
					VALUES (@Name, @Description, @CreationDate, @Creator, @IsActive); SELECT SCOPE_IDENTITY();",
				Params, CommandType.Text);
		}

		public override TicketQueue GetQueue(int QueueId)
		{
			SqlParameter[] Params = new SqlParameter[] { new SqlParameter("@QueueId", QueueId) };

			DataSet tempDS = DBHelper.FillDataset(
				@"SELECT [name], description, creation_date, creator, is_active
					FROM Queue
					WHERE Queue.queue_id = @QueueId",
					Params, CommandType.Text);

			TicketQueue temp;

			if (DBHelper.DataSetHasRows(tempDS))
			{
				DataRow dr = tempDS.Tables[0].Rows[0];
				temp = new TicketQueue();

				temp.CreationDate = System.Convert.ToDateTime(dr["creation_date"]);
				temp.Creator = dr["creator"].ToString();
				temp.Description = dr["description"].ToString();
				temp.IsActive = System.Convert.ToBoolean(dr["is_active"]);
				temp.Name = dr["name"].ToString();
				temp.QueueId = QueueId;

				//Do not load the tickets in this method...  a full load is reserved for another method.
				temp.Tickets = null;
			}
			else { temp = null; }

			return temp;
		}

		public override void UpdateQueue(TicketQueue queue)
		{
			SqlParameter[] Params = new SqlParameter[] {
				new SqlParameter("@Name", queue.Name), 
				new SqlParameter("@Description", queue.Description), 
				new SqlParameter("@Creator", queue.Creator), 
				new SqlParameter("@CreationDate", queue.CreationDate), 
				new SqlParameter("@QueueId", queue.QueueId), 
				new SqlParameter("@IsActive", queue.IsActive)
			};

			DBHelper.ExecSQL(
				@"UPDATE [Queue] SET [name] = @Name, description = @Description, creator = @creator, 
					creation_date = @CreationDate, is_active = @IsActive
					WHERE [Queue].queue_id = @QueueId",
					Params, CommandType.Text);
		}

		public override void DeleteQueue(int QueueId)
		{
			SqlParameter[] Params = new SqlParameter[] {
				new SqlParameter("@QueueId", QueueId)
			};

			DBHelper.ExecSQL(
				@"DELETE FROM [Queue] WHERE [Queue].queue_id = @QueueId",
					Params, CommandType.Text);
		}

		public override TicketQueueCollection GetAllQueues()
		{
			//Initialize an empty array.
			SqlParameter[] tempParams = new SqlParameter[0];
			DataSet tempDS = DBHelper.FillDataset(
				@"SELECT queue_id, [name], description, creation_date, creator, is_active
					FROM [Queue]",
				tempParams, CommandType.Text);

			TicketQueueCollection resultSet = new TicketQueueCollection();


			//Make sure the dataset has rows, otherwise return an empty collection.
			if (DBHelper.DataSetHasRows(tempDS))
			{
				foreach (DataRow dr in tempDS.Tables[0].Rows)
				{
					TicketQueue temp = new TicketQueue();

					temp.CreationDate = System.Convert.ToDateTime(dr["creation_date"]);
					temp.Creator = dr["creator"].ToString();
					temp.Description = dr["description"].ToString();
					temp.IsActive = System.Convert.ToBoolean(dr["is_active"]);
					temp.Name = dr["name"].ToString();
					temp.QueueId = Convert.ToInt32(dr["queue_id"]);

					resultSet.Add(temp);
				}
			}
			else { return null; }



			return resultSet;
		}

		public override TicketQueueCollection GetAllEnabledQueues()
		{
			//Initialize an empty array.
			SqlParameter[] tempParams = new SqlParameter[0];
			DataSet tempDS = DBHelper.FillDataset(
				@"SELECT queue_id, [name], description, creation_date, creator, is_active
					FROM [Queue]
					WHERE is_active = 1",
				tempParams, CommandType.Text);
			TicketQueueCollection resultSet = new TicketQueueCollection();

			//Make sure the dataset has rows, otherwise return an empty collection.
			if (tempDS.Tables.Count > 0 && tempDS.Tables[0].Rows.Count > 0)
			{
				foreach (DataRow dr in tempDS.Tables[0].Rows)
				{
					TicketQueue temp = new TicketQueue();

					temp.CreationDate = System.Convert.ToDateTime(dr["creation_date"]);
					temp.Creator = dr["creator"].ToString();
					temp.Description = dr["description"].ToString();
					temp.IsActive = System.Convert.ToBoolean(dr["is_active"]);
					temp.Name = dr["name"].ToString();
					temp.QueueId = Convert.ToInt32(dr["queue_id"]);

					resultSet.Add(temp);
				}
			}

			return resultSet;
		}

		#endregion

		#region -------------  Tickets  -------------

		public override void CreateTicket(Ticket ticket)
		{
			SqlParameter[] Params = new SqlParameter[] {
					new SqlParameter("@TicketModuleId", ticket.Module.TicketModuleId),
					new SqlParameter("@CreationDate", ticket.CreationDate),
					new SqlParameter("@Creator", ticket.Creator),
					new SqlParameter("@Description", ticket.Description),
					new SqlParameter("@DueDate", ticket.DueDate),
					new SqlParameter("@Priority", ticket.Priority),
					new SqlParameter("@TicketStatusId", ticket.Status.TicketStatusId),
					new SqlParameter("@RequestorId", ticket.Requestor.RequestorId),
					new SqlParameter("@CategoryId", ticket.Category.TicketCategoryId)
					};

			//Insert the records and set the status id.
			ticket.TicketId =

			Convert.ToInt32(
				DBHelper.ExecScalarSQL(
					@"INSERT INTO Ticket (module_id, category_id, creation_date, creator, description,
						due_date, priority, status_id, requestor_id)
						VALUES (@TicketModuleId, @CategoryId, @CreationDate, @Creator, @Description, @DueDate, @Priority,
							@TicketStatusId, @RequestorId); SELECT SCOPE_IDENTITY();",
					Params, CommandType.Text));
		}

		public override void UpdateTicket(Ticket ticket)
		{
			throw new Exception("The method or operation is not implemented.");
		}

		/// <summary>
		/// Load Ticket Dat by the supplied ID.
		/// </summary>
		/// <param name="TicketId">Id of the requested ticket</param>
		/// <param name="IsBasicDataOnly">If true, this will only load the id's of the associated classes.  If false, every object of the class will be loaded from SQLSERVER.</param>
		/// <returns></returns>
		public override Ticket GetTicket(int TicketId, bool IsBasicDataOnly)
		{
			//Based on whether or not the request is for all data, return the loaded ticket object.
			return IsBasicDataOnly ? LoadBasicDataById(TicketId) : LoadAllDataById(TicketId);
		}

		public override void DeleteTicket(int TicketId)
		{
			throw new Exception("The method or operation is not implemented.");
		}

		public override TicketCollection GetTicketsByQueueId(int QueueId)
		{
			throw new Exception("The method or operation is not implemented.");
		}

		public override TicketCollection GetTicketsByAssignment(string user)
		{
			throw new Exception("The method or operation is not implemented.");
		}

		#region -------------  Private Ticket Helper Methods  -------------

		private Ticket LoadAllDataById(int TicketId) { return new Ticket(); }

		private Ticket LoadBasicDataById(int TicketId) 
		{
			SqlParameter[] Params = new SqlParameter[] { new SqlParameter("@TicketId", TicketId) };
			DataSet ds = DBHelper.FillDataset(
				@"	SELECT status_id, category_id, creation_date, creator, description, module_id,
					requestor_id, priority, due_date
					FROM Ticket
					WHERE Ticket.ticket_id = @TicketId;",
				Params, CommandType.Text);
			Ticket temp;

			if (DBHelper.DataSetHasRows(ds))
			{
				DataRow dr = ds.Tables[0].Rows[0];

				temp = new Ticket();
				temp.Assignment = new AssignmentCollection();

				TicketCategory cat = new TicketCategory();
				temp.Category = new TicketCategory(Convert.ToInt32(dr["category_id"]));
				temp.CreationDate = Convert.ToDateTime(dr["creation_date"]);
				temp.Creator = dr["creator"].ToString();
				temp.Description = dr["description"].ToString();
				temp.DueDate = Convert.ToDateTime(dr["due_date"]);
				temp.Module = new TicketModule(Convert.ToInt32(dr["module_id"]));
				temp.Priority = (TicketPriority)Convert.ToInt32(dr["priority"]);
				temp.Requestor = new Requestor(Convert.ToInt32(dr["requestor_id"]));
				temp.Responses = new TicketResponseCollection();
				temp.Status = new TicketStatus(Convert.ToInt32(dr["status_id"]));
				temp.TicketId = TicketId;
			}
			else { temp = null; }

			return temp;

		}

		private TicketCollection LoadAllDataByArgs(string[] WhereArgs) { return new TicketCollection(); }
		private TicketCollection LoadBasicDataByArgs(string[] WhereArgs) { return new TicketCollection(); }

		#endregion

		#endregion

		#region -------------  Responses  -------------


		public override void CreateResponse(TicketResponse response)
		{
			throw new Exception("The method or operation is not implemented.");
		}

		public override void UpdateResponse(TicketResponse response)
		{
			throw new Exception("The method or operation is not implemented.");
		}

		public override TicketResponse GetResponse(int ResponseId)
		{
			throw new Exception("The method or operation is not implemented.");
		}

		public override void DeleteResponse(int ResponseId)
		{
			throw new Exception("The method or operation is not implemented.");
		}

		public override TicketResponseCollection GetResponsesByTicketId(int TicketId)
		{
			throw new Exception("The method or operation is not implemented.");
		}

		#endregion

		#region -------------  Assignment  -------------

		public override void CreateAssignment(Assignment assignment)
		{
			SqlParameter[] Params = new SqlParameter[]
			{
				new SqlParameter("@TicketId", assignment.TicketId),
				new SqlParameter("@Creator", assignment.Creator),
				new SqlParameter("@AssignedTo", assignment.AssignedTo),
				new SqlParameter("@AssignmentDate", assignment.AssignmentDate),
				new SqlParameter("@IsActive", assignment.IsActive)
			};
			assignment.AssignmentId = Convert.ToInt32(DBHelper.ExecScalarSQL(
				@"INSERT INTO Assignment (ticket_id, creator, assigned_to, assignment_date, is_active) 
					VALUES(@TicketId, @Creator, @AssignedTo, @AssignmentDate, @IsActive); SELECT SCOPE_IDENTITY();",
				Params, CommandType.Text));
		}

		public override void EditAssignment(Assignment assignment)
		{
			SqlParameter[] Params = new SqlParameter[]
			{
				new SqlParameter("@TicketId", assignment.TicketId),
				new SqlParameter("@Creator", assignment.Creator),
				new SqlParameter("@AssignedTo", assignment.AssignedTo),
				new SqlParameter("@AssignmentDate", assignment.AssignmentDate),
				new SqlParameter("@IsActive", assignment.IsActive), 
				new SqlParameter("@AssignmentId", assignment.AssignmentId)
			};
			DBHelper.ExecSQL(
				@"UPDATE Assignment 
					SET ticket_id = @TicketId, creator = @Creator, assigned_to = @AssignedTo, is_active = @IsActive,
						assignment_date = @AssignmentDate
					WHERE Assignment.assignment_id = @AssignmentId",
				Params, CommandType.Text);
		}

		public override AssignmentCollection GetAssignmentsByTicketId(int TicketId)
		{
			SqlParameter[] Params = new SqlParameter[] { new SqlParameter("@TicketId", TicketId) };

			//Fill the dataset w/ records that have the right QueueId
			DataSet ds = DBHelper.FillDataset(
				@"SELECT ticket_id, creator, assigned_to, is_active, assignment_date, assignment_id
				FROM Assignment
				WHERE Assignment.ticket_id = @TicketId;",
				Params, CommandType.Text);

			AssignmentCollection temp;

			////If it has rows, populate the collection, otherwise return null.
			if (DBHelper.DataSetHasRows(ds))
			{
				temp = new AssignmentCollection();

				foreach (DataRow dr in ds.Tables[0].Rows)
				{
					Assignment tempAssignment = new Assignment();
					tempAssignment.AssignedTo = dr["assigned_to"].ToString();
					tempAssignment.AssignmentDate = Convert.ToDateTime(dr["assignment_date"]);
					tempAssignment.AssignmentId = Convert.ToInt32(dr["assignment_id"]);
					tempAssignment.Creator = dr["creator"].ToString();
					tempAssignment.IsActive = Convert.ToBoolean(dr["is_active"]);
					tempAssignment.TicketId = TicketId;

					//Add to the collection!
					temp.Add(tempAssignment);
				}
			}
			else { temp = null; }

			return temp;
		}

		public override AssignmentCollection GetAssignmentsByUser(string user)
		{
			SqlParameter[] Params = new SqlParameter[] { new SqlParameter("@UserName", user) };

			//Fill the dataset w/ records that have the right QueueId
			DataSet ds = DBHelper.FillDataset(
				@"SELECT ticket_id, creator, assigned_to, is_active, assignment_date, assignment_id
				FROM Assignment
				WHERE Assignment.assigned_to = @UserName;",
				Params, CommandType.Text);

			AssignmentCollection temp;

			////If it has rows, populate the collection, otherwise return null.
			if (DBHelper.DataSetHasRows(ds))
			{
				temp = new AssignmentCollection();

				foreach (DataRow dr in ds.Tables[0].Rows)
				{
					Assignment tempAssignment = new Assignment();
					tempAssignment.AssignedTo = user;
					tempAssignment.AssignmentDate = Convert.ToDateTime(dr["assignment_date"]);
					tempAssignment.AssignmentId = Convert.ToInt32(dr["assignment_id"]);
					tempAssignment.Creator = dr["creator"].ToString();
					tempAssignment.IsActive = Convert.ToBoolean(dr["is_active"]);
					tempAssignment.TicketId = Convert.ToInt32(dr["ticket_id"]);

					//Add to the collection!
					temp.Add(tempAssignment);
				}
			}
			else { temp = new AssignmentCollection(); }

			return temp;
		}

		public override AssignmentCollection GetAssignmentByUserAndActive(string user, bool IsActive)
		{
			SqlParameter[] Params = new SqlParameter[] { 
				new SqlParameter("@UserName", user),
				new SqlParameter("@IsActive", IsActive)};

			//Fill the dataset w/ records that have the right QueueId
			DataSet ds = DBHelper.FillDataset(
				@"SELECT ticket_id, creator, assigned_to, is_active, assignment_date, assignment_id
				FROM Assignment
				WHERE Assignment.assigned_to = @UserName AND Assignment.is_active = @IsActive;",
				Params, CommandType.Text);

			AssignmentCollection temp;

			////If it has rows, populate the collection, otherwise return null.
			if (DBHelper.DataSetHasRows(ds))
			{
				temp = new AssignmentCollection();

				foreach (DataRow dr in ds.Tables[0].Rows)
				{
					Assignment tempAssignment = new Assignment();
					tempAssignment.AssignedTo = user;
					tempAssignment.AssignmentDate = Convert.ToDateTime(dr["assignment_date"]);
					tempAssignment.AssignmentId = Convert.ToInt32(dr["assignment_id"]);
					tempAssignment.Creator = dr["creator"].ToString();
					tempAssignment.IsActive = IsActive;
					tempAssignment.TicketId = Convert.ToInt32(dr["ticket_id"]);

					//Add to the collection!
					temp.Add(tempAssignment);
				}
			}
			else { temp = new AssignmentCollection(); }

			return temp;
		}

		public override Assignment GetAssignmentById(int AssignmentId)
		{
			SqlParameter[] Params = new SqlParameter[] { new SqlParameter("@AssignmentId", AssignmentId) };
			DataSet ds = DBHelper.FillDataset(
				@"	SELECT ticket_id, creator, assigned_to, assignment_date, is_active
					FROM Assignment
					WHERE Assignment.assignment_id = @AssignmentId;",
				Params, CommandType.Text);
			Assignment temp;

			if (DBHelper.DataSetHasRows(ds))
			{
				DataRow dr = ds.Tables[0].Rows[0];

				temp = new Assignment();
				temp.TicketId = Convert.ToInt32(dr["ticket_id"]);
				temp.AssignedTo = dr["assigned_to"].ToString();
				temp.AssignmentDate = Convert.ToDateTime(dr["assignment_date"]);
				temp.Creator = dr["creator"].ToString();
				temp.IsActive = Convert.ToBoolean(dr["is_active"]);
				temp.AssignmentId = AssignmentId;
			}
			else { temp = null; }

			return temp;
		}

		#endregion

		#region -------------  Ticket Module  -------------


		public override void CreateModule(TicketModule module)
		{
			SqlParameter[] Params = new SqlParameter[]
			{
				new SqlParameter("@Name", module.Name),
				new SqlParameter("@Description", module.Description),
				new SqlParameter("@IsActive", module.IsActive),
				new SqlParameter("@QueueId", module.TicketQueueId)
			};
			module.TicketModuleId = Convert.ToInt32(DBHelper.ExecScalarSQL(
				@"INSERT INTO Module ([name], description, queue_id, is_active) 
					VALUES(@Name, @Description, @QueueId, @IsActive); SELECT SCOPE_IDENTITY();",
				Params, CommandType.Text));
		}

		public override void EditModule(TicketModule module)
		{
			SqlParameter[] Params = new SqlParameter[]
			{
				new SqlParameter("@ModuleId", module.TicketModuleId),
				new SqlParameter("@Name", module.Name),
				new SqlParameter("@Description", module.Description),
				new SqlParameter("@QueueId", module.TicketQueueId),
				new SqlParameter("@IsActive", module.IsActive)
			};
			DBHelper.ExecSQL(
				@"UPDATE Module 
					SET [name] = @name, description = @Description, queue_id = @QueueId, is_active = @IsActive
					WHERE Module.module_id = @ModuleId",
				Params, CommandType.Text);
		}

		public override TicketModuleCollection GetModulesByQueueId(int QueueId)
		{
			SqlParameter[] Params = new SqlParameter[] { new SqlParameter("@QueueId", QueueId) };

			//Fill the dataset w/ records that have the right QueueId
			DataSet ds = DBHelper.FillDataset(
				@"	SELECT module_id, [name], description, queue_id, is_active
				FROM Module
				WHERE Module.queue_id = @QueueId;",
				Params, CommandType.Text);

			TicketModuleCollection temp;

			//If it has rows, populate the collection, otherwise return null.
			if (DBHelper.DataSetHasRows(ds))
			{
				temp = new TicketModuleCollection();

				foreach (DataRow dr in ds.Tables[0].Rows)
				{
					TicketModule tempTicketModule = new TicketModule();
					tempTicketModule.Description = dr["description"].ToString();
					tempTicketModule.IsActive = Convert.ToBoolean(dr["is_active"]);
					tempTicketModule.Name = dr["name"].ToString();
					tempTicketModule.TicketModuleId = Convert.ToInt32(dr["module_id"]);
					tempTicketModule.TicketQueueId = Convert.ToInt32(dr["queue_id"]);

					//Add to the collection!
					temp.Add(tempTicketModule);
				}
			}
			else { temp = null; }

			return temp;
		}

		public override TicketModule GetModule(int ModuleId)
		{
			SqlParameter[] Params = new SqlParameter[] { new SqlParameter("@ModuleId", ModuleId) };
			DataSet ds = DBHelper.FillDataset(
				@"	SELECT [name], description, queue_id, is_active
					FROM Module
					WHERE Module.module_id = @ModuleId;",
				Params, CommandType.Text);
			TicketModule temp;

			if (DBHelper.DataSetHasRows(ds))
			{
				DataRow dr = ds.Tables[0].Rows[0];

				temp = new TicketModule();
				temp.Name = dr["name"].ToString();
				temp.Description = dr["description"].ToString();
				temp.IsActive = Convert.ToBoolean(dr["is_active"]);
				temp.TicketQueueId = Convert.ToInt32(dr["queue_id"]);
				temp.TicketModuleId = ModuleId;
			}
			else { temp = null; }

			return temp;
		}

		public override TicketModuleCollection GetAllModules()
		{
			SqlParameter[] Params = new SqlParameter[0];

			//Fill the dataset w/ records that have the right QueueId
			DataSet ds = DBHelper.FillDataset(
				@"SELECT module_id, [name], description, queue_id, is_active
					FROM Module;", Params, CommandType.Text);

			TicketModuleCollection temp;

			//If it has rows, populate the collection, otherwise return null.
			if (DBHelper.DataSetHasRows(ds))
			{
				temp = new TicketModuleCollection();

				foreach (DataRow dr in ds.Tables[0].Rows)
				{
					TicketModule tempTicketModule = new TicketModule();
					tempTicketModule.Description = dr["description"].ToString();
					tempTicketModule.IsActive = Convert.ToBoolean(dr["is_active"]);
					tempTicketModule.Name = dr["name"].ToString();
					tempTicketModule.TicketModuleId = Convert.ToInt32(dr["module_id"]);
					tempTicketModule.TicketQueueId = Convert.ToInt32(dr["queue_id"]);

					//Add to the collection!
					temp.Add(tempTicketModule);
				}
			}
			else { temp = null; }

			return temp;
		}


		#endregion

		#region -------------  Ticket Status  -------------

		public override void CreateStatus(TicketStatus status)
		{
			//Adjust the sort order to the status on SQLSERVER...
			SqlParameter[] SortParams = new SqlParameter[] {
					new SqlParameter("@TableName", "Status"),
					new SqlParameter("@SortColumnName", "status_order"),
					new SqlParameter("@IndexColumnName", "status_id"),
					new SqlParameter("@RequestedIndex", status.TicketStatusId)
					};

			status.StatusOrder =
					DBHelper.ExecScalarSQL("SortTable", SortParams, CommandType.StoredProcedure);

			//Insert the new record...

			SqlParameter[] Params = new SqlParameter[] {
					new SqlParameter("@Name", status.Name),
					new SqlParameter("@Description", status.Description),
					new SqlParameter("@SortOrder", status.StatusOrder),
					new SqlParameter("@IsActive", status.IsActive)
					};

			//Insert the records and set the status id.
			status.TicketStatusId =

			Convert.ToInt32(
				DBHelper.ExecScalarSQL(
					@"INSERT INTO Status ([name], description, status_order, is_active)
						VALUES (@Name, @Description, 
						@SortOrder, @IsActive); SELECT SCOPE_IDENTITY();",
					Params, CommandType.Text)
			);
		}

		public override TicketStatus GetStatusById(int TicketStatusId)
		{
			SqlParameter[] Params = new SqlParameter[] { new SqlParameter("@StatusId", TicketStatusId) };
			DataSet ds = DBHelper.FillDataset(
				@"	SELECT [name], description, status_order, is_active
					FROM Status
					WHERE Status.status_id = @StatusId;",
				Params, CommandType.Text);
			TicketStatus temp;

			if (DBHelper.DataSetHasRows(ds))
			{
				DataRow dr = ds.Tables[0].Rows[0];

				temp = new TicketStatus();
				temp.Name = dr["name"].ToString();
				temp.Description = dr["description"].ToString();
				temp.IsActive = Convert.ToBoolean(dr["is_active"]);
				temp.StatusOrder = Convert.ToInt32(dr["status_order"]);
				temp.TicketStatusId = TicketStatusId;
			}
			else { temp = null; }

			return temp;
		}

		public override void EditStatus(TicketStatus status)
		{
			//Adjust the sort order to the status on SQLSERVER...
			SqlParameter[] SortParams = new SqlParameter[] {
					new SqlParameter("@TableName", "Status"),
					new SqlParameter("@SortColumnName", "status_order"),
					new SqlParameter("@IndexColumnName", "status_id"),
					new SqlParameter("@RequestedIndex", status.StatusOrder)
					};

			status.StatusOrder =
					DBHelper.ExecScalarSQL("SortTable", SortParams, CommandType.StoredProcedure);

			//Update the record.

			SqlParameter[] Params = new SqlParameter[]
			{
					new SqlParameter("@Name", status.Name),
					new SqlParameter("@Description", status.Description),
					new SqlParameter("@SortOrder", status.StatusOrder),
					new SqlParameter("@StatusId", status.TicketStatusId),
					new SqlParameter("@IsActive", status.IsActive)
			};
			DBHelper.ExecSQL(
				@"UPDATE Status 
					SET [name] = @Name, description = @Description, status_order = @SortOrder, is_active = @IsActive
					WHERE Status.status_id = @StatusId",
				Params, CommandType.Text);
		}

		public override TicketStatusCollection GetAllStatus()
		{
			SqlParameter[] Params = new SqlParameter[0];

			//Fill the dataset w/ records that have the right QueueId
			DataSet ds = DBHelper.FillDataset(
				@"SELECT status_id, [name], description, status_order, is_active
					FROM Status;", Params, CommandType.Text);

			TicketStatusCollection temp;

			//If it has rows, populate the collection, otherwise return null.
			if (DBHelper.DataSetHasRows(ds))
			{
				temp = new TicketStatusCollection();

				foreach (DataRow dr in ds.Tables[0].Rows)
				{
					TicketStatus TempTicketStatus = new TicketStatus();
					TempTicketStatus.Description = dr["description"].ToString();
					TempTicketStatus.IsActive = Convert.ToBoolean(dr["is_active"]);
					TempTicketStatus.Name = dr["name"].ToString();
					TempTicketStatus.StatusOrder = Convert.ToInt32(dr["status_order"]);
					TempTicketStatus.TicketStatusId = Convert.ToInt32(dr["status_id"]);

					//Add to the collection!
					temp.Add(TempTicketStatus);
				}
			}
			else { temp = null; }

			return temp;
		}

		#endregion

		#region -------------  Ticket Category  -------------

		public override void CreateCategory(TicketCategory category)
		{
			SqlParameter[] Params = new SqlParameter[] {
					new SqlParameter("@Name", category.Name),
					new SqlParameter("@Description", category.Description),
					new SqlParameter("@QueueId", category.QueueId),
					new SqlParameter("@IsActive", category.IsActive),
					};

			//Insert the records and set the status id.
			category.TicketCategoryId =

			Convert.ToInt32(
				DBHelper.ExecScalarSQL(
					@"INSERT INTO Category ([name], description, queue_id, is_active)
						VALUES (@Name, @Description, 
						@QueueId, @IsActive); SELECT SCOPE_IDENTITY();",
					Params, CommandType.Text));
		}

		public override TicketCategory GetCategoryById(int TicketCategoryid)
		{
			SqlParameter[] Params = new SqlParameter[] { new SqlParameter("@CategoryId", TicketCategoryid) };
			DataSet ds = DBHelper.FillDataset(
				@"	SELECT [name], description, queue_id, is_active
					FROM Category
					WHERE Category.ticket_category_id = @CategoryId;",
				Params, CommandType.Text);
			TicketCategory temp;

			if (DBHelper.DataSetHasRows(ds))
			{
				DataRow dr = ds.Tables[0].Rows[0];

				temp = new TicketCategory();
				temp.Name = dr["name"].ToString();
				temp.Description = dr["description"].ToString();
				temp.IsActive = Convert.ToBoolean(dr["is_active"]);
				temp.QueueId = Convert.ToInt32(dr["queue_id"]);
				temp.TicketCategoryId = TicketCategoryid;
			}
			else { temp = null; }

			return temp;
		}

		public override void EditCategory(TicketCategory category)
		{
			SqlParameter[] Params = new SqlParameter[]
			{
					new SqlParameter("@Name", category.Name),
					new SqlParameter("@Description", category.Description),
					new SqlParameter("@QueueId", category.QueueId),
					new SqlParameter("@CategoryId", category.TicketCategoryId),
					new SqlParameter("@IsActive", category.IsActive)
			};
			DBHelper.ExecSQL(
				@"UPDATE Category 
					SET [name] = @Name, description = @Description, queue_id = @QueueId, is_active = @IsActive
					WHERE Category.ticket_category_id = @CategoryId",
				Params, CommandType.Text);
		}

		public override TicketCategoryCollection GetAllCategory()
		{
			SqlParameter[] Params = new SqlParameter[0];

			//Fill the dataset w/ records that have the right QueueId
			DataSet ds = DBHelper.FillDataset(
				@"SELECT ticket_category_id, [name], description, queue_id, is_active
					FROM Category;", Params, CommandType.Text);

			TicketCategoryCollection temp;

			//If it has rows, populate the collection, otherwise return null.
			if (DBHelper.DataSetHasRows(ds))
			{
				temp = new TicketCategoryCollection();

				foreach (DataRow dr in ds.Tables[0].Rows)
				{
					TicketCategory TempTicketCategory = new TicketCategory();
					TempTicketCategory.Description = dr["description"].ToString();
					TempTicketCategory.IsActive = Convert.ToBoolean(dr["is_active"]);
					TempTicketCategory.Name = dr["name"].ToString();
					TempTicketCategory.QueueId = Convert.ToInt32(dr["queue_id"]);
					TempTicketCategory.TicketCategoryId = Convert.ToInt32(dr["ticket_category_id"]);

					//Add to the collection!
					temp.Add(TempTicketCategory);
				}
			}
			else { temp = null; }

			return temp;
		}

		#endregion

		#region -------------  Requestor  -------------

		public override void CreateRequestor(Requestor requestor)
		{
			SqlParameter[] Params = new SqlParameter[] {
					new SqlParameter("@FirstName", requestor.FirstName),
					new SqlParameter("@LastName", requestor.LastName),
					new SqlParameter("@Email", requestor.Email),
					new SqlParameter("@ContactNumber", requestor.ContactNumber),
					};

			//Insert the records and set the status id.
			requestor.RequestorId =

			Convert.ToInt32(
				DBHelper.ExecScalarSQL(
					@"INSERT INTO Requestor (first_name, last_name, email, contact_number)
						VALUES (@FirstName, @LastName, 
						@Email, @ContactNumber); SELECT SCOPE_IDENTITY();",
					Params, CommandType.Text));
		}

		public override void EditRequestor(Requestor requestor)
		{
			SqlParameter[] Params = new SqlParameter[]
			{
					new SqlParameter("@FirstName", requestor.FirstName),
					new SqlParameter("@LastName", requestor.LastName),
					new SqlParameter("@Email", requestor.Email),
					new SqlParameter("@ContactNumber", requestor.ContactNumber),
					new SqlParameter("@RequestorId", requestor.RequestorId)
			};
			DBHelper.ExecSQL(
				@"UPDATE Requestor 
					SET first_name = @FirstName, last_name = @LastName, email = @Email, contact_number = @ContactNumber
					WHERE Requestor.requestor_id = @RequestorId",
				Params, CommandType.Text);
		}

		public override Requestor GetRequestorById(int RequestorId)
		{
			SqlParameter[] Params = new SqlParameter[] { new SqlParameter("@RequestorId", RequestorId) };
			DataSet ds = DBHelper.FillDataset(
				@"	SELECT first_name, last_name, email, contact_number
					FROM Requestor
					WHERE Requestor.requestor_id = @RequestorId;",
				Params, CommandType.Text);
			Requestor temp;

			if (DBHelper.DataSetHasRows(ds))
			{
				DataRow dr = ds.Tables[0].Rows[0];

				temp = new Requestor();
				temp.FirstName = dr["first_name"].ToString();
				temp.LastName = dr["last_name"].ToString();
				temp.Email = dr["email"].ToString();
				temp.ContactNumber = dr["contact_number"].ToString();
				temp.RequestorId = RequestorId;
			}
			else { temp = null; }

			return temp;
		}

		public override RequestorCollection GetAllRequestor()
		{
			SqlParameter[] Params = new SqlParameter[0];

			//Fill the dataset w/ records that have the right QueueId
			DataSet ds = DBHelper.FillDataset(
				@"SELECT first_name, last_name, email, contact_number, requestor_id FROM Requestor;",
					Params, CommandType.Text);

			RequestorCollection temp;

			//If it has rows, populate the collection, otherwise return null.
			if (DBHelper.DataSetHasRows(ds))
			{
				temp = new RequestorCollection();

				foreach (DataRow dr in ds.Tables[0].Rows)
				{
					Requestor TempRequestor = new Requestor();
					TempRequestor.FirstName = dr["first_name"].ToString();
					TempRequestor.LastName = dr["last_name"].ToString();
					TempRequestor.ContactNumber = dr["contact_number"].ToString();
					TempRequestor.Email = dr["email"].ToString();
					TempRequestor.RequestorId = Convert.ToInt32(dr["requestor_id"]);

					//Add to the collection!
					temp.Add(TempRequestor);
				}
			}
			else { temp = null; }

			return temp;
		}

		#endregion

		#region -------------  Charge Code  -------------

		public override void CreateChargeCode(TicketChargeCode ChargeCode)
		{
			SqlParameter[] Params = new SqlParameter[] {
					new SqlParameter("@ChargeCode", ChargeCode.ChargeCode),
					new SqlParameter("@Description", ChargeCode.Description),
					new SqlParameter("@ExpirationDate", ChargeCode.ExpirationDate),
					new SqlParameter("@IsActive", ChargeCode.IsActive)
					};

			//Insert the records and set the status id.
			ChargeCode.ChargeCodeId =

			Convert.ToInt32(
				DBHelper.ExecScalarSQL(
					@"INSERT INTO ChargeCode (charge_code, description, expiration_date, is_active)
						VALUES (@ChargeCode, @Description, 
						@ExpirationDate, @IsActive); SELECT SCOPE_IDENTITY();",
					Params, CommandType.Text));
		}

		public override void EditChargeCode(TicketChargeCode ChargeCode)
		{
			SqlParameter[] Params = new SqlParameter[]
			{
					new SqlParameter("@ChargeCode", ChargeCode.ChargeCode),
					new SqlParameter("@Description", ChargeCode.Description),
					new SqlParameter("@ExpirationDate", ChargeCode.ExpirationDate),
					new SqlParameter("@IsActive", ChargeCode.IsActive),
					new SqlParameter("@ChargeCodeId", ChargeCode.ChargeCodeId),

			};
			DBHelper.ExecSQL(
				@"UPDATE ChargeCode 
					SET charge_code = @ChargeCode, description = @Description, expiration_date = @ExpirationDate, is_active = @IsActive
					WHERE ChargeCode.charge_code_id = @ChargeCodeId",
				Params, CommandType.Text);
		}

		public override TicketChargeCode GetChargeCodeById(int ChargeCodeId)
		{
			SqlParameter[] Params = new SqlParameter[] { new SqlParameter("@ChargeCodeId", ChargeCodeId) };
			DataSet ds = DBHelper.FillDataset(
				@"	SELECT charge_code, description, expiration_date, is_active
					FROM ChargeCode
					WHERE ChargeCode.charge_code_id = @ChargeCodeId;",
				Params, CommandType.Text);
			TicketChargeCode temp;

			if (DBHelper.DataSetHasRows(ds))
			{
				DataRow dr = ds.Tables[0].Rows[0];

				temp = new TicketChargeCode();
				temp.ChargeCode = dr["charge_code"].ToString();
				temp.Description = dr["description"].ToString();
				temp.ExpirationDate = Convert.ToDateTime(dr["expiration_date"]);
				temp.IsActive = Convert.ToBoolean(dr["is_active"]);
				temp.ChargeCodeId = ChargeCodeId;
			}
			else { temp = null; }

			return temp;
		}

		public override TicketChargeCodeCollection GetAllChargeCode()
		{
			SqlParameter[] Params = new SqlParameter[0];

			//Fill the dataset w/ records that have the right QueueId
			DataSet ds = DBHelper.FillDataset(
				@"SELECT charge_code, description, expiration_date, is_active, charge_code_id
					FROM ChargeCode;", Params, CommandType.Text);

			TicketChargeCodeCollection temp;

			//If it has rows, populate the collection, otherwise return null.
			if (DBHelper.DataSetHasRows(ds))
			{
				temp = new TicketChargeCodeCollection();

				foreach (DataRow dr in ds.Tables[0].Rows)
				{
					TicketChargeCode TempCC = new TicketChargeCode();
					TempCC.Description = dr["description"].ToString();
					TempCC.IsActive = Convert.ToBoolean(dr["is_active"]);
					TempCC.ChargeCode = dr["charge_code"].ToString();
					TempCC.ExpirationDate = Convert.ToDateTime(dr["expiration_date"]);
					TempCC.ChargeCodeId = Convert.ToInt32(dr["charge_code_id"]);

					//Add to the collection!
					temp.Add(TempCC);
				}
			}
			else { temp = null; }

			return temp;
		}

		public override TicketChargeCodeCollection GetAllChargeCodeByActive(bool IsActive)
		{
			SqlParameter[] Params = new SqlParameter[] { new SqlParameter("@IsActive", IsActive) };

			//Fill the dataset w/ records that have the right QueueId
			DataSet ds = DBHelper.FillDataset(
				@"SELECT charge_code, description, expiration_date, charge_code_id
					FROM ChargeCode
					WHERE is_active = @IsActive;",
					Params, CommandType.Text);

			TicketChargeCodeCollection temp;

			//If it has rows, populate the collection, otherwise return null.
			if (DBHelper.DataSetHasRows(ds))
			{
				temp = new TicketChargeCodeCollection();

				foreach (DataRow dr in ds.Tables[0].Rows)
				{
					TicketChargeCode TempCC = new TicketChargeCode();
					TempCC.Description = dr["description"].ToString();
					TempCC.IsActive = IsActive;
					TempCC.ChargeCode = dr["charge_code"].ToString();
					TempCC.ExpirationDate = Convert.ToDateTime(dr["expiration_date"]);
					TempCC.ChargeCodeId = Convert.ToInt32(dr["charge_code_id"]);

					//Add to the collection!
					temp.Add(TempCC);
				}
			}
			else { temp = null; }

			return temp;
		}

		#endregion
	}
}

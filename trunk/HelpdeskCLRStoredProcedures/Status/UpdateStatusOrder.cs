using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;
using System.Collections;

public partial class UserDefinedFunctions
{
	/// <summary>
	/// Reorders the status order of the current status entries and returns the new status order of the requesting stored proc.
	/// </summary>
	/// <param name="StatusOrder">New status order for status record in question.</param>
	/// <returns></returns>
	[Microsoft.SqlServer.Server.SqlFunction]
	public static SqlInt32 UpdateStatusOrder(int StatusOrder)
	{
		SqlConnection conn;
		ArrayList IndexArray;
		GetAllStatusIdsOrderedByCurrentStatusOrder(out conn, out IndexArray);

		int RowCount = 1;
		int NewInsertOrder = 0;

		for (int x = 0; x < IndexArray.Count; x++)
		{
			//If true, this is the spot were the user is requesting we insert the new status order.
			if (StatusOrder == RowCount)
			{
				NewInsertOrder = StatusOrder;
				RowCount++;
			}
			//We are at the end of the list and need to set the status order to be last.
			else if (IndexArray.Count == RowCount || StatusOrder == 0)
			{
				NewInsertOrder = RowCount + 1;
			}

			SqlCommand updateCmd = new SqlCommand(@"Update Status SET status_order = @order 
									WHERE status_id = @status_id", conn);
			updateCmd.Parameters.Add(new SqlParameter("@order", RowCount));
			updateCmd.Parameters.Add(new SqlParameter("@status_id", IndexArray[x]));
			updateCmd.ExecuteNonQuery();

			RowCount++;
		}

		//Always clean up.
		conn.Close();

		return NewInsertOrder;
	}

	private static void GetAllStatusIdsOrderedByCurrentStatusOrder(out SqlConnection conn, out ArrayList IndexArray)
	{
		//Create the connection
		conn = new SqlConnection();
		conn.ConnectionString = "Context Connection=true";

		//Set up the reader and command object.
		SqlCommand cmd = new SqlCommand(
			@"SELECT status_id
				FROM Status
				WHERE is_active = 1 
				ORDER BY status_order",
			conn);

		IndexArray = new ArrayList();

		//Attempt to open up the connection and lop through the results, adding the appropriate
		//data to the arraylists.  This will allow us to sort in a sec.

		conn.Open();
		SqlDataReader rdr = cmd.ExecuteReader();
		while (rdr.Read())
		{
			IndexArray.Add(Convert.ToInt32(rdr["status_id"]));
		}

		rdr.Close();
	}
};


using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Diagnostics;

namespace HelpdeskModule
{
	public class DBHelper
	{
		//TODO:  Set this to read from the web.config when it is posted live.
		/// <summary>
		/// Database connection string.
		/// </summary>
		const string DBConnStr = @"Data Source=localhost\devsql;Initial Catalog=Helpdesk;Integrated Security=True";

		/// <summary>
		/// Returns filled dataset from stored procedure name and its parameters
		/// </summary>
		public static DataSet FillDataset(string Statement, SqlParameter[] Params, CommandType SQLCommandType)
		{
			SqlConnection myConnection = new SqlConnection(DBConnStr);
			SqlDataAdapter myAdapter = new SqlDataAdapter();

			myAdapter.SelectCommand = new SqlCommand(Statement, myConnection);
			myAdapter.SelectCommand.CommandType = SQLCommandType;

			// assign all parameters with its values
			for (int i = 0; i < Params.Length; i++)
			{
				myAdapter.SelectCommand.Parameters.Add(Params[i]);
			}

			DataSet myDataSet = new DataSet();

			myAdapter.Fill(myDataSet);

			return myDataSet;
		}

		/// <summary>
		/// Executes stored procedure with its parameters
		/// </summary>
		public static void ExecSQL(string Statement, SqlParameter[] Params, CommandType SQLCommandType)
		{
			SqlConnection myConnection = new SqlConnection(DBConnStr);
			SqlCommand myCmd = new SqlCommand(Statement, myConnection);
			myCmd.CommandType = SQLCommandType;

			// assign all parameters with its values
			for (int i = 0; i < Params.Length; i++)
			{
				myCmd.Parameters.Add(Params[i]);
			}

			try
			{
				myConnection.Open();
				myCmd.ExecuteNonQuery();
			}
			finally
			{
				myConnection.Close();
			}
		}

		/// <summary>
		/// Executes stored procedure with its parameters
		/// </summary>
		public static int ExecScalarSQL(string Statement, SqlParameter[] Params, CommandType SQLCommandType)
		{
			int ReturnValue;

			SqlConnection myConnection = new SqlConnection(DBConnStr);
			SqlCommand myCmd = new SqlCommand(Statement, myConnection);
			myCmd.CommandType = SQLCommandType;

			// assign all parameters with its values
			for (int i = 0; i < Params.Length; i++)
			{
				myCmd.Parameters.Add(Params[i]); ;
			}

			try
			{
				myConnection.Open();
				ReturnValue = System.Convert.ToInt32(myCmd.ExecuteScalar());
			}
			finally
			{
				myConnection.Close();
			}

			return ReturnValue;
		}

		/// <summary>
		/// Checks a dataset to make sure that it has at least 1 table with 1 row in it.
		/// </summary>
		public static bool DataSetHasRows(DataSet ds)
		{
			return ds.Tables.Count > 0 & ds.Tables[0].Rows.Count > 0;
		}
	}
}

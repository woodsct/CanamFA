using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace CanamLiveFA.DAL
{
    public class BaseDal
    {
        protected static int GetIntValue(object obj)
        {
            int returnValue = 0;
            if (!obj.Equals(DBNull.Value))
                returnValue = int.Parse(obj.ToString());
            return returnValue;
        }

        protected static decimal GetDecimalValue(object obj)
        {
            decimal returnValue = 0;
            if (!obj.Equals(DBNull.Value))
                returnValue = decimal.Parse(obj.ToString());
            return returnValue;
        }

        protected static double GetDoubleValue(object obj)
        {
            double returnValue = 0;
            if (!obj.Equals(DBNull.Value))
                returnValue = double.Parse(obj.ToString());
            return returnValue;
        }

        protected static bool GetBooleanValue(object obj)
        {
            bool returnValue = false;
            if (!obj.Equals(DBNull.Value))
                returnValue = (bool)obj;
            return returnValue;
        }

        protected static string GetStringValue(object obj)
        {
            string returnValue = string.Empty;
            if (!obj.Equals(DBNull.Value))
                returnValue = obj.ToString();
            return returnValue;
        }

        public static SqlDataReader SelectData(string procedureName, Hashtable inParameters, Hashtable outParameters)
        {
            SqlDataReader sqlDataReader = null;
            SqlCommand commandObj = InitializeSqlCommand(procedureName);
            SetProcedureInputOutputParameters(ref commandObj, inParameters, outParameters);

            try
            {
                if (commandObj.Transaction == null)
                {
                    commandObj.Connection.Open();
                    sqlDataReader = commandObj.ExecuteReader(CommandBehavior.CloseConnection);
                }
                else
                    sqlDataReader = commandObj.ExecuteReader();
            }
            catch (Exception)
            {
                if (commandObj.Transaction == null && commandObj.Connection != null)
                {
                    commandObj.Connection.Close();
                    if (sqlDataReader != null)
                        sqlDataReader.Close();
                }
                throw;
            }

            RetrieveValuesOfOutputParameters(commandObj, outParameters);
            return sqlDataReader;
        }

        public static DataTable SelectDataInDataTable(string procedureName, Hashtable inParameters, Hashtable outParameters)
        {
            DataTable dTable = new DataTable();
            SqlDataAdapter dAdapter = new SqlDataAdapter();
            SqlCommand commandObj = InitializeSqlCommand(procedureName);
            SetProcedureInputOutputParameters(ref commandObj, inParameters, outParameters);

            try
            {
                if (commandObj.Transaction == null)
                {
                    commandObj.Connection.Open();
                    dAdapter.SelectCommand = commandObj;
                    dAdapter.Fill(dTable);
                }
                else
                {
                    dAdapter.SelectCommand = commandObj;
                    dAdapter.Fill(dTable);
                }
            }
            catch (Exception)
            {
                throw;
            }

            RetrieveValuesOfOutputParameters(commandObj, outParameters);
            return dTable;
        }

        public static int ExecuteProcedure(string procedureName, Hashtable inParameters, Hashtable OutParameters)
        {
            var rValue = 0;
            SqlCommand commandObj = InitializeSqlCommand(procedureName);
            SetProcedureInputOutputParameters(ref commandObj, inParameters, OutParameters);

            try 
            {
                if (commandObj.Transaction == null)
                {
                    commandObj.Connection.Open();
                }
                rValue = commandObj.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (commandObj.Transaction == null && commandObj.Connection != null)
                    commandObj.Connection.Close();
            }
            RetrieveValuesOfOutputParameters(commandObj, OutParameters);
            return rValue;
        }

        private static SqlCommand InitializeSqlCommand(string procedureName)
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["CanamLiveFAdb"].ConnectionString);
            SqlCommand command = new SqlCommand("dbo." + procedureName, connection);
            command.CommandType = CommandType.StoredProcedure;

            return command;
        }

        public static void SetProcedureInputOutputParameters(ref SqlCommand commandObj, Hashtable inParameters, Hashtable outParameters)
        {
            if (inParameters != null)
            {
                foreach (string parameter in inParameters.Keys)
                {
                    SqlParameter param = new SqlParameter(parameter, inParameters[parameter]);
                    if (param.Value != null && param.Value.GetType() == typeof(string))
                    {
                        param.Value = param.Value.ToString().Trim();
                    }

                    commandObj.Parameters.Add(param);
                }
            }

            if (outParameters != null)
            {
                foreach (string parameter in outParameters.Keys)
                {
                    SqlParameter param = new SqlParameter(parameter, outParameters[parameter]);
                    param.Direction = ParameterDirection.Output;
                    param.Size = Int32.MaxValue;
                    commandObj.Parameters.Add(param);
                }
            }
        }

        public static void RetrieveValuesOfOutputParameters(SqlCommand commandObj, Hashtable outParameters)
        {
            if (outParameters!=null)
            {
                string[] outParameterNames = new string[outParameters.Count];
                outParameters.Keys.CopyTo(outParameterNames, 0);
                foreach (string parameter in outParameterNames)
                {
                    outParameters[parameter] = commandObj.Parameters[parameter].Value;
                }
            }
        }
    }
}
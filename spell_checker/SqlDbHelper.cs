using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace DAL
{
    public class SqlDbHelper
    {
        public static string CONNECTION_STRING = ConfigurationManager.AppSettings["eng"];

        public static DataRowCollection ExecuteSelectSqlDataRow(string cmdText)
        {
            DataRowCollection dr = ExecuteSelectSqlDataSet(cmdText).Tables[0].Rows;
            return dr;
        }

        public static DataRowCollection ExecuteSelectSqlDataRow(string cmdText, Paramete[] param)
        {
            DataRowCollection dr = ExecuteSelectSqlDataSet(cmdText, param).Tables[0].Rows;
            return dr;
        }


        public static DataTable ExecuteSelectSqlDataTable(string cmdText)
        {
            return ExecuteSelectSqlDataSet(cmdText).Tables[0];
        }

        public static DataTable ExecuteSelectSqlDataTable(string cmdText, Paramete[] param)
        {
            return ExecuteSelectSqlDataSet(cmdText, param).Tables[0];
        }

        public static DataTable ExecuteSelectSqlDataTable(string cmdText, List<Paramete> param)
        {
            return ExecuteSelectSqlDataSet(cmdText, param.ToArray()).Tables[0];
        }

        public static DataSet ExecuteSelectSqlDataSet(string cmdText)
        {
            SqlConnection conn = new SqlConnection();
            SqlCommand cmd = new SqlCommand();
            PreCommandText(conn, cmd, cmdText);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            try
            {
                da.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                conn.Close();
            }
        }

        public static DataSet ExecuteSelectSqlDataSet(string cmdText, Paramete[] param)
        {
            SqlConnection conn = new SqlConnection();
            SqlCommand cmd = new SqlCommand();
            PreCommandText(conn, cmd, cmdText, param);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            try
            {
                da.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                conn.Close();
            }
        }


        public static bool ExecuteInsertTransactionData(string tableName, DataTable dt)
        {
            bool result = false;
            using (var con = new SqlConnection(CONNECTION_STRING))
            {
                con.Open();
                using (var tx = con.BeginTransaction())
                {
                    using (var cmd = con.CreateCommand())
                    {
                        cmd.Transaction = tx;

                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.Text;

                        List<string> key = new List<string>();
                        List<string> value = new List<string>();
                        for (int i = 0; i < dt.Columns.Count; i++)
                        {
                            key.Add(dt.Columns[i].ColumnName);
                            value.Add("@" + dt.Columns[i].ColumnName);
                        }

                        StringBuilder sb = new StringBuilder();
                        sb.Append("insert into").Append(" "); ;
                        sb.Append(tableName).Append(" ");
                        sb.Append("(").Append(" ");
                        sb.Append(string.Join(",", key.ToArray())).Append(" ");
                        sb.Append(")").Append(" ");

                        sb.Append("values").Append(" ");
                        sb.Append("(").Append(" ");
                        sb.Append(string.Join(",", value.ToArray())).Append(" ");
                        sb.Append(")").Append(" ");

                        string cmdText = sb.ToString();
                        cmd.CommandText = cmdText;
                        foreach (DataRow dto in dt.Rows)
                        {
                            SqlParameter[] param = new SqlParameter[dt.Columns.Count];
                            for (int i = 0; i < dt.Columns.Count; i++)
                            {
                                param[i] = new SqlParameter(value[i], dto[i]);
                            }

                            if (param != null)
                            {
                                cmd.Parameters.AddRange(param);
                            }
                            cmd.ExecuteNonQuery();
                            cmd.Parameters.Clear();
                        }
                    }

                    tx.Commit();
                    result = true;
                }
                con.Close();
            }
            return result;
        }

        public void ExecuteSqlBulkCopyInsertData(string tableName, DataTable dt, int BulkCopyTimeout)
        {
            //using (var ts = new TransactionScope())
            //{
            using (var connection = new SqlConnection(CONNECTION_STRING))
            {
                connection.Open();
                using (var bulkCopy = new SqlBulkCopy(connection))
                {
                    bulkCopy.BulkCopyTimeout = BulkCopyTimeout;
                    bulkCopy.DestinationTableName = tableName;
                    bulkCopy.WriteToServer(dt);
                }
            }

        }

        public static void ExecuteSqlBulkCopyInsertData(string tableName, DataTable dt)
        {

            using (var connection = new SqlConnection(CONNECTION_STRING))
            {
                connection.Open();
                using (var bulkCopy = new SqlBulkCopy(connection))
                {
                    bulkCopy.BulkCopyTimeout = 60 * 5;
                    bulkCopy.DestinationTableName = tableName;
                    bulkCopy.WriteToServer(dt);
                }
            }

        }


        public static bool ExecuteNonQuery(string cmdText)
        {
            SqlConnection conn = new SqlConnection();
            SqlCommand cmd = new SqlCommand();
            PreCommandText(conn, cmd, cmdText);
            bool result = false;
            try
            {
                cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                result = true;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                conn.Close();
            }
            return result;
        }

        public static bool ExecuteNonQuery(string cmdText, Paramete[] param)
        {
            SqlConnection conn = new SqlConnection();
            SqlCommand cmd = new SqlCommand();
            PreCommandText(conn, cmd, cmdText, param);
            bool result = false;
            try
            {
                cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                result = true;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                conn.Close();
            }
            return result;
        }

        public static int ExecuteNonQueryINT(string cmdText, Paramete[] param)
        {
            SqlConnection conn = new SqlConnection();
            SqlCommand cmd = new SqlCommand();
            PreCommandText(conn, cmd, cmdText, param);
            int result = 0;
            try
            {
                result = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                conn.Close();
            }
            return result;
        }


        public static bool ExecuteNonQuery(string cmdText, List<Paramete> param)
        {
            SqlConnection conn = new SqlConnection();
            SqlCommand cmd = new SqlCommand();
            PreCommandText(conn, cmd, cmdText, param.ToArray());
            bool result = false;
            try
            {
                cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                result = true;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                conn.Close();
            }
            return result;
        }

        public static object ExecuteScalar(string cmdText)
        {
            SqlConnection conn = new SqlConnection();
            SqlCommand cmd = new SqlCommand();
            PreCommandText(conn, cmd, cmdText);
            object obj = null;
            try
            {
                obj = cmd.ExecuteScalar();
                cmd.Parameters.Clear();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                conn.Close();
            }
            return obj;
        }

        public static object ExecuteScalar(string cmdText, Paramete[] param)
        {
            SqlConnection conn = new SqlConnection();
            SqlCommand cmd = new SqlCommand();
            PreCommandText(conn, cmd, cmdText, param);
            object obj = null;
            try
            {
                obj = cmd.ExecuteScalar();
                cmd.Parameters.Clear();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                conn.Close();
            }
            return obj;
        }

        public static SqlDataReader ExecuteReaderSql(string cmdText)
        {
            SqlConnection conn = new SqlConnection();
            SqlCommand cmd = new SqlCommand();
            PreCommandText(conn, cmd, cmdText);
            SqlDataReader dr = null;
            try
            {
                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                cmd.Parameters.Clear();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                conn.Close();
            }
            return dr;
        }

        public static SqlDataReader ExecuteReaderSql(string cmdText, Paramete[] param)
        {
            SqlConnection conn = new SqlConnection();
            SqlCommand cmd = new SqlCommand();
            PreCommandText(conn, cmd, cmdText, param);
            SqlDataReader dr = null;
            try
            {
                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                cmd.Parameters.Clear();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                conn.Close();
            }
            return dr;
        }

        private static void PreCommandProc(SqlConnection conn, SqlCommand cmd, string cmdText, Paramete[] param)
        {
            conn = new SqlConnection(CONNECTION_STRING);
            if (conn.State == ConnectionState.Closed)
                conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            cmd.CommandType = CommandType.StoredProcedure;
            if (param != null)
                for (int i = 0; i < param.Length; i++)
                {
                    cmd.Parameters.AddWithValue(param[i].Name.ToString(), param[i].Value);
                }
        }

        private static void PreCommandText(SqlConnection conn, SqlCommand cmd, string cmdText, Paramete[] param)
        {
            PreCommandText(conn, cmd, cmdText);
            if (param != null)
                for (int i = 0; i < param.Length; i++)
                {
                    cmd.Parameters.AddWithValue(param[i].Name.ToString(), param[i].Value);
                }
        }


        private static void PreCommandText(SqlConnection conn, SqlCommand cmd, string cmdText)
        {
            conn = new SqlConnection(CONNECTION_STRING);
            if (conn.State == ConnectionState.Closed)
                conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            cmd.CommandType = CommandType.Text;
        }

        public static string MessageBoxJsText(string TxtMessage, string Url)
        {
            string str;
            str = "<script language=javascript>alert('" + TxtMessage + "');location='" + Url + "'</script>";
            return str;
        }

        public static string MessageBoxJsText(string TxtMessage)
        {
            string str;
            str = "<script language=javascript>alert('" + TxtMessage + "')</script>";
            return str;
        }

        public static string MessageBox(string TxtMessage, string Url)
        {
            string str;
            str = "<script language=javascript>alert('" + TxtMessage + "');location='" + Url + "'</script>";
            return str;
        }

        public static string MessageBox(string TxtMessage)
        {
            string str;
            str = "<script language=javascript>alert('" + TxtMessage + "')</script>";
            return str;
        }
    }

    public class Paramete
    {
        public object Name;
        public object Value;

        public Paramete(string Name, object Value)
        {
            this.Name = Name;
            this.Value = Value;
        }
    }
}
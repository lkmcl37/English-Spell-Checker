using System;
using System.Windows.Forms;
using System.Data.SQLite;
using System.Data;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.Serialization;

namespace SqlLiteHelper
{
    public static class Sql
    {
        public static string Quanxian;
        public static string Name;
        public static string PassWord = "";
        public static string dbFileName = "db.dll";
        public static string connStr = "data source=" + Application.StartupPath + @"\" + dbFileName;
        public static void CreateDataBase()
        {
            if (!File.Exists(dbFileName))
            {
                string path = Application.StartupPath + @"\" + dbFileName;
                SQLiteConnection.CreateFile(path);
                using (SQLiteConnection conn = new SQLiteConnection(connStr))
                {
                    conn.Open();
                    conn.ChangePassword(PassWord);

                    string sql = "create table wordlist(ID,word,explain,ping,difficult,pronunciation)";
                    SQLiteCommand cmd = new SQLiteCommand(sql, conn);
                    cmd.ExecuteNonQuery();



                }
                MessageBox.Show("Sucessfully create DB！");
            }
        }

        public static void ReCreateDataBase()
        {
            if (File.Exists(dbFileName))
            {
                File.Delete(dbFileName);
            }
            CreateDataBase();
            MessageBox.Show("Sucessfully recreate DB！");
        }

        public static DataTable ExecuteSelectSqlDataTable(string sql)
        {
            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                SQLiteCommand cmd = new SQLiteCommand();
                PrepareCommond(cmd, CommandType.Text, sql, conn);
                SQLiteDataAdapter dp = new SQLiteDataAdapter(sql, conn);
                DataSet ds = new DataSet();
                dp.Fill(ds, "tb");
                return ds.Tables[0];
            }
        }

        private static DataTable SelectDataTable(string sql)
        {
            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                SQLiteCommand cmd = new SQLiteCommand();
                PrepareCommond(cmd, CommandType.Text, sql, conn);
                SQLiteDataAdapter dp = new SQLiteDataAdapter(sql, conn);
                DataSet ds = new DataSet();
                dp.Fill(ds, "tb");
                return ds.Tables[0];
            }
        }


        public static DataTable SelectDataTable(string sql, Paramete[] queryParameter)
        {

            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                SQLiteCommand cmd = new SQLiteCommand();
                PrepareCommond(cmd, CommandType.Text, queryParameter, sql, conn);
                SQLiteDataAdapter dp = new SQLiteDataAdapter(cmd);
                DataSet ds = new DataSet();
                dp.Fill(ds, "tb");
                return ds.Tables[0];
            }
        }

        public static DataTable SelectDataTableHasParams(string sql, params Paramete[] queryParameter)
        {
            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                SQLiteCommand cmd = new SQLiteCommand();
                PrepareCommond(cmd, CommandType.Text, queryParameter, sql, conn);
                SQLiteDataAdapter dp = new SQLiteDataAdapter(cmd);
                DataSet ds = new DataSet();
                dp.Fill(ds, "tb");
                return ds.Tables[0];
            }
        }



        public static bool ExecuteNonQuery(string sql)
        {
            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                conn.SetPassword(PassWord);
                conn.Open();
                SQLiteCommand cmd = new SQLiteCommand(sql, conn);
                int num = cmd.ExecuteNonQuery();
                sql = "vacuum";
                cmd = new SQLiteCommand(sql, conn);
                cmd.ExecuteNonQuery();
                return num >= 1 ? true : false;
            }
        }

        public static bool ExecuteNonQuery(string sql, Paramete[] queryParameter)
        {
            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                SQLiteCommand cmd = new SQLiteCommand();
                PrepareCommond(cmd, CommandType.Text, queryParameter, sql, conn);
                int num = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                conn.Close();
                return num > 0;
            }
        }


        public static int ExecuteNonQueryHasParams(string sql, params Paramete[] queryParameter)
        {
            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                SQLiteCommand cmd = new SQLiteCommand();
                PrepareCommond(cmd, CommandType.Text, queryParameter, sql, conn);
                int num = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                conn.Close();
                return num;
            }
        }

        private static void PrepareCommond(SQLiteCommand cmd, CommandType commandType, string commandtext, SQLiteConnection conn)
        {
            PrepareCommond(cmd, commandType, new Paramete[0], commandtext, conn);
        }

        private static void PrepareCommond(SQLiteCommand cmd, CommandType commandType, Paramete[] parameters, string commandtext, SQLiteConnection conn)
        {
            conn.SetPassword(PassWord);
            if (conn.State == ConnectionState.Closed)
                conn.Open();
            cmd.CommandType = commandType;
            cmd.CommandText = commandtext;
            cmd.Connection = conn;
            if (parameters != null && parameters.Length > 0)
            {
                for (int i = 0; i < parameters.Length; i++)
                {
                    cmd.Parameters.AddWithValue(parameters[i].Name, parameters[i].Value);
                }
            }
        }}

    public class Paramete
    {
        public string Name { get; set; }
        public object Value { get; set; }
        public DbType DbType { get; set; }
        public Paramete(string Name, object Value)
        {
            this.Name = Name;
            this.Value = Value;
        }
    }

}

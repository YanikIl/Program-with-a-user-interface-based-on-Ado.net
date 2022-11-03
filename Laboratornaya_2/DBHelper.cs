using Microsoft.Data.SqlClient;
using Microsoft.VisualBasic.Devices;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laboratornaya_2
{
    public static class DBHelper
    {
        private static SqlConnection? _conn;

        public static bool Connect(string connStr)
        {
            try
            {
                _conn = new SqlConnection(connStr);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static void CreateMainTable()
        {
            var query = "Create Table player(" +
                "id int not null primary key identity," +
                "fname varchar(50) not null," +
                "lname varchar(50) not null," +
                "[group] varchar(6) not null," +
                "rating float not null," +
                "birth date not null," +
                "gender bit not null default 1);";

            var cmd = new SqlCommand();
            cmd.CommandText = query;
            ExecuteNonQuery(cmd);
        }

        public static void CreateTableGroups()
        {
            var query = "Create Table Groups(" +
                "Id int not null primary key identity," +
                "Group varchar(50) not null);";

            var cmd = new SqlCommand();
            cmd.CommandText = query;
            ExecuteNonQuery(cmd);
        }

        public static void ExecuteNonQuery(SqlCommand cmd)
        {
            if (_conn is { } conn)
            {
                try
                {
                    conn.Open();
                    if (conn.State == ConnectionState.Open)
                    {
                        cmd.Connection = conn;
                        cmd.ExecuteNonQuery();
                    }
                    else
                    {
                        throw new Exception("Working on closed connection");
                    }
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        private static void ExecuteQuery(SqlCommand cmd, out BindingList<Player> plys)
        {
            plys = new BindingList<Player>();
            if (_conn is { } conn)
            {
                try
                {
                    conn.Open();
                    if (conn.State == ConnectionState.Open)
                    {
                        cmd.Connection = conn;
                        var dr = cmd.ExecuteReader();
                        if (dr is { } r)
                        {
                            if (r.HasRows)
                            {
                                while (r.Read())
                                {
                                    plys.Add(new Player(
                                        r.GetInt32("Id"),
                                        r.GetString("fname"),
                                        r.GetString("lname"),
                                        r.GetInt32("groupid"),
                                        0.0,
                                        new DateTime(),
                                        r.GetBoolean("gender")
                                    ));
                                }
                            }
                        }
                    }
                    else
                    {
                        throw new Exception("Working on closed connection");
                    }
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        private static void ExecuteDoubleQuery(SqlCommand cmd, out double res)
        {
            res = -1f;
            if (_conn is { } conn)
            {
                try
                {
                    conn.Open();
                    if (conn.State == ConnectionState.Open)
                    {
                        cmd.Connection = conn;
                        res = (double)cmd.ExecuteScalar();
                    }
                    else
                    {
                        throw new Exception("Working on closed connection");
                    }
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        //public static double GetAvgForAll()
        //{
        //    double res;
        //    //Version query = "[Avg]";
        //    //SqlCommand cmd = new SqlCommand();
        //    //cmd.CommandType = CommandType.StoredProcedure;
        //    //cmd.CommandText = query;
        //    //ExecuteDoubleQuery(cmd, out res);
        //    return res;
        //}


        //вставляет данные в табл player
        public static void InsertData(params Player[] plrs)
        {
            var query = "INSERT INTO player (" +
                        "fname, lname, [groupid], rating, birth, gender) " +
                        "values (@fname, @lname, @groupId, @rating, @birth, @gender)" +
                        ";";
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = query;
            foreach (var pls in plrs)
            {
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("fname", pls.FirstName);
                cmd.Parameters.AddWithValue("lname", pls.LastName);
                cmd.Parameters.AddWithValue("gr", pls.GroupId);
                cmd.Parameters.AddWithValue("gender", pls.Gender);
                cmd.Parameters.AddWithValue("raring", pls.Rating);
                cmd.Parameters.AddWithValue("birth", pls.Birth);
                ExecuteNonQuery(cmd);
            }
        }

        //вставляет данные в табл Groups
        public static void InsertDataGroups(params Groups[] grs)
        {
            var query = "INSERT INTO player (" +
                        "group) " +
                        "values (@group)" +
                        ";";
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = query;
            foreach (var gr in grs)
            {
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("group", gr.Group);
                ExecuteNonQuery(cmd);
            }
        }

        // возвращает список игроков
        public static BindingList<Player> GetData()
        {
            BindingList<Player> plys;
            var query = "SELECT * FROM player;";
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = query;
            ExecuteQuery(cmd, out plys);
            return plys;
        }


    }
}

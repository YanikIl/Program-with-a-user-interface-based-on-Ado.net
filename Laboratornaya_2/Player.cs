using Microsoft.Data.SqlClient;
using Microsoft.VisualBasic.Devices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Laboratornaya_2
{
    public class Player : INotifyPropertyChanged
    {
        private int id;
        private string firstName;
        private string lastName;
        private int groupid;
        private double rating;
        private DateTime birth;
        private bool gender;

        [DisplayName("Номер")]
        public int Id
        {
            get => id;
            set => id = value;
        }

        [DisplayName("Имя")]
        public string FirstName { get => firstName; set => firstName = value; }

        [DisplayName("Фамилия")]
        public string LastName
        {
            get => lastName; set
            {
                lastName = value;
                OnPropertyChanged();
            }
        }
        [DisplayName("Группа")]
        public int GroupId { get => groupid; set => groupid = value; }

        [DisplayName("Рейтинг")]
        public double Rating { get => rating; set => rating = value; }

        [DisplayName("Дата рождения")]
        public DateTime Birth { get => birth; set => birth = value; }

        [DisplayName("Пол")]
        public bool Gender { get => gender; set => gender = value; }


        public Player(){}

        public Player(
            int id, string firstName, string lastName, int groupid, double rating, DateTime birth, bool gender)
        {
            this.id = id;
            this.firstName = firstName;
            this.lastName = lastName;
            this.groupid = groupid;
            this.rating = rating;
            this.birth = birth;
            this.gender = gender;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // вставляет данные
        public void Insert()
        {
            SqlConnection connection = new SqlConnection(TablePlayers.connectionString);
            connection.Open();
            SqlCommand insertCommand = new SqlCommand("insert into player (fname, lname, groupid, rating, birth, gender) " +
                "values (@fname, @lname, @groupid, @rating, @birth, @gender)");
            insertCommand.Connection = connection;
            SqlParameter parName = new SqlParameter("@fname", System.Data.SqlDbType.NVarChar);
            parName.Value = FirstName;
            insertCommand.Parameters.Add(parName);
            insertCommand.Parameters.AddWithValue("@lname", LastName);
            insertCommand.Parameters.AddWithValue("@groupid", GroupId);
            insertCommand.Parameters.AddWithValue("@rating", Rating);
            insertCommand.Parameters.AddWithValue("@birth", Birth);
            insertCommand.Parameters.AddWithValue("@gender", Gender);
            insertCommand.ExecuteNonQuery();
        }

        public void Update()
        {
            SqlConnection connection = new SqlConnection(TablePlayers.connectionString);
            connection.Open();
            SqlCommand updateCommand = new SqlCommand("update player set fname=@fname, lname=@lname, groupid=@groupid, rating=@rating, birth=@birth, gender=@gender where id=@id");
            updateCommand.Parameters.AddWithValue("@id", Id);
            updateCommand.Parameters.AddWithValue("fname", FirstName);
            updateCommand.Parameters.AddWithValue("lname", LastName);
            updateCommand.Parameters.AddWithValue("groupid", GroupId);
            updateCommand.Parameters.AddWithValue("rating", Rating);
            updateCommand.Parameters.AddWithValue("birth", Birth);
            updateCommand.Parameters.AddWithValue("gender", Gender);
            updateCommand.Connection = connection;
            updateCommand.ExecuteNonQuery();
            connection.Close();

        }


        public static void Delete(int id)
        {
            SqlConnection connection = new SqlConnection(TablePlayers.connectionString);
            connection.Open();
            SqlCommand deleteCommand = new SqlCommand("delete from player where id=" + id);
            deleteCommand.Connection = connection;
            deleteCommand.ExecuteNonQuery();
            connection.Close();
        }


        public static Player Load(int id)
        {
            Player player = new Player();
            SqlConnection connection = new SqlConnection(TablePlayers.connectionString);
            connection.Open();
            SqlCommand readerCommand = new SqlCommand("select fname, lname, groupid, rating, birth, gender from player where id=" + id);
            readerCommand.Connection = connection;
            SqlDataReader reader = readerCommand.ExecuteReader();
            while (reader.Read())
            {
                player.Id = id;
                player.FirstName = (string)reader["fname"];
                player.LastName = (string)reader["lname"];
                player.GroupId = (int)reader["groupid"];
                player.Rating = (double)reader["rating"];
                player.Birth = (DateTime)reader["birth"];
                player.Gender = (bool)reader["gender"];
            
            }
            reader.Close();
            connection.Close();
            return player;
        }

        public static string Proc(int id)
        {
            SqlConnection conn = new SqlConnection(TablePlayers.connectionString);
            SqlCommand command1 = new SqlCommand("GetPlayerName");
            command1.CommandType = CommandType.StoredProcedure;
            command1.Parameters.AddWithValue("@id", id);

            SqlParameter pName = new SqlParameter();
            pName.ParameterName = "@fname";
            pName.SqlDbType = SqlDbType.NVarChar;
            pName.Size = 1000;
            pName.Direction = ParameterDirection.Output;
            pName.Value = "";
            command1.Parameters.Add(pName);

            command1.Connection = conn;
            command1.Connection.Open();
            command1.ExecuteNonQuery();
            command1.Connection.Close();

            return (string)pName.Value;
        }
    }
}

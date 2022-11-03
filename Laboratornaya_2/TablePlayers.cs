using Microsoft.Data.SqlClient;
using Microsoft.VisualBasic.Devices;
using System;
using System.ComponentModel;
using System.Data;

namespace Laboratornaya_2
{
    public partial class TablePlayers : Form
    {
        public static string connectionString = @"Data Source=HOME-PC;Initial Catalog=Players;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        
        DataSet dsPlayer = new DataSet();
        SqlDataAdapter adapter;

        public TablePlayers()
        {
            InitializeComponent();
            RefreshView();

        }

        void RefreshView()
        {
            string selectString = "select player.id, player.fname, player.lname, Groups.[Group], player.rating, player.birth from player left outer join Groups on player.groupid=Groups.Id ";
            if (textBoxFilter.Text != "")
            {
                selectString = selectString + " where player.fname like '%" + textBoxFilter.Text + "%'";
            }
            adapter = new SqlDataAdapter(selectString, connectionString);
            dsPlayer = new DataSet();
            adapter.Fill(dsPlayer, "player");
            dataGridViewPlayers.DataSource = dsPlayer.Tables["player"];
            dataGridViewPlayers.Columns["id"].Visible = false;
        }


        //средний балл
        private void buttonAvg_Click(object sender, EventArgs e)
        {

        }

        // добавить
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            EditTablePlayers etp = new EditTablePlayers();
            if (etp.ShowDialog() == DialogResult.OK)
            {
                RefreshView();
            }
        }

        // редактировать
        private void buttonEdit_Click(object sender, EventArgs e)
        {
            int ThisRow = dataGridViewPlayers.CurrentCell.RowIndex;
            int id = int.Parse(dataGridViewPlayers["id", ThisRow].EditedFormattedValue.ToString());
            EditTablePlayers etp = new EditTablePlayers(id);
            if (etp.ShowDialog() == DialogResult.OK)
            {
                RefreshView();
            }
        }

        // удалить
        private void buttonDelete_Click(object sender, EventArgs e)
        {
            int ThisRow = dataGridViewPlayers.CurrentCell.RowIndex;
            int id = int.Parse(dataGridViewPlayers["id", ThisRow].EditedFormattedValue.ToString());
            string name = Player.Proc(id);
            DialogResult res = MessageBox.Show(name, "Подтверждение удаления", MessageBoxButtons.YesNo);
            if (res == DialogResult.Yes)
            {
                Player.Delete(id);
                RefreshView();
            }
        }

        // фильтр
        private void buttonFilter_Click(object sender, EventArgs e)
        {
            RefreshView();
        }
    }


    //private BindingList<Player> players = new();
    //private List<Groups> groups = new();
    //// заполнение таблиц
    //private void buttonInsert_Click(object sender, EventArgs e)
    //{
    //    players.Clear();
    //    players.Add(new Player(1, "Катя", "Мельникова", 1, 9.0, new DateTime(2008, 5, 1, 8, 30, 52), true));
    //    players.Add(new Player(2, "Настя", "Иванова", 2, 9.2, new DateTime(2003, 5, 1, 8, 30, 52), true));
    //    players.Add(new Player(3, "Юля", "Студенова", 3, 9.3, new DateTime(1987, 5, 1, 8, 30, 52), true));
    //    players.Add(new Player(4, "Игорь", "Иванов", 4, 7.2, new DateTime(2000, 5, 1, 8, 30, 52), false));
    //    players.Add(new Player(5, "Антон", "Степанов", 5, 5.9, new DateTime(2001, 5, 1, 8, 30, 52), false));
    //    dataGridViewPlayers.DataSource = players;

    //    groups.Clear();
    //    groups.Add(new Groups(1, "гонщик"));
    //    groups.Add(new Groups(2, "врач"));
    //    groups.Add(new Groups(3, "продавец"));
    //    groups.Add(new Groups(4, "учитель"));
    //    groups.Add(new Groups(5, "строитель"));
    //}

    //// создание таблиц
    //private void buttonCreateTable_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        if (DBHelper.Connect(@"Data Source=HOME-PC;Initial Catalog=Players;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
    //        {
    //            DBHelper.CreateMainTable();
    //            DBHelper.CreateTableGroups();
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
    //    }
    //}

    //// вставка данных в БД
    //private void buttonInsertData_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        if (DBHelper.Connect(@"Data Source=HOME-PC;Initial Catalog=Players;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
    //        {
    //            DBHelper.InsertData(players.ToArray());
    //            DBHelper.InsertDataGroups(groups.ToArray());
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
    //    }
    //}

    //// Загрузка данных из БД
    //private void buttonGetData_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        if (DBHelper.Connect(@"Data Source=HOME-PC;Initial Catalog=Players;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
    //        {
    //            players = DBHelper.GetData();
    //            dataGridViewPlayers.DataSource = players;
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        MessageBox.Show(ex.Message, "Ошибка при загрузке из базы", MessageBoxButtons.OK, MessageBoxIcon.Error);
    //    }
    //}
}
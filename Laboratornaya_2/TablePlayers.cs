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


        //������� ����
        private void buttonAvg_Click(object sender, EventArgs e)
        {

        }

        // ��������
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            EditTablePlayers etp = new EditTablePlayers();
            if (etp.ShowDialog() == DialogResult.OK)
            {
                RefreshView();
            }
        }

        // �������������
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

        // �������
        private void buttonDelete_Click(object sender, EventArgs e)
        {
            int ThisRow = dataGridViewPlayers.CurrentCell.RowIndex;
            int id = int.Parse(dataGridViewPlayers["id", ThisRow].EditedFormattedValue.ToString());
            string name = Player.Proc(id);
            DialogResult res = MessageBox.Show(name, "������������� ��������", MessageBoxButtons.YesNo);
            if (res == DialogResult.Yes)
            {
                Player.Delete(id);
                RefreshView();
            }
        }

        // ������
        private void buttonFilter_Click(object sender, EventArgs e)
        {
            RefreshView();
        }
    }


    //private BindingList<Player> players = new();
    //private List<Groups> groups = new();
    //// ���������� ������
    //private void buttonInsert_Click(object sender, EventArgs e)
    //{
    //    players.Clear();
    //    players.Add(new Player(1, "����", "����������", 1, 9.0, new DateTime(2008, 5, 1, 8, 30, 52), true));
    //    players.Add(new Player(2, "�����", "�������", 2, 9.2, new DateTime(2003, 5, 1, 8, 30, 52), true));
    //    players.Add(new Player(3, "���", "���������", 3, 9.3, new DateTime(1987, 5, 1, 8, 30, 52), true));
    //    players.Add(new Player(4, "�����", "������", 4, 7.2, new DateTime(2000, 5, 1, 8, 30, 52), false));
    //    players.Add(new Player(5, "�����", "��������", 5, 5.9, new DateTime(2001, 5, 1, 8, 30, 52), false));
    //    dataGridViewPlayers.DataSource = players;

    //    groups.Clear();
    //    groups.Add(new Groups(1, "������"));
    //    groups.Add(new Groups(2, "����"));
    //    groups.Add(new Groups(3, "��������"));
    //    groups.Add(new Groups(4, "�������"));
    //    groups.Add(new Groups(5, "���������"));
    //}

    //// �������� ������
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
    //        MessageBox.Show(ex.Message, "������", MessageBoxButtons.OK, MessageBoxIcon.Error);
    //    }
    //}

    //// ������� ������ � ��
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
    //        MessageBox.Show(ex.Message, "������!", MessageBoxButtons.OK, MessageBoxIcon.Error);
    //    }
    //}

    //// �������� ������ �� ��
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
    //        MessageBox.Show(ex.Message, "������ ��� �������� �� ����", MessageBoxButtons.OK, MessageBoxIcon.Error);
    //    }
    //}
}
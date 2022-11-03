using Microsoft.Data.SqlClient;
using Microsoft.VisualBasic.Devices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Laboratornaya_2
{
    public partial class EditTablePlayers : Form
    {
        Player player = new Player();
        public EditTablePlayers()
        {
            InitializeComponent();
            SqlDataAdapter adapter = new SqlDataAdapter("select Id, [Group] from Groups", TablePlayers.connectionString);
            DataSet dsGroup = new DataSet();
            adapter.Fill(dsGroup, "Groups");
            comboBoxgroupid.DataSource = dsGroup.Tables["Groups"];
            comboBoxgroupid.ValueMember = "Id";
            comboBoxgroupid.DisplayMember = "[Group]";
        }
        public EditTablePlayers(int id)
        {
            InitializeComponent();
            SqlDataAdapter adapter = new SqlDataAdapter("select Id, [Group] from Groups", TablePlayers.connectionString);
            DataSet dsGroup = new DataSet();
            adapter.Fill(dsGroup, "Groups");
            comboBoxgroupid.DataSource = dsGroup.Tables["Groups"];
            comboBoxgroupid.ValueMember = "Id";
            comboBoxgroupid.DisplayMember = "[Group]";
            player = Player.Load(id);

            textBoxfname.Text = player.FirstName;
            textBoxlastname.Text = player.LastName;
            comboBoxgroupid.SelectedValue = player.GroupId;

            textBoxrating.Text = player.Rating.ToString();
            textBoxbirth.Text = player.Birth.ToString();
            textBoxgender.Text = player.Gender.ToString();
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            //player = new Player();
            player.FirstName = textBoxfname.Text;
            player.LastName = textBoxlastname.Text;
            player.GroupId = (int)comboBoxgroupid.SelectedValue;
            player.Rating = double.Parse(textBoxrating.Text);
            player.Birth = DateTime.Parse(textBoxbirth.Text);
            player.Gender = bool.Parse(textBoxgender.Text);

            if (player.Id == 0)
            {
                player.Insert();
            }
            else
            {
                player.Update();
            }
            this.DialogResult = DialogResult.OK;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}

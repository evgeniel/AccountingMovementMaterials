using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UchetPeremesheniaMaterialov
{
    public partial class Podrazdelenie : Form
    {
        private SQLiteConnection con;
        private SQLiteCommand cmd;
        private DataTable dt = new DataTable();
        private DataSet ds = new DataSet();
        private string sPath = Path.Combine(Application.StartupPath, @"C:\Users\EvgenieL\Source\Repos\AccountingMovementMaterials\UchetPeremesheniaMaterialov\mybd.db");

        public Podrazdelenie()
        {
            InitializeComponent();
        }

        private void Podrazdelenie_Load(object sender, EventArgs e)
        {
            string ConnectionString = @"Data Source=" + sPath + ";New=False;Version=3";
            String selectCommand = "Select * from Podrazdelenie";

            selectTable(ConnectionString, selectCommand);
        }

        public void selectTable(string ConnectionString, String selectCommand)
        {
            SQLiteConnection connect = new SQLiteConnection(ConnectionString);
            connect.Open();
            SQLiteDataAdapter dataAdapter = new SQLiteDataAdapter(selectCommand, connect);
            DataSet ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView1.DataSource = ds;
            dataGridView1.DataMember = ds.Tables[0].ToString();
            connect.Close();
        }


        public object selectValue(string ConnectionString, String selectCommand)
        {
            SQLiteConnection connect = new SQLiteConnection(ConnectionString);
            connect.Open();
            SQLiteCommand command = new SQLiteCommand(selectCommand, connect);
            SQLiteDataReader reader = command.ExecuteReader();
            object value = "";
            while (reader.Read())
            {
                value = reader[0];
            }
            connect.Close();
            return value;
        }

        private void ExecuteQuery(string txtQuery)
        {
            con = new SQLiteConnection("Data Source=" + sPath +
           ";Version=3;New=False;Compress=True;");
            con.Open();
            cmd = con.CreateCommand();
            cmd.CommandText = txtQuery;
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public void refreshForm(string ConnectionString, String selectCommand)
        {
            selectTable(ConnectionString, selectCommand);
            dataGridView1.Update();
            dataGridView1.Refresh();
            toolStripTextBox1.Text = "";
        }

    

        public void changeValue(string ConnectionString, String selectCommand)
        {
            SQLiteConnection connect = new
            SQLiteConnection(ConnectionString);
            connect.Open();
            SQLiteTransaction trans;
            SQLiteCommand cmd = new SQLiteCommand();
            trans = connect.BeginTransaction();
            cmd.Connection = connect;
            cmd.CommandText = selectCommand;
            cmd.ExecuteNonQuery();
            trans.Commit();
            connect.Close();
        }

        private void toolStripButton1_Click_1(object sender, EventArgs e)
        {
            string ConnectionString = @"Data Source=" + sPath + ";New=False;Version=3";
            String selectCommand = "Select MAX(idPodrazd) from Podrazdelenie";
            object maxValue = selectValue(ConnectionString, selectCommand);
            if (Convert.ToString(maxValue) == "")
                maxValue = 0;

            string txtSQLQuery = "insert into Podrazdelenie(idPodrazd, name) values (" + (Convert.ToInt32(maxValue) + 1) + ",'" + toolStripTextBox1.Text + "')";
            ExecuteQuery(txtSQLQuery);            selectCommand = "select * from Podrazdelenie";            refreshForm(ConnectionString, selectCommand);            toolStripTextBox1.Text = "";
        }

        private void toolStripButton2_Click_1(object sender, EventArgs e)
        {
            //выбрана строка CurrentRow
            int CurrentRow = dataGridView1.SelectedCells[0].RowIndex;
            //получить значение idMOL выбранной строки
            string valueId = dataGridView1[0, CurrentRow].Value.ToString();
            String selectCommand = "delete from Podrazdelenie where idPodrazd=" + valueId;
            string ConnectionString = @"Data Source=" + sPath +
           ";New=False;Version=3";
            changeValue(ConnectionString, selectCommand);
            //обновление dataGridView1
            selectCommand = "select * from Podrazdelenie";
            refreshForm(ConnectionString, selectCommand);
            toolStripTextBox1.Text = "";
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;
using System.IO;

namespace UchetPeremesheniaMaterialov
{
    public partial class PlanSchetov : Form
    {
        private SQLiteConnection con;
        private SQLiteCommand cmd;
        private DataTable dt = new DataTable();
        private DataSet ds = new DataSet();
        private string sPath = Path.Combine(Application.StartupPath, @"C:\Users\EvgenieL\Source\Repos\AccountingMovementMaterials\UchetPeremesheniaMaterialov\mybd.db");


        public PlanSchetov()
        {
            InitializeComponent();
        }

        private void PlanSchetov_Load(object sender, EventArgs e)
        {
            string ConnectionString = @"Data Source=" + sPath + ";New=False;Version=3";
            String selectCommand = "Select * from planSchetov";

            selectTable(ConnectionString, selectCommand);
            //con = new SQLiteConnection();
            //con.ConnectionString = @"Data Source=C:\Users\EvgenieL\Source\Repos\AccountingMovementMaterials\UchetPeremesheniaMaterialov\mybd.db;New=False;Version=3";

            //cmd = new SQLiteCommand();
            //cmd.Connection = con;

            //dt = new DataTable();
            //dataGridView1.DataSource = dt; // связываешь DataTable и таблицу на форме

            //con.Open(); // открываешь соединение с БД
            //cmd.CommandText = "Select * from planSchetov";
            //dt.Clear();
            //dt.Load(cmd.ExecuteReader()); // выполняешь SQL-запрос
            //con.Close(); // закрываешь соединение с БД
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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

    }

}
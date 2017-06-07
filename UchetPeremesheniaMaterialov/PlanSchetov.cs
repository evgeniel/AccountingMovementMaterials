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

namespace UchetPeremesheniaMaterialov
{
    public partial class PlanSchetov : Form
    {
        private SQLiteConnection con;
        private SQLiteCommand cmd;
        private DataTable dt;

        public PlanSchetov()
        {
            InitializeComponent();
        }

        private void PlanSchetov_Load(object sender, EventArgs e)
        {
            con = new SQLiteConnection();
            con.ConnectionString = @"Data Source=C:\Users\EvgenieL\Documents\Visual Studio 2013\Projects\UchetPeremesheniaMaterialov\UchetPeremesheniaMaterialov\mybd.db;New=False;Version=3";

            cmd = new SQLiteCommand();
            cmd.Connection = con;

            dt = new DataTable();
            dataGridView1.DataSource = dt; // связываешь DataTable и таблицу на форме

            con.Open(); // открываешь соединение с БД
            cmd.CommandText = "Select * from PlanSchetov";
            dt.Clear();
            dt.Load(cmd.ExecuteReader()); // выполняешь SQL-запрос
            con.Close(); // закрываешь соединение с БД
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

    }

}
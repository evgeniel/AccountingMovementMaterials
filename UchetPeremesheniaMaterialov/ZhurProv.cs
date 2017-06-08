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
    public partial class ZhurProv : Form
    {
        private SQLiteConnection con;
        private SQLiteCommand cmd;
        private DataTable dt = new DataTable();
        private DataSet ds = new DataSet();
        private string sPath = Path.Combine(Application.StartupPath, @"C:\Users\EvgenieL\Source\Repos\AccountingMovementMaterials\UchetPeremesheniaMaterialov\mybd.db");

        public ZhurProv()
        {
            InitializeComponent();
        }

        private void ZhurProv_Load(object sender, EventArgs e)
        {
            string ConnectionString = @"Data Source=" + sPath + ";New=False;Version=3";
            selectTable(ConnectionString);
            // выбрать значения из справочников для отображения в comboBox
            String selectOS = "Select idMat, name from Mater";
            selectCombo(ConnectionString, selectOS, comboBox1, "name", "idMat");
            String selectSubd = "SELECT idPodrazd, name FROM Podrazdelenie";
            selectCombo(ConnectionString, selectSubd, comboBox2, "name", "idPodrazd");
            String selectMOL = "SELECT idMOL, Name FROM MOL";
            selectCombo(ConnectionString, selectMOL, comboBox3, "Name", "idMOL");
            comboBox1.SelectedIndex = -1;
            comboBox2.SelectedIndex = -1;
            comboBox3.SelectedIndex = -1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string ConnectionString = @"Data Source=" + sPath + ";New=False;Version=3";
            // находим максимальное значение кода проводок для записи первичного ключа
            String mValue = "select MAX(idhurnProv) from ZhurnProvodok";
            object maxValue = selectValue(ConnectionString, mValue);
            if (Convert.ToString(maxValue) == "")
                maxValue = 0;
            // Обнулить значения переменных
            string sum = "0";
            string count = "0";
            
            string Value1 = null;
            string Value2 = null;
            string Value3 = null;
            if (comboBox1.Text != "")
            {
                //ОС
                Value1 = comboBox1.SelectedValue.ToString();
            }
            if (comboBox2.Text != "")
            {
                //Подразделение
                Value2 = comboBox2.SelectedValue.ToString();
            }
            if (comboBox3.Text != "")
            {
                //МОЛ
                Value3 = comboBox3.SelectedValue.ToString();
            }

            ConnectionString = @"Data Source=" + sPath + ";New=False;Version=3";
            // находим максимальное значение кода проводок для записи первичного ключа
            mValue = "select MAX(idhurnProv) from ZhurnProvodok";
            maxValue = selectValue(ConnectionString, mValue);
            if (Convert.ToString(maxValue) == "")
                maxValue = 0;
            // Обнулить значения переменных
            sum = "0";
            count = "0";
            
            Value1 = null;
            Value2 = null;
            Value3 = null;
            if (comboBox1.Text != "")
            {
                //ОС
                Value1 = comboBox1.SelectedValue.ToString();
            }
            if (comboBox2.Text != "")
            {
                //Подразделение
                Value2 = comboBox2.SelectedValue.ToString();
            }
            if (comboBox3.Text != "")
            {
                //МОЛ
                Value3 = comboBox3.SelectedValue.ToString();
            }
            //Поле количество
            if (textBox2.Text != "")
            {
                count = textBox2.Text;
            }
            //Поиск по базе данных значений
            String selectCost = "select Cena from Mater where IdMat = " + Value1;
            object Price = selectValue(ConnectionString, selectCost);
            double Summa = Convert.ToDouble(Price) * Convert.ToDouble(count);
            String selectDT = "select idPlSch from planSchetov where schet = 10";
            object DT = selectValue(ConnectionString, selectDT);
            String selectKT = "select idPlSch from planSchetov where schet = 60";
            object KT = selectValue(ConnectionString, selectKT);
            string add = "INSERT INTO ZhurnProvodok (idhurnProv, date, DB, " 
                + "subDB1, subDB2, subDB3, KR, kolVO, sum, vidOper) VALUES(" + (Convert.ToInt32(maxValue) + 1) +","
                + Convert.ToString(maskedTextBox2) + "," + Convert.ToInt32(DT) + "," + Convert.ToInt32(Value1) 
                + "," + Convert.ToInt32(Value2) + "," + Convert.ToInt32(Value3) + "," 
                + Convert.ToInt32(KT) + "," + Convert.ToDouble(count) + "," + Summa + "," 
                + Convert.ToInt32(textBox1.Text) + ")";
            ExecuteQuery(add);
            selectTable(ConnectionString);
        }

        private void ExecuteQuery(string txtQuery)
        {
            con = new SQLiteConnection("Data Source=" + sPath + ";Version=3;New=False;Compress=True;");
            con.Open();
            cmd = con.CreateCommand();
            cmd.CommandText = txtQuery;
            cmd.ExecuteNonQuery();
            con.Close();
        }
        // метод выгрузки таблицы на форму
        public void selectTable(string ConnectionString)
        {
            try
            {
                SQLiteConnection connect = new
               SQLiteConnection(ConnectionString);
                connect.Open();
                SQLiteDataAdapter dataAdapter = new
               SQLiteDataAdapter("Select idhurnProv, date, DB, subDB1, subDB2, subDB3, KR, subKR1, subKR2, subKR3, "
                    + "sum, kolVO, vidOper from ZhurnProvodok", connect);
               
                DataSet ds = new DataSet();
                dataAdapter.Fill(ds);
                dataGridView1.DataSource = ds;
                dataGridView1.DataMember = ds.Tables[0].ToString();
                connect.Close();
                dataGridView1.Columns["idhurnProv"].DisplayIndex = 0;
                dataGridView1.Columns["date"].DisplayIndex = 1;
                dataGridView1.Columns["DB"].DisplayIndex = 2;
                dataGridView1.Columns["subDB1"].DisplayIndex = 3;
                dataGridView1.Columns["subDB2"].DisplayIndex = 4;
                dataGridView1.Columns["subDB3"].DisplayIndex = 5;
                dataGridView1.Columns["KR"].DisplayIndex = 6;
                dataGridView1.Columns["subKR1"].DisplayIndex = 7;
                dataGridView1.Columns["subKR2"].DisplayIndex = 8;
                dataGridView1.Columns["subKR3"].DisplayIndex = 9;
                dataGridView1.Columns["sum"].DisplayIndex = 10;
                dataGridView1.Columns["kolVO"].DisplayIndex = 11;
                dataGridView1.Columns["vidOper"].DisplayIndex = 12;
            }
            catch
            {

            }
        }
        //метод отображения в ComboBox значений из базы данных
        public void selectCombo(string ConnectionString, String selectCommand,
       ComboBox comboBox, string displayMember, string valueMember)
        {
            SQLiteConnection connect = new
           SQLiteConnection(ConnectionString);
            connect.Open();
            SQLiteDataAdapter dataAdapter = new
           SQLiteDataAdapter(selectCommand, connect);
            DataSet ds = new DataSet();
            dataAdapter.Fill(ds);
            comboBox.DataSource = ds.Tables[0];
            comboBox.DisplayMember = displayMember;
            comboBox.ValueMember = valueMember;
            connect.Close();
        }
        // метод возвращает значение записи из таблицы
        public object selectValue(string ConnectionString, String selectCommand)
        {
            SQLiteConnection connect = new
           SQLiteConnection(ConnectionString);
            connect.Open();
            SQLiteCommand command = new SQLiteCommand(selectCommand,
connect);
            SQLiteDataReader reader = command.ExecuteReader();
            object value = "";
            while (reader.Read())
            {
                value = reader[0];
            }
            connect.Close();
            return value;
        }
        //метод очищения полей для ввода нового значения
        private void buttonClear_Click(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = -1;
            comboBox2.SelectedIndex = -1;
            comboBox3.SelectedIndex = -1;
            textBox1.Clear();
            textBox2.Clear();
            maskedTextBox2.Clear();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

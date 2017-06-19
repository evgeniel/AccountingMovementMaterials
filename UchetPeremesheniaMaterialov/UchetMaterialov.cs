using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UchetPeremesheniaMaterialov
{
    public partial class UchetMaterialov : Form
    {
        public UchetMaterialov()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void планСчетовToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChartOfAccounts pS = new ChartOfAccounts();
            pS.Show();
        }

        private void мОЛToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MOL ml = new MOL();
            ml.Show();
        }

        private void подразделениеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Subdivision pd = new Subdivision();
            pd.Show();
        }

        private void мToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Materials mm = new Materials();
            mm.Show();
        }

        private void журналПроводокToolStripMenuItem_Click(object sender, EventArgs e)
        {
            JournalEntries zp = new JournalEntries();
            zp.Show();
        }
    }
}

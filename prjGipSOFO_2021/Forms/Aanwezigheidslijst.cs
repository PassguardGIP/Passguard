using prjGipSOFO_2021.DA;
using prjGipSOFO_2021.Forms;
using prjGipSOFO_2021.Helper;
using prjGipSOFO_2021.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace prjGipSOFO_2021
{
    public partial class Aanwezigheidslijst : Form
    {
        public Aanwezigheidslijst()
        {
            InitializeComponent();

            lsvAanwezig.Items.Clear();
            foreach (ListViewItem item in UserDA.GetAllAanwezig())
            {
                lsvAanwezig.Items.Add(item);
            }

            lblAanwezig.Text = lsvAanwezig.Items.Count.ToString() + " momenteel aanwezig";
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {

        }

        private void btnPDF_Click(object sender, EventArgs e)
        {
            Export.toPDF(lsvAanwezig);
        }

        private void btnMail_Click(object sender, EventArgs e)
        {
            Export.toPDF(lsvAanwezig);
            
        }
    }
}

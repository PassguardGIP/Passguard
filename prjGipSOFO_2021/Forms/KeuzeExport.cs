using prjGipSOFO_2021.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace prjGipSOFO_2021.Forms
{
    public partial class KeuzeExport : Form
    {
        public int Obj { get; set; }

        public KeuzeExport(int obj)
        {
            InitializeComponent();
            Obj = obj;
        }

        private void picPDF_Click(object sender, EventArgs e)
        {
            Export.toPDF(AdminPanel.admin.lsvRegistraties);
            this.Close();
        }

        private void picCSV_Click(object sender, EventArgs e)
        {
            Export.toCSV(AdminPanel.admin.lsvRegistraties);
            this.Close();
        }

        private void picJSON_Click(object sender, EventArgs e)
        {
            Export.toJSON(AdminPanel.admin.lsvRegistraties, Obj);
            this.Close();
        }
    }
}

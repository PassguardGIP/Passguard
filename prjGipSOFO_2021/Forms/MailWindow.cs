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
    public partial class MailWindow : Form
    {
        public string Filename { get; set; }

        public MailWindow(string file)
        {
            InitializeComponent();
            Filename = file;
        }

        private void btnVerzend_Click(object sender, EventArgs e)
        {
            string strAan = txtAan.Text;
            string strOnderwerp = txtOnderwerp.Text;
            string strBody = txtBody.Text;

            Webmail.SendMail(strAan, strOnderwerp, strBody, Filename);

        }
    }
}

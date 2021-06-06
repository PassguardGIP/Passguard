using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using prjGipSOFO_2021.Model;
using prjGipSOFO_2021.DA;
using prjGipSOFO_2021.Helper;
using prjGipSOFO_2021.Forms;
using System.IO;
using System.Threading;
using BarcodeLib;
using System.Data.OleDb;
using System.Runtime.InteropServices;

// http://objectlistview.sourceforge.net/cs/index.html ...
// = blijkbaar makkelijker dan gewone listview

namespace prjGipSOFO_2021
{
    public partial class AdminPanel : Form
    {
        // data wordt hier opgeslagen om listviews te kunnen filteren
        public List<ListViewItem> allRegistraties = new List<ListViewItem>();
        public List<ListViewItem> allPoorten = new List<ListViewItem>();
        public List<ListViewItem> allUsers = new List<ListViewItem>();
        public List<ListViewItem> allMeldingen = new List<ListViewItem>();

        public ListViewColumnSorter lvwColumnSorter;

        BackgroundWorker bw;
        public static AdminPanel admin;

        int intClickedAdd = 0;

        public AdminPanel()
        {
            InitializeComponent();
            picUser.Visible = false;
            btnUsersMeerInfo.Visible = false;

            lvwColumnSorter = new ListViewColumnSorter();
            lsvRegistraties.ListViewItemSorter = lvwColumnSorter;
            lsvPoorten.ListViewItemSorter = lvwColumnSorter;

            // eerste listview refreshen
            ListViewTools.RefreshRegistraties(lsvRegistraties, allRegistraties);

            // opvraging FTP
            Thread thread = new Thread(FTPclient.GetNewFiles);
            thread.Start();

            RefreshAanwezig();

            // time pickers
            dtVan.Format = DateTimePickerFormat.Custom;
            dtTot.Format = DateTimePickerFormat.Custom;

            dtVan.CustomFormat = "HH:mm";
            dtTot.CustomFormat = "HH:mm";

            dtVan.Text = "00:00";
            dtTot.Text = "00:00";

            dtVan.ShowUpDown = true;
            dtTot.ShowUpDown = true;

            // listener op poort 8888
            admin = this;
            bw = new BackgroundWorker();
            bw.DoWork += (obj, ea) => ListenTCP();
            bw.RunWorkerAsync();


        }

        private void ListenTCP()
        {
            TCPclient.Listen("192.168.0.5", 8888);
        }

        private void RefreshAanwezig()
        {
            // zet aantal personen binnen en buiten in de labels
            int[] arrData = UserDA.GetData();
            lblNavAanwezig.Text = string.Format("{0}/{1} aanwezig", arrData[0], arrData[2]);
            lblNavBuiten.Text = string.Format("{0} buiten", arrData[1]);
        }

        public static void EventTriggered()
        {
            // refresh registraties via andere thread
            admin.Invoke(new Action(() =>
            {
                ListViewTools.RefreshRegistraties(admin.lsvRegistraties, admin.allRegistraties);
                admin.RefreshAanwezig();
            }));

        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // refresh listviews bij tabcontrol tab switch
            switch (tabNav.SelectedIndex)
            {
                case 0:
                    Console.WriteLine("registraties");
                    ListViewTools.RefreshRegistraties(lsvRegistraties, allRegistraties);
                    break;

                case 1:
                    Console.WriteLine("meldingen");
                    ListViewTools.RefreshMeldingen(lsvMeldingen, allMeldingen);

                    break;

                case 2:
                    Console.WriteLine("poorten");
                    ListViewTools.RefreshPoorten(lsvPoorten, allPoorten);

                    break;

                case 3:
                    Console.WriteLine("users");

                    ListViewTools.RefreshUsers(lsvUsers, allUsers);
                    break;

                default:
                    Console.WriteLine("default");

                    break;

            }
        }

        // navigatie - tabcontrol tabs veranderen
        private void btnNavRegistraties_Click(object sender, EventArgs e)
        {
            tabNav.SelectedIndex = 0;
        }

        private void btnNavMeldingen_Click(object sender, EventArgs e)
        {
            tabNav.SelectedIndex = 1;
        }

        private void btnNavLocaties_Click(object sender, EventArgs e)
        {
            tabNav.SelectedIndex = 2;
        }

        private void btnNavUsers_Click(object sender, EventArgs e)
        {
            tabNav.SelectedIndex = 3;
        }


        // zoekbalk (nog niet optimaal)
        private void txtSearchRegistratie_TextChanged(object sender, EventArgs e)
        {
            ListViewTools.SearchItem(lsvRegistraties, txtSearchRegistratie, allRegistraties);
            ListViewStyle.Afwisselend(lsvRegistraties);
        }

        private void txtLocatiesSearch_TextChanged(object sender, EventArgs e)
        {
            ListViewTools.SearchItem(lsvPoorten, txtLocatiesSearch, allPoorten);
            ListViewStyle.Afwisselend(lsvPoorten);
        }

        private void txtMeldingenSearch_TextChanged(object sender, EventArgs e)
        {
            ListViewTools.SearchItem(lsvMeldingen, txtMeldingenSearch, allMeldingen);
            ListViewStyle.Afwisselend(lsvMeldingen);
        }

        // de "net terug van andere form" event
        private void AdminPanel_Activated(object sender, EventArgs e)
        {
            if (tabNav.SelectedIndex == 2)
            {
                ListViewTools.RefreshPoorten(lsvPoorten, allPoorten);
            }
        }

        private void btnLocatiesNieuw_Click(object sender, EventArgs e)
        {
            // add action 0 - Add
            PoortActions add = new PoortActions(0);
            add.txtLocatie.Text = "Nieuwe Locatie";
            add.Show();
        }

        private void btnLocatiesEdit_Click(object sender, EventArgs e)
        {
            // er verschijnt form met alle data al ingevuld zodat je ze kunt aanpassen
            if (lsvPoorten.SelectedItems.Count > 0)
            {
                int idPoort = Convert.ToInt32(lsvPoorten.SelectedItems[0].SubItems[0].Text);
                string strLocatie = lsvPoorten.SelectedItems[0].SubItems[1].Text;

                // edit action
                PoortActions edit = new PoortActions(1);
                edit.txtLocatie.Text = strLocatie;
                edit.IDPoort = idPoort;

                edit.lsvTijden.Items.Clear();
                foreach(ListViewItem item in lsvTijden.Items)
                {
                    string van = item.SubItems[0].Text;
                    string tot = item.SubItems[1].Text;

                    edit.lsvTijden.Items.Add(new ListViewItem(new String[] { van, tot }));

                    edit.lstTijden.Add((Convert.ToDateTime(van), Convert.ToDateTime(tot)));
                }


                edit.Show();
            }
        }

        private void btnLocatieDelete_Click(object sender, EventArgs e)
        {
            if (lsvPoorten.SelectedItems.Count > 0)
            {
                int idPoort = Convert.ToInt32(lsvPoorten.SelectedItems[0].SubItems[0].Text);
                string strLocatie = lsvPoorten.SelectedItems[0].SubItems[1].Text;

                //  na deze dialog wordt FormActivate getriggered, dus listview refresht automatisch
                DialogResult dialog = MessageBox.Show(string.Format("Ben je zeker dat je deze poort wilt verwijderen?\n\n{0}", strLocatie.ToUpper()), "Verwijderen", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (dialog == DialogResult.Yes)
                {
                    // bij delete poort, delete tijden en refresh listview
                    PoortDA.DeletePoort(idPoort);
                    PoortDA.WipeTijden(idPoort);
                    ListViewTools.RefreshPoorten(lsvPoorten, allPoorten);
                }
                else
                {

                }
            }
        }

        private void lsvUsers_SelectedIndexChanged(object sender, EventArgs e)
        {
            picUser.Visible = true;
            picUser.BorderStyle = BorderStyle.FixedSingle;
            btnUsersMeerInfo.Visible = true;
            
            // toon foto en barcode van user
            if (lsvUsers.SelectedItems.Count > 0)
            {
                string voornaam = lsvUsers.SelectedItems[0].SubItems[1].Text.Replace(" ", "_");
                string naam = lsvUsers.SelectedItems[0].SubItems[2].Text.Replace(" ", "_");
                string barcode = lsvUsers.SelectedItems[0].SubItems[3].Text;

                // voornaam_naam.JPG
                string filename = "fotosSJZ/" + voornaam.ToLower() + "_" + naam.ToLower() + ".JPG";

                if (File.Exists(filename))
                {
                    picUser.ImageLocation = filename;

                }
                else
                {
                    picUser.ImageLocation = "fotosSJZ/unknown.png";
                }

                // Create an instance of the API
                Barcode b = new Barcode();

                // Define basic settings of the image
                int imageWidth = 290;
                int imageHeight = 120;
                Color foreColor = Color.Black;
                Color backColor = Color.Transparent;
                string data = barcode;

                // Generate the barcode with your settings
                Image img = b.Encode(TYPE.CODE39, data, foreColor, backColor, imageWidth, imageHeight);

                picBarcode.Image = img;
                lblBarcode.Text = barcode;


            }
        }

        private void lsvRegistraties_SelectedIndexChanged(object sender, EventArgs e)
        {
            // toon foto en locatie
            if (lsvRegistraties.SelectedItems.Count > 0)
            {
                string voornaam = lsvRegistraties.SelectedItems[0].SubItems[1].Text;
                string naam = lsvRegistraties.SelectedItems[0].SubItems[2].Text.Replace(" ", "_");
                string poort = lsvRegistraties.SelectedItems[0].SubItems[4].Text;

                lblRegistratiesNaam.Text = voornaam.ToUpper() + " " + naam.ToUpper();

                // voornaam_naam.JPG
                string filename = "fotosSJZ/" + voornaam.ToLower() + "_" + naam.ToLower() + ".JPG";

                picRegistratie.ImageLocation = filename;
                lblPoort.Text = poort;
            }

        }

        private void btnUsersAdd_Click(object sender, EventArgs e)
        {
            // add action 0 - Add
            UserActions add = new UserActions(0);
            add.Show();
        }

        private void btnUsersEdit_Click(object sender, EventArgs e)
        {
            if (lsvUsers.SelectedItems.Count > 0)
            {
                int id = Convert.ToInt32(lsvUsers.SelectedItems[0].SubItems[0].Text);
                User user = UserDA.GetUser(id);

                // edit action 1
                UserActions edit = new UserActions(1);
                edit.txtVoornaam.Text = user.Voornaam;
                edit.txtNaam.Text = user.Naam;
                edit.txtAdres.Text = user.Adres;
                edit.txtPostcode.Text = user.Postcode.ToString();
                edit.cboGemeente.Text = user.Gemeente;
                edit.txtEmail.Text = user.Emailadres;
                edit.txtBarcode.Text = user.Barcode;

                edit.idUser = id;

                // voornaam_naam.JPG
                string filename = "fotosSJZ/" + user.Voornaam.ToLower() + "_" + user.Naam.Replace(" ", "_").ToLower() + ".JPG";

                if (File.Exists(filename))
                {
                    edit.picUser.ImageLocation = filename;

                }
                else
                {
                    edit.picUser.ImageLocation = "fotosSJZ/unknown.png";
                }

                edit.Show();
            }

        }

        private string[] ConvertToStringArray(Array values)
        {
            //create a new string array 
            string[] arrays = new string[values.Length];
            for (int i = 1; i <= values.Length; i++)
            {
                if (values.GetValue(1, i) == null)
                    arrays[i - 1] = "";
                else
                    arrays[i - 1] = (string)values.GetValue(1, i).ToString();
            }
            return arrays;
        }

        private void btnUserImportInformat_Click(object sender, EventArgs e)
        {
            string chosenfile;
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Informat (*.xls) | *.xls";
            dialog.Title = "Selecteer bestand";
            dialog.Multiselect = false;

            // https://foxlearn.com/windows-forms/how-to-open-and-read-an-excel-file-into-a-listview-in-csharp-530.html

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                chosenfile = dialog.FileName;

                var excel = new Microsoft.Office.Interop.Excel.Application();

                lsvExcel.Items.Clear();

                //Make the Application invisible 
                excel.Visible = false;
                var workbook = excel.Workbooks.Open(chosenfile);

                //get the collection of sheets in the workbook 
                var sheets = workbook.Worksheets;

                // get the first and only worksheet from the collection of worksheets 
                Microsoft.Office.Interop.Excel.Worksheet worksheet = (Microsoft.Office.Interop.Excel.Worksheet)sheets.get_Item(1);

                //loop through total rows of the spreadsheet and place each row in the list view
                //Interop Excel UsedRange Rows Count
                //how to get the count of rows in excel sheet using c#
                for (int i = 2; i <= worksheet.UsedRange.Rows.Count; i++)
                {
                    Microsoft.Office.Interop.Excel.Range range = worksheet.get_Range("B" + i.ToString(), "F" + i.ToString());
                    System.Array values = (System.Array)range.Cells.Value;

                    string[] strArray = ConvertToStringArray(values);

                    string voornaam = strArray[1];
                    string naam = strArray[0];
                    string adres = strArray[2];
                    int postcode = Convert.ToInt32(strArray[3]);
                    string gemeente = strArray[4];

                    Console.WriteLine("Test: {0} {1}", voornaam, naam);
                    lsvExcel.Items.Add(new ListViewItem(new String[] {voornaam, naam, adres, postcode.ToString(), gemeente}));
                }

                workbook.Close(false);
                excel.Quit();
                Marshal.ReleaseComObject(worksheet);
                Marshal.ReleaseComObject(workbook);
                Marshal.ReleaseComObject(excel);

                btnInformatAdd.Visible = true;
                lsvExcel.Visible = true;

            }
        }

        private void chkRegistratiesVandaag_CheckedChanged(object sender, EventArgs e)
        {
            // filter op vandaag
            if (chkRegistratiesVandaag.Checked)
            {
                lsvRegistraties.Items.Clear();
                string vandaag = DateTime.Now.ToShortDateString();

                foreach (ListViewItem item in allRegistraties.Where(i => Convert.ToDateTime(i.SubItems[3].Text.Substring(0, 5)).ToShortDateString() == vandaag))
                {
                    lsvRegistraties.Items.Add(item);

                }
            }
            else
            {
                lsvRegistraties.Items.Clear();
                foreach (ListViewItem item in allRegistraties)
                {
                    lsvRegistraties.Items.Add(item);
                }
            }

            ListViewStyle.Afwisselend(lsvRegistraties);

        }

        private void btnBrand_Click(object sender, EventArgs e)
        {
            // toon lijst aanwezigen
            Aanwezigheidslijst aanwezig = new Aanwezigheidslijst();
            aanwezig.Show();
        }

        private void lblNavAanwezig_Click(object sender, EventArgs e)
        {
            Aanwezigheidslijst aanwezig = new Aanwezigheidslijst();
            aanwezig.Show();
        }


        private void btnUsersMeerInfo_Click(object sender, EventArgs e)
        {
            // toon form met meer info
            if (lsvUsers.SelectedItems.Count == 1)
            {
                UserInfo info = new UserInfo();
                int id = Convert.ToInt32(lsvUsers.SelectedItems[0].SubItems[0].Text);
                User user = UserDA.GetUser(id);

                info.lblNaam.Text = user.Voornaam + "\n" + user.Naam;
                info.Text = user.Voornaam + " " + user.Naam;

                info.picUser.Image = picUser.Image;
                info.picBarcode.Image = picBarcode.Image;
                info.lblBarcode.Text = user.Barcode;

                if (string.IsNullOrEmpty(user.Adres))
                {
                    info.lblAdres.Text = "unknown";
                }
                else
                {
                    info.lblAdres.Text = user.Adres;
                }

                info.lblPostcodeGemeente.Text = user.Postcode.ToString() + " " + user.Gemeente.ToUpper();

                if (user.Status == true)
                {
                    info.lblStatus.Text = "Binnen";
                    info.lblStatus.ForeColor = Color.ForestGreen;
                }
                else
                {
                    info.lblStatus.Text = "Buiten";
                    info.lblStatus.ForeColor = Color.FromArgb(239, 35, 60);
                }

                if (user.MagBuiten == true)
                {
                    info.lblMagBuiten.Text = "Ja";
                    info.lblMagBuiten.ForeColor = Color.ForestGreen;
                }
                else
                {
                    info.lblMagBuiten.Text = "Nee";
                    info.lblMagBuiten.ForeColor = Color.FromArgb(239, 35, 60);
                }

                info.lblEmail.Text = user.Emailadres;

                info.Show();

            }
        }

        private void RefreshTijden()
        {
            if (lsvPoorten.SelectedItems.Count == 1)
            {
                lsvTijden.Items.Clear();
                int id = Convert.ToInt32(lsvPoorten.SelectedItems[0].SubItems[0].Text);
                List<Tijden> lstTijden = PoortDA.getTijden(id);

                try
                {
                    string openingstijden = lstTijden[0].Openingstijden.ToString();
                    string sluitingstijden = lstTijden[0].Sluitingstijden.ToString();

                    List<String> arrStart = openingstijden.Split(',').ToList();
                    List<String> arrEind = sluitingstijden.Split(',').ToList();

                    for (int count = 0; count < arrStart.Count; count++)
                    {
                        arrStart[count] = Convert.ToDateTime(arrStart[count]).ToString("HH:mm");
                        arrEind[count] = Convert.ToDateTime(arrEind[count]).ToString("HH:mm");

                        Console.WriteLine("{0}: {1} - {2}", id, arrStart[count], arrEind[count]);

                        ListViewItem item = new ListViewItem(new String[] { arrStart[count], arrEind[count] });
                        lsvTijden.Items.Add(item);
                    }

                    ListViewStyle.Afwisselend(lsvTijden);
                }
                catch (ArgumentOutOfRangeException ex)
                {

                }

            }
        }
        private void lsvPoorten_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshTijden();
        }

        private void btnLocatieBevestig_Click(object sender, EventArgs e)
        {
            if (btnLocatieBevestig.Text == "Add")
            {
                if (lsvPoorten.SelectedItems.Count == 1 && dtVan.Value < dtTot.Value)
                {
                    int id = Convert.ToInt32(lsvPoorten.SelectedItems[0].SubItems[0].Text);
                    DateTime van = dtVan.Value;
                    DateTime tot = dtTot.Value;

                    List<(DateTime, DateTime)> lstTijden = new List<(DateTime, DateTime)>();
                    lstTijden.Add((van, tot));

                    foreach (ListViewItem item in lsvTijden.Items)
                    {
                        lstTijden.Add((Convert.ToDateTime(item.SubItems[0].Text), Convert.ToDateTime(item.SubItems[1].Text)));
                    }

                    lstTijden = PoortDA.Combine(lstTijden).ToList();

                    lsvTijden.Items.Clear();

                    PoortDA.WipeTijden(id);

                    foreach (var tijd in lstTijden)
                    {
                        Console.WriteLine("{0} - {1}", tijd.Item1, tijd.Item2);
                        PoortDA.AddTijd(id, TimeSpan.Parse(tijd.Item1.ToShortTimeString()), TimeSpan.Parse(tijd.Item2.ToShortTimeString()));
                    }

                    RefreshTijden();
                }

            }
        }

        private void btnLocatieAddTijd_Click(object sender, EventArgs e)
        {
            btnLocatieBevestig.Text = "Add";

            if (intClickedAdd == 1)
            {
                intClickedAdd = 0;
                pnlTijd.Visible = false;
            }
            else if (intClickedAdd == 0)
            {
                intClickedAdd = 1;
                pnlTijd.Visible = true;
            }
        }

        private void btnRegistratieExport_Click(object sender, EventArgs e)
        {
            KeuzeExport export = new KeuzeExport(0);
            export.Show();
        }

        private void btnUsersDelete_Click(object sender, EventArgs e)
        {
            if (lsvUsers.SelectedItems.Count > 0)
            {
                int id = Convert.ToInt32(lsvUsers.SelectedItems[0].SubItems[0].Text);
                string voornaam = lsvUsers.SelectedItems[0].SubItems[1].Text;
                string naam = lsvUsers.SelectedItems[0].SubItems[2].Text;
                DialogResult result = MessageBox.Show("Weet je zeker dat je deze gebruiker wilt verwijderen?\n" + voornaam + " " + naam, "Waarschuwing", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if(result == DialogResult.Yes)
                {
                    UserDA.DeleteUser(id);
                    MessageBox.Show("Gebruiker succesvol verwijderd", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ListViewTools.RefreshUsers(lsvUsers, allUsers);
                }
                else
                {

                }
            }
        }

        private void btnMeldingenExport_Click(object sender, EventArgs e)
        {
            KeuzeExport export = new KeuzeExport(1);
            export.Show();
        }

        private void btnLocatieDeleteTijd_Click(object sender, EventArgs e)
        {
            if (lsvTijden.SelectedItems.Count == 1 || lsvPoorten.SelectedItems.Count == 1)
            {
                try
                {
                    int id = Convert.ToInt32(lsvPoorten.SelectedItems[0].SubItems[0].Text);
                    TimeSpan van = TimeSpan.Parse(lsvTijden.SelectedItems[0].SubItems[0].Text);
                    TimeSpan tot = TimeSpan.Parse(lsvTijden.SelectedItems[0].SubItems[1].Text);

                    Console.WriteLine("{0} - {1}", van, tot);
                    PoortDA.DeleteTijd(id, van, tot);
                    RefreshTijden();
                }
                catch (Exception ex)
                {

                }
                
            }
            
        }

        private void btnInformatAdd_Click(object sender, EventArgs e)
        {
            // generate barcode voor elk persoon
            foreach (ListViewItem item in lsvExcel.Items)
            {
                User user = new User();

                string voornaam = item.SubItems[0].Text;
                string naam = item.SubItems[1].Text;
                string barcode = UserDA.GenerateBarcode();
                string adres = item.SubItems[2].Text;
                int postcode = Convert.ToInt32(item.SubItems[3].Text);
                string gemeente = item.SubItems[4].Text;

                user.Voornaam = voornaam;
                user.Naam = naam;
                user.Barcode = barcode;
                user.MagBuiten = true;
                user.Rol_id = 0;
                user.Status = false;
                user.Adres = adres;
                user.Postcode = postcode;
                user.Gemeente = gemeente;
                user.Emailadres = "a";

                Console.WriteLine("{0} {1}: {2}", voornaam, naam, barcode);

                UserDA.AddUser(user);
            }
        }
    }
}
using prjGipSOFO_2021.DA;
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

namespace prjGipSOFO_2021.Forms
{
    public partial class PoortActions : Form
    {
        // action 0 = add
        // action 1 = edit

        public int Action { get; set; }
        public int IDPoort { get; set; }

        public List<(DateTime, DateTime)> lstTijden = new List<(DateTime, DateTime)>();

        public PoortActions(int action)
        {
            InitializeComponent();

            // time pickers
            dtVan.Format = DateTimePickerFormat.Custom;
            dtTot.Format = DateTimePickerFormat.Custom;

            dtVan.CustomFormat = "HH:mm";
            dtTot.CustomFormat = "HH:mm";

            dtVan.ShowUpDown = true;
            dtTot.ShowUpDown = true;

            Action = action;

            switch (Action)
            {
                case 0:
                    btnSubmit.Text = "Add";

                    dtVan.Text = "00:00";
                    dtTot.Text = "00:00";

                    break;

                case 1:
                    btnSubmit.Text = "Update";
               
                    dtVan.Text = "00:00";
                    dtTot.Text = "00:00";

                    break;
            }

        }


        private void btnSubmit_Click(object sender, EventArgs e)
        {
            switch (Action)
            {
                case 0:
                    // add
                    Poort poort = new Poort();
                    poort.Locatie = txtLocatie.Text;

                    // add poort en bewaar idPoort
                    long idAfterInsert = PoortDA.AddPoort(poort);

                    if (lsvTijden.Items.Count > 0)
                    {
                        foreach (ListViewItem item in lsvTijden.Items)
                        {
                            string van = item.SubItems[0].Text;
                            string tot = item.SubItems[1].Text;

                            Console.WriteLine("{0} - {1}", van, tot);
                            Console.WriteLine(idAfterInsert);

                            PoortDA.AddTijd(Convert.ToInt32(idAfterInsert), TimeSpan.Parse(van), TimeSpan.Parse(tot));
                        }

                        this.Close();

                    }
                    else
                    {
                        MessageBox.Show("geen tijden");
                    }
                    break;

                case 1:
                    if (lsvTijden.Items.Count > 0)
                    {
                        PoortDA.WipeTijden(IDPoort);
                        foreach (ListViewItem item in lsvTijden.Items)
                        {
                            string van = item.SubItems[0].Text;
                            string tot = item.SubItems[1].Text;

                            Console.WriteLine("{0} - {1}", van, tot);
                            Console.WriteLine(IDPoort);

                            PoortDA.AddTijd(Convert.ToInt32(IDPoort), TimeSpan.Parse(van), TimeSpan.Parse(tot));
                        }

                        this.Close();

                    }
                    else
                    {
                        MessageBox.Show("geen tijden");
                    }
                    


                    break;
            }

        }

        private void btnAddTijd_Click(object sender, EventArgs e)
        {
            if (dtVan.Value < dtTot.Value)
            {
                if (lsvTijden.Items.Count == 20)
                {
                    MessageBox.Show("funny");
                }

                DateTime van = Convert.ToDateTime(dtVan.Text);
                DateTime tot = Convert.ToDateTime(dtTot.Text);

                (DateTime, DateTime) inputInterval = (van, tot);

                lstTijden.Add(inputInterval);

                lstTijden = PoortDA.Combine(lstTijden).ToList();

                lsvTijden.Items.Clear();
                for (int i = 0; i < lstTijden.Count; i++)
                {
                    string strVan = lstTijden[i].Item1.ToString("HH:mm");
                    string strTot = lstTijden[i].Item2.ToString("HH:mm");

                    ListViewItem item = new ListViewItem(new String[] { strVan, strTot });
                    lsvTijden.Items.Add(item);

                    Console.WriteLine("{0} - {1}", dtVan, dtTot);
                }
            }
            else
            {
                MessageBox.Show("nope");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if(lsvTijden.SelectedItems.Count > 0)
            {
                foreach (ListViewItem item in lsvTijden.SelectedItems)
                {
                    DateTime van = Convert.ToDateTime(item.SubItems[0].Text);
                    DateTime tot = Convert.ToDateTime(item.SubItems[1].Text);

                    (DateTime, DateTime) interval = (van, tot);
                    lstTijden.Remove(interval);

                    lsvTijden.Items.Clear();
                    for (int i = 0; i < lstTijden.Count; i++)
                    {
                        string strVan = lstTijden[i].Item1.ToString("HH:mm");
                        string strTot = lstTijden[i].Item2.ToString("HH:mm");

                        ListViewItem newitem = new ListViewItem(new String[] { strVan, strTot });
                        lsvTijden.Items.Add(newitem);

                        Console.WriteLine("{0} - {1}", strVan, strTot);
                    }

                }
            }
        }

        private void lsvTijden_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(lsvTijden.SelectedItems.Count == 1)
            {
                dtVan.Value = Convert.ToDateTime(lsvTijden.SelectedItems[0].SubItems[0].Text);
                dtTot.Value = Convert.ToDateTime(lsvTijden.SelectedItems[0].SubItems[1].Text);
            }
            
        }
    }
}

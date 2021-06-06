using MySql.Data.MySqlClient;
using prjGipSOFO_2021.DA;
using prjGipSOFO_2021.Helper;
using prjGipSOFO_2021.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace prjGipSOFO_2021.Forms
{
    // idee voor textbox validatie: https://docs.microsoft.com/en-us/dotnet/api/system.windows.forms.errorprovider?redirectedfrom=MSDN&view=net-5.0#Y2680

    public partial class UserActions : Form
    {
        // 0 = add, 1 = edit
        public int Action { get; set; }
        public int idUser { get; set; }

        string chosenfile;

        public UserActions(int action)
        {
            InitializeComponent();
            Action = action;

            picUser.Left = (ClientSize.Width - picUser.Width) / 2;
            txtBarcode.Text = UserDA.GenerateBarcode();
            cboGemeente.Items.Clear();

            switch (Action)
            {
                case 0:
                    btnSubmit.Text = "Add";
                    break;
                case 1:
                    btnSubmit.Text = "Update";
                    break;
            }

            XmlDocument doc = new XmlDocument();
            doc.Load("postcodes/postcodes.xml");

            XmlNodeList xmlList = doc.GetElementsByTagName("plaats");

            foreach (XmlElement element in xmlList)
            {
                string postcode = element["postcode"].InnerXml;
                string gemeente = element["naam"].InnerXml;
                string provincie = element["provincie"].InnerXml;

                if (gemeente == "Brugge")
                {
                    Console.WriteLine("{0}: {1}",gemeente, postcode);
                }


                cboGemeente.Items.Add(gemeente);
            }
        }

        private void btnGenereerBarcode_Click(object sender, EventArgs e)
        {
            txtBarcode.Text = UserDA.GenerateBarcode();
        }

        private void picUser_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";
            dialog.Title = "Selecteer bestand";
            dialog.Multiselect = false;

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                chosenfile = dialog.FileName;
                picUser.ImageLocation = chosenfile;
            }
        }

        private bool isNullOrEmpty(TextBox textbox)
        {
            if (string.IsNullOrEmpty(textbox.Text))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void EditEmail()
        {
            string voornaam = txtVoornaam.Text.ToLower()
            .Replace(" ", "")
            .Replace(" ", "")
            .Replace("'", "")
            .Replace("-", "");

            string naam = txtNaam.Text.ToLower()
            .Replace(" ", "")
            .Replace(" ", "")
            .Replace("'", "")
            .Replace("-", "");

            if (!isNullOrEmpty(txtVoornaam) && !isNullOrEmpty(txtNaam))
            {
                txtEmail.Text = string.Format("{0}.{1}@sintjozefbrugge.be", voornaam, naam);
            }
            else
            {
                txtEmail.Text = "";
            }
        }

        private bool isValidVoornaam()
        {
            /*
                REGEX           ^(?=.{1,40}$)[a-zA-Z]+(?:[-'\s][a-zA-Z]+)*$

                ^               start 
                (?=.{1,40}$)    min 1 max 40 letters
                [a-zA-Z]+       1 of meer letters
                (?:             optionele gedeelde (groep)
                [-'\s]          -, ' of spatie
                [a-zA-Z]+       1 of meer letters
                )*              einde groep
                $               einde
            */

            Match matchVoornaam = Regex.Match(txtVoornaam.Text, @"^(?=.{1,40}$)[a-zA-Zéëïóöüà'-]+(?:[-'\s][a-zA-Zéëïóöüà'-]+)*$");

            if (!matchVoornaam.Success)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private bool isValidNaam()
        {
            // zelfde als voornaam
            Match matchNaam = Regex.Match(txtVoornaam.Text, @"^(?=.{1,40}$)[a-zA-Zéëïóöüà'-]+(?:[-'\s][a-zA-Zéëïóöüà'-]+)*$");

            if (!matchNaam.Success)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private bool isValidAdres()
        {
            // Straat + huisnummer + busnummer (indien van toepassing)
            // zie http://www.bpost.be/sites/editor.bpost/files/landing_page/bpost_masspost2017_NL_1_Adressering_en_frankeermethodes.pdf

            // https://regex101.com/r/ZqaCvY/1 - eigen creatie
            Match matchAdres = Regex.Match(txtAdres.Text, @"^[a-zA-Zéëïóöüà']+((\s{1}|-)[a-zA-Zéëïóöüà']+)*?[a-zA-Zéëïóöüà']?\s{1}[0-9]+\s?(bus\s[0-9]+|-[0-9]+)?");

            if (!matchAdres.Success)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private bool isValidEmail()
        {
            Match matchEmail = Regex.Match(txtEmail.Text, @"[a-zA-Z0-9-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+");

            if (!matchEmail.Success)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void txtVoornaam_Validating(object sender, CancelEventArgs e)
        {
            if (!isValidVoornaam())
            {
                errorProvider.SetError(txtVoornaam, "Vul een geldige voornaam in aub!");
            }
            else
            {
                errorProvider.SetError(txtVoornaam, null);
            }
        }

        private void txtNaam_Validating(object sender, CancelEventArgs e)
        {
            if (!isValidNaam())
            {
                errorProvider.SetError(txtNaam, "Vul een geldige naam in aub!");
            }
            else
            {
                errorProvider.SetError(txtNaam, null);
            }
        }

        private void txtAdres_Validating(object sender, CancelEventArgs e)
        {
            if (!isValidAdres())
            {
                errorProvider.SetError(txtAdres, "Formaat: Straat + huisnummer + busnummer (indien van toepassing)\nbv: Bisschopsstraat 26\n      Steengroefstraat 21-27\n      Zennestraat 32 bus 20");

            }
            else
            {
                errorProvider.SetError(txtAdres, null);
            }

        }

        private void txtEmail_Validating(object sender, CancelEventArgs e)
        {
            if (!isValidEmail())
            {
                errorProvider.SetError(txtEmail, "Ongeldige email");
            }
            else
            {
                errorProvider.SetError(txtEmail, null);
            }
        }

        private void txtVoornaam_TextChanged(object sender, EventArgs e)
        {
            EditEmail();
        }

        private void txtNaam_TextChanged(object sender, EventArgs e)
        {
            EditEmail();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            // stop execution bij 1 of meerdere textbox errors
            if (isValidVoornaam() && isValidNaam() && isValidAdres() && isValidEmail())
            {
                Console.WriteLine("validate");
            }
            else
            {
                Console.WriteLine("not validate");
            }
            
            User user = new User();
            user.Voornaam = txtVoornaam.Text;
            user.Naam = txtNaam.Text;
            user.Adres = txtAdres.Text;
            user.Postcode = Convert.ToInt32(txtPostcode.Text);
            user.Gemeente = cboGemeente.Text;
            user.Barcode = txtBarcode.Text;
            user.Emailadres = txtEmail.Text;
            user.Rol_id = 0;

            switch (Action)
            {
                case 0:
                    UserDA.AddUser(user);
                    MessageBox.Show("User succesvol toegevoegd!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.Close();
                    break;
                case 1:
                    UserDA.UpdateUser(idUser, user);
                    MessageBox.Show("User succesvol bewerkt!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.Close();
                    break;
            }


            // pfp
            // upload

            // verander filename
            if (chosenfile != "")
            {
                string username = user.Voornaam.ToLower().Replace(" ", "_") + user.Naam.ToLower().Replace(" ", "_");
                string dest = "fotosSJZ/" + username + Path.GetExtension(chosenfile);

                if (!File.Exists(dest))
                {
                    File.Copy(chosenfile, dest);
                    picUser.Image = new Bitmap(dest);
                }

                Console.WriteLine(dest);

            }

            
        }

        private void txtPostcode_TextChanged(object sender, EventArgs e)
        {
            //if (cboBestaande.FindString(cboBestaande.Text) == -1)
            //{
            //    cboBestaande.Text = "";
            //}
        }
    }
}

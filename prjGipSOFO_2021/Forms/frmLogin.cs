using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using prjGipSOFO_2021.Helper;
using prjGipSOFO_2021.Model;
using prjGipSOFO_2021.DA;
using System.Text.RegularExpressions;

namespace prjGipSOFO_2021
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (UserDA.Login(txtUsername.Text, txtPassword.Text))
            {
                this.Text = "login succesvol.";

                // check voor rol
   
                AdminPanel adminpanel = new AdminPanel();
                adminpanel.Show();
                this.Hide();
            }
            else
            {
                this.Text = "nope";

                txtUsername.Text = "";
                txtPassword.Text = "";
            }


        }

        private bool isValidEmail(string email)
        {
            Match matchEmail = Regex.Match(email, @"[a-zA-Z0-9-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+");

            if (!matchEmail.Success)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void btnShowPassword_MouseDown(object sender, MouseEventArgs e)
        {
            txtPassword.UseSystemPasswordChar = false;
            btnShowPassword.IconChar = FontAwesome.Sharp.IconChar.EyeSlash;
        }

        private void btnShowPassword_MouseUp(object sender, MouseEventArgs e)
        {
            txtPassword.UseSystemPasswordChar = true;
            btnShowPassword.IconChar = FontAwesome.Sharp.IconChar.Eye;
        }

        private void lblRegister_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 1;
        }

        private void lblLogin_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 0;
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            string username = txtRegisterUsername.Text;
            string password = txtRegisterPassword.Text;
            string repeatpassword = txtRegisterRepeatPassword.Text;

            if (password == repeatpassword)
            {
                password = UserDA.Hash(password);
                UserDA.AddLogin(username, password);
                errorProvider.SetError(txtRegisterRepeatPassword, null);

                this.Close();
                AdminPanel admin = new AdminPanel();
                admin.Show();
                

            }
            else
            {
                errorProvider.SetError(txtRegisterRepeatPassword, "Wachtwoorden komen niet overeen");
                return;

            }


        }
    }
}

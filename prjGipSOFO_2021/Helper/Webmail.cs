using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace prjGipSOFO_2021.Helper
{
    public class Webmail
    {
        public static void SendMail(string ontvanger, string onderwerp, string body, string filename)
        {
            try
            {
                SmtpClient client = new SmtpClient("mail.privateemail.com", 587);
                client.EnableSsl = true;
                client.Credentials = new NetworkCredential("xxxxx", "xxxxxx");
                MailMessage msg = new MailMessage();
                msg.To.Add(ontvanger);
                msg.From = new MailAddress("info@passguard.be");
                msg.Subject = onderwerp;
                msg.Body = body;

                if (filename != "")
                {
                    System.Net.Mail.Attachment attachment = new System.Net.Mail.Attachment(filename);
                    msg.Attachments.Add(attachment);
                }
                
                client.Send(msg);
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

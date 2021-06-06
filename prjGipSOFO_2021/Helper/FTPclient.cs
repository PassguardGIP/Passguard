using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Renci.SshNet;
using Renci.SshNet.Sftp;
using System.IO;

namespace prjGipSOFO_2021.Helper
{
    public class FTPclient
    {
        public static void GetNewFiles()
        {
            string host = @"xxxx";
            string username = "xxxx";
            string password = @"xxxxx";

            string remoteDirectory = "fotosSJZ/";

            using (SftpClient sftp = new SftpClient(host, username, password))
            {
                try
                {
                    sftp.Connect();

                    var files = sftp.ListDirectory(remoteDirectory);

                    foreach (var file in files)
                    {
                        Console.WriteLine(file.Name);

                        if(!File.Exists("fotosSJZ/" + file.Name))
                        {
                            Console.WriteLine("file {0} does not exist", file.Name);
                            if ((file.Name != ".") && (file.Name != ".."))
                            {
                                using (Stream stream = File.OpenWrite("fotosSJZ/" + file.Name))
                                {
                                    sftp.DownloadFile(file.FullName, stream);
                                }

                            }
                        }
                        

                    }

                    sftp.Disconnect();
                }
                catch (Exception e)
                {
                    Console.WriteLine("Exception: " + e.ToString());
                }
            }
        }


    }
}

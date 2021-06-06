using iTextSharp.text;
using iTextSharp.text.pdf;
using Newtonsoft.Json;
using prjGipSOFO_2021.DA;
using prjGipSOFO_2021.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace prjGipSOFO_2021.Helper
{
    public class Export
    {  
        public static void toCSV(ListView listview)
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Filter = "CSV file |*.csv";
            saveFile.Title = "Save CSV";
            saveFile.FileName = "Export.csv";
            saveFile.DefaultExt = ".csv";
            saveFile.ShowDialog();

            Console.WriteLine(saveFile.FileName);

            string file = saveFile.FileName;

            StringBuilder sb = new StringBuilder();

            // headers 
            Console.WriteLine("Columns: {0}", listview.Columns.Count);

            for (int col = 0; col < listview.Columns.Count; col++)
            {
                sb.Append(string.Format("{0};", listview.Columns[col].Text));
            }

            sb.AppendLine();



            foreach (ListViewItem item in listview.Items)
            {
                for (int col = 0; col < listview.Columns.Count; col++)
                {
                    sb.Append(string.Format("{0};", item.SubItems[col].Text));
                }

                sb.AppendLine();
            }

            Console.WriteLine(sb);

            using (StreamWriter swriter = new StreamWriter(file))
            {
                swriter.Write(sb.ToString());
            }

            System.Diagnostics.Process.Start(file);
        }

        public static float[] GetHeaderWidths(Font font, params string[] headers)
        {
            var total = 0;
            var columns = headers.Length;
            var widths = new int[columns];
            for (var i = 0; i < columns; ++i)
            {
                var w = font.GetCalculatedBaseFont(true).GetWidth(headers[i]);
                total += w;
                widths[i] = w;
            }
            var result = new float[columns];
            for (var i = 0; i < columns; ++i)
            {
                result[i] = (float)widths[i] / total * 100;
            }
            return result;
        }

        public static void toJSON(ListView listview, int obj)
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Filter = "JSON file |*.json";
            saveFile.Title = "Save JSON";
            saveFile.FileName = "Export.json";
            saveFile.DefaultExt = ".json";
            saveFile.ShowDialog();

            Console.WriteLine(saveFile.FileName);
            string file = saveFile.FileName;

            List<String> lstHeaders = new List<String>();
            for (int col = 0; col < listview.Columns.Count; col++)
            {
                lstHeaders.Add(listview.Columns[col].Text);
            }

            string output = "";
            switch (obj)
            {
                case 0:
                    // registraties
                    List<Registratie> lstRegistraties = new List<Registratie>();

                    foreach (Registratie reg in RegistratieDA.GetRegistraties())
                    {
                        lstRegistraties.Add(reg);
                    }
     
                    output = JsonConvert.SerializeObject(lstRegistraties, Formatting.Indented);

                    break;

                case 1:
                    // meldingen
                    List<Melding> lstMeldingen = new List<Melding>();

                    foreach (Melding melding in MeldingDA.GetMeldingen())
                    {
                        lstMeldingen.Add(melding);
                    }

                    output = JsonConvert.SerializeObject(lstMeldingen);
                    break;
            }

            StringBuilder sb = new StringBuilder();
            sb.Append(output);

            using (StreamWriter swriter = new StreamWriter(file))
            {
                swriter.Write(sb.ToString());
            }

            System.Diagnostics.Process.Start(file);

        }


        public static void toPDF(ListView listview)
        {
            // headers 
            Console.WriteLine("Columns: {0}", listview.Columns.Count);

            List<String> lstHeaders = new List<String>();
            for (int col = 0; col < listview.Columns.Count; col++)
            {
                lstHeaders.Add(listview.Columns[col].Text);
            }

            // path
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Filter = "PDF file |*.pdf";
            saveFile.Title = "Save PDF";
            saveFile.FileName = "Export.pdf";
            saveFile.DefaultExt = ".pdf";
            saveFile.ShowDialog();

            Console.WriteLine(saveFile.FileName);

            string outputFile = saveFile.FileName;

            // standard .Net FileStream for the file
            using (FileStream fs = new FileStream(outputFile, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                // maak a3 document
                using (Document doc = new Document(PageSize.A3))
                {
                    // bind pdf to filestream
                    using (PdfWriter w = PdfWriter.GetInstance(doc, fs))
                    {
                        doc.Open();

                        // PdfPTable met 2 kolommen
                        PdfPTable t = new PdfPTable(listview.Columns.Count);
                        Font font = new Font(Font.FontFamily.HELVETICA, 14);
                        t.SetWidths(GetHeaderWidths(font, lstHeaders.ToArray()));
                        t.HorizontalAlignment = Element.ALIGN_CENTER;


                        // border
                        // t.DefaultCell.Border = 1;

                        Paragraph pNaam = new Paragraph("Passguard");
                        doc.Add(pNaam);

                        doc.Add(new Chunk("\n"));

                        Paragraph pTitel = new Paragraph("EXPORT");
                        pTitel.Alignment = Element.ALIGN_CENTER;
                        doc.Add(pTitel);

                        doc.Add(new Chunk("\n"));

                        // add headers
                        for (int col = 0; col < listview.Columns.Count; col++)
                        {
                            Paragraph p = new Paragraph(listview.Columns[col].Text);
                            t.AddCell(p);
                        }


                        // cellen maken
                        foreach (ListViewItem item in listview.Items)
                        {
                            for (int count = 0; count < lstHeaders.Count; count++)
                            {
                                t.AddCell(item.SubItems[count].Text);
                            }
                        }
                                        
                        // tabel toevoegen aan pdf
                        doc.Add(t);

                        // pdf sluiten
                        doc.Close();

                        System.Diagnostics.Process.Start(outputFile);
                    }
                }
            }
        }




    }
}

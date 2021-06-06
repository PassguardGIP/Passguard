
namespace prjGipSOFO_2021.Forms
{
    partial class KeuzeExport
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(KeuzeExport));
            this.picJSON = new System.Windows.Forms.PictureBox();
            this.picCSV = new System.Windows.Forms.PictureBox();
            this.picPDF = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picJSON)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picCSV)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picPDF)).BeginInit();
            this.SuspendLayout();
            // 
            // picJSON
            // 
            this.picJSON.Image = global::prjGipSOFO_2021.Properties.Resources.json;
            this.picJSON.Location = new System.Drawing.Point(563, 55);
            this.picJSON.Name = "picJSON";
            this.picJSON.Size = new System.Drawing.Size(172, 129);
            this.picJSON.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picJSON.TabIndex = 3;
            this.picJSON.TabStop = false;
            this.picJSON.Click += new System.EventHandler(this.picJSON_Click);
            // 
            // picCSV
            // 
            this.picCSV.Image = global::prjGipSOFO_2021.Properties.Resources.csv;
            this.picCSV.Location = new System.Drawing.Point(306, 55);
            this.picCSV.Name = "picCSV";
            this.picCSV.Size = new System.Drawing.Size(172, 129);
            this.picCSV.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picCSV.TabIndex = 2;
            this.picCSV.TabStop = false;
            this.picCSV.Click += new System.EventHandler(this.picCSV_Click);
            // 
            // picPDF
            // 
            this.picPDF.Image = global::prjGipSOFO_2021.Properties.Resources.pdf;
            this.picPDF.Location = new System.Drawing.Point(49, 55);
            this.picPDF.Name = "picPDF";
            this.picPDF.Size = new System.Drawing.Size(172, 129);
            this.picPDF.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picPDF.TabIndex = 1;
            this.picPDF.TabStop = false;
            this.picPDF.Click += new System.EventHandler(this.picPDF_Click);
            // 
            // KeuzeExport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(813, 268);
            this.Controls.Add(this.picJSON);
            this.Controls.Add(this.picCSV);
            this.Controls.Add(this.picPDF);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "KeuzeExport";
            this.Text = "KeuzeExport";
            ((System.ComponentModel.ISupportInitialize)(this.picJSON)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picCSV)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picPDF)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.PictureBox picPDF;
        private System.Windows.Forms.PictureBox picCSV;
        private System.Windows.Forms.PictureBox picJSON;
    }
}
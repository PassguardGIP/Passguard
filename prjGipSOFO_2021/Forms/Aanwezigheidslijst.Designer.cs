
namespace prjGipSOFO_2021
{
    partial class Aanwezigheidslijst
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Aanwezigheidslijst));
            this.lblAanwezig = new System.Windows.Forms.Label();
            this.lsvAanwezig = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnMail = new FontAwesome.Sharp.IconButton();
            this.btnPDF = new FontAwesome.Sharp.IconButton();
            this.btnPrint = new FontAwesome.Sharp.IconButton();
            this.SuspendLayout();
            // 
            // lblAanwezig
            // 
            this.lblAanwezig.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblAanwezig.Font = new System.Drawing.Font("Microsoft Sans Serif", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAanwezig.Location = new System.Drawing.Point(0, 0);
            this.lblAanwezig.Name = "lblAanwezig";
            this.lblAanwezig.Padding = new System.Windows.Forms.Padding(0, 30, 0, 0);
            this.lblAanwezig.Size = new System.Drawing.Size(1066, 746);
            this.lblAanwezig.TabIndex = 0;
            this.lblAanwezig.Text = "X momenteel aanwezig";
            this.lblAanwezig.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lsvAanwezig
            // 
            this.lsvAanwezig.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.lsvAanwezig.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lsvAanwezig.FullRowSelect = true;
            this.lsvAanwezig.HideSelection = false;
            this.lsvAanwezig.Location = new System.Drawing.Point(139, 138);
            this.lsvAanwezig.Name = "lsvAanwezig";
            this.lsvAanwezig.Size = new System.Drawing.Size(808, 451);
            this.lsvAanwezig.TabIndex = 1;
            this.lsvAanwezig.UseCompatibleStateImageBehavior = false;
            this.lsvAanwezig.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Voornaam";
            this.columnHeader1.Width = 282;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Naam";
            this.columnHeader2.Width = 271;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Laatste activiteit";
            this.columnHeader3.Width = 247;
            // 
            // btnMail
            // 
            this.btnMail.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(65)))), ((int)(((byte)(90)))));
            this.btnMail.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMail.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold);
            this.btnMail.ForeColor = System.Drawing.Color.White;
            this.btnMail.IconChar = FontAwesome.Sharp.IconChar.None;
            this.btnMail.IconColor = System.Drawing.Color.Black;
            this.btnMail.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnMail.Location = new System.Drawing.Point(829, 623);
            this.btnMail.Name = "btnMail";
            this.btnMail.Size = new System.Drawing.Size(117, 58);
            this.btnMail.TabIndex = 4;
            this.btnMail.Text = "MAIL";
            this.btnMail.UseVisualStyleBackColor = false;
            this.btnMail.Click += new System.EventHandler(this.btnMail_Click);
            // 
            // btnPDF
            // 
            this.btnPDF.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(65)))), ((int)(((byte)(90)))));
            this.btnPDF.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPDF.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold);
            this.btnPDF.ForeColor = System.Drawing.Color.White;
            this.btnPDF.IconChar = FontAwesome.Sharp.IconChar.None;
            this.btnPDF.IconColor = System.Drawing.Color.Black;
            this.btnPDF.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnPDF.Location = new System.Drawing.Point(484, 623);
            this.btnPDF.Name = "btnPDF";
            this.btnPDF.Size = new System.Drawing.Size(117, 58);
            this.btnPDF.TabIndex = 3;
            this.btnPDF.Text = "PDF";
            this.btnPDF.UseVisualStyleBackColor = false;
            this.btnPDF.Click += new System.EventHandler(this.btnPDF_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(65)))), ((int)(((byte)(90)))));
            this.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrint.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold);
            this.btnPrint.ForeColor = System.Drawing.Color.White;
            this.btnPrint.IconChar = FontAwesome.Sharp.IconChar.None;
            this.btnPrint.IconColor = System.Drawing.Color.Black;
            this.btnPrint.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnPrint.Location = new System.Drawing.Point(139, 623);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(117, 58);
            this.btnPrint.TabIndex = 2;
            this.btnPrint.Text = "PRINT";
            this.btnPrint.UseVisualStyleBackColor = false;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // Aanwezigheidslijst
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1066, 746);
            this.Controls.Add(this.btnMail);
            this.Controls.Add(this.btnPDF);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.lsvAanwezig);
            this.Controls.Add(this.lblAanwezig);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Aanwezigheidslijst";
            this.Text = "Aanwezigheidslijst";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ListView lsvAanwezig;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private FontAwesome.Sharp.IconButton btnPrint;
        private FontAwesome.Sharp.IconButton btnPDF;
        private FontAwesome.Sharp.IconButton btnMail;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.Label lblAanwezig;
    }
}
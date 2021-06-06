
namespace prjGipSOFO_2021.Forms
{
    partial class PoortActions
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PoortActions));
            this.dtTot = new System.Windows.Forms.DateTimePicker();
            this.dtVan = new System.Windows.Forms.DateTimePicker();
            this.Locatie = new System.Windows.Forms.Label();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtLocatie = new System.Windows.Forms.TextBox();
            this.lsvTijden = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnDelete = new FontAwesome.Sharp.IconButton();
            this.btnAddTijd = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // dtTot
            // 
            this.dtTot.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtTot.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtTot.Location = new System.Drawing.Point(296, 131);
            this.dtTot.Name = "dtTot";
            this.dtTot.Size = new System.Drawing.Size(128, 29);
            this.dtTot.TabIndex = 16;
            // 
            // dtVan
            // 
            this.dtVan.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtVan.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtVan.Location = new System.Drawing.Point(103, 131);
            this.dtVan.Name = "dtVan";
            this.dtVan.Size = new System.Drawing.Size(128, 29);
            this.dtVan.TabIndex = 15;
            this.dtVan.Value = new System.DateTime(2021, 4, 28, 0, 0, 0, 0);
            // 
            // Locatie
            // 
            this.Locatie.AutoSize = true;
            this.Locatie.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Locatie.Location = new System.Drawing.Point(41, 31);
            this.Locatie.Name = "Locatie";
            this.Locatie.Size = new System.Drawing.Size(52, 16);
            this.Locatie.TabIndex = 14;
            this.Locatie.Text = "Locatie";
            // 
            // btnSubmit
            // 
            this.btnSubmit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(65)))), ((int)(((byte)(90)))));
            this.btnSubmit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSubmit.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold);
            this.btnSubmit.ForeColor = System.Drawing.Color.White;
            this.btnSubmit.Location = new System.Drawing.Point(217, 495);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(207, 43);
            this.btnSubmit.TabIndex = 13;
            this.btnSubmit.Text = "Add";
            this.btnSubmit.UseVisualStyleBackColor = false;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(39, 130);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 30);
            this.label2.TabIndex = 12;
            this.label2.Text = "Van";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(246, 129);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 30);
            this.label1.TabIndex = 11;
            this.label1.Text = "tot";
            // 
            // txtLocatie
            // 
            this.txtLocatie.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLocatie.Location = new System.Drawing.Point(44, 50);
            this.txtLocatie.Name = "txtLocatie";
            this.txtLocatie.Size = new System.Drawing.Size(522, 31);
            this.txtLocatie.TabIndex = 10;
            // 
            // lsvTijden
            // 
            this.lsvTijden.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader3});
            this.lsvTijden.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lsvTijden.FullRowSelect = true;
            this.lsvTijden.HideSelection = false;
            this.lsvTijden.Location = new System.Drawing.Point(44, 210);
            this.lsvTijden.Name = "lsvTijden";
            this.lsvTijden.Size = new System.Drawing.Size(522, 255);
            this.lsvTijden.TabIndex = 17;
            this.lsvTijden.UseCompatibleStateImageBehavior = false;
            this.lsvTijden.View = System.Windows.Forms.View.Details;
            this.lsvTijden.SelectedIndexChanged += new System.EventHandler(this.lsvTijden_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Van";
            this.columnHeader1.Width = 253;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Tot";
            this.columnHeader3.Width = 265;
            // 
            // btnDelete
            // 
            this.btnDelete.FlatAppearance.BorderSize = 0;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.IconChar = FontAwesome.Sharp.IconChar.Trash;
            this.btnDelete.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(65)))), ((int)(((byte)(90)))));
            this.btnDelete.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnDelete.IconSize = 25;
            this.btnDelete.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnDelete.Location = new System.Drawing.Point(572, 210);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(33, 33);
            this.btnDelete.TabIndex = 18;
            this.btnDelete.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnAddTijd
            // 
            this.btnAddTijd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(65)))), ((int)(((byte)(90)))));
            this.btnAddTijd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddTijd.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold);
            this.btnAddTijd.ForeColor = System.Drawing.Color.White;
            this.btnAddTijd.Location = new System.Drawing.Point(456, 131);
            this.btnAddTijd.Name = "btnAddTijd";
            this.btnAddTijd.Size = new System.Drawing.Size(100, 29);
            this.btnAddTijd.TabIndex = 19;
            this.btnAddTijd.Text = "Add tijd";
            this.btnAddTijd.UseVisualStyleBackColor = false;
            this.btnAddTijd.Click += new System.EventHandler(this.btnAddTijd_Click);
            // 
            // PoortActions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(642, 577);
            this.Controls.Add(this.btnAddTijd);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.lsvTijden);
            this.Controls.Add(this.dtTot);
            this.Controls.Add(this.dtVan);
            this.Controls.Add(this.Locatie);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtLocatie);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PoortActions";
            this.Text = "PoortActions";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label Locatie;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox txtLocatie;
        public System.Windows.Forms.DateTimePicker dtTot;
        public System.Windows.Forms.DateTimePicker dtVan;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private FontAwesome.Sharp.IconButton btnDelete;
        private System.Windows.Forms.Button btnAddTijd;
        public System.Windows.Forms.ListView lsvTijden;
    }
}
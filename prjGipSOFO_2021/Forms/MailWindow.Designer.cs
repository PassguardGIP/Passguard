
namespace prjGipSOFO_2021.Forms
{
    partial class MailWindow
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
            this.txtBody = new System.Windows.Forms.TextBox();
            this.txtAan = new System.Windows.Forms.TextBox();
            this.txtOnderwerp = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnVerzend = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblBijlagen = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtBody
            // 
            this.txtBody.Font = new System.Drawing.Font("Segoe UI", 13F);
            this.txtBody.Location = new System.Drawing.Point(33, 171);
            this.txtBody.Multiline = true;
            this.txtBody.Name = "txtBody";
            this.txtBody.Size = new System.Drawing.Size(790, 284);
            this.txtBody.TabIndex = 0;
            // 
            // txtAan
            // 
            this.txtAan.Font = new System.Drawing.Font("Segoe UI", 13F);
            this.txtAan.Location = new System.Drawing.Point(230, 29);
            this.txtAan.Name = "txtAan";
            this.txtAan.Size = new System.Drawing.Size(593, 31);
            this.txtAan.TabIndex = 3;
            // 
            // txtOnderwerp
            // 
            this.txtOnderwerp.Font = new System.Drawing.Font("Segoe UI", 13F);
            this.txtOnderwerp.Location = new System.Drawing.Point(230, 82);
            this.txtOnderwerp.Name = "txtOnderwerp";
            this.txtOnderwerp.Size = new System.Drawing.Size(593, 31);
            this.txtOnderwerp.TabIndex = 4;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(185)))), ((int)(((byte)(188)))));
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.txtAan);
            this.panel1.Controls.Add(this.txtOnderwerp);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(872, 147);
            this.panel1.TabIndex = 5;
            // 
            // btnVerzend
            // 
            this.btnVerzend.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(120)))));
            this.btnVerzend.FlatAppearance.BorderSize = 0;
            this.btnVerzend.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVerzend.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold);
            this.btnVerzend.ForeColor = System.Drawing.Color.White;
            this.btnVerzend.Location = new System.Drawing.Point(576, 481);
            this.btnVerzend.Name = "btnVerzend";
            this.btnVerzend.Size = new System.Drawing.Size(247, 52);
            this.btnVerzend.TabIndex = 6;
            this.btnVerzend.Text = "Verzend";
            this.btnVerzend.UseVisualStyleBackColor = false;
            this.btnVerzend.Click += new System.EventHandler(this.btnVerzend_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(163, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 25);
            this.label2.TabIndex = 8;
            this.label2.Text = "Aan:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(96, 83);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(123, 25);
            this.label5.TabIndex = 11;
            this.label5.Text = "Onderwerp:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(28, 472);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 25);
            this.label1.TabIndex = 12;
            this.label1.Text = "Bijlage (1)";
            // 
            // lblBijlagen
            // 
            this.lblBijlagen.AutoSize = true;
            this.lblBijlagen.Location = new System.Drawing.Point(37, 613);
            this.lblBijlagen.Name = "lblBijlagen";
            this.lblBijlagen.Size = new System.Drawing.Size(0, 13);
            this.lblBijlagen.TabIndex = 13;
            // 
            // MailWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(872, 571);
            this.Controls.Add(this.lblBijlagen);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnVerzend);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.txtBody);
            this.Name = "MailWindow";
            this.Text = "MailWindow";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtBody;
        private System.Windows.Forms.TextBox txtAan;
        private System.Windows.Forms.TextBox txtOnderwerp;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnVerzend;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblBijlagen;
    }
}
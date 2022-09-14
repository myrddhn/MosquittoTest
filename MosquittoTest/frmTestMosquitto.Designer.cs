namespace MosquittoTest
{
    partial class frmTestMosquitto
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
            this.btnPub = new System.Windows.Forms.Button();
            this.txtPayload = new System.Windows.Forms.TextBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnPub
            // 
            this.btnPub.Location = new System.Drawing.Point(142, 148);
            this.btnPub.Name = "btnPub";
            this.btnPub.Size = new System.Drawing.Size(75, 23);
            this.btnPub.TabIndex = 0;
            this.btnPub.Text = "Send";
            this.btnPub.UseVisualStyleBackColor = true;
            this.btnPub.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtPayload
            // 
            this.txtPayload.Location = new System.Drawing.Point(400, 148);
            this.txtPayload.Name = "txtPayload";
            this.txtPayload.Size = new System.Drawing.Size(249, 20);
            this.txtPayload.TabIndex = 1;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(142, 177);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.button2_Click);
            // 
            // frmTestMosquitto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.txtPayload);
            this.Controls.Add(this.btnPub);
            this.Name = "frmTestMosquitto";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.frmTestMosquitto_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnPub;
        private System.Windows.Forms.TextBox txtPayload;
        private System.Windows.Forms.Button btnClose;
    }
}


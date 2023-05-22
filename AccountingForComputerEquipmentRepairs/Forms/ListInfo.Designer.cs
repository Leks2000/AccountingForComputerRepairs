namespace AccountingForComputerEquipmentRepairs.Forms
{
    partial class ListInfo
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
            this.BtnEscape = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // BtnEscape
            // 
            this.BtnEscape.BackColor = System.Drawing.Color.RosyBrown;
            this.BtnEscape.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.BtnEscape.ForeColor = System.Drawing.Color.Black;
            this.BtnEscape.Location = new System.Drawing.Point(384, 191);
            this.BtnEscape.Name = "BtnEscape";
            this.BtnEscape.Size = new System.Drawing.Size(146, 52);
            this.BtnEscape.TabIndex = 18;
            this.BtnEscape.Text = "Назад";
            this.BtnEscape.UseVisualStyleBackColor = false;
            this.BtnEscape.Click += new System.EventHandler(this.BtnEscape_Click);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(12, 12);
            this.listBox1.MultiColumn = true;
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(518, 173);
            this.listBox1.TabIndex = 20;
            // 
            // ListInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::AccountingForComputerEquipmentRepairs.Properties.Resources._1587215785_61_p_foni_dlya_prilozhenii_109;
            this.ClientSize = new System.Drawing.Size(538, 247);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.BtnEscape);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ListInfo";
            this.Text = "ListInfo";
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button BtnEscape;
        private System.Windows.Forms.ListBox listBox1;
    }
}
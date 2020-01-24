namespace search_gui
{
    partial class Form1
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.browseFolderButton = new System.Windows.Forms.Button();
            this.selectedPathLabel = new System.Windows.Forms.Label();
            this.inputBox = new System.Windows.Forms.TextBox();
            this.startButton = new System.Windows.Forms.Button();
            this.listView1 = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // browseFolderButton
            // 
            this.browseFolderButton.Location = new System.Drawing.Point(632, 12);
            this.browseFolderButton.Name = "browseFolderButton";
            this.browseFolderButton.Size = new System.Drawing.Size(75, 23);
            this.browseFolderButton.TabIndex = 0;
            this.browseFolderButton.Text = "Browse Folder";
            this.browseFolderButton.UseVisualStyleBackColor = true;
            this.browseFolderButton.Click += new System.EventHandler(this.browseFolderButton_Click);
            // 
            // selectedPathLabel
            // 
            this.selectedPathLabel.AutoSize = true;
            this.selectedPathLabel.Location = new System.Drawing.Point(629, 47);
            this.selectedPathLabel.Name = "selectedPathLabel";
            this.selectedPathLabel.Size = new System.Drawing.Size(29, 13);
            this.selectedPathLabel.TabIndex = 1;
            this.selectedPathLabel.Text = "Path";
            this.selectedPathLabel.Click += new System.EventHandler(this.selectedPathLabel_Click);
            // 
            // inputBox
            // 
            this.inputBox.Location = new System.Drawing.Point(632, 99);
            this.inputBox.Name = "inputBox";
            this.inputBox.Size = new System.Drawing.Size(163, 20);
            this.inputBox.TabIndex = 2;
            // 
            // startButton
            // 
            this.startButton.Location = new System.Drawing.Point(632, 461);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(163, 23);
            this.startButton.TabIndex = 3;
            this.startButton.Text = "Starte Suche";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // listView1
            // 
            this.listView1.Location = new System.Drawing.Point(12, 12);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(611, 472);
            this.listView1.TabIndex = 4;
            this.listView1.UseCompatibleStateImageBehavior = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(806, 494);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.inputBox);
            this.Controls.Add(this.selectedPathLabel);
            this.Controls.Add(this.browseFolderButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Signal Search";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button browseFolderButton;
        private System.Windows.Forms.Label selectedPathLabel;
        private System.Windows.Forms.TextBox inputBox;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.ListView listView1;
    }
}


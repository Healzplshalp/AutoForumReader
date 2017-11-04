namespace BSIEncryptionKey
{
    partial class KeyGenerator
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(KeyGenerator));
            this.Key = new System.Windows.Forms.TextBox();
            this.KeygenButton = new System.Windows.Forms.Button();
            this.EncryptedIn = new System.Windows.Forms.TextBox();
            this.EncryptButton = new System.Windows.Forms.Button();
            this.EncryptedOut = new System.Windows.Forms.TextBox();
            this.EncryptedKey = new System.Windows.Forms.TextBox();
            this.DecryptOut = new System.Windows.Forms.TextBox();
            this.DecryptButton = new System.Windows.Forms.Button();
            this.EncryptionKeyLabel = new System.Windows.Forms.Label();
            this.EncryptionTextLabel = new System.Windows.Forms.Label();
            this.EncryptionTextOutLabel = new System.Windows.Forms.Label();
            this.DecryptedTextLabel = new System.Windows.Forms.Label();
            this.TestPanel = new System.Windows.Forms.Panel();
            this.PanelLabel = new System.Windows.Forms.Label();
            this.SaveKeyButton = new System.Windows.Forms.Button();
            this.SaveLabel = new System.Windows.Forms.Label();
            this.TestPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // Key
            // 
            this.Key.BackColor = System.Drawing.Color.Moccasin;
            this.Key.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Key.ForeColor = System.Drawing.Color.DarkOrange;
            this.Key.Location = new System.Drawing.Point(31, 100);
            this.Key.Margin = new System.Windows.Forms.Padding(4);
            this.Key.Multiline = true;
            this.Key.Name = "Key";
            this.Key.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Key.Size = new System.Drawing.Size(1331, 46);
            this.Key.TabIndex = 0;
            // 
            // KeygenButton
            // 
            this.KeygenButton.BackColor = System.Drawing.Color.Orange;
            this.KeygenButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.KeygenButton.Location = new System.Drawing.Point(31, 30);
            this.KeygenButton.Margin = new System.Windows.Forms.Padding(4);
            this.KeygenButton.Name = "KeygenButton";
            this.KeygenButton.Size = new System.Drawing.Size(326, 63);
            this.KeygenButton.TabIndex = 1;
            this.KeygenButton.Text = "Generate Key";
            this.KeygenButton.UseVisualStyleBackColor = false;
            this.KeygenButton.Click += new System.EventHandler(this.KeygenButton_Click);
            // 
            // EncryptedIn
            // 
            this.EncryptedIn.BackColor = System.Drawing.SystemColors.Menu;
            this.EncryptedIn.Location = new System.Drawing.Point(39, 214);
            this.EncryptedIn.Margin = new System.Windows.Forms.Padding(4);
            this.EncryptedIn.Multiline = true;
            this.EncryptedIn.Name = "EncryptedIn";
            this.EncryptedIn.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.EncryptedIn.Size = new System.Drawing.Size(1331, 50);
            this.EncryptedIn.TabIndex = 2;
            // 
            // EncryptButton
            // 
            this.EncryptButton.BackColor = System.Drawing.Color.NavajoWhite;
            this.EncryptButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EncryptButton.Location = new System.Drawing.Point(39, 24);
            this.EncryptButton.Margin = new System.Windows.Forms.Padding(4);
            this.EncryptButton.Name = "EncryptButton";
            this.EncryptButton.Size = new System.Drawing.Size(326, 61);
            this.EncryptButton.TabIndex = 3;
            this.EncryptButton.Text = "Encrypt";
            this.EncryptButton.UseVisualStyleBackColor = false;
            this.EncryptButton.Click += new System.EventHandler(this.EncryptButton_Click);
            // 
            // EncryptedOut
            // 
            this.EncryptedOut.BackColor = System.Drawing.SystemColors.Menu;
            this.EncryptedOut.Location = new System.Drawing.Point(39, 303);
            this.EncryptedOut.Margin = new System.Windows.Forms.Padding(4);
            this.EncryptedOut.Multiline = true;
            this.EncryptedOut.Name = "EncryptedOut";
            this.EncryptedOut.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.EncryptedOut.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.EncryptedOut.Size = new System.Drawing.Size(1331, 154);
            this.EncryptedOut.TabIndex = 4;
            // 
            // EncryptedKey
            // 
            this.EncryptedKey.BackColor = System.Drawing.SystemColors.Menu;
            this.EncryptedKey.Location = new System.Drawing.Point(39, 129);
            this.EncryptedKey.Margin = new System.Windows.Forms.Padding(4);
            this.EncryptedKey.Multiline = true;
            this.EncryptedKey.Name = "EncryptedKey";
            this.EncryptedKey.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.EncryptedKey.Size = new System.Drawing.Size(1331, 48);
            this.EncryptedKey.TabIndex = 5;
            // 
            // DecryptOut
            // 
            this.DecryptOut.AcceptsTab = true;
            this.DecryptOut.BackColor = System.Drawing.SystemColors.Menu;
            this.DecryptOut.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.DecryptOut.Location = new System.Drawing.Point(39, 571);
            this.DecryptOut.Margin = new System.Windows.Forms.Padding(4);
            this.DecryptOut.Multiline = true;
            this.DecryptOut.Name = "DecryptOut";
            this.DecryptOut.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.DecryptOut.Size = new System.Drawing.Size(1331, 52);
            this.DecryptOut.TabIndex = 6;
            // 
            // DecryptButton
            // 
            this.DecryptButton.BackColor = System.Drawing.Color.NavajoWhite;
            this.DecryptButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DecryptButton.Location = new System.Drawing.Point(39, 477);
            this.DecryptButton.Margin = new System.Windows.Forms.Padding(4);
            this.DecryptButton.Name = "DecryptButton";
            this.DecryptButton.Size = new System.Drawing.Size(326, 57);
            this.DecryptButton.TabIndex = 7;
            this.DecryptButton.Text = "Decrypt";
            this.DecryptButton.UseVisualStyleBackColor = false;
            this.DecryptButton.Click += new System.EventHandler(this.DecryptButton_Click);
            // 
            // EncryptionKeyLabel
            // 
            this.EncryptionKeyLabel.AutoSize = true;
            this.EncryptionKeyLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EncryptionKeyLabel.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.EncryptionKeyLabel.Location = new System.Drawing.Point(40, 94);
            this.EncryptionKeyLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.EncryptionKeyLabel.Name = "EncryptionKeyLabel";
            this.EncryptionKeyLabel.Size = new System.Drawing.Size(187, 29);
            this.EncryptionKeyLabel.TabIndex = 8;
            this.EncryptionKeyLabel.Text = "Encryption Key";
            // 
            // EncryptionTextLabel
            // 
            this.EncryptionTextLabel.AutoSize = true;
            this.EncryptionTextLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EncryptionTextLabel.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.EncryptionTextLabel.Location = new System.Drawing.Point(40, 183);
            this.EncryptionTextLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.EncryptionTextLabel.Name = "EncryptionTextLabel";
            this.EncryptionTextLabel.Size = new System.Drawing.Size(197, 29);
            this.EncryptionTextLabel.TabIndex = 9;
            this.EncryptionTextLabel.Text = "Text To Encrypt";
            // 
            // EncryptionTextOutLabel
            // 
            this.EncryptionTextOutLabel.AutoSize = true;
            this.EncryptionTextOutLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EncryptionTextOutLabel.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.EncryptionTextOutLabel.Location = new System.Drawing.Point(40, 268);
            this.EncryptionTextOutLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.EncryptionTextOutLabel.Name = "EncryptionTextOutLabel";
            this.EncryptionTextOutLabel.Size = new System.Drawing.Size(188, 29);
            this.EncryptionTextOutLabel.TabIndex = 10;
            this.EncryptionTextOutLabel.Text = "Encrypted Text";
            // 
            // DecryptedTextLabel
            // 
            this.DecryptedTextLabel.AutoSize = true;
            this.DecryptedTextLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DecryptedTextLabel.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.DecryptedTextLabel.Location = new System.Drawing.Point(40, 538);
            this.DecryptedTextLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.DecryptedTextLabel.Name = "DecryptedTextLabel";
            this.DecryptedTextLabel.Size = new System.Drawing.Size(190, 29);
            this.DecryptedTextLabel.TabIndex = 11;
            this.DecryptedTextLabel.Text = "Decrypted Text";
            // 
            // TestPanel
            // 
            this.TestPanel.BackColor = System.Drawing.Color.LightSlateGray;
            this.TestPanel.Controls.Add(this.EncryptButton);
            this.TestPanel.Controls.Add(this.DecryptedTextLabel);
            this.TestPanel.Controls.Add(this.EncryptedIn);
            this.TestPanel.Controls.Add(this.EncryptionTextOutLabel);
            this.TestPanel.Controls.Add(this.EncryptedOut);
            this.TestPanel.Controls.Add(this.EncryptionTextLabel);
            this.TestPanel.Controls.Add(this.EncryptedKey);
            this.TestPanel.Controls.Add(this.EncryptionKeyLabel);
            this.TestPanel.Controls.Add(this.DecryptOut);
            this.TestPanel.Controls.Add(this.DecryptButton);
            this.TestPanel.Location = new System.Drawing.Point(31, 198);
            this.TestPanel.Margin = new System.Windows.Forms.Padding(4);
            this.TestPanel.Name = "TestPanel";
            this.TestPanel.Size = new System.Drawing.Size(1412, 652);
            this.TestPanel.TabIndex = 12;
            // 
            // PanelLabel
            // 
            this.PanelLabel.AutoSize = true;
            this.PanelLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PanelLabel.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.PanelLabel.Location = new System.Drawing.Point(31, 166);
            this.PanelLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.PanelLabel.Name = "PanelLabel";
            this.PanelLabel.Size = new System.Drawing.Size(296, 29);
            this.PanelLabel.TabIndex = 13;
            this.PanelLabel.Text = "Test Key and Encryption";
            // 
            // SaveKeyButton
            // 
            this.SaveKeyButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("SaveKeyButton.BackgroundImage")));
            this.SaveKeyButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.SaveKeyButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.SaveKeyButton.Location = new System.Drawing.Point(1392, 100);
            this.SaveKeyButton.Margin = new System.Windows.Forms.Padding(6);
            this.SaveKeyButton.Name = "SaveKeyButton";
            this.SaveKeyButton.Size = new System.Drawing.Size(51, 50);
            this.SaveKeyButton.TabIndex = 15;
            this.SaveKeyButton.UseVisualStyleBackColor = true;
            this.SaveKeyButton.Click += new System.EventHandler(this.SaveKeyButton_Click);
            // 
            // SaveLabel
            // 
            this.SaveLabel.AutoSize = true;
            this.SaveLabel.ForeColor = System.Drawing.Color.White;
            this.SaveLabel.Location = new System.Drawing.Point(1371, 150);
            this.SaveLabel.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.SaveLabel.Name = "SaveLabel";
            this.SaveLabel.Size = new System.Drawing.Size(98, 25);
            this.SaveLabel.TabIndex = 16;
            this.SaveLabel.Text = "Save Key";
            // 
            // KeyGenerator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SlateGray;
            this.ClientSize = new System.Drawing.Size(1480, 878);
            this.Controls.Add(this.SaveLabel);
            this.Controls.Add(this.SaveKeyButton);
            this.Controls.Add(this.PanelLabel);
            this.Controls.Add(this.TestPanel);
            this.Controls.Add(this.KeygenButton);
            this.Controls.Add(this.Key);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "KeyGenerator";
            this.Text = "BSI Key Generator Tool";
            this.TestPanel.ResumeLayout(false);
            this.TestPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        /// <summary>
        /// Text Box that contains the generated encryption key
        /// </summary>
        private System.Windows.Forms.TextBox Key;

        /// <summary>
        /// Button used to generate encryption key
        /// </summary>
        private System.Windows.Forms.Button KeygenButton;

        /// <summary>
        /// Text box used for plain text unencrypted text input string
        /// </summary>
        private System.Windows.Forms.TextBox EncryptedIn;

        /// <summary>
        /// Button that handles calling of encryption method
        /// </summary>
        private System.Windows.Forms.Button EncryptButton;
        
        /// <summary>
        /// Text field that contains the output of encryption
        /// </summary>
        private System.Windows.Forms.TextBox EncryptedOut;

        /// <summary>
        /// Text box that must contain the generated encryption key, or a copied in key from another location
        /// this field is NOT populated by the Key text box.
        /// </summary>
        private System.Windows.Forms.TextBox EncryptedKey;

        /// <summary>
        /// This field contains the output of the decryption test
        /// </summary>
        private System.Windows.Forms.TextBox DecryptOut;

        /// <summary>
        /// This button handles the decryption of the encrypted text from the encrypted out text box using the encrypted key.
        /// </summary>
        private System.Windows.Forms.Button DecryptButton;

        /// <summary>
        /// Label for field
        /// </summary>
        private System.Windows.Forms.Label EncryptionKeyLabel;

        /// <summary>
        /// Label for field
        /// </summary>
        private System.Windows.Forms.Label EncryptionTextLabel;

        /// <summary>
        /// Label for field
        /// </summary>
        private System.Windows.Forms.Label EncryptionTextOutLabel;

        /// <summary>
        /// Label for field
        /// </summary>
        private System.Windows.Forms.Label DecryptedTextLabel;

        /// <summary>
        /// Panel used to contain the test workspace
        /// </summary>
        private System.Windows.Forms.Panel TestPanel;

        /// <summary>
        /// Label for panel
        /// </summary>
        private System.Windows.Forms.Label PanelLabel;

        /// <summary>
        /// This button handles the saving of the generated key using windows dialogue
        /// </summary>
        private System.Windows.Forms.Button SaveKeyButton;

        /// <summary>
        /// Label for button
        /// </summary>
        private System.Windows.Forms.Label SaveLabel;
    }
}


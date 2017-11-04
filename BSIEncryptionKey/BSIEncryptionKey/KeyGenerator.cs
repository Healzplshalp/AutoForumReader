using System;
using System.IO;
using System.Security.Cryptography;
using System.Windows.Forms;
using BSIEncrypt;
using BSIDecryption;

namespace BSIEncryptionKey
{
    /// <summary>
    /// This class handles the windows form that can be used to generate an enecryped 256bit key 
    /// and test the encryption and decryption algorithms.  
    /// </summary>
    public partial class KeyGenerator : Form
    {
        /// <summary>
        /// Initialize windows form
        /// </summary>
        public KeyGenerator()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Generate key button click.  This method handles the geneate key button being clicked
        /// </summary>
        /// <param name="sender">OnClick</param>
        /// <param name="e">EventHandler</param>
        private void KeygenButton_Click(object sender, EventArgs e)
        {
            try
            {
                var key = new byte[32];
                using (var randomKey = new RNGCryptoServiceProvider())
                {
                    randomKey.GetBytes(key);
                }
                string keyPlainText = Convert.ToBase64String(key);
                Key.Text = keyPlainText;
            }
            catch (Exception Ex)
            {
                string localError = "Error encountered while generating key!: ";
                MessageBox.Show("-- CRYPTO001: " + localError + Ex.Message.ToString(), "Error: CRYPTO001");
            }
        }

        /// <summary>
        /// This metho dhandles the ecnryption button being pressed.  The encryption method takes 
        /// the key from the key textbox and uses it to encrpyt the plain text that is in the encrypted in box
        /// Key must be 32bytes or 256bits.  The key must be manually input into the input key text box
        /// The plain text field must be populated
        /// </summary>
        /// <param name="sender">OnClick</param>
        /// <param name="e">EventHandler</param>
        private void EncryptButton_Click(object sender, EventArgs e)
        {
            try
            {
                WhiteFields();

                string plainText = EncryptedIn.Text;
                string plainTextKey = EncryptedKey.Text;

                if (plainText.Length < 1 || plainTextKey.Length < 1)
                {
                    throw new Exception("-- CRYPTO0099: Text to encrypt or key fields are empty: ");
                }

                Encrypter newEncryption = new Encrypter();

                string encryptedText = newEncryption.EncryptString(plainText, plainTextKey);

                EncryptedOut.Text = encryptedText;
            }
             catch (Exception Ex)
            {
                string localError = "Error encountered while encrypting test text!: ";
                MessageBox.Show("-- CRYPTO002: " + localError + Ex.Message.ToString(), "Error: CRYPTO002");
                EncryptedKey.BackColor = System.Drawing.Color.Red;
                EncryptedIn.BackColor = System.Drawing.Color.Red;
            }
        }

        /// <summary>
        /// The decrypt button handles the decrypting test of a plain text encrypted string using the 
        /// existing key in the key encrypted in text box.
        /// </summary>
        /// <param name="sender">OnClick</param>
        /// <param name="e">EventHandler</param>
        private void DecryptButton_Click(object sender, EventArgs e)
        {
            try
            {
                WhiteFields();

                string plainTextKey = EncryptedKey.Text;
                string plainTextEncryptedText = EncryptedOut.Text;

                if (plainTextKey.Length < 1 || plainTextEncryptedText.Length < 1)
                {
                    throw new Exception("-- CRYPTO0087: Text to decrypt or key fields are empty: ");
                }

                Decrypyter newDecryption = new Decrypyter();

                string decryptedText = newDecryption.DecryptString(plainTextEncryptedText, plainTextKey);

                DecryptOut.Text = decryptedText;
            }
            catch (Exception Ex)
            {
                string localError = "Error encountered while decrypting test text!: ";
                MessageBox.Show("-- CRYPTO003: " + localError + Ex.Message.ToString(), "Error: CRYPTO003");
                EncryptedKey.BackColor = System.Drawing.Color.Red;
                EncryptedOut.BackColor = System.Drawing.Color.Red;
            }
        }

        /// <summary>
        /// The save key button method handles saving the generated key to a text file using the file 
        /// dialogue window.  
        /// </summary>
        /// <param name="sender">OnClick</param>
        /// <param name="e">EventHandler</param>
        private void SaveKeyButton_Click(object sender, EventArgs e)
        {
            try
            {
                WhiteFields();

                if (Key.Text.Length < 1)
                {
                    throw new Exception("-- CRYPTO0057: Key field is empty.  Click on Generate Key to create a new key: ");
                }

                SaveFileDialog save = new SaveFileDialog();
                save.FileName = "BSIKey.txt";
                save.Filter = "Text File | *.txt";
                if (save.ShowDialog() == DialogResult.OK)
                {
                    StreamWriter writer = new StreamWriter(save.OpenFile());
                    writer.WriteLine(Key.Text);
                    writer.Dispose();
                    writer.Close();
                }
            }
            catch (Exception Ex)
            {
                string localError = "Error encountered while saving key!: ";
                MessageBox.Show("-- CRYPTO004: " + localError + Ex.Message.ToString(), "Error: CRYPTO004");
                Key.BackColor = System.Drawing.Color.Red;
            }
        }

        /// <summary>
        /// This method handles clearing the red highlight from the fields that have been focused as fields 
        /// requiring input for the selected action. 
        /// </summary>
        private void WhiteFields()
        {
            EncryptedKey.BackColor = System.Drawing.Color.White;
            EncryptedIn.BackColor = System.Drawing.Color.White;
            EncryptedOut.BackColor = System.Drawing.Color.White;
            Key.BackColor = System.Drawing.Color.White;
        }
    }
}

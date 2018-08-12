using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace File_Encryption
{
    public partial class FrmEncryption : Form
    {
        public FrmEncryption()
        {
            InitializeComponent();

        }
        EncryptionClass Encrypt = new EncryptionClass();
        ThreadClass threadin = new ThreadClass();
        private void btnEncrypt_Click(object sender, EventArgs e)
        {
            if (txtencryptpass.Text.Trim()=="")
            {
                ToolTip tooool = new ToolTip();
                tooool.Show("Choose a password for next time! Don't Forget",txtencryptpass,2500);
                return;
            }
            try
            {
               
                openFileDialog1.ShowDialog(this);
                string fileaddress = openFileDialog1.FileName;
                saveFileDialog1.ShowDialog();
                string pass = txtencryptpass.Text;
                string filesave = saveFileDialog1.FileName;
                Encrypt.EncryptionFile(fileaddress,filesave,pass);


                lblencrypt.Text = "Address:" + fileaddress;
                lblstatus.Text = "Suuessfully Encryption!";
                lblstatus.ForeColor = Color.Yellow;
                

            }
            catch
            {
                
                lblstatus.Text = "Error in Encryption";
                threadin.EndThread();
                lblstatus.ForeColor = Color.Beige;
            }

        }

        private void btnDecryption_Click(object sender, EventArgs e)
        {
            if (txtencryptpass.Text.Trim() == "")
            {
                ToolTip tooool = new ToolTip();
                tooool.Show(" Input Password", txtencryptpass, 2500);
                return;
            }
            try
            {
                openFileDialog1.ShowDialog(this);
                string fileaddress = openFileDialog1.FileName;
                saveFileDialog1.ShowDialog();
                string pass = txtencryptpass.Text;
                string filesave = saveFileDialog1.FileName;
                Encrypt.DecryptFile(fileaddress, filesave, pass);


                lblencrypt.Text = "Address:" + fileaddress;
                lblstatus.Text = "Suuessfully Encryption!";
                lblstatus.ForeColor = Color.Yellow;

            }
            catch
            {

                lblstatus.Text = "Error in Encryption";
                threadin.EndThread();
                lblstatus.ForeColor = Color.Beige;
            }
        }

        private void FrmEncryption_Load(object sender, EventArgs e)
        {

        }
    }
}

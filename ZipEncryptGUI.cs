using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ionic.Zip; // import DotNetZip library

namespace ZipEncryptGUI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        // "Select Files" button click event handler
        private void btnSelectFiles_Click(object sender, EventArgs e)
        {
            // create an OpenFileDialog to allow the user to select one or more files
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // add each selected file to the list box
                foreach (String file in openFileDialog.FileNames)
                {
                    lstFiles.Items.Add(file);
                }
            }
        }
        // "Zip" button click event handler
        private void btnZip_Click(object sender, EventArgs e)
        {
            // make sure the user has selected files to zip
            if (lstFiles.Items.Count == 0)
            {
                MessageBox.Show("Please select files to zip");
                return;
            }
            // create a SaveFileDialog to allow the user to specify the zip file name and location
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Zip files (*.zip)|*.zip";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                // create a new ZipFile object and add each file in the list box to it
                using (ZipFile zip = new ZipFile())
                {
                    foreach (string file in lstFiles.Items)
                    {
                        zip.AddFile(file);
                    }
                    // save the zip file and display a success message
                    zip.Save(saveFileDialog.FileName);
                    MessageBox.Show("Files zipped successfully");
                }
            }
        }
        // "Encrypt" button click event handler
        private void btnEncrypt_Click(object sender, EventArgs e)
        {
            // make sure the user has selected files to encrypt
            if (lstFiles.Items.Count == 0)
            {
                MessageBox.Show("Please select files to encrypt");
                return;
            }
            // create a SaveFileDialog to allow the user to specify the encrypted zip file name and location
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Zip files (*.zip)|*.zip";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                // create a new ZipFile object and add each file in the list box to it
                using (ZipFile zip = new ZipFile())
                {
                    foreach (string file in lstFiles.Items)
                    {
                        zip.AddFile(file);
                    }
                    // save the zip file and display a success message
                    zip.Save(saveFileDialog.FileName);
                    MessageBox.Show("Files zipped successfully");
                    // prompt the user for a password and encrypt the zip file if a password is provided
                    string password = Microsoft.VisualBasic.Interaction.InputBox("Enter password", "Password", "", -1, -1);
                    if (password != "")
                    {
                        zip.Password = password;
                        zip.Save(saveFileDialog.FileName);
                        MessageBox.Show("Files encrypted successfully");
                    }
                }
            }
        }
        // "Unzip" button click event handler
        private void btnUnzip_Click(object sender, EventArgs e)
        {
            // create an OpenFileDialog to allow the user to select a zip file to unzip
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Zip files (*.zip)|*.zip";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // read the ZipFile object 
                using (ZipFile zip = ZipFile.Read(openFileDialog.FileName))
                {
                    // if password not blank get password from user
                    if (zip.Password != "")
                    {
                        string password = Microsoft.VisualBasic.Interaction.InputBox("Enter password", "Password", "", -1, -1);
                        if (password != "")
                        {
                            zip.Password = password;
                            zip.ExtractAll(Path.GetDirectoryName(openFileDialog.FileName), ExtractExistingFileAction.OverwriteSilently);
                            MessageBox.Show("Files unencrypted and unzipped successfully");
                        }
                    }
                    // if password blank, then just unzip
                    else
                    {
                        zip.ExtractAll(Path.GetDirectoryName(openFileDialog.FileName), ExtractExistingFileAction.OverwriteSilently);
                        MessageBox.Show("Files unzipped successfully");
                    }
                }
            }
        }
    }
}

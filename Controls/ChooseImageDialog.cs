using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using PluginCore;
using CustomizeToolbar.Helpers;

namespace CustomizeToolbar.Controls
{
    public partial class ChooseImageDialog : Form
    {
        private Image _selectedImage = null;
        private string _selectedImageName = string.Empty;

        public ChooseImageDialog()
        {
            InitializeComponent();
            InitializeDialog();
            InitializeImageList();
        }

        private void InitializeDialog()
        {
            this.Font = PluginBase.Settings.DefaultFont;
            this.Text = ResourceHelper.GetString("CustomizeToolbar.Label.ChooseImage");
            this.select.Text = ResourceHelper.GetString("CustomizeToolbar.Label.Select");
            this.cancel.Text = ResourceHelper.GetString("CustomizeToolbar.Label.Cancel");
            this.browse.Text = ResourceHelper.GetString("CustomizeToolbar.Label.Browse");
            openFileDialog.Title = ResourceHelper.GetString("CustomizeToolbar.Label.Browse");
        }

        private void InitializeImageList()
        {
            imageList.LargeImageList = ImageHelper.Images;
            // Add each image as an item in the list
            for (int imageIndex = 0; imageIndex < imageList.LargeImageList.Images.Count; imageIndex++)
            {
                string imageName = imageIndex.ToString();
                imageList.Items.Add(imageName, imageIndex);
            }
        }

        public Image SelectedImage
        {
            get
            {
                return imageList.SelectedItems.Count > 0 ? imageList.LargeImageList.Images[imageList.SelectedItems[0].ImageIndex] : _selectedImage;
            }
        }

        public string SelectedImageName
        {
            get
            {
                return imageList.SelectedItems.Count > 0 ? imageList.SelectedItems[0].Text : _selectedImageName;
            }
        }

        private void select_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void imageList_ItemActivate(object sender, EventArgs e)
        {
            select_Click(null, null);
        }

        private void browse_Click(object sender, EventArgs e)
        {
            // Show the open file dialog so that the user can select a custom image
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                imageList.SelectedItems.Clear();
                _selectedImageName = openFileDialog.FileName;
                _selectedImage = Image.FromFile(_selectedImageName);
                select_Click(null, null);
            }
        }
    }
}

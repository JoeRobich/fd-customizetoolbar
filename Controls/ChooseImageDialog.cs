using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PluginCore;
using CustomizeToolbar.Helpers;

namespace CustomizeToolbar.Controls
{
    public partial class ChooseImageDialog : Form
    {
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
        }

        private void InitializeImageList()
        {
            imageList.LargeImageList = ImageHelper.Images;
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
                return imageList.SelectedItems.Count > 0 ? imageList.LargeImageList.Images[imageList.SelectedItems[0].ImageIndex] : null;
            }
        }

        public string SelectedImageName
        {
            get
            {
                return imageList.SelectedItems.Count > 0 ? imageList.SelectedItems[0].Text : string.Empty;
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
    }
}

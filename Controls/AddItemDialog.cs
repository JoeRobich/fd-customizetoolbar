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
    public partial class AddItemDialog : Form
    {
        public AddItemDialog()
        {
            InitializeComponent();
            InitializeDialog();
            HookEvents();
            PopulateMenuList();
        }

        private void InitializeDialog()
        {
            this.Font = PluginBase.Settings.DefaultFont;
            this.Text = ResourceHelper.GetString("CustomizeToolbar.Label.AddItem");
            this.menuList.Font = PluginBase.Settings.DefaultFont;
            this.categories.Font = PluginBase.Settings.DefaultFont;
            this.categories.Text = ResourceHelper.GetString("CustomizeToolbar.Label.Categories");
            this.commands.Font = PluginBase.Settings.DefaultFont;
            this.commands.Text = ResourceHelper.GetString("CustomizeToolbar.Label.Commands");
            this.select.Text = ResourceHelper.GetString("CustomizeToolbar.Label.Select");
            this.cancel.Text = ResourceHelper.GetString("CustomizeToolbar.Label.Cancel");
            menuItemList.Renderer = new DockPanelStripRenderer();
        }

        private void HookEvents()
        {
            menuItemList.SelectedItemChanged += new EventHandler(menuItemList_SelectedItemChanged);
        }

        void menuItemList_SelectedItemChanged(object sender, EventArgs e)
        {
            if (menuItemList.SelectedItem.Image != null)
            {
                this.select.Text = ResourceHelper.GetString("CustomizeToolbar.Label.Select");
            }
            else
            {
                this.select.Text = ResourceHelper.GetString("CustomizeToolbar.Label.ChooseImage");
            }
        }

        public ToolStripMenuItem SelectedItem
        {
            get { return menuItemList.SelectedItem; }
        }

        public string SelectedMenu
        {
            get { return (string)menuList.SelectedItem; }
        }

        private void PopulateMenuList()
        {
            menuList.Items.Clear();

            foreach (ToolStripMenuItem item in PluginBase.MainForm.MenuStrip.Items)
            {
                menuList.Items.Add(item.Text.Replace("&", ""));
            }

            if (menuList.Items.Count > 0)
                menuList.SelectedIndex = 0;
        }

        private void menuList_SelectedIndexChanged(object sender, EventArgs e)
        {
            ToolStripMenuItem menu = null;
            foreach (ToolStripMenuItem item in PluginBase.MainForm.MenuStrip.Items)
            {
                string itemText = item.Text.Replace("&", "");
                if (itemText == (string)menuList.SelectedItem)
                {
                    menu = item;
                    break;
                }   
            }

            if (menu == null)
                return;

            List<ToolStripMenuItem> menuItems = new List<ToolStripMenuItem>();

            foreach (ToolStripItem item in menu.DropDownItems)
            {
                if (!(item is ToolStripMenuItem))
                    continue;

                ToolStripMenuItem menuItem = (ToolStripMenuItem)item;
                if (menuItem.HasDropDownItems)
                    continue;

                menuItems.Add(menuItem);
            }

            menuItemList.SetItems(menuItems);
        }

        private void select_Click(object sender, EventArgs e)
        {
            if (SelectedItem.Image == null)
            {
                ChooseImageDialog chooseImage = new ChooseImageDialog();
                if (chooseImage.ShowDialog() == DialogResult.OK)
                {
                    ((ToolStripMenuItem)SelectedItem.Tag).Image = chooseImage.SelectedImage;
                    ((ToolStripMenuItem)SelectedItem.Tag).Image.Tag = chooseImage.SelectedImageName;
                }
                else
                    return;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}

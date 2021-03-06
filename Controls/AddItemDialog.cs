﻿using System;
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
    public partial class AddItemDialog : Form
    {
        private ToolStripItem _selectedItem = null;

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
            this.addSeparator.Text = ResourceHelper.GetString("CustomizeToolbar.Label.AddSeparator");
            menuItemList.Renderer = new DockPanelStripRenderer();
        }

        private void HookEvents()
        {
            menuItemList.SelectedItemChanged += new EventHandler(menuItemList_SelectedItemChanged);
        }

        void menuItemList_SelectedItemChanged(object sender, EventArgs e)
        {
            // Update the text on the select button to indicate whether the user will need to
            // choose an image for the command
            if (menuItemList.SelectedItem.Image != MenuItem.Checkmark)
            {
                this.select.Text = ResourceHelper.GetString("CustomizeToolbar.Label.Select");
            }
            else
            {
                this.select.Text = ResourceHelper.GetString("CustomizeToolbar.Label.ChooseImage");
            }
        }

        public ToolStripItem SelectedItem
        {
            // Return the selected item. If _selectedItem is populated then it will take precedence.
            get { return _selectedItem ?? menuItemList.SelectedItem; }
        }

        public string SelectedMenu
        {
            get { return (string)menuList.SelectedItem; }
        }

        private void PopulateMenuList()
        {
            menuList.Items.Clear();

            // Add all the menu bar items. Removing short cut indicator.
            foreach (ToolStripItem item in PluginBase.MainForm.MenuStrip.Items)
            {
                if (item is ToolStripMenuItem)
                    menuList.Items.Add(item.Text.Replace("&", ""));
            }

            if (menuList.Items.Count > 0)
                menuList.SelectedIndex = 0;
        }

        private void menuList_SelectedIndexChanged(object sender, EventArgs e)
        {
            ToolStripMenuItem menu = null;

            // Find the selected menu item by comparing names.
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

            // Add each of the items from the selected menu
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
            // If the selected command has no image then show the choose image dialog.
            if (SelectedItem.Image == MenuItem.Checkmark)
            {
                ChooseImageDialog chooseImage = new ChooseImageDialog();
                if (chooseImage.ShowDialog() == DialogResult.OK)
                {
                    ((ToolStripMenuItem)SelectedItem.Tag).Image = chooseImage.SelectedImage;
                    ((ToolStripMenuItem)SelectedItem.Tag).Image.Tag = chooseImage.SelectedImageName;
                }
                else
                {
                    this.DialogResult = DialogResult.Cancel;
                    this.Close();
                    return;
                }
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void addSeparator_Click(object sender, EventArgs e)
        {
            _selectedItem = new ToolStripSeparator();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}

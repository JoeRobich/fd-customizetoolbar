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
    public partial class CustomizeDialog : Form
    {
        private List<ToolItem> _items = null;

        public CustomizeDialog()
        {
            InitializeComponent();
            InitializeDialog();
        }

        private void InitializeDialog()
        {
            this.Font = PluginBase.Settings.DefaultFont;
            this.Text = ResourceHelper.GetString("CustomizeToolbar.Label.Customize");
            this.itemList.Font = PluginBase.Settings.DefaultFont;
            moveItemUp.Image = PluginBase.MainForm.FindImage("8");
            moveItemDown.Image = PluginBase.MainForm.FindImage("22");
            addMenuItem.Image = PluginBase.MainForm.FindImage("12");
            removeMenuItem.Image = PluginBase.MainForm.FindImage("18");
        }

        public List<ToolItem> Items
        {
            get
            {
                return _items;   
            }

            set
            {
                _items = value;
                itemList.BeginUpdate();
                itemList.Items.Clear();

                if (_items != null)
                {
                    // Add all the ToolItems to the list.
                    foreach (ToolItem item in _items)
                    {
                        itemList.Items.Add(item);
                        // Check the item if it is visible
                        if (item.Visible)
                            itemList.SetItemChecked(itemList.Items.Count - 1, true);
                    }
                }

                itemList.EndUpdate();
            }
        }

        private void toolItemVisibility_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            // Toggle item visibility
            ToolItem item = (ToolItem)itemList.Items[e.Index];
            item.Visible = e.NewValue == CheckState.Checked;
        }

        private void moveItemUp_Click(object sender, EventArgs e)
        {
            if (itemList.SelectedItem != null)
            {
                ToolItem item = (ToolItem)itemList.SelectedItem;
                int newIndex = itemList.SelectedIndex - 1;
                if (newIndex >= 0)
                    moveItem(item, newIndex);
            }
        }

        private void moveItemDown_Click(object sender, EventArgs e)
        {
            if (itemList.SelectedItem != null)
            {
                ToolItem item = (ToolItem)itemList.SelectedItem;
                int newIndex = itemList.SelectedIndex + 1;
                if (newIndex < itemList.Items.Count)
                    moveItem(item, newIndex);
            }
        }

        private void moveItem(ToolItem item, int newIndex)
        {
            _items.Remove(item);
            _items.Insert(newIndex, item);

            itemList.Items.Remove(item);
            itemList.Items.Insert(newIndex, item);
            itemList.SetItemChecked(newIndex, item.Visible);

            itemList.SelectedIndex = newIndex;

            ToolbarHelper.ArrangeToolbar(Items);
        }

        private void addMenuItem_Click(object sender, EventArgs e)
        {
            AddItemDialog dialog = new AddItemDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                ToolItem toolItem = null;

                // The selected item was a MenuItem so add it as a ToolStripButton
                if (dialog.SelectedItem is ToolStripMenuItem)
                {
                    ToolStripMenuItem item = dialog.SelectedItem as ToolStripMenuItem;
                    ToolStripMenuItem menuItem = (ToolStripMenuItem)item.Tag;

                    // Create ToolBar Button
                    ToolStripButton toolButton = ToolbarHelper.CreateButton(menuItem);

                    // Create ToolItem
                    toolItem = new ToolItem(toolButton, dialog.SelectedMenu);
                }
                else
                {
                    // Otherwise create a ToolItem for the separator
                    dialog.SelectedItem.Name = "-";
                    toolItem = new ToolItem(dialog.SelectedItem, "-");
                }
                toolItem.Visible = true;

                // Insert it based on the currently selected item in the tool item list
                int index = itemList.SelectedIndex < 0 ? itemList.Items.Count : itemList.SelectedIndex + 1;

                Items.Insert(index, toolItem);
                itemList.Items.Insert(index, toolItem);
                itemList.SetItemChecked(index, true);

                // Select the new item to more easily move it
                itemList.SelectedIndex = index;
                
                ToolbarHelper.ArrangeToolbar(Items);
            }
        }

        private void removeMenuItem_Click(object sender, EventArgs e)
        {
            if (itemList.SelectedItem != null)
            {               
                ToolItem item = (ToolItem)itemList.SelectedItem;
                // Only remove items that have a MenuName
                if (!string.IsNullOrEmpty(item.MenuName))
                {
                    PluginBase.MainForm.ToolStrip.Items.Remove(item.Item);
                    Items.Remove(item);
                    itemList.Items.Remove(item);
                    ToolbarHelper.ArrangeToolbar(Items);
                }
            }
        }

        private void itemList_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Update the buttons based on what actions can be performed on the selected item.
            if (itemList.SelectedItem != null)
            {
                moveItemUp.Enabled = itemList.SelectedIndex > 0;
                moveItemDown.Enabled = itemList.SelectedIndex < (itemList.Items.Count - 1);

                ToolItem item = (ToolItem)itemList.SelectedItem;
                removeMenuItem.Enabled = !string.IsNullOrEmpty(item.MenuName);
            }
            else
            {
                moveItemUp.Enabled = false;
                moveItemDown.Enabled = false;
                removeMenuItem.Enabled = false;
            }
        }
    }
}

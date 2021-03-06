﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using PluginCore;
using PluginCore.Helpers;

namespace CustomizeToolbar.Controls
{
    /// <summary>
    /// A CheckBox List that renders as a DropDown menu.
    /// </summary>
    public class MenuItemList : Panel
    {
        public event EventHandler SelectedItemChanged;

        private MenuToolStrip _menuToolStrip = null;
        private MenuItem _selectedMenuItem = null;

        public MenuItemList()
        {
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            this._menuToolStrip = new MenuToolStrip();
            // 
            // menuItems
            // 
            this._menuToolStrip.CanOverflow = false;
            this._menuToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this._menuToolStrip.ImageScalingSize = ScaleHelper.Scale(new Size(16, 16));
            this._menuToolStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.VerticalStackWithOverflow;
            this._menuToolStrip.Location = new System.Drawing.Point(0, 0);
            this._menuToolStrip.Name = "menuItems";
            this._menuToolStrip.Size = new System.Drawing.Size(228, 102);
            this._menuToolStrip.Stretch = true;
            this._menuToolStrip.TabIndex = 0;
            this._menuToolStrip.Text = "";
            // 
            // MenuItemList
            // 
            this.AutoScroll = true;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this._menuToolStrip);
        }

        public void SetItems(List<ToolStripMenuItem> items)
        {
            _menuToolStrip.Items.Clear();

            // Add the MenuItems to the list
            foreach (ToolStripMenuItem item in items)
            {
                ToolStripMenuItem menuItem = new MenuItem(item.Text, item.Image, null, item.Name);
                menuItem.Tag = item;
                menuItem.AutoSize = true;
                menuItem.Click += new EventHandler(menuItem_Click);

                _menuToolStrip.Items.Add(menuItem);
            }

            if (_menuToolStrip.Items.Count > 0)
                _menuToolStrip.Items[0].PerformClick();
        }

        /// <summary>
        /// Gets the currently selected MenuItem
        /// </summary>
        public ToolStripMenuItem SelectedItem
        {
            get { return _selectedMenuItem; }
        }

        /// <summary>
        /// Gets and Sets the renderer used to draw the menu list
        /// </summary>
        public ToolStripRenderer Renderer
        {
            get { return _menuToolStrip.Renderer; }
            set { _menuToolStrip.Renderer = value; }
        }

        private void menuItem_Click(object sender, EventArgs e)
        {
            // Update the selected menu item
            if (_selectedMenuItem != null)
                _selectedMenuItem.Checked = false;

            _selectedMenuItem = (MenuItem)sender;
            _selectedMenuItem.Checked = true;

            OnSelectedItemChanged();
        }

        protected override void OnScroll(ScrollEventArgs se)
        {
            // Force the menu to refresh
            base.OnScroll(se);
            _menuToolStrip.Refresh();
        }

        protected void OnSelectedItemChanged()
        {
            if (SelectedItemChanged != null)
                SelectedItemChanged(this, new EventArgs());
        }
    }

    class MenuToolStrip : ToolStrip
    {
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            // Force draw the image margin to simulate a dropdown menu
            base.OnPaintBackground(e);
            var imageWidth = ScaleHelper.Scale(27);
            Renderer.DrawImageMargin(new ToolStripRenderEventArgs(e.Graphics, this, new Rectangle(0, 0, imageWidth, this.Height), SystemColors.Control));
        }
    }

    class MenuItem : ToolStripMenuItem
    {
        // Share the checkmark image
        public static Image Checkmark = null;

        public MenuItem(string text, Image image, EventHandler onClick, string name)
            : base(text, image, onClick, name)
        {
            if (Checkmark == null)
            {
                // Get the checkmark image used by FD
                Checkmark = PluginBase.MainForm.FindImage("485");
            }
        }

        public override Image Image
        {
            get
            {
                if (this.Checked && base.Image == null)
                    return Checkmark;

                return base.Image;
            }
            set
            {
                base.Image = value;
            }
        }

        public override bool Selected
        {
            get
            {
                if (this.Checked)
                    return true;

                return base.Selected;
            }
        }

        /// <summary>
        /// Gets the toolstrip renderer
        /// </summary>
        private ToolStripRenderer Renderer
        {
            get { return ((ToolStrip)Owner).Renderer; }
        }

        public override Size GetPreferredSize(Size constrainingSize)
        {
            Size preferredSize = base.GetPreferredSize(constrainingSize);
            preferredSize.Height = ScaleHelper.Scale(23);
            return preferredSize;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            int imageSize = ScaleHelper.Scale(16);
            int imageMargin = ScaleHelper.Scale(27);
            int thisHeight = this.Height;
            int imageXOffset = ((imageMargin - imageSize) / 2);
            int imageYOffset = ((thisHeight - imageSize) / 2);

            //Draw background
            Renderer.DrawMenuItemBackground(new ToolStripItemRenderEventArgs(e.Graphics, this));

            // Draw image
            Rectangle imageRectagle = new Rectangle(new Point(imageXOffset, imageYOffset), ScaleHelper.Scale(new Size(16, 16)));
            Renderer.DrawItemImage(new ToolStripItemImageRenderEventArgs(e.Graphics, this, imageRectagle));

            // Draw Text
            Rectangle textRectangle = new Rectangle(e.ClipRectangle.Location, e.ClipRectangle.Size);
            textRectangle.Offset(imageMargin + 4, 0);
            textRectangle.Width -= (imageMargin + 4);
            Renderer.DrawItemText(new ToolStripItemTextRenderEventArgs(e.Graphics, this, this.Text, textRectangle, this.ForeColor, this.Font, ContentAlignment.MiddleLeft));

            //// Draw Checked
            //if (this.Checked)
            //    if (this.Image == null)
            //        Renderer.DrawItemCheck(new ToolStripItemImageRenderEventArgs(e.Graphics, this, Checkmark, imageRectagle));
            //    else
            //        Renderer.DrawItemCheck(new ToolStripItemImageRenderEventArgs(e.Graphics, this, this.Image, imageRectagle));
        }
    }
}

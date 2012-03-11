using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using PluginCore;

namespace CustomizeToolbar.Controls
{
    public class MenuItemList : Panel
    {
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

        public ToolStripMenuItem SelectedItem
        {
            get { return _selectedMenuItem; }
        }

        public ToolStripRenderer Renderer
        {
            get { return _menuToolStrip.Renderer; }
            set { _menuToolStrip.Renderer = value; }
        }

        private void menuItem_Click(object sender, EventArgs e)
        {
            if (_selectedMenuItem != null)
                _selectedMenuItem.Checked = false;

            _selectedMenuItem = (MenuItem)sender;
            _selectedMenuItem.Checked = true;
        }

        protected override void OnScroll(ScrollEventArgs se)
        {
            base.OnScroll(se);
            _menuToolStrip.Refresh();
        }
    }

    class MenuToolStrip : ToolStrip
    {
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);
            Renderer.DrawImageMargin(new ToolStripRenderEventArgs(e.Graphics, this, new Rectangle(0, 0, 27, this.Height), SystemColors.Control));
        }
    }

    class MenuItem : ToolStripMenuItem
    {
        private static Image Checkmark = null;

        public MenuItem(string text, Image image, EventHandler onClick, string name)
            : base(text, image, onClick, name)
        {
            if (Checkmark == null)
            {
                Checkmark = PluginBase.MainForm.FindImage("485");
            }
        }

        private ToolStripRenderer Renderer
        {
            get { return ((ToolStrip)Owner).Renderer; }
        }

        public override Size GetPreferredSize(Size constrainingSize)
        {
            Size preferredSize = base.GetPreferredSize(constrainingSize);
            preferredSize.Height = 23;
            return preferredSize;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            int imageSize = 16;
            int imageMargin = 27;
            int thisHeight = this.Height;
            int imageXOffset = ((imageMargin - imageSize) / 2);
            int imageYOffset = ((thisHeight - imageSize) / 2);

            //Draw background
            Renderer.DrawMenuItemBackground(new ToolStripItemRenderEventArgs(e.Graphics, this));

            // Draw image
            Point topLeft = new Point(imageXOffset, imageYOffset);
            Rectangle imageRectagle = new Rectangle(topLeft, new Size(16, 16));
            Renderer.DrawItemImage(new ToolStripItemImageRenderEventArgs(e.Graphics, this, imageRectagle));

            // Draw Text
            topLeft.Offset(imageMargin - imageXOffset + 4, -imageYOffset);
            Rectangle textRectangle = new Rectangle(topLeft, new Size(e.ClipRectangle.Width - imageMargin, thisHeight));
            Renderer.DrawItemText(new ToolStripItemTextRenderEventArgs(e.Graphics, this, this.Text, textRectangle, this.ForeColor, this.Font, ContentAlignment.MiddleLeft));

            // Draw Checked
            if (this.Checked)
                if (this.Image == null)
                    Renderer.DrawItemCheck(new ToolStripItemImageRenderEventArgs(e.Graphics, this, Checkmark, imageRectagle));
                else
                    Renderer.DrawItemCheck(new ToolStripItemImageRenderEventArgs(e.Graphics, this, this.Image, imageRectagle));
        }
    }
}

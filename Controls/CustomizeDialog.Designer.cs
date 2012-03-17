namespace CustomizeToolbar.Controls
{
    partial class CustomizeDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.itemList = new System.Windows.Forms.CheckedListBox();
            this.moveItemUp = new System.Windows.Forms.Button();
            this.moveItemDown = new System.Windows.Forms.Button();
            this.addMenuItem = new System.Windows.Forms.Button();
            this.removeMenuItem = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // itemList
            // 
            this.itemList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.itemList.FormattingEnabled = true;
            this.itemList.Location = new System.Drawing.Point(12, 12);
            this.itemList.Name = "itemList";
            this.itemList.ScrollAlwaysVisible = true;
            this.itemList.Size = new System.Drawing.Size(332, 229);
            this.itemList.TabIndex = 0;
            this.itemList.SelectedIndexChanged += new System.EventHandler(this.itemList_SelectedIndexChanged);
            this.itemList.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.toolItemVisibility_ItemCheck);
            // 
            // moveItemUp
            // 
            this.moveItemUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.moveItemUp.Enabled = false;
            this.moveItemUp.Location = new System.Drawing.Point(350, 12);
            this.moveItemUp.Name = "moveItemUp";
            this.moveItemUp.Size = new System.Drawing.Size(24, 24);
            this.moveItemUp.TabIndex = 1;
            this.moveItemUp.UseVisualStyleBackColor = true;
            this.moveItemUp.Click += new System.EventHandler(this.moveItemUp_Click);
            // 
            // moveItemDown
            // 
            this.moveItemDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.moveItemDown.Enabled = false;
            this.moveItemDown.Location = new System.Drawing.Point(350, 42);
            this.moveItemDown.Name = "moveItemDown";
            this.moveItemDown.Size = new System.Drawing.Size(24, 24);
            this.moveItemDown.TabIndex = 2;
            this.moveItemDown.UseVisualStyleBackColor = true;
            this.moveItemDown.Click += new System.EventHandler(this.moveItemDown_Click);
            // 
            // addMenuItem
            // 
            this.addMenuItem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.addMenuItem.Location = new System.Drawing.Point(350, 72);
            this.addMenuItem.Name = "addMenuItem";
            this.addMenuItem.Size = new System.Drawing.Size(24, 24);
            this.addMenuItem.TabIndex = 3;
            this.addMenuItem.UseVisualStyleBackColor = true;
            this.addMenuItem.Click += new System.EventHandler(this.addMenuItem_Click);
            // 
            // removeMenuItem
            // 
            this.removeMenuItem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.removeMenuItem.Enabled = false;
            this.removeMenuItem.Location = new System.Drawing.Point(350, 102);
            this.removeMenuItem.Name = "removeMenuItem";
            this.removeMenuItem.Size = new System.Drawing.Size(24, 24);
            this.removeMenuItem.TabIndex = 4;
            this.removeMenuItem.UseVisualStyleBackColor = true;
            this.removeMenuItem.Click += new System.EventHandler(this.removeMenuItem_Click);
            // 
            // CustomizeDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(383, 253);
            this.Controls.Add(this.removeMenuItem);
            this.Controls.Add(this.addMenuItem);
            this.Controls.Add(this.moveItemDown);
            this.Controls.Add(this.moveItemUp);
            this.Controls.Add(this.itemList);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(399, 291);
            this.Name = "CustomizeDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Customize";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckedListBox itemList;
        private System.Windows.Forms.Button moveItemUp;
        private System.Windows.Forms.Button moveItemDown;
        private System.Windows.Forms.Button addMenuItem;
        private System.Windows.Forms.Button removeMenuItem;
    }
}
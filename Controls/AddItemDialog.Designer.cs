namespace CustomizeToolbar.Controls
{
    partial class AddItemDialog
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
            System.Windows.Forms.ToolStripProfessionalRenderer toolStripProfessionalRenderer1 = new System.Windows.Forms.ToolStripProfessionalRenderer();
            this.menuList = new System.Windows.Forms.ListBox();
            this.categories = new System.Windows.Forms.Label();
            this.commands = new System.Windows.Forms.Label();
            this.cancel = new System.Windows.Forms.Button();
            this.select = new System.Windows.Forms.Button();
            this.menuItemList = new CustomizeToolbar.Controls.MenuItemList();
            this.addSeparator = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // menuList
            // 
            this.menuList.FormattingEnabled = true;
            this.menuList.Location = new System.Drawing.Point(13, 26);
            this.menuList.Name = "menuList";
            this.menuList.Size = new System.Drawing.Size(132, 225);
            this.menuList.TabIndex = 0;
            this.menuList.SelectedIndexChanged += new System.EventHandler(this.menuList_SelectedIndexChanged);
            // 
            // categories
            // 
            this.categories.AutoSize = true;
            this.categories.Location = new System.Drawing.Point(10, 10);
            this.categories.Name = "categories";
            this.categories.Size = new System.Drawing.Size(60, 13);
            this.categories.TabIndex = 2;
            this.categories.Text = "Categories:";
            // 
            // commands
            // 
            this.commands.AutoSize = true;
            this.commands.Location = new System.Drawing.Point(152, 10);
            this.commands.Name = "commands";
            this.commands.Size = new System.Drawing.Size(62, 13);
            this.commands.TabIndex = 3;
            this.commands.Text = "Commands:";
            // 
            // cancel
            // 
            this.cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancel.Location = new System.Drawing.Point(282, 257);
            this.cancel.Name = "cancel";
            this.cancel.Size = new System.Drawing.Size(100, 23);
            this.cancel.TabIndex = 5;
            this.cancel.Text = "Cancel";
            this.cancel.UseVisualStyleBackColor = true;
            this.cancel.Click += new System.EventHandler(this.cancel_Click);
            // 
            // select
            // 
            this.select.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.select.Location = new System.Drawing.Point(176, 257);
            this.select.Name = "select";
            this.select.Size = new System.Drawing.Size(100, 23);
            this.select.TabIndex = 6;
            this.select.Text = "Select";
            this.select.UseVisualStyleBackColor = true;
            this.select.Click += new System.EventHandler(this.select_Click);
            // 
            // menuItemList
            // 
            this.menuItemList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.menuItemList.AutoScroll = true;
            this.menuItemList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.menuItemList.Location = new System.Drawing.Point(155, 26);
            this.menuItemList.Name = "menuItemList";
            toolStripProfessionalRenderer1.RoundedEdges = true;
            this.menuItemList.Renderer = toolStripProfessionalRenderer1;
            this.menuItemList.Size = new System.Drawing.Size(227, 225);
            this.menuItemList.TabIndex = 4;
            // 
            // addSeparator
            // 
            this.addSeparator.Location = new System.Drawing.Point(12, 257);
            this.addSeparator.Name = "addSeparator";
            this.addSeparator.Size = new System.Drawing.Size(100, 23);
            this.addSeparator.TabIndex = 7;
            this.addSeparator.Text = "Add Separator";
            this.addSeparator.UseVisualStyleBackColor = true;
            this.addSeparator.Click += new System.EventHandler(this.addSeparator_Click);
            // 
            // AddItemDialog
            // 
            this.AcceptButton = this.select;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancel;
            this.ClientSize = new System.Drawing.Size(394, 286);
            this.Controls.Add(this.addSeparator);
            this.Controls.Add(this.select);
            this.Controls.Add(this.cancel);
            this.Controls.Add(this.menuItemList);
            this.Controls.Add(this.commands);
            this.Controls.Add(this.categories);
            this.Controls.Add(this.menuList);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(410, 300);
            this.Name = "AddItemDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Add Item";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox menuList;
        private System.Windows.Forms.Label categories;
        private System.Windows.Forms.Label commands;
        private MenuItemList menuItemList;
        private System.Windows.Forms.Button cancel;
        private System.Windows.Forms.Button select;
        private System.Windows.Forms.Button addSeparator;
    }
}
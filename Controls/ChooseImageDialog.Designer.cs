namespace CustomizeToolbar.Controls
{
    partial class ChooseImageDialog
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
            this.imageList = new System.Windows.Forms.ListView();
            this.cancel = new System.Windows.Forms.Button();
            this.select = new System.Windows.Forms.Button();
            this.browse = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // imageList
            // 
            this.imageList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.imageList.Location = new System.Drawing.Point(13, 13);
            this.imageList.MultiSelect = false;
            this.imageList.Name = "imageList";
            this.imageList.Size = new System.Drawing.Size(359, 209);
            this.imageList.TabIndex = 0;
            this.imageList.UseCompatibleStateImageBehavior = false;
            this.imageList.ItemActivate += new System.EventHandler(this.imageList_ItemActivate);
            // 
            // cancel
            // 
            this.cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancel.Location = new System.Drawing.Point(272, 228);
            this.cancel.Name = "cancel";
            this.cancel.Size = new System.Drawing.Size(100, 23);
            this.cancel.TabIndex = 1;
            this.cancel.Text = "Cancel";
            this.cancel.UseVisualStyleBackColor = true;
            this.cancel.Click += new System.EventHandler(this.cancel_Click);
            // 
            // select
            // 
            this.select.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.select.Location = new System.Drawing.Point(166, 228);
            this.select.Name = "select";
            this.select.Size = new System.Drawing.Size(100, 23);
            this.select.TabIndex = 2;
            this.select.Text = "Select";
            this.select.UseVisualStyleBackColor = true;
            this.select.Click += new System.EventHandler(this.select_Click);
            // 
            // browse
            // 
            this.browse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.browse.Location = new System.Drawing.Point(13, 228);
            this.browse.Name = "browse";
            this.browse.Size = new System.Drawing.Size(100, 23);
            this.browse.TabIndex = 3;
            this.browse.Text = "Browse...";
            this.browse.UseVisualStyleBackColor = true;
            this.browse.Click += new System.EventHandler(this.browse_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.Filter = "Image Files|*.jpg;*.bmp;*.gif;*.png|All Files|*.*";
            this.openFileDialog.Title = "Browse...";
            // 
            // ChooseImageDialog
            // 
            this.AcceptButton = this.select;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancel;
            this.ClientSize = new System.Drawing.Size(384, 263);
            this.Controls.Add(this.browse);
            this.Controls.Add(this.select);
            this.Controls.Add(this.cancel);
            this.Controls.Add(this.imageList);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(400, 301);
            this.Name = "ChooseImageDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Choose Image";
            this.Click += new System.EventHandler(this.select_Click);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView imageList;
        private System.Windows.Forms.Button cancel;
        private System.Windows.Forms.Button select;
        private System.Windows.Forms.Button browse;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
    }
}
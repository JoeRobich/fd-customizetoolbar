using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PluginCore;
using System.Windows.Forms;

namespace CustomizeToolbar.Helpers
{
    public class ToolbarHelper
    {
        /// <summary>
        /// Arranges the Buttons on the FD Toolbar based on the ToolItem list.
        /// </summary>
        /// <param name="items"></param>
        public static void ArrangeToolbar(List<ToolItem> items)
        {
            List<ToolItem> missingToolItems = new List<ToolItem>();
            for (int index = 0; index < items.Count; index++)
            {
                ToolItem toolItem = items[index];
                if (toolItem.Item == null)
                {
                    missingToolItems.Add(toolItem);
                    continue;
                }
                if (string.IsNullOrEmpty(toolItem.MenuName))
                {
                    PluginBase.MainForm.ToolStrip.Items.Remove(toolItem.Item);
                }
                PluginBase.MainForm.ToolStrip.Items.Insert(index - missingToolItems.Count, toolItem.Item);
            }

            foreach (ToolItem toolItem in missingToolItems)
                items.Remove(toolItem);
        }

        /// <summary>
        /// Create a ToolStrip button for the menu item.
        /// </summary>
        /// <param name="menuItem"></param>
        /// <returns></returns>
        public static ToolStripButton CreateButton(ToolStripMenuItem menuItem)
        {
            ToolStripButton toolButton = new ToolStripButton(menuItem.Text, menuItem.Image);
            toolButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolButton.Tag = menuItem;
            toolButton.Click += new EventHandler(toolButton_Click);
            return toolButton;
        }

        /// <summary>
        /// Invoke the wrapped MenuItem when the button is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void toolButton_Click(object sender, EventArgs e)
        {
            ToolStripButton toolButton = (ToolStripButton)sender;
            ToolStripItem menuItem = (ToolStripItem)toolButton.Tag;
            menuItem.PerformClick();
        }
    }
}

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
        public static void ArrangeToolbar(List<ToolItem> items)
        {
            for (int index = 0; index < items.Count; index++)
            {
                ToolItem toolItem = items[index];
                if (string.IsNullOrEmpty(toolItem.MenuName))
                {
                    PluginBase.MainForm.ToolStrip.Items.Remove(toolItem.Item);
                }
                PluginBase.MainForm.ToolStrip.Items.Insert(index, toolItem.Item);
            }
        }

        public static ToolStripButton CreateButton(ToolStripMenuItem menuItem)
        {
            ToolStripButton toolButton = new ToolStripButton(menuItem.Text, menuItem.Image);
            toolButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolButton.Tag = menuItem;
            toolButton.Click += new EventHandler(toolButton_Click);
            return toolButton;
        }

        private static void toolButton_Click(object sender, EventArgs e)
        {
            ToolStripButton toolButton = (ToolStripButton)sender;
            ToolStripItem menuItem = (ToolStripItem)toolButton.Tag;
            menuItem.PerformClick();
        }
    }
}

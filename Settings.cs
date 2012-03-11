using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Text;
using System.Drawing;
using CustomizeToolbar.Localization;

namespace CustomizeToolbar
{
    public delegate void SettingsChangesEvent();

    [Serializable]
    public class Settings
    {
        [field: NonSerialized]
        public event SettingsChangesEvent OnSettingsChanged;

        private List<ToolItem> toolItems = new List<ToolItem>();

        [Browsable(false)]
        public List<ToolItem> ToolItems
        {
            get { return toolItems; }
            set { toolItems = value; }
        }

        private void FireChanged()
        {
            if (OnSettingsChanged != null) OnSettingsChanged();
        }
    }
}

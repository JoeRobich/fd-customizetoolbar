using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.Serialization;
using CustomizeToolbar.Helpers;

namespace CustomizeToolbar
{
    [Serializable]
    public class ToolItem : ISerializable
    {
        private ToolStripItem _item = null;
        private bool _visible = false;
        private string _menuName = string.Empty;
        private string _itemName = string.Empty;
        private string _itemText = string.Empty;
        private string _imageName = string.Empty;

        public ToolItem(ToolStripItem item)
        {
            Visible = item.Visible;
            Item = item;
            _itemName = item.Name;
            _itemText = item.Text;
        }

        public ToolItem(ToolStripItem item, string menuName)
            : this(item)
        {
            _menuName = menuName;
            _imageName = (item.Image != null && item.Image.Tag != null) ? item.Image.Tag.ToString() : string.Empty;
        }

        protected ToolItem(SerializationInfo info, StreamingContext context)
        {
            // Selectively deserialize the ToolItem
            Visible = info.GetBoolean("visible");
            _menuName = info.GetString("menuName");
            _itemName = info.GetString("itemName");
            _itemText = info.GetString("itemText");
            if (info.MemberCount > 4)
                _imageName = info.GetString("imageName");
        }

        public string MenuName { get { return _menuName; } }

        public bool Visible
        {
            get { return _visible; }
            set
            {
                _visible = value;
                if (Item == null)
                    return;
                Item.Visible = _visible;
            }
        }

        public string Name
        {
            get
            {
                return Item == null ? _itemName : Item.Name;
            }
        }

        public string Text
        {
            get
            {
                return Item == null ? _itemText : Item is ToolStripSeparator ? "Separator" : Item.Text;
            }
        }

        public string ImageName
        {
            get 
            { 
                return _imageName; 
            }

            set
            {
                _imageName = value;
            }
        }

        public ToolStripItem Item 
        {
            get { return _item; }
            set { _item = value; _item.Visible = _visible; }
        }

        public override string ToString()
        {
            // Create a readable name based on Name, Text, and MenuName properties.
            string label = string.IsNullOrEmpty(Text) ? Name : Text;
            label = label.Replace("&", string.Empty);
            if (!string.IsNullOrEmpty(MenuName))
                label += " (" + MenuName + ")";
            return label;
        }

        public override bool Equals(object obj)
        {
            if (obj is ToolItem)
                return Item == ((ToolItem)obj).Item;
            return false;
        }

        public override int GetHashCode()
        {
            return Item == null ? base.GetHashCode() : Item.GetHashCode();
        }

        #region ISerializable Members

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            // Selectively serialize the ToolItem
            info.AddValue("visible", Visible);
            info.AddValue("menuName", MenuName);
            info.AddValue("itemName", Name);
            info.AddValue("itemText", Text);
            info.AddValue("imageName", ImageName);
        }

        #endregion
    }
}
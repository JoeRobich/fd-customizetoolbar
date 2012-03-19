using System;
using System.IO;
using System.Windows.Forms;
using System.ComponentModel;
using PluginCore.Localization;
using PluginCore.Utilities;
using PluginCore.Managers;
using PluginCore;
using System.Drawing;
using System.Runtime.InteropServices;
using CustomizeToolbar.Helpers;
using CustomizeToolbar.Controls;
using System.Collections.Generic;

namespace CustomizeToolbar
{
	public class PluginMain : IPlugin
	{
        private const int API_KEY = 1;
        private const String NAME = "CustomizeToolbar";
        private const String GUID = "A6A38601-1DAC-4d2e-A610-5D6400093346";
        private const String HELP = "www.flashdevelop.org/community/viewtopic.php?f=4&t=9482";
        private const String DESCRIPTION = "Allows you to customize the order and visibility of toolbar items.";
        private const String AUTHOR = "Joey Robichaud";

        private String _settingsFilename = "";
        private Settings _settings = null;
        private CustomizeDialog _dialog = new CustomizeDialog();

	    #region Required Properties

        /// <summary>
        /// Api level of the plugin
        /// </summary>
        public Int32 Api
        {
            get { return API_KEY; }
        }

        /// <summary>
        /// Name of the plugin
        /// </summary> 
        public String Name
		{
			get { return NAME; }
		}

        /// <summary>
        /// GUID of the plugin
        /// </summary>
        public String Guid
		{
			get { return GUID; }
		}

        /// <summary>
        /// Author of the plugin
        /// </summary> 
        public String Author
		{
			get { return AUTHOR; }
		}

        /// <summary>
        /// Description of the plugin
        /// </summary> 
        public String Description
		{
			get { return DESCRIPTION; }
		}

        /// <summary>
        /// Web address for help
        /// </summary> 
        public String Help
		{
			get { return HELP; }
		}

        /// <summary>
        /// Object that contains the settings
        /// </summary>
        [Browsable(false)]
        public Object Settings
        {
            get { return this._settings; }
        }
		
		#endregion

        #region Required Methods

        /// <summary>
		/// Initializes the plugin
		/// </summary>
		public void Initialize()
		{
            this.InitBasics();
            this.LoadSettings();
            this.CreateMenuItems();
            this.AddEventHandlers();
        }
		
		/// <summary>
		/// Disposes the plugin
		/// </summary>
		public void Dispose()
		{
            this.SaveSettings();
		}
		
		/// <summary>
		/// Handles the incoming events
		/// </summary>
		public void HandleEvent(Object sender, NotifyEvent e, HandlingPriority prority)
		{
            if (e.Type == EventType.UIStarted)
            {
                // Use a timer to wait for any additional updates
                new Timer() { Interval = 100, Enabled = true }.Tick += new EventHandler(PluginMain_Tick);
            }
		}

        void PluginMain_Tick(object sender, EventArgs e)
        {
            Timer timer = (Timer)sender;
            timer.Stop();
            timer.Tick -= new EventHandler(PluginMain_Tick);
            PopulateToolbarItems();
        }
		
		#endregion

        #region Plugin Methods

        private void CustomizeClick(object sender, EventArgs e)
        {
            _dialog.ShowDialog();
        }

        public void PopulateToolbarItems()
        {
            SyncToolbarItems();
            AddMenuItems();
            ToolbarHelper.ArrangeToolbar(_settings.ToolItems);
            _dialog.Items = _settings.ToolItems;
        }

        private void SyncToolbarItems()
        {
            List<ToolItem> toolItems = _settings.ToolItems;
            bool firstRun = toolItems.Count == 0;
            int unknownIndex = 0;
            int separatorIndex = 0;

            for (int index = 0; index < PluginBase.MainForm.ToolStrip.Items.Count; index++)
            {
                ToolStripItem item = PluginBase.MainForm.ToolStrip.Items[index];
                if (item.Alignment == ToolStripItemAlignment.Right)
                    continue;

                // Give separators a name
                if (item is ToolStripSeparator)
                    item.Name = "Separator" + separatorIndex++;

                // Name buttons without a name
                if (string.IsNullOrEmpty(item.Name))
                    if (string.IsNullOrEmpty(item.Text))
                        item.Name = "Unknown" + unknownIndex++;
                    else
                        item.Name = item.Text.Replace(" ", "");

                // Sync ToolItems with their ToolButtons
                if (!firstRun)
                {
                    bool found = false;
                    foreach (ToolItem toolItem in toolItems)
                    {
                        if (string.IsNullOrEmpty(toolItem.MenuName) &&
                            toolItem.Name == item.Name)
                        {
                            found = true;
                            toolItem.Item = item;
                            break;
                        }
                    }

                    if (found)
                        continue;
                }

                // Add new items to our configuration
                toolItems.Add(new ToolItem(item));
            }
        }

        private void AddMenuItems()
        {
            List<ToolItem> toolItems = _settings.ToolItems;
            foreach (ToolItem toolItem in toolItems)
            {
                if (!string.IsNullOrEmpty(toolItem.MenuName))
                {
                    ToolStripMenuItem menu = null;
                    // Find Menu
                    foreach (ToolStripMenuItem item in PluginBase.MainForm.MenuStrip.Items)
                    {
                        string itemText = item.Text.Replace("&", "");
                        if (itemText == toolItem.MenuName)
                        {
                            menu = item;
                            break;
                        }
                    }

                    // Find Menu Item
                    foreach (ToolStripItem menuItem in menu.DropDownItems)
                    {
                        if (menuItem.Text == toolItem.Text)
                        {
                            if (!string.IsNullOrEmpty(toolItem.ImageName))
                            {
                                int imageNumber = -1;
                                if (int.TryParse(toolItem.ImageName, out imageNumber))
                                    menuItem.Image = PluginBase.MainForm.FindImage(toolItem.ImageName);
                                else if (File.Exists(toolItem.ImageName))
                                    menuItem.Image = Image.FromFile(toolItem.ImageName);
                            }

                            // Create ToolButton for menu item
                            ToolStripButton toolButton = ToolbarHelper.CreateButton((ToolStripMenuItem)menuItem);
                            toolItem.Item = toolButton;
                            break;
                        }
                    }
                }
            }
        }

        #endregion

        #region Custom Methods

        /// <summary>
        /// Initializes important variables
        /// </summary>
        public void InitBasics()
        {
            String dataPath = Path.Combine(PluginCore.Helpers.PathHelper.DataDir, NAME);
            if (!Directory.Exists(dataPath)) Directory.CreateDirectory(dataPath);
            _settingsFilename = Path.Combine(dataPath, "Settings.fdb");
        }

        /// <summary>
        /// Adds the required event handlers
        /// </summary> 
        public void AddEventHandlers()
        {
            // Set events you want to listen (combine as flags)
            EventManager.AddEventHandler(this, EventType.UIStarted, HandlingPriority.Low);
        }

        /// <summary>
        /// Adds shortcuts for manipulating the mini map
        /// </summary>
        public void CreateMenuItems()
        {
            ContextMenuStrip menu = new ContextMenuStrip();
            menu.Renderer = new DockPanelStripRenderer();
            menu.Items.Add(ResourceHelper.GetString("CustomizeToolbar.Label.Customize"), null, CustomizeClick);
            PluginBase.MainForm.ToolStrip.ContextMenuStrip = menu;
        }

        /// <summary>
        /// Loads the plugin settings
        /// </summary>
        public void LoadSettings()
        {
            _settings = new Settings();
            if (!File.Exists(_settingsFilename)) this.SaveSettings();
            else
            {
                Object obj = ObjectSerializer.Deserialize(_settingsFilename, _settings);
                _settings = (Settings)obj;
            }
        }

        /// <summary>
        /// Saves the plugin settings
        /// </summary>
        public void SaveSettings()
        {
            ObjectSerializer.Serialize(_settingsFilename, _settings);
        }

		#endregion
	}
}
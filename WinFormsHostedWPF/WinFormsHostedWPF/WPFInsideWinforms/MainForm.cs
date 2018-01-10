using System;
using System.Windows.Forms;
using NavLibWPFExample;
using System.Windows.Forms.Integration;
using OxTS.NavLib.PluginInterface;
using OxTS.NavLib.DataStoreManager.Interface;
using System.ComponentModel.Composition;

namespace WPFInsideWinforms
{
    [Export(typeof(INAVdisplayPlugin))]
    public partial class MainForm : OxTS.NAVdisplay.Toolbox.Items.PluginBase, INAVdisplayPlugin
    {
        IRealTimeDataStoreManagerAsync rtdsman_async;


        public MainForm()
        {
            InitializeComponent();
        }


        public new void Show()
        {
            
            ElementHost ctrlHost = new ElementHost
            {
                Dock = DockStyle.Fill
            };

            panel1.Controls.Add(ctrlHost);

            var WPFInputControl = new NavLibExampleUserControl();
            var WPFInputControlVM = new SelectUnitViewModel(rtdsman_async);

            WPFInputControl.DataContext = WPFInputControlVM;

            WPFInputControl.InitializeComponent();

            ctrlHost.Child = WPFInputControl;
        }

        public PluginTypes PluginType => PluginTypes.Utility;

        public bool MultipleInstancesAllowed => true;

        public void ConnectToDatastore(IRealTimeDataStoreManagerAsync rtds_man)
        {
            rtdsman_async = rtds_man;
            Show();
        }



        /// <summary>
        /// This is the text that will appear in the menu bar of NAVdisplay
        /// </summary>
        /// <returns></returns>
        public string GetMenuTextKey()
        {
            return "NAVLib WPF Example";
        }

        /// <summary>
        /// A valid licence key needs to be provided here
        /// </summary>
        /// <returns></returns>
        public string GetNAVlibLicense()
        {
            return "17a1272ed31f49d6970dbc6c3c6021ef";
        }

        /// <summary>
        /// Provide the type of the plugin here
        /// </summary>
        /// <returns></returns>
        public string GetTypeName()
        {
            return "1";
        }

        /// <summary>
        /// Provide the version of the plugin here
        /// </summary>
        /// <returns></returns>
        public string GetVersion()
        {
            return "1";
        }
    }
}

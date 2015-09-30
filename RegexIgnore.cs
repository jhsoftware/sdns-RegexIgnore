using System;
using System.Collections.Generic;
using System.Text;

namespace RegexIgnore
{
    public class RegexIgnore : JHSoftware.SimpleDNS.Plugin.IIgnoreRequestPlugIn
    {
        public event JHSoftware.SimpleDNS.Plugin.IPlugInBase.LogLineEventHandler LogLine;
        public event JHSoftware.SimpleDNS.Plugin.IPlugInBase.SaveConfigEventHandler SaveConfig;
        public event JHSoftware.SimpleDNS.Plugin.IPlugInBase.AsyncErrorEventHandler AsyncError;

        private System.Text.RegularExpressions.Regex rx;

        public JHSoftware.SimpleDNS.Plugin.IPlugInBase.PlugInTypeInfo GetPlugInTypeInfo()
        {
            JHSoftware.SimpleDNS.Plugin.IPlugInBase.PlugInTypeInfo rv;
            rv.Name = "Regex Ignore";
            rv.Description = "Ignore DNS requests where requested name matches a regular expression";
            rv.InfoURL = "";
            rv.ConfigFile = false;
            rv.MultiThreaded = false;
            return rv;
        }

        public bool IgnoreRequest(JHSoftware.SimpleDNS.Plugin.IDNSRequest request)
        {
            return rx.IsMatch(request.QName.ToString());
        }

        public JHSoftware.SimpleDNS.Plugin.DNSAskAbout GetDNSAskAbout()
        {
            return new JHSoftware.SimpleDNS.Plugin.DNSAskAbout();
        }

        public JHSoftware.SimpleDNS.Plugin.OptionsUI GetOptionsUI(Guid instanceID, string dataPath)
        {
            return new SettingsUI();
        }

        public bool InstanceConflict(string config1, string config2, ref string errorMsg)
        {
            return false;
        }

        public void LoadConfig(string config, Guid instanceID, string dataPath, ref int maxThreads)
        {
            rx = new System.Text.RegularExpressions.Regex(config, System.Text.RegularExpressions.RegexOptions.Compiled);
        }

        public void LoadState(string state)
        {
            return;
        }

        public string SaveState()
        {
            return null;
        }

        public void StartService()
        {
            return;
        }

        public void StopService()
        {
            return;
        }
    }
}

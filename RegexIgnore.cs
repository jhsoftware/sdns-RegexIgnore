using System;
using System.Collections.Generic;
using System.Text;
using JHSoftware.SimpleDNS;
using JHSoftware.SimpleDNS.Plugin;

namespace RegexIgnore
{
  public class RegexIgnore : IIgnoreRequest, IOptionsUI
  {
    private System.Text.RegularExpressions.Regex rx;

    IHost _Host;
    IHost IPlugInBase.Host { get => _Host; set => _Host=value; }

    public JHSoftware.SimpleDNS.Plugin.IPlugInBase.PlugInTypeInfo GetPlugInTypeInfo()
    {
      JHSoftware.SimpleDNS.Plugin.IPlugInBase.PlugInTypeInfo rv;
      rv.Name = "Regex Ignore";
      rv.Description = "Ignore DNS requests where requested name matches a regular expression";
      rv.InfoURL = "https://simpledns.plus/kb/184/regex-ignore-plug-in";
      return rv;
    }

    public System.Threading.Tasks.Task<bool> IgnoreRequest(IDNSRequest request)
    {
      return System.Threading.Tasks.Task.FromResult(rx.IsMatch(request.QName.ToString()));
    }

    public OptionsUI GetOptionsUI(Guid instanceID, string dataPath)
    {
      return new SettingsUI();
    }

    public bool InstanceConflict(string config1, string config2, ref string errorMsg)
    {
      return false;
    }

    public void LoadConfig(string config, Guid instanceID, string dataPath)
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

    public System.Threading.Tasks.Task StartService()
    {
      return System.Threading.Tasks.Task.CompletedTask;
    }

    public void StopService()
    {
      return;
    }
  }
}

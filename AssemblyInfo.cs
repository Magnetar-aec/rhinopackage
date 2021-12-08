using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Rhino.PlugIns;

[assembly: PlugInDescription(DescriptionType.Address,      "")]
[assembly: PlugInDescription(DescriptionType.Country,      "")]
[assembly: PlugInDescription(DescriptionType.Email,        "")]
[assembly: PlugInDescription(DescriptionType.Phone,        "")]
[assembly: PlugInDescription(DescriptionType.Organization, "G")]
[assembly: PlugInDescription(DescriptionType.UpdateUrl,    "")]
[assembly: PlugInDescription(DescriptionType.WebSite,      "")]

[assembly: AssemblyTitle("GregTest")]
[assembly: AssemblyDescription("no description")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("G")]
[assembly: AssemblyProduct("GregTest")]
[assembly: AssemblyCopyright("Copyright © G 2021")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

[assembly: ComVisible(false)]
[assembly: Guid("981443b0-9937-beb3-b5fa-283e5cc5cc67")]
[assembly: AssemblyVersion("0.1.8011.36094")]
[assembly: AssemblyFileVersion("0.1.8011.36094")]
[assembly: AssemblyInformationalVersion("0.1.0")]

public class CompilerPlugin : PlugIn 
{ 
  private static bool librariesLoaded = false;
  internal static void LoadLibraries()
  {
    if (librariesLoaded)
      return;
    librariesLoaded = true;

    
  }

  protected override LoadReturnCode OnLoad(ref string errorMessage)
  {
    var result = base.OnLoad(ref errorMessage);
    string message = "";
    if (!string.IsNullOrWhiteSpace(message))
    {
      Rhino.RhinoApp.WriteLine(message);
    }
    return result;
  }
}

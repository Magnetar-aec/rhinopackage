[Rhino.Commands.CommandStyle(Rhino.Commands.Style.ScriptRunner)]
public class Command_CreateFloorsfromMass : Rhino.Commands.Command
{
  public override string EnglishName
  {
    get { return "CreateFloorsfromMass"; }
  }

  string _definitionName = "Create Floors from Mass.gh";
  Grasshopper.Kernel.GH_Document _ghDoc = null;

  protected override Rhino.Commands.Result RunCommand(Rhino.RhinoDoc doc, Rhino.Commands.RunMode mode)
  {
    if(_ghDoc == null)
    {
      var archive = new GH_IO.Serialization.GH_Archive();
      System.Resources.ResourceManager rm = new System.Resources.ResourceManager("ScriptCode",
                                            System.Reflection.Assembly.GetExecutingAssembly());
      string source = rm.GetString("CreateFloorsfromMass");
      source = DecryptString(source);
      if (_definitionName.EndsWith(".gh", System.StringComparison.OrdinalIgnoreCase))
      {
        // source is base 64 encoded
        var bytes = System.Convert.FromBase64String(source);
        archive.Deserialize_Binary(bytes);
      }
      else
      {
        archive.Deserialize_Xml(source);
      }
      _ghDoc = new Grasshopper.Kernel.GH_Document();
      archive.ExtractObject(_ghDoc, "Definition");
    }

    object gh = Rhino.RhinoApp.GetPlugInObject("Grasshopper");
    var method = gh.GetType().GetMethod("RunAsCommand");
    if (method == null)
    {
      Rhino.RhinoApp.WriteLine("GrasshopperPlayer functionality not found. Make sure you are runninng at least Rhino 7");
      return Rhino.Commands.Result.Failure;
    }

    _ghDoc.ExpireSolution();

    var rc = (Rhino.Commands.Result)method.Invoke(gh, new object[] { _ghDoc, this, doc, mode });
    return rc;
  }

  private string DecryptString(string text)
  {
    if (text == null) { throw new System.ArgumentNullException("text"); }
    if (text.Length == 0) { return string.Empty; }

    byte[] data = System.Convert.FromBase64String(text);

    System.Security.Cryptography.RijndaelManaged rijndael = new System.Security.Cryptography.RijndaelManaged();
    rijndael.KeySize = 128;
    rijndael.BlockSize = 128;

    System.Guid key = new System.Guid("077b3a69-bace-ec56-b64c-4c3819c579a9");
    rijndael.Key = key.ToByteArray();
    rijndael.IV = key.ToByteArray();
    rijndael.Mode = System.Security.Cryptography.CipherMode.CBC;
    rijndael.Padding = System.Security.Cryptography.PaddingMode.PKCS7;

    System.Security.Cryptography.ICryptoTransform decryptor = rijndael.CreateDecryptor();
    byte[] result = decryptor.TransformFinalBlock(data, 0, data.Length);

    return System.Text.Encoding.UTF8.GetString(result);
  }
}

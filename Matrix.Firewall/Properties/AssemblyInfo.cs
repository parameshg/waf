using Matrix.Firewall.Tools;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;

[assembly: AssemblyTitle("Matrix.Firewall")]
[assembly: AssemblyDescription("Web Access Firewall")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("paramg.com")]
[assembly: AssemblyProduct("Matrix.Firewall")]
[assembly: AssemblyCopyright("Copyright © 2017 Web Access Firewall")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]
[assembly: ComVisible(false)]
[assembly: Guid("9d07811d-62ef-48ad-82bb-31392ce9cdf9")]
[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyFileVersion("1.0.0.0")]

public static class AssemblyInfo
{
    public static string Description
    {
        get
        {
            var result = string.Empty;

            var assembly = Assembly.GetExecutingAssembly();

            var attributes = assembly.GetCustomAttributes(typeof(AssemblyDescriptionAttribute), true);

            if (attributes.Length > 0)
                result = ((AssemblyDescriptionAttribute)attributes[0]).Description;

            return result;
        }
    }

    public static string Version
    {
        get
        {
            var result = string.Empty;

            var assembly = Assembly.GetExecutingAssembly();

            var fvi = FileVersionInfo.GetVersionInfo(assembly.Location);

            result = fvi.FileVersion;

            return result;
        }
    }

    public static string Build
    {
        get
        {
            var result = string.Empty;

            var assembly = Assembly.GetExecutingAssembly();

            result = new FileInfo(assembly.Location).LastWriteTime.ToString();

            return result;
        }
    }

    public static string Copyright
    {
        get
        {
            var result = string.Empty;

            var assembly = Assembly.GetExecutingAssembly();

            var attributes = assembly.GetCustomAttributes(typeof(AssemblyCopyrightAttribute), true);

            if (attributes.Length > 0)
                result = ((AssemblyCopyrightAttribute)attributes[0]).Copyright;

            return result;
        }
    }

    public static string Fingerprint
    {
        get
        {
            var result = string.Empty;

            var buffer = File.ReadAllBytes(Assembly.GetExecutingAssembly().Location);

            result = Hash.SHA256(buffer);

            return result;
        }
    }

    private static string AssemblyMetadata(string key)
    {
        var result = string.Empty;

        var assembly = Assembly.GetExecutingAssembly();

        var attributes = assembly.GetCustomAttributes(typeof(AssemblyMetadataAttribute), true);

        if (attributes.Length > 0)
        {
            foreach (var i in attributes)
            {
                var meta = i as AssemblyMetadataAttribute;

                if (meta != null && meta.Key.Equals(key))
                    result = meta.Value;
            }
        }

        return result;
    }
}

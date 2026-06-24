namespace SunamoCryptAlgorithms.Crypting;

public class UtilsNonNetStandard
{
    public static string GetXmlElement(string xml, string element)
    {
        var match = Regex.Match(xml, "<" + element + ">(?<Element>[^>]*)</" + element + ">", RegexOptions.IgnoreCase);
        if (match is null)
        {
            throw new Exception(Translate.FromKey(XlfKeys.CouldNotFind) + " " + element + "></" + element + "  " + Translate.FromKey(XlfKeys.inProvidedPublicKeyXML) + ".");
        }
        return match.Groups["Element"].ToString();
    }

    public static string GetConfigString(string key, bool isRequired)
    {
        string? configValue = null;
        if (configValue is null)
        {
            if (isRequired)
            {
                throw new Exception("key " + key + "  is missing from .config file");
            }
            else
            {
                return "";
            }
        }
        else
        {
            return configValue;
        }
    }

    public static string WriteConfigKey(string key, string value)
    {
        string format = "<add key=\"{0}\" value=\"{1}\" />" + Environment.NewLine;
        return string.Format(format, key, value);
    }

    public static string WriteXmlElement(string element, string value)
    {
        string format = "<{0}>{1}</{0}>" + Environment.NewLine;
        return string.Format(format, element, value);
    }

    public static string WriteXmlNode(string element, bool isClosing)
    {
        string format;
        if (isClosing)
        {
            format = "</{0}>" + Environment.NewLine;
        }
        else
        {
            format = "<{0}>" + Environment.NewLine;
        }
        return string.Format(format, element);
    }
}

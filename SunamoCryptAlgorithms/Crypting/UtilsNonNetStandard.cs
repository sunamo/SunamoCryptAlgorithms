namespace SunamoCryptAlgorithms.Crypting;

/// <summary>
/// Represents the utils non net standard type.
/// </summary>
public class UtilsNonNetStandard
{
    /// <summary>
    /// Get xml element.
    /// </summary>
    /// <param name="xml">The xml.</param>
    /// <param name="element">The element.</param>
    /// <returns>The get xml element result.</returns>
    public static string GetXmlElement(string xml, string element)
    {
        var match = Regex.Match(xml, "<" + element + ">(?<Element>[^>]*)</" + element + ">", RegexOptions.IgnoreCase);
        if (match is null)
        {
            throw new Exception("CouldNotFind" + " " + element + "></" + element + "  " + "In provided public key XML" + ".");
        }
        return match.Groups["Element"].ToString();
    }

    /// <summary>
    /// Get config string.
    /// </summary>
    /// <param name="key">The key.</param>
    /// <param name="isRequired">The is required.</param>
    /// <returns>The get config string result.</returns>
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

    /// <summary>
    /// Write config key.
    /// </summary>
    /// <param name="key">The key.</param>
    /// <param name="value">The value.</param>
    /// <returns>The write config key result.</returns>
    public static string WriteConfigKey(string key, string value)
    {
        string format = "<add key=\"{0}\" value=\"{1}\" />" + Environment.NewLine;
        return string.Format(format, key, value);
    }

    /// <summary>
    /// Write xml element.
    /// </summary>
    /// <param name="element">The element.</param>
    /// <param name="value">The value.</param>
    /// <returns>The write xml element result.</returns>
    public static string WriteXmlElement(string element, string value)
    {
        string format = "<{0}>{1}</{0}>" + Environment.NewLine;
        return string.Format(format, element, value);
    }

    /// <summary>
    /// Write xml node.
    /// </summary>
    /// <param name="element">The element.</param>
    /// <param name="isClosing">The is closing.</param>
    /// <returns>The write xml node result.</returns>
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

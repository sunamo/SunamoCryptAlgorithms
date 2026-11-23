namespace SunamoCryptAlgorithms.Crypting;

/// <summary>
/// Represents the utils type.
/// </summary>
public class Utils
{
    /// <summary>
    /// To hex.
    /// </summary>
    /// <param name="byteList">The byte list.</param>
    /// <returns>The to hex result.</returns>
    public static string ToHex(List<byte> byteList)
    {
        if (byteList is null || byteList.Count == 0)
        {
            return "";
        }

        const string HexFormat = "{0:X2}";
        var stringBuilder = new StringBuilder();
        foreach (byte byteValue in byteList)
        {
            stringBuilder.Append(string.Format(HexFormat, byteValue));
        }

        return stringBuilder.ToString();
    }

    /// <summary>
    /// From hex.
    /// </summary>
    /// <param name="hexEncoded">The hex encoded.</param>
    /// <returns>The from hex result.</returns>
    public static List<byte> FromHex(string hexEncoded)
    {
        if (hexEncoded is null || hexEncoded.Length == 0)
        {
            return null!;
        }

        try
        {
            hexEncoded = hexEncoded.TrimStart('#');
            int length = Convert.ToInt32(hexEncoded.Length / 2);
            var byteList = new List<byte>(length);

            for (int index = 0; index <= length - 1; index++)
            {
                byteList.Add(Convert.ToByte(hexEncoded.Substring(index * 2, 2), 16));
            }

            return byteList;
        }
        catch (Exception exception)
        {
            throw new Exception("The provided string does not appear to be hex encoded" + ":" + Environment.NewLine + hexEncoded + Environment.NewLine + Exceptions.TextOfExceptions(exception));
        }
    }

    /// <summary>
    /// From base 64.
    /// </summary>
    /// <param name="base64Encoded">The base 64 encoded.</param>
    /// <returns>The from base 64 result.</returns>
    public static byte[] FromBase64(string base64Encoded)
    {
        if (base64Encoded is null || base64Encoded.Length == 0)
        {
            return null!;
        }

        try
        {
            return Convert.FromBase64String(base64Encoded);
        }
        catch (FormatException exception)
        {
            throw new Exception("The provided string does not appear to be base 64 encoded" + ":" + Environment.NewLine + base64Encoded + Environment.NewLine + exception.Message);
        }
    }

    /// <summary>
    /// To base 64.
    /// </summary>
    /// <param name="byteList">The byte list.</param>
    /// <returns>The to base 64 result.</returns>
    public static string ToBase64(List<byte> byteList)
    {
        if (byteList is null || byteList.Count == 0)
        {
            return "";
        }

        return Convert.ToBase64String(byteList.ToArray());
    }
}

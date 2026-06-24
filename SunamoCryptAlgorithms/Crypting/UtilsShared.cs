namespace SunamoCryptAlgorithms.Crypting;

public class Utils
{
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

            for (int i = 0; i <= length - 1; i++)
            {
                byteList.Add(Convert.ToByte(hexEncoded.Substring(i * 2, 2), 16));
            }

            return byteList;
        }
        catch (Exception ex)
        {
            throw new Exception(Translate.FromKey(XlfKeys.TheProvidedStringDoesNotAppearToBeHexEncoded) + ":" + Environment.NewLine + hexEncoded + Environment.NewLine + Exceptions.TextOfExceptions(ex));
        }
    }

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
        catch (FormatException ex)
        {
            throw new Exception(Translate.FromKey(XlfKeys.TheProvidedStringDoesNotAppearToBeBase64Encoded) + ":" + Environment.NewLine + base64Encoded + Environment.NewLine + ex.Message);
        }
    }

    public static string ToBase64(List<byte> byteList)
    {
        if (byteList is null || byteList.Count == 0)
        {
            return "";
        }

        return Convert.ToBase64String(byteList.ToArray());
    }
}

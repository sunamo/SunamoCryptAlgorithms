namespace SunamoCryptAlgorithms.Crypting;

/// <summary>
/// Represents the data crypt type.
/// </summary>
public class DataCrypt
{
    private byte[] bytes = null!;
    private int maxBytes = 0;
    private int minBytes = 0;
    private int stepBytes = 0;
    /// <summary>
    /// The default encoding.
    /// </summary>
    public static Encoding DefaultEncoding = Encoding.GetEncoding("Windows1252");
    /// <summary>
    /// The encoding.
    /// </summary>
    public Encoding Encoding = DefaultEncoding;

    /// <summary>
    /// Data crypt.
    /// </summary>
    /// <param name=""Windows1252"">The windows 1252.</param>
    /// <returns>The data crypt result.</returns>
    public DataCrypt()
    {
    }

    /// <summary>
    /// Data crypt.
    /// </summary>
    /// <returns>The data crypt result.</returns>
    public DataCrypt(byte[] data)
    {
        bytes = data;
    }

    /// <summary>
    /// Initializes a new instance of the data crypt class.
    /// </summary>
    /// <param name="data">The data.</param>
    public DataCrypt(string text)
    {
        Text = text;
    }

    /// <summary>
    /// Initializes a new instance of the data crypt class.
    /// </summary>
    /// <param name="text">The text.</param>
    public DataCrypt(string text, Encoding encoding)
    {
        Encoding = encoding;
        Text = text;
    }

    /// <summary>
    /// The is empty.
    /// </summary>
    public bool IsEmpty
    {
        get
        {
            if (bytes is null)
            {
                return true;
            }
            if (bytes.Length == 0)
            {
                return true;
            }
            return false;
        }
    }

    /// <summary>
    /// The step bytes.
    /// </summary>
    public int StepBytes
    {
        get => stepBytes;
        set => stepBytes = value;
    }

    /// <summary>
    /// The step bits.
    /// </summary>
    public int StepBits
    {
        get => stepBytes * 8;
        set => stepBytes = value / 8;
    }

    /// <summary>
    /// The min bytes.
    /// </summary>
    public int MinBytes
    {
        get => minBytes;
        set => minBytes = value;
    }

    /// <summary>
    /// The min bits.
    /// </summary>
    public int MinBits
    {
        get => minBytes * 8;
        set => minBytes = value / 8;
    }

    /// <summary>
    /// The max bytes.
    /// </summary>
    public int MaxBytes
    {
        get => maxBytes;
        set => maxBytes = value;
    }

    /// <summary>
    /// The max bits.
    /// </summary>
    public int MaxBits
    {
        get => maxBytes * 8;
        set => maxBytes = value / 8;
    }

    /// <summary>
    /// The bytes.
    /// </summary>
    public byte[] Bytes
    {
        get
        {
            if (maxBytes > 0)
            {
                if (bytes.Length > maxBytes)
                {
                    byte[] trimmed = new byte[maxBytes];
                    Array.Copy(bytes, trimmed, trimmed.Length);
                    bytes = trimmed;
                }
            }
            if (minBytes > 0)
            {
                if (bytes.Length < minBytes)
                {
                    byte[] padded = new byte[minBytes];
                    Array.Copy(bytes, padded, bytes.Length);
                    bytes = padded;
                }
            }
            return bytes;
        }
        set => bytes = value;
    }

    /// <summary>
    /// The text.
    /// </summary>
    public string Text
    {
        get
        {
            if (bytes is null)
            {
                return "";
            }
            else
            {
                int nullIndex = Array.IndexOf(bytes, Convert.ToByte(0));
                if (nullIndex >= 0)
                {
                    return Encoding.GetString(bytes, 0, nullIndex);
                }
                else
                {
                    return Encoding.GetString(bytes);
                }
            }
        }
        set => bytes = Encoding.GetBytes(value);
    }

    /// <summary>
    /// The hex.
    /// </summary>
    public string Hex
    {
        get => Utils.ToHex(bytes.ToList());
        set => bytes = Utils.FromHex(value).ToArray();
    }

    /// <summary>
    /// The base 64.
    /// </summary>
    public string Base64
    {
        get => Utils.ToBase64(bytes.ToList());
        set => bytes = Utils.FromBase64(value);
    }

    /// <summary>
    /// To string.
    /// </summary>
    /// <param name="null">The null.</param>
    /// <returns>The to string result.</returns>
    public new string ToString()
    {
        return Text;
    }

    /// <summary>
    /// To base 64.
    /// </summary>
    /// <param name="bytes">The bytes.</param>
    /// <param name="Convert.ToByte(0)">The convert to byte 0.</param>
    /// <returns>The to base 64 result.</returns>
    public string ToBase64()
    {
        return Base64;
    }

    /// <summary>
    /// To hex.
    /// </summary>
    /// <param name="bytes">The bytes.</param>
    /// <param name="0">The 0.</param>
    /// <param name="nullIndex">The null index.</param>
    /// <returns>The to hex result.</returns>
    public string ToHex()
    {
        return Hex;
    }

    /// <summary>
    /// From file.
    /// </summary>
    /// <param name="filePath">The file path.</param>
    /// <returns>The from file result.</returns>
    public static
async Task<DataCrypt>
FromFile(string filePath)
    {
        var dataCrypt = new DataCrypt();
        dataCrypt.Text =
            await FileAsync.ReadAllTextAsync(filePath);
        return dataCrypt;
    }
}

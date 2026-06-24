namespace SunamoCryptAlgorithms.Crypting;

public class DataCrypt
{
    private byte[] bytes = null!;
    private int maxBytes = 0;
    private int minBytes = 0;
    private int stepBytes = 0;
    public static Encoding DefaultEncoding = Encoding.GetEncoding(Translate.FromKey(XlfKeys.Windows1252));
    public Encoding Encoding = DefaultEncoding;

    public DataCrypt()
    {
    }

    public DataCrypt(byte[] data)
    {
        bytes = data;
    }

    public DataCrypt(string text)
    {
        Text = text;
    }

    public DataCrypt(string text, Encoding encoding)
    {
        Encoding = encoding;
        Text = text;
    }

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

    public int StepBytes
    {
        get => stepBytes;
        set => stepBytes = value;
    }

    public int StepBits
    {
        get => stepBytes * 8;
        set => stepBytes = value / 8;
    }

    public int MinBytes
    {
        get => minBytes;
        set => minBytes = value;
    }

    public int MinBits
    {
        get => minBytes * 8;
        set => minBytes = value / 8;
    }

    public int MaxBytes
    {
        get => maxBytes;
        set => maxBytes = value;
    }

    public int MaxBits
    {
        get => maxBytes * 8;
        set => maxBytes = value / 8;
    }

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

    public string Hex
    {
        get => Utils.ToHex(bytes.ToList());
        set => bytes = Utils.FromHex(value).ToArray();
    }

    public string Base64
    {
        get => Utils.ToBase64(bytes.ToList());
        set => bytes = Utils.FromBase64(value);
    }

    public new string ToString()
    {
        return Text;
    }

    public string ToBase64()
    {
        return Base64;
    }

    public string ToHex()
    {
        return Hex;
    }

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

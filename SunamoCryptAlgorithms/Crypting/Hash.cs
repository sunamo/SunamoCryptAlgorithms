namespace SunamoCryptAlgorithms.Crypting;

public partial class Hash
{
    public enum Provider
    {
        CRC32,
        SHA1,
        SHA256,
        SHA384,
        SHA512,
        MD5
    }

    private HashAlgorithm hashAlgorithm = null!;
    private DataCrypt hashValue = new DataCrypt();

    private Hash()
    {
    }

    public Hash(Provider provider)
    {
        switch (provider)
        {
            case Provider.CRC32:
                hashAlgorithm = new CRC32();
                break;
            case Provider.MD5:
                hashAlgorithm = MD5.Create();
                break;
            case Provider.SHA1:
                hashAlgorithm = SHA1.Create();
                break;
            case Provider.SHA256:
                hashAlgorithm = SHA256.Create();
                break;
            case Provider.SHA384:
                hashAlgorithm = SHA384.Create();
                break;
            case Provider.SHA512:
                hashAlgorithm = SHA512.Create();
                break;
        }
    }

    public DataCrypt Value => hashValue;

    public DataCrypt Calculate(ref Stream stream)
    {
        hashValue.Bytes = hashAlgorithm.ComputeHash(stream);
        return hashValue;
    }

    public DataCrypt Calculate(DataCrypt dataCrypt)
    {
        return CalculatePrivate(dataCrypt.Bytes);
    }

    public DataCrypt Calculate(DataCrypt dataCrypt, DataCrypt salt)
    {
        byte[] combined = new byte[dataCrypt.Bytes.Length + salt.Bytes.Length];
        salt.Bytes.CopyTo(combined, 0);
        dataCrypt.Bytes.CopyTo(combined, salt.Bytes.Length);
        return CalculatePrivate(combined);
    }

    private DataCrypt CalculatePrivate(byte[] data)
    {
        hashValue.Bytes = hashAlgorithm.ComputeHash(data);
        return hashValue;
    }
}

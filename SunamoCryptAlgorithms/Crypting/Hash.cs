namespace SunamoCryptAlgorithms.Crypting;

/// <summary>
/// Represents the hash type.
/// </summary>
public partial class Hash
{
    /// <summary>
    /// Represents the provider type.
    /// </summary>
    public enum Provider
    {
        /// <summary>
        /// The CRC 32 value.
        /// </summary>
        CRC32,
        /// <summary>
        /// The SHA 1 value.
        /// </summary>
        SHA1,
        /// <summary>
        /// The SHA 256 value.
        /// </summary>
        SHA256,
        /// <summary>
        /// The SHA 384 value.
        /// </summary>
        SHA384,
        /// <summary>
        /// The SHA 512 value.
        /// </summary>
        SHA512,
        /// <summary>
        /// The MD 5 value.
        /// </summary>
        MD5
    }

    private HashAlgorithm hashAlgorithm = null!;
    private DataCrypt hashValue = new DataCrypt();

    private Hash()
    {
    }

    /// <summary>
    /// Initializes a new instance of the hash class.
    /// </summary>
    /// <param name="provider">The provider.</param>
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

    /// <summary>
    /// Gets the value.
    /// </summary>
    public DataCrypt Value => hashValue;

    /// <summary>
    /// Calculate.
    /// </summary>
    /// <returns>The calculate result.</returns>
    public DataCrypt Calculate(ref Stream stream)
    {
        hashValue.Bytes = hashAlgorithm.ComputeHash(stream);
        return hashValue;
    }

    /// <summary>
    /// Calculate.
    /// </summary>
    /// <returns>The calculate result.</returns>
    public DataCrypt Calculate(DataCrypt dataCrypt)
    {
        return CalculatePrivate(dataCrypt.Bytes);
    }

    /// <summary>
    /// Calculate.
    /// </summary>
    /// <returns>The calculate result.</returns>
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

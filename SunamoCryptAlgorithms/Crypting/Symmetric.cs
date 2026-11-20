namespace SunamoCryptAlgorithms.Crypting;

/// <summary>
/// Represents the symmetric type.
/// </summary>
public partial class Symmetric
{
    private const string defaultInitializationVector = "%1Az=-@qT";
    private const int bufferSize = 2048;

    /// <summary>
    /// Represents the provider type.
    /// </summary>
    public enum Provider
    {
        /// <summary>
        /// The DES value.
        /// </summary>
        DES,
        /// <summary>
        /// The RC 2 value.
        /// </summary>
        RC2,
        /// <summary>
        /// The rijndael value.
        /// </summary>
        Rijndael,
        /// <summary>
        /// The triple DES value.
        /// </summary>
        TripleDES
    }

    private DataCrypt encryptionKey = null!;
    private DataCrypt initializationVector = null!;
    private SymmetricAlgorithm symmetricAlgorithm = null!;

    private Symmetric()
    {
    }

    /// <summary>
    /// Symmetric.
    /// </summary>
    /// <returns>The symmetric result.</returns>
    public Symmetric(Provider provider, bool isUsingDefaultInitializationVector)
    {
        switch (provider)
        {
            case Provider.DES:
                symmetricAlgorithm = DES.Create();
                break;
            case Provider.RC2:
                symmetricAlgorithm = RC2.Create();
                break;
            case Provider.Rijndael:
                symmetricAlgorithm = Aes.Create();
                symmetricAlgorithm.Mode = CipherMode.CBC;
                break;
            case Provider.TripleDES:
                symmetricAlgorithm = TripleDES.Create();
                break;
        }

        Key = RandomKey();
        if (isUsingDefaultInitializationVector)
        {
            IntializationVector = new DataCrypt(defaultInitializationVector);
        }
        else
        {
            IntializationVector = RandomInitializationVector();
        }
    }

    /// <summary>
    /// The key size bytes.
    /// </summary>
    public int KeySizeBytes
    {
        get => symmetricAlgorithm.KeySize / 8;

        set
        {
            symmetricAlgorithm.KeySize = value * 8;
            encryptionKey.MaxBytes = value;
        }
    }

    /// <summary>
    /// The key size bits.
    /// </summary>
    public int KeySizeBits
    {
        get => symmetricAlgorithm.KeySize;

        set
        {
            symmetricAlgorithm.KeySize = value;
            encryptionKey.MaxBits = value;
        }
    }

    /// <summary>
    /// The key.
    /// </summary>
    public DataCrypt Key
    {
        get => encryptionKey;

        set
        {
            encryptionKey = value;
            encryptionKey.MaxBytes = symmetricAlgorithm.LegalKeySizes[0].MaxSize / 8;
            encryptionKey.MinBytes = symmetricAlgorithm.LegalKeySizes[0].MinSize / 8;
            encryptionKey.StepBytes = symmetricAlgorithm.LegalKeySizes[0].SkipSize / 8;
        }
    }

    /// <summary>
    /// The intialization vector.
    /// </summary>
    public DataCrypt IntializationVector
    {
        get => initializationVector;

        set
        {
            initializationVector = value;
            initializationVector.MaxBytes = symmetricAlgorithm.BlockSize / 8;
            initializationVector.MinBytes = symmetricAlgorithm.BlockSize / 8;
        }
    }

    /// <summary>
    /// Random initialization vector.
    /// </summary>
    /// <returns>The random initialization vector result.</returns>
    public DataCrypt RandomInitializationVector()
    {
        symmetricAlgorithm.GenerateIV();
        var dataCrypt = new DataCrypt(symmetricAlgorithm.IV);
        return dataCrypt;
    }

    /// <summary>
    /// Random key.
    /// </summary>
    /// <returns>The random key result.</returns>
    public DataCrypt RandomKey()
    {
        symmetricAlgorithm.GenerateKey();
        var dataCrypt = new DataCrypt(symmetricAlgorithm.Key);
        return dataCrypt;
    }

    private void ValidateKeyAndIv(bool isEncrypting)
    {
        if (encryptionKey.IsEmpty)
        {
            if (isEncrypting)
            {
                encryptionKey = RandomKey();
            }
            else
            {
                throw new Exception("No key was provided for the decryption operation" + "!");
            }
        }

        if (initializationVector.IsEmpty)
        {
            if (isEncrypting)
            {
                initializationVector = RandomInitializationVector();
            }
            else
            {
                throw new Exception("No initialization vector was provided for the decryption operation" + "!");
            }
        }

        symmetricAlgorithm.Key = encryptionKey.Bytes;
        symmetricAlgorithm.IV = initializationVector.Bytes;
    }

    /// <summary>
    /// Encrypt.
    /// </summary>
    /// <returns>The encrypt result.</returns>
    public DataCrypt Encrypt(DataCrypt dataCrypt, DataCrypt key)
    {
        Key = key;
        return Encrypt(dataCrypt);
    }

    /// <summary>
    /// Encrypt.
    /// </summary>
    /// <param name="initializationVector.IsEmpty">The initialization vector is empty.</param>
    /// <returns>The encrypt result.</returns>
    public DataCrypt Encrypt(DataCrypt dataCrypt)
    {
        var memoryStream = new MemoryStream();
        ValidateKeyAndIv(true);
        var cryptoStream = new CryptoStream(memoryStream, symmetricAlgorithm.CreateEncryptor(), CryptoStreamMode.Write);
        cryptoStream.Write(dataCrypt.Bytes, 0, dataCrypt.Bytes.Length);
        cryptoStream.Close();
        memoryStream.Close();
        return new DataCrypt(memoryStream.ToArray());
    }

    /// <summary>
    /// Encrypt.
    /// </summary>
    /// <param name="dataCrypt">The data crypt.</param>
    /// <param name="key">The key.</param>
    /// <returns>The encrypt result.</returns>
    public DataCrypt Encrypt(Stream stream, DataCrypt key, DataCrypt initVector)
    {
        IntializationVector = initVector;
        Key = key;
        return Encrypt(stream);
    }

    /// <summary>
    /// Encrypt.
    /// </summary>
    /// <param name="dataCrypt">The data crypt.</param>
    /// <param name="key">The key.</param>
    /// <returns>The encrypt result.</returns>
    public DataCrypt Encrypt(Stream stream, DataCrypt key)
    {
        Key = key;
        return Encrypt(stream);
    }
}

namespace SunamoCryptAlgorithms.Crypting;

public partial class Symmetric
{
    private const string defaultInitializationVector = "%1Az=-@qT";
    private const int bufferSize = 2048;

    public enum Provider
    {
        DES,
        RC2,
        Rijndael,
        TripleDES
    }

    private DataCrypt encryptionKey = null!;
    private DataCrypt initializationVector = null!;
    private SymmetricAlgorithm symmetricAlgorithm = null!;

    private Symmetric()
    {
    }

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

    public int KeySizeBytes
    {
        get => symmetricAlgorithm.KeySize / 8;

        set
        {
            symmetricAlgorithm.KeySize = value * 8;
            encryptionKey.MaxBytes = value;
        }
    }

    public int KeySizeBits
    {
        get => symmetricAlgorithm.KeySize;

        set
        {
            symmetricAlgorithm.KeySize = value;
            encryptionKey.MaxBits = value;
        }
    }

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

    public DataCrypt RandomInitializationVector()
    {
        symmetricAlgorithm.GenerateIV();
        var dataCrypt = new DataCrypt(symmetricAlgorithm.IV);
        return dataCrypt;
    }

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
                throw new Exception(Translate.FromKey(XlfKeys.NoKeyWasProvidedForTheDecryptionOperation) + "!");
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
                throw new Exception(Translate.FromKey(XlfKeys.NoInitializationVectorWasProvidedForTheDecryptionOperation) + "!");
            }
        }

        symmetricAlgorithm.Key = encryptionKey.Bytes;
        symmetricAlgorithm.IV = initializationVector.Bytes;
    }

    public DataCrypt Encrypt(DataCrypt dataCrypt, DataCrypt key)
    {
        Key = key;
        return Encrypt(dataCrypt);
    }

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

    public DataCrypt Encrypt(Stream stream, DataCrypt key, DataCrypt initVector)
    {
        IntializationVector = initVector;
        Key = key;
        return Encrypt(stream);
    }

    public DataCrypt Encrypt(Stream stream, DataCrypt key)
    {
        Key = key;
        return Encrypt(stream);
    }
}

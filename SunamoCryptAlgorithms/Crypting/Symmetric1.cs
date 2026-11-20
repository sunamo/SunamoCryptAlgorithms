namespace SunamoCryptAlgorithms.Crypting;

public partial class Symmetric
{
    /// <summary>
    /// Encrypt.
    /// </summary>
    /// <param name="stream">The stream.</param>
    /// <returns>The encrypt result.</returns>
    public DataCrypt Encrypt(Stream stream)
    {
        var memoryStream = new MemoryStream();
        var buffer = new byte[bufferSize + 1];
        ValidateKeyAndIv(true);
        var cryptoStream = new CryptoStream(memoryStream, symmetricAlgorithm.CreateEncryptor(), CryptoStreamMode.Write);
        int bytesRead = stream.Read(buffer, 0, bufferSize);
        while (bytesRead > 0)
        {
            cryptoStream.Write(buffer, 0, bytesRead);
            bytesRead = stream.Read(buffer, 0, bufferSize);
        }

        cryptoStream.Close();
        memoryStream.Close();
        return new DataCrypt(memoryStream.ToArray());
    }

    /// <summary>
    /// Decrypt.
    /// </summary>
    /// <param name="encryptedDataCrypt">The encrypted data crypt.</param>
    /// <param name="key">The key.</param>
    /// <returns>The decrypt result.</returns>
    public DataCrypt Decrypt(DataCrypt encryptedDataCrypt, DataCrypt key)
    {
        Key = key;
        return Decrypt(encryptedDataCrypt);
    }

    /// <summary>
    /// Decrypt.
    /// </summary>
    /// <param name="encryptedStream">The encrypted stream.</param>
    /// <param name="key">The key.</param>
    /// <returns>The decrypt result.</returns>
    public DataCrypt Decrypt(Stream encryptedStream, DataCrypt key)
    {
        Key = key;
        return Decrypt(encryptedStream);
    }

    /// <summary>
    /// Decrypt.
    /// </summary>
    /// <param name="encryptedStream">The encrypted stream.</param>
    /// <returns>The decrypt result.</returns>
    public DataCrypt Decrypt(Stream encryptedStream)
    {
        var memoryStream = new MemoryStream();
        var buffer = new byte[bufferSize + 1];
        ValidateKeyAndIv(false);
        var cryptoStream = new CryptoStream(encryptedStream, symmetricAlgorithm.CreateDecryptor(), CryptoStreamMode.Read);
        int bytesRead = cryptoStream.Read(buffer, 0, bufferSize);
        while (bytesRead > 0)
        {
            memoryStream.Write(buffer, 0, bytesRead);
            bytesRead = cryptoStream.Read(buffer, 0, bufferSize);
        }

        cryptoStream.Close();
        memoryStream.Close();
        return new DataCrypt(memoryStream.ToArray());
    }

    /// <summary>
    /// Decrypt.
    /// </summary>
    /// <param name="encryptedDataCrypt">The encrypted data crypt.</param>
    /// <returns>The decrypt result.</returns>
    public DataCrypt Decrypt(DataCrypt encryptedDataCrypt)
    {
        var memoryStream = new MemoryStream(encryptedDataCrypt.Bytes, 0, encryptedDataCrypt.Bytes.Length);
        var buffer = new byte[encryptedDataCrypt.Bytes.Length];
        ValidateKeyAndIv(false);
        var cryptoStream = new CryptoStream(memoryStream, symmetricAlgorithm.CreateDecryptor(), CryptoStreamMode.Read);
        try
        {
            cryptoStream.ReadExactly(buffer, 0, encryptedDataCrypt.Bytes.Length - 1);
        }
        catch (CryptographicException exception)
        {
            throw new Exception("Unable to decrypt data the provided key may be invalid" + "." + Exceptions.TextOfExceptions(exception));
        }
        finally
        {
            cryptoStream.Close();
        }

        return new DataCrypt(buffer);
    }
}

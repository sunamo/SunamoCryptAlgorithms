namespace SunamoCryptAlgorithms.Crypting;

public partial class Asymmetric
{
    public DataCrypt Encrypt(DataCrypt dataCrypt, PublicKey publicKey)
    {
        rsaCryptoProvider.ImportParameters(publicKey.ToParameters());
        return EncryptPrivate(dataCrypt);
    }

    public DataCrypt Encrypt(DataCrypt dataCrypt, string publicKeyXml)
    {
        LoadKeyXml(publicKeyXml, false);
        return EncryptPrivate(dataCrypt);
    }

    private DataCrypt EncryptPrivate(DataCrypt dataCrypt)
    {
        try
        {
            return new DataCrypt(rsaCryptoProvider.Encrypt(dataCrypt.Bytes, false));
        }
        catch (CryptographicException exception)
        {
            if (exception.Message.ToLower().IndexOf("bad length") > -1)
            {
                throw new Exception("Your data is too large RSA encryption is designed to encrypt relatively small amounts of data the exact byte limit depends on the key size to encrypt more data use symmetric encryption and then encrypt that symmetric key with asymmetric RSA encryption" + ".");
            }
            else
            {
                throw;
            }
        }
    }

    public DataCrypt Decrypt(DataCrypt encryptedDataCrypt)
    {
        var privateKey = new PrivateKey();
        privateKey.LoadFromConfig();
        return Decrypt(encryptedDataCrypt, privateKey);
    }

    public DataCrypt Decrypt(DataCrypt encryptedDataCrypt, PrivateKey privateKey)
    {
        rsaCryptoProvider.ImportParameters(privateKey.ToParameters());
        return DecryptPrivate(encryptedDataCrypt);
    }

    public DataCrypt Decrypt(DataCrypt encryptedDataCrypt, string privateKeyXml)
    {
        LoadKeyXml(privateKeyXml, true);
        return DecryptPrivate(encryptedDataCrypt);
    }

    private void LoadKeyXml(string keyXml, bool isPrivate)
    {
        try
        {
            rsaCryptoProvider.FromXmlString(keyXml);
        }
        catch (Exception)
        {
            string keyType = isPrivate ? "private" : "public";
            throw new Exception(string.Format("The provided 0 encryption key XML does not appear to be valid" + ".", keyType));
        }
    }

    private DataCrypt DecryptPrivate(DataCrypt encryptedDataCrypt)
        => new DataCrypt(rsaCryptoProvider.Decrypt(encryptedDataCrypt.Bytes, false));

    [System.Runtime.Versioning.SupportedOSPlatform("windows")]
    private RSACryptoServiceProvider GetRSAProvider()
    {
        RSACryptoServiceProvider? rsa = null;
        CspParameters? csp = null;
        try
        {
            csp = new CspParameters();
            csp.KeyContainerName = keyContainerName;
            rsa = new RSACryptoServiceProvider(keySize, csp);
            rsa.PersistKeyInCsp = false;
            RSACryptoServiceProvider.UseMachineKeyStore = true;
            return rsa;
        }
        catch (CryptographicException exception)
        {
            if (exception.Message.ToLower().IndexOf("csp for this implementation could not be acquired") > -1)
            {
                throw new Exception("Unable to obtain cryptographic service provider" + ". " + "Either the permissions are incorrect on the" + " 'C:\\Documents and Settings\\All Users\\Application DataCrypt\\Microsoft\\Crypto\\RSA\\MachineKeys' folder, or the current security context '" + WindowsIdentity.GetCurrent().Name + "' does not have access to this folder.");
            }
            else
            {
                throw;
            }
        }
        finally
        {
            rsa = null;
            csp = null;
        }
#pragma warning disable CS0162
        return null!;
#pragma warning restore CS0162
    }
}

namespace SunamoCryptAlgorithms.Crypting;

public partial class Asymmetric
{
    /// <summary>
    /// The default private key.
    /// </summary>
    public PrivateKey DefaultPrivateKey
    {
        get
        {
            var privateKey = new PrivateKey();
            privateKey.LoadFromConfig();
            return privateKey;
        }
    }

    /// <summary>
    /// Generate new keyset.
    /// </summary>
    /// <param name="publicKey">The public key.</param>
    /// <param name="privateKey">The private key.</param>
    /// <returns>The generate new keyset result.</returns>
    public void GenerateNewKeyset(ref PublicKey publicKey, ref PrivateKey privateKey)
    {
        string? publicKeyXml = null;
        string? privateKeyXml = null;
        GenerateNewKeyset(ref publicKeyXml!, ref privateKeyXml!);
        publicKey = new PublicKey(publicKeyXml!);
        privateKey = new PrivateKey(privateKeyXml!);
    }

    /// <summary>
    /// Generate new keyset.
    /// </summary>
    /// <param name="privateKeyXml!">The private key xml.</param>
    /// <returns>The generate new keyset result.</returns>
    public void GenerateNewKeyset(ref string publicKeyXml, ref string privateKeyXml)
    {
        var rsa = RSA.Create();
        publicKeyXml = rsa.ToXmlString(false);
        privateKeyXml = rsa.ToXmlString(true);
    }

    /// <summary>
    /// Encrypt.
    /// </summary>
    /// <param name="true">The true.</param>
    /// <returns>The encrypt result.</returns>
    public DataCrypt Encrypt(DataCrypt dataCrypt)
    {
        var publicKey = DefaultPublicKey;
        return Encrypt(dataCrypt, publicKey);
    }
}

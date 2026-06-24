namespace SunamoCryptAlgorithms.Crypting;

public partial class Asymmetric
{
    public PrivateKey DefaultPrivateKey
    {
        get
        {
            var privateKey = new PrivateKey();
            privateKey.LoadFromConfig();
            return privateKey;
        }
    }

    public void GenerateNewKeyset(ref PublicKey publicKey, ref PrivateKey privateKey)
    {
        string? publicKeyXml = null;
        string? privateKeyXml = null;
        GenerateNewKeyset(ref publicKeyXml!, ref privateKeyXml!);
        publicKey = new PublicKey(publicKeyXml!);
        privateKey = new PrivateKey(privateKeyXml!);
    }

    public void GenerateNewKeyset(ref string publicKeyXml, ref string privateKeyXml)
    {
        var rsa = RSA.Create();
        publicKeyXml = rsa.ToXmlString(false);
        privateKeyXml = rsa.ToXmlString(true);
    }

    public DataCrypt Encrypt(DataCrypt dataCrypt)
    {
        var publicKey = DefaultPublicKey;
        return Encrypt(dataCrypt, publicKey);
    }
}

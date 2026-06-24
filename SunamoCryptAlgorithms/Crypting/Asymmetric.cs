namespace SunamoCryptAlgorithms.Crypting;

public partial class Asymmetric
{
    private RSACryptoServiceProvider rsaCryptoProvider;
    private string keyContainerName = "Encryption.AsymmetricEncryption.DefaultContainerName";
    private int keySize = 1024;
    private const string elementParent = "RSAKeyValue";
    private const string elementModulus = "Modulus";
    private const string elementExponent = "Exponent";
    private const string elementPrimeP = "P";
    private const string elementPrimeQ = "Q";
    private const string elementPrimeExponentP = "DP";
    private const string elementPrimeExponentQ = "DQ";
    private const string elementCoefficient = "InverseQ";
    private const string elementPrivateExponent = "D";
    private const string keyModulus = "PublicKey.Modulus";
    private const string keyExponent = "PublicKey.Exponent";
    private const string keyPrimeP = "PrivateKey.P";
    private const string keyPrimeQ = "PrivateKey.Q";
    private const string keyPrimeExponentP = "PrivateKey.DP";
    private const string keyPrimeExponentQ = "PrivateKey.DQ";
    private const string keyCoefficient = "PrivateKey.InverseQ";
    private const string keyPrivateExponent = "PrivateKey.D";

    public class PublicKey
    {
        public string Modulus = null!;
        public string Exponent = null!;

        public PublicKey()
        {
        }

        public PublicKey(string keyXml)
        {
            LoadFromXml(keyXml);
        }

        public void LoadFromConfig()
        {
            Modulus = UtilsNonNetStandard.GetConfigString(keyModulus, true);
            Exponent = UtilsNonNetStandard.GetConfigString(keyExponent, true);
        }

        public string ToConfigSection()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append(UtilsNonNetStandard.WriteConfigKey(keyModulus, Modulus));
            stringBuilder.Append(UtilsNonNetStandard.WriteConfigKey(keyExponent, Exponent));
            return stringBuilder.ToString();
        }

        public void ExportToConfigFile(string filePath)
        {
            var streamWriter = new StreamWriter(filePath, false);
            streamWriter.Write(ToConfigSection());
            streamWriter.Close();
        }

        public void LoadFromXml(string keyXml)
        {
            Modulus = UtilsNonNetStandard.GetXmlElement(keyXml, Translate.FromKey(XlfKeys.Modulus));
            Exponent = UtilsNonNetStandard.GetXmlElement(keyXml, Translate.FromKey(XlfKeys.Exponent));
        }

        public RSAParameters ToParameters()
        {
            var parameters = new RSAParameters();
            parameters.Modulus = Convert.FromBase64String(Modulus);
            parameters.Exponent = Convert.FromBase64String(Exponent);
            return parameters;
        }

        public string ToXml()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append(UtilsNonNetStandard.WriteXmlNode(elementParent, false));
            stringBuilder.Append(UtilsNonNetStandard.WriteXmlElement(elementModulus, Modulus));
            stringBuilder.Append(UtilsNonNetStandard.WriteXmlElement(elementExponent, Exponent));
            stringBuilder.Append(UtilsNonNetStandard.WriteXmlNode(elementParent, true));
            return stringBuilder.ToString();
        }

        public void ExportToXmlFile(string filePath)
        {
            var streamWriter = new StreamWriter(filePath, false);
            streamWriter.Write(ToXml());
            streamWriter.Close();
        }
    }
}

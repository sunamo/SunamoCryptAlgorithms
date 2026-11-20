namespace SunamoCryptAlgorithms.Crypting;

/// <summary>
/// Represents the asymmetric type.
/// </summary>
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

    /// <summary>
    /// Represents the public key type.
    /// </summary>
    public class PublicKey
    {
        /// <summary>
        /// The modulus.
        /// </summary>
        public string Modulus = null!;
        /// <summary>
        /// The exponent.
        /// </summary>
        public string Exponent = null!;

        /// <summary>
        /// Public key.
        /// </summary>
        /// <returns>The public key result.</returns>
        public PublicKey()
        {
        }

        /// <summary>
        /// Public key.
        /// </summary>
        /// <returns>The public key result.</returns>
        public PublicKey(string keyXml)
        {
            LoadFromXml(keyXml);
        }

        /// <summary>
        /// Initializes a new instance of the load from config class.
        /// </summary>
        /// <param name="keyXml">The key xml.</param>
        public void LoadFromConfig()
        {
            Modulus = UtilsNonNetStandard.GetConfigString(keyModulus, true);
            Exponent = UtilsNonNetStandard.GetConfigString(keyExponent, true);
        }

        /// <summary>
        /// To config section.
        /// </summary>
        public string ToConfigSection()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append(UtilsNonNetStandard.WriteConfigKey(keyModulus, Modulus));
            stringBuilder.Append(UtilsNonNetStandard.WriteConfigKey(keyExponent, Exponent));
            return stringBuilder.ToString();
        }

        /// <summary>
        /// Export to config file.
        /// </summary>
        /// <returns>The export to config file result.</returns>
        public void ExportToConfigFile(string filePath)
        {
            var streamWriter = new StreamWriter(filePath, false);
            streamWriter.Write(ToConfigSection());
            streamWriter.Close();
        }

        /// <summary>
        /// Load from xml.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <param name="false">The false.</param>
        /// <returns>The load from xml result.</returns>
        public void LoadFromXml(string keyXml)
        {
            Modulus = UtilsNonNetStandard.GetXmlElement(keyXml, "Modulus");
            Exponent = UtilsNonNetStandard.GetXmlElement(keyXml, "Exponent");
        }

        /// <summary>
        /// To parameters.
        /// </summary>
        /// <param name="keyXml">The key xml.</param>
        public RSAParameters ToParameters()
        {
            var parameters = new RSAParameters();
            parameters.Modulus = Convert.FromBase64String(Modulus);
            parameters.Exponent = Convert.FromBase64String(Exponent);
            return parameters;
        }

        /// <summary>
        /// To xml.
        /// </summary>
        /// <returns>The to xml result.</returns>
        public string ToXml()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append(UtilsNonNetStandard.WriteXmlNode(elementParent, false));
            stringBuilder.Append(UtilsNonNetStandard.WriteXmlElement(elementModulus, Modulus));
            stringBuilder.Append(UtilsNonNetStandard.WriteXmlElement(elementExponent, Exponent));
            stringBuilder.Append(UtilsNonNetStandard.WriteXmlNode(elementParent, true));
            return stringBuilder.ToString();
        }

        /// <summary>
        /// Initializes a new instance of the export to xml file class.
        /// </summary>
        /// <param name="Modulus)">The modulus.</param>
        public void ExportToXmlFile(string filePath)
        {
            var streamWriter = new StreamWriter(filePath, false);
            streamWriter.Write(ToXml());
            streamWriter.Close();
        }
    }
}

namespace SunamoCryptAlgorithms.Crypting;

[System.Runtime.Versioning.SupportedOSPlatform("windows")]
public partial class Asymmetric
{
    /// <summary>
    /// Represents the private key type.
    /// </summary>
    public class PrivateKey
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
        /// The prime p.
        /// </summary>
        public string PrimeP = null!;
        /// <summary>
        /// The prime q.
        /// </summary>
        public string PrimeQ = null!;
        /// <summary>
        /// The prime exponent p.
        /// </summary>
        public string PrimeExponentP = null!;
        /// <summary>
        /// The prime exponent q.
        /// </summary>
        public string PrimeExponentQ = null!;
        /// <summary>
        /// The coefficient.
        /// </summary>
        public string Coefficient = null!;
        /// <summary>
        /// The private exponent.
        /// </summary>
        public string PrivateExponent = null!;

        /// <summary>
        /// Initializes a new instance of the private key class.
        /// </summary>
        public PrivateKey()
        {
        }

        /// <summary>
        /// Initializes a new instance of the private key class.
        /// </summary>
        /// <param name="keyXml">The key xml.</param>
        public PrivateKey(string keyXml)
        {
            LoadFromXml(keyXml);
        }

        /// <summary>
        /// Load from config.
        /// </summary>
        public void LoadFromConfig()
        {
            Modulus = UtilsNonNetStandard.GetConfigString(keyModulus, true);
            Exponent = UtilsNonNetStandard.GetConfigString(keyExponent, true);
            PrimeP = UtilsNonNetStandard.GetConfigString(keyPrimeP, true);
            PrimeQ = UtilsNonNetStandard.GetConfigString(keyPrimeQ, true);
            PrimeExponentP = UtilsNonNetStandard.GetConfigString(keyPrimeExponentP, true);
            PrimeExponentQ = UtilsNonNetStandard.GetConfigString(keyPrimeExponentQ, true);
            Coefficient = UtilsNonNetStandard.GetConfigString(keyCoefficient, true);
            PrivateExponent = UtilsNonNetStandard.GetConfigString(keyPrivateExponent, true);
        }

        /// <summary>
        /// To parameters.
        /// </summary>
        /// <returns>The to parameters result.</returns>
        public RSAParameters ToParameters()
        {
            var parameters = new RSAParameters();
            parameters.Modulus = Convert.FromBase64String(Modulus);
            parameters.Exponent = Convert.FromBase64String(Exponent);
            parameters.P = Convert.FromBase64String(PrimeP);
            parameters.Q = Convert.FromBase64String(PrimeQ);
            parameters.DP = Convert.FromBase64String(PrimeExponentP);
            parameters.DQ = Convert.FromBase64String(PrimeExponentQ);
            parameters.InverseQ = Convert.FromBase64String(Coefficient);
            parameters.D = Convert.FromBase64String(PrivateExponent);
            return parameters;
        }

        /// <summary>
        /// To config section.
        /// </summary>
        /// <param name="keyModulus">The key modulus.</param>
        /// <param name="true">The true.</param>
        /// <returns>The to config section result.</returns>
        public string ToConfigSection()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append(UtilsNonNetStandard.WriteConfigKey(keyModulus, Modulus));
            stringBuilder.Append(UtilsNonNetStandard.WriteConfigKey(keyExponent, Exponent));
            stringBuilder.Append(UtilsNonNetStandard.WriteConfigKey(keyPrimeP, PrimeP));
            stringBuilder.Append(UtilsNonNetStandard.WriteConfigKey(keyPrimeQ, PrimeQ));
            stringBuilder.Append(UtilsNonNetStandard.WriteConfigKey(keyPrimeExponentP, PrimeExponentP));
            stringBuilder.Append(UtilsNonNetStandard.WriteConfigKey(keyPrimeExponentQ, PrimeExponentQ));
            stringBuilder.Append(UtilsNonNetStandard.WriteConfigKey(keyCoefficient, Coefficient));
            stringBuilder.Append(UtilsNonNetStandard.WriteConfigKey(keyPrivateExponent, PrivateExponent));
            return stringBuilder.ToString();
        }

        /// <summary>
        /// Export to config file.
        /// </summary>
        /// <param name="Exponent">The exponent.</param>
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
        /// <returns>The load from xml result.</returns>
        public void LoadFromXml(string keyXml)
        {
            Modulus = UtilsNonNetStandard.GetXmlElement(keyXml, "Modulus");
            Exponent = UtilsNonNetStandard.GetXmlElement(keyXml, "Exponent");
            PrimeP = UtilsNonNetStandard.GetXmlElement(keyXml, "P");
            PrimeQ = UtilsNonNetStandard.GetXmlElement(keyXml, "Q");
            PrimeExponentP = UtilsNonNetStandard.GetXmlElement(keyXml, "DP");
            PrimeExponentQ = UtilsNonNetStandard.GetXmlElement(keyXml, "DQ");
            Coefficient = UtilsNonNetStandard.GetXmlElement(keyXml, "InverseQ");
            PrivateExponent = UtilsNonNetStandard.GetXmlElement(keyXml, "D");
        }

        /// <summary>
        /// Initializes a new instance of the to xml class.
        /// </summary>
        /// <param name="Coefficient)">The coefficient.</param>
        public string ToXml()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append(UtilsNonNetStandard.WriteXmlNode(elementParent, false));
            stringBuilder.Append(UtilsNonNetStandard.WriteXmlElement(elementModulus, Modulus));
            stringBuilder.Append(UtilsNonNetStandard.WriteXmlElement(elementExponent, Exponent));
            stringBuilder.Append(UtilsNonNetStandard.WriteXmlElement(elementPrimeP, PrimeP));
            stringBuilder.Append(UtilsNonNetStandard.WriteXmlElement(elementPrimeQ, PrimeQ));
            stringBuilder.Append(UtilsNonNetStandard.WriteXmlElement(elementPrimeExponentP, PrimeExponentP));
            stringBuilder.Append(UtilsNonNetStandard.WriteXmlElement(elementPrimeExponentQ, PrimeExponentQ));
            stringBuilder.Append(UtilsNonNetStandard.WriteXmlElement(elementCoefficient, Coefficient));
            stringBuilder.Append(UtilsNonNetStandard.WriteXmlElement(elementPrivateExponent, PrivateExponent));
            stringBuilder.Append(UtilsNonNetStandard.WriteXmlNode(elementParent, true));
            return stringBuilder.ToString();
        }

        /// <summary>
        /// Export to xml file.
        /// </summary>
        /// <param name="keyXml">The key xml.</param>
        /// <param name=""P"">The p.</param>
        /// <returns>The export to xml file result.</returns>
        public void ExportToXmlFile(string filePath)
        {
            var streamWriter = new StreamWriter(filePath, false);
            streamWriter.Write(ToXml());
            streamWriter.Close();
        }
    }

    /// <summary>
    /// Asymmetric.
    /// </summary>
    /// <returns>The asymmetric result.</returns>
    public Asymmetric()
    {
        rsaCryptoProvider = GetRSAProvider();
    }

    /// <summary>
    /// Initializes a new instance of the asymmetric class.
    /// </summary>
    /// <param name="Exponent)">The exponent.</param>
    public Asymmetric(int keySize)
    {
        this.keySize = keySize;
        rsaCryptoProvider = GetRSAProvider();
    }

    /// <summary>
    /// The key container name.
    /// </summary>
    public string KeyContainerName
    {
        get => keyContainerName;
        set => keyContainerName = value;
    }

    /// <summary>
    /// Gets the key size bits.
    /// </summary>
    public int KeySizeBits => rsaCryptoProvider.KeySize;

    /// <summary>
    /// Gets the key size max bits.
    /// </summary>
    public int KeySizeMaxBits => rsaCryptoProvider.LegalKeySizes[0].MaxSize;

    /// <summary>
    /// Gets the key size min bits.
    /// </summary>
    public int KeySizeMinBits => rsaCryptoProvider.LegalKeySizes[0].MinSize;

    /// <summary>
    /// Gets the key size step bits.
    /// </summary>
    public int KeySizeStepBits => rsaCryptoProvider.LegalKeySizes[0].SkipSize;

    /// <summary>
    /// The default public key.
    /// </summary>
    public PublicKey DefaultPublicKey
    {
        get
        {
            var publicKey = new PublicKey();
            publicKey.LoadFromConfig();
            return publicKey;
        }
    }
}

namespace SunamoCryptAlgorithms.Crypting;

[System.Runtime.Versioning.SupportedOSPlatform("windows")]
public partial class Asymmetric
{
    public class PrivateKey
    {
        public string Modulus = null!;
        public string Exponent = null!;
        public string PrimeP = null!;
        public string PrimeQ = null!;
        public string PrimeExponentP = null!;
        public string PrimeExponentQ = null!;
        public string Coefficient = null!;
        public string PrivateExponent = null!;

        public PrivateKey()
        {
        }

        public PrivateKey(string keyXml)
        {
            LoadFromXml(keyXml);
        }

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
            PrimeP = UtilsNonNetStandard.GetXmlElement(keyXml, "P");
            PrimeQ = UtilsNonNetStandard.GetXmlElement(keyXml, "Q");
            PrimeExponentP = UtilsNonNetStandard.GetXmlElement(keyXml, "DP");
            PrimeExponentQ = UtilsNonNetStandard.GetXmlElement(keyXml, "DQ");
            Coefficient = UtilsNonNetStandard.GetXmlElement(keyXml, "InverseQ");
            PrivateExponent = UtilsNonNetStandard.GetXmlElement(keyXml, "D");
        }

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

        public void ExportToXmlFile(string filePath)
        {
            var streamWriter = new StreamWriter(filePath, false);
            streamWriter.Write(ToXml());
            streamWriter.Close();
        }
    }

    public Asymmetric()
    {
        rsaCryptoProvider = GetRSAProvider();
    }

    public Asymmetric(int keySize)
    {
        this.keySize = keySize;
        rsaCryptoProvider = GetRSAProvider();
    }

    public string KeyContainerName
    {
        get => keyContainerName;
        set => keyContainerName = value;
    }

    public int KeySizeBits => rsaCryptoProvider.KeySize;

    public int KeySizeMaxBits => rsaCryptoProvider.LegalKeySizes[0].MaxSize;

    public int KeySizeMinBits => rsaCryptoProvider.LegalKeySizes[0].MinSize;

    public int KeySizeStepBits => rsaCryptoProvider.LegalKeySizes[0].SkipSize;

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

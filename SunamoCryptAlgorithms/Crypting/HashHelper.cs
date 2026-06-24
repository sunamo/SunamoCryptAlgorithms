namespace SunamoCryptAlgorithms.Crypting;

public class HashHelper
{
    public static string GetHashString(string text)
    {
        var hash = GetHash(UTF8Encoding.UTF8.GetBytes(text));
        return Encoding.UTF8.GetString(hash);
    }

    public static byte[] GetHash(byte[] passwordBytes, byte[] saltBytes)
    {
        var joined = CA.JoinBytesArray(passwordBytes, saltBytes);
        return GetHash(joined.ToArray());
    }

    public static byte[] GetHash(byte[] input)
    {
        var sha = SHA256.Create();
        return sha.ComputeHash(input);
    }

    public static void GetHashAndSalt(byte[] passwordBytes, out byte[] hash, out byte[] saltBytes)
    {
        saltBytes = RandomHelper.RandomBytes(10);
        var joined = CA.JoinBytesArray(passwordBytes, saltBytes);
        hash = GetHash(joined.ToArray());
    }

    public static void GetHashAndSalt(byte[] passwordBytes, out byte[] hash, out byte[] saltBytes, int saltByteCount)
    {
        saltBytes = RandomHelper.RandomBytes(saltByteCount);
        var joined = CA.JoinBytesArray(passwordBytes, saltBytes);
        hash = GetHash(joined.ToArray());
    }

    public static string GetMd5Hash(string text)
    {
        return GetMd5Hash(text, Encoding.UTF8);
    }

    public static string GetMd5Hash(string text, Encoding encoding)
    {
        var hash = MD5.Create();
        byte[] data = hash.ComputeHash(encoding.GetBytes(text));
        var stringBuilder = new StringBuilder();
        for (int i = 0; i < data.Length; i++)
        {
            stringBuilder.Append(data[i].ToString("x2"));
        }

        return stringBuilder.ToString();
    }

    public static bool PairHashAndPassword(byte[] hash, byte[] saltBytes, byte[] passwordBytes)
    {
        byte[] computedHash = GetHash(CA.JoinBytesArray(passwordBytes, saltBytes).ToArray());
        if (hash == computedHash)
        {
            return true;
        }

        return false;
    }
}

namespace SunamoCryptAlgorithms.Crypting;

/// <summary>
/// Represents the hash helper type.
/// </summary>
public class HashHelper
{
    /// <summary>
    /// Get hash string.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <returns>The get hash string result.</returns>
    public static string GetHashString(string text)
    {
        var hash = GetHash(UTF8Encoding.UTF8.GetBytes(text));
        return Encoding.UTF8.GetString(hash);
    }

    /// <summary>
    /// Get hash.
    /// </summary>
    /// <param name="passwordBytes">The password bytes.</param>
    /// <param name="saltBytes">The salt bytes.</param>
    /// <returns>The get hash result.</returns>
    public static byte[] GetHash(byte[] passwordBytes, byte[] saltBytes)
    {
        var joined = CA.JoinBytesArray(passwordBytes, saltBytes);
        return GetHash(joined.ToArray());
    }

    /// <summary>
    /// Get hash.
    /// </summary>
    /// <param name="input">The input.</param>
    /// <returns>The get hash result.</returns>
    public static byte[] GetHash(byte[] input)
    {
        var sha = SHA256.Create();
        return sha.ComputeHash(input);
    }

    /// <summary>
    /// Get hash and salt.
    /// </summary>
    /// <param name="passwordBytes">The password bytes.</param>
    /// <param name="hash">The hash.</param>
    /// <param name="saltBytes">The salt bytes.</param>
    public static void GetHashAndSalt(byte[] passwordBytes, out byte[] hash, out byte[] saltBytes)
    {
        saltBytes = RandomHelper.RandomBytes(10);
        var joined = CA.JoinBytesArray(passwordBytes, saltBytes);
        hash = GetHash(joined.ToArray());
    }

    /// <summary>
    /// Get hash and salt.
    /// </summary>
    /// <param name="passwordBytes">The password bytes.</param>
    /// <param name="hash">The hash.</param>
    /// <param name="saltBytes">The salt bytes.</param>
    /// <param name="saltByteCount">The salt byte count.</param>
    public static void GetHashAndSalt(byte[] passwordBytes, out byte[] hash, out byte[] saltBytes, int saltByteCount)
    {
        saltBytes = RandomHelper.RandomBytes(saltByteCount);
        var joined = CA.JoinBytesArray(passwordBytes, saltBytes);
        hash = GetHash(joined.ToArray());
    }

    /// <summary>
    /// Get md 5 hash.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <returns>The get md 5 hash result.</returns>
    public static string GetMd5Hash(string text)
    {
        return GetMd5Hash(text, Encoding.UTF8);
    }

    /// <summary>
    /// Get md 5 hash.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <param name="encoding">The encoding.</param>
    /// <returns>The get md 5 hash result.</returns>
    public static string GetMd5Hash(string text, Encoding encoding)
    {
        var hash = MD5.Create();
        byte[] data = hash.ComputeHash(encoding.GetBytes(text));
        var stringBuilder = new StringBuilder();
        for (int index = 0; index < data.Length; index++)
        {
            stringBuilder.Append(data[index].ToString("x2"));
        }

        return stringBuilder.ToString();
    }

    /// <summary>
    /// Pair hash and password.
    /// </summary>
    /// <param name="hash">The hash.</param>
    /// <param name="saltBytes">The salt bytes.</param>
    /// <param name="passwordBytes">The password bytes.</param>
    /// <returns>The pair hash and password result.</returns>
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

namespace SunamoCryptAlgorithms.Crypting;

public class CryptHelperAdvanced
{
    private static string InverseByBase(string text, int moveBase)
    {
        var stringBuilder = new StringBuilder();
        int chunkLength;
        for (int i = 0; i < text.Length; i += moveBase)
        {
            if (i + moveBase > text.Length - 1)
                chunkLength = text.Length - i;
            else
                chunkLength = moveBase;
            stringBuilder.Append(InverseString(text.Substring(i, chunkLength)));
        }
        return stringBuilder.ToString();
    }

    private static string InverseString(string text)
    {
        var stringBuilder = new StringBuilder();
        for (int i = text.Length - 1; i >= 0; i--)
        {
            stringBuilder.Append(text[i]);
        }
        return stringBuilder.ToString();
    }

    private static string ConvertToLetterDigit(string text)
    {
        var stringBuilder = new StringBuilder();
        foreach (char character in text)
        {
            if (char.IsLetterOrDigit(character) == false)
                stringBuilder.Append(Convert.ToInt16(character).ToString());
            else
                stringBuilder.Append(character);
        }
        return stringBuilder.ToString();
    }

    private static string Boring(string text)
    {
        int newPlace;
        char character;
        for (int i = 0; i < text.Length; i++)
        {
            newPlace = i * Convert.ToUInt16(text[i]);
            newPlace = newPlace % text.Length;
            character = text[i];
            text = text.Remove(i, 1);
            text = text.Insert(newPlace, character.ToString());
        }
        return text;
    }

    private static char ChangeChar(char character, int[] encodeValues)
    {
        character = char.ToUpper(character);
        if (character >= 'A' && character <= 'H')
            return Convert.ToChar(Convert.ToInt16(character) + 2 * encodeValues[0]);
        else if (character >= 'I' && character <= 'P')
            return Convert.ToChar(Convert.ToInt16(character) - encodeValues[2]);
        else if (character >= 'Q' && character <= 'Z')
            return Convert.ToChar(Convert.ToInt16(character) - encodeValues[1]);
        else if (character >= '0' && character <= '4')
            return Convert.ToChar(Convert.ToInt16(character) + 5);
        else if (character >= '5' && character <= '9')
            return Convert.ToChar(Convert.ToInt16(character) - 5);
        else
            return '0';
    }
}

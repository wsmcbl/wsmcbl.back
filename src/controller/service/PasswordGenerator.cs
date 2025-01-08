using System.Security.Cryptography;
using System.Text;

namespace wsmcbl.src.controller.service;

public class PasswordGenerator
{
    private const string LowerCase = "abcdefghijklmnopqrstuvwxyz";
    private const string UpperCase = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    private const string Digits = "0123456789";
    private const string SpecialChars = "!@#$%&*-_=+:.?";

    public string GeneratePassword(int length = 12, bool includeSpecialChars = true)
    {
        if (length < 8)
        {
            throw new ArgumentException("Password length must be at least 8 characters.");
        }
        
        var characterSet = LowerCase + UpperCase + Digits;
        if (includeSpecialChars)
        {
            characterSet += SpecialChars;
        }

        var password = new StringBuilder();
        var charArray = characterSet.ToCharArray();

        using (var rng = RandomNumberGenerator.Create())
        {
            var byteBuffer = new byte[sizeof(uint)];

            while (password.Length < length)
            {
                rng.GetBytes(byteBuffer);
                var num = BitConverter.ToUInt32(byteBuffer, 0);
                var index = num % characterSet.Length;

                password.Append(charArray[index]);
            }
        }

        return password.ToString();
    }
}
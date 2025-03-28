using System.Security.Cryptography;
using System.Text;

namespace wsmcbl.src.controller.service;

public class PasswordGenerator
{
    private const string LowerCase = "abcdefghijklmnopqrstuvwxyz";
    private const string UpperCase = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    private const string Digits = "0123456789";
    private const string SpecialChars = "!#$%&*-_=+:.";
    private StringBuilder? password { get; set; }

    public string generatePassword(int passwordLength = 12, bool includeSpecialChars = true)
    {
        if (passwordLength < 8)
        {
            throw new ArgumentException("Password length must be at least 8 characters.");
        }
        
        const string characterSet = LowerCase + UpperCase;
        var charArray = characterSet.ToCharArray();

        setMinimumChars(includeSpecialChars);
        using (var rng = RandomNumberGenerator.Create())
        {
            var byteBuffer = new byte[sizeof(uint)];

            while (password!.Length < passwordLength)
            {
                rng.GetBytes(byteBuffer);
                var num = BitConverter.ToUInt32(byteBuffer, 0);
                var index = num % characterSet.Length;

                password.Append(charArray[index]);
            }
        }
        
        var random = new Random();
        return new string(password.ToString().OrderBy(c => random.Next()).ToArray());
    }

    private void setMinimumChars(bool includeSpecialChars)
    {
        password = new StringBuilder();
        
        var random = new Random();
        password.Append(Digits[random.Next(Digits.Length)]);
        password.Append(Digits[random.Next(Digits.Length)]);
        
        password.Append(LowerCase[random.Next(Digits.Length)]);
        password.Append(UpperCase[random.Next(Digits.Length)]);

        if (!includeSpecialChars)
            return;
        
        password.Append(SpecialChars[random.Next(SpecialChars.Length)]);
        password.Append(SpecialChars[random.Next(SpecialChars.Length)]);
    }
}
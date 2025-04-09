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
        
        return new string(password.ToString()
            .OrderBy(c => RandomNumberGenerator.GetInt32(password.Length)).ToArray());
    }

    private void setMinimumChars(bool includeSpecialChars)
    {
        password = new StringBuilder();
        
        password.Append(Digits[RandomNumberGenerator.GetInt32(Digits.Length)]);
        password.Append(Digits[RandomNumberGenerator.GetInt32(Digits.Length)]);
        
        password.Append(LowerCase[RandomNumberGenerator.GetInt32(Digits.Length)]);
        password.Append(UpperCase[RandomNumberGenerator.GetInt32(Digits.Length)]);

        if (!includeSpecialChars)
            return;
        
        password.Append(SpecialChars[RandomNumberGenerator.GetInt32(SpecialChars.Length)]);
        password.Append(SpecialChars[RandomNumberGenerator.GetInt32(SpecialChars.Length)]);
    }
}
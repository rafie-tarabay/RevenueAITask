using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using RevenueAITask.Models;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RevenueAITask.Data
{
    public class CommonFunctions
    {
        public static async Task SendMail(User user, string initPassword)
        {
            //var apiKey = Environment.GetEnvironmentVariable("APIkey");
            var client = new SendGridClient("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");
            var from = new EmailAddress("eng.rafie@gmail.com", "Rafie");
            var subject = "Password Recovery!";
            var to = new EmailAddress(user.UserName, user.LastName);
            var plainTextContent = "To recover your account use this password within 15 min:      "+ initPassword;
            var htmlContent = "<strong>To recover your account use this password within 15 min: </strong>" + initPassword;
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
        }
    }
    public static class StringExtension
    {
        public static string PW_Encryptold(this string input)
        {
            using MD5 md5Hash = MD5.Create();
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
            StringBuilder sBuilder = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }
        public static string PW_Encrypt(this string password)
        {
            // Encrypt password
            byte[] salt = Encoding.ASCII.GetBytes("Sghdgytsg!*&^%(g");

            #region MyRegion
            //byte[] salt = new byte[128 / 8]; // Generate a 128-bit salt using a secure PRNG
            //using (var rng = RandomNumberGenerator.Create())
            //{
            //    rng.GetBytes(salt);
            //} 
            #endregion

            string encryptedPassw = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8
            ));

            return encryptedPassw;
        }
    }



    public static class GeneratePassword
    {
        private static readonly char[] Punctuations = "!@#$%^&*()_-+=[{]};:>|./?".ToCharArray();

        public static string Generate(int length, int numberOfNonAlphanumericCharacters)
        {
            if (length < 1 || length > 128)
            {
                throw new ArgumentException(nameof(length));
            }

            if (numberOfNonAlphanumericCharacters > length || numberOfNonAlphanumericCharacters < 0)
            {
                throw new ArgumentException(nameof(numberOfNonAlphanumericCharacters));
            }

            using (var rng = RandomNumberGenerator.Create())
            {
                var byteBuffer = new byte[length];

                rng.GetBytes(byteBuffer);

                var count = 0;
                var characterBuffer = new char[length];

                for (var iter = 0; iter < length; iter++)
                {
                    var i = byteBuffer[iter] % 87;

                    if (i < 10)
                    {
                        characterBuffer[iter] = (char)('0' + i);
                    }
                    else if (i < 36)
                    {
                        characterBuffer[iter] = (char)('A' + i - 10);
                    }
                    else if (i < 62)
                    {
                        characterBuffer[iter] = (char)('a' + i - 36);
                    }
                    else
                    {
                        characterBuffer[iter] = Punctuations[i - 62];
                        count++;
                    }
                }

                if (count >= numberOfNonAlphanumericCharacters)
                {
                    return new string(characterBuffer);
                }

                int j;
                var rand = new Random();

                for (j = 0; j < numberOfNonAlphanumericCharacters - count; j++)
                {
                    int k;
                    do
                    {
                        k = rand.Next(0, length);
                    }
                    while (!char.IsLetterOrDigit(characterBuffer[k]));

                    characterBuffer[k] = Punctuations[rand.Next(0, Punctuations.Length)];
                }

                return new string(characterBuffer);
            }
        }
    }
}

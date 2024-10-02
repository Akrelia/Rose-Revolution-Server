﻿using RevolutionCore.Networking;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RoseLoginServer.Core.Handling
{
    /// <summary>
    /// The packet handler.
    /// </summary>
    public partial class LoginPacketHandler
    {
        /// <summary>
        /// Test reply.
        /// </summary>
        /// <param name="client">Client.</param>
        /// <param name="packet">Packet.</param>
        /// <returns>Task.</returns>
        public async Task HandleLogin(LoginClient client, Packet packet)
        {
            ushort size = packet.Size;
            ushort command = packet.Command;
            byte[] dataBytes = packet.GetBytes(size - 4);

            string dataString = Encoding.UTF8.GetString(dataBytes);

            string[] credentials = dataString.Split(':');

            if (credentials.Length == 2)
            {
                string username = credentials[0].Replace("\0", string.Empty).Trim();
                string password = credentials[1].Replace("\0", string.Empty).Trim();

                Cryptography passwordManager = new Cryptography();
                dbAccountService accountService = new dbAccountService(database);
                var user = await accountService.GetUserAsync(username);

                if (user == null)
                {
                    await SendPacket(LoginFailedPacket("Something went wrong, could not login."), client);
                }

                if (user != null)
                {
                    bool loginSuccess = passwordManager.VerifyPassword(password, user.Password, user.Salt);

                    if (loginSuccess)
                    {
                        await SendPacket(LoginSuccessfullPacket(), client);
                    }
                    else
                    {
                        await SendPacket(LoginFailedPacket("Wrong password."), client);
                    }
                }
            }
            else
            {
                Console.WriteLine("Invalid chain format, ':' is missing.");
            }
        }

    }

    public class Cryptography
    {
        public string HashPassword(string password, string salt)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                string saltedPassword = password + salt; // Ajouter le sel au mot de passe
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(saltedPassword));
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2")); // Convertir en hexadécimal
                }
                return builder.ToString();
            }
        }

        public bool VerifyPassword(string password, string hashedPassword , string salt)
        {
            string hashedInputPassword = HashPassword(password, salt);
            Console.WriteLine($"hashedInputPassword : {hashedInputPassword}, hashed : {hashedPassword}");
            return hashedPassword.Equals(hashedInputPassword, StringComparison.OrdinalIgnoreCase);
        }
    }
}
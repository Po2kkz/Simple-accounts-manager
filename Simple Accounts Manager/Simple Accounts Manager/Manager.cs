using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using System.Windows;
using System.Security.Cryptography;

namespace Simple_Accounts_Manager
{
    class Manager
    {
        public static List<Account> accounts = new List<Account>();
        public static List<Account> Encryptedaccounts = new List<Account>();
        public static User user = new User(true,"");
        public static String appdata = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        public static String file = "SimpleAccountsManager.data";
        public static String folder = "Simple accounts manager";
        public static String userData = "UserData.data";
        public static void SaveToFile()
        {
            EncryptAccounts();
            String jsonAccounts = JsonConvert.SerializeObject(Encryptedaccounts);
            if (!Directory.Exists(Path.Combine(appdata,folder)))
            {
                Directory.CreateDirectory(Path.Combine(appdata, folder));
            }
            if(!File.Exists(Path.Combine(appdata,folder,file)))
            {
                File.Create(Path.Combine(appdata,folder,file)).Close();
            }
            File.WriteAllText(Path.Combine(appdata, folder, file), jsonAccounts);
            MainWindow.Instance.needsSave = false;
        }

        private static void EncryptAccounts()
        {
            Account holder = new Account();
            Encryptedaccounts.Clear();
            foreach(Account acc in accounts)
            {
                    holder.username = Encrypt(acc.username, user.Decryptedpassword);
                    holder.password = Encrypt(acc.password, user.Decryptedpassword);
                    holder.location = Encrypt(acc.location, user.Decryptedpassword);
                    Encryptedaccounts.Add(holder);
            }
        }

        public static void WriteToUserFile()
        {
            user.registered = true;
            
            String jsonUser = JsonConvert.SerializeObject(user);
            if (!Directory.Exists(Path.Combine(appdata, folder)))
            {
                Directory.CreateDirectory(Path.Combine(appdata, folder));
            }
            if (!File.Exists(Path.Combine(appdata, folder, userData)))
            {
                File.Create(Path.Combine(appdata, folder, userData)).Close();
            }
            File.WriteAllText(Path.Combine(appdata, folder, userData),jsonUser);
        }

        public static string ReadPasswordFromUserData()
        {
            user = JsonConvert.DeserializeObject<User>(File.ReadAllText(Path.Combine(appdata, folder, userData)));
            return user.password;
        }

        public static bool isFirstTimeJoin()
        {
            return !File.Exists(Path.Combine(appdata, folder, userData));
        }

        public static void LoadFromFile()
        {
            if(File.Exists(Path.Combine(appdata,folder,file)))
            {
                Encryptedaccounts = JsonConvert.DeserializeObject<List<Account>>(File.ReadAllText(Path.Combine(appdata, folder, file)));
            }
            DecryptAccounts();
        }

        public static void DecryptAccounts()
        {
            Account holder = new Account(null, null, null);
            accounts.Clear();
            foreach(Account acc in Encryptedaccounts)
            {
                    holder.username = Decrypt(acc.username, user.Decryptedpassword);
                    holder.password = Decrypt(acc.password, user.Decryptedpassword);
                    holder.location = Decrypt(acc.location, user.Decryptedpassword);
                    accounts.Add(holder);
                
            }
        }

        public static string Encrypt(string plainText, string password)
        {
            int SaltSize = 16;
            if (plainText == null)
                throw new ArgumentNullException("plainText");
            if (password == null)
                throw new ArgumentNullException("password");

            // Will return the cipher text
            string cipherText = "";

            // Utilizes helper function to generate random 16 byte salt using RNG
            RNGCryptoServiceProvider generator = new RNGCryptoServiceProvider();
            byte[] salt = new byte[16];
            generator.GetBytes(salt);
            // Convert plain text to bytes
            byte[] plainBytes = Encoding.Unicode.GetBytes(plainText);

            // create new password derived bytes using password/salt
            using (Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(password, salt))
            {
                using (Aes aes = AesManaged.Create())
                {
                    
                    // Generate key and iv from password/salt and pass to aes
                    aes.Key = pdb.GetBytes(aes.KeySize / 8);
                    aes.IV = pdb.GetBytes(aes.BlockSize / 8);
                    aes.Padding = PaddingMode.Zeros;
                    // Open a new memory stream to write the encrypted data to
                    using (MemoryStream ms = new MemoryStream())
                    {
                        // Create a crypto stream to perform encryption
                        using (CryptoStream cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
                        {
                            // write encrypted bytes to memory
                            cs.Write(plainBytes, 0, plainBytes.Length);
                        }
                        // get the cipher bytes from memory
                        byte[] cipherBytes = ms.ToArray();
                        // create a new byte array to hold salt + cipher
                        byte[] saltedCipherBytes = new byte[salt.Length + cipherBytes.Length];
                        // copy salt + cipher to new array
                        Array.Copy(salt, 0, saltedCipherBytes, 0, salt.Length);
                        Array.Copy(cipherBytes, 0, saltedCipherBytes, salt.Length, cipherBytes.Length);
                        // convert cipher array to base 64 string
                        cipherText = Convert.ToBase64String(saltedCipherBytes);
                    }
                    aes.Clear();
                }
            }
            return cipherText;
        }


        public static string Decrypt(string cipherText, string password)
        {
            int SaltSize = 16;
            if (cipherText == null)
                throw new ArgumentNullException("cipherText");
            if (password == null)
                throw new ArgumentNullException("password");

            // will return plain text
            string plainText = "";
            // get salted cipher array
            byte[] saltedCipherBytes = Convert.FromBase64String(cipherText);
            // create array to hold salt
            byte[] salt = new byte[SaltSize];
            // create array to hold cipher
            byte[] cipherBytes = new byte[saltedCipherBytes.Length - salt.Length];

            // copy salt/cipher to arrays
            Array.Copy(saltedCipherBytes, 0, salt, 0, salt.Length);
            Array.Copy(saltedCipherBytes, salt.Length, cipherBytes, 0, saltedCipherBytes.Length - salt.Length);

            // create new password derived bytes using password/salt
            using (Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(password, salt))
            {
                using (Aes aes = AesManaged.Create())
                {
                    
                    
                    // Generate key and iv from password/salt and pass to aes
                    aes.Key = pdb.GetBytes(aes.KeySize / 8);
                    aes.IV = pdb.GetBytes(aes.BlockSize / 8);
                    aes.Padding = PaddingMode.Zeros;
                    // Open a new memory stream to write the encrypted data to
                    using (MemoryStream ms = new MemoryStream())
                    {
                        
                        // Create a crypto stream to perform decryption
                        using (CryptoStream cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Write))
                        {
                            // write decrypted data to memory
                            
                            cs.Write(cipherBytes, 0, cipherBytes.Length);
                        }
                        // convert decrypted array to plain text string
                        plainText = Encoding.Unicode.GetString(ms.ToArray());
                        plainText = plainText.TrimEnd(char.MinValue);
                    }
                    aes.Clear();
                }
            }
            return plainText;
        }

    }
}

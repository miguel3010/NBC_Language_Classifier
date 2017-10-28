namespace General
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net.Mail;
    using System.Security.Cryptography;
    using System.Text;

    public class Util
    {
        public static string Encrypt(string clearText)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }

        public static string Decrypt(string cipherText)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }

        public static Boolean validateString(string txt)
        {
            if (txt != null)
            {
                if (txt != string.Empty)
                {
                    return true;
                }
            }
            return false;
        }

        public static Boolean isNumeric(string k)
        {

            if (validateString(k))
            {
                try
                {
                    int l = Int32.Parse(k);
                    return true;
                }
                catch (Exception)
                {
                }
            }
            return false;
        }

        public static Boolean validatePassword(string pass)
        {
            if (validateString(pass))
            {
                if (pass.Length >= 8)
                {
                    return true;
                }
            }
            return false;
        }

        public static bool ValidateEmail(string emailaddress)
        {
            if (Util.validateString(emailaddress))
            {
                try
                {
                    MailAddress m = new MailAddress(emailaddress);
                    return true;
                }
                catch (Exception)
                {

                }
            }
            return false;
        }

        public static Boolean validateDateTime(DateTime dt)
        {
            if (DateTime.Compare(dt, new DateTime()) == 0)
            {
                return false;
            }
            return true;
        }

        private static DateTime getDate(string date, Boolean firstDate)
        {
            if (Util.validateString(date))
            {
                string[] fechas = date.Split('-');
                if (fechas.Length == 2)
                {
                    DateTime g = Convert.ToDateTime(fechas[(firstDate ? 0 : 1)]);
                    if (Util.validateDateTime(g))
                    {
                        return g;
                    }
                }
            }
            return new DateTime();
        }
        /// <summary>
        /// Obtiene la primera fecha de un rango de fechas ("08/11/2016 - 11/18/2016")
        /// </summary>
        /// <param name="date">DateTime = "08/11/2016" format M/d/yyyy</param>
        /// <returns></returns>
        public static DateTime getFechaInicio(string date)
        {
            return getDate(date, true);
        }
        /// <summary>
        /// Obtiene la segunda fecha de un rango de fechas ("08/11/2016 - 11/18/2016")
        /// </summary>
        /// <param name="date">DateTime = "11/18/2016" format M/d/yyyy</param>
        /// <returns></returns>
        public static DateTime getFechaFinal(string date)
        {
            return getDate(date, false);
        }
        /// <summary>
        /// Agrega el contenido de una lista en otra
        /// </summary>
        /// <typeparam name="T">Tipo de datos de las listas</typeparam>
        /// <param name="Ld"></param>
        /// <param name="Lo"></param>
        public static void AddToList<T>(List<T> Ld, List<T> Lo)
        {
            if (Ld != null && Lo != null)
            {
                int i = 0;
                while (i < Lo.Count)
                {
                    Ld.Add(Lo[i++]);
                }
            }
        }

        public static void AddNotRepeatedToList(List<String> ld, String lo)
        {
            if (ld != null && lo != null && lo.Any())
            {

                if (!ld.Any())
                {
                    ld.Add(lo);
                }
                else
                {
                    bool regex = false;
                    foreach (var i in ld)
                    {
                        if (i.Equals(lo))
                        {
                            regex = true;
                            break;
                        }
                    }
                    if (!regex)
                    {
                        ld.Add(lo);
                    }

                }

            }
        }
    }
}

using System;
using System.Text;
using System.IO;
using System.Runtime.Remoting.Contexts;
using System.Xml.Schema;
using static System.Net.Mime.MediaTypeNames;

namespace vernam
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 3)
            {
                Console.WriteLine("Usage:\n    vernam [option] [CIPHER_TEXT] [PATH/TO/KEY]");
                return;
            }
            string option = args[0];
            string path = args[2];
            string key = File.ReadAllText(args[2]).Trim();
            switch (option)
            {
                case ("-e"):
                case ("--encrypt"):
                    (string, int) ck = Encrypt(args[1], key);
                    Console.WriteLine("-----START OF ENCRYPTED MESSAGE-----\n" + ck.Item1 + "\n-----END OF ENCRYPTED MESSAGE-----");
                    File.WriteAllText(path, key.Remove(0, ck.Item2));
                    break;
                case ("-d"):
                case ("--decrypt"):
                    (string, int) pk = Decrypt(args[1], key);
                    Console.WriteLine("-----START OF DECRYPTED MESSAGE-----\n" + pk.Item1 + "\n-----END OF DECRYPTED MESSAGE-----");
                    File.WriteAllText(path, key.Remove(0, pk.Item2));

                    break;
            }
        }
        private static (string, int) Encrypt(string p, string k)
        {
            StringBuilder sb = new StringBuilder();
            int len = 0;
            for (int i = 0; i < p.Length; i++)
            {
                if (p[i] == ' ')
                {
                    sb.Append(' ');
                    continue;
                }
                if (char.ToLower(p[i]) == p[i])
                {
                    sb.Append(char.ConvertFromUtf32((p[i] - 'a' + k[i] - 'a') % 26 + 'a'));
                    len++;
                }
                else
                {
                    sb.Append(char.ConvertFromUtf32((p[i] - 'A' + k[i] - 'A') % 26 + 'A'));
                    len++;
                } 
            }
            return (sb.ToString(), len);
        }
        private static (string, int) Decrypt(string c, string k)
        {
            StringBuilder sb = new StringBuilder();
            int len = 0;
            for (int i = 0; i < c.Length; i++)
            {
                if (c[i] == ' ')
                {
                    sb.Append(' ');
                    continue;
                }
                if (char.ToLower(c[i]) == c[i])
                {
                    sb.Append(char.ConvertFromUtf32((c[i] - 'a' - (k[i] - 'a') + 26) % 26 + 'a'));
                    len++;
                }
                else
                {
                    sb.Append(char.ConvertFromUtf32((c[i] - 'A' - (k[i] - 'A') + 26) % 26 + 'A'));
                    len++;
                }
            }
            return (sb.ToString(), len);
        }
    }
}

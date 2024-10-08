using System;
using System.Text;
using System.IO;

namespace vernam
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(args.Length.ToString());
            if (args.Length != 4)
            {
                Console.WriteLine("Usage:\n    vernam [option] [CIPHER_TEXT] [PATH/TO/KEY]");
                return;
            }
            switch (args[1])
            {
                case ("-e"):
                case ("--encrypt"):
                    (string, string) ck = Encrypt(args[2], args[3]);
                    Console.WriteLine("-----START OF ENCRYPTED MESSAGE-----\n" + ck.Item1 + "\n-----END OF ENCRYPTED MESSAGE-----");
                    //File.WriteAllText(args[3], ck.Item2);
                    break;
                case ("-d"):
                case ("--decrypt"):
                    (string, string) pk = Decrypt(args[2], args[3]);
                    Console.WriteLine("-----START OF DECRYPTED MESSAGE-----\n" + pk.Item1 + "\n-----END OF DECRYPTED MESSAGE-----");
                    //File.WriteAllText(args[3], pk.Item2); break;
                    break;
            }
        }
        private static (string, string) Encrypt(string p, string k)
        {
            StringBuilder sb = new StringBuilder();
            string ukey = k;
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
                    ukey.Remove(i, 1);
                }
                else
                {
                    sb.Append(char.ConvertFromUtf32((p[i] - 'A' + k[i] - 'A') % 26 + 'A'));
                    ukey.Remove(i, 1);
                } 
            }
            return (sb.ToString(), ukey);
        }
        private static (string, string) Decrypt(string c, string k)
        {
            StringBuilder sb = new StringBuilder();
            string ukey = k;

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
                    ukey.Remove(i, 1);
                }
                else
                {
                    sb.Append(char.ConvertFromUtf32((c[i] - 'A' - (k[i] - 'A') + 26) % 26 + 'A'));
                    ukey.Remove(i, 1);

                }
            }
            return (sb.ToString(), ukey);
        }
    }
}

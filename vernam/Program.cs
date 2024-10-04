using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vernam
{
    internal class Program
    {
        static void Main(string[] args)
        {
            switch (args.Count) 
            {
                case (2):
                    try
                    {

                    }
                    catch 
                    {
                    
                    }
                    break;
                default:
                    Help();
                    Environment.ExitCode(0);
            }
        }

        private void Help()
        {
            Console.WriteLine("Usage:\n    vernam [CIPHER_TEXT] [PATH/TO/KEY]");
        }
    }
}

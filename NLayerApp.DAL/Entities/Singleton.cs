using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayerApp.DAL.Entities
{
    public class Singleton
    {
        public string Date { get; private set; }
        public static string text = "Hello!";
        private Singleton()
        {
            Date = DateTime.Now.ToString("HH:mm:ss");
        }

        public static Singleton GetInstance()
        {
            Console.WriteLine($"GetInstance {DateTime.Now.ToString("HH:mm:ss")}");
            return Nested.instance;
        }

        private class Nested
        {
            static Nested() { }
            internal static readonly Singleton instance = new Singleton();
        }
    }
}
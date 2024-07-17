using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace _04.GeneradorAleatorioDeContraseñas
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //int length = 16; //Longitud de la contraseña
            Console.WriteLine("Programa que genera una contraseña aleatoria con ciertas características que se deben de cumplir.")
            string password = GenerarContrasenia(8, 16);
            Console.WriteLine("Contraseña generada: "+password);
            Console.ReadLine();
        }

        static string GenerarContrasenia(int minlength, int maxlength)
        {
            string shuffledPassword = string.Empty;
            try
            {
                const string lower = "abcdefghijklmnopqrstuvwxyz";
                const string upper = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
                const string digits = "0123456789";
                const string special = "!@#$%^&*()_+[]{}|;:,.<>?";
              
                StringBuilder password = new StringBuilder();
                Random random = new Random();
                List<char> requiredChars = new List<char>
                        {
                            upper[random.Next(upper.Length)],
                            lower[random.Next(lower.Length)],
                            digits[random.Next(digits.Length)],
                            special[random.Next(special.Length)]
                        };
                string allChars = lower + upper + digits + special;
                int totalChars = random.Next(minlength, maxlength + 1) - requiredChars.Count;
                // Uso de RNGCryptoServiceProvider para generar números aleatorios seguros
                using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
                {
                    byte[] randomNumber = new byte[4]; // Tamaño de un entero (4 bytes)

                    for (int i = 0; i < totalChars; i++)
                    {
                        rng.GetBytes(randomNumber);
                        int value = BitConverter.ToInt32(randomNumber, 0);
                        requiredChars.Add(allChars[Math.Abs(value) % allChars.Length]);
                    }
                }
                // Mezclar los caracteres para evitar patrones predecibles
                shuffledPassword = ShuffleString(new string(requiredChars.ToArray()));
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hubo un error al generar la contraseña. " + ex.Message);
            }
            return shuffledPassword;
        }

        static string ShuffleString(string str)
        {
            char[] array = str.ToCharArray();
            Random random = new Random();
            for (int i = array.Length - 1; i > 0; i--)
            {
                int j = random.Next(i + 1);
                char temp = array[i];
                array[i] = array[j];
                array[j] = temp;
            }
            return new string(array);
        }

    }
}

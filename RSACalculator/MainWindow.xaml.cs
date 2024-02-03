using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Security.Cryptography;
using System.Numerics;
using System.Reflection;
using System.Collections;
using static System.Net.Mime.MediaTypeNames;

namespace RSACalculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            try
            {
                //var text = TryDecrypt();
                var text = TryFullDecrypt("string");
                //TestRSA("string");
            }
            catch (ArgumentNullException)
            {
                //Catch this exception in case the encryption did
                //not succeed.
                Console.WriteLine("Encryption failed.");
            }

        }

        /// <summary>
        ///  try to decrypt the data of ergasia 3
        /// </summary>
        /// <returns></returns>
        public static string TryDecrypt()
        {
            try
            {
                string nString = "84641628266040968134726266858994621642842114704041843715900121029779624800354442181024933538554572539172519633298583132480574427234887384864237862510536135711979678422472297405718744900411759436052738392404109005261750528594360602400820678382015390265136033037813004859176942808586218232493968301780230653856757977924165795192647059713220241170741461160395394356567033643278968175549740103490059728058303074181319743641739740188742499285217435763085176127055686256451491791548801388560202149363758975803673725819798718410301085155062206092370667133063831263112034304132340281237240943617970645337927088718346956199378919";
                BigInteger n = BigInteger.Parse(nString);

                string eString = "65537";
                BigInteger e = BigInteger.Parse(eString);

                string cString = "76578687539749270334567433327419068016846482753554386520492865010545019207873571124020033190161215682615446396410129994711325892805940645283340607450605814292242214864807736616366963263577528008848595692050559543833562773099105599921926111989644313805176327043401089098178374013481826785604433013115807761169019821323105749924330624040032176017861852570855402207200857443405652874317191870029368327928342025351921450031345537488126081651667235334445880387925966636557003621257916083406394805099048054073995341365825171820787167114782405887040220243880710430205621846850135276025997937602400232787790214801902431955984378";
                BigInteger c = BigInteger.Parse(cString);

                //var number = StringToNumber("string");
                //var text = NumberToString(number);

                BigInteger p = 3;
                BigInteger q = n / p;
                BigInteger f_n = (p - 1) * (q - 1);
                BigInteger d = ModInverse(e, f_n);


                var m = Decrypt(c, d, n);
                var result = NumberToString(m);

                return result;
            }
            catch (ArgumentNullException)
            {
                //Catch this exception in case the encryption did
                //not succeed.
                Console.WriteLine("routine failed.");
                return "";
            }
        }

        public static string TryFullDecrypt(string text)
        {
            try
            {
                // encryption
                BigInteger m = StringToNumber(text);
                
                string nString = "84641628266040968134726266858994621642842114704041843715900121029779624800354442181024933538554572539172519633298583132480574427234887384864237862510536135711979678422472297405718744900411759436052738392404109005261750528594360602400820678382015390265136033037813004859176942808586218232493968301780230653856757977924165795192647059713220241170741461160395394356567033643278968175549740103490059728058303074181319743641739740188742499285217435763085176127055686256451491791548801388560202149363758975803673725819798718410301085155062206092370667133063831263112034304132340281237240943617970645337927088718346956199378919";
                BigInteger q = BigInteger.Parse(nString);

                string eString = "65537";
                BigInteger e = BigInteger.Parse(eString);

                BigInteger p = 3;
                BigInteger n = p * q;
                BigInteger f_n = (p - 1) * (q - 1);
                BigInteger d = ModInverse(e, f_n);
                var c = Encrypt(m, e, n);

                // encryption
                m = Decrypt(c, d, n);
                var result = NumberToString(m);

                return result;
            }
            catch (ArgumentNullException)
            {
                //Catch this exception in case the encryption did
                //not succeed.
                Console.WriteLine("routine failed.");
                return "";
            }
        }

        public static BigInteger StringToNumber(string text)
        {
            var bytes = Encoding.UTF8.GetBytes(text);
            Array.Reverse(bytes);
            var number = new BigInteger(bytes); //126943972912743

            return number;
        }

        public static string NumberToString(BigInteger number)
        {
            byte[] bytes = number.ToByteArray();
            Array.Reverse(bytes);
            var text = Encoding.Default.GetString(bytes);

            return text;
        }

        private static BigInteger Encrypt(BigInteger m, BigInteger n, BigInteger e)
        {
            return BigInteger.ModPow(m, e, n);
        }

        private static BigInteger Decrypt(BigInteger c, BigInteger d, BigInteger n)
        {
            return BigInteger.ModPow(c, d, n);
        }

        /// <summary>
        /// Calculates the modular multiplicative inverse of <paramref name="a"/> modulo <paramref name="m"/>
        /// using the extended Euclidean algorithm.
        /// </summary>
        /// <remarks>
        /// This implementation comes from the pseudocode defining the inverse(a, n) function at
        /// https://en.wikipedia.org/wiki/Extended_Euclidean_algorithm
        /// </remarks>
        public static BigInteger ModInverse(BigInteger a, BigInteger n)
        {
            BigInteger t = 0, nt = 1, r = n, nr = a;

            if (n < 0)
                n = -n;

            if (a < 0)
                a = n - (-a % n);

            while (nr != 0)
            {
                var quot = r / nr;

                var tmp = nt; nt = t - quot * nt; t = tmp;
                tmp = nr; nr = r - quot * nr; r = tmp;
            }

            if (r > 1) throw new ArgumentException(nameof(a) + " is not convertible.");
            if (t < 0) t = t + n;
            return t;
        }




        public static void TestRSA(string text)
        {
            try
            {
                //Create a UnicodeEncoder to convert between byte array and string.
                UnicodeEncoding ByteConverter = new UnicodeEncoding();

                //Create byte arrays to hold original, encrypted, and decrypted data.
                byte[] dataToEncrypt = ByteConverter.GetBytes(text);
                byte[] encryptedData;
                byte[] decryptedData;

                //Create a new instance of RSACryptoServiceProvider to generate
                //public and private key data.
                using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
                {

                    var p = BigInteger.Parse("17");
                    var q = BigInteger.Parse("19");
                    var e = BigInteger.Parse("5");
                    BigInteger n = p * q;
                    BigInteger f_n = (p - 1) * (q - 1);
                    BigInteger d = ModInverse(e, f_n);

                    //Pass the data to ENCRYPT, the public key information 
                    //(using RSACryptoServiceProvider.ExportParameters(false),
                    //and a boolean flag specifying no OAEP padding.
                    encryptedData = RSAEncrypt(dataToEncrypt, RSA.ExportParameters(false), false);

                    //Pass the data to DECRYPT, the private key information 
                    //(using RSACryptoServiceProvider.ExportParameters(true),
                    //and a boolean flag specifying no OAEP padding.
                    decryptedData = RSADecrypt(encryptedData, RSA.ExportParameters(true), false);
                    //decryptedData = RSADecrypt(encryptedData, parameters, false);

                    //Display the decrypted plaintext to the console. 
                    Console.WriteLine("Decrypted plaintext: {0}", ByteConverter.GetString(decryptedData));
                }
            }
            catch (ArgumentNullException)
            {
                //Catch this exception in case the encryption did
                //not succeed.
                Console.WriteLine("Encryption failed.");
            }
        }
        public static byte[] RSAEncrypt(byte[] DataToEncrypt, RSAParameters RSAKeyInfo, bool DoOAEPPadding)
        {
            try
            {
                byte[] encryptedData;
                //Create a new instance of RSACryptoServiceProvider.
                using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
                {

                    //Import the RSA Key information. This only needs
                    //to include the public key information.
                    RSA.ImportParameters(RSAKeyInfo);

                    //Encrypt the passed byte array and specify OAEP padding.  
                    //OAEP padding is only available on Microsoft Windows XP or
                    //later.  
                    encryptedData = RSA.Encrypt(DataToEncrypt, DoOAEPPadding);
                }
                return encryptedData;
            }
            //Catch and display a CryptographicException  
            //to the console.
            catch (CryptographicException e)
            {
                Console.WriteLine(e.Message);

                return null;
            }
        }

        public static byte[] RSADecrypt(byte[] DataToDecrypt, RSAParameters RSAKeyInfo, bool DoOAEPPadding)
        {
            try
            {
                byte[] decryptedData;
                //Create a new instance of RSACryptoServiceProvider.
                using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
                {
                    //Import the RSA Key information. This needs
                    //to include the private key information.
                    RSA.ImportParameters(RSAKeyInfo);

                    //Decrypt the passed byte array and specify OAEP padding.  
                    //OAEP padding is only available on Microsoft Windows XP or
                    //later.  
                    decryptedData = RSA.Decrypt(DataToDecrypt, DoOAEPPadding);
                }
                return decryptedData;
            }
            //Catch and display a CryptographicException  
            //to the console.
            catch (CryptographicException e)
            {
                Console.WriteLine(e.ToString());

                return null;
            }
        }


        private static RSAParameters Create(byte[] p, byte[] q, byte[] exponent, byte[] modulus)
        {
            var addlParameters = GetFullPrivateParameters(
                p: new BigInteger(CopyAndReverse(p)),
                q: new BigInteger(CopyAndReverse(q)),
                e: new BigInteger(CopyAndReverse(exponent)),
                modulus: new BigInteger(CopyAndReverse(modulus)));

            return new RSAParameters
            {
                P = p,
                Q = q,
                Exponent = exponent,
                Modulus = modulus,
                D = addlParameters.D,
                DP = addlParameters.DP,
                DQ = addlParameters.DQ,
                InverseQ = addlParameters.InverseQ,
            };
        }

        private static RSAParameters GetFullPrivateParameters(BigInteger p, BigInteger q, BigInteger e, BigInteger modulus)
        {
            var n = p * q;
            var phiOfN = (p - 1) * (q - 1);

            var d = ModInverse(e, phiOfN);
            //Assert.Equal(1, (d * e) % phiOfN);

            var dp = d % (p - 1);
            var dq = d % (q - 1);

            var qInv = ModInverse(q, p);
            //Assert.Equal(1, (qInv * q) % p);

            return new RSAParameters
            {
                D = CopyAndReverse(d.ToByteArray()),
                DP = CopyAndReverse(dp.ToByteArray()),
                DQ = CopyAndReverse(dq.ToByteArray()),
                InverseQ = CopyAndReverse(qInv.ToByteArray()),
            };
        }

        private static byte[] CopyAndReverse(byte[] data)
        {
            byte[] reversed = new byte[data.Length];
            Array.Copy(data, 0, reversed, 0, data.Length);
            Array.Reverse(reversed);
            return reversed;
        }

       




    }
}
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
                //Create a UnicodeEncoder to convert between byte array and string.
                UnicodeEncoding ByteConverter = new UnicodeEncoding();

                //Create byte arrays to hold original, encrypted, and decrypted data.
                byte[] dataToEncrypt = ByteConverter.GetBytes("Data to Encrypt");
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

                    //var f = ModInverse(e, f_n);


                    BigInteger m = 32;
                    var bytes = Encoding.UTF8.GetBytes("Nick Kritikou");
                    //var m = new BigInteger(bytes.Concat(new byte[] { 0 }).ToArray());
                    var c = Encrypt(m, n, e);
                    var m_ = Decrypt(c, d, n);


                    var parameters = Create(p.ToByteArray(), q.ToByteArray(), e.ToByteArray(), n.ToByteArray());

                    //var parameters = new RSAParameters
                    //{
                    //    P = p.ToByteArray(),
                    //    Q = q.ToByteArray(),
                    //    D = d.ToByteArray(),
                    //    Modulus = n.ToByteArray(),
                    //    Exponent = e.ToByteArray(),
                    //};

                    //var bytes = Encoding.UTF8.GetBytes(DataResource.RequiredModuleNotFound);

                    //byte[] bytes = BitConverter.GetBytes(17);

                    //var result = BigInteger.TryParse("76578687539749270334567433327419068016846482753554386520492865010545019207873571124020033190161215682615446396410129994711325892805940645283340607450605814292242214864807736616366963263577528008848595692050559543833562773099105599921926111989644313805176327043401089098178374013481826785604433013115807761169019821323105749924330624040032176017861852570855402207200857443405652874317191870029368327928342025351921450031345537488126081651667235334445880387925966636557003621257916083406394805099048054073995341365825171820787167114782405887040220243880710430205621846850135276025997937602400232787790214801902431955984378", null, out BigInteger value);
                    //value++;


                    //Pass the data to ENCRYPT, the public key information 
                    //(using RSACryptoServiceProvider.ExportParameters(false),
                    //and a boolean flag specifying no OAEP padding.
                    //encryptedData = RSAEncrypt(dataToEncrypt, RSA.ExportParameters(false), false);
                    //encryptedData = RSAEncrypt(dataToEncrypt, parameters, false);

                    //Pass the data to DECRYPT, the private key information 
                    //(using RSACryptoServiceProvider.ExportParameters(true),
                    //and a boolean flag specifying no OAEP padding.
                    //decryptedData = RSADecrypt(encryptedData, RSA.ExportParameters(true), false);
                    //decryptedData = RSADecrypt(encryptedData, parameters, false);

                    //Display the decrypted plaintext to the console. 
                    //Console.WriteLine("Decrypted plaintext: {0}", ByteConverter.GetString(decryptedData));
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
            {
                n = -n;
            }

            if (a < 0)
            {
                a = n - (-a % n);
            }

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

        private static byte[] CopyAndReverse(byte[] data)
        {
            byte[] reversed = new byte[data.Length];
            Array.Copy(data, 0, reversed, 0, data.Length);
            Array.Reverse(reversed);
            return reversed;
        }



        private static BigInteger Encrypt(BigInteger m, BigInteger n, BigInteger e)
        {
            return BigInteger.ModPow(m, e, n);
        }

        private static BigInteger Decrypt(BigInteger mEnc, BigInteger d, BigInteger n)
        {
            return BigInteger.ModPow(mEnc, d, n);
        }
    }
}
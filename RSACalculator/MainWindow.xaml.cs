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
using System.Collections.Specialized;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace RSACalculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static List<int> primes = new List<int>() { 2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37, 41, 43, 47, 53, 59, 61, 67, 71, 73, 79, 83, 89, 97, 101, 
            103, 107, 109, 113, 127, 131, 137, 139, 149, 151, 157, 163, 167, 173, 179, 181, 191, 193, 197, 199, 211, 223, 227, 229, 233, 239, 241, 251, 257, 
            263, 269, 271, 277, 281, 283, 293, 307, 311, 313, 317, 331, 337, 347, 349, 353, 359, 367, 373, 379, 383, 389, 397, 401, 409, 419, 421, 431, 433, 
            439, 443, 449, 457, 461, 463, 467, 479, 487, 491, 499, 503, 509, 521, 523, 541 };


        public MainWindow()
        {
            InitializeComponent();

            try
            {
                //BigInteger n = BigInteger.Parse("84641628266040968134726266858994621642842114704041843715900121029779624800354442181024933538554572539172519633298583132480574427234887384864237862510536135711979678422472297405718744900411759436052738392404109005261750528594360602400820678382015390265136033037813004859176942808586218232493968301780230653856757977924165795192647059713220241170741461160395394356567033643278968175549740103490059728058303074181319743641739740188742499285217435763085176127055686256451491791548801388560202149363758975803673725819798718410301085155062206092370667133063831263112034304132340281237240943617970645337927088718346956199378919");
                //BigInteger e = BigInteger.Parse("65537");
                //BigInteger m = StringToNumber("{Mother taught us patience, the virtues of restraint.}");
                //var c = Encrypt(m, e, n);

                TryDecrypt2();


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
        public static void TryDecrypt1()
        {
            try
            {
                string nString = "84641628266040968134726266858994621642842114704041843715900121029779624800354442181024933538554572539172519633298583132480574427234887384864237862510536135711979678422472297405718744900411759436052738392404109005261750528594360602400820678382015390265136033037813004859176942808586218232493968301780230653856757977924165795192647059713220241170741461160395394356567033643278968175549740103490059728058303074181319743641739740188742499285217435763085176127055686256451491791548801388560202149363758975803673725819798718410301085155062206092370667133063831263112034304132340281237240943617970645337927088718346956199378919";
                string eString = "65537";
                string cString = "76578687539749270334567433327419068016846482753554386520492865010545019207873571124020033190161215682615446396410129994711325892805940645283340607450605814292242214864807736616366963263577528008848595692050559543833562773099105599921926111989644313805176327043401089098178374013481826785604433013115807761169019821323105749924330624040032176017861852570855402207200857443405652874317191870029368327928342025351921450031345537488126081651667235334445880387925966636557003621257916083406394805099048054073995341365825171820787167114782405887040220243880710430205621846850135276025997937602400232787790214801902431955984378";

                List<int> primes = new List<int>() { 1399, 6779, 13183, 13297, 22469, 33721, 37993, 38569, 42359, 45307, 55001, 63353, 81373, 87179, 97397, 105929,
                    108223, 110479, 112757, 136189, 145303, 150893, 181273, 183473, 191929, 201961, 204133, 206821, 208511, 221581, 232877, 245071, 258023, 276083, 304961, 319427,
                    324931, 356803, 387449, 402343, 412637, 442517, 453671, 455809, 468491, 483853, 488947, 493049, 498053, 507029, 524269, 535103, 539293, 562019, 576899, 587173,
                    588871, 613747, 629281, 654803, 666013, 694079, 703561, 717047, 725099, 746129, 749893, 751021, 762599, 794077, 805537, 824419, 839633, 840611, 873497, 874091,
                    880723, 892237, 904027, 911219, 932683, 933973, 974977, 1016927, 1017131, 1026251, 1033987, 1037497, 1056401, 1089943, 1092043, 1099783, 1108049, 1158683,
                    1159777, 1162877, 1180819, 1183121, 1185791, 1189231, 1203661, 1209347, 1228631, 1256531, 1261627, 1267183, 1271671, 1287749, 1290161, 1292243, 1296929 };

                BigInteger n = BigInteger.Parse(nString);
                BigInteger e = BigInteger.Parse(eString);
                BigInteger c = BigInteger.Parse(cString);

                BigInteger f_n = 1;
                foreach(var prime in primes)
                    f_n *= prime - 1;

                BigInteger d = ModInverse(e, f_n);

                var m = Decrypt(c, d, n);
                string result = NumberToString(m);
                MessageBox.Show(result);
            }
            catch (ArgumentNullException)
            {
                //Catch this exception in case the encryption did not succeed.
                Console.WriteLine("routine failed.");
            }
        }

        public static string TryDecrypt2()
        {
            try
            {
                string result = "not executed";

                string nString = "18784930108456612406607814982869320937100357623565293065520882099446807661280672536412900261980252211590001129415416692842708423735233445954982126337452630953642389398728359447338862517090153799904393322766932296070412642108354965913688213429893600565940214997335063029350129915211361225154608407197811535838113388016141398750885113342334046258520069354875722557429638792502313832152491728469179017286106563712702581115089781944984882457716639211444194974007013843331628459682686708234290546709545912155519705333002154730995212015948695451676666505480477641054802308304079577983185418820145281416757335093826951423117";
                string eString = "10706997089278343179826885210060827147463409493523510777431387685771671442851399238492089215565793886478149477207867523308649570870806078530234678604755861159531961701795254771192002888524222172978592793668157761774276617760632894679477733512701268203499385472869419715289158276662873644519187935785549641679751291116387452539507235136056210643936490424871113629840892783091758193454166139128577293272832880405477781482386734589325917451384947487161071406557141600567159179305543501912418793324336259009277406706410254025135571209187854192795599940361924030053346219417234844146017500403546598659553371609984800535467";
                string cString = "15197314651067757355946584379946834164931969541043120164078064085944642365278681324096893476214152328730756513591485443945964837192597487877625274868334436921605239988927931381505891228509512463939955833545400394889094131125180282146395678844330453848518996586269025617541770111167605227113505192399546564506354156746766953521298852057983986116832842122594930311561489326839768342790373109683646472371945983378497288646413872942727101686965536906488145794402081854686784352899478311196208601541472632958619970624634811042002621463373384931741642559063446141175096440251352453487199537100563902295374105432210649258622";

                BigInteger n = BigInteger.Parse(nString);
                BigInteger e = BigInteger.Parse(eString);
                BigInteger c = BigInteger.Parse(cString);


                //var res = CalcFraction(1, 1, (4, 1));
                //List<BigInteger> numbers1 = new List<BigInteger>() { 2, 3, 1, 4 };
                //var res = CalcContinuedFraction(numbers1);

                (BigInteger result, BigInteger remainder) divResult = new(0, 1);
                BigInteger a = e;
                BigInteger b = n;
                List<BigInteger> numbers = new List<BigInteger>();
                List<string> results = new List<string>();
                do
                {
                    divResult = ContinuedFraction(a, b);
                    numbers.Add(divResult.result);
                    if (divResult.remainder > 0)
                    {
                        a = b;
                        b = divResult.remainder;
                    }

                    var res = CalcContinuedFraction(numbers);
                    BigInteger d = res.denominator;
                    var m = Decrypt(c, d, n);
                    result = NumberToString(m);

                    if (result[0] >= 0 && result[0] <= 255 &&
                        result[1] >= 0 && result[1] <= 255 &&
                        result[2] >= 0 && result[2] <= 255 &&
                        result[3] >= 0 && result[3] <= 255 &&
                        result[4] >= 0 && result[4] <= 255 &&
                        result[5] >= 0 && result[5] <= 255 &&
                        result[6] >= 0 && result[6] <= 255 &&
                        result[7] >= 0 && result[7] <= 255 &&
                        result[8] >= 0 && result[8] <= 255 &&
                        result[9] >= 0 && result[9] <= 255 &&
                        result[10] >= 0 && result[10] <= 255)
                    {
                        results.Add(result);
                        results.Add(d.ToString());
                    }

                } while (divResult.remainder != 0);

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

        public static (BigInteger result, BigInteger remainder) ContinuedFraction(BigInteger a, BigInteger b)
        {
            var result = a / b;
            var remainder = a % b;

            return (result, remainder);
        }

        // υπολογισμός του a1 + b1/b2 σε κλάσμα
        public static (BigInteger numerator, BigInteger denominator) CalcFraction(BigInteger a1, BigInteger b1, BigInteger b2)
        {
            BigInteger numerator, denominator;

            if (b1 % b2 == 0)
            {
                numerator = a1 + b1 / b2;
                denominator = 1;

                return (numerator, denominator);
            }
            else
            {
                numerator = a1 * b2;
                numerator = numerator + b1;
                denominator = b2;

                return (numerator, denominator);
            }
        }

        // υπολογισμός του a1 + b1/(b2/b3) σε κλάσμα
        public static (BigInteger numerator, BigInteger denominator) CalcFraction(BigInteger a1, BigInteger b1, (BigInteger b2, BigInteger b3) c)
        {
            BigInteger numerator, denominator;

            b1 = b1 * c.b3;

            if (b1 % c.b2 == 0)
            {
                numerator = a1 + b1 / c.b2;
                denominator = 1;

                return (numerator, denominator);
            }
            else
            {
                numerator = a1 * c.b2;
                numerator = numerator + b1;
                denominator = c.b2;

                return (numerator, denominator);
            }
        }

        // υπολογισμός μιας ContinuedFraction
        public static (BigInteger numerator, BigInteger denominator) CalcContinuedFraction(List<BigInteger> numbers)
        {
            (BigInteger numerator, BigInteger denominator) result = new (0, 0);
            for (var i = numbers.Count-1; i >= 0; i--)
            {
                if (i == numbers.Count - 1)
                {
                    result = CalcFraction(numbers[i], 0, (1, 1));
                }
                else
                {
                    result = CalcFraction(numbers[i], 1, (result.numerator, result.denominator));
                }
            }
            return result;
        }

        public static BigInteger Sqrt(BigInteger n)
        {
            if (n == 0) return 0;
            if (n > 0)
            {
                int bitLength = Convert.ToInt32(Math.Ceiling(BigInteger.Log(n, 2)));
                BigInteger root = BigInteger.One << (bitLength / 2);

                while (!isSqrt(n, root))
                {
                    root += n / root;
                    root /= 2;
                }

                return root;
            }

            throw new ArithmeticException("NaN");
        }

        private static bool isSqrt(BigInteger n, BigInteger root)
        {
            BigInteger lowerBound = root * root;
            BigInteger upperBound = (root + 1) * (root + 1);

            return (n >= lowerBound && n < upperBound);
        }

        public static string TryFullDecrypt(string text, BigInteger p, BigInteger q, BigInteger e)
        {
            try
            {
                // encryption
                BigInteger m = StringToNumber(text);
                BigInteger n = p * q;

                if (m >= n)
                    throw new Exception("m is bigger than n");

                BigInteger f_n = (p - 1) * (q - 1);
                BigInteger d = ModInverse(e, f_n);
                var c = Encrypt(m, e, n);

                // encryption
                m = Decrypt(c, d, n);
                var result = NumberToString(m);

                throw new Exception(result);

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

        private static BigInteger Encrypt(BigInteger m, BigInteger e, BigInteger n)
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
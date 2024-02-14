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
        }

        private void Question1_Click(object sender, RoutedEventArgs e)
        {
            TryDecrypt1();
        }

        private void Question2_Click(object sender, RoutedEventArgs e)
        {
            TryDecrypt2();
        }


        public static void TryDecrypt1()
        {
            try
            {
                string nString = "84641628266040968134726266858994621642842114704041843715900121029779624800354442181024933538554572539172519633298583132480574427234887384864237862510536135711979678422472297405718744900411759436052738392404109005261750528594360602400820678382015390265136033037813004859176942808586218232493968301780230653856757977924165795192647059713220241170741461160395394356567033643278968175549740103490059728058303074181319743641739740188742499285217435763085176127055686256451491791548801388560202149363758975803673725819798718410301085155062206092370667133063831263112034304132340281237240943617970645337927088718346956199378919";
                string eString = "65537";
                string cString = "76578687539749270334567433327419068016846482753554386520492865010545019207873571124020033190161215682615446396410129994711325892805940645283340607450605814292242214864807736616366963263577528008848595692050559543833562773099105599921926111989644313805176327043401089098178374013481826785604433013115807761169019821323105749924330624040032176017861852570855402207200857443405652874317191870029368327928342025351921450031345537488126081651667235334445880387925966636557003621257916083406394805099048054073995341365825171820787167114782405887040220243880710430205621846850135276025997937602400232787790214801902431955984378";

                List<int> factors = new List<int>() { 1399, 6779, 13183, 13297, 22469, 33721, 37993, 38569, 42359, 45307, 55001, 63353, 81373, 87179, 97397, 105929,
                    108223, 110479, 112757, 136189, 145303, 150893, 181273, 183473, 191929, 201961, 204133, 206821, 208511, 221581, 232877, 245071, 258023, 276083, 304961, 319427,
                    324931, 356803, 387449, 402343, 412637, 442517, 453671, 455809, 468491, 483853, 488947, 493049, 498053, 507029, 524269, 535103, 539293, 562019, 576899, 587173,
                    588871, 613747, 629281, 654803, 666013, 694079, 703561, 717047, 725099, 746129, 749893, 751021, 762599, 794077, 805537, 824419, 839633, 840611, 873497, 874091,
                    880723, 892237, 904027, 911219, 932683, 933973, 974977, 1016927, 1017131, 1026251, 1033987, 1037497, 1056401, 1089943, 1092043, 1099783, 1108049, 1158683,
                    1159777, 1162877, 1180819, 1183121, 1185791, 1189231, 1203661, 1209347, 1228631, 1256531, 1261627, 1267183, 1271671, 1287749, 1290161, 1292243, 1296929 };

                BigInteger n = BigInteger.Parse(nString);
                BigInteger e = BigInteger.Parse(eString);
                BigInteger c = BigInteger.Parse(cString);

                BigInteger f_n = 1;
                foreach(var prime in factors)
                    f_n *= prime - 1;

                BigInteger d = ModInverse(e, f_n);

                var m = Decrypt(c, d, n);
                string result = NumberToString(m);
                MessageBox.Show(result + "\n\nd = " + d.ToString());
            }
            catch (ArgumentNullException)
            {
                Console.WriteLine("routine failed.");
            }
        }

        public static void TryDecrypt2()
        {
            try
            {
                string nString = "18784930108456612406607814982869320937100357623565293065520882099446807661280672536412900261980252211590001129415416692842708423735233445954982126337452630953642389398728359447338862517090153799904393322766932296070412642108354965913688213429893600565940214997335063029350129915211361225154608407197811535838113388016141398750885113342334046258520069354875722557429638792502313832152491728469179017286106563712702581115089781944984882457716639211444194974007013843331628459682686708234290546709545912155519705333002154730995212015948695451676666505480477641054802308304079577983185418820145281416757335093826951423117";
                string eString = "10706997089278343179826885210060827147463409493523510777431387685771671442851399238492089215565793886478149477207867523308649570870806078530234678604755861159531961701795254771192002888524222172978592793668157761774276617760632894679477733512701268203499385472869419715289158276662873644519187935785549641679751291116387452539507235136056210643936490424871113629840892783091758193454166139128577293272832880405477781482386734589325917451384947487161071406557141600567159179305543501912418793324336259009277406706410254025135571209187854192795599940361924030053346219417234844146017500403546598659553371609984800535467";
                string cString = "15197314651067757355946584379946834164931969541043120164078064085944642365278681324096893476214152328730756513591485443945964837192597487877625274868334436921605239988927931381505891228509512463939955833545400394889094131125180282146395678844330453848518996586269025617541770111167605227113505192399546564506354156746766953521298852057983986116832842122594930311561489326839768342790373109683646472371945983378497288646413872942727101686965536906488145794402081854686784352899478311196208601541472632958619970624634811042002621463373384931741642559063446141175096440251352453487199537100563902295374105432210649258622";

                BigInteger n = BigInteger.Parse(nString);
                BigInteger e = BigInteger.Parse(eString);
                BigInteger c = BigInteger.Parse(cString);

                (BigInteger quotient, BigInteger remainder) divResult = new(0, 1);
                BigInteger a = e;
                BigInteger b = n;
                List<BigInteger> numbers = new List<BigInteger>();
                do
                {
                    divResult = Division(a, b);
                    numbers.Add(divResult.quotient);
                    if (divResult.remainder > 0)
                    {
                        a = b;
                        b = divResult.remainder;
                    }

                    var res = CalcContinuedFraction(numbers);
                    BigInteger d = res.denominator;
                    var m = Decrypt(c, d, n);
                    string result = NumberToString(m);

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
                        MessageBox.Show(result + "\n\nd = " + d.ToString());
                        break;
                    }

                } while (divResult.remainder != 0);

            }
            catch (ArgumentNullException)
            {
                Console.WriteLine("routine failed.");
            }
        }

        /// <summary>
        ///  Διαιρεί δύο ακέραιους και επιστρέφει το πηλίκο και το υπόλοιπο
        /// </summary>
        public static (BigInteger quotient, BigInteger remainder) Division(BigInteger a, BigInteger b)
        {
            var quotient = a / b;
            var remainder = a % b;

            return (quotient, remainder);
        }

        /// <summary>
        ///  υπολογισμός του a + b/(c1/c2) σε κλάσμα
        /// </summary>
        public static (BigInteger numerator, BigInteger denominator) CalcFraction(BigInteger a, BigInteger b, (BigInteger c1, BigInteger c2) c)
        {
            BigInteger numerator, denominator;

            b = b * c.c2;

            if (b % c.c1 == 0)
            {
                numerator = a + b / c.c1;
                denominator = 1;

                return (numerator, denominator);
            }
            else
            {
                numerator = a * c.c1;
                numerator = numerator + b;
                denominator = c.c1;

                return (numerator, denominator);
            }
        }

        /// <summary>
        ///  υπολογισμός μιας ContinuedFraction και επιστροφή της με μορφή κλάσματος
        /// </summary>
        public static (BigInteger numerator, BigInteger denominator) CalcContinuedFraction(List<BigInteger> numbers)
        {
            (BigInteger numerator, BigInteger denominator) result = new (0, 0);
            for (var i = numbers.Count-1; i >= 0; i--)
            {
                if (i == numbers.Count - 1)
                    result = CalcFraction(numbers[i], 0, (1, 1));
                else
                    result = CalcFraction(numbers[i], 1, (result.numerator, result.denominator));
            }
            return result;
        }

        /// <summary>
        /// Μετατρέπει ένα κείμενο σε πίνακα από bytes και τον πίνακα σε μεγάλο ακέραιο
        /// </summary>
        public static BigInteger StringToNumber(string text)
        {
            var bytes = Encoding.UTF8.GetBytes(text);
            Array.Reverse(bytes);
            var number = new BigInteger(bytes);

            return number;
        }

        /// <summary>
        /// Μετατρέπει ένα μεγάλο ακέραιο σε πίνακα από bytes και τον πίνακα σε κείμενο
        /// </summary>
        public static string NumberToString(BigInteger number)
        {
            byte[] bytes = number.ToByteArray();
            Array.Reverse(bytes);
            var text = Encoding.Default.GetString(bytes);

            return text;
        }

        /// <summary>
        /// Κρυπτογραφεί ένα μήνυμα με τον RSA αλγόριθμο
        /// </summary>
        private static BigInteger Encrypt(BigInteger m, BigInteger e, BigInteger n)
        {
            return BigInteger.ModPow(m, e, n);
        }

        /// <summary>
        /// Αποκρυπτογραφεί ένα μήνυμα με τον RSA αλγόριθμο
        /// </summary>
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

    
    }
}
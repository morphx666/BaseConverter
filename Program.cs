namespace BaseConverter {
    // https://www.rapidtables.com/convert/number/base-converter.html
    public class BaseConverter {
        public static void Main(string[] argv) {
            if(argv.Length != 3) {
                Console.WriteLine(
                                $"""
                                BaseConverter {System.Reflection.Assembly.GetEntryAssembly()?.GetName().Version}

                                Usage:
                                    bc value fromBase toBase

                                    value: the numeric value to convert
                                    fromBase: the base in which the value is expressed
                                    toBase: the desired target base

                                Example:
                                    > bc 37 5 9
                                        (convert 37 from base 5 to base 9)
                                    > 37₅ = 24₉
                                """);
                Environment.Exit(1);
            }

            string v1 = argv[0];
            int fb = int.Parse(argv[1]);
            int tb = int.Parse(argv[2]);
            
            string result = Convert(v1, fb, tb);

            //Console.WriteLine($"{v1} b{fb} = {result} b{tb}");
            string r = $"{v1}|{fb}| = {result}|{tb}|";
            bool isBase = false;
            for(int i = 0; i < r.Length; i++) {
                if(r[i] == '|') {
                    isBase = !isBase;
                    continue;
                }
                // http://www.unicode.org/charts/PDF/U2070.pdf
                Console.Write(isBase ? char.ConvertFromUtf32(0x2080 + (r[i] - 48)) : r[i]);
            }
        }

        private static string Convert(string value, int fromBase, int toBase) {
            UInt64 b10 = ToBase10(value, fromBase);
            string result = FromBase10(b10, toBase);
            return result;
        }

        private static string FromBase10(UInt64 value, int toBase) {
            string res = "";
            UInt64 b = (UInt64)toBase;
            while(value > 0) {
                UInt64 r = (UInt64)(value % b);
                res = (char)((r >= 10 ? r + 7 : r) + 48) + res;
                value = (UInt64)Math.Floor((decimal)(value / b));
            }
            
            return res;
        }

        private static UInt64 ToBase10(string value, int fromBase) {
            UInt64 res = 0;
            int len = value.Length - 1;
            for(int i = 0; i <= len; i++) {
                int d = (int)value[i] - 48;
                if(d >= fromBase) throw new ArgumentException($"{value} is not representable in base {fromBase}");
                res += (UInt64)((d >= 10 ? d - 7 : d) * Math.Pow(fromBase, len - i));
            }

            return res;
        }
    }
}
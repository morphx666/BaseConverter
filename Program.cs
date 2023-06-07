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

                                    value: The numeric value to convert
                                           It can also be defined as a range of values: a-b
                                    fromBase: The base in which the value is expressed
                                    toBase: The desired target base

                                Example:
                                    q> bc 37 5 9
                                        (convert 37 from base 5 to base 9)
                                    r> 37₅ = 24₉

                                    q> bc 7-12 11 3
                                        (convert all values from 7 to 12 from base 11 to base 3)
                                    r> 7₁₁ = 21₃
                                    r> 8₁₁ = 22₃
                                    r> 9₁₁ = 100₃
                                    r> A₁₁ = 101₃
                                    r> 10₁₁ = 102₃
                                    r> 11₁₁ = 110₃
                                    r> 12₁₁ = 111₃
                                """);
                Environment.Exit(1);
            }

            int fb = int.Parse(argv[1]);
            int tb = int.Parse(argv[2]);
            string v = argv[0].ToUpper();

            if(v.Contains('-')) {
                UInt64[] vs = v.Split('-').Select(x => ToBase10(x, fb)).ToArray();
                for(UInt64 i = vs[0]; i <= vs[^1]; i++) {
                    v = FromBase10(i, fb);
                    PrintConversion(v, fb, tb);
                }
            } else {
                PrintConversion(v, fb, tb);
            }
        }

        private static void PrintConversion(string v, int fb, int tb) {
            string result = Convert(v, fb, tb);

            //Console.WriteLine($"{v1} b{fb} = {result} b{tb}");
            string r = $"{v}|{fb}| = {result}|{tb}|";
            bool isBase = false;
            for(int i = 0; i < r.Length; i++) {
                if(r[i] == '|') {
                    isBase = !isBase;
                    continue;
                }
                // http://www.unicode.org/charts/PDF/U2070.pdf
                Console.Write(isBase ? char.ConvertFromUtf32(0x2080 + (r[i] - 48)) : r[i]);
            }
            Console.WriteLine();
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
            
            return res == "" ? "0" : res;
        }

        private static UInt64 ToBase10(string value, int fromBase) {
            UInt64 res = 0;
            int len = value.Length - 1;
            for(int i = 0; i <= len; i++) {
                int d = (int)value[i] - 48;
                d = d >= 10 ? d - 7 : d;
                if(d >= fromBase) throw new ArgumentException($"Input value '{value[i]}' at position '{len - i}' not representable in base {fromBase}");
                UInt64 r = (UInt64)(d * Math.Pow(fromBase, len - i));
                res += r;
            }

            return res;
        }
    }
}
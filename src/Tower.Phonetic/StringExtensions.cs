using System.Text;

namespace Tower.Phonetic
{
    public static class StringExtensions
    {
        public static string ToPhonetic(this string s)
        {
            if (string.IsNullOrEmpty(s))
                return "";

            var sb = new StringBuilder();
            foreach (var c in s)
                sb.Append((sb.Length > 0 ? " " : "") + c.ToPhonetic());

            return sb.ToString();
        }

        public static string FromPhonetic(this string s)
        {
            switch (s.ToLower())
            {
                case "alpha":
                    return "A";

                case "bravo":
                    return "B";

                case "charlie":
                    return "C";

                case "delta":
                    return "D";

                case "echo":
                    return "E";

                case "foxtrot":
                    return "F";

                case "golf":
                    return "G";

                case "hotel":
                    return "H";

                case "india":
                    return "I";

                case "juliet":
                    return "J";

                case "kilo":
                    return "K";

                case "lima":
                    return "L";

                case "mike":
                    return "M";

                case "november":
                    return "N";

                case "oscar":
                    return "O";

                case "papa":
                    return "P";

                case "quebec":
                    return "Q";

                case "romeo":
                    return "R";

                case "sierra":
                    return "S";

                case "tango":
                    return "T";

                case "uniform":
                    return "U";

                case "victor":
                    return "V";

                case "whiskey":
                    return "W";

                case "x. ray":
                case "x-ray":
                    return "X";

                case "yankee":
                    return "Y";

                case "zulu":
                    return "Z";

                default:
                    return "";
            }
        }
    }
}

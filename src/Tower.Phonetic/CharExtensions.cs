namespace Tower.Phonetic
{
    public static class CharExtensions
    {
        public static string ToPhonetic(this char c)
        {
            switch (c)
            {
                case 'A':
                    return "Alpha";

                case 'B':
                    return "Bravo";

                case 'C':
                    return "Charlie";

                case 'D':
                    return "Delta";

                case 'E':
                    return "Echo";

                case 'F':
                    return "Foxtrot";

                case 'G':
                    return "Golf";

                case 'H':
                    return "Hotel";

                case 'I':
                    return "India";

                case 'J':
                    return "Juliet";

                case 'K':
                    return "Kilo";

                case 'L':
                    return "Lima";

                case 'M':
                    return "Mike";

                case 'N':
                    return "November";

                case 'O':
                    return "Oscar";

                case 'P':
                    return "Papa";

                case 'Q':
                    return "Quebec";

                case 'R':
                    return "Romeo";

                case 'S':
                    return "Sierra";

                case 'T':
                    return "Tango";

                case 'U':
                    return "Uniform";

                case 'V':
                    return "Victor";

                case 'W':
                    return "Whiskey";

                case 'X':
                    return "X-ray";

                case 'Y':
                    return "Yankee";

                case 'Z':
                    return "Zulu";

                case '0':
                    return "Zero";

                case '1':
                    return "One";

                case '2':
                    return "Two";

                case '3':
                    return "Three";

                case '4':
                    return "Four";

                case '5':
                    return "Five";

                case '6':
                    return "Six";

                case '7':
                    return "Seven";

                case '8':
                    return "Eight";

                case '9':
                    return "Niner";

                case '.':
                    return "Point";

                default:
                    return "";
            }
        }        
    }
}


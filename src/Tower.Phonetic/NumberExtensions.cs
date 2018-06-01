namespace Tower.Phonetic
{
    public static class NumberExtensions
    {
        public static string ToPhonetic(this int n)
        {
            return n.ToString().ToPhonetic();
        }
    }
}

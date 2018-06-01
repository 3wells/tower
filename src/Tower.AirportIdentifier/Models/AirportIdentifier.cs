using System.Text;
using Tower.Phonetic;

namespace Tower.AirportIdentifier.Models
{
    public class AirportIdentifier
    {
        public string Icao { get; set; }
        public string Name { get; set; }

        public AirportIdentifier(string icao, string name)
        {
            Icao = icao;
            Name = name;
        }

        public string Letters
        {
            get
            {
                if (string.IsNullOrEmpty(Icao))
                    return "";

                var s = new StringBuilder();
                foreach (var c in Icao)
                    s.Append((s.Length > 0 ? " " : "") + c);

                return s.ToString();
            }
        }

        public string Phonetic => string.IsNullOrEmpty(Icao) ? "" : Icao.ToPhonetic();
    }
}
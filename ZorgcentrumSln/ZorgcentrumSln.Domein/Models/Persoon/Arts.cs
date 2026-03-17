using ZorgcentrumSln.Domein.Exceptions;
using static System.Net.WebRequestMethods;

namespace ZorgcentrumSln.Domein.Models.Persoon;

internal class Arts : Persoon
{
    public Arts(string rijksregisterNummer, string naam, string voornaam, string riziv, string specialisatie, HashSet<string> afdelingen) : base(rijksregisterNummer, naam, voornaam)
    {
        RIZIV = riziv;
        Specialisatie = specialisatie;
        Afdelingen = afdelingen;
    }

    private static readonly List<string> SpecialisatieCodes = [
        "ANES",
        "BIOL",
        "CARD",
        "CHIR",
        "DERM",
        "ENDO",
        "GAST",
        "GERC",
        "GYNA",
        "HEMA",
        "NCHI",
        "NEFR",
        "NEUR",
        "NUCL",
        "ONCO",
        "ORL",
        "OPHT",
        "ORTH",
        "PAAN",
        "PEDI",
        "PNEU",
        "PSYC",
        "RADO",
        "REUM",
        "REVA",
        "RXDI",
        "SLAP",
        "STOM",
        "URGE",
        "UROL"
        ];

    public string RIZIV { get;
        init
        {
            try
            {
                string nummer;
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException(nameof(value), $"RIZIV {value} niet null of leeg zijn");
                }
                else if ((nummer = value.Replace("-", "")).Length == 11)
                {
                    // Controleer of het nummer alleen cijfers bevat
                    long.Parse(nummer);
                }
            }
            catch (Exception e)
            {
                throw new PersoonException("Er liep iets fout met het maken van deze persoon", e);
            }

            field = value;
        }
    }

    public string Specialisatie { get;
        init
        {
            try
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException(nameof(value), "Specialisatie  niet null of leeg zijn");
                }
                else if (!SpecialisatieCodes.Contains(value.Replace("}", "").Replace("{", "")))
                {
                    throw new ArgumentOutOfRangeException(nameof(value), $"Dit is geen geldige waarde {value}");
                }
            }
            catch (Exception e)
            {
                throw new PersoonException("Er liep iets fout met het maken van deze persoon", e);
            }

            field = value;
        }
    }

    public HashSet<string> Afdelingen { 
        get; init => field = CheckAfdelingCodes(value).ToHashSet();
    }

    public override bool Equals(object? obj) => obj is Arts arts && base.Equals(arts) && arts.RIZIV == RIZIV;

    public override int GetHashCode() => HashCode.Combine(base.GetHashCode(), RIZIV);
}

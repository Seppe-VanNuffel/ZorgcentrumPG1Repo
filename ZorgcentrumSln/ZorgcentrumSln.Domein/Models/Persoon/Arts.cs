using ZorgcentrumSln.Domein.Exceptions;

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
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException(nameof(value), "Specialisatie niet null of leeg zijn");
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
                    throw new ArgumentNullException(nameof(value), "Specialisatie niet null of leeg zijn");
                }
                else if (!SpecialisatieCodes.Contains(value.Replace("}", "").Replace("{", "")))
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "Dit is geen geldige waarde");
                }
            }
            catch (Exception e)
            {
                throw new PersoonException("Er liep iets fout met het maken van deze persoon", e);
            }

            field = value;
        }
    }

    public HashSet<string> Afdelingen { get;
        init
        {
            foreach (var item in value)
            {
                if (string.IsNullOrWhiteSpace(item))
                {
                    try
                    {
                        if (string.IsNullOrWhiteSpace(item))
                        {
                            throw new ArgumentNullException(nameof(value), "Alle waarden in de collectie mogen niet null of leeg zijn");
                        }
                        else if (!AfdelingCodes.Contains(item.Replace("}", "").Replace("{", "")))
                        {
                            throw new ArgumentOutOfRangeException(nameof(value), "Dit is geen geldige waarde");
                        }
                    }
                    catch (Exception e)
                    {
                        throw new PersoonException("Er liep iets fout met het maken van deze persoon", e);
                    }
                }
            }

            field = value;
        }
    }
}

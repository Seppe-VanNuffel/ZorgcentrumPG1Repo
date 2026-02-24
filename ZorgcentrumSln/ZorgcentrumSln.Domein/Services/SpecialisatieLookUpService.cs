using System;
using System.Collections.Generic;
using System.Text;

namespace ZorgcentrumSln.Domein.Services;

public class SpecialisatieLookUpService
{
    private static readonly Dictionary<string, string> _specialisaties = new()
    {
        { "ANES", "Anesthesie‑reanimatie" },
        { "BIOL", "Klinische biologie" },
        { "CARD", "Cardiologie" },
        { "CHIR", "Heelkunde" },
        { "DERM", "Dermatologie" },
        { "ENDO", "Endocrino‑diabetologie" },
        { "GAST", "Gastro‑enterologie" },
        { "GERC", "Geriatrie" },
        { "GYNA", "Gynaecologie & verloskunde" },
        { "HEMA", "Klinische hematologie" },
        { "NCHI", "Neurochirurgie" },
        { "NEFR", "Nefrologie" },
        { "NEUR", "Neurologie" },
        { "NUCL", "Nucleaire geneeskunde" },
        { "ONCO", "Medische oncologie" },
        { "ORL", "Oto-Rhino-Laryngologie" },
        { "OPHT", "Oftalmologie" },
        { "ORTH", "Orthopedische heelkunde" },
        { "PAAN", "Pathologische anatomie" },
        { "PEDI", "Kindergeneeskunde" },
        { "PNEU", "Pneumologie" },
        { "PSYC", "Psychiatrie" },
        { "RADO", "Radiotherapie‑oncologie" },
        { "REUM", "Reumatologie" },
        { "REVA", "Fysische geneeskunde & revalidatie" },
        { "RXDI", "Röntgendiagnose" },
        { "SLAP", "Slaapgeneeskunde" },
        { "STOM", "Stomatologie" },
        { "URGE", "Urgentiegeneeskunde" },
        { "UROL", "Urologie" }
    };

    public static string VraagNaamVanCode(string code)
    {
        return _specialisaties[code];
    }
}

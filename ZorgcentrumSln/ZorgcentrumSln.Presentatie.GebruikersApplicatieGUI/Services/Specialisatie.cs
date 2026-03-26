namespace ZorgcentrumSln.Domein.Models;

internal class Specialisatie
{
    public string Code { get; set; }
    public string Naam { get; set; }

    public Specialisatie(string code)
    {
        Code = code;
        Naam = VraagNaam();
    }

    /// <summary>
    /// Converteert de Code naar de Display naam van de code
    /// </summary>
    /// <returns>Display naam</returns>
    /// <exception cref="NotImplementedException"></exception>
    private string VraagNaam()
    {
        return Code switch
        {
            "ANES" => "Anesthesie‑reanimatie",
            "BIOL" => "Klinische biologie",
            "CARD" => "Cardiologie",
            "CHIR" => "Heelkunde",
            "DERM" => "Dermatologie",
            "ENDO" => "Endocrino‑diabetologie",
            "GAST" => "Gastro‑enterologie",
            "GERC" => "Geriatrie",
            "GYNA" => "Gynaecologie & verloskunde",
            "HEMA" => "Klinische hematologie",
            "NCHI" => "Neurochirurgie",
            "NEFR" => "Nefrologie",
            "NEUR" => "Neurologie",
            "NUCL" => "Nucleaire geneeskunde",
            "ONCO" => "Medische oncologie",
            "ORL"  => "Oto-Rhino-Laryngologie",
            "OPHT" => "Oftalmologie",
            "ORTH" => "Orthopedische heelkunde",
            "PAAN" => "Pathologische anatomie",
            "PEDI" => "Kindergeneeskunde",
            "PNEU" => "Pneumologie",
            "PSYC" => "Psychiatrie",
            "RADO" => "Radiotherapie‑oncologie",
            "REUM" => "Reumatologie",
            "REVA" => "Fysische geneeskunde & revalidatie",
            "RXDI" => "Röntgendiagnose",
            "SLAP" => "Slaapgeneeskunde",
            "STOM" => "Stomatologie",
            "URGE" => "Urgentiegeneeskunde",
            "UROL" => "Urologie",
            _ => throw new NotImplementedException(),
        };
    }
}

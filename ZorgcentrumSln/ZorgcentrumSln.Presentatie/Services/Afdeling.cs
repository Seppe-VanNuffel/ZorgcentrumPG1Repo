using System;

namespace ZorgcentrumSln.Domein.Models;

internal class Afdeling(string code)
{
    public string Code { get; } = code;

    /// <summary>
    /// Converteert de Code naar de Display naam van de code
    /// </summary>
    /// <returns>Display naam</returns>
    /// <exception cref="NotImplementedException"></exception>
    public string VraagNaam()
    {
        return Code switch
        {
            "PNEC" => "Pneumologie – Slaapcentrum",
            "CARD" => "Cardiologie‑afdeling",
            "GAST" => "Gastro‑enterologie afdeling",
            "NEUR" => "Neurologie afdeling",
            "ORTH" => "Orthopedie afdeling",
            "GERC" => "Geriatrie afdeling",
            "ONCO" => "Oncologie afdeling",
            "PAAN" => "Pathologische anatomie",
            "RADI" => "Radiologie / MRI / CT",
            "URGE" => "Spoed & urgentiegeneeskunde",
            _ => throw new NotImplementedException(),
        };
    }
}

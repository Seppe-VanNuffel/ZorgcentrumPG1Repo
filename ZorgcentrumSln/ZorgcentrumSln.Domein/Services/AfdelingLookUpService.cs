namespace ZorgcentrumSln.Domein.Services;

internal class AfdelingLookUpService
{
    private static readonly Dictionary<string, string> _afdeling = new()
    {
        { "PNEC", "Pneumologie – Slaapcentrum" },
        { "CARD", "Cardiologie‑afdeling" },
        { "GAST", "Gastro‑enterologie afdeling" },
        { "NEUR", "Neurologie afdeling" },
        { "ORTH", "Orthopedie afdeling" },
        { "GERC", "Geriatrie afdeling" },
        { "ONCO", "Oncologie afdeling" },
        { "PAAN", "Pathologische anatomie" },
        { "RADI", "Radiologie / MRI / CT" },
        { "URGE", "Spoed & urgentiegeneeskunde" }
    };

    public static string VraagNaamVanCode(string code)
    {
        return _afdeling[code];
    }
}

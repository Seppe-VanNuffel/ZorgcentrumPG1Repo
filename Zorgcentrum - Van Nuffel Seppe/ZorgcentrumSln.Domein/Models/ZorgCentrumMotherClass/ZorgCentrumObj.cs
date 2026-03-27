using System;
using System.Collections.Generic;
using System.Text;

namespace ZorgcentrumSln.Domein.Models.ZorgCentrumMotherClass;

internal class ZorgCentrumObj
{
    protected static readonly List<string> AfdelingCodes = [
        "PNEC",
        "CARD",
        "GAST",
        "NEUR",
        "ORTH",
        "GERC",
        "ONCO",
        "PAAN",
        "RADI",
        "URGE"
        ];
}

using System;
using System.Collections.Generic;
using System.Text;
using ZorgcentrumSln.Domein.Interfaces;
using ZorgcentrumSln.PersistentieBestand.BestandsBeheer;

namespace ZorgcentrumSln.Persistentie.Bestand;

public class ErrorLoggerBestand : IErrorLogging
{
    private string _pad;

    public ErrorLoggerBestand(string padNaarBestand)
    {
        _pad = padNaarBestand;
    }

    public void LogError(Exception e)
    {
        BestandsVerwerking.SchrijfErrorBestand(_pad, e);
    }
}

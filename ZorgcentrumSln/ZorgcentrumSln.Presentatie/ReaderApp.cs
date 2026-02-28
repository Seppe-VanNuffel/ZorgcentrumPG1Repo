using System;
using System.Collections.Generic;
using System.Text;
using ZorgcentrumSln.Domein;

namespace ZorgcentrumSln.Presentatie;

public class ReaderApp(DomeinController dm)
{
    private DomeinController _dm = dm;

    public void Run()
    {
        Console.WriteLine(string.Join("\n", _dm.GetAllPersonen()));
    }
}

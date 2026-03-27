using System;
using System.Collections.Generic;
using System.Text;
using ZorgcentrumSln.Domein.Exceptions;
using ZorgcentrumSln.Domein.Models.Afspraak;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ZorgcentrumSln.Domein.Tests.Models.Afspraken;

public class ControleTest
{
    [Fact]
    public void TEST_CONTROLE_CTOR()
    {
        var controle = new Controle(1, DateTime.Now, 20, "63.01.02-359.38", "15.01.23-042.07", "GAST", 2);

        Assert.Equal(2, controle.Referentie);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(-27)]
    public void TEST_REFERENTIE_ALS_FOUT(int num)
    {
        Assert.Throws<AfspraakException>(() => new Controle(1, DateTime.Now, 20, "63.01.02-359.38", "15.01.23-042.07", "GAST", num));
    }
}
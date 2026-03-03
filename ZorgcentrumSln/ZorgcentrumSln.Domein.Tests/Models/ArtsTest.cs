using ZorgcentrumSln.Domein.Models.Persoon;

namespace ZorgcentrumSln.Domein.Tests.Models;

public class ArtsTest
{
    [Fact]
    public void TEST_ARTSCONSTRUCTOR()
    {
        Arts persoon = new("61.08.05-432.76", "ABDUL-LATIF", "KHALID", "1-79010-52-100", "{ANES}", ["{ORTH}"]);

        Assert.Equal("61.08.05-432.76", persoon.RijksregisterNummer);
        Assert.Equal("ABDUL-LATIF", persoon.Naam);
        Assert.Equal("KHALID", persoon.Voornaam);
        Assert.Equal("1-79010-52-100", persoon.RIZIV);
        Assert.Equal("{ANES}", persoon.Specialisatie);
        Assert.Equal(["{ORTH}"], persoon.Afdelingen);
    }

    [Theory]
    [InlineData("61.08.05-432.76")]
    [InlineData("69.08.06-893.44")]
    [InlineData("96.05.02-114.34")]
    [InlineData("73.12.28-670.10")]
    [InlineData("57.04.30-901.06")]
    public void TEST_RIJKSREGISTERNUMMER(string rijksregisterNummer)
    {
        var nummer = Persoon.CheckRijksregisterNummer(rijksregisterNummer);

        Assert.Equal(rijksregisterNummer, nummer);
    }

    [Theory]
    [InlineData("61.08.05-4.76")]
    [InlineData("90.05.15-12.44")]
    [InlineData("88.10.20-555.6")]
    [InlineData("75.12.01-4321.88")]
    [InlineData("99.01.01-123.456")]
    public void TEST_RIJKSREGISTERNUMMER_ALS_TEKORT_OF_TELANG(string rijksregisterNummer)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => Persoon.CheckRijksregisterNummer(rijksregisterNummer));
    }

    [Theory]
    [InlineData("61.08.05-431.76")]
    [InlineData("74.02.14-123.88")]
    [InlineData("92.11.30-456.12")]
    [InlineData("85.02.30-215.44")]
    [InlineData("99.13.01-102.35")]
    public void TEST_RIJKSREGISTERNUMMER_ALS_FOUT_NUMMER(string rijksregisterNummer)
    {
        Assert.Throws<ArgumentException>(() => Persoon.CheckRijksregisterNummer(rijksregisterNummer));
    }
}

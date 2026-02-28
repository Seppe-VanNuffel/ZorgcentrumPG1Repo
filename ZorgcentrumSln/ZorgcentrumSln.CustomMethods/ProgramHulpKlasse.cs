namespace CustomMethods;

public class ProgramHulpKlasse
{
    /// <summary>
    /// Checked ofdat een getal binnen bereik ligt
    /// </summary>
    /// <param name="nummer">Het nummer dat wordt gechecked</param>
    /// <param name="bereik">het bereik</param>
    /// <returns>true als het getal binnen het bereik ligt, anders false</returns>
    public static bool IsNummerBinnenBereik(int nummer, (int min, int max) bereik)
    {
        if(nummer < bereik.max && nummer > bereik.min)
            return true;
        return false;
    }

    /// <summary>
    /// Genereert een array van willekeurige getallen
    /// </summary>
    /// <param name="numberCount">De grootte van de teruggegeven array</param>
    /// <param name="minSize">De minimum grootte van de willekeurige getallen</param>
    /// <param name="maxSize">De maximum grootte van de willekeurige getallen</param>
    /// <returns>De array met willekeurige getalllen</returns>
    public static int[] GenereerRandomIntArray(int numberCount, int minSize = int.MinValue, int maxSize = int.MaxValue)
    {
        Random rand = new();
        int[] antwoorden = new int[numberCount];
        for (int i = 0; i < numberCount; i++)
        {
            antwoorden[i] = rand.Next(minSize, maxSize + 1);
        }
        return antwoorden;
    }
}

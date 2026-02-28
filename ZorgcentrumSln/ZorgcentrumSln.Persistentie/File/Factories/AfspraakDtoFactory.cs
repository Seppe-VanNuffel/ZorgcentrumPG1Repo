using ZorgcentrumSln.Domein.Dtos;

namespace ZorgcentrumSln.Persistentie.File.Factories;

internal class AfspraakDtoFactory
{
    internal static AfspraakDto MapToAfspraakDto(string line)
    {
        if (string.IsNullOrWhiteSpace(line))
            throw new ArgumentException("Ingelezen lijn is null, niet ingevuld of leeg", nameof(line));

        var data = line.Split(';');

        return data[1] switch
        {
            "CONSULTATIE" => MapToConsultatieDto(data),
            "CONTROLE" => MapToControleDto(data),
            "OPERATIE" => MapToOperatieDto(data),
            _ => throw new NotImplementedException("Ingelezen lijn heeft niet het juiste type")
        };
    }
    private static AfspraakDto MapToConsultatieDto(string[] data)
    {
        if(data.Length != 7)
            throw new ArgumentOutOfRangeException(nameof(data), "Ingelezen lijn heeft niet genoeg data");

        var afspraak = MapToAfspraakTuple(data);

        return new ConsultatieAfspraakDto(afspraak.Id, afspraak.DatumTijdStart, afspraak.duurMin, afspraak.RijksregisterNrPatient, afspraak.RijksregisterNrArts, afspraak.afdelingCode);
    }
    private static AfspraakDto MapToControleDto(string[] data)
    {
        if (data.Length != 8)
            throw new ArgumentOutOfRangeException(nameof(data), "Ingelezen lijn heeft niet genoeg data");

        var afspraak = MapToAfspraakTuple(data);

        return new ControleAfspraakDto(afspraak.Id, afspraak.DatumTijdStart, afspraak.duurMin, afspraak.RijksregisterNrPatient, afspraak.RijksregisterNrArts, afspraak.afdelingCode, data[7]);
    }

    private static AfspraakDto MapToOperatieDto(string[] data)
    {
        if (data.Length != 9)
            throw new ArgumentOutOfRangeException(nameof(data), "Ingelezen lijn heeft niet genoeg data");

        var afspraak = MapToAfspraakTuple(data);

        return new OperatieAfspraakDto(afspraak.Id, afspraak.DatumTijdStart, afspraak.duurMin, afspraak.RijksregisterNrPatient, afspraak.RijksregisterNrArts, afspraak.afdelingCode, data[7], int.Parse(data[8]));
    }

    private static (int Id, DateTime DatumTijdStart, int duurMin, string RijksregisterNrPatient, string RijksregisterNrArts, string afdelingCode) MapToAfspraakTuple(string[] data)
    {
        //AfspraakDto(int Id, DateTime DatumTijdStart, int duurMin, string RijksregisterNrPatient, string RijksregisterNrArts, string afdelingCode);

        int id = int.Parse(data[0]);
        DateTime datumTijdStart = DateTime.Parse(data[2]);
        int duurMin = int.Parse(data[3]);

        return (id, datumTijdStart, duurMin, data[4], data[5], data[6]);
    }
}
using Microsoft.Data.SqlClient;
using ZorgcentrumSln.Domein.Dtos;
using ZorgcentrumSln.Domein.Interfaces;

namespace ZorgcentrumSln.Persistentie.DataBase;

public class AfspraakRepositoryDatabase : IAfspraakRepositoryCRUD
{
    private readonly string _connectionString;

    public AfspraakRepositoryDatabase(string connectionString)
    {
        _connectionString = connectionString;
    }

    public AfspraakDto GetById(string id)
    {
        using var conn = new SqlConnection(_connectionString);
        string sql = @"
            SELECT a.*, o.Beschrijving, o.AantalMinutenOpVoorhand, c.Referentie
            FROM Afspraken a
            LEFT JOIN Operaties o ON a.Id = o.Id
            LEFT JOIN Controles c ON a.Id = c.Id
            WHERE a.Id = @Id";

        conn.Open();
        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@Id", id);
        using var reader = cmd.ExecuteReader();

        if (reader.Read()) return MapReaderToDto(reader);
        return null;
    }

    public IEnumerable<AfspraakDto> GetAll()
    {
        var list = new List<AfspraakDto>();
        using var conn = new SqlConnection(_connectionString);
        string sql = @"
            SELECT a.*, o.Beschrijving, o.AantalMinutenOpVoorhand, c.Referentie
            FROM Afspraken a
            LEFT JOIN Operaties o ON a.Id = o.Id
            LEFT JOIN Controles c ON a.Id = c.Id";

        conn.Open();
        using var cmd = new SqlCommand(sql, conn);
        using var reader = cmd.ExecuteReader();
        while (reader.Read()) list.Add(MapReaderToDto(reader));
        return list;
    }

    public void Insert(AfspraakDto dto)
    {
        using var conn = new SqlConnection(_connectionString);
        conn.Open();
        using var trans = conn.BeginTransaction();
        try
        {
            string type = dto switch
            {
                OperatieAfspraakDto => "Operatie",
                ControleAfspraakDto => "Controle",
                _ => "Consultatie"
            };

            string baseSql = @"INSERT INTO Afspraken (Type, AfdelingCode, DatumTijdStart, DuurMin, RijksregisterNrArts, RijksregisterNrPatient) 
                               OUTPUT INSERTED.Id
                               VALUES (@Type, @Afdeling, @Start, @Duur, @Arts, @Patient)";

            using var cmd = new SqlCommand(baseSql, conn, trans);
            cmd.Parameters.AddWithValue("@Type", type);
            cmd.Parameters.AddWithValue("@Afdeling", dto.afdelingCode);
            cmd.Parameters.AddWithValue("@Start", dto.DatumTijdStart);
            cmd.Parameters.AddWithValue("@Duur", dto.duurMin);
            cmd.Parameters.AddWithValue("@Arts", dto.RijksregisterNrArts);
            cmd.Parameters.AddWithValue("@Patient", dto.RijksregisterNrPatient);

            int newId = (int)cmd.ExecuteScalar();

            if (dto is OperatieAfspraakDto op)
            {
                string opSql = "INSERT INTO Operaties (Id, AantalMinutenOpVoorhand, Beschrijving) VALUES (@Id, @Min, @Desc)";
                using var cmdOp = new SqlCommand(opSql, conn, trans);
                cmdOp.Parameters.AddWithValue("@Id", newId);
                cmdOp.Parameters.AddWithValue("@Min", op.AantalMinutenOpVoorhand);
                cmdOp.Parameters.AddWithValue("@Desc", op.Beschrijving);
                cmdOp.ExecuteNonQuery();
            }
            else if (dto is ControleAfspraakDto co)
            {
                string coSql = "INSERT INTO Controles (Id, Referentie) VALUES (@Id, @Ref)";
                using var cmdCo = new SqlCommand(coSql, conn, trans);
                cmdCo.Parameters.AddWithValue("@Id", newId);
                cmdCo.Parameters.AddWithValue("@Ref", co.Referentie);
                cmdCo.ExecuteNonQuery();
            }
            // Consultatie has no extra table fields in this split logic

            trans.Commit();
        }
        catch { trans.Rollback(); throw; }
    }

    public void Update(AfspraakDto dto) 
    {
        using var conn = new SqlConnection(_connectionString);
        conn.Open();
        using var trans = conn.BeginTransaction();

        try
        {
            // 1. Update the Base Table (Properties shared by all Afspraken)
            string baseSql = @"UPDATE Afspraken 
                           SET AfdelingCode = @Afd, 
                               DatumTijdStart = @Start, 
                               DuurMin = @Duur, 
                               RijksregisterNrArts = @Arts, 
                               RijksregisterNrPatient = @Pat
                           WHERE Id = @Id";

            using (var cmdBase = new SqlCommand(baseSql, conn, trans))
            {
                cmdBase.Parameters.AddWithValue("@Id", dto.Id);
                cmdBase.Parameters.AddWithValue("@Afd", dto.afdelingCode);
                cmdBase.Parameters.AddWithValue("@Start", dto.DatumTijdStart);
                cmdBase.Parameters.AddWithValue("@Duur", dto.duurMin);
                cmdBase.Parameters.AddWithValue("@Arts", dto.RijksregisterNrArts);
                cmdBase.Parameters.AddWithValue("@Pat", dto.RijksregisterNrPatient);
                cmdBase.ExecuteNonQuery();
            }

            // 2. Update Sub-type Specific Tables
            // We use pattern matching to handle the specific fields for each type.
            switch (dto)
            {
                case OperatieAfspraakDto op:
                    string opSql = @"UPDATE Operaties 
                                 SET AantalMinutenOpVoorhand = @Min, 
                                     Beschrijving = @Desc 
                                 WHERE Id = @Id";
                    using (var cmdOp = new SqlCommand(opSql, conn, trans))
                    {
                        cmdOp.Parameters.AddWithValue("@Id", op.Id);
                        cmdOp.Parameters.AddWithValue("@Min", op.AantalMinutenOpVoorhand);
                        cmdOp.Parameters.AddWithValue("@Desc", op.Beschrijving);
                        cmdOp.ExecuteNonQuery();
                    }
                    break;

                case ControleAfspraakDto co:
                    string coSql = @"UPDATE Controles 
                                 SET Referentie = @Ref 
                                 WHERE Id = @Id";
                    using (var cmdCo = new SqlCommand(coSql, conn, trans))
                    {
                        cmdCo.Parameters.AddWithValue("@Id", co.Id);
                        cmdCo.Parameters.AddWithValue("@Ref", co.Referentie);
                        cmdCo.ExecuteNonQuery();
                    }
                    break;

                case ConsultatieAfspraakDto:
                    // Consultaties currently have no extra fields in the sub-table 
                    // based on your DTO, so no further SQL is needed here.
                    break;
            }

            trans.Commit();
        }
        catch
        {
            trans.Rollback();
            throw;
        }
    }

    public void Delete(string id) 
    {
        using SqlConnection connection = new SqlConnection(_connectionString);

        string query = "DELETE FROM Afspraken WHERE Id = @Id";

        using SqlCommand command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@Id", id);

        connection.Open();
        command.ExecuteNonQuery();
    }

    private AfspraakDto MapReaderToDto(SqlDataReader reader)
    {
        int id = (int)reader["Id"];
        string type = reader["Type"].ToString();
        var start = (DateTime)reader["DatumTijdStart"];
        int duur = (int)reader["DuurMin"];
        string pat = reader["RijksregisterNrPatient"].ToString();
        string arts = reader["RijksregisterNrArts"].ToString();
        string afd = reader["AfdelingCode"].ToString();

        return type switch
        {
            "Operatie" => new OperatieAfspraakDto(id, start, duur, pat, arts, afd, reader["Beschrijving"].ToString(), (int)reader["AantalMinutenOpVoorhand"]),
            "Controle" => new ControleAfspraakDto(id, start, duur, pat, arts, afd, (int)reader["Referentie"]),
            _ => new ConsultatieAfspraakDto(id, start, duur, pat, arts, afd)
        };
    }

    public void ClearAllAfspraken()
    {
        using var conn = new SqlConnection(_connectionString);
        conn.Open();
        using var trans = conn.BeginTransaction();

        try
        {
            // 1. Clear sub-tables first (if ON DELETE CASCADE is NOT enabled)
            new SqlCommand("DELETE FROM Operaties", conn, trans).ExecuteNonQuery();
            new SqlCommand("DELETE FROM Controles", conn, trans).ExecuteNonQuery();
            new SqlCommand("DELETE FROM Consultaties", conn, trans).ExecuteNonQuery();

            // 2. Clear the base table
            new SqlCommand("DELETE FROM Afspraken", conn, trans).ExecuteNonQuery();

            trans.Commit();
        }
        catch
        {
            trans.Rollback();
            throw;
        }
    }
}
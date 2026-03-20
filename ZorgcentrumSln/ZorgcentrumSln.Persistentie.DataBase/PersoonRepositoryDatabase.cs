using Microsoft.Data.SqlClient;
using ZorgcentrumSln.Domein.Dtos;
using ZorgcentrumSln.Domein.Interfaces;

namespace ZorgcentrumSln.Persistentie.DataBase;

public class PersoonRepositoryDatabase : IPersoonRepositoryCRUD
{
    private readonly string _connectionString;

    public PersoonRepositoryDatabase(string connectionString) => _connectionString = connectionString;

    public PersoonDto GetArtsById(string id)
    {
        using var conn = new SqlConnection(_connectionString);
        string sql = @"
        SELECT p.RijksregisterNummer, p.Naam, p.Voornaam, a.RIZIV, a.SpecialisatieCode 
        FROM Personen p 
        INNER JOIN Artsen a ON p.RijksregisterNummer = a.RijksregisterNummer
        WHERE p.RijksregisterNummer = @Id";

        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@Id", id);
        conn.Open();

        using var reader = cmd.ExecuteReader();
        if (reader.Read())
        {
            // Reuse your GetDepartments helper
            var depts = GetAfdelingen(id, "ArtsenAfdelingen");

            return new ArtsPersoonDto(
                reader["RijksregisterNummer"].ToString(),
                reader["Naam"].ToString(),
                reader["Voornaam"].ToString(),
                reader["RIZIV"].ToString(),
                reader["SpecialisatieCode"].ToString(),
                depts
            );
        }

        return null;
    }

    public PersoonDto GetPatientById(string id)
    {
        using var conn = new SqlConnection(_connectionString);
        string sql = @"
        SELECT p.RijksregisterNummer, p.Naam, p.Voornaam 
        FROM Personen p 
        INNER JOIN Patienten pt ON p.RijksregisterNummer = pt.RijksregisterNummer
        WHERE p.RijksregisterNummer = @Id";

        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@Id", id);
        conn.Open();

        using var reader = cmd.ExecuteReader();
        if (reader.Read())
        {
            // Reuse your GetDepartments helper
            var depts = GetAfdelingen(id, "PatientenAfdelingen");

            return new PatientPersoonDto(
                reader["RijksregisterNummer"].ToString(),
                reader["Naam"].ToString(),
                reader["Voornaam"].ToString(),
                depts
            );
        }

        return null;
    }

    public IEnumerable<PersoonDto> GetAll()
    {
        var personen = new List<PersoonDto>();

        using var conn = new SqlConnection(_connectionString);
        conn.Open();

        // 1. Get all Doctors
        string artsSql = @"
        SELECT p.RijksregisterNummer, p.Naam, p.Voornaam, a.RIZIV, a.SpecialisatieCode 
        FROM Personen p 
        INNER JOIN Artsen a ON p.RijksregisterNummer = a.RijksregisterNummer";

        using (var cmd = new SqlCommand(artsSql, conn))
        {
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                var rr = reader["RijksregisterNummer"].ToString();
                // Note: We get departments inside the loop or via a separate joined query. 
                // For simplicity and to avoid MultipleActiveResultSets issues, we use a helper.
                var depts = GetAfdelingen(rr, "ArtsenAfdelingen");

                personen.Add(new ArtsPersoonDto(
                    rr,
                    reader["Naam"].ToString(),
                    reader["Voornaam"].ToString(),
                    reader["RIZIV"].ToString(),
                    reader["SpecialisatieCode"].ToString(),
                    depts
                ));
            }
        }

        // 2. Get all Patients
        string patSql = @"
        SELECT p.RijksregisterNummer, p.Naam, p.Voornaam 
        FROM Personen p 
        INNER JOIN Patienten pt ON p.RijksregisterNummer = pt.RijksregisterNummer";

        using (var cmd = new SqlCommand(patSql, conn))
        {
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                var rr = reader["RijksregisterNummer"].ToString();
                var depts = GetAfdelingen(rr, "PatientenAfdelingen");

                personen.Add(new PatientPersoonDto(
                    rr,
                    reader["Naam"].ToString(),
                    reader["Voornaam"].ToString(),
                    depts
                ));
            }
        }

        return personen;
    }

    private HashSet<string> GetAfdelingen(string rrn, string table)
    {
        var departments = new HashSet<string>();

        // Using a new connection here to avoid conflicts if the 
        // calling method already has an active DataReader open.
        using (var conn = new SqlConnection(_connectionString))
        {
            // Table names cannot be parameters, but since we define them 
            // in our code (ArtsenAfdelingen/PatientenAfdelingen), this is safe.
            string sql = $"SELECT AfdelingCode FROM {table} WHERE RijksregisterNummer = @Id";

            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@Id", rrn);
                conn.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        departments.Add(reader["AfdelingCode"].ToString());
                    }
                }
            }
        }

        return departments;
    }

    public void Insert(PersoonDto persoon)
    {
        using SqlConnection connection = new SqlConnection(_connectionString);
        connection.Open();

        using SqlTransaction transaction = connection.BeginTransaction();

        try
        {
            // first see if the person already exists in the Personen table to avoid PK conflicts
            bool exist = false;
            string checkExistenceQuery = "SELECT COUNT(*) FROM Personen WHERE RijksregisterNummer = @RijksregisterNummer";

            using (SqlCommand checkCommand = new SqlCommand(checkExistenceQuery, connection, transaction))
            {
                checkCommand.Parameters.AddWithValue("@RijksregisterNummer", persoon.RijksregisterNummer);
                int count = (int)checkCommand.ExecuteScalar();
                if (count > 0)
                {
                    exist = true;
                }
            }

            if (!exist)
            {
                string insertPersoonQuery = @"
                INSERT INTO Personen (RijksregisterNummer, Naam, Voornaam)
                VALUES (@RijksregisterNummer, @Naam, @Voornaam)";

                using SqlCommand persoonCommand = new SqlCommand(insertPersoonQuery, connection, transaction);
                persoonCommand.Parameters.AddWithValue("@RijksregisterNummer", persoon.RijksregisterNummer);
                persoonCommand.Parameters.AddWithValue("@Naam", persoon.Naam);
                persoonCommand.Parameters.AddWithValue("@Voornaam", persoon.Voornaam);

                persoonCommand.ExecuteNonQuery(); 
            }

            if(persoon is ArtsPersoonDto arts)
            {
                string query = @"
                    INSERT INTO Artsen (RijksregisterNummer, RIZIV, SpecialisatieCode)
                    VALUES (@RijksregisterNummer, @RIZIV, @SpecialisatieCode)";

                using SqlCommand command = new SqlCommand(query, connection, transaction);
                command.Parameters.AddWithValue("@RijksregisterNummer", arts.RijksregisterNummer);
                command.Parameters.AddWithValue("@RIZIV", arts.RIZIV);
                command.Parameters.AddWithValue("@SpecialisatieCode", arts.Specialisatie);

                command.ExecuteNonQuery();

                SaveAfdelingen(arts.RijksregisterNummer, arts.Afdelingen, "ArtsenAfdelingen", connection, transaction);

                transaction.Commit();
            }
            else if(persoon is PatientPersoonDto patient)
            {
                string query = @"
                    INSERT INTO Patienten(RijksregisterNummer) 
                    VALUES(@RijksregisterNummer)";

                using SqlCommand command = new SqlCommand(query, connection, transaction);
                command.Parameters.AddWithValue("@RijksregisterNummer", patient.RijksregisterNummer);

                command.ExecuteNonQuery();

                SaveAfdelingen(patient.RijksregisterNummer, patient.Afdelingen, "PatientenAfdelingen", connection, transaction);

                transaction.Commit();
            }
        }
        catch
        {
            transaction.Rollback();
            throw;
        }
    }

    public void Update(PersoonDto persoon) 
    {
        using SqlConnection connection = new SqlConnection(_connectionString);
        connection.Open();

        using SqlTransaction transaction = connection.BeginTransaction();

        try
        {
            string insertPersoonQuery = @"UPDATE Personen 
                           SET Naam = @Naam, Voornaam = @Voornaam 
                           WHERE RijksregisterNummer = @RijksregisterNummer";

            using SqlCommand persoonCommand = new SqlCommand(insertPersoonQuery, connection, transaction);
            persoonCommand.Parameters.AddWithValue("@RijksregisterNummer", persoon.RijksregisterNummer);
            persoonCommand.Parameters.AddWithValue("@Naam", persoon.Naam);
            persoonCommand.Parameters.AddWithValue("@Voornaam", persoon.Voornaam);

            persoonCommand.ExecuteNonQuery();

            if (persoon is ArtsPersoonDto arts)
            {
                string query = @"UPDATE Artsen 
                               SET RIZIV = @RIZIV, SpecialisatieCode = @SpecialisatieCode 
                               WHERE RijksregisterNummer = @RijksregisterNummer";

                using SqlCommand command = new SqlCommand(query, connection, transaction);
                command.Parameters.AddWithValue("@RijksregisterNummer", arts.RijksregisterNummer);
                command.Parameters.AddWithValue("@RIZIV", arts.RIZIV);
                command.Parameters.AddWithValue("@SpecialisatieCode", arts.Specialisatie);

                command.ExecuteNonQuery();

                SyncDepartments(arts.RijksregisterNummer, arts.Afdelingen, "ArtsenAfdelingen", connection, transaction);

                transaction.Commit();
            }
            else if (persoon is PatientPersoonDto patient)
            {
                SyncDepartments(patient.RijksregisterNummer, patient.Afdelingen, "PatientenAfdelingen", connection, transaction);

                transaction.Commit();
            }
        }
        catch
        {
            transaction.Rollback();
            throw;
        }
    }

    public void DeletePatient(string id)
    {
        using SqlConnection connection = new SqlConnection(_connectionString);

        string query = "DELETE FROM Patienten WHERE RijksregisterNummer = @RijksregisterNummer";

        using SqlCommand command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@RijksregisterNummer", id);

        connection.Open();
        command.ExecuteNonQuery();
    }

    public void DeleteArts(string id)
    {
        using SqlConnection connection = new SqlConnection(_connectionString);

        string query = "DELETE FROM Artsen WHERE RijksregisterNummer = @RijksregisterNummer";

        using SqlCommand command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@RijksregisterNummer", id);

        connection.Open();
        command.ExecuteNonQuery();
    }

    public void Delete(string id) 
    {
        using SqlConnection connection = new SqlConnection(_connectionString);

        string query = "DELETE FROM Personen WHERE RijksregisterNummer = @RijksregisterNummer";

        using SqlCommand command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@RijksregisterNummer", id);

        connection.Open();
        command.ExecuteNonQuery();
    }

    private void SaveAfdelingen(string rrn, IEnumerable<string> afdelingen, string table, SqlConnection conn, SqlTransaction trans)
    {
        foreach (var afdeling in afdelingen)
        {
            string sql = $"INSERT INTO {table} (RijksregisterNummer, AfdelingCode) VALUES (@rrn, @Code)";
            using var cmd = new SqlCommand(sql, conn, trans);
            cmd.Parameters.AddWithValue("@rrn", rrn);
            cmd.Parameters.AddWithValue("@Code", afdeling);
            cmd.ExecuteNonQuery();
        }
    }

    /// <summary>
    /// Synchronizes the junction table by clearing existing entries and inserting the current set.
    /// </summary>
    private void SyncDepartments(string rrn, IEnumerable<string> afdelingen, string table, SqlConnection conn, SqlTransaction trans)
    {
        // Step A: Delete all existing links for this person
        string deleteSql = $"DELETE FROM {table} WHERE RijksregisterNummer = @rrn";
        using (var cmdDel = new SqlCommand(deleteSql, conn, trans))
        {
            cmdDel.Parameters.AddWithValue("@rrn", rrn);
            cmdDel.ExecuteNonQuery();
        }

        // Step B: Insert the new set of departments
        foreach (var afdeling in afdelingen)
        {
            string insertSql = $"INSERT INTO {table} (RijksregisterNummer, AfdelingCode) VALUES (@Id, @Code)";
            using (var cmdIns = new SqlCommand(insertSql, conn, trans))
            {
                cmdIns.Parameters.AddWithValue("@Id", rrn);
                cmdIns.Parameters.AddWithValue("@Code", afdeling);
                cmdIns.ExecuteNonQuery();
            }
        }
    }

    public void ClearAllPersonen()
    {
        using var conn = new SqlConnection(_connectionString);
        conn.Open();
        using var trans = conn.BeginTransaction();

        try
        {
            // 1. Clear Junction tables (Many-to-Many links)
            new SqlCommand("DELETE FROM ArtsenAfdelingen", conn, trans).ExecuteNonQuery();
            new SqlCommand("DELETE FROM PatientenAfdelingen", conn, trans).ExecuteNonQuery();

            // 2. Clear Role sub-tables
            new SqlCommand("DELETE FROM Artsen", conn, trans).ExecuteNonQuery();
            new SqlCommand("DELETE FROM Patienten", conn, trans).ExecuteNonQuery();

            // 3. Finally clear the base Identity table
            new SqlCommand("DELETE FROM Personen", conn, trans).ExecuteNonQuery();

            trans.Commit();
        }
        catch
        {
            trans.Rollback();
            throw;
        }
    }
}
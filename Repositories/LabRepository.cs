using LabManager.Database;
using LabManager.Models;
using Microsoft.Data.Sqlite;

namespace LabManager.Repositories;

class LabRepository
{
    private readonly DatabaseConfig _databaseConfig;

    public LabRepository(DatabaseConfig databaseConfig)
    {
        _databaseConfig = databaseConfig;
    }

    public List<Lab> GetAll()
    {
        var labs = new List<Lab>();
        var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();
        var command = connection.CreateCommand();

        command.CommandText = "SELECT * FROM Labs;";

        var reader = command.ExecuteReader();
        while (reader.Read())
        {
            var lab = ReaderToLab(reader);
            labs.Add(lab);
        }
        connection.Close();
        
        return labs;
    }

    public Lab Save(Lab lab)
    {

        var connection = new SqliteConnection("Data Source=database.db");
        connection.Open();
        var command = connection.CreateCommand();

        command.CommandText = $"INSERT INTO Labs VALUES($id, $number, $name, $block)";

        command.Parameters.AddWithValue("$id", lab.Id);
        command.Parameters.AddWithValue("$number", lab.Number);
        command.Parameters.AddWithValue("$name", lab.Name);
        command.Parameters.AddWithValue("$block", lab.Block);

        command.ExecuteNonQuery();

        connection.Close();

        return lab;
    }

    public Lab Update(Lab lab)
    {
        var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();
        var command = connection.CreateCommand();

        command.CommandText = @"
            UPDATE Labs 
            SET number = $number, name = $name, block = $block 
            WHERE id = $id;
        ";

        command.Parameters.AddWithValue("$id", lab.Id);
        command.Parameters.AddWithValue("$number", lab.Number);
        command.Parameters.AddWithValue("$name", lab.Name);
        command.Parameters.AddWithValue("$block", lab.Block);

        command.ExecuteNonQuery();

        connection.Close();

        return lab;
    }

    public Lab GetById(int id)
    {
        var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();
        var command = connection.CreateCommand();

        command.CommandText = "SELECT * FROM Labs WHERE id = $id;";

        command.Parameters.AddWithValue("$id", id);

        var reader = command.ExecuteReader();
        reader.Read();

        var lab = ReaderToLab(reader);

        connection.Close();

        return lab;
    }

    public void Delete(int id)
    {
         var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();
        var command = connection.CreateCommand();

        command.CommandText = "DELETE FROM Labs WHERE id = $id;";

        command.Parameters.AddWithValue("$id", id);

        command.ExecuteNonQuery();

        connection.Close();
    }

    private Lab ReaderToLab(SqliteDataReader reader)
    {
        var lab = new Lab(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3));
        return lab;
    }

     public bool ExistsById(int id)
    {   
        var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();
        
        var command = connection.CreateCommand();
        command.CommandText = "SELECT count(ID) FROM Labs WHERE ID = $id;";
        command.Parameters.AddWithValue("$id", id);

        var result = Convert.ToBoolean(command.ExecuteScalar()); // ExecuteScalar devolve apenas o primeiro valor 
        connection.Close();

        return result;
    }
}
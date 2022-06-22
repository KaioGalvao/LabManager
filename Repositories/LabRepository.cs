using Dapper;
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

    public IEnumerable<Lab> GetAll()
    {
        using var connection = new SqliteConnection(_databaseConfig.ConnectionString);

        var labs = connection.Query<Lab>("SELECT * FROM Labs");

        return labs;
    }

    public Lab Save(Lab lab)
    {

        var connection = new SqliteConnection("Data Source=database.db");

        connection.Execute("INSERT INTO Labs VALUES(@Id, @Number, @Name, @Block)", lab);

        return lab;
    }

    public Lab Update(Lab lab)
    {
        using var connection = new SqliteConnection(_databaseConfig.ConnectionString);

        connection.Execute(@"
            UPDATE Labs 
            SET number = @Number, name = @Name, block = @Block 
            WHERE id = @Id;
        ", lab);

        return lab;
    }

    public Lab GetById(int id)
    {
        using var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        
        var lab = connection.QuerySingle<Lab>("SELECT * FROM Labs WHERE id = @Id;", new {Id = id});

        return lab;
    }

    public void Delete(int id)
    {
        using var connection = new SqliteConnection(_databaseConfig.ConnectionString);

        connection.Execute("DELETE FROM Labs WHERE id = @Id;", new {Id = id});
    }

    public bool ExistsById(int id)
    {   
        using (var connection = new SqliteConnection(_databaseConfig.ConnectionString))
        {   
            var result = connection.ExecuteScalar<Boolean>("SELECT count(ID) FROM Labs WHERE ID = @Id;", new {Id = id});
            return result;
        }
    }
}

using LabManager.Database;
using LabManager.Models;
using Microsoft.Data.Sqlite;

namespace LabManager.Repositories;


class ComputerRepository
{

    private readonly DatabaseConfig _databaseConfig;

    public ComputerRepository(DatabaseConfig databaseConfig)
    {
        _databaseConfig = databaseConfig;
    }

    public List<Computer> GetAll()
    {
        var computers = new List<Computer>();

        var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();
        var command = connection.CreateCommand();

        command.CommandText = $"SELECT * FROM Computers;";

        var reader = command.ExecuteReader();

        while (reader.Read())
        {
            var computer = new Computer(reader.GetInt32(0),reader.GetString(1), reader.GetString(2));
            
            computers.Add(computer);

            /*computer.Add(
                new Computer(
                    reader.GetInt32(0),
                    reader.GetString(1), 
                    reader.GetString(2)
                )
            );*/
        }

        connection.Close();

        return computers;
    }
}
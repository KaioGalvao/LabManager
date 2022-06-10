using Microsoft.Data.Sqlite;

namespace LabManager.Database;

class DatabaseSetup{

    private readonly DatabaseConfig _databaseConfig;

    public DatabaseSetup(DatabaseConfig databaseConfig)
    {   
        _databaseConfig = databaseConfig;
        CreateComputerTable();
        CreateLabTable();
    }


    private void CreateComputerTable()
    {
        var connection = new SqliteConnection(_databaseConfig.ConnectionString);

        connection.Open();
        var command = connection.CreateCommand();

        command.CommandText = @"
            CREATE TABLE IF NOT EXISTS Computers(
                ID int not null primary key,
                ram varchar(100) not null,
                processor varchar(100) not null
            );
        ";

        command.ExecuteNonQuery();

        connection.Close();
    }

    private void CreateLabTable()
    {
        var connection = new SqliteConnection("Data Source=database.db");

        connection.Open();
        var command = connection.CreateCommand();

        command.CommandText = @"
            CREATE TABLE IF NOT EXISTS Labs(
                id INT NOT NULL PRIMARY KEY,
                number VARCHAR(100) NOT NULL,
                name VARCHAR(100) NOT NULL,
                block VARCHAR(50) NOT NULL
            );
        ";

        command.ExecuteNonQuery();

        connection.Close();
    }
}





﻿using Microsoft.Data.Sqlite;

var connection = new SqliteConnection("Data Source=database.db");

connection.Open();
var command = connection.CreateCommand();

command.CommandText = @"
    CREATE TABLE IF NOT EXISTS Computers(
        ID int not null primary key,
        ram varchar(100) not null,
        processor varchar(100) not null
    );
    CREATE TABLE IF NOT EXISTS Labs(
        ID INT NOT NULL PRIMARY KEY,
        Number INT NOT NULL,
        Name VARCHAR(100) NOT NULL,
        Block VARCHAR(50) NOT NULL
    );";

command.ExecuteNonQuery();

connection.Close();

var modelName = args[0];
var modelAction = args[1];

if (modelName == "Computer")
{
    if (modelAction == "List")
    {
        connection = new SqliteConnection("Data Source=database.db");
        connection.Open();
        command = connection.CreateCommand();

        command.CommandText = $"SELECT * FROM Computers;";

        var reader = command.ExecuteReader();

        while (reader.Read())
        {
            Console.WriteLine($"{reader.GetInt32(0)} {reader.GetString(1)} {reader.GetString(2)}");
        }

        connection.Close();
    }
    else if (modelAction == "New")
    {
        var id = Convert.ToInt32(args[2]);
        var ram = args[3];
        var processor = args[4];
        connection = new SqliteConnection("Data Source=database.db");
        connection.Open();
        command = connection.CreateCommand();

        command.CommandText = $"INSERT INTO Computers VALUES($id, $ram, $processor)";

        command.Parameters.AddWithValue("$id", id);
        command.Parameters.AddWithValue("$ram", ram);
        command.Parameters.AddWithValue("$processor", processor);

        command.ExecuteNonQuery();

        connection.Close();
    }
}
else if (modelName == "Lab")
{
    switch (modelAction)
    {
        case "New":
            {
                var id = Convert.ToInt32(args[2]);
                var number = args[3];
                var name = args[4];
                var block = args[5];
                connection = new SqliteConnection("Data Source=database.db");
                connection.Open();
                command = connection.CreateCommand();

                command.CommandText = $"INSERT INTO Labs VALUES($id, $number, $name, $block)";

                command.Parameters.AddWithValue("$id", id);
                command.Parameters.AddWithValue("$number", number);
                command.Parameters.AddWithValue("$name", name);
                command.Parameters.AddWithValue("$block", block);

                command.ExecuteNonQuery();

                connection.Close();

                break;
            }
        case "List":
            {
                connection = new SqliteConnection("Data Source=database.db");
                connection.Open();
                command = connection.CreateCommand();

                command.CommandText = $"SELECT * FROM Labs;";

                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Console.WriteLine($"{reader.GetInt32(0)} {reader.GetString(1)} {reader.GetString(2)} {reader.GetString(3)}");
                }

                connection.Close();
                break;
            }
        default:
            Console.WriteLine("Comando inválido");
            break;
    }
}
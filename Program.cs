/*
CRUD (CREATE, READ, UPDATE, DELETE)
Computer - Id, Ram, Processor
dotnet run -- Computer List
dotnet run -- Computer New 1 '16' 'Intel Dual Core'
dotnet run -- Computer Delete 1
dotnet run -- Computer Update 1 '8' 'Intel Dual Core'
dotnet run -- Computer Show 1
Lab - Id, Number, Name, Block
dotnet run -- Lab List
dotnet run -- Lab New 1 '2' 'Charles ...' '2'
dotnet run -- Lab Delete 1
dotnet run -- Lab Update 1 '2' 'Charles ...' '2'
dotnet run -- Lab Show 1
foreach (var arg in args)
{
    Console.WriteLine(arg);
}
dotnet add package Microsoft.Data.Sqlite
dotnet add package Microsoft.Data.Sqlite -s 'C:\Users\IFSP\.nuget\packages'



*/


using Microsoft.Data.Sqlite;
using LabManager.Database;
using LabManager.Repositories;
using LabManager.Models;

var databaseConfig = new DatabaseConfig();

var databaseSetup = new DatabaseSetup(databaseConfig);

//Routing
var modelName = args[0];
var modelAction = args[1];

if (modelName == "Computer")
{   
    var computerRepository = new ComputerRepository(databaseConfig);

    switch (modelAction)
    {   
        case "List" :
        {
            Console.WriteLine("Lista de computadores:");
        
            foreach (var computer in computerRepository.GetAll())
            {
                Console.WriteLine($"{computer.Id}, {computer.Ram}, {computer.Processor}");
            }

            break;
        }

        case "New" :
        {
            var id = Convert.ToInt32(args[2]);
            var ram = args[3];
            var processor = args[4];
            var computer = new Computer(id, ram, processor);
            computerRepository.Save(computer);

            break;
        }

        case "Show" :
        {
            var id = Convert.ToInt32(args[2]);

            var computer = computerRepository.GetById(id);

            Console.WriteLine($"{computer.Id}, {computer.Ram}, {computer.Processor}");

            break;
        }

        case "Update" :
        {
            var id = Convert.ToInt32(args[2]);
            var ram = args[3];
            var processor = args[4];
            
            var computer = new Computer(id, ram, processor);

            computer = computerRepository.Update(computer);
            
            break;
        }
        
        case "Delete" :
        {
            var id = Convert.ToInt32(args[2]);

            computerRepository.Delete(id);
            break;
        }

        default: 
        {
            Console.WriteLine("Comando inválido");
            break;
        }
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
            var connection = new SqliteConnection("Data Source=database.db");
            connection.Open();
            var command = connection.CreateCommand();

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
            var connection = new SqliteConnection("Data Source=database.db");
            connection.Open();
            var command = connection.CreateCommand();

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
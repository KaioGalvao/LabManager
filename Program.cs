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
var modelAction = args[1]; //Roteamento para executar partes do código

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

            if (computerRepository.ExistsById(id))
            {
                var computer = computerRepository.GetById(id);
                Console.WriteLine($"{computer.Id}, {computer.Ram}, {computer.Processor}");
            } 
            else {
                Console.WriteLine($"Computador com id = {id} não existe.");
            } 
            
            break;
        }

        case "Update" :
        {
            var id = Convert.ToInt32(args[2]);

            if(computerRepository.ExistsById(id)){
                var ram = args[3];
                var processor = args[4];
                var computer = new Computer(id, ram, processor);
                computer = computerRepository.Update(computer);
            }
            else {
                Console.WriteLine($"Computador com id = {id} não existe.");
            }    
            
            break;
        }
        
        case "Delete" :
        {
            var id = Convert.ToInt32(args[2]);
            if (computerRepository.ExistsById(id))
            {
                computerRepository.Delete(id);
            }
            else {
               Console.WriteLine($"Computador com id = {id} não existe."); 
            }
        
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

    var labRepository = new LabRepository(databaseConfig);

    switch (modelAction)
    {
        
        case "New":
        {
            var id = Convert.ToInt32(args[2]);
            var number = args[3];
            var name = args[4];
            var block = args[5];
            var lab = new Lab(id, number, name, block);
            
            labRepository.Save(lab);

            break;
        }
        
        case "List":
        {
            Console.WriteLine("Lista de Laboratórios:");
        
            foreach (var lab in labRepository.GetAll())
            {
                Console.WriteLine($"{lab.Id}, {lab.Number}, {lab.Name}, {lab.Block}");
            }
            

            break;
        }

        case "Show":
        {
            var id = Convert.ToInt32(args[2]);
            
            if (labRepository.ExistsById(id))
            {
                var lab =  labRepository.GetById(id);
                Console.WriteLine($"{lab.Id},{lab.Number}, {lab.Name}, {lab.Block} ");
            }
            
            break;
        }

        case "Update":
        {
            var id = Convert.ToInt32(args[2]);
            
            if (labRepository.ExistsById(id))
            {
                var number = args[3];
                var name = args[4];
                var block = args[5];
                var lab = new Lab(id, number, name, block);
                lab = labRepository.Update(lab);
            }
            
            else
            {
                Console.WriteLine($"Laboratório com id = {id} não existe.");
            }

            break;
        }

        case "Delete":
        {
            var id = Convert.ToInt32(args[2]);

            if (labRepository.ExistsById(id))
            {
                labRepository.Delete(id);
            }
            
            else
            {
                Console.WriteLine($"Laboratório com id = {id} não existe.");
            }

            break;
        }
        
        default:
        {
            Console.WriteLine("Comando inválido");
            break;
        }
    }
}
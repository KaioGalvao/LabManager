namespace LabManager.Models;

class Computer
{
    public int Id { get; }
    public String Ram { get;}
    public String Processor { get;}

    public Computer(int id, string ram, string processor)
    {
        Id = id;
        Ram = ram;
        Processor = processor;
    }
}
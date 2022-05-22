using JsonEmployeeNewGenerator.Services;

namespace JsonEmployeeGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            var employees = ReadFileService.ReadFile();
            WriteFileService.WriteFile(employees);
        }
    }
}

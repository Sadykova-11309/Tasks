using System.Reflection;

namespace Task5
{
    class Program
    {
        static void Main(string[] args)
        {
            string currentDirectory = Directory.GetCurrentDirectory();

            var assemblyFiles = Directory.GetFiles(currentDirectory, "*.dll");

            foreach (var assemblyFile in assemblyFiles)
            {
                    Assembly assembly = Assembly.LoadFrom(assemblyFile);

                    foreach (var type in assembly.GetTypes())
                    {
                        var printMethod = type.GetMethod("Print");

                        if (printMethod != null)
                        {
                            Console.WriteLine($"Класс: {type.FullName} содержит метод Print()");
                        }
                    }
            }
            Console.WriteLine("Поиск завершен.");
        }
    }
}

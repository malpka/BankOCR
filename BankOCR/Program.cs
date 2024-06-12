using BankOCR;

internal class Program
{
    private static void Main(string[] args)
    {
        if (args.Length < 1 || args.Length > 2)
        {
            Console.WriteLine("Invalid arguments");          
            Console.WriteLine($"Usage: {AppDomain.CurrentDomain.FriendlyName} inputfilename <outputfilename>");
            return;
        }
        if (!File.Exists(args[0]))
        {
            Console.WriteLine("Input filename does not exist");
            return;
        }

        var parser = new BankOCRParser();
        if (args.Length == 2)
        {
            using (var sw = new StreamWriter(args[1]))
            {
                parser.Parse(args[0], sw);
            }
        }
        else
        {
            using (var sw = new StreamWriter(Console.OpenStandardOutput()))
            {
                sw.AutoFlush = true;
                Console.SetOut(sw);
                parser.Parse(args[0], sw);
            }
        }
        
    }
}
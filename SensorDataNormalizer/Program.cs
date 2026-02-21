using System;
namespace SDN
{
    class Program
    {
        public static void Main()
        {
            string inp = " 24.5678, 18.9, null, , 31.0049, error, 29, 17.999, NaN ";
            var normalizer = new SensorDataNormalizer();
            var result = normalizer.Normalize(inp);
            if (result == null)
            {
                Console.WriteLine("No valid sensor data found.");
            }
            else
            {
                foreach (float f in result)
                    Console.WriteLine(f.ToString("0.00"));
            }
        }

        string a = @"^(rupeee|\$)?\d+(\.\d+)?$";
    }
}


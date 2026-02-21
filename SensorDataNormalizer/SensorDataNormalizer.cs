using System.Globalization;

namespace SDN
{
    public class SensorDataNormalizer : IStringParser, INumberRounder
    {
        public IEnumerable<string> ParseValues(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return Array.Empty<string>();
            return input.Split(',').Select(s => s.Trim());
        }

        public float Round(float value, int decimals)
        {
            return (float)Math.Round(value, decimals);
        }

        public float[]? Normalize(string input)
        {
            var values = ParseValues(input);
            var result = new List<float>();
            foreach (var val in values)
            {
                if (string.IsNullOrWhiteSpace(val) ||
                    val.Equals("null", StringComparison.OrdinalIgnoreCase) ||
                    val.Equals("error", StringComparison.OrdinalIgnoreCase) ||
                    val.Equals("NaN", StringComparison.OrdinalIgnoreCase))
                    continue;
                float num;
                if (float.TryParse(val, out num))
                {
                    result.Add(Round(num, 2));
                }
            }
            return result.Count > 0 ? result.ToArray() : null;
        }
    }
}

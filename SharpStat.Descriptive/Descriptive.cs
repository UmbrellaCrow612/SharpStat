
namespace SharpStat.Descriptive
{

    public class Descriptive : IDescriptive
    {
        public double Mean(double[] numbers)
        {
            ArgumentNullException.ThrowIfNull(numbers);
            if(numbers.Length is 0) return 0;

            Span<double> _numbers = numbers;
            double total = 0;

            for (int i = 0; i < _numbers.Length; i++)
            {
                total += _numbers[i];
            }

            return total / _numbers.Length;
        }
    }
}

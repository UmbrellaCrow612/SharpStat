
namespace SharpStat.Descriptive
{
    /// <summary>
    /// Contains descriptive statistical operations
    /// </summary>
    public class Descriptive : IDescriptive
    {
        public double Mean(double[] numbers)
        {
            ArgumentNullException.ThrowIfNull(numbers);
            if (numbers.Length is 0) return 0;

            Span<double> _numbers = numbers;
            double total = 0;

            for (int i = 0; i < _numbers.Length; i++)
            {
                total += _numbers[i];
            }

            return total / _numbers.Length;
        }

        public double MedianFromSorted(double[] sortedNumbers)
        {
            ArgumentNullException.ThrowIfNull(sortedNumbers);
            if (sortedNumbers.Length is 0) return 0;

            Span<double> _sortedNumbers = sortedNumbers;
            int mid = _sortedNumbers.Length / 2;

            if (_sortedNumbers.Length % 2 == 1)
            {
                return _sortedNumbers[mid];
            }
            else
            {
                return (_sortedNumbers[mid - 1] + _sortedNumbers[mid]) / 2.0;
            }
        }
    }
}

namespace SharpStat.Descriptive
{
    /// <summary>
    /// Defines the contract for descriptive statistical operations that must be implemented by the <see cref="Descriptive"/> class.
    /// </summary>
    internal interface IDescriptive
    {
        /// <summary>
        /// Calculates the arithmetic mean (average) of a sequence of numbers.
        /// </summary>
        /// <param name="numbers">A sequence of numeric values.</param>
        /// <returns>The calculated mean of the input values.</returns>
        double Mean(double[] numbers);
    }
}

namespace SharpStat.Descriptive.Tests
{
    public class DescriptiveTests
    {
        private readonly Descriptive _descriptive;

        public DescriptiveTests()
        {
            _descriptive = new Descriptive();
        }


        /// ====================== <see cref="Descriptive.Mean(double[])"/> Tests =================================== 

        [Fact]
        public void Mean_WithValidArray_ReturnsCorrectMean()
        {
            // Arrange
            double[] numbers = [1, 2, 3, 4, 5];

            // Act
            double result = _descriptive.Mean(numbers);

            // Assert
            Assert.Equal(3.0, result);
        }

        [Fact]
        public void Mean_WithSingleElement_ReturnsSameElement()
        {
            // Arrange
            double[] numbers = [42.0];

            // Act
            double result = _descriptive.Mean(numbers);

            // Assert
            Assert.Equal(42.0, result);
        }

        [Fact]
        public void Mean_WithNegativeAndPositiveNumbers_ReturnsCorrectMean()
        {
            // Arrange
            double[] numbers = [-5, 0, 5];

            // Act
            double result = _descriptive.Mean(numbers);

            // Assert
            Assert.Equal(0.0, result);
        }

        [Fact]
        public void Mean_WithEmptyArray_ReturnsZero()
        {
            // Arrange
            double[] numbers = [];

            // Act & Assert
            Assert.Equal(0, _descriptive.Mean(numbers));
        }

        [Fact]
        public void Mean_WithNullArray_ThrowsArgumentNullException()
        {
            // Arrange
            double[] numbers = null!;

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => _descriptive.Mean(numbers));
        }


        /// ====================== <see cref="Descriptive.Mean(double[])"/> Tests end =================================== 
    }
}

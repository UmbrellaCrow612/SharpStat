**Project Specification: `SharpStat` NuGet Package**

**1. Project Goal:**
To create a .NET Standard library providing common statistical functions, inspired by R's `stats` package, for use in .NET applications. It aims for ease of use, reasonable performance, and clear, well-documented APIs.

**2. Target Audience:**
.NET developers who need statistical capabilities for data analysis, modeling, or simulations, and appreciate an API that might feel somewhat familiar to R users or provides similar functionalities.

**3. Core Features (MVP - Minimum Viable Product):**

    **A. Descriptive Statistics:**
        *   **Namespace:** `SharpStat.Descriptive`
        *   **Input:** Primarily `IEnumerable<double>`, `double[]`. Handle `null` or empty collections gracefully (e.g., return `NaN`, throw `ArgumentException`).
        *   **Functions:**
            *   `Mean(IEnumerable<double> data)`
            *   `Median(IEnumerable<double> data)` (handle even/odd counts)
            *   `Mode(IEnumerable<double> data)` (can return `double[]` or `List<double>` if multiple modes)
            *   `Variance(IEnumerable<double> data, bool population = false)` (sample variance by default)
            *   `StandardDeviation(IEnumerable<double> data, bool population = false)`
            *   `Sum(IEnumerable<double> data)`
            *   `Product(IEnumerable<double> data)`
            *   `Min(IEnumerable<double> data)`
            *   `Max(IEnumerable<double> data)`
            *   `Range(IEnumerable<double> data)` (returns a tuple or simple class `(Min, Max)`)
            *   `Quantiles(IEnumerable<double> data, IEnumerable<double> probabilities)` (e.g., `probabilities = {0.25, 0.5, 0.75}` for quartiles)
            *   `InterquartileRange(IEnumerable<double> data)`
            *   `Covariance(IEnumerable<double> data1, IEnumerable<double> data2, bool population = false)`
            *   `Correlation(IEnumerable<double> data1, IEnumerable<double> data2)` (Pearson)
            *   `Summary(IEnumerable<double> data)`: Returns an object/struct with Min, Q1, Median, Mean, Q3, Max (similar to R's `summary()`).

    **B. Probability Distributions:**
        *   **Namespace:** `SharpStat.Distributions`
        *   **Design:** For each distribution, provide static methods or a dedicated class.
            *   `Pdf(x, ...params)`: Probability Density/Mass Function
            *   `Cdf(x, ...params)`: Cumulative Distribution Function
            *   `Quantile(p, ...params)`: Inverse CDF
            *   `Sample(...params)`: Generate a single random sample
            *   `Samples(n, ...params)`: Generate `n` random samples
        *   **Distributions (Start with these):**
            *   **Normal:** `Normal.Pdf(x, mean, stdDev)`, `Normal.Cdf(x, mean, stdDev)`, etc.
            *   **Uniform:** `Uniform.Pdf(x, min, max)`, etc.
            *   **Binomial:** `Binomial.Pdf(k, n, p)`, etc. (k successes, n trials, p prob of success)
            *   **Poisson:** `Poisson.Pdf(k, lambda)`, etc. (k occurrences, lambda mean rate)
            *   **Student's t:** `StudentsT.Pdf(x, df)`, etc. (df = degrees of freedom)
        *   **Random Number Generation:** Internally use `System.Random` or a more robust generator if desired. Allow seeding for reproducibility.

    **C. Hypothesis Tests:**
        *   **Namespace:** `SharpStat.Tests`
        *   **Return Type:** A custom class/struct for each test result (e.g., `TTestResult`) containing:
            *   `Statistic` (e.g., t-value)
            *   `PValue`
            *   `DegreesOfFreedom` (if applicable)
            *   `ConfidenceInterval` (tuple or simple class)
            *   `AlternativeHypothesis` (enum: `TwoSided`, `LessThan`, `GreaterThan`)
            *   `MethodName` (string, e.g., "One-Sample t-test")
            *   `NullValue` (e.g., `mu` for t-test)
        *   **Tests (Start with these):**
            *   **One-Sample t-test:** `TTest.OneSample(IEnumerable<double> data, double mu, AlternativeHypothesis alternative = AlternativeHypothesis.TwoSided, double confLevel = 0.95)`
            *   **Two-Sample Independent t-test:** `TTest.TwoSampleIndependent(IEnumerable<double> data1, IEnumerable<double> data2, bool equalVariance = false, AlternativeHypothesis alternative = AlternativeHypothesis.TwoSided, double confLevel = 0.95)` (Welch's test if `equalVariance = false`)
            *   **Paired t-test:** `TTest.Paired(IEnumerable<double> data1, IEnumerable<double> data2, AlternativeHypothesis alternative = AlternativeHypothesis.TwoSided, double confLevel = 0.95)`
            *   **(Stretch Goal) Chi-squared Goodness of Fit:** `ChiSquared.GoodnessOfFit(IEnumerable<long> observedCounts, IEnumerable<double> expectedProbabilities)` or `ChiSquared.GoodnessOfFit(IEnumerable<long> observedCounts, IEnumerable<long> expectedCounts)`

    **D. Simple Linear Regression (OLS):**
        *   **Namespace:** `SharpStat.Models`
        *   **Function:** `LinearRegression.Fit(IEnumerable<double> yValues, IEnumerable<double> xValues)`
        *   **Return Type:** `LinearRegressionResult` class containing:
            *   `Intercept`
            *   `Slope`
            *   `StandardErrorIntercept`
            *   `StandardErrorSlope`
            *   `TStatisticIntercept`
            *   `TStatisticSlope`
            *   `PValueIntercept`
            *   `PValueSlope`
            *   `RSquared`
            *   `AdjustedRSquared`
            *   `Residuals` (`double[]`)
            *   `FittedValues` (`double[]`)
        *   **Method:** `Predict(double newXValue)` on the `LinearRegressionResult` object.

**4. Non-Functional Requirements:**

    *   **Target Framework:** .NET Standard 2.0 (for maximum compatibility).
    *   **Dependencies:** Minimize external dependencies. If Math.NET Numerics is used for complex matrix algebra or special functions, it should be a deliberate choice and clearly documented. For a personal project, implementing from scratch can be a learning goal.
    *   **Performance:** While not the primary driver for an MVP, algorithms should be reasonably efficient. Avoid grossly inefficient implementations.
    *   **Accuracy:** Strive for good numerical accuracy. Compare results against R or other established libraries. Be aware of floating-point precision issues.
    *   **Testability:** Code should be structured to be easily unit-tested. Aim for high test coverage.
    *   **Documentation:**
        *   XML documentation comments for all public APIs (for IntelliSense).
        *   A `README.md` file with project description, installation instructions, and basic usage examples.
    *   **Error Handling:** Use exceptions for invalid input or computation errors (e.g., `ArgumentNullException`, `ArgumentOutOfRangeException`, `InvalidOperationException` for singular matrices).

**5. API Design Principles:**

    *   **Clarity and Discoverability:** Namespaces and method names should be intuitive.
    *   **.NET Idiomatic:** Follow .NET naming conventions (PascalCase for methods and properties).
    *   **Immutability where possible:** For result objects, prefer immutable properties.
    *   **Fluent API (Optional):** Consider if a fluent API makes sense for certain operations, but don't force it if it complicates things.
    *   **Extensibility:** Think about how new distributions, tests, or models could be added in the future (e.g., interfaces if appropriate, but not strictly necessary for MVP).

**6. Development & Build:**

    *   **Source Control:** Use Git.
    *   **Build System:** Standard .NET SDK (`dotnet build`, `dotnet test`, `dotnet pack`).
    *   **Unit Testing Framework:** MSTest, NUnit, or xUnit.

**7. Future Considerations (Post-MVP):**

    *   More distributions (F, Chi-squared, Gamma, Beta, etc.)
    *   More hypothesis tests (ANOVA, Mann-Whitney U, Wilcoxon Signed-Rank, Shapiro-Wilk for normality)
    *   Multiple Linear Regression
    *   Generalized Linear Models (GLMs) (e.g., Logistic Regression)
    *   Time Series basic tools (e.g., ACF, PACF)
    *   Clustering algorithms (e.g., k-means)
    *   Handling of weighted data
    *   Integration with data frame-like structures (if a popular .NET one emerges or you build a minimal one)

**8. Key Challenges & Learning Opportunities:**

    *   Understanding the mathematical formulas and algorithms correctly.
    *   Implementing numerically stable algorithms.
    *   Designing a clean and usable API.
    *   Thorough testing and validation against known results (e.g., from R itself).
using Fractions;

namespace ConsoleCalculator;

internal abstract class ConsoleCalculator
{
    private static void Main(string[] args)
    {
        var calculator = new BasicCalculatorProgram();
        calculator.Run();
    }
}
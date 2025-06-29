using Fractions;

namespace ConsoleCalculator;

public class BasicCalculatorProgram : ICalculatorProgram
{
    public void Run()
    {
        Console.WriteLine("\u2797 \u2795 \u2796 Calculator App \u2797 \u2795 \u2796");
        var number1 = AskForNumber("First number: "); 
        var operation = AskForOperation("operation: "); 
        var number2 = AskForNumber("Second number: "); 
        var result = operation(number1, number2);
        Console.WriteLine($"Result: {result}");
    }

    private static Fraction AskForNumber(string message)
    {
        // Probably this method is not easy to test due Console operations.
        // The only important method is Fraction.TryParse(), which is from 
        // an external library.
        
        var isCorrectInput = false;
        Fraction number;
        
        do
        {
            Console.Write(message);
            var userResponse = Console.ReadLine();
            isCorrectInput = Fraction.TryParse(userResponse, out var fraction);    
            number = fraction;
        } while (!isCorrectInput);

        return number;
    }

    private static Func<Fraction, Fraction, Fraction> AskForOperation(string message)
    {
        // The same, this isn't easy to test, I like it returns a function though.
        
        var isCorrectInput = false;
        var allowedOperations = new HashSet<string>()
        {
            "+", "/", "-", "*"
        };
        var operation = "";
        while (!isCorrectInput)
        {
            Console.Write(message);
            var userResponse = Console.ReadLine();
            if (userResponse == null || !allowedOperations.Contains(userResponse.Trim())) continue;
            isCorrectInput = true;
            operation = userResponse;
        }

        return operation switch
        {
            "+" => (x, y) => x + y,
            "-" => (x, y) => x - y,
            "*" => (x, y) => x * y,
            _ => (x, y) => x / y,
        };
    }
}
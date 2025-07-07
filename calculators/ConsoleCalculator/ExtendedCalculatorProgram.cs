using Fractions;

namespace ConsoleCalculator;

public class ExtendedCalculatorProgram : ICalculatorProgram
{

    private Fraction _memory = Fraction.Zero;
    private Fraction _firstOperand = Fraction.Zero;
    private Fraction _secondOperand = Fraction.Zero;

    private enum InputNumber
    {
        One = 1,
        Two = 2
    }

    public void Run()
    {
        Console.WriteLine("Calculator");
        Console.WriteLine("Memory operations:");
        Console.WriteLine("\tM+ : Adds the previous value up to the memory");
        Console.WriteLine("\tM- : Subtracts the previous value to the memory");
        Console.WriteLine("\tMS : Puts the previous value into the memory");
        Console.WriteLine("\tMR : Uses the memory's value as input");
        Console.WriteLine("\tMC : Sets memory's value as Zero");
        Console.WriteLine("Numerical operations:");
        Console.WriteLine("\t+, *, -, /");
        Console.WriteLine("Inputs:");
        Console.WriteLine("\tRational numbers");
        Console.WriteLine("\tIf your Input [1] is empty, it takes Result's value\n");
        
        while (true)
        {
            _firstOperand = AskForInput(_firstOperand, InputNumber.One); 
            var operation = AskForOperation(_firstOperand);
            _secondOperand = AskForInput(Fraction.Zero, InputNumber.Two);
            var result = operation(_firstOperand, _secondOperand);
            Console.WriteLine("Result: " + result.ToString());
            _firstOperand = result;
        }
    }

    
    /// <summary>
    /// Asks for a numerical input. It can also receive:
    /// <list type="bullet">
    /// <item>MC for memory clear.</item>
    /// <item>MR for memory recall.</item>
    /// </list>
    /// </summary>
    /// <param name="defaultValue">The method returns this defaultValue if
    /// the user's input is empty.</param>
    /// <param name="inputNumber">Whether is the first operand or second operand.</param>
    /// <returns>A fraction that it's the user's input.</returns>
    private Fraction AskForInput(Fraction defaultValue, InputNumber inputNumber)
    {
        var inputNumberValue = inputNumber == InputNumber.One ? 1 : 2;
        var inputMessage = $"Input [{inputNumberValue}]: ";
        Console.Write(inputMessage);
        var value = Console.ReadLine()?.Trim();

        Fraction number;
        if (value == "MC")
        {
            _memory = Fraction.Zero;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Memory: " + Fraction.Zero);
            Console.ResetColor();
            return AskForInput(defaultValue, inputNumber);
        }
        else if (value == "MR")
        { 
            number = _memory; 
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(inputMessage + number);
            Console.ResetColor();
        } 
        else if (value == "")
        {
            number = defaultValue;
        }
        else if (Fraction.TryParse(value, out var fraction))
        {
            number = fraction;
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(inputMessage + value);
            Console.ResetColor();
            return AskForInput(defaultValue, inputNumber);
        }
        return number;
    }

    /// <summary>
    /// Asks the user for an operation. For instance: *,-,+,/.
    /// As side effect, it can receive memory operations M+, M- and MS that
    /// interacts with the first operand.
    /// </summary>
    /// <param name="firstOperand">The first operand that could interact with memory operations.</param>
    /// <returns></returns>
    private Func<Fraction, Fraction, Fraction> AskForOperation(Fraction firstOperand)
    {
       const string inputMessage = "Operation: "; 
       Console.Write(inputMessage);
       var value = Console.ReadLine()?.Trim();

       if (value == "M+")
       {
           _memory += firstOperand;
           Console.ForegroundColor = ConsoleColor.Green;
           Console.WriteLine("Memory: " + _memory);
           Console.ResetColor();
           return AskForOperation(firstOperand);
       }
       else if (value == "M-")
       {
           _memory -= firstOperand;
           Console.ForegroundColor = ConsoleColor.Green;
           Console.WriteLine("Memory: " + _memory);
           Console.ResetColor();
           return AskForOperation(firstOperand);
       }
       else if (value == "MS")
       {
           _memory = firstOperand;
           Console.ForegroundColor = ConsoleColor.Green;
           Console.WriteLine("Memory: " + _memory);
           Console.ResetColor();
           return AskForOperation(firstOperand);
       }

       var operations = new HashSet<string> { "+", "-", "*", "/" };
       if (value != null && operations.Contains(value))
       {
           return ParseOperation(value);
       }
           
       Console.ForegroundColor = ConsoleColor.Red;
       Console.WriteLine(inputMessage + value);
       Console.ResetColor();
       return AskForOperation(firstOperand);

    }
    
    private static Func<Fraction, Fraction, Fraction> ParseOperation(string operation)
    {
        return operation switch
        {
            "+" => (x, y) => x + y,
            "-" => (x, y) => x - y,
            "*" => (x, y) => x * y,
            _ => (x, y) => x / y,
        };
    }

}
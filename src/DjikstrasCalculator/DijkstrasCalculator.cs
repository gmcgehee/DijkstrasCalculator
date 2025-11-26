using QueueLib;
using Stacking;

class MyCalculator {

    /// <summary>
    /// Verifies that the parenthsis are balanced
    /// </summary>
    /// <param name="equation"></param>
    /// <returns>true if balanced, false if unbalanced</returns>
    public static bool CheckParenthesis(string equation) {
        // Creates the queue to which values will be pushed

        MyStack<char> par = new MyStack<char>();

        // Checks if char is (, if so, pushes, and if ), compares to par.Pop(), which is the last value.
        foreach (char c in equation) {

            if (c == '(') {
                par.Push(c);
            }
            else if (c == ')') {
                try {
                    if (par.Pop() != '(') {
                        return false;
                    }
                }
                catch (System.IndexOutOfRangeException e) { return false; }

            }
        }

        // Makes sure there aren't any remaining opening parenthesis at the end
        try {
            if (par.Pop() == '(') {
                return false;
            }
        }
        catch (System.IndexOutOfRangeException) { return true; }

        return true;
    }

    /// <summary>
    /// Converts the equation to a Queue, making it readable to Compute()
    /// </summary>
    /// <param name="equation"></param>
    /// <returns>The equation as a queue</returns>
    /// <exception cref="ArgumentException"></exception>
    public static MyQueue<string> ParseEquation(string equation) {
        // Splits the equation array and then enqueues every value into a queue...pretty self-explanatory
        if (CheckParenthesis(equation)) {
            string[] par = equation.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            MyQueue<string> tokens = new MyQueue<string>(par);

            return tokens;
        }
        else {
            MyQueue<string> tokens = new MyQueue<string>();
            return tokens;
        }
    }

    /// <summary>
    /// Computes the end value of the equation
    /// </summary>
    /// <param name="tokens"></param>
    /// <returns>The final evaluation of the equation</returns>
    /// <exception cref="ArgumentException"></exception>
    public static string Compute(MyQueue<string> tokens) {

        try {
            if (tokens.Peek() == default) {
                return "Parenthesis are unbalanced! Try again!";
            }
        }
        catch (System.InvalidOperationException e) {
            return "Parenthesis are unbalanced! Try again!";
        }
        // Creates two stacks: valueStack, which holds the numbers, and tokenStack, which holds the operators
        MyStack<string> valueStack = new MyStack<string>();
        MyStack<string> tokenStack = new MyStack<string>();

        // Unqueues and checks which each value from the Queue is. Pushes it to the according stack and, in the case of a ), evaluates accordingly.
        try {
            bool noP = true;
            foreach (string s in tokens) {

                // Tests if it is possible to convert to double, if not, just continue with the code. The "test" variable is just filler for the out param
                double test = 0;
                if (double.TryParse(s, out test)) {
                    valueStack.Push(s);
                }
                else if ((s == "+") || (s == "-") || (s == "*") || (s == "/") || (s == "**") || (s == "sqrt") || (s == "tan") || (s == "cos") || (s == "sin")) {
                    tokenStack.Push(s);
                }
                else if (s == ")") {
                    noP = false;
                    if (tokenStack.IsEmpty()) {
                        return "Invalid equation! There are more ) than operators! Try again!";
                    }
                    string act = tokenStack.Pop();
                    if (act == "+") {
                        double answer = Convert.ToDouble(valueStack.Pop()) + Convert.ToDouble(valueStack.Pop());
                        valueStack.Push(Convert.ToString(answer));
                    }
                    else if (act == "-") {
                        // Uses temp to store that the first value we Pop() is actually being subtracted from the other value. Process is repeated throughout the code.
                        double temp = Convert.ToDouble(valueStack.Pop());
                        double answer = Convert.ToDouble(valueStack.Pop()) - temp;
                        valueStack.Push(Convert.ToString(answer));
                    }
                    else if (act == "*") {
                        double answer = Convert.ToDouble(valueStack.Pop()) * Convert.ToDouble(valueStack.Pop());
                        valueStack.Push(Convert.ToString(answer));
                    }
                    else if (act == "/") {
                        double temp = Convert.ToDouble(valueStack.Pop());
                        double answer = Convert.ToDouble(valueStack.Pop()) / temp;
                        valueStack.Push(Convert.ToString(answer));
                    }
                    else if (act == "**") {
                        double temp = Convert.ToDouble(valueStack.Pop());
                        double temp2 = Convert.ToDouble(valueStack.Pop());
                        double answer = Math.Pow(temp2, temp);
                        valueStack.Push(Convert.ToString(answer));
                    }
                    else if (act == "sqrt") {
                        double answer = Math.Sqrt(Convert.ToDouble(valueStack.Pop()));
                        valueStack.Push(Convert.ToString(answer));

                    }
                    else if (act == "tan") {

                        double answer = Math.Tan(Convert.ToDouble(valueStack.Pop()) * (Math.PI / 180));
                        valueStack.Push(Convert.ToString(answer));

                    }
                    else if (act == "cos") {
                        double answer = Math.Cos(Convert.ToDouble(valueStack.Pop()) * (Math.PI / 180));
                        valueStack.Push(Convert.ToString(answer));
                    }
                    else if (act == "sin") {
                        double answer = Math.Sin(Convert.ToDouble(valueStack.Pop()) * (Math.PI / 180));
                        valueStack.Push(Convert.ToString(answer));
                    }

                }
                else if (s == "(") ; // Does nothing in the case of ( 
                else {
                    return "Invalid character! Try again!";
                }

            }

            if (noP) {
                return "Invalid equation! No parenthesis. Try again!";
            }
            return valueStack.Pop();
        }
        catch (System.IndexOutOfRangeException e) { // Final just-in-case if there are more operators than ) 
            return "Invalid equation! One of the operations cannot be calculated.";
        }
    }


}

class Program {
    static void Main(string[] args) {

        // Checks if an equation is entered as a command line argument
        if (args.Length > 0) {
            Console.WriteLine(MyCalculator.Compute(MyCalculator.ParseEquation(args[0])));
        }

        // Always runs -- continuous calculator
        string equation = "";
        while (true) {
            Console.Write("Enter values to calculate: ");
            equation = Console.ReadLine();
            if (equation == "quit" || equation == "exit") {
                Console.WriteLine("Program ended.");
                break;
            }
            Console.WriteLine(MyCalculator.Compute(MyCalculator.ParseEquation(equation)));
        }
    }
}


# DjikstrasCalculator
A C#/.NET implementation of using Djikstra's calculator algorithm.

## Installation
Run the following command to download the project.
```bash
git clone https://github.com/gmcgehee/DjikstrasCalculator.git
```

## Usage
This is a dotnet project. [Install the dotnet development platform](https://dotnet.microsoft.com/en-us/download) in order to use this project.

In order to run the projct, first navigate to src/DjikstrasCalculator:
```bash
cd src/DjikstrasCalculator
```

Then run the following command to build and run the project:
```bash
dotnet run
```

The following prompt should appear:
```bash
Enter values to calculate:
```

The syntax for this calculator is very precise. All expressions and sub-expressions must be enclosed in parenthesis, and every symbol must have delineating spaces. The calculator supports addition, subtraction, multiplication, division, sin, and cosine.

A valid equation might look like this:

```bash
( ( 5 + 4 ) ** sin ( 12 ) )
```

Press `enter` to run the calculation.

```bash
Enter values to calculate: ( ( 5 + 4 ) ** sin ( 12 ) )
1.579058332032481
```

Type `quit` or `exit` to exit the program gracefully.
```bash
Enter values to calculate: ( ( 5 + 4 ) ** sin ( 12 ) )
1.579058332032481
Enter values to calculate: quit
Program ended.
```


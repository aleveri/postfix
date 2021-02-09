using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PostFix
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] lines = File.ReadAllText("input.txt").Split("\r\n");

            string output = string.Empty;

            foreach (string line in lines)
            {
                line.Trim();

                if (!line.Equals("q"))
                {
                    (IEnumerable<double>, IEnumerable<string>) cleanLine = SplitLine(line);

                    int index = 0;

                    double result = 0;

                    foreach (string singleOperator in cleanLine.Item2)
                    {
                        switch (singleOperator)
                        {
                            case "+":
                                if (result == 0)
                                {
                                    result = cleanLine.Item1.ElementAt(index) + cleanLine.Item1.ElementAt(index + 1);
                                    index += 2;
                                }
                                else
                                {
                                    result += cleanLine.Item1.ElementAt(index);
                                    index++;
                                }
                                break;

                            case "-":
                                if (result == 0)
                                {
                                    result = result - cleanLine.Item1.ElementAt(index) + cleanLine.Item1.ElementAt(index + 1);
                                    index += 2;
                                }
                                else
                                {
                                    result -= cleanLine.Item1.ElementAt(index);
                                    index++;
                                }
                                break;

                            case "*":
                                if (result == 0)
                                {
                                    result = cleanLine.Item1.ElementAt(index) * cleanLine.Item1.ElementAt(index + 1);
                                    index += 2;
                                }
                                else
                                {
                                    result *= cleanLine.Item1.ElementAt(index);
                                    index++;
                                }
                                break;

                            case "/":
                                if (result == 0)
                                {
                                    result = cleanLine.Item1.ElementAt(index) / cleanLine.Item1.ElementAt(index + 1);
                                    index += 2;
                                }
                                else
                                {
                                    result /= cleanLine.Item1.ElementAt(index);
                                    index++;
                                }
                                break;

                            default:
                                break;
                        }
                    }

                    output = output.Equals(string.Empty) ? output + result : output + "\r\n" + result;
                }
            }

            Console.WriteLine(output);
        }

        private static (IEnumerable<double>, IEnumerable<string>) SplitLine(string line)
        {
            List<double> numbers = new List<double>();

            List<string> operators = new List<string>();

            string[] lines = line.Split(" ");

            for (int i = 0; i < lines.Length; i++)
            {
                if (double.TryParse(lines[i], out double number))
                    numbers.Add(number);
                else
                    operators.Add(lines[i]);
            }

            return (numbers, operators);
        }

    }
}

using System;
using System.Text.RegularExpressions;

public class Program
{
    static double Solve(string equation)
    {
        int w = 0;
        double a = 0;
        if (equation[equation.Length - 1] == '-')
        {
            equation = "0" + equation + "0";
        }
        else
        {
            equation = "0+" + equation + "+0";
        }
        char[] mun = equation.ToCharArray();
        string[] num = new string[mun.Length];

        for (int i = 0; i < mun.Length; i++)
        {
            num[i] = mun[i].ToString().ToLowerInvariant();
        }
        for (int i = 0; i < num.Length; i++)
        {
            try
            {
                if (Int32.TryParse(num[i], out w) && Int32.TryParse(num[i + 1], out w))
                {
                    if (Int32.TryParse(num[i + 1], out w) && Int32.TryParse(num[i + 2], out w))
                    {
                        if (Int32.TryParse(num[i + 2], out w) && Int32.TryParse(num[i + 3], out w))
                        {
                            num[i] = num[i] + num[i + 1] + num[i + 2] + num[i + 3];
                            num[i + 1] = "";
                            num[i + 2] = "";
                            num[i + 3] = "";
                        }
                        else
                        {
                            num[i] = num[i] + num[i + 1] + num[i + 2];
                            num[i + 1] = "";
                            num[i + 2] = "";

                        }
                    }
                    else
                    {
                        num[i] = num[i] + num[i + 1];
                        num[i + 1] = "";
                    }
                }
            }
            catch { }
        }
        for (int y = 0; y < num.Length; y++)
        {
            if (num[y] == "+" || (num[y] == "/") || (num[y] == "x") || (num[y] == "-") || (num[y] == "!"))
            {
                //do nothing and cry
            }
            else
            {
                try
                {
                    if (num[y - 1] == "!")
                    {
                        int fact = Int32.Parse(num[y]);
                        for (int i = fact - 1; i > 0; i--)
                        {
                            fact *= i;
                        }

                        a += fact;
                    }

                    if (num[y - 1] == "+")
                    {
                        a += Int32.Parse(num[y]);
                    }

                    if (num[y] == "^")
                    {
                        a = Math.Pow(a, Int32.Parse(num[y + 1]));
                    }

                    if (num[y - 1] == "x")
                    {
                        a *= Int32.Parse(num[y]);
                    }

                    if (num[y - 1] == "/")
                    {
                        a /= Int32.Parse(num[y]);
                    }

                    if (num[y - 1] == "-")
                    {
                        a -= Int32.Parse(num[y]);
                    }
                }
                catch
                {
                    try
                    {
                        a += Int32.Parse(num[y]);
                    }
                    catch { }
                }
            }
        }
        return a;
    }
    static double splitItUp(string equation)
    {
        int w = 0;
        string eq = "";
        equation += "+0";
        char[] mun = equation.ToCharArray();
        string[] num = new string[mun.Length];
        for (int i = 0; i < mun.Length; i++)
        {
            num[i] = mun[i].ToString().ToLowerInvariant();
        }
        for (int i = 0; i < num.Length; i++)
        {
            try
            {
                if (Int32.TryParse(num[i], out w) && Int32.TryParse(num[i + 1], out w))
                {
                    if (Int32.TryParse(num[i + 1], out w) && Int32.TryParse(num[i + 2], out w))
                    {
                        if (Int32.TryParse(num[i + 2], out w) && Int32.TryParse(num[i + 3], out w))
                        {
                            num[i] = num[i] + num[i + 1] + num[i + 2] + num[i + 3];
                            num[i + 1] = "";
                            num[i + 2] = "";
                            num[i + 3] = "";
                        }
                        else
                        {
                            num[i] = num[i] + num[i + 1] + num[i + 2];
                            num[i + 1] = "";
                            num[i + 2] = "";

                        }
                    }
                    else
                    {
                        num[i] = num[i] + num[i + 1];
                        num[i + 1] = "";
                    }
                }
            }
            catch { }
        }
        for (int i = 0; i < num.Length; i++)
        {
            if (num.ToString().ToLowerInvariant().Contains("x") && num.ToString().ToLowerInvariant().Contains("/") && !(num[i].ToString().ToLowerInvariant().Contains("-") && num[i].ToString().ToLowerInvariant().Contains("+")))
            {
                Solve(num.ToString());
            }
            else if (num.ToString().ToLowerInvariant().Contains("+") && num.ToString().ToLowerInvariant().Contains("-") && !(num[i].ToString().ToLowerInvariant().Contains("x") && num[i].ToString().ToLowerInvariant().Contains("-")))
            {
                Solve(num.ToString());
            }
            else
            {
                switch (num[i].ToString().ToLowerInvariant())
                {
                    case ("("):
                        num[i] = "";
                        int original = i;
                        string innerEq = "";
                        while (!(num[original] == ")"))
                        {
                            try
                            {
                                if (Int32.TryParse(num[i], out w))
                                {
                                    innerEq += splitItUp(num[original]).ToString();
                                }
                                else
                                {
                                    innerEq += num[original];
                                }
                                num[original] = "";
                                original++;
                                //Console.WriteLine(innerEq); //For Debugging purposes.

                            }
                            catch { }
                        }
                        num[original] = splitItUp(innerEq).ToString();
                        num[i] = "";
                        break;

                    case ("^"):
                        if (num[i + 1].Contains("(") || num[i - 1].Contains(")"))
                        {

                        }
                        else
                        {
                            string s = num[i - 1] + num[i] + num[i + 1];
                            num[i] = Solve(s).ToString();
                            num[i - 1] = "";
                            num[i + 1] = "";
                        }
                        break;
                    case ("x"):
                        if (num.ToString().ToLowerInvariant().Contains("x") && num.ToString().ToLowerInvariant().Contains("/") && !(num[i].ToString().ToLowerInvariant().Contains("-") && num[i].ToString().ToLowerInvariant().Contains("+")))
                        {
                            num[i + 1] += "'";
                            num[i - 2] += "'";
                        }
                        else if (num.ToString().ToLowerInvariant().Contains("+") && num.ToString().ToLowerInvariant().Contains("-") && !(num[i].ToString().ToLowerInvariant().Contains("x") && num[i].ToString().ToLowerInvariant().Contains("-")))
                        {
                            num[i + 1] += "'";
                            num[i - 2] += "'";
                        }
                        break;
                    case ("/"):
                        if (num.ToString().ToLowerInvariant().Contains("x") && num.ToString().ToLowerInvariant().Contains("/") && !(num[i].ToString().ToLowerInvariant().Contains("-") && num[i].ToString().ToLowerInvariant().Contains("+")))
                        {
                            num[i + 1] += "'";
                            num[i - 2] += "'";
                        }
                        else if (num.ToString().ToLowerInvariant().Contains("+") && num.ToString().ToLowerInvariant().Contains("-") && !(num[i].ToString().ToLowerInvariant().Contains("x") && num[i].ToString().ToLowerInvariant().Contains("-")))
                        {
                            num[i + 1] += "'";
                            num[i - 2] += "'";
                        }
                        break;
                    case ("+"):
                        num[i] += "'";
                        break;
                    case ("-"):
                        try
                        {
                            num[i - 1] += "'";
                        }
                        catch { }
                        break;
                }
            }
        }
        for (int i = 0; i < num.Length; i++)
        {
            eq += num[i];
        }
        String[] newEq = Regex.Split(eq, "'");
        /*for(int i = 0; i < newEq.Length; i++)
               {
                   Console.WriteLine(newEq[i]);//Debugging purposes
             }*/
        for (int i = 0; i < newEq.Length; i++)
        {
            if (newEq[i].Contains("^"))
            {
                newEq[i] = Solve(newEq[i]).ToString();
            }
        }
        for (int i = 0; i < newEq.Length; i++)
        {
            if (newEq[i].Contains("x"))
            {
                newEq[i] = Solve(newEq[i]).ToString();
            }
        }
        for (int i = 0; i < newEq.Length; i++)
        {
            if (newEq[i].Contains("/"))
            {
                newEq[i] = Solve(newEq[i]).ToString();
            }
        }
        for (int i = 0; i < newEq.Length; i++)
        {
            if (newEq[i].Contains("+"))
            {
                newEq[i] = Solve(newEq[i]).ToString();
            }
        }
        double a = 0;
        for (int i = 0; i < newEq.Length; i++)
        {
            if (newEq[i] == "-")
            {
                newEq[i] = newEq[i - 1] + newEq[i] + newEq[i + 1];
                newEq[i - 1] = "";
                newEq[i + 1] = "";
                newEq[i] = Solve(newEq[i]).ToString();
            }
        }
        for (int i = 0; i < newEq.Length; i++)
        {
            try
            {
                a += Double.Parse(newEq[i]);
            }
            catch { }
        }
        return a;
    }
    public static void Main()
    {
        while (true)
        {
            string a = Console.ReadLine();
            Console.WriteLine(splitItUp(a));
        }

    }
}

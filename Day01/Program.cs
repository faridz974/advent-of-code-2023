using System.Diagnostics;

var content = File.ReadAllLines("input.txt");

static int GetPartOne(string[] content)
{
    var parsedLines = new List<int>(content.Length);
    foreach (var line in content)
    {
        var parsedNumbers = line
            .Where(c => char.IsDigit(c))
            .Select(c => c.ToString())
            .ToList();
        parsedLines.Add(int.Parse(parsedNumbers.First() + parsedNumbers.Last()));
    }
    return parsedLines.Sum();
}


static int GetPartTwo(string[] content)
{
    var parsedLines = new List<int>(content.Length);
    foreach (var line in content)
    {
        var numbers = new List<string>();
        for (int current = 0; current < line.Length; current++)
        {
            var character = line[current];
            if (char.IsDigit(character))
            {
                numbers.Add(character.ToString());
            }
            else
            {
                switch (character)
                {
                    case 'e':
                        // eight
                        var (isEightNumber, eightValue) = IsStringNumber("eight", current, line);
                        if (isEightNumber)
                        {
                            numbers.Add(eightValue);
                        }
                        break;
                    case 'f':
                        // four or five
                        var (isFourNumber, fourValue) = IsStringNumber("four", current, line);
                        if (isFourNumber)
                        {
                            numbers.Add(fourValue);
                        }

                        var (isFiveNumber, fiveValue) = IsStringNumber("five", current, line);
                        if (isFiveNumber)
                        {
                            numbers.Add(fiveValue);
                        }
                        break;
                    case 'n':
                        // nine
                        var (isNineNumber, nineValue) = IsStringNumber("nine", current, line);
                        if (isNineNumber)
                        {
                            numbers.Add(nineValue);
                        }
                        break;
                    case 'o':
                        // one
                        var (isOneValue, oneValue) = IsStringNumber("one", current, line);
                        if (isOneValue)
                        {
                            numbers.Add(oneValue);
                        }
                        break;
                    case 't':
                        // two
                        var (isTwoValue, twoValue) = IsStringNumber("two", current, line);
                        if (isTwoValue)
                        {
                            numbers.Add(twoValue);
                        }

                        // three
                        var (isThreeValue, threeValue) = IsStringNumber("three", current, line);
                        if (isThreeValue)
                        {
                            numbers.Add(threeValue);
                        }
                        break;
                    case 's':
                        // six
                        var (isSixValue, sixValue) = IsStringNumber("six", current, line);
                        if (isSixValue)
                        {
                            numbers.Add(sixValue);
                        }

                        // seven
                        var (isSevenValue, sevenValue) = IsStringNumber("seven", current, line);
                        if (isSevenValue)
                        {
                            numbers.Add(sevenValue);
                        }
                        break;
                }
            }
        }

        var number = int.Parse(numbers.First() + numbers.Last());
        parsedLines.Add(number);
    }
    return parsedLines.Sum();

    static (bool isNumber, string numberValue) IsStringNumber(string numberValue, int currentIndex, string text)
    {
        var numbersMap = new Dictionary<string, int>
        {
            ["one"] = 1,
            ["two"] = 2,
            ["three"] = 3,
            ["four"] = 4,
            ["five"] = 5,
            ["six"] = 6,
            ["seven"] = 7,
            ["eight"] = 8,
            ["nine"] = 9
        };
        if (currentIndex + numberValue.Length > text.Length)
        {
            return (false, string.Empty);
        }

        var number = text[currentIndex..(currentIndex + numberValue.Length)];
        if (number == numberValue)
        {
            return (true, numbersMap.GetValueOrDefault(numberValue).ToString());
        }

        return (false, string.Empty);
    }
}

Console.WriteLine("Part 1:");
var stopwatch = Stopwatch.StartNew();
Console.WriteLine(GetPartOne(content));
stopwatch.Stop();
Console.WriteLine($"Elapsed: {stopwatch.ElapsedMilliseconds}ms");

Console.WriteLine("Part 2:");
stopwatch = Stopwatch.StartNew();
Console.WriteLine(GetPartTwo(content));
stopwatch.Stop();
Console.WriteLine($"Elapsed: {stopwatch.ElapsedMilliseconds}ms");
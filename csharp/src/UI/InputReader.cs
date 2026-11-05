
namespace Tait.UI;

public sealed class InputReader
{
    public int ReadAnswer(int max, bool allowBack)
    {
        while (true)
        {
            string inp = Console.ReadLine() ?? string.Empty;

            if (int.TryParse(inp, out int answer))
            {
                if (allowBack && answer == 0 )
                {
                    return -1;
                }

                if (answer >= 1 && answer <= max) return answer - 1;
            }

            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write($"Unknown answer option. \nEnter a number from 1 to {max}\n");
            Console.ResetColor();
        }
    }
}
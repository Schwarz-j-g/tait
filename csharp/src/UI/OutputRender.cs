
using Tait.Models;
using Tait.Services;

namespace Tait.UI;

using CN = Console;

public sealed class OutputRender
{
    private const string Separator = "=================================================";

    public void RenderHeader(Config config)
    {
        if (config == null) return;

        CN.Clear();
        CN.ForegroundColor = ConsoleColor.Cyan;
        CN.WriteLine(Separator);
        CN.WriteLine($" {config.Title.ToUpperInvariant()}");
        CN.WriteLine(Separator);
        CN.ResetColor();
        CN.WriteLine();
        CN.WriteLine(config.Description);
        CN.WriteLine();
        CN.ForegroundColor = ConsoleColor.DarkGray;
        CN.WriteLine("Press any key to start the test...");
        CN.ResetColor();
        CN.ReadKey(true);
    }

    public void RenderQuestion(Question question,  int currentPos, int totalQuestions)
    {
        if (question == null) return;

        CN.Clear();
        CN.ForegroundColor = ConsoleColor.Yellow;
        CN.WriteLine($"Question {currentPos} from {totalQuestions}");
        CN.ForegroundColor = ConsoleColor.White;
        CN.WriteLine(question.Text);
        CN.ForegroundColor = ConsoleColor.DarkGray;
        CN.WriteLine(Separator);
        CN.ResetColor();

        for (int i = 0; i < question.Answers.Count; i++)
        {
            CN.WriteLine($"{i + 1}. {question.Answers[i].Text}");
        }

        if (currentPos > 1)
        {
            CN.ForegroundColor = ConsoleColor.DarkGray;
            CN.WriteLine("0. Return to previous question");
            CN.ResetColor();
        }

        CN.WriteLine();
        CN.ForegroundColor = ConsoleColor.Green;
        CN.Write("Ответ: ");
        CN.ResetColor();
    }

    public void RenderResult(Result result)
    {
        if (result == null) return;

        CN.Clear();
        CN.ForegroundColor = ConsoleColor.Magenta;
        CN.WriteLine(Separator);
        CN.WriteLine("      РЕЗУЛЬТАТ ТЕСТИРОВАНИЯ");
        CN.WriteLine(Separator);
        CN.ResetColor();
        CN.WriteLine();
        
        CN.ForegroundColor = ConsoleColor.Yellow;
        CN.WriteLine(result.Name);
        CN.ResetColor();
        
        CN.WriteLine();
        CN.WriteLine(result.Description);
        CN.WriteLine();
        
        CN.ForegroundColor = ConsoleColor.DarkGray;
        CN.WriteLine(Separator);
        CN.WriteLine("Press any key to exit...");
        CN.ResetColor();
        CN.ReadKey(intercept: true);
    }

    public void RenderError(string errorMessage)
    {
        CN.ForegroundColor = ConsoleColor.Red;
        CN.Error.WriteLine();
        CN.Error.WriteLine("ERROR");
        CN.Error.WriteLine(errorMessage);
        CN.Error.WriteLine();
        CN.ResetColor();
    }
}
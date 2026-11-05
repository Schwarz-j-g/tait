using Tait.Models;
using Tait.UI;

namespace Tait.Services;

public sealed class Manager
{
    private readonly InputReader _reader = new InputReader();
    private readonly OutputRender _render = new OutputRender();
    private readonly ScoreCalculator _calc = new ScoreCalculator();

    public void Full(string path)
    {
        Config cfg = Config.FromFile(path);

        if (cfg.Questions.Count == 0 || cfg.Results.Count == 0)
        {
            throw new InvalidOperationException("Interactive test configuration cannot be empty");
        }

        _render.RenderHeader(cfg);

        int totalQuestions = cfg.Questions.Count;

        List<Answer?> answers = new List<Answer?>(totalQuestions);

        for (int i = 0; i < totalQuestions; i++)
        {
            answers.Add(null);
        } 

        int currentQuestionIdx = 0;

        while (currentQuestionIdx < totalQuestions)
        {
            Question question = cfg.Questions[currentQuestionIdx];
            int displayNum = currentQuestionIdx + 1;
            bool allowBack = currentQuestionIdx > 0;

            _render.RenderQuestion(question, displayNum, totalQuestions);

            int choice = _reader.ReadAnswer(question.Answers.Count, allowBack);

            if (choice == -1)
            {
                currentQuestionIdx--;
            }
            else
            {
                answers[currentQuestionIdx] = question.Answers[choice];
                currentQuestionIdx++;
            }
        }

        List<Answer> final = new List<Answer>(totalQuestions);
        for (int i = 0; i < totalQuestions; i++)
        {
            if (answers[i] != null) final.Add(answers[i]!);
        }

        Result res = _calc.CalculateResult(final, cfg.Results);
        _render.RenderResult(res);
    }
}
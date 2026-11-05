
using Tait.Models;

namespace Tait.Services;

public sealed class ScoreCalculator
{
    public Result CalculateResult(IReadOnlyList<Answer> answers, IReadOnlyList<Result> results)
    {
        if (answers == null || answers.Count == 0)
        {
            throw new ArgumentException("The answer list cannot be empty", nameof(answers));
        }

        if (results == null || results.Count == 0)
        {
            throw new ArgumentException("The result list cannot be empty", nameof(results));
        }


        Dictionary<string, int> scores = new(results.Count);

        for (int i = 0; i < answers.Count; i++ )
        {
            Answer answer = answers[i];

            foreach (KeyValuePair<string, int> pair in answer.Scores)
            {
                if (scores.TryGetValue(pair.Key, out int currentScore))
                {
                    scores[pair.Key] = currentScore + pair.Value;
                }
                else
                {
                    scores[pair.Key] = pair.Value;
                }
            }
        }


        string winningId = string.Empty;
        int maxScore = 0;

        foreach (KeyValuePair<string, int> pair in scores)
        {
            if (pair.Value > maxScore)
            {
                maxScore = pair.Value;
                winningId = pair.Key;
            }
        }


        for (int i = 0; i < results.Count; i++)
        {
            if (string.Equals(results[i].Id, winningId, StringComparison.OrdinalIgnoreCase))
            {
                return results[i];
            }
        }

        throw new ArgumentException($"The type '{winningId}' was defined, but its description is missing");
    }
}
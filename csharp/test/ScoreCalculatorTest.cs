

using System.Collections.Immutable;
using Tait.Models;
using Tait.Services;
using Xunit;

namespace Tait.Tests;

public sealed class ScoreCalculatorTest
{
    private readonly ScoreCalculator _cacl = new ScoreCalculator();

    private static IReadOnlyList<Result> GetResults() => new List<Result>
    {
        new Result { Id = "id1", Name = "Name1", Description = "Desc1" },
        new Result { Id = "id2", Name = "Name2", Description = "Desc2" },
        new Result { Id = "id3", Name = "Name3", Description = "Desc3" }
    };

    [Fact]
    public void CalculateResul_ReturnDominantResult()
    {
        IReadOnlyList<Result> results = GetResults();

        IReadOnlyList<Answer> answers = new List<Answer>
        {
            new Answer { Scores = new Dictionary<string, int> { { "id1", 3 } }.ToImmutableDictionary() },    
            new Answer { Scores = new Dictionary<string, int> { { "id1", 3 }, { "id2", 2 } }.ToImmutableDictionary() },    
        };

        Result res = _cacl.CalculateResult(answers, results);

        Assert.NotNull(res);
        Assert.Equal("id1", res.Id);
    } 

    [Fact]
    public void CalculateResult_DrawSituaton_ResultFirstOccuredId()
    {
        IReadOnlyList<Result> results = GetResults();

        IReadOnlyList<Answer> answers = new List<Answer>
        {
            new Answer { Scores = new Dictionary<string, int> { { "id1", 3 } }.ToImmutableDictionary() },    
            new Answer { Scores = new Dictionary<string, int> { { "id3", 3 }}.ToImmutableDictionary() },    
        };

        Result res = _cacl.CalculateResult(answers, results);

        Assert.NotNull(res);
        Assert.Equal("id1", res.Id);
    }


    [Fact]
    public void CalculateResult_EmptyAnswer()
    {
        IReadOnlyList<Answer> a = Array.Empty<Answer>();
        IReadOnlyList<Result> r = GetResults();

        Assert.Throws<ArgumentException>(() => _cacl.CalculateResult(a, r));
    }

    [Fact]
    public void CalculateResult_EmptyResults()
    {
        IReadOnlyList<Result> r = Array.Empty<Result>();

        IReadOnlyList<Answer> a = new List<Answer>
        {
            new Answer { Scores = new Dictionary<string, int> { { "id3", 3 }}.ToImmutableDictionary() }
        };
    } 

    [Fact]
    public void CalculateResult_WinningMissingDescription()
    {
        IReadOnlyList<Result> r = GetResults();
        IReadOnlyList<Answer> a = new List<Answer>
        {
            new Answer { Scores = new Dictionary<string, int> { { "id0", 3 }}.ToImmutableDictionary() }
        };

        ArgumentException exception = Assert.Throws<ArgumentException>(() => 
            _cacl.CalculateResult(a, r));
        
        Assert.Contains("The type 'id0' was defined, but its description is missing", exception.Message, StringComparison.OrdinalIgnoreCase);
    }
}
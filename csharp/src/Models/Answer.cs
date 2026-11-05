
using System.Collections.Immutable;
using Newtonsoft.Json;

namespace Tait.Models;

public sealed class Answer
{
    [JsonProperty("text")]
    public string Text { get; init; } = string.Empty;

    [JsonProperty("scores")]
    public IReadOnlyDictionary<string, int> Scores { get; init; } = ImmutableDictionary<string, int>.Empty;
}

using Newtonsoft.Json;

namespace Tait.Models;

public sealed class Question
{
    [JsonProperty("text")]
    public string Text { get; init; } = string.Empty;

    [JsonProperty("answers")]
    public IReadOnlyList<Answer> Answers { get; init; } = Array.Empty<Answer>();
}
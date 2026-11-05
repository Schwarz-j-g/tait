using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Tait.Models;

public sealed class Result
{
    [JsonProperty("id")]
    public string Id { get; init; } = string.Empty;

    [JsonProperty("name")]
    public string Name { get; init; } = string.Empty;

    [JsonProperty("description")]
    public string Description { get; init; } = string.Empty;

}
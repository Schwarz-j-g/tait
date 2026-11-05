using Tait.Models;
using Newtonsoft.Json;

namespace Tait.Services;

public sealed class Config
{
    [JsonProperty("title")]
    public string Title { get; init; } = string.Empty;

    [JsonProperty("description")]
    public string Description { get; init; } = string.Empty;

    [JsonProperty("questions")]
    public IReadOnlyList<Question> Questions { get; init; } = Array.Empty<Question>();

    [JsonProperty("results")]
    public IReadOnlyList<Result> Results { get; init; } = Array.Empty<Result>();

    public static Config FromFile(string path)
    {
        if (string.IsNullOrWhiteSpace(path))
        {
            throw new ArgumentException("The path to the configuration file cannot be empty.", nameof(path));
        }


        using FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
        using StreamReader reader = new StreamReader(stream);
        using JsonTextReader jsonReader = new JsonTextReader(reader);

        JsonSerializer serializer = new JsonSerializer();

        Config? config = serializer.Deserialize<Config>(jsonReader);

        return config ?? throw new JsonException("The configuration file is empty or has an incorrect structure.");
    }  
}
using Newtonsoft.Json;
using Tait.Services;
using Xunit;

namespace Tait.Tests;

public sealed class ConfigTest : IDisposable
{
    private readonly string _path;

    public ConfigTest()
    {
        _path = Path.GetTempFileName();
    }

    public void Dispose()
    {
        if (File.Exists(_path))
        {
            File.Delete(_path);
        }
    }


    [Fact]
    public void FromFile_ReturnCorrectConfig()
    {
        string cnt = """
        {
            "title": "Test",
            "description": "Desc",
            "questions": [
                {
                    "text": "Question 1",
                    "answers": [
                        { "text": "Answer 1", "scores": { "tt": 3 }}
                    ]
                }
            ],
            "results": [
                {
                    "id": "tt",
                    "name": "Name",
                    "description": "Test details"
                }
            ]
        }
        """;
        File.WriteAllText(_path, cnt);

        Config config = Config.FromFile(_path);

        Assert.NotNull(config);
        Assert.Equal("Test", config.Title);
        Assert.Equal("Desc", config.Description);
        Assert.Equal("tt", config.Results[0].Id);
    }

    [Fact]
    public void FromFile_EmptyFile_ThrowJsonException()
    {
        File.WriteAllText(_path, string.Empty);

        Assert.Throws<JsonException>(() => Config.FromFile(_path));
    }

    [Fact]
    public void FromFile_InvalidJson()
    {
        string invalid = "{ \"title\": \" Invalid JSON\" ";
        File.WriteAllText(_path, invalid);

        Assert.Throws<JsonSerializationException>(() => Config.FromFile(_path));
    }

    [Fact]
    public void FromFile_EmptyArgument()
    {
        Assert.Throws<ArgumentException>(() => Config.FromFile("    "));
    }
}
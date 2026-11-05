# tait
**tait** — this is a console application for conducting interactive tests, implemented on the platform .NET. The project is designed using the **Data-Driven** methodology: application logic is separated from all content. The code knows nothing about the test topic - all behavior, questions, scoring scales and results are controlled by an external JSON file.

## Main Features

* **Data Driven на контент**: change the test theme simply by editing the JSON configuration, without recompiling the code.
* **Single-Binary**: compiled into one independent executable file for Windows, Linux and macOS with built-in runtime and IL trimming.

## Quick start

### 1. Loading
Go to the [Releases](../../releases) section of the repository and download the finished compiled file for your operating system.

### 2. Launch
Takes a command and the absolute path to the JSON configuration file as a command line argument. Place the file with the test content next to the program and launch the terminal:

**Windows:**
```powershell
.\tait.exe use test-config.json
```

**Linux**
```bash
chmod +X tait-linux-64
./tait-linux-64 use test-config.json
```

**macOS**
```bash
chmod +X tait-linux-64
./tait-osx-64 use test-config.json
```

## Configuration file format

To create your own test, compose a JSON file in **lowercase** with the following structure. Number of answers and resulting scales **not limited by code**.

```json
{
  "title": "Test name",
  "description": "Welcome description for the user",
  "questions": [
    {
      "text": "Question text?",
      "answers": [
        {
          "text": "Answer option 1",
          "scores": { "id_archetype_a": 3, "id_archetype_b": 0 }
        },
        {
          "text": "Answer option 2",
          "scores": { "id_archetype_b": 4 }
        }
      ]
    }
  ],
  "results": [
    {
      "id": "id_archetype_a",
      "name": "Human archetype name A",
      "description": "Expanded text of the final"
    },
    {
      "id": "id_archetype_b",
      "name": "Human archetype name B",
      "description": "Expanded text of the final"
    }
  ]
}
```

### Markup rules
1. **Allowed data**: if the answer does not assign points to a certain archetype, its key in the `scores` object need not be written (like `archetype_a` in the second answer), since the code will automatically accept them as `0`.
2. **Scales connection**: key lines inside `"scores"` must exactly match the `"id"` field in the `"results"` mass.

## For developers

The project uses a hybrid structure, in which the source code and Unit tests are inside one project.

**Clone the monorepository and go to the directory with the C# implementation**
```bash
git clone https://github.com/Schwarz-j-g/tait
cd csharp
```

**Running unit tests**
```bash 
dotnet test
```

**Running the application locally**
```bash
dotnet run use ../test-config.json
```

**Compiling to one file**
```bash
dotnet publish -c Release -r <your-os> --self-contained true  dotnet publish -c Release -r win-x64 --self-contained true /p:PublishSingleFile=true /p:PublishTrimmed=true /p:IncludeNativeLibrariesForSelfContained=true 
```

## License

The project is distributed under the free **MIT** license. You are free to modify, distribute and use this engine for commercial purposes.
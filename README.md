# tait

Multi-platform monorepository for performing interactive tests.

## Concept

**tait** - this is a project in which logic is completely separated from content. The executing code itself knows nothing about the test topic, number of questions, or resulting scales. All system behavior is controlled by a single external configuration file `test-config.json`

## Repository structure

Each subdirectory contains an isolated implementation of the engine with its own language optimizations, tests and local build tools:

* [**C#**](./csharp/) - Assembly into a single independent binary using IL trimming, built-in runtime, navigation state machine.


## Test generation using AI

```text
Write a valid JSON configuration file for the interactive test.

Test topic: [YOUR TOPIC]
Number of questions: [SPECIFY THE NUMBER]
Number of archetypes/results: [NUMBER]

JSON formatting rules:
1. All keys and string identifiers of scales must be strictly in LOWER CASE.
2. Use a sparse data structure: in the "scores" block inside the answers, indicate ONLY those archetype keys that receive points (>0). If an answer gives 0 points to an archetype, omit this key completely.
3. The texts of questions and answers must be [TARGET LANGUAGE].

Structure template:
{
  "title": "Test name",
  "description": "Welcome description for the user",
  "questions": [
    {
      "text": "Question text?",
      "answers": [
        {
          "text": "Answer option 1",
          "scores": { "id_archetype_1": 3, "id_archetype_2": 1 }
        }
      ]
    }
  ],
  "results": [
    {
      "id": "id_archetype_1",
      "name": "Human archetype name",
      "description": "Expanded text of the final"
    }
  ]
}

Output ONLY pure JSON code without unnecessary text explanations before or after the code.
```


## Installing ready-made binaries

All releases are published in a single releases section`. You can download optimized executable files for your OS on the page [**Releases**](../../releases).

## License

The project is distributed under the free **MIT** license. You are free to modify, distribute and use this engine for commercial purposes.
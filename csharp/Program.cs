

using Newtonsoft.Json;
using Tait.Services;
using Tait.UI;

OutputRender err = new OutputRender();

try
{
    if (args.Length == 0)
    {
        err.RenderError("The path to the configuration file is not specified.\nUse: tait <cofig-path>");
        return;
    }

    string command = args[0];

    if (command != "use") {
        err.RenderError($"\"{command}\" is not an internal command.");
    }

    string configPath = args[1];

    Manager manager = new Manager();
    manager.Full(configPath);
}
catch (FileNotFoundException)
{
    err.RenderError("The specified configuration file was not found.");
}
catch (JsonException ex)
{
    err.RenderError($"Failed to read the config file:\n{ex.Message}");
}
catch (ArgumentException ex)
{
    err.RenderError($"Invalid arguments:\n{ex.Message}");
}
catch (Exception ex)
{
    err.RenderError($"Other error:\n{ex.Message}");
}
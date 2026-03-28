using System;
using System.IO;
using System.Text.Json;

public class AppSettings
{
    public bool AutostartAsked { get; set; } = false;
    public bool FirstRun { get; set; } = true;
    public int TimeoutSeconds { get; set; } = 3;
    public string ProgramList { get; set; } = "outlook,olk,code";
    public bool ExcludeMode { get; set; } = false; // true = exclude, false = include

    public static string SettingsPath => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "settings.json");

    public static AppSettings Load()
    {
        try
        {
            if (File.Exists(SettingsPath))
            {
                var json = File.ReadAllText(SettingsPath);
                return JsonSerializer.Deserialize<AppSettings>(json) ?? new AppSettings();
            }
        }
        catch { }
        return new AppSettings();
    }

    public void Save()
    {
        var json = JsonSerializer.Serialize(this, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(SettingsPath, json);
    }
}

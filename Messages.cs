using System.Text.Json;

public class Messages
{
    public string CurrentLanguage { get; private set; } = "English";

    private Dictionary<string, string> _dictionary = new(StringComparer.OrdinalIgnoreCase);
    private Dictionary<string, string> _raceMap = new(StringComparer.OrdinalIgnoreCase);
    private Dictionary<string, string> _occupationMap = new(StringComparer.OrdinalIgnoreCase);
    private Dictionary<string, string> _displayRaceMap = new(StringComparer.OrdinalIgnoreCase);
    private Dictionary<string, string> _displayOccupationMap = new(StringComparer.OrdinalIgnoreCase);
    private Dictionary<string, string> _displayWeaponMap = new(StringComparer.OrdinalIgnoreCase);

    public Messages()
    {
    }

    public void SetCurrentLanguage(string language)
    {
        CurrentLanguage = language;
    }

    public void ReadDictionary()
    {
        string jsonPath = Path.Combine(AppContext.BaseDirectory, "language_data.json");
        if (!File.Exists(jsonPath))
            throw new FileNotFoundException($"Language file not found at: {jsonPath}");

        string jsonText = File.ReadAllText(jsonPath);
        using JsonDocument doc = JsonDocument.Parse(jsonText);

        if (!doc.RootElement.TryGetProperty(CurrentLanguage, out JsonElement langSection))
        {
            if (!doc.RootElement.TryGetProperty("English", out langSection))
                throw new InvalidOperationException("Default 'English' section not found in language_data.json.");
        }

        _dictionary = LoadSection(langSection, "dictionary");
        _raceMap = LoadSection(langSection, "raceMap");
        _occupationMap = LoadSection(langSection, "occupationMap");
        _displayRaceMap = LoadSection(langSection, "displayRaceMap");
        _displayOccupationMap = LoadSection(langSection, "displayOccupationMap");
        _displayWeaponMap = LoadSection(langSection, "displayWeaponMap");
    }

    private static Dictionary<string, string> LoadSection(JsonElement parent, string propertyName)
    {
        var dict = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        if (parent.TryGetProperty(propertyName, out JsonElement section))
        {
            foreach (JsonProperty entry in section.EnumerateObject())
                dict[entry.Name] = entry.Value.GetString() ?? string.Empty;
        }
        return dict;
    }

    public string GetMessage(string? key)
    {
        if (string.IsNullOrEmpty(key)) return string.Empty;
        return _dictionary.TryGetValue(key, out string? message) ? message : $"[{key}]";
    }

    public bool IsValidRace(string? input)
    {
        if (string.IsNullOrWhiteSpace(input)) return false;
        return _raceMap.ContainsKey(input.Trim());
    }

    public string NormalizeRace(string? input)
    {
        if (string.IsNullOrWhiteSpace(input)) return string.Empty;
        return _raceMap.TryGetValue(input.Trim(), out string? canonical) ? canonical : input;
    }

    public bool IsValidOccupation(string? input)
    {
        if (string.IsNullOrWhiteSpace(input)) return false;
        return _occupationMap.ContainsKey(input.Trim());
    }

    public string NormalizeOccupation(string? input)
    {
        if (string.IsNullOrWhiteSpace(input)) return string.Empty;
        return _occupationMap.TryGetValue(input.Trim(), out string? canonical) ? canonical : input;
    }

    public string TranslateRaceForDisplay(string? race)
    {
        if (string.IsNullOrWhiteSpace(race)) return string.Empty;
        return _displayRaceMap.TryGetValue(race.Trim(), out string? translated) ? translated : race;
    }

    public string TranslateOccupationForDisplay(string? occupation)
    {
        if (string.IsNullOrWhiteSpace(occupation)) return string.Empty;
        return _displayOccupationMap.TryGetValue(occupation.Trim(), out string? translated) ? translated : occupation;
    }

    public string TranslateWeaponForDisplay(string? weaponType)
    {
        if (string.IsNullOrWhiteSpace(weaponType)) return string.Empty;
        return _displayWeaponMap.TryGetValue(weaponType.Trim(), out string? translated) ? translated : weaponType;
    }
}

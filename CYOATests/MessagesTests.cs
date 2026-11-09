// CYOATests/MessagesTests.cs
[TestClass]
public class MessagesTests
{
    [TestMethod]
    public void Constructor_DefaultLanguageIsEnglish()
    {
        Messages messages = new Messages();
        Assert.AreEqual("English", messages.CurrentLanguage);
    }

    [TestMethod]
    public void SetCurrentLanguage_SetsLanguageToSpanish()
    {
        Messages messages = new Messages();
        messages.SetCurrentLanguage("Spanish");
        Assert.AreEqual("Spanish", messages.CurrentLanguage);
    }

    [TestMethod]
    public void SetCurrentLanguage_SetsLanguageToFrench()
    {
        Messages messages = new Messages();
        messages.SetCurrentLanguage("French");
        Assert.AreEqual("French", messages.CurrentLanguage);
    }

    [TestMethod]
    public void SetCurrentLanguage_SetsLanguageToItalian()
    {
        Messages messages = new Messages();
        messages.SetCurrentLanguage("Italian");
        Assert.AreEqual("Italian", messages.CurrentLanguage);
    }

    [TestMethod]
    public void SetCurrentLanguage_SetsLanguageToEnglish()
    {
        Messages messages = new Messages();
        messages.SetCurrentLanguage("Spanish");
        messages.SetCurrentLanguage("English");
        Assert.AreEqual("English", messages.CurrentLanguage);
    }

    [TestMethod]
    public void ReadDictionary_LoadsEnglishWelcomeMessage()
    {
        Messages messages = new Messages();
        messages.SetCurrentLanguage("English");
        messages.ReadDictionary();
        string result = messages.GetMessage("welcome");
        Assert.AreEqual("Welcome to Choose Your Adventure Game", result);
    }

    [TestMethod]
    public void ReadDictionary_LoadsSpanishWelcomeMessage()
    {
        Messages messages = new Messages();
        messages.SetCurrentLanguage("Spanish");
        messages.ReadDictionary();
        string result = messages.GetMessage("welcome");
        Assert.AreEqual("Bienvenido al Juego de Elige Tu Aventura", result);
    }

    [TestMethod]
    public void ReadDictionary_LoadsFrenchWelcomeMessage()
    {
        Messages messages = new Messages();
        messages.SetCurrentLanguage("French");
        messages.ReadDictionary();
        string result = messages.GetMessage("welcome");
        Assert.AreEqual("Bienvenue dans Choisissez Votre Aventure", result);
    }

    [TestMethod]
    public void ReadDictionary_LoadsItalianWelcomeMessage()
    {
        Messages messages = new Messages();
        messages.SetCurrentLanguage("Italian");
        messages.ReadDictionary();
        string result = messages.GetMessage("welcome");
        Assert.AreEqual("Benvenuto a Scegli la Tua Avventura", result);
    }

    [TestMethod]
    public void GetMessage_ReturnsFallbackForUnknownKey()
    {
        Messages messages = new Messages();
        messages.SetCurrentLanguage("English");
        messages.ReadDictionary();
        string result = messages.GetMessage("unknown_key");
        Assert.AreEqual("[unknown_key]", result);
    }

    [TestMethod]
    public void GetMessage_ReturnsDifferentMessageAfterLanguageSwitch()
    {
        Messages messages = new Messages();
        messages.SetCurrentLanguage("English");
        messages.ReadDictionary();
        string english = messages.GetMessage("welcome");

        messages.SetCurrentLanguage("Spanish");
        messages.ReadDictionary();
        string spanish = messages.GetMessage("welcome");

        Assert.AreNotEqual(english, spanish);
    }

    [TestMethod]
    public void ReadDictionary_LoadsGoodbyeMessageInEachLanguage()
    {
        Messages messages = new Messages();

        messages.SetCurrentLanguage("English");
        messages.ReadDictionary();
        Assert.AreEqual("Adventure ends. Bye.", messages.GetMessage("goodbye"));

        messages.SetCurrentLanguage("Spanish");
        messages.ReadDictionary();
        Assert.AreEqual("Adios!", messages.GetMessage("goodbye"));

        messages.SetCurrentLanguage("French");
        messages.ReadDictionary();
        Assert.AreEqual("Au revoir!", messages.GetMessage("goodbye"));

        messages.SetCurrentLanguage("Italian");
        messages.ReadDictionary();
        Assert.AreEqual("Arrivederci!", messages.GetMessage("goodbye"));
    }

    [TestMethod]
    public void ReadDictionary_AllRequiredKeysExistInEnglish()
    {
        Messages messages = new Messages();
        messages.SetCurrentLanguage("English");
        messages.ReadDictionary();

        string[] requiredKeys = { "welcome", "menu",
                                   "choose_path", "path_north", "path_south", "attack_prompt",
                                   "victory", "defeat", "retreat", "invalid", "goodbye" };

        foreach (string key in requiredKeys)
        {
            string result = messages.GetMessage(key);
            Assert.AreNotEqual($"[{key}]", result, $"Key '{key}' is missing from English dictionary.");
        }
    }

    [TestMethod]
    public void ReadDictionary_AllRequiredKeysExistInSpanish()
    {
        Messages messages = new Messages();
        messages.SetCurrentLanguage("Spanish");
        messages.ReadDictionary();

        string[] requiredKeys = { "welcome", "menu",
                                   "choose_path", "path_north", "path_south", "attack_prompt",
                                   "victory", "defeat", "retreat", "invalid", "goodbye" };

        foreach (string key in requiredKeys)
        {
            string result = messages.GetMessage(key);
            Assert.AreNotEqual($"[{key}]", result, $"Key '{key}' is missing from Spanish dictionary.");
        }
    }

    [TestMethod]
    public void ReadDictionary_AllRequiredKeysExistInFrench()
    {
        Messages messages = new Messages();
        messages.SetCurrentLanguage("French");
        messages.ReadDictionary();

        string[] requiredKeys = { "welcome", "menu",
                                   "choose_path", "path_north", "path_south", "attack_prompt",
                                   "victory", "defeat", "retreat", "invalid", "goodbye" };

        foreach (string key in requiredKeys)
        {
            string result = messages.GetMessage(key);
            Assert.AreNotEqual($"[{key}]", result, $"Key '{key}' is missing from French dictionary.");
        }
    }

    [TestMethod]
    public void ReadDictionary_AllRequiredKeysExistInItalian()
    {
        Messages messages = new Messages();
        messages.SetCurrentLanguage("Italian");
        messages.ReadDictionary();

        string[] requiredKeys = { "welcome", "menu",
                                   "choose_path", "path_north", "path_south", "attack_prompt",
                                   "victory", "defeat", "retreat", "invalid", "goodbye" };

        foreach (string key in requiredKeys)
        {
            string result = messages.GetMessage(key);
            Assert.AreNotEqual($"[{key}]", result, $"Key '{key}' is missing from Italian dictionary.");
        }
    }

    // --- IsValidRace ---

    [TestMethod]
    public void IsValidRace_ReturnsTrue_ForHuman()
    {
        Messages messages = new Messages();
        messages.SetCurrentLanguage("English");
        messages.ReadDictionary();
        Assert.IsTrue(messages.IsValidRace("human"));
    }

    [TestMethod]
    public void IsValidRace_ReturnsTrue_CaseInsensitive()
    {
        Messages messages = new Messages();
        messages.SetCurrentLanguage("English");
        messages.ReadDictionary();
        Assert.IsTrue(messages.IsValidRace("HUMAN"));
    }

    [TestMethod]
    public void IsValidRace_ReturnsFalse_ForInvalidRace()
    {
        Messages messages = new Messages();
        messages.SetCurrentLanguage("English");
        messages.ReadDictionary();
        Assert.IsFalse(messages.IsValidRace("orc"));
    }

    [TestMethod]
    public void IsValidRace_ReturnsFalse_ForNull()
    {
        Messages messages = new Messages();
        messages.SetCurrentLanguage("English");
        messages.ReadDictionary();
        Assert.IsFalse(messages.IsValidRace(null));
    }

    [TestMethod]
    public void NormalizeRace_ReturnsCanonical_ForHuman()
    {
        Messages messages = new Messages();
        messages.SetCurrentLanguage("English");
        messages.ReadDictionary();
        Assert.AreEqual("Human", messages.NormalizeRace("human"));
    }

    [TestMethod]
    public void NormalizeRace_ReturnsCanonical_CaseInsensitive()
    {
        Messages messages = new Messages();
        messages.SetCurrentLanguage("English");
        messages.ReadDictionary();
        Assert.AreEqual("Human", messages.NormalizeRace("HUMAN"));
    }

    // --- IsValidOccupation ---

    [TestMethod]
    public void IsValidOccupation_ReturnsTrue_ForFighter()
    {
        Messages messages = new Messages();
        messages.SetCurrentLanguage("English");
        messages.ReadDictionary();
        Assert.IsTrue(messages.IsValidOccupation("fighter"));
    }

    [TestMethod]
    public void IsValidOccupation_ReturnsTrue_CaseInsensitive()
    {
        Messages messages = new Messages();
        messages.SetCurrentLanguage("English");
        messages.ReadDictionary();
        Assert.IsTrue(messages.IsValidOccupation("FIGHTER"));
    }

    [TestMethod]
    public void IsValidOccupation_ReturnsFalse_ForInvalidOccupation()
    {
        Messages messages = new Messages();
        messages.SetCurrentLanguage("English");
        messages.ReadDictionary();
        Assert.IsFalse(messages.IsValidOccupation("wizard"));
    }

    [TestMethod]
    public void IsValidOccupation_ReturnsFalse_ForNull()
    {
        Messages messages = new Messages();
        messages.SetCurrentLanguage("English");
        messages.ReadDictionary();
        Assert.IsFalse(messages.IsValidOccupation(null));
    }

    [TestMethod]
    public void NormalizeOccupation_ReturnsCanonical_ForFighter()
    {
        Messages messages = new Messages();
        messages.SetCurrentLanguage("English");
        messages.ReadDictionary();
        Assert.AreEqual("Fighter", messages.NormalizeOccupation("fighter"));
    }

    [TestMethod]
    public void NormalizeOccupation_ReturnsCanonical_CaseInsensitive()
    {
        Messages messages = new Messages();
        messages.SetCurrentLanguage("English");
        messages.ReadDictionary();
        Assert.AreEqual("Fighter", messages.NormalizeOccupation("FIGHTER"));
    }

    // --- TranslateForDisplay Tests ---

    [TestMethod]
    public void TranslateRaceForDisplay_ReturnsTranslatedName_InAllLanguages()
    {
        Messages spanish = new Messages();
        spanish.SetCurrentLanguage("Spanish");
        spanish.ReadDictionary();
        Assert.AreEqual("Elfo", spanish.TranslateRaceForDisplay("Elf"));
        Assert.AreEqual("Elfo", spanish.TranslateRaceForDisplay("elf"));

        Messages french = new Messages();
        french.SetCurrentLanguage("French");
        french.ReadDictionary();
        Assert.AreEqual("Elfe", french.TranslateRaceForDisplay("Elf"));

        Messages italian = new Messages();
        italian.SetCurrentLanguage("Italian");
        italian.ReadDictionary();
        Assert.AreEqual("Elfo", italian.TranslateRaceForDisplay("Elf"));
        Assert.AreEqual("Nano", italian.TranslateRaceForDisplay("Dwarf"));
        Assert.AreEqual("Umano", italian.TranslateRaceForDisplay("Human"));
    }

    [TestMethod]
    public void TranslateOccupationForDisplay_ReturnsTranslatedName_InAllLanguages()
    {
        Messages spanish = new Messages();
        spanish.SetCurrentLanguage("Spanish");
        spanish.ReadDictionary();
        Assert.AreEqual("Guerrero", spanish.TranslateOccupationForDisplay("Fighter"));
        Assert.AreEqual("Guerrero", spanish.TranslateOccupationForDisplay("fighter"));

        Messages french = new Messages();
        french.SetCurrentLanguage("French");
        french.ReadDictionary();
        Assert.AreEqual("Guerrier", french.TranslateOccupationForDisplay("Fighter"));

        Messages italian = new Messages();
        italian.SetCurrentLanguage("Italian");
        italian.ReadDictionary();
        Assert.AreEqual("Guerriero", italian.TranslateOccupationForDisplay("Fighter"));
        Assert.AreEqual("Mago", italian.TranslateOccupationForDisplay("Magician"));
    }

    [TestMethod]
    public void TranslateWeaponForDisplay_ReturnsTranslatedName_InAllLanguages()
    {
        Messages spanish = new Messages();
        spanish.SetCurrentLanguage("Spanish");
        spanish.ReadDictionary();
        Assert.AreEqual("espada larga", spanish.TranslateWeaponForDisplay("long sword"));

        Messages french = new Messages();
        french.SetCurrentLanguage("French");
        french.ReadDictionary();
        Assert.AreEqual("epee longue", french.TranslateWeaponForDisplay("long sword"));

        Messages italian = new Messages();
        italian.SetCurrentLanguage("Italian");
        italian.ReadDictionary();
        Assert.AreEqual("spada lunga", italian.TranslateWeaponForDisplay("long sword"));
        Assert.AreEqual("pugnale", italian.TranslateWeaponForDisplay("dagger"));
    }

    [TestMethod]
    public void IsValidRace_And_NormalizeRace_WorkForItalian()
    {
        Messages italian = new Messages();
        italian.SetCurrentLanguage("Italian");
        italian.ReadDictionary();

        Assert.IsTrue(italian.IsValidRace("umano"));
        Assert.IsTrue(italian.IsValidRace("NANO"));
        Assert.AreEqual("Human", italian.NormalizeRace("umano"));
        Assert.AreEqual("Dwarf", italian.NormalizeRace("NANO"));
    }

    [TestMethod]
    public void IsValidOccupation_And_NormalizeOccupation_WorkForItalian()
    {
        Messages italian = new Messages();
        italian.SetCurrentLanguage("Italian");
        italian.ReadDictionary();

        Assert.IsTrue(italian.IsValidOccupation("guerriero"));
        Assert.IsTrue(italian.IsValidOccupation("MAGO"));
        Assert.AreEqual("Fighter", italian.NormalizeOccupation("guerriero"));
        Assert.AreEqual("Magician", italian.NormalizeOccupation("MAGO"));
    }

    [TestMethod]
    public void TranslateMethods_DoNotThrow_WhenCalledBeforeReadDictionary()
    {
        Messages messages = new Messages();

        Assert.AreEqual("Elf", messages.TranslateRaceForDisplay("Elf"));
        Assert.AreEqual("Fighter", messages.TranslateOccupationForDisplay("Fighter"));
        Assert.AreEqual("long sword", messages.TranslateWeaponForDisplay("long sword"));
        Assert.AreEqual(string.Empty, messages.TranslateRaceForDisplay(null));
        Assert.AreEqual(string.Empty, messages.TranslateOccupationForDisplay(null));
        Assert.AreEqual(string.Empty, messages.TranslateWeaponForDisplay(null));
    }
}

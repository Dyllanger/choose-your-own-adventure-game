# ADR 0001: Support Italian as an Additional Language

- **Status**: Accepted
- **Date**: 2026-11-09
- **Deciders**: Dyllanger
- **Context**: CIDM 4390 - Choose Your Own Adventure (CYOA) Game

---

## 1. Context and Problem Statement

The Choose Your Own Adventure (CYOA) console application is designed with an internationalization (i18n) layer that originally supported three languages: English, Spanish, and French. Localization is managed through `Messages.cs`, which parses a centralized `language_data.json` document containing narrative strings, user prompts, input normalization tables, and display dictionaries.

To validate the extensibility of the architecture and demonstrate that the system adheres to the Open/Closed Principle (open for extension, closed for modification), a decision was made to introduce Italian as a fourth supported language.

The key challenge was ensuring that adding a new language:
1. Did not alter or destabilize core domain logic (`Player`, `Dragon`, `Combat`, `Die`, `Weapon`).
2. Maintained 100% string and mapping parity with existing languages (all 69 narrative keys and 5 mapping tables).
3. Preserved canonical internal representations (e.g., race `"Elf"`, occupation `"Fighter"`) so game rules and stat calculations remain language-agnostic.
4. Functioned seamlessly across console environments without encoding artifacts.

---

## 2. Decision Drivers

- **Architectural Validation**: Prove that the dictionary-driven architecture in `Messages.cs` can scale to new locales without refactoring game systems.
- **Data Integrity & Parity**: Guarantee that no runtime `KeyNotFoundException` or fallback strings occur when navigating any path, combat encounter, or menu in Italian.
- **Canonical Model Isolation**: Keep gameplay mechanics (racial strength/agility modifiers, occupation weapon assignments, attack calculations) bound to canonical keys, while allowing users to input and view terms in their native language (e.g., entering `"guerriero"` or `"ladro"`).
- **Automated Regression Prevention**: Ensure that new language additions are guarded by unit tests in the MSTest suite.

---

## 3. Considered Options

1. **Option 1: Add Italian via `language_data.json` and update `Game.cs` language prompt (Chosen)**.
   - Maintain the existing architecture.
   - Add a full `"Italian"` section to `language_data.json`.
   - Update `Game.cs` startup language selection menu to include option `4. Italiano`.
   - Add unit test coverage in `CYOATests/MessagesTests.cs` and `CYOATests/GameTests.cs`.

2. **Option 2: Dynamically detect and list available languages from `language_data.json`**.
   - Instead of hardcoding `"1. English 2. Espanol 3. Francais 4. Italiano"` in `Game.cs`, inspect the top-level keys of `language_data.json` dynamically.
   - *Rationale for deferral*: While cleaner long-term, it exceeds the immediate requirement and would modify the console startup sequence diagram established for the project.

3. **Option 3: Hardcode localized strings inside C# classes using resource (.resx) files**.
   - Use standard .NET `.resx` resource files.
   - *Rationale for rejection*: Conflicts with the project's design pattern, which centers on a unified JSON configuration file shared and inspected directly by students and instructors.

---

## 4. Decision Outcome

**Chosen Option: Option 1.**

The Italian language was integrated by:

1. **Extending [`language_data.json`](file:///choose-your-own-adventure-game/language_data.json)**:
   - **`dictionary`**: Added all 69 narrative entries, prompts, dragon taunts, and game over messages translated into Italian.
   - **`raceMap`**: Maps Italian inputs (`"umano"`, `"elfo"`, `"nano"`, `"halfling"`) to canonical types (`"Human"`, `"Elf"`, `"Dwarf"`, `"Halfling"`).
   - **`occupationMap`**: Maps Italian inputs (`"guerriero"`, `"mago"`, `"ladro"`, `"arciere"`) to canonical occupations (`"Fighter"`, `"Magician"`, `"Thief"`, `"Archer"`).
   - **`displayRaceMap`**: Maps canonical races to Italian display names (*Umano*, *Elfo*, *Nano*, *Halfling*).
   - **`displayOccupationMap`**: Maps canonical occupations to Italian display names (*Guerriero*, *Mago*, *Ladro*, *Arciere*).
   - **`displayWeaponMap`**: Maps canonical weapon types to Italian names (*spada lunga*, *incantesimo fulmine*, *pugnale*, *arco lungo*, *artigli*).

2. **Updating [`Game.cs`](file:///choose-your-own-adventure-game/Game.cs)**:
   - Added `Seleziona lingua:` to the language banner.
   - Added option `4. Italiano` to the menu.
   - Extended the switch expression to map input `"4"` to `"Italian"`.

3. **Expanding Unit Tests in [`CYOATests`](file:///choose-your-own-adventure-game/CYOATests)**:
   - Added language switching tests: `SetCurrentLanguage_SetsLanguageToItalian`.
   - Added message loading tests: `ReadDictionary_LoadsItalianWelcomeMessage`, `ReadDictionary_LoadsGoodbyeMessageInEachLanguage`, `ReadDictionary_AllRequiredKeysExistInItalian`.
   - Added input normalization tests: `IsValidRace_And_NormalizeRace_WorkForItalian`, `IsValidOccupation_And_NormalizeOccupation_WorkForItalian`.
   - Added display translation tests for Italian in `TranslateRaceForDisplay`, `TranslateOccupationForDisplay`, and `TranslateWeaponForDisplay`.
   - Added end-game flow tests in `CYOATests/GameTests.cs` verifying victory, defeat, and retreat narratives in Italian.

---

## 5. Consequences

### Positive Consequences
- **Zero Impact on Domain Code**: No alterations were needed in `Player.cs`, `Dragon.cs`, `Combat.cs`, `Weapon.cs`, or `Die.cs`.
- **Full Parity**: All 4 languages now support identical features, prompts, and narrative branches without missing keys.
- **Robustness**: All 106 automated tests pass, verifying that canonical translations and edge cases behave consistently.
- **Standardized Terminal Compatibility**: Uses ASCII-compatible transliteration consistent with the existing Spanish and French sections to avoid terminal character encoding issues on diverse operating systems.

### Negative Consequences / Trade-offs
- **Content Maintenance**: Any future quests, weapons, or narrative strings added to `language_data.json` must now be maintained across 4 languages rather than 3.
- **Language Menu Scalability**: The language prompt in `Game.cs` remains hardcoded with numbered options (1–4). If many more languages are added, migrating to dynamic language menu generation will be recommended.

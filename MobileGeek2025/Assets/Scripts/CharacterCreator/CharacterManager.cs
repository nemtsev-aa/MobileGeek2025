using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CharacterManager : MonoBehaviour {
    public static string GalleryPath => Application.persistentDataPath;

    [SerializeField] private DefaultBasicParametersConfig _defaultParameters;
    [SerializeField] private DefaultBodyDescriptionConfig _defaultBodyDescription;

    private static CharacterManager _instance;

    private Character _character;

    private CharacterManager() { }

    public static CharacterManager Instance {
        get {
            if (_instance == null) {
                _instance = FindObjectOfType<CharacterManager>();

                if (_instance == null) {
                    GameObject singletonObject = new GameObject("CharacterManager");
                    _instance = singletonObject.AddComponent<CharacterManager>();
                }
            }
            return _instance;
        }
    }

    public Character Character {
        get {

            if (_character == null)
                CreateCharacter();

            return _character;
        }
        set {
            _character = value;
        }
    }

    public void Init() {
        if (_instance != null && _instance != this) {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(gameObject);

        CreateCharacter();
    }

    // Метод для сохранения данных персонажа в JSON-файл
    public void SaveCharacter(Character character) {
        // Сериализация объекта Character в JSON
        string json = JsonUtility.ToJson(character, true);
        string savePath = Path.Combine(GalleryPath, $"{character.Name}.json");

        // Запись JSON в файл
        File.WriteAllText(savePath, json);

        Debug.Log($"Character saved to {savePath}");
    }

    // Метод для загрузки данных персонажа из JSON-файла
    public Character LoadCharacter(string name) {
        // Проверяем, существует ли файл
        string savePath = Path.Combine(GalleryPath, $"{name}.json");

        if (!File.Exists(savePath)) {
            Debug.LogWarning("No save file found.");
            return null;
        }

        // Чтение JSON из файла
        string json = File.ReadAllText(savePath);

        // Десериализация JSON в объект Character
        Character character = JsonUtility.FromJson<Character>(json);

        Debug.Log($"Character loaded from {savePath}");
        return character;
    }

    // Метод для загрузки всех JSON-файлов из папки
    public List<Character> LoadAllCharacters() {
        // Список для хранения загруженных персонажей
        List<Character> characters = new List<Character>();

        // Получаем все файлы с расширением .json в папке
        string[] jsonFiles = Directory.GetFiles(GalleryPath, "*.json");

        // Обрабатываем каждый файл
        foreach (string filePath in jsonFiles) {
            try {
                // Читаем содержимое файла
                string json = File.ReadAllText(filePath);

                // Десериализуем JSON в объект Character
                Character character = JsonUtility.FromJson<Character>(json);

                // Добавляем персонажа в список
                characters.Add(character);

                Debug.Log($"Character loaded from {filePath}");
            }
            catch (System.Exception ex) {
                Debug.LogError($"Failed to load character from {filePath}: {ex.Message}");
            }
        }

        return characters;
    }

    // Метод для отправки JSON-файла
    public void ShareCharacter(Character character) {
        string filePath = Path.Combine(GalleryPath, $"{character.Name}.json");

        // Проверяем, существует ли файл
        if (!File.Exists(filePath)) {
            Debug.LogError("File does not exist: " + filePath);
            return;
        }

        // Получаем путь к файлу в формате, понятном Android
        string androidPath = "file://" + filePath;

        // Создаем Android-интент для отправки файла
        using (AndroidJavaClass intentClass = new AndroidJavaClass("android.content.Intent"))
        using (AndroidJavaObject intentObject = new AndroidJavaObject("android.content.Intent")) {
            // Устанавливаем действие интента (отправка)
            intentObject.Call<AndroidJavaObject>("setAction", intentClass.GetStatic<string>("ACTION_SEND"));

            // Устанавливаем тип файла (JSON)
            intentObject.Call<AndroidJavaObject>("setType", "application/json");

            // Создаем Android-объект для URI файла
            using (AndroidJavaClass uriClass = new AndroidJavaClass("android.net.Uri"))
            using (AndroidJavaObject uriObject = uriClass.CallStatic<AndroidJavaObject>("parse", androidPath)) {
                // Добавляем URI файла в интент
                intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_STREAM"), uriObject);

                // Запускаем интент
                using (AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
                using (AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity")) {
                    currentActivity.Call("startActivity", intentObject);
                }
            }
        }

        Debug.Log("File shared successfully: " + filePath);
    }

    private void CreateCharacter() {
        _character = new Character();
        _character.SetName("DefaultName");
        _character.SetBasicParameters(GetDefaultBasicParameters());
        _character.SetBodyDescription(GetDefaultBodyDescription());
    }

    private BasicParameters GetDefaultBasicParameters() {

        var parameters = new List<BasicParameter>();
        foreach (var item in _defaultParameters.Parameters) {
            BasicParameter newParameter = new BasicParameter(item.Type, item.Name, item.Max, item.Min, item.Value);
            parameters.Add(newParameter);
        }


        return new BasicParameters(parameters);
    }

    private BodyDescription GetDefaultBodyDescription() {
        return new BodyDescription(
            _defaultBodyDescription.GetBodyPartDescriptionByType(BodyPartTypes.All),
            _defaultBodyDescription.GetBodyPartDescriptionByType(BodyPartTypes.Head),
            _defaultBodyDescription.GetBodyPartDescriptionByType(BodyPartTypes.Body),
            _defaultBodyDescription.GetBodyPartDescriptionByType(BodyPartTypes.Arms),
            _defaultBodyDescription.GetBodyPartDescriptionByType(BodyPartTypes.Legs)
            );
    }
}



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

    // ����� ��� ���������� ������ ��������� � JSON-����
    public void SaveCharacter(Character character) {
        // ������������ ������� Character � JSON
        string json = JsonUtility.ToJson(character, true);
        string savePath = Path.Combine(GalleryPath, $"{character.Name}.json");

        // ������ JSON � ����
        File.WriteAllText(savePath, json);

        Debug.Log($"Character saved to {savePath}");
    }

    // ����� ��� �������� ������ ��������� �� JSON-�����
    public Character LoadCharacter(string name) {
        // ���������, ���������� �� ����
        string savePath = Path.Combine(GalleryPath, $"{name}.json");

        if (!File.Exists(savePath)) {
            Debug.LogWarning("No save file found.");
            return null;
        }

        // ������ JSON �� �����
        string json = File.ReadAllText(savePath);

        // �������������� JSON � ������ Character
        Character character = JsonUtility.FromJson<Character>(json);

        Debug.Log($"Character loaded from {savePath}");
        return character;
    }

    // ����� ��� �������� ���� JSON-������ �� �����
    public List<Character> LoadAllCharacters() {
        // ������ ��� �������� ����������� ����������
        List<Character> characters = new List<Character>();

        // �������� ��� ����� � ����������� .json � �����
        string[] jsonFiles = Directory.GetFiles(GalleryPath, "*.json");

        // ������������ ������ ����
        foreach (string filePath in jsonFiles) {
            try {
                // ������ ���������� �����
                string json = File.ReadAllText(filePath);

                // ������������� JSON � ������ Character
                Character character = JsonUtility.FromJson<Character>(json);

                // ��������� ��������� � ������
                characters.Add(character);

                Debug.Log($"Character loaded from {filePath}");
            }
            catch (System.Exception ex) {
                Debug.LogError($"Failed to load character from {filePath}: {ex.Message}");
            }
        }

        return characters;
    }

    // ����� ��� �������� JSON-�����
    public void ShareCharacter(Character character) {
        string filePath = Path.Combine(GalleryPath, $"{character.Name}.json");

        // ���������, ���������� �� ����
        if (!File.Exists(filePath)) {
            Debug.LogError("File does not exist: " + filePath);
            return;
        }

        // �������� ���� � ����� � �������, �������� Android
        string androidPath = "file://" + filePath;

        // ������� Android-������ ��� �������� �����
        using (AndroidJavaClass intentClass = new AndroidJavaClass("android.content.Intent"))
        using (AndroidJavaObject intentObject = new AndroidJavaObject("android.content.Intent")) {
            // ������������� �������� ������� (��������)
            intentObject.Call<AndroidJavaObject>("setAction", intentClass.GetStatic<string>("ACTION_SEND"));

            // ������������� ��� ����� (JSON)
            intentObject.Call<AndroidJavaObject>("setType", "application/json");

            // ������� Android-������ ��� URI �����
            using (AndroidJavaClass uriClass = new AndroidJavaClass("android.net.Uri"))
            using (AndroidJavaObject uriObject = uriClass.CallStatic<AndroidJavaObject>("parse", androidPath)) {
                // ��������� URI ����� � ������
                intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_STREAM"), uriObject);

                // ��������� ������
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



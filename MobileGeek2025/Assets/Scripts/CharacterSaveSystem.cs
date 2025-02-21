using System.IO;
using UnityEngine;

public static class CharacterSaveSystem {
    // ���� � ����� ����������
    private static string SavePath => Path.Combine(Application.persistentDataPath, "character.json");
    
    // ����� ��� ���������� ������ ��������� � JSON-����
    public static void SaveCharacter(Character character) {
        // ������������ ������� Character � JSON
        string json = JsonUtility.ToJson(character, true);

        // ������ JSON � ����
        File.WriteAllText(SavePath, json);

        Debug.Log($"Character saved to {SavePath}");
    }

    // ����� ��� �������� ������ ��������� �� JSON-�����
    public static Character LoadCharacter() {
        // ���������, ���������� �� ����
        if (!File.Exists(SavePath)) {
            Debug.LogWarning("No save file found.");
            return null;
        }

        // ������ JSON �� �����
        string json = File.ReadAllText(SavePath);

        // �������������� JSON � ������ Character
        Character character = JsonUtility.FromJson<Character>(json);

        Debug.Log($"Character loaded from {SavePath}");
        return character;
    }
}

using System.IO;
using UnityEngine;

public static class CharacterSaveSystem {
    // Путь к файлу сохранения
    private static string SavePath => Path.Combine(Application.persistentDataPath, "character.json");
    
    // Метод для сохранения данных персонажа в JSON-файл
    public static void SaveCharacter(Character character) {
        // Сериализация объекта Character в JSON
        string json = JsonUtility.ToJson(character, true);

        // Запись JSON в файл
        File.WriteAllText(SavePath, json);

        Debug.Log($"Character saved to {SavePath}");
    }

    // Метод для загрузки данных персонажа из JSON-файла
    public static Character LoadCharacter() {
        // Проверяем, существует ли файл
        if (!File.Exists(SavePath)) {
            Debug.LogWarning("No save file found.");
            return null;
        }

        // Чтение JSON из файла
        string json = File.ReadAllText(SavePath);

        // Десериализация JSON в объект Character
        Character character = JsonUtility.FromJson<Character>(json);

        Debug.Log($"Character loaded from {SavePath}");
        return character;
    }
}

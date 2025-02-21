using UnityEngine;

public class EntryPoint : MonoBehaviour {
    [SerializeField] private UIManager _uIManager;
    [SerializeField] private CharacterManager _characterManager; 
    [SerializeField] private CharacterCustomManager _characterCustomManager;

    private void Start() {
        _characterManager.Init();
        _uIManager.Init(_characterCustomManager.SaveManager);
        _characterCustomManager.Init(_characterManager, _uIManager);
    }
}
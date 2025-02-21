using UnityEngine;

public class Device : MonoBehaviour {
    public PrehistoryTypes PrehistoryTypes { get; private set; }

    public void Init(PrehistoryTypes type) {
        PrehistoryTypes = type;
        Show(false);
    }

    public void Show(bool status) {
        gameObject.SetActive(status);
    }
}

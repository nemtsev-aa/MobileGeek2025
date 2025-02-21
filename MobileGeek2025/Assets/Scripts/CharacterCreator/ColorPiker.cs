using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ColorPiker : Piker {
    public event Action<Color> ColorChanged;

    [SerializeField] private Button _colorSelector;
    [SerializeField] private Image _nowColorShow;
    [SerializeField] private InputField _hexColorText;

    private Color _selectionColor;
    private Texture2D _tex;

    public override void Init() {
        base.Init();
    }

    public override void AddListeners() {
        base.AddListeners();

        _colorSelector.onClick.AddListener(GetColor);
    }

    public override void RemoveListeners() {
        base.RemoveListeners();

        _colorSelector.onClick.RemoveListener(GetColor);
    }

    private void GetColor() {
        _tex = new Texture2D(1, 1);
        StartCoroutine(CaptureTempArea());
    }

    IEnumerator CaptureTempArea() {
        yield return new WaitForEndOfFrame();
#if ENABLE_INPUT_SYSTEM
        Vector2 pos =  Mouse.current.position.ReadValue();
#elif ENABLE_LEGACY_INPUT_MANAGER
        Vector2 pos = EventSystem.current.currentInputModule.input.mousePosition;
#endif

        _tex.ReadPixels(new Rect(pos.x, pos.y, 1, 1), 0, 0);
        _tex.Apply();
        _selectionColor = _tex.GetPixel(0, 0);

        yield return new WaitForSecondsRealtime(0.1f);

        _nowColorShow.color = _selectionColor;
        _hexColorText.text = ColorToStr(_selectionColor);

        ColorChanged?.Invoke(_selectionColor);
    }

    public string ColorToStr(Color color) {
        string r = ((int)(color.r * 255)).ToString("X2");
        string g = ((int)(color.g * 255)).ToString("X2");
        string b = ((int)(color.b * 255)).ToString("X2");

        string result = string.Format("{0}{1}{2}", r, g, b);

        return result;
    }

    public Color StrToColor(string str) {
        str = str.ToLowerInvariant();
        
        if (str.Length == 6) {
            char[] arr = str.ToCharArray();
            char[] color_arr = new char[6];

            for (int i = 0; i < 6; i++) {
                if (arr[i] >= '0' && arr[i] <= '9')
                    color_arr[i] = (char)(arr[i] - '0');
                else if (arr[i] >= 'a' && arr[i] <= 'f')
                    color_arr[i] = (char)(10 + arr[i] - 'a');
                else
                    color_arr[i] = (char)0;
            }

            float red = (color_arr[0] * 16 + color_arr[1]) / 255.0f;
            float green = (color_arr[2] * 16 + color_arr[3]) / 255.0f;
            float blue = (color_arr[4] * 16 + color_arr[5]) / 255.0f;

            return new Color(red, green, blue);
        }

        return Color.white;
    }

}

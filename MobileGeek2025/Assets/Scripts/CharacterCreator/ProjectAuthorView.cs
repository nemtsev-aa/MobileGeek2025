using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProjectAuthorView : UICompanent {
    [SerializeField] private Image _icon;
    [SerializeField] private TextMeshProUGUI _nameLabel;
    [SerializeField] private TextMeshProUGUI _typeLabel;

    private ProjectAuthorConfig _config;

    public void Init(ProjectAuthorConfig config) {
        _config = config;

        Activate(false);
    }

    public override void UpdateContent() {
        base.UpdateContent();

        _nameLabel.text = _config.Name;
        _icon.sprite = _config.Icon;
        _typeLabel.text = _config.ProjectRolleName;
    }
}

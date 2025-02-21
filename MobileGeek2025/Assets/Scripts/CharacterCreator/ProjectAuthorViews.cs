using System.Collections.Generic;
using UnityEngine;

public class ProjectAuthorViews : Piker {
    [SerializeField] private ProjectAuthorView _projectAuthorViewPrefab;
    [SerializeField] private RectTransform _viewParent;

    private ProjectAuthorConfigs _authorConfigs;
    private List<ProjectAuthorView> _views;

    public void Init(ProjectAuthorConfigs configs) {
        _authorConfigs = configs;
    }

    public override void Show(bool value) {
        base.Show(value);

        if (value == true && _views == null)
            CreateViews();
    }

    private void CreateViews() {
        _views = new List<ProjectAuthorView>();
        foreach (ProjectAuthorConfig iConfig in _authorConfigs.Configs) {

            ProjectAuthorView view = Instantiate(_projectAuthorViewPrefab, _viewParent);
            view.Init(iConfig);
            _views.Add(view);
        }
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class UiBindEntity {

    public UIDataBind.Entitas.Components.PathComponent path { get { return (UIDataBind.Entitas.Components.PathComponent)GetComponent(UiBindComponentsLookup.Path); } }
    public bool hasPath { get { return HasComponent(UiBindComponentsLookup.Path); } }

    public void AddPath(string newValue) {
        var index = UiBindComponentsLookup.Path;
        var component = (UIDataBind.Entitas.Components.PathComponent)CreateComponent(index, typeof(UIDataBind.Entitas.Components.PathComponent));
        component.Value = newValue;
        AddComponent(index, component);
    }

    public void ReplacePath(string newValue) {
        var index = UiBindComponentsLookup.Path;
        var component = (UIDataBind.Entitas.Components.PathComponent)CreateComponent(index, typeof(UIDataBind.Entitas.Components.PathComponent));
        component.Value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemovePath() {
        RemoveComponent(UiBindComponentsLookup.Path);
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentMatcherApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed partial class UiBindMatcher {

    static Entitas.IMatcher<UiBindEntity> _matcherPath;

    public static Entitas.IMatcher<UiBindEntity> Path {
        get {
            if (_matcherPath == null) {
                var matcher = (Entitas.Matcher<UiBindEntity>)Entitas.Matcher<UiBindEntity>.AllOf(UiBindComponentsLookup.Path);
                matcher.componentNames = UiBindComponentsLookup.componentNames;
                _matcherPath = matcher;
            }

            return _matcherPath;
        }
    }
}
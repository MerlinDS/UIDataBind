//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class UiBindEntity {

    public UIDataBind.Entitas.Components.Properties.ColorProperty colorProperty { get { return (UIDataBind.Entitas.Components.Properties.ColorProperty)GetComponent(UiBindComponentsLookup.ColorProperty); } }
    public bool hasColorProperty { get { return HasComponent(UiBindComponentsLookup.ColorProperty); } }

    public void AddColorProperty(UnityEngine.Color newValue) {
        var index = UiBindComponentsLookup.ColorProperty;
        var component = (UIDataBind.Entitas.Components.Properties.ColorProperty)CreateComponent(index, typeof(UIDataBind.Entitas.Components.Properties.ColorProperty));
        component.Value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceColorProperty(UnityEngine.Color newValue) {
        var index = UiBindComponentsLookup.ColorProperty;
        var component = (UIDataBind.Entitas.Components.Properties.ColorProperty)CreateComponent(index, typeof(UIDataBind.Entitas.Components.Properties.ColorProperty));
        component.Value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveColorProperty() {
        RemoveComponent(UiBindComponentsLookup.ColorProperty);
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

    static Entitas.IMatcher<UiBindEntity> _matcherColorProperty;

    public static Entitas.IMatcher<UiBindEntity> ColorProperty {
        get {
            if (_matcherColorProperty == null) {
                var matcher = (Entitas.Matcher<UiBindEntity>)Entitas.Matcher<UiBindEntity>.AllOf(UiBindComponentsLookup.ColorProperty);
                matcher.componentNames = UiBindComponentsLookup.componentNames;
                _matcherColorProperty = matcher;
            }

            return _matcherColorProperty;
        }
    }
}
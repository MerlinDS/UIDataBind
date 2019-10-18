//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class UiBindEntity {

    public UIDataBind.Entitas.Components.Properties.TextureProperty textureProperty { get { return (UIDataBind.Entitas.Components.Properties.TextureProperty)GetComponent(UiBindComponentsLookup.TextureProperty); } }
    public bool hasTextureProperty { get { return HasComponent(UiBindComponentsLookup.TextureProperty); } }

    public void AddTextureProperty(UnityEngine.Texture newValue) {
        var index = UiBindComponentsLookup.TextureProperty;
        var component = (UIDataBind.Entitas.Components.Properties.TextureProperty)CreateComponent(index, typeof(UIDataBind.Entitas.Components.Properties.TextureProperty));
        component.Value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceTextureProperty(UnityEngine.Texture newValue) {
        var index = UiBindComponentsLookup.TextureProperty;
        var component = (UIDataBind.Entitas.Components.Properties.TextureProperty)CreateComponent(index, typeof(UIDataBind.Entitas.Components.Properties.TextureProperty));
        component.Value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveTextureProperty() {
        RemoveComponent(UiBindComponentsLookup.TextureProperty);
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

    static Entitas.IMatcher<UiBindEntity> _matcherTextureProperty;

    public static Entitas.IMatcher<UiBindEntity> TextureProperty {
        get {
            if (_matcherTextureProperty == null) {
                var matcher = (Entitas.Matcher<UiBindEntity>)Entitas.Matcher<UiBindEntity>.AllOf(UiBindComponentsLookup.TextureProperty);
                matcher.componentNames = UiBindComponentsLookup.componentNames;
                _matcherTextureProperty = matcher;
            }

            return _matcherTextureProperty;
        }
    }
}

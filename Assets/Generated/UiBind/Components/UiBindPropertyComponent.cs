//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class UiBindEntity {

    static readonly UIDataBind.Entitas.Components.Properties.PropertyComponent propertyComponent = new UIDataBind.Entitas.Components.Properties.PropertyComponent();

    public bool isProperty {
        get { return HasComponent(UiBindComponentsLookup.Property); }
        set {
            if (value != isProperty) {
                var index = UiBindComponentsLookup.Property;
                if (value) {
                    var componentPool = GetComponentPool(index);
                    var component = componentPool.Count > 0
                            ? componentPool.Pop()
                            : propertyComponent;

                    AddComponent(index, component);
                } else {
                    RemoveComponent(index);
                }
            }
        }
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

    static Entitas.IMatcher<UiBindEntity> _matcherProperty;

    public static Entitas.IMatcher<UiBindEntity> Property {
        get {
            if (_matcherProperty == null) {
                var matcher = (Entitas.Matcher<UiBindEntity>)Entitas.Matcher<UiBindEntity>.AllOf(UiBindComponentsLookup.Property);
                matcher.componentNames = UiBindComponentsLookup.componentNames;
                _matcherProperty = matcher;
            }

            return _matcherProperty;
        }
    }
}

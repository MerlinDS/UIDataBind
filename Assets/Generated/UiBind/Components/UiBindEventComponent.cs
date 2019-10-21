//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class UiBindEntity {

    public UIDataBind.Entitas.Components.Event @event { get { return (UIDataBind.Entitas.Components.Event)GetComponent(UiBindComponentsLookup.Event); } }
    public bool hasEvent { get { return HasComponent(UiBindComponentsLookup.Event); } }

    public void AddEvent(UIDataBind.Base.ControlEvent newValue) {
        var index = UiBindComponentsLookup.Event;
        var component = (UIDataBind.Entitas.Components.Event)CreateComponent(index, typeof(UIDataBind.Entitas.Components.Event));
        component.Value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceEvent(UIDataBind.Base.ControlEvent newValue) {
        var index = UiBindComponentsLookup.Event;
        var component = (UIDataBind.Entitas.Components.Event)CreateComponent(index, typeof(UIDataBind.Entitas.Components.Event));
        component.Value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveEvent() {
        RemoveComponent(UiBindComponentsLookup.Event);
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

    static Entitas.IMatcher<UiBindEntity> _matcherEvent;

    public static Entitas.IMatcher<UiBindEntity> Event {
        get {
            if (_matcherEvent == null) {
                var matcher = (Entitas.Matcher<UiBindEntity>)Entitas.Matcher<UiBindEntity>.AllOf(UiBindComponentsLookup.Event);
                matcher.componentNames = UiBindComponentsLookup.componentNames;
                _matcherEvent = matcher;
            }

            return _matcherEvent;
        }
    }
}

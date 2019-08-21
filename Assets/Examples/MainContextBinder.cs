using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Plugins.UIDataBind.Binders;
using UIDataBindCore;
using UIDataBindCore.Attributes;
using UIDataBindCore.Properties;

namespace Plugins.UIDataBind.Examples
{
    public class MainContextBinder : DataContextBinder<MainContext>
    {
        //TODO: Remove this after implementation of contexts binding
        protected override void Configure() =>
            Context.Configure(GetComponentsInChildren<IDataContextBinder>()
                                  .Select(x=>x.Context as ISampleSubDataContext).Where(x=>x!=null));
    }
    public class MainContext : IDataContext, IInitializable
    {

        private ISampleSubDataContext[] _tabs;

        public void Configure(IEnumerable<ISampleSubDataContext> tabs) =>
            _tabs = tabs.ToArray();

        public void Init() => TabIndex = -1;

        #region Binding Methods

        [Bind("First Tab", "Show the first sub context"), UsedImplicitly]
        private void ShowFirst() => TabIndex = 0;

        [Bind("Second Tab", "Show the second sub context"), UsedImplicitly]
        private void ShowSecond() => TabIndex = 1;

        [Bind("Third Tab", "Show the third sub context"), UsedImplicitly]
        private void ShowThird() => TabIndex = 2;

        #endregion

        [Bind]
        private readonly IntProperty _tabLabelProperty = new IntProperty(0);

        private int TabIndex
        {
            get => _tabLabelProperty.Value;
            set
            {
                if (_tabLabelProperty.Value == value)
                    value = -1;

                _tabLabelProperty.Value = value;
                for (var index = 0; index < _tabs.Length; index++)
                {
                    var tab = _tabs[index];
                    tab.Visible = index == value;
                    if(tab.Visible)
                        _descriptionProperty.Value = tab.Label;

                }
                _logoVisibleProperty.Value = value < 0;
            }
        }

        [Bind]
        private readonly BooleanProperty _logoVisibleProperty = new BooleanProperty(true);

        [Bind]
        private readonly StringProperty _descriptionProperty = new StringProperty();
    }
}
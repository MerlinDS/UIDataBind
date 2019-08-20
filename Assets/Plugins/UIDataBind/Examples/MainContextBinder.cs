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
                                  .Select(x=>x.Context as IVisibleDataContext).Where(x=>x!=null));
    }
    public class MainContext : IDataContext
    {

        private IVisibleDataContext[] _tabs;

        public void Configure(IEnumerable<IVisibleDataContext> tabs) =>
            _tabs = tabs.ToArray();

        #region Binding Methods

        [Bind("First Tab", "Show the first sub context"), UsedImplicitly]
        private void ShowFirst() => TabIndex = 0;

        [Bind("Second Tab", "Show the second sub context"), UsedImplicitly]
        private void ShowSecond() => TabIndex = 1;

        [Bind("Third Tab", "Show the third sub context"), UsedImplicitly]
        private void ShowThird() => TabIndex = 2;

        #endregion

        [Bind]
        private readonly IntProperty _tabLabelProperty = new IntProperty();

        private int TabIndex
        {
            get => _tabLabelProperty.Value;
            set
            {
                _tabLabelProperty.Value = value;
                for (var index = 0; index < _tabs.Length; index++)
                    _tabs[index].Visible = index == value;
            }
        }
    }
}
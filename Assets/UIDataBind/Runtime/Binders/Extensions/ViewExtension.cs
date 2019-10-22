using JetBrains.Annotations;
using UIDataBind.Base;
using UnityEngine;

namespace UIDataBind.Binders.Extensions
{
    public static class ViewExtension
    {
        [CanBeNull]
        public static IView GetParentView([NotNull] this Component component)
        {
            IView view = null;
            var transform = component.transform;
            while (view == null && transform != null)
            {
                view = transform.GetComponent<IView>();
                if ((Component) view == component)
                    view = null;

                transform = transform.parent;
            }

            return view;
        }
    }
}
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace UI.RunnerManagement.Helpers
{
    public static class ViewHelper
    {
        public static bool IsWithin(DependencyObject c1, DependencyObject c2, Popup popup = null)
            => c1 is null || c2 is null ? false
            : c1 == c2 ? true
            : IsAncestor(c1, c2) || IsAncestor(c2, c1) ? true
            : popup != null && (IsPopupItem(c1, popup) || IsPopupItem(c2, popup)) ? true
            : IsPopupItem(c1, c2) || IsPopupItem(c2, c1);

        public static bool IsAncestor(DependencyObject obj, DependencyObject ancestor, bool preventEquals = false)
        {
            if (preventEquals)
                obj = GetVisualOrLogicalAncestor(obj);

            do
            {
                if (obj == ancestor)
                    return true;
                obj = GetVisualOrLogicalAncestor(obj);
            }
            while (obj != null);

            return false;
        }

        public static DependencyObject GetVisualOrLogicalAncestor(DependencyObject obj)
        {
            if (obj == null)
                return null;

            if (obj is Visual || obj is Visual3D)
                return VisualTreeHelper.GetParent(obj);

            // If we're in Logical Land then we must walk
            // up the logical tree until we find a
            // Visual/Visual3D to get us back to Visual Land.
            return LogicalTreeHelper.GetParent(obj);
        }


        public static bool IsPopupItem(DependencyObject obj, DependencyObject parent)
        {
            if (parent is ItemsControl)
                return ItemsControl.ItemsControlFromItemContainer(obj) == (ItemsControl)parent;

            var popupInheritanceParent = GetPopupInheritanceParent(obj);
            return popupInheritanceParent == parent ? true
                : popupInheritanceParent is Popup && !(parent is Popup) ? ((Popup)popupInheritanceParent).PlacementTarget == parent
                : false;
        }

        public static DependencyObject GetPopupInheritanceParent(DependencyObject control)
        {
            var topLevelParet = GetTopLevelParent<FrameworkElement>(control);
            if (topLevelParet != null && topLevelParet.GetType().Name.Equals("PopupRoot"))
            {
                var parent = LogicalTreeHelper.GetParent(topLevelParet);
                if (parent == null)
                    parent = GetInheritanceParent(topLevelParet);
                return parent;
            }

            return null;
        }

        public static T GetTopLevelParent<T>(DependencyObject obj)
        {
            var parent = obj;
            T prevParent;
            do
            {
                prevParent = parent is T ? (T)(object)parent : default;
                parent = (DependencyObject)(object)GetParentElement<T>(parent);
            }
            while (parent != null);

            return prevParent;
        }

        public static T GetParentElement<T>(DependencyObject obj, Predicate<T> controlFilter = null)
        {
            if (obj == null)
                throw new ArgumentNullException("obj");

            object parent = obj;
            do
            {
                parent = GetVisualOrLogicalAncestor(parent as DependencyObject);

                if ((parent is T) && ((controlFilter == null) || controlFilter((T)parent)))
                    break;
            }
            while (parent != null);

            return parent == null ? default : (T)parent;
        }

        private static Func<object, DependencyObject> _getInheritanceParentMethod = null;
        private static DependencyObject GetInheritanceParent(DependencyObject control)
        {
            if (_getInheritanceParentMethod == null)
                _getInheritanceParentMethod = ILMemberAccessor.GetPrivateGetPropertyGeneric<DependencyObject>(typeof(DependencyObject), "InheritanceParent");

            return _getInheritanceParentMethod(control);
        }
    }
}

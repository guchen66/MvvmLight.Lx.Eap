using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;
using System.Windows.Media.Media3D;
using System.Windows.Media;
using System.Windows;

namespace MvvmLight.Lx.Controls.Behaviors
{
    public static class TreeHelper
    {
        //
        // 摘要:
        //     Finds a parent of a given item on the visual tree.
        //
        // 参数:
        //   child:
        //     A direct or indirect child of the queried item.
        //
        // 类型参数:
        //   T:
        //     The type of the queried item.
        //
        // 返回结果:
        //     The first parent item that matches the submitted type parameter. If not matching
        //     item can be found, a null reference is being returned.
        public static T TryFindParent<T>(this DependencyObject child) where T : DependencyObject
        {
            DependencyObject parentObject = child.GetParentObject();
            if (parentObject == null)
            {
                return null;
            }

            T val = parentObject as T;
            return val ?? parentObject.TryFindParent<T>();
        }

        //
        // 摘要:
        //     Finds all Ancestors of a given item on the visual tree.
        //
        // 参数:
        //   child:
        //     A node in a visual tree
        //
        // 返回结果:
        //     All ancestors in visual tree of child element
        public static IEnumerable<DependencyObject> GetAncestors(this DependencyObject child)
        {
            for (DependencyObject parent = VisualTreeHelper.GetParent(child); parent != null; parent = VisualTreeHelper.GetParent(parent))
            {
                yield return parent;
            }
        }

        //
        // 摘要:
        //     Returns full visual ancestry, starting at the leaf.
        //
        //     If element is not of System.Windows.Media.Visual or System.Windows.Media.Media3D.Visual3D
        //     the logical ancestry is used.
        //
        // 参数:
        //   leaf:
        //     The starting object.
        public static IEnumerable<DependencyObject> GetVisualAncestry(this DependencyObject leaf)
        {
            while (leaf != null)
            {
                yield return leaf;
                leaf = ((leaf is Visual || leaf is Visual3D) ? VisualTreeHelper.GetParent(leaf) : LogicalTreeHelper.GetParent(leaf));
            }
        }

        //
        // 摘要:
        //     Tries to find and returns a visual ancestor, starting at the leaf.
        //
        //     If element is not of System.Windows.Media.Visual or System.Windows.Media.Media3D.Visual3D
        //     the logical ancestry is used.
        //
        // 参数:
        //   leaf:
        //     The starting object.
        public static T GetVisualAncestor<T>(this DependencyObject leaf) where T : DependencyObject
        {
            while (leaf != null)
            {
                if (leaf is T result)
                {
                    return result;
                }

                leaf = ((leaf is Visual || leaf is Visual3D) ? VisualTreeHelper.GetParent(leaf) : LogicalTreeHelper.GetParent(leaf));
            }

            return null;
        }

        //
        // 摘要:
        //     Finds a Child of a given item in the visual tree.
        //
        // 参数:
        //   parent:
        //     A direct parent of the queried item.
        //
        //   childName:
        //     x:Name or Name of child.
        //
        // 类型参数:
        //   T:
        //     The type of the queried item.
        //
        // 返回结果:
        //     The first parent item that matches the submitted type parameter. If not matching
        //     item can be found, a null parent is being returned.
        public static T FindChild<T>(this DependencyObject parent, string childName = null) where T : DependencyObject
        {
            if (parent == null)
            {
                return null;
            }

            T val = null;
            int childrenCount = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < childrenCount; i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(parent, i);
                if (!(child is T))
                {
                    val = child.FindChild<T>(childName);
                    if (val != null)
                    {
                        break;
                    }

                    continue;
                }

                if (!string.IsNullOrEmpty(childName))
                {
                    if (child is IFrameworkInputElement frameworkInputElement && frameworkInputElement.Name == childName)
                    {
                        val = (T)child;
                        break;
                    }

                    val = child.FindChild<T>(childName);
                    if (val != null)
                    {
                        break;
                    }

                    continue;
                }

                val = (T)child;
                break;
            }

            return val;
        }

        //
        // 摘要:
        //     This method is an alternative to WPF's System.Windows.Media.VisualTreeHelper.GetParent(System.Windows.DependencyObject)
        //     method, which also supports content elements. Keep in mind that for content element,
        //     this method falls back to the logical tree of the element!
        //
        // 参数:
        //   child:
        //     The item to be processed.
        //
        // 返回结果:
        //     The submitted item's parent, if available. Otherwise null.
        public static DependencyObject GetParentObject(this DependencyObject child)
        {
            if (child == null)
            {
                return null;
            }

            if (child is ContentElement contentElement)
            {
                DependencyObject parent = ContentOperations.GetParent(contentElement);
                if (parent != null)
                {
                    return parent;
                }

                if (!(contentElement is FrameworkContentElement frameworkContentElement))
                {
                    return null;
                }

                return frameworkContentElement.Parent;
            }

            DependencyObject parent2 = VisualTreeHelper.GetParent(child);
            if (parent2 != null)
            {
                return parent2;
            }

            if (child is FrameworkElement frameworkElement)
            {
                DependencyObject parent3 = frameworkElement.Parent;
                if (parent3 != null)
                {
                    return parent3;
                }
            }

            return null;
        }

        //
        // 摘要:
        //     Analyzes both visual and logical tree in order to find all elements of a given
        //     type that are descendants of the source item.
        //
        // 参数:
        //   source:
        //     The root element that marks the source of the search. If the source is already
        //     of the requested type, it will not be included in the result.
        //
        //   forceUsingTheVisualTreeHelper:
        //     Sometimes it's better to search in the VisualTree (e.g. in tests)
        //
        // 类型参数:
        //   T:
        //     The type of the queried items.
        //
        // 返回结果:
        //     All descendants of source that match the requested type.
        public static IEnumerable<T> FindChildren<T>(this DependencyObject source, bool forceUsingTheVisualTreeHelper = false) where T : DependencyObject
        {
            if (source == null)
            {
                yield break;
            }

            IEnumerable<DependencyObject> childObjects = source.GetChildObjects(forceUsingTheVisualTreeHelper);
            foreach (DependencyObject child in childObjects)
            {
                if (child != null && child is T)
                {
                    yield return (T)child;
                }

                foreach (T item in child.FindChildren<T>(forceUsingTheVisualTreeHelper))
                {
                    yield return item;
                }
            }
        }

        //
        // 摘要:
        //     This method is an alternative to WPF's System.Windows.Media.VisualTreeHelper.GetChild(System.Windows.DependencyObject,System.Int32)
        //     method, which also supports content elements. Keep in mind that for content elements,
        //     this method falls back to the logical tree of the element.
        //
        // 参数:
        //   parent:
        //     The item to be processed.
        //
        //   forceUsingTheVisualTreeHelper:
        //     Sometimes it's better to search in the VisualTree (e.g. in tests)
        //
        // 返回结果:
        //     The submitted item's child elements, if available.
        public static IEnumerable<DependencyObject> GetChildObjects(this DependencyObject parent, bool forceUsingTheVisualTreeHelper = false)
        {
            if (parent == null)
            {
                yield break;
            }

            if (!forceUsingTheVisualTreeHelper && (parent is ContentElement || parent is FrameworkElement))
            {
                foreach (object child in LogicalTreeHelper.GetChildren(parent))
                {
                    if (child is DependencyObject dependencyObject)
                    {
                        yield return dependencyObject;
                    }
                }
            }
            else if (parent is Visual || parent is Visual3D)
            {
                int count = VisualTreeHelper.GetChildrenCount(parent);
                for (int i = 0; i < count; i++)
                {
                    yield return VisualTreeHelper.GetChild(parent, i);
                }
            }
        }

        //
        // 摘要:
        //     Tries to locate a given item within the visual tree, starting with the dependency
        //     object at a given position.
        //
        // 参数:
        //   reference:
        //     The main element which is used to perform hit testing.
        //
        //   point:
        //     The position to be evaluated on the origin.
        //
        // 类型参数:
        //   T:
        //     The type of the element to be found on the visual tree of the element at the
        //     given location.
        public static T TryFindFromPoint<T>(UIElement reference, Point point) where T : DependencyObject
        {
            if (!(reference.InputHitTest(point) is DependencyObject dependencyObject))
            {
                return null;
            }

            if (dependencyObject is T result)
            {
                return result;
            }

            return dependencyObject.TryFindParent<T>();
        }

        public static bool IsDescendantOf(this DependencyObject node, DependencyObject reference)
        {
            bool result = false;
            DependencyObject dependencyObject = node;
            while (dependencyObject != null)
            {
                if (dependencyObject == reference)
                {
                    result = true;
                    break;
                }

                if (dependencyObject is Popup popup)
                {
                    dependencyObject = popup;
                    if (popup != null)
                    {
                        dependencyObject = popup.Parent;
                        if (dependencyObject == null)
                        {
                            dependencyObject = popup.PlacementTarget;
                        }
                    }
                }
                else
                {
                    dependencyObject = dependencyObject.GetParentObject();
                }
            }

            return result;
        }
    }
}

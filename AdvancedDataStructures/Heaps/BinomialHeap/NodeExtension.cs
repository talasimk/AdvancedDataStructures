using System;

namespace AdvancedDataStructures.Heaps.BinomialHeap
{
    internal static class NodeExtension
    {
        public static void LinkNodes<T>(this Node<T> child, Node<T> parent) where T : IComparable
        {
            child.Parent = parent;
            child.Sibling = parent.Child;
            parent.Child = child;
            ++parent.Degree;
        }

        public static void Heapify<T>(this Node<T> source) where T : IComparable
        {
            Node<T> current = source;
            while (current.Parent != null)
            {
                if (current.Data.CompareTo(current.Parent.Data) == -1)
                {
                    current.SwapWithParent();
                }
                current = current.Parent;
            }
        }

        public static void ReduceKey<T>(this Node<T> source) where T : IComparable
        {
            Node<T> current = source;
            while (current.Parent != null)
            {
               current.SwapWithParent();

               current = current.Parent;
            }
        }

        public static Node<T> FindNodeByValue<T>(this Node<T> source, T value) where T : IComparable
        {
            Node<T> sibling = source;
            while (sibling != null)
            {
                if (sibling.Data.CompareTo(value) == 0)
                    return sibling;

                Node<T> child = sibling.Child;
                while (child != null)
                {
                    Node<T> result = child.FindNodeByValue(value);
                    if (result != null)
                        return result;

                    child = child.Child;
                }

                sibling = sibling.Sibling;
            }

            return null;
        }

            private static void SwapWithParent<T>(this Node<T> current) where T : IComparable
        {
            T tmp = current.Data;
            current.Data = current.Parent.Data;
            current.Parent.Data = tmp;
        }
    }
}

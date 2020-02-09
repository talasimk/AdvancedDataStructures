using System;

namespace AdvancedDataStructures.Heaps.BinomialHeap
{
    public class BinomialHeap<T> where T : IComparable
    {
        public Node<T> Head { get; private set; }

        public BinomialHeap() { }

        public BinomialHeap(T value) => Head = new Node<T>() { Data = value };

        public void Insert(T value)
        {
            Node<T> node = new Node<T>() { Data = value };
            if (Head == null)
                Head = node;
            else
            {
                var newHeap = new BinomialHeap<T>(value);
                Union(newHeap);
            }
        }

        public T FindMinimum()
        {
            Node<T> minNode = FindMinimumNode();
            return minNode.Data;
        }

        public T ExtractMinimum()
        {
            BinomialHeap<T> resultHeap = new BinomialHeap<T>();
            Node<T> minRoot = FindMinimumNode();
            RemoveFromRoots(minRoot);

            Node<T> newRoot = minRoot.Child;
            RemoveFromChildren(minRoot);

            resultHeap.Head = newRoot;
            if (resultHeap.Head != null)
            {
                newRoot = newRoot.Sibling;
                resultHeap.Head.Sibling = null;
                while (newRoot != null)
                {
                    BinomialHeap<T> tmp = new BinomialHeap<T>();

                    tmp.Head = newRoot;
                    newRoot = newRoot.Sibling;
                    tmp.Head.Sibling = null;

                    resultHeap.Union(tmp);
                }

                if (Head == null)
                    Head = resultHeap.Head;
                else
                    Union(resultHeap);
            }

            return minRoot.Data;
        }

        private void RemoveFromChildren(Node<T> node)
        {
            Node<T> child = node.Child;

            while (child != null)
            {
                child.Parent = null;
                child = child.Sibling;
            }
        }

        private void RemoveFromRoots(Node<T> node)
        {
            if (node == Head)
            {
                Head = node.Sibling;
                return;
            }

            Node<T> root = Head;
            while (root != null)
            {
                if (root.Sibling == node)
                    root.Sibling = node.Sibling;
                root = root.Sibling;
            }
        }

        public T Delete(T value)
        {
            var node = Head.FindNodeByValue(value);
            T nodeValue = node.Data;
            if (node == null)
                throw new Exception("There are no element with such value");

            node.ReduceKey();
            ExtractMinimum();
            return node.Data;
        }

        public void DecreaseKey(T value, T newValue)
        {
            var node = Head.FindNodeByValue(value);
            if (node == null)
                throw new Exception("There are no element with such value");
            node.Data = newValue;
            node.Heapify();
        }

        public void Union(BinomialHeap<T> heapToUnite)
        {
            Merge(heapToUnite);
            if (Head == null)
                return;

            Node<T> previousRoot = null;
            Node<T> currentRoot = Head;
            Node<T> nextRoot = currentRoot.Sibling;

            while (nextRoot != null)
            {
                if ((currentRoot.Degree != nextRoot.Degree) || (nextRoot.Sibling != null && currentRoot.Degree == nextRoot.Sibling.Degree))
                {
                    previousRoot = currentRoot;
                    currentRoot = nextRoot;
                }
                else
                {
                    if (currentRoot.Data.CompareTo(nextRoot.Data) != 1)
                    {
                        currentRoot.Sibling = nextRoot.Sibling;
                        nextRoot.LinkNodes(currentRoot);
                    }
                    else
                    {
                        if (previousRoot == null)
                            Head = nextRoot;
                        else
                            previousRoot.Sibling = nextRoot;

                        currentRoot.LinkNodes(nextRoot);
                        currentRoot = nextRoot;
                    }
                }
                nextRoot = currentRoot.Sibling;
            }
        }

        private Node<T> FindMinimumNode()
        {
            Node<T> minNode = null;
            Node<T> sibling = Head;

            while (sibling != null)
            {
                if (minNode == null || sibling.Data.CompareTo(minNode.Data) == -1)
                {
                    minNode = sibling;
                }

                sibling = sibling.Sibling;
            }

            return minNode;
        }

        private void Merge(BinomialHeap<T> heapToMerge)
        {
            Node<T> head1 = Head;
            Node<T> head2 = heapToMerge.Head;
            Node<T> headOfHeap;

            if (head1.Degree < head2.Degree)
            {
                headOfHeap = head1;
                head1 = head1.Sibling;
            }
            else
            {
                headOfHeap = head2;
                head2 = head2.Sibling;
            }

            while (head1 != null && head2 != null)
            {
                if (head1.Degree < head2.Degree)
                    head1 = head1.Sibling;
                else
                    head2 = head2.Sibling;
            }

            Head = headOfHeap;
        }
    }
}

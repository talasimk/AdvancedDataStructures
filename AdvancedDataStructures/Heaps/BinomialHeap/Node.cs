using System;
using System.Collections.Generic;
using System.Text;

namespace AdvancedDataStructures.Heaps.BinomialHeap
{
    public class Node<T> where T : IComparable
    {
        public T Data { get; set; }
        public int Degree { get; set; }

        public Node<T> Child { get; set; }
        public Node<T> Parent { get; set; }
        public Node<T> Sibling { get; set; }
    }
}

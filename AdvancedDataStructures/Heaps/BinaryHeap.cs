using System;
using System.Collections.Generic;

namespace AdvancedDataStructures.Heaps
{
    public class BinaryHeap<T> where T : IComparable
    {
        private List<T> elements;
        private int capacity;

        public BinaryHeap()
        {
            elements = new List<T>();
        }
        
        public BinaryHeap(int capacity)
        {
            this.capacity = capacity;
            elements = new List<T>(capacity);
        }

        public BinaryHeap(IEnumerable<T>values)
        {
            elements = new List<T>();
            foreach (var value in values)
            {
                Insert(value);
            }
        }

        public T GetMin
        {
            get
            {
                if (elements.Count < 1) throw new Exception("Heap is empty");
                return elements[0];
            }
        }

        public void Insert(T value)
        {
            int newSize = elements.Count;
            elements.Add(value);
            siftUp(newSize);
        }

        public void DecreaseKey(T oldValue, T newValue)
        {
            int index = FindIndex(oldValue);
            if (index == -1)
                throw new Exception("No such value");

            elements[index] = newValue;
            siftUp(index);
        }

        public T ExtractMin()
        {
            if (elements.Count < 1) throw new Exception("Heap is empty");

            T root = elements[0];

            elements.RemoveAt(0);

            if (elements.Count > 1)
                siftDown(0);

            return root;
        }

        public void DeleteElement(T value)
        {
            int index = FindIndex(value);
            if (index == -1)
                throw new Exception("No such value");

            elements.RemoveAt(index);
            if (elements.Count > 1)
                siftDown(0);
        }

        private int FindIndex(T value)
        {
            for (int i = 0; i < elements.Count; ++i)
                if (elements[i].CompareTo(value) == 0)
                    return i;
            
            return -1;
        }

        private int parent(int i) { return (i - 1) / 2; }

        private int left(int i) { return (2 * i + 1); }

        private int right(int i) { return (2 * i + 2); }

        private void siftDown(int index)
        {
            int i = index;
            int leftIndex, rightIndex;
            while(left(i) < elements.Count)
            {
                leftIndex = left(i);
                rightIndex = right(i);
                int j = leftIndex;
                if(rightIndex < elements.Count && elements[rightIndex].CompareTo(elements[leftIndex]) == -1)
                    j = rightIndex;
                if (elements[i].CompareTo(elements[j]) != 1)
                    break;
                swap(i, j);
                i = j;
            }
        }

        private void siftUp(int index)
        {
            int i = index;
            while(elements[i].CompareTo(elements[parent(i)]) == -1)
            {
                swap(i, parent(i));
                i = parent(i);
            }
        }

        private void swap(int firstIndex, int secondIndex)
        {
            T temp = elements[firstIndex];
            elements[firstIndex] = elements[secondIndex];
            elements[secondIndex] = temp;
        }
    }
}

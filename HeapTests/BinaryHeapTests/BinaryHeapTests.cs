using AdvancedDataStructures.Heaps;
using NUnit;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace HeapTests.BinaryHeapTests
{
    class BinaryHeapTests
    {
        private List<FakeClass> fakeElements;

        [SetUp]
        public void Setup()
        {
            fakeElements = new List<FakeClass>()
            {
                new FakeClass() { Value = 12 },
                new FakeClass() { Value = -112 },
                new FakeClass() { Value = 120 },
                new FakeClass() { Value = 29 }
            };
        }

        [Test]
        public void GetMin_With_ValidIntValues_Returns_MinValue()
        {
            //Arrange
            var heap = new BinaryHeap<int>(5);
            int minValue = -1;
            heap.Insert(2);
            heap.Insert(9);
            heap.Insert(5);
            heap.Insert(minValue);
            heap.Insert(7);

            //Act
            int min = heap.GetMin;

            //Assert
            Assert.AreEqual(minValue, min);
        }

        [Test]
        public void GetMin_With_ValidStringValues_Returns_MinValue()
        {
            //Arrange
            var heap = new BinaryHeap<string>(5);
            string minValue = "a";
            heap.Insert(minValue);
            heap.Insert("b");
            heap.Insert("gdhdg");

            //Act
            string min = heap.GetMin;

            //Assert
            Assert.AreEqual(minValue, min);
        }

        [Test]
        public void GetMin_With_ValidFakeClassValues_Returns_MinValue()
        {
            //Arrange
            var heap = new BinaryHeap<FakeClass>(fakeElements);
            var minValue = fakeElements[1];

            //Act
            var min = heap.GetMin;

            //Assert
            Assert.AreEqual(minValue, min);
            
        }

        [Test]
        public void ExtractMin_With_ValidFakeClassValue_Returns_MinValue()
        {
            //Arrange
            var heap = new BinaryHeap<FakeClass>(fakeElements);
            var minValue = fakeElements[1];
            var checkedNext = fakeElements[0];

            //Act
            var min = heap.ExtractMin();
            var newValue = heap.GetMin;

            //Assert
            Assert.AreEqual(minValue, min);
            Assert.AreEqual(checkedNext, newValue);
        }

        [Test]
        public void DecreaseKey_With_ValidValues_Returns_NewMinValue()
        {
            //Arrange
            var heap = new BinaryHeap<FakeClass>(fakeElements);
            var valueForChanged = fakeElements[0];
            var newValue = new FakeClass() { Value = int.MinValue };

            //Act
            heap.DecreaseKey(valueForChanged, newValue);
            var newMin = heap.GetMin;

            //Assert
            Assert.AreEqual(newValue, newMin);
        }

        [Test]
        public void DeleteElement_With_ValidFakeClassValue_Returns_NewMinValue()
        {
            //Arrange
            var heap = new BinaryHeap<FakeClass>(fakeElements);
            var valueForChanged = fakeElements[0];
            var newValue = new FakeClass() { Value = int.MinValue };

            //Act
            heap.DecreaseKey(valueForChanged, newValue);
            var newMin = heap.GetMin;

            //Assert
            Assert.AreEqual(newValue, newMin);
        }


        private class FakeClass : IComparable
        {
            public int Value { get; set; }
            public int CompareTo(object obj)
            {
                var cprClass = obj as FakeClass;

                return Value.CompareTo(cprClass.Value);
            }
        }
    }
}

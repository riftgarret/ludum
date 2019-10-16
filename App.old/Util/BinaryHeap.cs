using System;
using System.Collections;
using System.Collections.Generic;

namespace App.Util
{
    /// <summary>
    /// Binary heap. A priority queue implementation. Required that elements implement IComparable
    /// </summary>
    public class BinaryHeap<T> where T : IComparable
    {
        // head element
        private HeapChild head;

        private T[] heap;
        private Dictionary<T, int> positionDict;
        private int tail;

        private bool min;

        /// <summary>
        /// Create a new Heap with min property as 'top'
        /// </summary>
        /// <param name="isMin">If set to <c>true</c> is minimum.</param>
        public BinaryHeap(bool isMin)
        {
            min = isMin;
            positionDict = new Dictionary<T, int>();
            heap = new T[16];
            tail = -1;
        }

        /// <summary>
        /// Insert the specified item.
        /// </summary>
        /// <param name="item">Item.</param>
        public void Insert(T item)
        {
            tail++;
            if (tail >= heap.Length)
            {
                T[] newSrc = new T[heap.Length * 2];
                Array.Copy(heap, newSrc, heap.Length);
                heap = newSrc;
            }
            heap[tail] = item;
            positionDict[item] = tail;
            BubbleUp(tail);
        }

        /// <summary>
        /// When adding an element to the heap, bubble up the min / max
        /// </summary>
        /// <param name="index">Index.</param>
        private void BubbleUp(int index)
        {
            int minMax = min ? 1 : -1;
            while (index > 0)
            {
                // because we start at zero, dividing by 2 needs to be -1, to get correct half index
                int parent = (index - 1) / 2;
                if (heap[parent].CompareTo(heap[index]) * minMax <= 0)
                {
                    break;
                }
                // swap and continue
                Swap(index, parent);

                // update index and continue traverse
                index = parent;
            }
        }

        /// <summary>
        /// Look at the top
        /// </summary>
        public T Pop()
        {
            if (tail < 0)
            {
                return default(T);
            }

            T top = heap[0];
            positionDict.Remove(top);
            if (tail > 0)
            {
                heap[0] = heap[tail];
                positionDict[heap[0]] = 0;
            }
            tail--;
            FixDown(0);
            return top;
        }

        private void Swap(int i1, int i2)
        {
            positionDict[heap[i2]] = i1;
            positionDict[heap[i1]] = i2;

            T temp = heap[i2];
            heap[i2] = heap[i1];
            heap[i1] = temp;
        }

        private void FixDown(int index)
        {
            int minMax = min ? 1 : -1;
            while (index < tail)
            {
                int leftChild = index * 2 + 1;
                int rightChild = index * 2 + 2;
                int largest = index;
                if (leftChild <= tail && heap[leftChild].CompareTo(heap[index]) * minMax >= 0)
                {
                    largest = leftChild;
                }

                if (rightChild <= tail && heap[rightChild].CompareTo(heap[index]) * minMax >= 0)
                {
                    largest = rightChild;
                }

                if (largest == index)
                {
                    break;
                }

                Swap(index, largest);
                index = largest;
            }
        }



        public T Peek()
        {
            if (tail < 0)
            {
                return default(T);
            }

            return heap[0];
        }



        class HeapChild
        {
            T child;
            HeapChild left;
            HeapChild right;
        }
    } 
}

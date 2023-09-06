using System;
using System.Collections.Generic;

namespace Veri_Yapilari_CS.Heap
{
    public class MinHeap<T> : Heap<T> where T : IComparable<T>
    {
        public MinHeap() 
        {
            
        }
        public MinHeap(IEnumerable<T> collection)
        {
            foreach (var item in collection)
                Add(item);
        }
        protected override int Karsilastir(T value1, T value2)
        {
            return value2.CompareTo(value1);
        }

    }
}

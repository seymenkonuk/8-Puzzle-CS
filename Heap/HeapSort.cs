using System;

namespace Veri_Yapilari_CS.Heap
{
    public enum SiralamaTuru
    {
        Ascending,
        Descending
    }
    static class HeapSort
    {
        public static void Sort<T>(T[] dizi, SiralamaTuru tur) where T : IComparable<T>
        {
            Heap<T> heap;
            if (tur == SiralamaTuru.Ascending)
                heap = new MinHeap<T>(dizi);
            else
                heap = new MaxHeap<T>(dizi);

            for (int i = 0; heap.Count > 0; i++)
                dizi[i] = heap.Delete();
        }
    }
}

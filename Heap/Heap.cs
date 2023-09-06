using System;
using System.Collections;

namespace Veri_Yapilari_CS.Heap
{
    public abstract class Heap<T> : IEnumerable<T> where T : IComparable<T>
    {
        protected readonly List<T> liste = new List<T>();

        public int Count => liste.Count;

        public object this[int index]
        {
            get => liste[index];
        }

        abstract protected int Karsilastir(T value1, T value2);

        public void Add(T value)
        {
            int pos = Count;
            liste.Add(value);

            while (pos > 0)
            {
                int parent = (pos - 1) / 2;

                if (Karsilastir(liste[pos], liste[parent]) > 0)
                {
                    T depo = liste[pos];
                    liste[pos] = liste[parent];
                    liste[parent] = depo;
                }
                else
                    break;

                pos = parent;
            }

        }

        public T Peek()
        {
            if (Count == 0)
                throw new Exception("Eleman Yok!");
            return liste[0];
        }

        public T Delete()
        {
            if (Count == 0)
                throw new Exception("Eleman Yok!");
            T sonuc = liste[0];

            liste[0] = liste[^1];
            liste.RemoveAt(liste.Count - 1);

            int pos = 0;
            
            while (true)
            {
                int left = pos * 2 + 1;
                int right = pos * 2 + 2;

                // En Küçük/En Büyük Hesapla
                int yeni = pos;
                if (left < Count && Karsilastir(liste[left], liste[yeni]) > 0)
                    yeni = left;
                if (right < Count && Karsilastir(liste[right], liste[yeni]) > 0)
                    yeni = right;

                if (pos == yeni) break;

                T depo = liste[pos];
                liste[pos] = liste[yeni];
                liste[yeni] = depo;

                pos = yeni;
            }

            return sonuc;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return liste.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}

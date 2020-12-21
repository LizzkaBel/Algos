using System;
using System.Diagnostics;

namespace Lab1Alghorithm
{
    class Program
    {
        static void InsertSort(int[] arr, int start, int end)
        {
            //лучший случай 16n ~ O(n)
            //худший случай 12n * 9n  ~ O(n^2)
            int key, pointer;
            for (int i = start + 1; i <= end; i++) //2+2+2 6n
            {
                key = arr[i]; //2
                pointer = i - 1; //2
                while (pointer >= start && arr[pointer] > key) //4n
                {
                    arr[pointer + 1] = arr[pointer]; //3
                    pointer--; //2
                }

                arr[pointer + 1] = key; //2
            }
        }

        static void Merge(int[] array, int start, int middle, int end)
        {
            int left = start;
            int right = middle + 1;
            int[] tempArray = new int[end - start + 1];
            int index = 0;

            while ((left <= middle) && (right <= end))
            {
                if (array[left] < array[right])
                {
                    tempArray[index] = array[left];
                    left++;
                }
                else
                {
                    tempArray[index] = array[right];
                    right++;
                }

                index++;
            }

            while (left <= middle)
                tempArray[index++] = array[left++];

            while (right <= end)
                tempArray[index++] = array[right++];

            for (int i = 0; i < tempArray.Length; i++)
            {
                array[start + i] = tempArray[i];
            }
        }

        static void MergeSort(int[] arr, int start, int end, int koef)
        {
            if (start < end)
            {
                if (end - start + 1 <= koef)
                {
                    InsertSort(arr, start, end);
                }
                else
                {
                    int middle = (start + end) / 2;
                    MergeSort(arr, start, middle, koef);
                    MergeSort(arr, middle + 1, end, koef);
                    Merge(arr, start, middle, end);
                }
            }
        }

        static void Main(string[] args)
        {
            const int sizeOfArray = 10000;
            const int leftLimitOfArray = 1;
            const int rightLimitOfArray = 100000;
            double bestTime = 10000;
            int koef = default(int);
            Random rand = new Random();
            int[] arr = new int[sizeOfArray];
            Stopwatch stopwatch = new Stopwatch();
            Stopwatch stopwatch1 = new Stopwatch();
            stopwatch1.Start();
            for (int l = 0; l < 50; l++)
            {
                stopwatch.Start();
                for(int j = 0; j < 1000; j++){
                    for (int i = 0; i < sizeOfArray; i++)
                    {
                        arr[i] = rand.Next(leftLimitOfArray, rightLimitOfArray);
                    }

                    MergeSort(arr, 0, sizeOfArray - 1, l);
                }
                stopwatch.Stop();
                if (bestTime > stopwatch.Elapsed.TotalSeconds)
                {
                    bestTime = stopwatch.Elapsed.TotalSeconds;
                    koef = l;
                }
                Console.WriteLine("Потрачено тактов в сумме на выполнение: " + stopwatch.Elapsed.ToString() + $" Порядковый номер k: {l} ");
                stopwatch.Reset();
            }
            stopwatch1.Stop();
            Console.WriteLine("Потрачено тактов на выполнение: " + stopwatch1.Elapsed.ToString());

            Console.WriteLine($"{bestTime} - коэффицент k {koef} ");

            foreach (int elem in arr)
            {
                Console.Write($"{elem} ");
            }
        }
    }
}
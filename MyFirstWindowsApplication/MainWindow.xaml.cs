using MyFirstWindowsApplication.dynamic_elements;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MyFirstWindowsApplication
{
    public partial class MainWindow : Window
    {
        private int SelectedAlgorithm = 0;
        public MainWindow()
        {
            InitializeComponent();
            SortingAlgoritmsComboBox.SelectedIndex = 0;
        }
        private void Start_Sort(object sender, RoutedEventArgs e)
        {
            int[] arr = new int[57];
            var random = new Random();
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = random.Next((int)DrawingField.ActualHeight);
            }

            BarChart barChart = new BarChart(arr.Length);
            barChart.SetValues(arr);
            barChart.Clear(DrawingField);
            barChart.Draw(DrawingField);

            Task.Run(() =>
            {
                switch (SelectedAlgorithm)
                {
                    case 0:
                        bubblesort(arr, barChart);
                        break;
                    case 1:
                        Insertionsort(arr, barChart);
                        break;
                    case 2:
                        SelectionSort(arr, barChart);
                        break;
                    case 3:
                        MergeSort(arr, 0, arr.Length - 1, barChart);
                        break;
                    case 4:
                        QuickSort(arr, 0, arr.Length - 1, barChart);
                        break;
                }
            });
        }
        private void sortComboBox_SelectedChanged(object sender, SelectionChangedEventArgs e)
        {
            if(SortingAlgoritmsComboBox.SelectedValue != null)
            {
                SelectedAlgorithm = SortingAlgoritmsComboBox.SelectedIndex;
            }
        }

        private void StartMoving(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
        private void Exit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        //Sorting algoritms
        void bubblesort(int[] arr, BarChart barChart)
        {
            bool b = true;
            int r = arr.Length;
            while (b)
            {
                b = false;
                for (int i = 0; i < r - 1; i++)
                {
                    if (arr[i] > arr[i + 1])
                    {
                        int buf = arr[i + 1];
                        arr[i + 1] = arr[i];
                        arr[i] = buf;
                        b = true;
                        Dispatcher.Invoke(() =>
                        {
                            barChart.SetValues(arr);
                        });
                    }
                    Thread.Sleep(30);
                }
                r--;
            }
        }
        // Insertion sort
        void Insertionsort(int[] arr, BarChart barChart)
        {
            for (int i = 1; i < arr.Length; i++)
            {
                int temp = arr[i];
                int j = i - 1;
                while (j >= 0 && arr[j] > temp)
                {
                    arr[j + 1] = arr[j];
                    j--;
                    Dispatcher.Invoke(() =>
                    {
                        barChart.SetValues(arr);
                    });
                    Thread.Sleep(30);
                }
                arr[j + 1] = temp;
            }
        }
        // Selection Sort
        void SelectionSort(int[] arr, BarChart barChart)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                int min = arr[i];
                int index = i;
                for (int j = i + 1; j < arr.Length; j++)
                {
                    if (arr[j] < min)
                    {
                        min = arr[j];
                        index = j;
                    }
                    Thread.Sleep(30);
                }
                int buf = arr[i];
                arr[i] = min;
                arr[index] = buf;
                Dispatcher.Invoke(() =>
                {
                    barChart.SetValues(arr);
                });
            }
        }
        // Merge Sort
        void MergeSort(int[] a, int l, int r, BarChart barChart)
        {
            int m;

            if (l < r)
            {
                m = (l + r) / 2;
                MergeSort(a, l, m, barChart);
                MergeSort(a, m + 1, r, barChart);
                Merge(a, l, r, barChart);
                Dispatcher.Invoke(() =>
                {
                    barChart.SetValues(a);
                });
                Thread.Sleep(30);
            }
        }
        void Merge(int[] arr, int l, int r, BarChart barChart) // начало левой части конец правой части r
        {
            int m = (l + r) / 2;
            if (arr.Length <= 1)
            {
                return;
            }
            int n1 = m - l + 1;
            int n2 = r - m;

            int[] left = new int[n1 + 1];
            int[] right = new int[n2 + 1];


            int i = 0;
            int j = 0;
            for (i = 0; i < n1; i++)
            {
                left[i] = arr[l + i];
            }

            for (j = 1; j <= n2; j++)
            {
                right[j - 1] = arr[m + j];
            }

            left[n1] = int.MaxValue;
            right[n2] = int.MaxValue;

            i = 0;
            j = 0;

            for (int k = l; k <= r; k++)
            {
                if (left[i] < right[j])
                {
                    arr[k] = left[i];
                    i = i + 1;
                }
                else
                {
                    arr[k] = right[j];
                    j = j + 1;
                }
                Dispatcher.Invoke(() =>
                {
                    barChart.SetValues(arr);
                });
                Thread.Sleep(30);
            }
        }
        // Quick Sort
        void QuickSort(int[] arr, int minIndex, int maxIndex, BarChart barChart)
        {
            if (minIndex >= maxIndex)
            {
                return;
            }
            int pivotIndex = GetPivotIndex(arr, minIndex, maxIndex, barChart);

            QuickSort(arr, minIndex, pivotIndex - 1, barChart);

            QuickSort(arr, pivotIndex + 1, maxIndex, barChart);
        }
        int GetPivotIndex(int[] array, int minIndex, int maxIndex, BarChart barChart)
        {
            int pivot = minIndex - 1;

            for (int i = minIndex; i <= maxIndex; i++)
            {
                if (array[i] < array[maxIndex])
                {
                    pivot++;
                    Swap(ref array[pivot], ref array[i]);
                }
                Dispatcher.Invoke(() =>
                {
                    barChart.SetValues(array);
                });
                Thread.Sleep(30);
            }

            pivot++;
            Swap(ref array[pivot], ref array[maxIndex]);
            Dispatcher.Invoke(() =>
            {
                barChart.SetValues(array);
            });
            Thread.Sleep(30);

            return pivot;
        }
        void Swap(ref int leftItem, ref int rightItem)
        {
            int temp = leftItem;

            leftItem = rightItem;

            rightItem = temp;
        }
        // Heap Sort
        // Counting Sort
        // Radix Sort
        // Timsort

    }
}
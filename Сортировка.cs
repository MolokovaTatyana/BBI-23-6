
using System;
using System.Security.AccessControl;

class Sportsmen
{
    private string famile;
    private double rez1, rez2, rez;
    private bool dis = false;
    public void Diss()
    {
        dis = true;
    }
    public Double Rez { get { return rez; } }
    private double max(double x, double y)
    {
        if (x > y)
        {
            return x;
        }
        else
        {
            return y;
        }
    }
    public Sportsmen(string famile1, double rezz1, double rezz2)
    {
        famile = famile1;
        rez1 = rezz1;
        rez2 = rezz2;
        rez = max(rez1, rez2);
    }
    public void Print()
    {
        if (dis == false)
        {
            Console.WriteLine("Фамилия {0}\t  Результат  {1:f2}", famile, rez);
        }
    }
}
class Program
{
    static void Main(string[] args)
    {
        Sportsmen[] sp = new Sportsmen[3];
        sp[0] = new Sportsmen("Романов", 1.48, 1.62);
        sp[1] = new Sportsmen("Кузнецов", 1.6, 1.55);
        sp[2] = new Sportsmen("Лавров", 1.39, 1.35);
        for (int i = 0; i < sp.Length; i++)
        {
            sp[i].Print();
        }
        sp = QuickSort(sp, 0, sp.Length - 1);
        Console.WriteLine();
        sp[0].Diss();
        for (int i = 0; i < sp.Length; i++)
        {
            sp[i].Print();

        }
        Console.ReadKey();
    }
    static int FindPivot(Sportsmen[] a, int minIndex, int maxIndex)
    {
        int pivot = minIndex - 1;
        Sportsmen temp;
        for (int i = minIndex; i < maxIndex; i++)
        {
            if (a[i].Rez < a[maxIndex].Rez)
            {
                pivot++;
                temp = a[pivot];
                a[pivot] = a[i];
                a[i] = temp;
            }
        }
        pivot++;
        temp = a[pivot];
        a[pivot] = a[maxIndex];
        a[maxIndex] = temp;
        return pivot;
    }
    static Sportsmen[] QuickSort(Sportsmen[] a, int minIndex, int maxIndex)
    {
        if (minIndex >= maxIndex)
        {
            return a;
        }
        int pivot = FindPivot(a, minIndex, maxIndex);
        QuickSort(a, minIndex, pivot - 1);
        QuickSort(a, pivot + 1, maxIndex);
        return a;
    }
}


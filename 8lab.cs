using System;
using System.Reflection.Metadata.Ecma335;
using System.Text.RegularExpressions; 
abstract class Task
{
    protected string result;
    public Task(string text) { }
    protected abstract void ParseText(string text); 
    protected virtual int Count() 
    {
        return -1;
    }
    protected double CountPersent(int number, int total)
    {
        return (double)number / (double)total * 100;
    }
    protected Dictionary<string, char> special_pairs = new Dictionary<string, char>();

}

class Task_8 : Task
{
    public Task_8(string text) : base(text)
    {
        ParseText(text);
    }
    public override string ToString()
    {
        return result;
    }
    protected override void ParseText(string text)
    {
        string[] words = text.Split();
        List<string> Stroki = new List<string>();
        string num = "";
        foreach(string word in words)
        {
            if(num.Length + word.Length > 50)
            {
                num = num.Remove(num.Length - 1);
                Stroki.Add(num);
                num = "";
            }
            num += word + " ";
        }
        num = num.Remove(num.Length - 1);
        Stroki.Add(num);
        Stroki = Plus(Stroki);
        foreach (string stroka in Stroki)
        {
            result += stroka + "\n"; //переход на след строку
        }

    }
    public List<string> Plus(List<string> a)
    {
        for(int i = 0; i < a.Count; i++)
        {
            string[] w = a[i].Split(" ");
            int lp = 50 - a[i].Length;
            int p = lp / (w.Length - 1);
            int f = lp % (w.Length - 1);
            for(int j = 0; j < w.Length -1; j++)
            {
                for(int k = 0; k < p+1; k++)
                {
                    w[j] += " ";
                }
                if(f > 0)
                {
                    w[j] += " ";
                    f = f - 1;
                }
            }
            a[i] = string.Join("", w);
        }
        return a;
    }

}

class Task_9 : Task
{
    public Task_9(string text) : base(text)
    {
        this.text = text;
        ParseText(text);
    }
    public override string ToString()
    {
        return result;
    }
    public string text;

    protected override void ParseText(string text)
    {
        Dictionary<string, int> pairs = new Dictionary<string, int>();
        for (int i = 0; i < text.Length - 1; i++)
        {
            if (char.IsLetter(text[i]) && char.IsLetter(text[i + 1]))
            {
                string p = text.Substring(i, 2);
                if (!pairs.ContainsKey(p))
                {
                    pairs[p] = 1;
                }
                else
                {
                    pairs[p]++;
                }
            }
        }
        char c = '}';
        foreach (var pair in pairs)
        {
            if (pair.Value > 1)
            {
                special_pairs[pair.Key] = c;
                c++;
            }
        }
        foreach (var special_pair in special_pairs)
        {
            text = text.Replace(special_pair.Key, special_pair.Value.ToString());
        }
        result += text;
        foreach (var special_pair in special_pairs)
        {
            result += special_pair.Key + "–" + special_pair.Value + "\n";
        }
    }
}

class Task_10: Task
{
    public Task_10(string text) : base(text)
    {
        ParseText(text);
    }
    public override string ToString()
    {
        return result;
    }
    protected override void ParseText(string text)
    {
        List<KeyValuePair <string, char>> list = new List<KeyValuePair <string, char>>();
        foreach(var special_pair in special_pairs)
        {
            list.Add(special_pair);
        }
        list.Reverse();
        foreach(var inlist in list)
        {
            text = text.Replace(inlist.Value.ToString(), inlist.Key);
        }
        result += text;
    }
}

class Task_12: Task
{
    public Task_12(string text, Dictionary<string, string> a) : base(text)
    {
        this.a = a;
        ParseText(text);    
    }
    protected Dictionary<string, string> a;
    public override string ToString()
    {
        return result;
    }
    protected override void ParseText(string text)
    {
        foreach(var zn in a)
        {
            text = text.Replace(zn.Key, zn.Value);
        }
        result += text;
    }  
}

class Task_13 : Task
{
    public Task_13(string text) : base(text)
    {
        ParseText(text);
    }
    public override string ToString()
    {
        return result;
    }
    protected override int Count()
    {
        return 0;
    }
    protected override void ParseText(string text)
    {
        text = text.ToLower();
        Dictionary<char, double>Bukva = new Dictionary<char, double>();
        string[] slova = text.Split();
        foreach(string slovo in slova)
        {
            if(slovo.Length > 0 && char.IsLetter(slovo[0]))
            {
                char first = slovo[0];
                if (!Bukva.ContainsKey(first))
                {
                    Bukva[first] = 0;
                    Bukva[first]++;
                }
                else
                {
                    Bukva[first]++;
                }
            }
        }
        Dictionary<char, double>Persent = new Dictionary<char, double>();
        foreach(var b in Bukva)
        {
            Persent[b.Key] = CountPersent(Convert.ToInt32(b.Value), Bukva.Count);
            result += b.Key + " " + Persent[b.Key] + "\n";
        }

    }
}

class Task_15 : Task
{
    private int sum = 0;
    public Task_15(string text) : base(text)
    {
        ParseText(text);
    }
    public override string ToString()
    {
        return "Сумма равнa: " + sum;
    }
    protected override void ParseText(string text)
    {
        string[] slova = text.Split();
        foreach(string slovo in slova)
        {
            for(int i = 0; i < slovo.Length; i++)
            {
                string numbers = "";
                while (i < slovo.Length && char.IsDigit(slovo[i]))
                {
                    numbers += slovo[i];
                    i++;
                }
                if(numbers.Length > 0)
                {
                    sum += int.Parse(numbers);
                }
            }
        }
    }

}

class Program
{
    public static void Main()
    {
        string text = ("Первое кругосветное путешествие было осуществлено флотом, возглавляемым португальским исследователем Фернаном Магелланом. Путешествие началось 20 сентября 1519 года, когда флот, состоящий из пяти кораблей и примерно 270 человек, отправился из порту Сан-Лукас в Испании. Хотя Магеллан не закончил свое путешествие из-за гибели в битве на Филиппинах в 1521 году, его экспедиция стала первой, которая успешно обогнула Землю и доказала ее круглую форму. Это путешествие открыло новые морские пути и имело огромное значение для картографии и географических открытий.");
        Task_8 task8 = new Task_8(text); 
        Task_9 task9 = new Task_9(text);
        Console.WriteLine(task8);
        Console.WriteLine(task9);
        Task_10 task10 = new Task_10(task9.text);
        Console.WriteLine(task10);
        Dictionary<string, string> zamena = new Dictionary<string, string> { { "#", "была" } };
        Task_12 task12 = new Task_12("Погода # солнечной. Погода # ветреной.", zamena);
        Console.WriteLine(task12);

    }
}


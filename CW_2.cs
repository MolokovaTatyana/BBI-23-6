using System.Text.Json;
using System.Text.Json.Serialization;
using static System.Net.Mime.MediaTypeNames;

abstract class Task
{
    protected string text;
    public string Text
    {
        get => text;
        protected set => text = value;
    }
    public Task(string text)
    {
        this.text = text;
    }
}
class Task1 : Task
{
    [JsonConstructor]
    public Task1(string text) : base(text) { }
    public override string ToString()
    {
        int[] letterCount = new int[33];

        int distinctCount = 0;

        foreach (char letter in text)
        {
            char lowercaseLetter = char.ToLower(letter);

            if (lowercaseLetter >= 'а' && lowercaseLetter <= 'я')
            {
                int index = lowercaseLetter - 'а';

                if (letterCount[index] == 0)
                {
                    distinctCount++;
                }
                letterCount[index]++;
            }
        }

        return distinctCount.ToString();
    }
}




class Task2 : Task
{
    [JsonConstructor]

    public Task2(string text) : base(text)
    {
    }

    public override string ToString()
    {
        char[] shiftedText = new char[text.Length];

        for (int i = 0; i < text.Length; i++)
        {
            char currentChar = text[i];

            if (IsRussianLetter(currentChar))
            {
                char shiftedChar = ShiftLetter(currentChar);
                shiftedText[i] = shiftedChar;
            }
            else
            {
                shiftedText[i] = currentChar;
            }
        }

        return new string(shiftedText);
    }



    private bool IsRussianLetter(char letter)
    {
        return (letter >= 'а' && letter <= 'я') || (letter >= 'А' && letter <= 'Я');
    }

    private char ShiftLetter(char letter)
    {
        const int Alphabet = 33;
        int currentPosition = GetPosition(letter);
        int shiftedPosition = (currentPosition + 10) % Alphabet;

        return GetAlphabet(shiftedPosition, char.IsUpper(letter));
    }

    private int GetPosition(char letter)
    {
        if (char.IsUpper(letter))
        {
            return letter - 'А';
        }
        else
        {
            return letter - 'а';
        }
    }

    private char GetAlphabet(int position, bool isUpperCase)
    {
        char startLetter = isUpperCase ? 'А' : 'а';
        return (char)(startLetter + position);
    }
}

class JsonIO
{
    public static void Write<T>(T obj, string filePath)
    {
        using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate))
        {
            JsonSerializer.Serialize(fs, obj);
        }
    }
    public static T Read<T>(string filePath)
    {
        using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate))
        {
            return JsonSerializer.Deserialize<T>(fs);
        }
        return default(T);
    }
}


class Program
{
    static void Main(string[] args)
    {
        Task[] tasks = {
            new Task1("Организованность"),            new Task2("Паровоз ехал")
        }; Console.WriteLine(tasks[0]);
        Console.WriteLine(tasks[1]);

        string path = @"C:\Users\m2303476\Documents";
        string folderName = "Solution";
        path = Path.Combine(path, folderName);

        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        string file1name = "task_1.json";
        string file2name = "task_2.json";
        file1name = Path.Combine(path, file1name);
        file2name = Path.Combine(path, file2name);

        if (!File.Exists(file1name))
        {
            JsonIO.Write<Task1>(tasks[0] as Task1, file1name);
        }
        else
        {
            var t1 = JsonIO.Read<Task1>(file1name);
            Console.WriteLine(t1);
        }

        if (!File.Exists(file2name))
        {
            JsonIO.Write<Task2>(tasks[1] as Task2, file2name);
        }
        else
        {
            var t2 = JsonIO.Read<Task2>(file2name);
            Console.WriteLine(t2);
        }
    }
}


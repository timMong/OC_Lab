using System;
using System.IO;
using System.IO.Compression;
using System.Xml;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace first_program
{
    class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }
    class Program
    {
        static async Task Main(string[] args)
        {
            char choice_2;
            Console.WriteLine("Добро пожаловать в меню!\n");
            do 
            {
                Console.WriteLine("================================");
                Console.WriteLine("=  Информация о дисках -    1  =");
                Console.WriteLine("=  Работа с файлами -       2  =");
                Console.WriteLine("=  Работа с ZIP сжатием -   3  =");
                Console.WriteLine("=  Работа с форматом XML -  4  =");
                Console.WriteLine("=  Работа с форматом JSON - 5  =");
                Console.WriteLine("=  Выход из программы -     0  =");
                Console.WriteLine("================================");
                Console.Write("Введите номер команды: ");
                int choice = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("================================\n");
                switch (choice)
                {
                    case 1:
                    /*FIRST - DRIVERS*/
                    {
                      Console.WriteLine("FIRST - DRIVERS:");
                      DriveInfo[] drivers = DriveInfo.GetDrives();
                      foreach (DriveInfo drive in drivers)
                      {
                        Console.WriteLine($"Name: {drive.Name}");
                        Console.WriteLine($"Type: {drive.DriveType}");
                        if (drive.IsReady)
                        {
                            Console.WriteLine($"Disk space: {drive.TotalSize}");
                            Console.WriteLine($"Free space: {drive.TotalFreeSpace}");
                            Console.WriteLine($"Track: {drive.VolumeLabel}");
                        }
                        Console.WriteLine();
                      }
                      break;
                    }
                    case 2:
                    /*SECOND - FILE*/
                    {
                        Console.WriteLine("SECOND - FILE:");
                        string path = @"C:\Users\1255970\Desktop\C#\"; //D:\Documents\БББО-06-19\
                        Console.WriteLine("Введите строку для записи в файл:");
                        string text = Console.ReadLine();
                        // запись в файл
                        using (FileStream fstream = new FileStream($"{path}text.txt", FileMode.OpenOrCreate))
                        {
                            // преобразуем строку в байты
                            byte[] array = System.Text.Encoding.Default.GetBytes(text);
                            // запись массива байтов в файл
                            fstream.Write(array, 0, array.Length);
                            Console.WriteLine("Текст записан в файл");
                        }
                        // чтение из файла
                        using (FileStream fstream = File.OpenRead($"{path}text.txt"))
                        {
                            // преобразуем строку в байты
                            byte[] array = new byte[fstream.Length];
                            // считываем данные
                            fstream.Read(array, 0, array.Length);
                            // декодируем байты в строку
                            string textFromFile = System.Text.Encoding.Default.GetString(array);
                            Console.WriteLine($"Текст из файла: {textFromFile}");
                        }
                        Console.WriteLine("Удалить файл? y/n: ");    //D:\Documents\БББО-06-19\text.txt
                        switch (Console.ReadLine())
                        {
                            case "y":
                                if (File.Exists($"{path}text.txt"))
                                {
                                    File.Delete($"{path}text.txt");
                                    Console.WriteLine("Файл удален!");
                                }
                                else Console.WriteLine("Файла не существует!");
                                break;
                            case "n":
                                break;
                            default: 
                                Console.WriteLine("Вы ввели неправильное значение!");
                                break;
                            }
                        Console.WriteLine();
                        break;
                    }
                    case 3:
                    /*THIRD - ZIP*/
                    {
                        Console.WriteLine("THIRD - ZIP:");
                        string SourceFile = @"C:\Users\1255970\Desktop\C#\cat.txt"; // исходный файл
                        string CompressedFile = @"C:\Users\1255970\Desktop\C#\book.gz"; // сжатый файл
                        string TargetFile = @"C:\Users\1255970\Desktop\C#\cat_new.txt"; // восстановленный файл
                         // создание сжатого файла
                        Compress(SourceFile, CompressedFile);
                        // чтение из сжатого файла
                        Decompress(CompressedFile, TargetFile);
                        Console.WriteLine("Удалить файлы? y/n: ");
                        switch (Console.ReadLine())
                        {
                            case "y":
                                if ((File.Exists(SourceFile) && 
                                     File.Exists(CompressedFile) && File.Exists(TargetFile)) == true)
                                {
                                    File.Delete(SourceFile);
                                    File.Delete(CompressedFile);
                                    File.Delete(TargetFile);
                                    Console.WriteLine("Файлы удалены!");
                                }
                                else Console.WriteLine("Ошибка в удалении файлов!\n Проверьте их наличие!");
                                break;
                            case "n":
                                break;
                            default: 
                                Console.WriteLine("Введены неправильные данные!");
                                break;
                            }
                        Console.WriteLine();
                        break;
                    }
                    case 4:
                        /*FORTH - XML*/
                        {
                            Console.WriteLine("FORTH - XML:");
                            XmlDocument xDoc = new XmlDocument();
                            XDocument xdoc = new XDocument();
                            Console.Write("Сколько пользователей нужно ввести: ");
                            int count = Convert.ToInt32(Console.ReadLine());
                            XElement list = new XElement("users");
                            for (int i = 1; i <= count; i++)
                            {
                                XElement User = new XElement("user");
                                Console.Write("Введите имя пользователя: ");
                                XAttribute UserName = new XAttribute("name", Console.ReadLine());
                                Console.WriteLine();
                                Console.Write("Введите возраст пользователя: ");
                                XElement UserAge = new XElement("age", Convert.ToInt32(Console.ReadLine()));
                                Console.WriteLine();
                                Console.Write("Введите название компании: ");
                                XElement UserCompany = new XElement("company", Console.ReadLine());
                                Console.WriteLine();
                                User.Add(UserName);
                                User.Add(UserAge);
                                User.Add(UserCompany);
                                list.Add(User);
                            }
                            xdoc.Add(list);
                            xdoc.Save(@"C:\Users\1255970\Desktop\C#\users.xml");

                            Console.Write("Прочитать XML файл? y/n: ");
                            switch (Console.ReadLine())
                            {
                                case "y":
                                    Console.WriteLine();
                                    xDoc.Load(@"C:\Users\1255970\Desktop\C#\users.xml");
                                    XmlElement xRoot = xDoc.DocumentElement;
                                    foreach (XmlNode xnode in xRoot)
                                    {
                                        if (xnode.Attributes.Count > 0)
                                        {
                                            XmlNode attr = xnode.Attributes.GetNamedItem("name");
                                            if (attr != null)
                                                Console.WriteLine($"Имя: {attr.Value}");
                                        }
                                        foreach (XmlNode childnode in xnode.ChildNodes)
                                        {
                                            if (childnode.Name == "age")
                                                Console.WriteLine($"Возраст: {childnode.InnerText}");
                                            if (childnode.Name == "company")
                                                Console.WriteLine($"Компания: {childnode.InnerText}");
                                        }
                                    }
                                    Console.WriteLine();
                                    break;
                                case "n":
                                    break;
                                default:
                                    Console.WriteLine("Введены неправильные данные!");
                                    break;
                            }
                            Console.Write("Удалить созданный xml файл? y/n: ");
                            switch (Console.ReadLine())
                            {
                                case "y":
                                    FileInfo xmlfilecheck = new FileInfo(@"C:\Users\1255970\Desktop\C#\users.xml");
                                    if (xmlfilecheck.Exists)
                                    {
                                        xmlfilecheck.Delete();
                                        Console.WriteLine("Файл удален!");
                                    }
                                    else Console.WriteLine("Файла не существует!");
                                    break;
                                case "n":
                                    break;
                                default:
                                    Console.WriteLine("Вы ввели неправильное зачение!");
                                    break;
                            }
                            Console.WriteLine();
                            break;
                        }
                    case 5:
                    /*FIFTH - JSON*/
                    {
                        Console.WriteLine("FIFTH - JSON:");
                        // сохранение данных
                        using (FileStream fs = new FileStream(@"C:\Users\1255970\Desktop\C#\user.json", FileMode.OpenOrCreate))
                        {
                            Person tom = new Person() { Name = "Tom", Age = 35 };
                            await JsonSerializer.SerializeAsync<Person>(fs, tom);
                            Console.WriteLine("Данные были введены автоматически и они сохранены!");
                        }

                        // чтение данных
                        using (FileStream fs = new FileStream(@"C:\Users\1255970\Desktop\C#\user.json", FileMode.OpenOrCreate))
                        {
                            Person restoredPerson = await JsonSerializer.DeserializeAsync<Person>(fs);
                            Console.WriteLine($"Name: {restoredPerson.Name}  Age: {restoredPerson.Age}");
                        }
                        Console.Write("Удалить файл? y/n: ");
                        switch (Console.ReadLine())
                        {
                            case "y":
                                File.Delete(@"C:\Users\1255970\Desktop\C#\user.json");
                                Console.WriteLine("\nФайл удален!");
                                break;
                            case "n":
                                break;
                        }
                        break;
                    }
                    default: 
                        Console.WriteLine("\nВВЕДЕНЫ НЕПРАВИЛЬНЫЕ ДАННЫЕ!");
                        break;
                }
                Console.WriteLine("================================");
                Console.Write("\nПродолжить? y/n: ");
                choice_2 = Convert.ToChar(Console.ReadLine());
                Console.Write('\n');
            } while (choice_2 != 'n');
        }
    public static void Compress(string sourceFile, string compressedFile)
    {
      // поток для чтения исходного файла
      using (FileStream sourceStream = new FileStream(sourceFile, FileMode.OpenOrCreate))
      {
        // поток для записи сжатого файла
        using (FileStream targetStream = File.Create(compressedFile))
        {
          // поток архивации
          using (GZipStream compressionStream = new GZipStream(targetStream, CompressionMode.Compress))
          {
            sourceStream.CopyTo(compressionStream); // копируем байты из одного потока в другой
            Console.WriteLine("Сжатие файла {0} завершено. Исходный размер: {1}  сжатый размер: {2}.",
                sourceFile, sourceStream.Length.ToString(), targetStream.Length.ToString());
          }
        }
      }
    }
    public static void Decompress(string compressedFile, string targetFile)
    {
      // поток для чтения из сжатого файла
      using (FileStream sourceStream = new FileStream(compressedFile, FileMode.OpenOrCreate))
      {
        // поток для записи восстановленного файла
        using (FileStream targetStream = File.Create(targetFile))
        {
          // поток разархивации
          using (GZipStream decompressionStream = new GZipStream(sourceStream, CompressionMode.Decompress))
          {
            decompressionStream.CopyTo(targetStream);
            Console.WriteLine("Восстановлен файл: {0}", targetFile);
          }
        }
      }
    }
  }
}

using System;
using System.Runtime.ConstrainedExecution;
using ClassPerson;
using ClassPersonList;

namespace Persons
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // a
                Console.WriteLine("Нажмите любую клавишу, чтобы создать два списка персон.");
                Console.ReadKey();
                Console.WriteLine();
                PersonList list_1 = new PersonList();
                PersonList list_2 = new PersonList();

                list_1.AddList(new Person("Иван", "Иванов", "30", Gender.Male));
                list_1.AddList(new Person("Мария", "Петрова", "25", Gender.Female));
                list_1.AddList(new Person("Сергей", "Сидоров", "40", Gender.Male));

                list_2.AddList(new Person("Andrey", "Mod", "54", Gender.Male));
                list_2.AddList(new Person("Aleksandra", "Medmon", "26", Gender.Female));
                list_2.AddList(new Person("Molli", "Kaum", "17", Gender.Female));
                // b
                Console.WriteLine("Нажмите любую клавишу, чтобы отобразить содержимое списков.");
                Console.ReadKey();
                Console.WriteLine();
                Console.WriteLine("\n\nСписок 1:");
                list_1.PrintList();
                Console.WriteLine("Список 2:");
                list_2.PrintList();
                Console.WriteLine("\n\n");
                // c
                Console.WriteLine("Нажмите любую клавишу, чтобы добавить нового человека в первый список.");
                Console.ReadKey();
                Console.WriteLine();
                list_1.AddList(new Person("Певел", "Арефьев", "23", Gender.Male));
                Console.WriteLine("\n\nСписок 1:");
                list_1.PrintList();
                Console.WriteLine("\n\n");
                // d
                Console.WriteLine("Нажмите любую клавишу, чтобы скопировать человека из первого списка в конец второго списка.");
                Console.ReadKey();
                Console.WriteLine();
                list_2.AddList(list_1.GetIndex(1));
                Console.WriteLine("\n\nСписок 1:");
                list_1.PrintList();
                Console.WriteLine("Список 2:");
                list_2.PrintList();
                Console.WriteLine("\n\n");
                // e
                Console.WriteLine("Нажмите любую клавишу, чтобы удалить человека из первого списка.");
                Console.ReadKey();
                Console.WriteLine();
                list_1.RemoveList(list_1.GetIndex(1));
                Console.WriteLine("\n\nСписок 1:");
                list_1.PrintList();
                Console.WriteLine("Список 2:");
                list_2.PrintList();
                Console.WriteLine("\n\n");
                // f
                Console.WriteLine("Нажмите любую клавишу, чтобы очистить второй список.");
                Console.ReadKey();
                Console.WriteLine();
                list_2.ClearList();
                Console.WriteLine("\n\nСписок 2:");
                list_2.PrintList();
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }

        }
    }
}

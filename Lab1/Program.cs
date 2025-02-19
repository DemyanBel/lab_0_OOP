using System;
using System.Runtime.ConstrainedExecution;
using ClassPerson;
using Microsoft.VisualBasic.FileIO;

namespace Persons
{
    /// <summary>
    /// Основной класс программы.
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// Главный метод программы.
        /// </summary>
        static void Main(string[] args)
        {
            try
            {
                // Вызов метода создания двух списков
                CreatePersonLists();
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }

            // Вызов метода ввода данных с клавиатуры
            Person person1 = ReadFromKeyboard();
            Console.WriteLine(person1.GetInfo());

            // Вызов метода создания случайной персоны
            RandomCreatePerson();
            Console.WriteLine("\nПрограмма завершена.");

        }

        /// <summary>
        /// Метод считывает данные о человеке с клавиатуры.
        /// </summary>
        public static Person ReadFromKeyboard()

        {
            string firstName;
            string lastName;
            int age;
            Gender gender;
            string genderString;
            string strAge;
            string firstNameCorrect;
            string lastNameCorrect;

            Console.WriteLine("Введите имя человека.");
            while (true)
            {
                firstNameCorrect = Console.ReadLine();
                try
                {
                    Person.NameVerification(firstNameCorrect);
                    break;
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine("Попробуйте еще раз.");
                }
            }
            firstName = firstNameCorrect;

            Console.WriteLine("Введите фамилию человека.");
            while (true)
            {
                lastNameCorrect = Console.ReadLine();
                try
                {
                    Person.LastNameVerification(lastNameCorrect);
                    Person.CheckLanguage(firstName, lastNameCorrect);
                    break;
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine("Попробуйте еще раз.");
                }
            }
            lastName = lastNameCorrect;

            Console.WriteLine("Введите возраст человека.");
            while (true)
            {
                strAge = Console.ReadLine();
                try
                {
                    age = AgeVerification(strAge);
                    break;
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine("Попробуйте еще раз.");
                }
            }

            Console.WriteLine("Введите пол человека из списка: Male, Female, Other. Регистр не важен.");
            while (true)
            {
                genderString = Console.ReadLine();
                try
                {
                    GendorVerification(genderString);
                    break;
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine("Попробуйте еще раз.");
                }
            }
            gender = NumToGender(genderString);

            return new Person(firstName, lastName, age, gender);
        }

        /// <summary>
        /// Метод создает два списка PersonList.
        /// </summary>
        private static void CreatePersonLists()
        {
            // Пункт задания "a"
            Console.WriteLine("Нажмите любую клавишу, чтобы создать два списка персон.");
            Console.ReadKey();
            Console.WriteLine();
            PersonList list1 = new PersonList();
            PersonList list2 = new PersonList();

            list1.AddList(new Person("Иван", "Иванов", 30, Gender.Male));
            list1.AddList(new Person("Мария", "Петрова", 25, Gender.Female));
            list1.AddList(new Person("Сергей", "Сидоров", 40, Gender.Male));

            list2.AddList(new Person("Andrey", "Mod", 54, Gender.Male));
            list2.AddList(new Person("Aleksandra", "Medmon", 26, Gender.Female));
            list2.AddList(new Person("Molli", "Kaum", 17, Gender.Female));
            // Пункт задания "b"
            Console.WriteLine("Нажмите любую клавишу, чтобы отобразить содержимое списков.");
            Console.ReadKey();
            Console.WriteLine();
            Console.WriteLine("\n\nСписок 1:");
            list1.PrintList();
            Console.WriteLine("Список 2:");
            list2.PrintList();
            Console.WriteLine("\n\n");
            // Пуннкт задания "c"
            Console.WriteLine("Нажмите любую клавишу, чтобы добавить нового человека в первый список.");
            Console.ReadKey();
            Console.WriteLine();
            list1.AddList(new Person("Певел", "Арефьев", 23, Gender.Male));
            Console.WriteLine("\n\nСписок 1:");
            list1.PrintList();
            Console.WriteLine("\n\n");
            // Пункст задания "d"
            Console.WriteLine("Нажмите любую клавишу, чтобы скопировать второго человека из первого списка в конец второго списка.");
            Console.ReadKey();
            Console.WriteLine();
            list2.AddList(list1.GetIndex(1));
            Console.WriteLine("\n\nСписок 1:");
            list1.PrintList();
            Console.WriteLine("Список 2:");
            list2.PrintList();
            Console.WriteLine("\n\n");
            // Пункт задания "e"
            Console.WriteLine("Нажмите любую клавишу, чтобы удалить человека из первого списка.");
            Console.ReadKey();
            Console.WriteLine();
            list1.RemoveList(list1.GetIndex(1));
            Console.WriteLine("\n\nСписок 1:");
            list1.PrintList();
            Console.WriteLine("Список 2:");
            list2.PrintList();
            Console.WriteLine("\n\n");
            // Пункт задания "f"
            Console.WriteLine("Нажмите любую клавишу, чтобы очистить второй список.");
            Console.ReadKey();
            Console.WriteLine();
            list2.ClearList();
            Console.WriteLine("\n\nСписок 2:");
            list2.PrintList();
        }

        /// <summary>
        /// Метод создает случайного человека.
        /// </summary>
        private static void RandomCreatePerson()
        {
            Console.WriteLine("\nНажмите любую клавишу, чтобы создать случайную персону.");
            Console.ReadKey();
            Console.WriteLine();

            do
            {
                Person randomPerson = Person.GetRandomPerson();

                Console.WriteLine("\nСлучайно сгенерированный человек:");
                Console.WriteLine(randomPerson.GetInfo());
                Console.WriteLine("\nСгенерировать ещё раз? (y/n)");
            }
            while (Console.ReadKey().Key == ConsoleKey.Y);
        }

        /// <summary>
        /// Метод преобразования строки в тип перечисления.
        /// </summary>
        private static Gender NumToGender(string strGender)
        {
            switch (strGender.ToLower())
            {
                case "male":
                    return Gender.Male;
                case "female":
                    return Gender.Female;
                default:
                    return Gender.Other;
            }
        }

        /// <summary>
        /// Метод проверки введенного пола.
        /// </summary>
        private static void GendorVerification(string genderString)
        {
            string lowerGender = genderString.ToLower();
            if (lowerGender != "male" && lowerGender != "female" && lowerGender != "other")
            {
                throw new ArgumentException("Неверный ввод пола.  Попробуйте ещё раз. Введите пол человека из списка: Male, Female, Other. Регистр не важен.");
            }
        }

        /// <summary>
        /// Минимальный допустимый возраст.
        /// </summary>
        private const int MinAge = 0;

        /// <summary>
        /// Максимальный допустимый возраст.
        /// </summary>
        private const int MaxAge = 125;

        /// <summary>
        /// Метод проверки введенного возраста.
        /// </summary>
        private static int AgeVerification(string strAge)
        {
            if (!int.TryParse(strAge, out int age) || age < MinAge || age >= MaxAge)
            {
                throw new ArgumentException("Возраст не должен содержать посторонних символов, а должен быть целым числом больше 0.", nameof(strAge));
            }
            return age;
        }
    }
}

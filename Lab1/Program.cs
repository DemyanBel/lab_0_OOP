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
            Person person = ReadFromKeyboard();
            Console.WriteLine(person.GetInfo());

            // Вызов метода создания случайной персоны
            RandomCreatePerson();
            Console.WriteLine("\nПрограмма завершена.");

        }

        /// <summary>
        /// Метод считывает данные о человеке с клавиатуры.
        /// </summary>
        /// //TODO: XML
        public static Person ReadFromKeyboard()
        {
            //TODO: duplication +

            Person personReader = new Person();

            var actionList = new List<PropertyHandler>
            {
                new PropertyHandler(
                    "имя",
                    new List<Type>
                    {
                        typeof(ArgumentException),
                        typeof(FormatException),
                    },
                    () => { personReader.FirstName = Console.ReadLine(); }
                    ),
                new PropertyHandler(
                    "фамилию",
                    new List<Type>
                    {
                        typeof(ArgumentException),
                        typeof(FormatException),
                    },
                    () => { personReader.LastName = Console.ReadLine(); }
                    ),
                new PropertyHandler(
                    "возраст",
                    new List<Type>
                    {
                        typeof(IndexOutOfRangeException),
                        typeof(FormatException),
                    },
                    () =>
                    {
                        string strAge = Console.ReadLine();
                        AgeVerification(strAge);
                        personReader.Age = Convert.ToInt32(strAge);
                    }),
                new PropertyHandler(
                    "пол",
                    new List<Type>
                    {
                        typeof(ArgumentException),
                    },
                    () => 
                    {

                        string GenderString = Console.ReadLine();
                        GendorVerification(GenderString);
                        personReader.Gender = NumToGender(GenderString); 
                    }),
            };

            for (int i = 0; i < actionList.Count; i++)
            {
                PersonPropertiesHandler(actionList[i]);
            }
            return personReader;
        }

        /// <summary>
        /// Метод создает два списка PersonList.
        /// </summary>
        private static void CreatePersonLists()
        {
            // Пункт задания "a"
            Console.WriteLine("Нажмите любую клавишу, " +
                "чтобы создать два списка персон.");
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
            Console.WriteLine("Нажмите любую клавишу, чтобы отобразить " +
                "содержимое списков.");
            Console.ReadKey();
            Console.WriteLine();
            Console.WriteLine("\n\nСписок 1:");
            PrintList(list1);
            Console.WriteLine("Список 2:");
            PrintList(list2);
            Console.WriteLine("\n\n");

            // Пуннкт задания "c"
            Console.WriteLine("Нажмите любую клавишу, чтобы добавить нового " +
                "человека в первый список.");
            Console.ReadKey();
            Console.WriteLine();
            list1.AddList(new Person("Певел", "Арефьев", 23, Gender.Male));
            Console.WriteLine("\n\nСписок 1:");
            PrintList(list1);
            Console.WriteLine("\n\n");

            // Пункст задания "d"
            Console.WriteLine("Нажмите любую клавишу, чтобы скопировать " +
                "второго человека из первого списка в конец второго списка.");
            Console.ReadKey();
            Console.WriteLine();
            list2.AddList(list1.GetIndex(1));
            Console.WriteLine("\n\nСписок 1:");
            PrintList(list1);
            Console.WriteLine("Список 2:");
            PrintList(list2);
            Console.WriteLine("\n\n");

            // Пункт задания "e"
            Console.WriteLine("Нажмите любую клавишу, чтобы " +
                "удалить человека из первого списка.");
            Console.ReadKey();
            Console.WriteLine();
            list1.RemoveList(list1.GetIndex(1));
            Console.WriteLine("\n\nСписок 1:");
            PrintList(list1);
            Console.WriteLine("Список 2:");
            PrintList(list2);
            Console.WriteLine("\n\n");

            // Пункт задания "f"
            Console.WriteLine("Нажмите любую клавишу, чтобы очистить " +
                "второй список.");
            Console.ReadKey();
            Console.WriteLine();
            list2.ClearList();
            Console.WriteLine("\n\nСписок 2:");
            PrintList(list2);
        }

        /// <summary>
        /// Метод создает случайного человека.
        /// </summary>
        private static void RandomCreatePerson()
        {
            Console.WriteLine("\nНажмите любую клавишу, " +
                "чтобы создать случайную персону.");
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
            if (lowerGender != "male" 
                && lowerGender != "female" 
                && lowerGender != "other")
            {
                throw new ArgumentException("Неверный ввод пола. " +
                    "Попробуйте ещё раз. Введите пол человека из " +
                    "списка: Male, Female, Other. Регистр не важен.");
            }
        }

        /// <summary>
        /// Метод проверки введенного возраста.
        /// </summary>
        private static int AgeVerification(string strAge)
        {
            if (!int.TryParse(strAge, out int age))
            {
                throw new FormatException("Возраст не должен " +
                    "содержать посторонних символов.");
            }
            return age;
        }

        /// <summary>
        /// Метод получения элементов в списке экземпляров Person.
        /// </summary>
        public static void PrintList(PersonList personList)
        {
            if (personList.People.Count == 0)
            {
                Console.WriteLine("Список пуст.");
                return;
            }
            foreach (Person person in personList.People)
            {
                Console.WriteLine(person.GetInfo());
            }
        }

        /// <summary>
        /// Метод распаковки actionList
        /// </summary>
        /// <param name="propertyHandeler">actionList</param>
        private static void PersonPropertiesHandler(
            PropertyHandler propertyHandeler)
        {
            var personField = propertyHandeler.PropertyName;
            var personTypes = propertyHandeler.ExceptionTypes;
            var personAction = propertyHandeler.PropertyHandlingAction;
            Console.WriteLine($"Введите {personField} человека:");
            while (true)
            {
                try
                {
                    personAction.Invoke();
                    break;
                }
                catch (Exception ex)
                {
                    if (personTypes.Contains(ex.GetType()))
                    {
                        Console.WriteLine(ex.Message);
                        Console.WriteLine($"Введите {personField} заново");
                        continue;
                    }
                    throw ex;
                }
            }
        }
    }
}

using System.Globalization;
using System;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace ClassPerson
{
    /// <summary>
    /// Класс Person
    /// </summary>
    public class Person
    {
        /// <summary>
        /// Имя персоны.
        /// </summary>
        private string _firstName;

        /// <summary>
        /// Фамилия персоны.
        /// </summary>
        private string _lastName;

        /// <summary>
        /// Возраст персоны.
        /// </summary>
        private int _age;

        /// <summary>
        /// Пол персоны.
        /// </summary>
        private Gender _gender;

        /// <summary>
        /// Свойство для получения и установки имени.
        /// </summary>
        public string FirstName
        {
            get => _firstName;
            set
            {
                NameVerification(value);
                // Преобразования имени к нужному регистру
                _firstName = FormatName(value);
            }
        }

        /// <summary>
        /// Свойство для получения и установки фамилии.
        /// </summary>
        public string LastName
        {
            get => _lastName;
            set
            {
                LastNameVerification(value);
                CheckLanguage(FirstName, value);
                // Преобразования фамилии к нужному регистру
                _lastName = FormatName(value);
            }
        }

        /// <summary>
        /// Свойство для получения и установки возраста.
        /// </summary>
        public int Age
        {
            get => _age;
            set
            {
                AgeVerification(value);
                _age = value;
            }
        }

        /// <summary>
        /// Свойство для получения и установки пола.
        /// </summary>
        public Gender Gender
        {
            get => _gender;
            set => _gender = value;
        }

        /// <summary>
        /// Конструктор класса Person.
        /// </summary>
        /// <param name="firstName">Имя человека.</param>
        /// <param name="lastName">Фамилия человека.</param>
        /// <param name="age">Возраст человека.</param>
        /// <param name="gender">Пол человека.</param>
        public Person(string firstName, string lastName, int age, Gender gender)
        {
            FirstName = firstName;
            LastName = lastName;
            Age = age;
            Gender = gender;
        }

        /// <summary>
        /// Возвращает строковое представление информации о человеке.
        /// </summary>
        /// <returns>Строка, содержащая имя, фамилию, возраст и пол человека.</returns>
        public string GetInfo()
        {
            return $"Имя: {FirstName}, Фамилия: {LastName}, Возраст: {Age}, Пол: {Gender}";
        }

        /// <summary>
        /// Регулярное выражение для проверки имени и фамилии.
        /// </summary>
        private static readonly Regex NameRegex = new Regex(@"^[a-zA-Zа-яА-Я]+(-[a-zA-Zа-яА-Я]+)?$", RegexOptions.Compiled);

        /// <summary>
        /// Метод проверки имени на соответствие заданным критериям.
        /// </summary>
        /// <param name="firstName">Имя для проверки.</param>
        public static void NameVerification(string firstName)
        {

            if (string.IsNullOrWhiteSpace(firstName))
            {
                throw new ArgumentException("Имя не может быть пустым, состоять из пробелов или null.", nameof(firstName));
            }
            if (!NameRegex.IsMatch(firstName))
            {
                throw new ArgumentException("Имя должно содержать только буквы русского или английского алфавита. Двойное имя содержит один символ тире.", nameof(firstName));
            }
        }

        /// <summary>
        /// Метод проверки фамилии на соответствие заданным критериям.
        /// </summary>
        /// <param name="lastName">Фамилия для проверки.</param>
        public static void LastNameVerification(string lastName)
        {
            if (string.IsNullOrWhiteSpace(lastName))
            {
                throw new ArgumentException("Фамилия не может быть пустой, состоять из пробелов или null.", nameof(lastName));
            }
            if (!NameRegex.IsMatch(lastName))
            {
                throw new ArgumentException("Фамилия должна содержать только буквы русского или английского алфавита. Двойная фамилия содержит один символ тире.", nameof(lastName));
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
        /// Метод проверки возраста на соответствие допустимому диапазону.
        /// </summary>
        /// <param name="age">Возраст для проверки.</param>
        private void AgeVerification(int age)
        {
            if (age < MinAge || age >= MaxAge)
            {
                throw new ArgumentException("Возраст должен быть положительным числом больше нуля.", nameof(age));
            }

        }

        /// <summary>
        /// Преобразует строку таким образом, что первая буква всегда большая.
        /// </summary>
        /// <param name="word">Строка для преобразования.</param>
        private static string FormatName(string word)
        {
            return CultureInfo.CurrentCulture.TextInfo.
                ToTitleCase(word.ToLower());
        }

        /// <summary>
        /// Проверяет, что имя и фамилия написаны на одном языке (кириллица или латиница).
        /// </summary>
        /// <param name="firstName">Имя.</param>
        /// <param name="lastName">Фамилия.</param>
        public static void CheckLanguage(string firstName, string lastName)
        {
            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName))
                return;
            // Определяем языковую категорию первого символа имени и фамилии
            UnicodeCategory firstNameCategory = Char.GetUnicodeCategory(firstName[0]);
            UnicodeCategory lastNameCategory = Char.GetUnicodeCategory(lastName[0]);

            // Проверяем, является ли первый символ кириллицей или латиницей для имени и фамилии
            bool firstNameIsCyrillic = (firstNameCategory == UnicodeCategory.LowercaseLetter || firstNameCategory == UnicodeCategory.UppercaseLetter) &&
                              (firstName[0] >= 'а' && firstName[0] <= 'я' || firstName[0] >= 'А' && firstName[0] <= 'Я' || firstName[0] == 'ё' || firstName[0] == 'Ё');
            bool lastNameIsCyrillic = (lastNameCategory == UnicodeCategory.LowercaseLetter || lastNameCategory == UnicodeCategory.UppercaseLetter) &&
                               (lastName[0] >= 'а' && lastName[0] <= 'я' || lastName[0] >= 'А' && lastName[0] <= 'Я' || lastName[0] == 'ё' || lastName[0] == 'Ё');

            // Если категории отличаются, значит, имя и фамилия на разных языках
            if (firstNameIsCyrillic != lastNameIsCyrillic)
            {
                throw new ArgumentException("Имя и фамилия должны быть на одном языке.");
            }
        }

        /// <summary>
        /// Метод создания случайной персоны.
        /// </summary>
        public static Person GetRandomPerson()
        {
            Random random = new Random();

            // Настройки вероятностей сделать имя и фамилию двойными (в процентах)
            const int probabilityNameChance = 30;
            const int probabilityLastNameChance = 30;

            const int MinAge = 1;
            const int MaxAge = 99;

            string[] maleNamesCyrillic = { "Алексей", "Иван", "Дмитрий", "Сергей" };
            string[] femaleNamesCyrillic = { "Анна", "Екатерина", "Ольга", "Мария" };
            string[] maleLastNamesCyrillic = { "Иванов", "Петров", "Смирнов", "Сидоров" };
            string[] femaleLastNamesCyrillic = { "Иванова", "Петрова", "Смирнова", "Сидорова" };

            string[] maleNamesLatin = { "John", "Michael", "David", "William" };
            string[] femaleNamesLatin = { "Emily", "Jessica", "Sophia", "Olivia" };
            string[] lastNamesLatin = { "Smith", "Johnson", "Brown", "Taylor" };

            // Определяем случайный пол (0 - Male, 1 - Female, 2 - Other)
            Gender gender = (Gender)random.Next(3);

            // Выбираем язык (0 - кириллица, 1 - латиница)
            bool useCyrillic = random.Next(2) == 0;

            string firstName, lastName;

            if (gender == Gender.Male)
            {
                firstName = useCyrillic
                    ? maleNamesCyrillic[random.Next(maleNamesCyrillic.Length)]
                    : maleNamesLatin[random.Next(maleNamesLatin.Length)];
            }
            else if (gender == Gender.Female)
            {
                firstName = useCyrillic
                    ? femaleNamesCyrillic[random.Next(femaleNamesCyrillic.Length)]
                    : femaleNamesLatin[random.Next(femaleNamesLatin.Length)];
            }
            else
            {
                firstName = useCyrillic
                    ? (random.Next(2) == 0 ? maleNamesCyrillic[random.Next(maleNamesCyrillic.Length)] : femaleNamesCyrillic[random.Next(femaleNamesCyrillic.Length)])
                    : (random.Next(2) == 0 ? maleNamesLatin[random.Next(maleNamesLatin.Length)] : femaleNamesLatin[random.Next(femaleNamesLatin.Length)]);
            }

            if (gender == Gender.Male)
            {
                lastName = useCyrillic
                    ? maleLastNamesCyrillic[random.Next(maleLastNamesCyrillic.Length)]
                    : lastNamesLatin[random.Next(lastNamesLatin.Length)];
            }
            else if (gender == Gender.Female)
            {
                lastName = useCyrillic
                    ? femaleLastNamesCyrillic[random.Next(femaleLastNamesCyrillic.Length)]
                    : lastNamesLatin[random.Next(lastNamesLatin.Length)];
            }
            else
            {
                lastName = useCyrillic
                    ? (gender == Gender.Male ? maleLastNamesCyrillic[random.Next(maleLastNamesCyrillic.Length)] : femaleLastNamesCyrillic[random.Next(femaleLastNamesCyrillic.Length)])
                    : (lastNamesLatin[random.Next(lastNamesLatin.Length)]);
            }

            string usedName = firstName;
            string usedLastName = lastName;
            if (random.Next(100) < probabilityNameChance)
            {
                string secondName;
                do
                {
                    secondName = useCyrillic
                        ? (gender == Gender.Male ? maleNamesCyrillic[random.Next(maleNamesCyrillic.Length)] : femaleNamesCyrillic[random.Next(femaleNamesCyrillic.Length)])
                        : (gender == Gender.Male ? maleNamesLatin[random.Next(maleNamesLatin.Length)] : femaleNamesLatin[random.Next(femaleNamesLatin.Length)]);
                } while (secondName == usedName);
                firstName += "-" + secondName;
            }

            if (random.Next(100) < probabilityLastNameChance)
            {
                string secondLastName;
                do
                {
                    secondLastName = useCyrillic
                        ? (gender == Gender.Male ? maleLastNamesCyrillic[random.Next(maleLastNamesCyrillic.Length)] : femaleLastNamesCyrillic[random.Next(femaleLastNamesCyrillic.Length)])
                        : lastNamesLatin[random.Next(lastNamesLatin.Length)];
                } while (secondLastName == usedLastName);
                lastName += "-" + secondLastName;
            }

            int age = random.Next(MinAge, MaxAge + 1);

            return new Person(firstName, lastName, age, gender);
        }
    }
}

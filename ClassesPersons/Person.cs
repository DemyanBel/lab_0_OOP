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
        /// Минимальный допустимый возраст.
        /// </summary>
        private const int MinAge = 0;

        /// <summary>
        /// Максимальный допустимый возраст.
        /// </summary>
        private const int MaxAge = 125;

        /// <summary>
        /// Свойство для получения и установки имени.
        /// </summary>
        public string FirstName
        {
            get => _firstName;
            set
            {

                NameVerification(value, "Имя");
                // Преобразования имени к нужному регистру
                _firstName = FormatName(value);
                LanguageVerification();
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
                //TODO: refactor +
                NameVerification(value, "Фамилия");
                // Преобразования фамилии к нужному регистру
                _lastName = FormatName(value);
                LanguageVerification();
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
                if (value >= MinAge && value <= MaxAge)
                {
                    _age = value;
                }
                else
                {
                    throw new IndexOutOfRangeException("Возраст должен быть" +
                          $" в диапазоне [{MinAge}:{MaxAge}].");
                }
            }
        }

        //TODO: autoproperty +
        /// <summary>
        /// Свойство для получения и установки пола.
        /// </summary>
        public Gender Gender { get; set; }

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
        /// Конструктор по умолчанию.
        /// </summary>
        public Person()
        { }

        /// <summary>
        /// Возвращает строковое представление информации о человеке.
        /// </summary>
        /// <returns>Строка, содержащая имя, фамилию, возраст и пол человека.</returns>
        public string GetInfo()
        {
            return $"Имя: {FirstName}, Фамилия: {LastName}, " +
                $"Возраст: {Age}, Пол: {Gender}";
        }

        //TODO: RSDN +
        /// <summary>
        /// Регулярное выражение для проверки имени и фамилии.
        /// </summary>
        private static readonly Regex _nameRegex =
            new Regex(@"^[a-zA-Zа-яА-Я]+(-[a-zA-Zа-яА-Я]+)?$",
                RegexOptions.Compiled);

        //TODO: encapsulation +
        /// <summary>
        /// Метод проверки имени и фамилии на соответствие заданным критериям.
        /// </summary>
        private void NameVerification(string GetName, string GetValueName)
        {
            //TODO: RSDN +
            //TODO: duplication +
            if (string.IsNullOrWhiteSpace(GetName))
            {
                throw new ArgumentException($"Возможно вы " +
                    $"не ввели {GetValueName}," +
                    $" так же не должно быть пробелов " +
                    $"или null.", nameof(GetName));
            }
            if (!_nameRegex.IsMatch(GetName))
            {
                throw new ArgumentException($"{GetValueName} должно содержать" +
                    $" только буквы русского или английского алфавита. " +
                    $"Двойные имена/фамилии содержитат" +
                    $" один символ тире.", nameof(GetName));
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
        /// Регулярное выражение для определения латиницы
        /// </summary>
        private static readonly Regex _latinSymbols = new Regex(@"[A-z]+");

        /// <summary>
        /// Регулярное выражение для определения кириллицы
        /// </summary>
        private static readonly Regex _cyrillicSymbols = new Regex(@"[А-я]+");

        /// <summary>
        /// Метод для определения языка на основе имени
        /// </summary>
        /// <param name="name">Имя для анализа.</param>
        /// <returns>Код языка ("ru-RU", "en-EN", 
        /// "mix" или "неизвестный язык").</returns>
        public static Language CheckLanguage(string name)
        {

            if (string.IsNullOrEmpty(name) == false)
            {
                if (_latinSymbols.IsMatch(name))
                {
                    return Language.En;
                }
                else if (_cyrillicSymbols.IsMatch(name))
                {
                    return Language.Ru;
                }
                else
                {
                    throw new ArgumentException($"Некорректный ввод." +
                        $" Пожалуйста, попробуйте снова!");
                }
            }

            return Language.Unknown;
        }

        /// <summary>
        /// Метод проверки на язык
        /// </summary>
        /// <exception cref="FormatException"></exception>
        private void LanguageVerification()
        {
            // Проверяем только если и имя, и фамилия уже установлены
            if (!string.IsNullOrEmpty(_firstName) 
                && !string.IsNullOrEmpty(_lastName))
            {
                Language firstNameLanguage = CheckLanguage(_firstName);
                Language lastNameLanguage = CheckLanguage(_lastName);

                if (firstNameLanguage != Language.Unknown 
                    && lastNameLanguage != Language.Unknown 
                    && firstNameLanguage != lastNameLanguage)
                {
                    throw new FormatException("Имя и фамилия " +
                        "должны быть на одном языке.");
                }
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

            //TODO: remove +

            //TODO: RSDN +
            string[] maleNamesCyrillic = {
                "Алексей",
                "Иван",
                "Дмитрий",
                "Сергей" };
            string[] femaleNamesCyrillic = {
                "Анна",
                "Екатерина",
                "Ольга",
                "Мария" };
            string[] maleLastNamesCyrillic = {
                "Иванов",
                "Петров",
                "Смирнов",
                "Сидоров" };
            string[] femaleLastNamesCyrillic = {
                "Иванова",
                "Петрова",
                "Смирнова",
                "Сидорова" };

            string[] maleNamesLatin = {
                "John",
                "Michael",
                "David",
                "William" };
            string[] femaleNamesLatin = {
                "Emily",
                "Jessica",
                "Sophia",
                "Olivia" };
            string[] lastNamesLatin = {
                "Smith",
                "Johnson",
                "Brown",
                "Taylor" };

            // Определяем случайный пол (0 - Male, 1 - Female, 2 - Other)
            Gender randomGender = (Gender)random.Next(3);

            // Выбираем язык (0 - кириллица, 1 - латиница)
            bool isUsingCyrillic = random.Next(2) == 0;

            //TODO: RSDN +

            string firstName = GetValueFromArrays(random,
                maleNamesCyrillic, femaleNamesCyrillic,
                maleNamesLatin, femaleNamesLatin,
                randomGender, isUsingCyrillic);

            string lastName = GetValueFromArrays(random,
                maleLastNamesCyrillic, femaleLastNamesCyrillic,
                lastNamesLatin, lastNamesLatin,
                randomGender, isUsingCyrillic);

            string usedName = firstName;
            string usedLastName = lastName;

            //TODO: duplication +
            firstName = GetValueFromDoubleArrays(random,
                probabilityNameChance,
                usedName, true, maleNamesCyrillic,
                femaleNamesCyrillic, maleNamesLatin,
                femaleNamesLatin, randomGender,
                isUsingCyrillic);

            //TODO: duplication +
            lastName = GetValueFromDoubleArrays(random,
                probabilityLastNameChance,
                usedLastName, false, maleLastNamesCyrillic,
                femaleLastNamesCyrillic, lastNamesLatin,
                lastNamesLatin, randomGender,
                isUsingCyrillic);

            int age = random.Next(MinAge, MaxAge + 1);

            return new Person(firstName, lastName, age, randomGender);
        }

        //TODO: XML +
        /// <summary>
        /// Получает случайное имя/фамилию из предоставленных массивов, учитывая пол и язык.
        /// </summary>
        /// <param name="random">Генератор случайных чисел.</param>
        /// <param name="maleNamesCyrillic">Массив мужских имен/фамилий на кириллице.</param>
        /// <param name="femaleNamesCyrillic">Массив женских имен/фамилий на кириллице.</param>
        /// <param name="maleNamesLatin">Массив мужских имен/фамилий на латинице.</param>
        /// <param name="femaleNamesLatin">Массив женских имен/фамилий на латинице.</param>
        /// <param name="gender">Пол, для которого требуется получить имя/фамилию.</param>
        /// <param name="isUsingCyrillic">Указывает, следует ли использовать кириллические имена/фамилии.</param>
        /// <returns>Случайно выбранное имя/фамилия из соответствующего массива.</returns>
        private static string GetValueFromArrays(Random random,
            string[] maleNamesCyrillic, string[] femaleNamesCyrillic,
            string[] maleNamesLatin, string[] femaleNamesLatin,
            Gender gender, bool isUsingCyrrillic)
        {
            string firstNameOrLastName;
            switch (gender)
            {
                case Gender.Male:
                    firstNameOrLastName = isUsingCyrrillic
                        ? GetRandomValueFromList(random, maleNamesCyrillic)
                        : GetRandomValueFromList(random, maleNamesLatin);
                    break;
                case Gender.Female:
                    firstNameOrLastName = isUsingCyrrillic
                        ? GetRandomValueFromList(random, femaleNamesCyrillic)
                        : GetRandomValueFromList(random, femaleNamesLatin);
                    break;
                default:
                    var randomValue = random.Next(2) == 0;
                    firstNameOrLastName = isUsingCyrrillic
                        ? randomValue
                            ? GetRandomValueFromList(random, maleNamesCyrillic)
                            : GetRandomValueFromList(random, femaleNamesCyrillic)
                        : randomValue
                            ? GetRandomValueFromList(random, maleNamesLatin)
                            : GetRandomValueFromList(random, femaleNamesLatin);
                    break;
            }

            return firstNameOrLastName;
        }

        /// <summary>
        /// Получает случайное значение из предоставленного массива строк.
        /// </summary>
        /// <param name="random">Генератор случайных чисел.</param>
        /// <param name="NamesLanguage">Массив строк, из которого необходимо выбрать случайное значение.</param>
        /// <returns>Случайно выбранная строка из массива.</returns>
        private static string GetRandomValueFromList(Random random, string[] NamesLanguage)
        {
            return NamesLanguage[random.Next(NamesLanguage.Length)];
        }

        /// <summary>
        /// Генерирует составное имя или фамилию.
        /// </summary>
        /// <param name="random">Генератор случайных чисел.</param>
        /// <param name="probabilityNameOrLastNameChance">Вероятность (в процентах) 
        /// добавления второй части к имени или фамилии.</param>
        /// <param name="baseValue">Основное имя или фамилия,
        ///  к которой может быть добавлена вторая часть.</param>
        /// <param name="isFirstName">Указывает, генерируется ли имя (<c>true</c>) или фамилия (<c>false</c>).</param>
        /// <param name="maleNamesCyrillic">Массив мужских имен/фамилий на кириллице.</param>
        /// <param name="femaleNamesCyrillic">Массив женских имен/фамилий на кириллице.</param>
        /// <param name="maleNamesLatin">Массив мужских имен/фамилий на латинице.</param>
        /// <param name="femaleNamesLatin">Массив женских имен/фамилий на латинице.</param>
        /// <param name="gender">Пол для генерации имени/фамилии.</param>
        /// <param name="isUsingCyrillic">Указывает, следует ли использовать кириллические имена.</param>
        /// <returns>
        /// Составное имя или фамилия, если была добавлена вторая часть,
        /// или основное значение <paramref name="baseValue"/>, если вторая часть не была добавлена.
        /// </returns>
        private static string GetValueFromDoubleArrays(Random random, int probabilityNameOrLastNameChance, string baseValue, bool isFirstName, string[] maleNamesCyrillic, string[] femaleNamesCyrillic, string[] maleNamesLatin, string[] femaleNamesLatin, Gender gender, bool isUsingCyrrillic)
        {
            if (random.Next(100) < probabilityNameOrLastNameChance)
            {
                string secondNameOrLastName;
                do
                {
                    if (isFirstName)
                    {
                        switch (gender)
                        {
                            case Gender.Male:
                                secondNameOrLastName = isUsingCyrrillic
                                    ? GetRandomValueFromList(random, maleNamesCyrillic)
                                    : GetRandomValueFromList(random, maleNamesLatin);
                                break;
                            default:
                                secondNameOrLastName = isUsingCyrrillic
                                    ? GetRandomValueFromList(random, femaleNamesCyrillic)
                                    : GetRandomValueFromList(random, femaleNamesLatin);
                                break;
                        }
                    }
                    else
                    {
                        switch (gender)
                        {
                            case Gender.Male:
                                secondNameOrLastName = isUsingCyrrillic
                                    ? GetRandomValueFromList(random, maleNamesCyrillic)
                                    : GetRandomValueFromList(random, maleNamesLatin);
                                break;
                            default:
                                secondNameOrLastName = isUsingCyrrillic
                                    ? GetRandomValueFromList(random, femaleNamesCyrillic)
                                    : GetRandomValueFromList(random, maleNamesLatin);
                                break;
                        }
                    }
                }
                while (secondNameOrLastName == baseValue);

                    baseValue += "-" + secondNameOrLastName;

                return baseValue;
            }
            return baseValue;
        }
    }
}

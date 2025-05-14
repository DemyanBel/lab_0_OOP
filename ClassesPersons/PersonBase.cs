using System.Globalization;
using System.Text.RegularExpressions;

namespace ClassPerson
{
    /// <summary>
    /// Класс PersonBase
    /// </summary>
    public abstract class PersonBase
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
        /// Минимальный возраст.
        /// </summary>
        protected const int MinAge = 0;

        /// <summary>
        /// Максимально допустимый возраст.
        /// </summary>
        protected const int MaxAge = 125;

        /// <summary>
        /// Свойство для получения и установки имени.
        /// </summary>
        public string FirstName
        {
            get => _firstName;
            set
            {
                _firstName = CheckNameSurname(value, _lastName);
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
                _lastName = CheckNameSurname(value, _firstName);
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
                CheckAge(value);
                _age = value;
            }
        }

        /// <summary>
        /// Свойство для получения и установки пола.
        /// </summary>
        public Gender Gender { get; set; }

        /// <summary>
        /// Конструктор класса PersonBase.
        /// </summary>
        /// <param name="firstName">Имя человека.</param>
        /// <param name="lastName">Фамилия человека.</param>
        /// <param name="age">Возраст человека.</param>
        /// <param name="gender">Пол человека.</param>
        protected PersonBase
            (string firstName, string lastName, int age, Gender gender)
        {
            FirstName = firstName;
            LastName = lastName;
            Age = age;
            Gender = gender;
        }



        /// <summary>
        /// Конструктор по умолчанию.
        /// </summary>
        protected PersonBase()
        { }

        /// <summary>
        /// Преобразует значения полей класса в строковый формат.
        /// </summary>
        /// <returns>Информация о человеке.</returns>
        public string GetPersonInfo()
        {
            return $"{FirstName} {LastName}; Age - {Age}; Gender - {Gender}";
        }

        /// <summary>
        /// Преобразует имя и фамилию в строковый формат.
        /// </summary>
        /// <returns>Имя и фамилия человека.</returns>
        public string GetPersonNameSurname()
        {
            return $"{FirstName} {LastName}";
        }

        /// <summary>
        /// Возвращает строковое представление информации о человеке.
        /// </summary>
        /// <returns>Строка, содержащая имя, фамилию, возраст и пол человека.</returns>
        public abstract string GetInfo();

        /// <summary>
        /// Проверка строки на язык.
        /// </summary>
        /// <param name="name">Входная строка.</param>
        /// <returns>Язык строки.</returns>
        private static Language CheckStringLanguage(string name)
        {
            var latinSymbols = new Regex(@"^[A-z]+(-[A-z])?[A-z]*$");
            var cyrillicSymbols = new Regex(@"^[А-я]+(-[А-я])?[А-я]*$");

            if (string.IsNullOrEmpty(name) == false)
            {
                if (latinSymbols.IsMatch(name))
                {
                    return Language.En;
                }
                else if (cyrillicSymbols.IsMatch(name))
                {
                    return Language.Ru;
                }
                else
                {
                    throw new ArgumentException("Некорректный ввод. " +
                        "Пожалуйста, попробуйте снова!");
                }
            }

            return Language.Unknown;
        }

        /// <summary>
        /// Метод, проверяющий, что язык строки не является неизвестным.
        /// </summary>
        /// <param name="tmpStr">Входная строка.</param>
        /// <exception cref="ArgumentException">
        /// Выбрасывается, если строка содержит символы разных языков.</exception>
        private void CheckUnknownLanguage(string tmpStr)
        {
            if (CheckStringLanguage(tmpStr) == Language.Unknown)
            {
                throw new ArgumentException("Некорректный ввод. " +
                    "Пожалуйста, используйте символы " +
                    "только одного языка.");
            }
        }

        /// <summary>
        /// Сравнение языков имени и фамилии.
        /// </summary>
        /// <param name="word1">Имя.</param>
        /// <param name="word2">Фамилия.</param>
        /// <exception cref="FormatException">
        /// Выбрасывается, если имя и фамилия на разных языках.</exception>
        private void CheckSameLanguage(string word1, string word2)
        {
            if ((string.IsNullOrEmpty(word1) == false)
                && (string.IsNullOrEmpty(word2) == false))
            {
                var word1Language = CheckStringLanguage(word1);
                var word2Language = CheckStringLanguage(word2);

                if (word1Language != word2Language)
                {
                    throw new FormatException("Имя и фамилия " +
                        "должны быть на одном языке.");
                }
            }
        }

        /// <summary>
        /// Преобразование регистра: первая буква заглавная, остальные строчные.
        /// </summary>
        /// <param name="word">Имя или фамилия.</param>
        /// <returns>Отредактированная строка с правильным регистром.</returns>
        private static string EditRegister(string word)
        {
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(word.ToLower());
        }

        /// <summary>
        /// Метод для комплексной проверки имени и фамилии.
        /// </summary>
        /// <param name="word1">Имя или фамилия.</param>
        /// <param name="word2">Имя или фамилия.</param>
        /// <returns>Отредактированное и проверенное имя или фамилия.</returns>
        private string CheckNameSurname(string word1, string word2)
        {
            CheckUnknownLanguage(word1);
            var tmpString = EditRegister(word1);
            CheckSameLanguage(word1, word2);
            return tmpString;
        }

        /// <summary>
        /// Проверка возраста персоны.
        /// </summary>
        /// <param name="age">Возраст персоны.</param>
        protected void CheckAge(int age, int minAge = MinAge, int maxAge = MaxAge)
        {
            if (age < minAge || age > maxAge)
            {
                throw new IndexOutOfRangeException($"Возраст " +
                    $"должен быть в диапазоне [{minAge};{maxAge}].");
            }
        }
    }
}

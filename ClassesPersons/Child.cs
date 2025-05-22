using ClassPerson;

namespace ClassesPersons
{
    /// <summary>
    /// Класс, описывающий определённого ребёнка.
    /// </summary>
    public class Child : PersonBase
    {
        /// <summary>
        /// Отец ребёнка.
        /// </summary>
        private Adult _father;

        /// <summary>
        /// Мать ребёнка.
        /// </summary>
        private Adult _mother;

        /// <summary>
        /// Учебное заведение (школа или детский сад).
        /// </summary>
        private string _educationPlace;

        /// <summary>
        /// Минимально допустимый возраст.
        /// </summary>
        private const int MinAge = 0;

        /// <summary>
        /// Максимально допустимый возраст ребёнка.
        /// </summary>
        private const int MaxAge = 17;

        /// <summary>
        /// Свойство  - отец ребёнка.
        /// </summary>
        public Adult Father
        {
            get => _father;
            set
            {
                CheckParentGender(value, Gender.Female);
                _father = value;
            }
        }

        /// <summary>
        /// Свойство - Мать ребёнка.
        /// </summary>
        public Adult Mother
        {
            get => _mother;
            set
            {
                CheckParentGender(value, Gender.Male);
                _mother = value;
            }
        }

        /// <summary>
        /// Свойство - учебное заведение (школа или детский сад)
        /// </summary>
        public string EducationPlace
        {
            get => _educationPlace;
            set => _educationPlace = value
                ?? throw new ArgumentNullException(
                    nameof(EducationPlace), "Место учебы" +
                    " не можетбыть null");
        }


        /// <summary>
        /// Конструктор для создания экземпляра класса Child.
        /// </summary>
        /// <param name="firstName">Имя человека.</param>
        /// <param name="lastName">Фамилия человека.</param>
        /// <param name="age">Возраст человека.</param>
        /// <param name="gender">Пол человека.</param>
        /// <param name="father">Отец ребёнка.</param>
        /// <param name="mother">Мать ребёнка.</param>
        /// <param name="educationPlace">Школа/детский сад ребёнка.</param>
        public Child(string firstName, string lastName, int age,
            Gender gender, Adult father, Adult mother,
            string educationPlace) : base(firstName, lastName, age, gender)
        {
            Father = father;
            Mother = mother;
            EducationPlace = educationPlace;
        }

        /// <summary>
        /// Конструктор для создания экземпляра класса Child без параметров.
        /// </summary>
        public Child() : this("Unknown", "Unknown", 11,
            Gender.Male, Adult.Dummy, Adult.Dummy, "LDU")
        { }

        /// <summary>
        /// Информация о ребенке.
        /// </summary>
        /// <returns>Информация о ребёнке.</returns>
        public override string GetInfo()
        {
            var fatherStatus = "Отец: отсутствует";
            var motherStatus = "Мать: отсутствует";

            if (Father != null)
            {
                fatherStatus = $"Отец: {Father.GetPersonNameSurname()}";
            }

            if (Mother != null)
            {
                motherStatus = $"Мать: {Mother.GetPersonNameSurname()}";
            }

            var educationStatus = "Не посещает образовательное учреждение";
            if (!string.IsNullOrEmpty(EducationPlace))
            {
                educationStatus = $"Образовательное " +
                    $"учреждение: {EducationPlace}";
            }

            return $"{GetPersonInfo()};\n {fatherStatus}; {motherStatus};" +
                $" {educationStatus}\n";
        }

        /// <summary>
        /// Проверка возраста ребёнка.
        /// </summary>
        /// <param name="age">Возраст ребёнка.</param>
        /// <exception cref="IndexOutOfRangeException">Возраст должен быть
        /// в допустимом диапазоне.</exception>
        protected void CheckAge(int age)
        {
            base.CheckAge(age, MinAge, MaxAge);
        }

        /// <summary>
        /// Проверка пола родителя.
        /// </summary>
        /// <param name="parent">Родитель.</param>
        /// <param name="gender">Пол родителя.</param>
        /// <exception cref="ArgumentException">Пол родителя должен
        /// отличаться от заданного.</exception>
        private static void CheckParentGender(Adult parent, Gender gender)
        {
            if (parent != null && parent.Gender == gender)
            {
                throw new ArgumentException("Пол родителя должен " +
                    "быть противоположным.");
            }
        }

        /// <summary>
        /// Получить случайного родителя.
        /// </summary>
        /// <param name="gender">Пол родителя.</param>
        /// <returns>Объект родителя или null.</returns>
        /// <exception cref="ArgumentException">Допустим только
        /// ввод 1 или 2.</exception>
        public static Adult GetRandomParent(Gender gender)
        {
            var random = new Random();
            var parentStatus = random.Next(1, 3);
            if (parentStatus == 1)
            {
                return null;
            }
            else
            {
                return Adult.GetRandomPerson(gender);
            }
        }

        /// <summary>
        /// Метод для создания случайного ребёнка.
        /// </summary>
        /// <returns>Информация о ребёнке.</returns>
        public static Child GetRandomPerson()
        {
            string[] maleNames =
            {
                "Liam", "Noah", "Oliver",
                "Elijah", "James",
                "William", "Benjamin",
                "Colin", "Lucas", "Marcus"
            };

            string[] femaleNames =
            {
                "Dolores", "Leta", "Pansy",
                "Olivia", "Tracey",
                "Charlotte", "Katie",
                "Mia", "Sophia", "Alicia"
            };

            string[] surnames =
            {
                "Smith", "Jones", "Weasley",
                "Williams", "Taylor",
                "Brown", "Davies", "Carrow",
                "Evans", "Thomas"
            };

            string[] schools =
            {
                "STEP", "BRAIN", "FOCUS",
                "LEAD", "WISE", "RISE",
                "INSPIRE"
            };

            string[] kindergartens =
            {
                "KIDS", "TOTS", "BLOOM",
                "LITE", "PLAY", "SMILE",
                "GROW"
            };

            var random = new Random();
            var tmpNumber = random.Next(1, 3);

            Gender tmpGender = tmpNumber == 1
                ? Gender.Male
                : Gender.Female;

            string tmpName = tmpGender == Gender.Male
                ? maleNames[random.Next(maleNames.Length)]
                : femaleNames[random.Next(femaleNames.Length)];

            var tmpSurname = surnames[random.Next(surnames.Length)];

            var tmpAge = random.Next(MinAge, MaxAge + 1);

            Adult tmpFather = GetRandomParent(Gender.Male);
            Adult tmpMother = GetRandomParent(Gender.Female);

            string education = tmpAge >= 7
                ? schools[random.Next(schools.Length)]
                : kindergartens[random.Next(kindergartens.Length)];

            return new Child(tmpName, tmpSurname, tmpAge, tmpGender,
                tmpFather, tmpMother, education);
        }

        /// <summary>
        /// Метод, который показывает хобби ребенка.
        /// </summary>
        /// <returns>Выбранное хобби.</returns>
        public string GetHobby()
        {
            var rnd = new Random();

            string[] hobby =
            {
                "Рисование", "Компьютерные игры",
                "Баскетбол", "Фехтование"
            };

            var preferredHobby = hobby[rnd.Next(hobby.Length)];

            return $"Предпочитаемое хобби" +
                $" для этого ребёнка — {preferredHobby}";
        }
    }
}

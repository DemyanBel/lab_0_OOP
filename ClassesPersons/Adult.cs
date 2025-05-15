using ClassPerson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassesPersons
{
    /// <summary>
    /// Класс, описывающий взрослого человека.
    /// </summary>
    public class Adult : PersonBase
    {
        /// <summary>
        /// Номер паспорта (6 цифр).
        /// </summary>
        private int _passportNumber;

        /// <summary>
        /// Первая и вторая части серии паспорта,
        /// объединённые в одно число 
        /// (например, 0122).
        /// </summary>
        private int _passportSeries;

        /// <summary>
        /// Минимальный возраст взрослого.
        /// </summary>
        private const int MinAge = 18;

        /// <summary>
        /// Максимально допустимый возраст.
        /// </summary>
        private const int MaxAge = 125;

        /// <summary>
        /// Нижняя граница диапазона номеров паспортов.
        /// </summary>
        private const int PassportLowBound = 101;

        /// <summary>
        /// Верхняя граница диапазона номеров паспортов.
        /// </summary>
        private const int PassportHighBound = 999999;

        /// <summary>
        /// Нижняя граница первой части серии.
        /// </summary>
        private const int SeriesPart1Low = 1;

        /// <summary>
        /// Верхняя граница первой части серии.
        /// </summary>
        private const int SeriesPart1High = 99;

        /// <summary>
        /// Нижняя граница второй части серии.
        /// </summary>
        private const int SeriesPart2Low = 0;

        /// <summary>
        /// Верхняя граница второй части серии.
        /// </summary>
        private const int SeriesPart2High = 25;
        
        /// <summary>
        /// Место работы
        /// </summary>
        private string _employer;

        /// <summary>
        /// Супруг/супруга взрослого.
        /// </summary>
        private Adult _spouse;

        // Статический dummy-объект
        public static readonly Adult Dummy = CreateDummy();

        /// <summary>
        /// Номер паспорта взрослого (101–999999).
        /// </summary>
        public int PassportNumber
        {
            get => _passportNumber;
            set
            {
                CheckPassportNumber(value);
                _passportNumber = value;
            }
        }

        /// <summary>
        /// Серия паспорта в формате 4 цифр (например, 0122).
        /// </summary>
        public int PassportSeries
        {
            get => _passportSeries;
            set
            {
                CheckPassportSeries(value);
                _passportSeries = value;
            }
        }

        /// <summary>
        /// Серия и номер паспорта в формате СССС НННННH.
        /// </summary>
        public string PassportSeriesAndNumber
        {
            get => $"{PassportSeries:D4} {PassportNumber:D6}";
            set
            {
                var parts = value.Split(' ');
                if (parts.Length != 2 || parts[0].Length != 4 || 
                    parts[1].Length != 6)
                    throw new FormatException("Паспорт должен " +
                        "быть в формате: 'СССС НННННН' " +
                        "(например: '0122 302011').");

                int series = int.Parse(parts[0]);
                int number = int.Parse(parts[1]);

                PassportSeries = series;
                PassportNumber = number;
            }
        }

        //TODO: validation
        /// <summary>
        /// Место работы взрослого.
        /// </summary>
        public string Employer
        {
            get => _employer;
            set => _employer = value
                ?? throw new ArgumentNullException(
                    nameof(Employer), "Место работы не можетбыть null");
        }

        /// <summary>
        /// Супруг/супруга взрослого.
        /// </summary>
        public Adult Spouse
        {
            get => _spouse;
            set => _spouse = value
                ?? throw new ArgumentNullException(
                    nameof(Spouse), "Супруг/супруга не может быть null");
        }

        /// <summary>
        /// Конструктор для создания экземпляра класса Adult.
        /// </summary>
        public Adult(string firstName, string lastName, int age,
            Gender gender, int passportSeries, int passportNumber,
            Adult spouse, string employer)
            : base(firstName, lastName, age, gender)
        {
            PassportSeries = passportSeries;
            PassportNumber = passportNumber;
            Employer = employer;
            if (spouse == null)
            {
                _spouse = this;
            }
            else
            {
                Spouse = spouse;
            }
        }

        /// <summary>
        /// Конструктор по умолчанию.
        /// </summary>
        public Adult() : this("Unknown", "Unknown", 18,
            Gender.Male, 101, 100000, Dummy, "Не работает")
        { }

        /// <summary>
        /// Создаёт и возвращает dummy-объект Adult.
        /// </summary>
        private static Adult CreateDummy()
        {
            var dummy = new Adult(
                firstName: "DUMMY_Имя",
                lastName: "DUMMY_Фамилия",
                age: 22,
                gender: Gender.Male,
                passportSeries: 102,
                passportNumber: 100003,
                spouse: null, // временно
                employer: "DUMMY_Не работает"
            );
            dummy.Spouse = dummy;
            return dummy;
        }

        /// <summary>
        /// Информация о взрослом.
        /// </summary>
        public override string GetInfo()
        {
            var marrigaeStatus = Spouse != null
                ? $"В браке с: {Spouse.GetPersonNameSurname()}"
                : "Не в браке";

            var employerStatus = string.IsNullOrEmpty(Employer)
                ? "Не работает"
                : $"Работает в: {Employer}";

            return $"{GetPersonInfo()};\n " +
                $"Паспорт: {PassportSeriesAndNumber}; " +
                $"{marrigaeStatus}; {employerStatus}\n ";
        }

        /// <summary>
        /// Проверка возраста взрослого.
        /// </summary>
        protected void CheckAge(int age)
        {
            base.CheckAge(age, MinAge, MaxAge);
        }

        /// <summary>
        /// Проверка корректности номера паспорта.
        /// </summary>
        private static void CheckPassportNumber(int passportNumber)
        {
            if (passportNumber is < PassportLowBound or > PassportHighBound)
            {
                throw new IndexOutOfRangeException("Номер паспорта" +
                    " должен быть в диапазоне" +
                    $" [{PassportLowBound:D6}:{PassportHighBound}]");
            }
        }

        /// <summary>
        /// Проверка корректности серии паспорта.
        /// </summary>
        private static void CheckPassportSeries(int passportSeries)
        {
            int part1 = passportSeries / 100;
            int part2 = passportSeries % 100;

            var parts = new (int value, int low, int high, string name)[]
            {
                (part1, SeriesPart1Low, SeriesPart1High, "Первая"),
                (part2, SeriesPart2Low, SeriesPart2High, "Вторая")
            };

            foreach (var (value, low, high, name) in parts)
            {
                if (value < low || value > high)
                {
                    string range = $"[{low}:{high}]";
                    throw new IndexOutOfRangeException($"{name}" +
                        $" часть серии должна быть в диапазоне {range}");
                }
            }
        }

        /// <summary>
        /// Метод для создания случайного взрослого.
        /// </summary>
        public static Adult GetRandomPerson(Gender gender = Gender.Other)
        {
            string[] maleNames =
            { 
                "Liam", "Noah", "Oliver",
                "Elijah", "James", "William",
                "Benjamin", "Colin", "Lucas",
                "Marcus" 
            };
            string[] femaleNames = 
            { 
                "Dolores", "Leta", "Pansy",
                "Olivia", "Tracey", "Charlotte",
                "Katie", "Mia", "Sophia", "Alicia"
            };
            string[] surnames = 
            { 
                "Smith", "Jones", "Weasley",
                "Williams", "Taylor", "Brown",
                "Davies", "Carrow", "Evans", "Thomas" 
            };
            string[] employers = 
            { 
                "SO UPS", "Ministry of Energy", 
                "Rosseti", "RusHydro", "Rosatom",
                "50Hz", "NYISO", "Tennet",
                "Bank of America", "Sber" 
            };

            var random = new Random();

            if (gender == Gender.Other)
            {
                gender = random.Next(1, 3) == 1 ? Gender.Male
                    : Gender.Female;
            }

            string tmpName = gender == 
                Gender.Male ? maleNames[random.Next(maleNames.Length)] :
                femaleNames[random.Next(femaleNames.Length)];
            string tmpSurname = surnames[random.Next(surnames.Length)];
            int tmpAge = random.Next(MinAge, MaxAge + 1);
            int tmpSeries1 = random.Next(SeriesPart1Low, SeriesPart1High + 1);
            int tmpSeries2 = random.Next(SeriesPart2Low, SeriesPart2High + 1);
            string combinedSeries = $"{tmpSeries1:D2}{tmpSeries2:D2}";
            int tmpPassportSeries = int.Parse(combinedSeries);
            int tmpPassportNumber = 
                random.Next(PassportLowBound, PassportHighBound + 1);

            Adult tmpSpouse = Dummy;
            if (random.Next(1, 3) == 1)
            {
                tmpSpouse = new Adult
                {
                    Gender = gender == 
                        Gender.Male ? Gender.Female : Gender.Male,
                    FirstName = gender == 
                        Gender.Female ? maleNames[random.Next(maleNames.Length)]
                        : femaleNames[random.Next(femaleNames.Length)],
                    LastName = surnames[random.Next(surnames.Length)]
                };
            }

            string? tmpEmployer = random.Next(1, 3) == 1 
                ? employers[random.Next(employers.Length)] : "Не работает";

            return new Adult(tmpName, tmpSurname, tmpAge,
                gender, tmpPassportSeries, tmpPassportNumber,
                tmpSpouse, tmpEmployer);
        }

        /// <summary>
        /// Метод, который показывает предпочитаемую машину.
        /// </summary>
        public string GetCar()
        {
            var rnd = new Random();
            string[] cars =
            { 
                "BMW", "Mercedes-Benz",
                "Audi", "Lada", "Toyota",
                "Tesla" 
            };
            return $"Предпочитаемый " +
                $"автомобиль этого человека:" +
                $" {cars[rnd.Next(cars.Length)]}";
        }
    }
}

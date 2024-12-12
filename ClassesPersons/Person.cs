using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace ClassPerson
{
    // Объявляем Gender
    public enum Gender
    {
        Male,
        Female,
        Other
    }
    // Класс Person
    public class Person
    {
        // Поля класса
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public int Age { get; private set; }
        public Gender Gender { get; private set; }


        // Конструктор класса
        public Person(string firstName, string lastName, string _age, Gender gender)
        {
            NameVerification(firstName);
            LastNameVerification(lastName);
            AgeVerification(_age);
            int age = Convert.ToInt32(_age);
            FirstName = firstName;
            LastName = lastName;
            Age = age;
            Gender = gender;
        }
        // Метод вывода информации
        public string GetInfo()
        {
            return $"Имя: {FirstName}, Фамилия: {LastName}, Возраст: {Age}, Пол: {Gender}";
        }
        // Регулярное выражение для проверки имени и фамилии
        private static readonly Regex nameRegex = new Regex(@"^[a-zA-Zа-яА-Я]+$");

        //Метод проверки имени
        private void NameVerification(string firstName)
        {

            if (string.IsNullOrWhiteSpace(firstName))
            {
                throw new ArgumentException("Имя не может быть пустым, состоять из пробелов или null.", nameof(firstName));
            }
            if (!nameRegex.IsMatch(firstName))
            {
                throw new ArgumentException("Имя должно содержать только буквы русского или английского алфавита.", nameof(firstName));
            }
        }
        // Метод проверки фамилии
        private void LastNameVerification(string lastName)
        {
            if (string.IsNullOrWhiteSpace(lastName))
            {
                throw new ArgumentException("Фамилия не может быть пустой, состоять из пробелов или null.", nameof(lastName));
            }
            if (!nameRegex.IsMatch(lastName))
            {
                throw new ArgumentException("Фамилия должна содержать только буквы русского или английского алфавита.", nameof(lastName));
            }
        }
        // Метод проверки возраста
        private void AgeVerification(string _age)
        {
            if (!int.TryParse(_age, out int age) || age < 0 || age >= 125)
            {
                throw new ArgumentException("Возраст не должен содержать постороних символов и должен быть положительным числом больше нуля.", nameof(age));
            }
        }
    }
}

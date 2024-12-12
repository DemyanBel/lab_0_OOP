using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassPerson;

namespace ClassPersonList
{
    public class PersonList
    {
        private List<Person> people = new List<Person>();
        // Метод добавления элементов списка
        public void AddList(Person person)
        {
            people.Add(person);
        }
        // Метод удаления элементов списка
        public void RemoveList(Person person)
        {
            people.Remove(person);
        }
        // Метод удаления элемента по индексу
        public void RemoveIndex(int index)
        {
            if (index < 0 || index >= people.Count)
            {
                throw new IndexOutOfRangeException("Индекс вне диапазона.");
            }
            people.RemoveAt(index);
        }
        // Метод получения элемента по индексу
        public Person GetIndex(int index)
        {
            if (index >= people.Count || index < 0)
            {
                throw new IndexOutOfRangeException("Индекс вне диапазона.");
            }
            return people[index];
        }
        // Метод возвращает индекс элемента в списке
        public int IndexElementFromList(Person person)
        {
            return people.IndexOf(person);
        }
        // Метод очистки списка
        public void ClearList()
        {
            people.Clear();
        }
        // Определение количества элементов в списке
        public int Count => people.Count;
        // Метод получения элементов в списке
        public void PrintList()
        {
            if (people.Count == 0)
            {
                Console.WriteLine("Список пуст.");
                return;
            }
            foreach (Person person in people)
            {
                Console.WriteLine(person.GetInfo());
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    internal class Program
    {
        public interface IVisitable
        {
            void Accept(ICompositeVisitor visitor);
        }

        public interface ICompositeVisitor
        {
            void VisitEmployee(Person person);
            void VisitPrivateHouse(PrivateHouse private_house);
            void VisitApartmentBuilding(ApartmentBuilding apartment_building);

        }
        public interface ICompositeComponent
        {
            ICompositeComponent Add(ICompositeComponent Component);
        }

        public class CompositeComponent : ICompositeComponent, IVisitable
        {
            public string name;

            public CompositeComponent(string name)
            {
                this.name = name;
            }
            public virtual ICompositeComponent Add(ICompositeComponent Component)
            {
                throw new Exception($"Can't add {Component} to {this}");
            }


            public virtual void Accept(ICompositeVisitor visitor)
            {

            }

            public override string ToString()
            {
                return name;
            }


        }

        public class Person : CompositeComponent
        {
            public string sex;
            public DateTime date_of_birth;

            public Person(string name, string sex, DateTime date_of_birth) : base(name)
            {
                this.sex = sex;
                this.date_of_birth = date_of_birth;
            }
            public override string ToString()
            {
                return $"{name}, {sex}, {date_of_birth}";
            }
            public override void Accept(ICompositeVisitor visitor)
            {
                visitor.VisitEmployee(this);
            }
        }

        public class PrivateHouse : CompositeComponent
        {
            public List<Person> persons = new List<Person>();

            public PrivateHouse(string name) : base(name)
            {

            }
            public ICompositeComponent Add(Person person)
            {
                persons.Add(person);
                return this;
            }


            
            public override void Accept(ICompositeVisitor visitor)
            {
                visitor.VisitPrivateHouse(this);
            }
        }

        public class ApartmentBuilding : CompositeComponent
        {
            public List<PrivateHouse> private_houses = new List<PrivateHouse>();

            public ApartmentBuilding(string name) : base(name)
            {

            }
            public ICompositeComponent Add(PrivateHouse private_house)
            {
                private_houses.Add(private_house);
                return this;
            }

            public override void Accept(ICompositeVisitor visitor)
            {
                visitor.VisitApartmentBuilding(this);
            }
        }

       

        public class DataOfAllconscripts : ICompositeVisitor
        {
            public void VisitEmployee(Person person)
            {
                if ((DateTime.Now.Year - person.date_of_birth.Year >= 18 && DateTime.Now.Year - person.date_of_birth.Year <= 27) && person.sex == "Чоловіча")
                    Console.WriteLine($"Ім'я: {person.name}, Стать: {person.sex}, Дата народження: {person.date_of_birth}");
                
            }
            public void VisitPrivateHouse(PrivateHouse private_house)
            {
                private_house.persons.ForEach(person => person.Accept(this));
            }
            public void VisitApartmentBuilding(ApartmentBuilding apartment_building)
            {
                apartment_building.private_houses.ForEach(person => person.Accept(this));

            }
           
        }

        public class CountTheTotalNumberOfResidents : ICompositeVisitor
        {
            public int sum = 0;
            public void VisitEmployee(Person person)
            {
                sum++;

            }
            
            public void VisitPrivateHouse(PrivateHouse private_house)
            {
                private_house.persons.ForEach(person => person.Accept(this));
                

                
            }
            public void VisitApartmentBuilding(ApartmentBuilding apartment_building)
            {
                apartment_building.private_houses.ForEach(person => person.Accept(this));
                Console.WriteLine($"Загальна кількість мешканців: {sum}");
            }

        }



        static void Main(string[] args)
        {
            

            Person person1 = new Person("Ім'я", "Чоловіча", new DateTime(2002, 03, 13));
            Person person2 = new Person("Ім'я", "Жіноча", new DateTime(2002, 09, 26));
            Person person3 = new Person("Ім'я", "Чоловіча", new DateTime(1987, 07, 01));
            Person person4 = new Person("Ім'я", "Чоловіча", DateTime.Now);
            Person person5 = new Person("Ім'я", "Чоловіча", new DateTime(2006, 12, 24));
            Person person6 = new Person("Ім'я", "Чоловіча", new DateTime(2000, 09, 16));
            Person person7 = new Person("Ім'я", "Чоловіча", new DateTime(1997, 05, 10));
            Person person8 = new Person("Ім'я", "Чоловіча", new DateTime(1999, 03, 08));


            PrivateHouse private_house1 = new PrivateHouse("");
            PrivateHouse private_house2 = new PrivateHouse("");
            PrivateHouse private_house3 = new PrivateHouse("");
            PrivateHouse private_house4 = new PrivateHouse("");

            ApartmentBuilding apartment_building1 = new ApartmentBuilding("");



            private_house1.Add(person1);
            private_house1.Add(person2);
            private_house1.Add(person3);

            private_house2.Add(person4);
            private_house2.Add(person5);
            private_house2.Add(person6);

            private_house3.Add(person7);

            private_house4.Add(person8);

            apartment_building1.Add(private_house1);
            apartment_building1.Add(private_house4);


            DataOfAllconscripts data_of_allconscripts = new DataOfAllconscripts();

            CountTheTotalNumberOfResidents count = new CountTheTotalNumberOfResidents();

            Console.WriteLine("Дані призовників: ");
            private_house2.Accept(data_of_allconscripts);
            private_house3.Accept(data_of_allconscripts);
            apartment_building1.Accept(data_of_allconscripts) ;

            private_house2.Accept(count);
            private_house3.Accept(count);
            apartment_building1.Accept(count);
        }
    }
}
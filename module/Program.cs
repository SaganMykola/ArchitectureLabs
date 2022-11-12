using System.Collections.Generic;
using System;

/*
Патерни Gof: 
Всього є 23 патерни (5 Породжуючі, 7 Структурні, 11 Поведінкові)

Патерни GoF. Візитер.
Потрібно додати нові операції над класами без внесення змін безпосередньо в класи.

Переваги:

Спрощує додавання операцій

Групування операцій в одному місці

Недоліки:
Жорстка прив'язка до конкретних класів

Можливе порушення інкапсуляції
 */



/* 
Резюме користувача – це об’єкт який може містити ПІБ, фотографію,
відомості про освіту (ЗВО, спеціальність, рік отримання диплому),
відомості про попередні місця роботи (компанія, посада, рік початку
роботи та рік звільнення) та список відомих технологій. Деякі дані в
резюме можуть бути відсутні. Створити декілька об’єктів резюме,
наприклад: молодого спеціаліста, який закінчив ЗВО в цьому році, без
досвіду роботи, з фото та резюме досвідченого сеньйора із 2 вищими
освітами та стажем 15 років в різних компаніях, без фото.
Вказати шаблон, який доцільно використати для розв&#39;язування задачі.
*/
namespace Builder
    {
        class Resume
        {
            public string name;
            public string photo;
            private List<object> information = new List<object>();

            public void Add(string info)
            {
                this.information.Add(info);
  
            }



        public override string ToString()
            {
                string str = string.Empty;
                foreach (string info in this.information)
                {
                    str += $"\t{info},\n ";
                }
                return $"ПІБ {this.name}, фотографія: {this.photo} інформація: \n {str}";
            }
        }

        interface IBuilder
        {
            IBuilder AddInfo(object info);
            IBuilder SetName(string name);
            IBuilder SetPhoto(string photo);
            Resume GetResume();
            void Reset();
        }

        class Builder : IBuilder
        {
            protected Resume resume = new Resume();

            public void Reset()
            {
                this.resume = new Resume();
            }
            public IBuilder SetName(string name)
            {
                this.resume.name = name;
                return this;

            }

            public IBuilder SetPhoto(string photo)
        {
            this.resume.photo = photo;
            return this;
        }

            public IBuilder AddInfo(object info)
            {
                this.resume.Add(info as string);
                return this;
            }


        public Resume GetResume()
            {
                Resume result = this.resume;
                this.Reset();
                return result;
            }
        }


            class Program
             {   
                static void Main(string[] args)
                {
                    IBuilder builder = new Builder();
                    Resume resume1 = builder
                                        .SetName("Full name")

                                        .SetPhoto("Photo")
                                      
                                        .AddInfo("закінчив ЗВО в цьому році")
                                     
                                        .AddInfo("бездосвіду роботи")

                                        .AddInfo("технології")

                                        .SetName("Full name")

                                        .AddInfo("2 вищимі освіти")

                                        .AddInfo("Стаж 15 років в різних компаніях")
                                        .GetResume();
                    Console.WriteLine(resume1.ToString());

                    
                    Resume resume2 = builder
                                        .SetName("Full name")

                                        .SetPhoto("No Photo")

                                        .AddInfo("з 2 вищими освіти")

                                        .AddInfo("Стаж 15 років в різних компаніях")
                                        .GetResume();
                    Console.WriteLine(resume2.ToString());
        }

    
    
       
     }
 }

using System;

namespace Creational
{
    
    namespace Prototype
    {
        interface IPrototype
        {
            IPrototype Clone();
        }
        class Product : IPrototype
        {
            public string name = "No name";
            public double price = 0;
            public string manufacturer = "No manufacturer";

            public IPrototype Clone()
            {
                return this.MemberwiseClone() as Product;
            }
        }
        class CRM : IPrototype
        {
            public DateTime date = DateTime.Now;
            public double order_amount = 0;
            public string address = "No address";
            protected int token;
            public bool payment = true;
            public string payment_status;

            public CRM()
            {
                Random rnd = new Random();
                this.token = rnd.Next();
            }

            public virtual IPrototype Clone()
            {
                return this.MemberwiseClone() as CRM;
            }
        }

        class CustomProduct : CRM
        {
            public Product obj;
            public CustomProduct(Product obj = null) : base()
            {
                this.obj = obj;
            }
            public override CustomProduct Clone()
            {
                CustomProduct clone = this.MemberwiseClone() as CustomProduct;
                clone.obj = this.obj.Clone() as Product;
                clone.payment_status = "Ні";
                

                return clone;
                
            }

            public override string ToString()
            {
                return $"{{\nДата замовлення:{this.date},\n\tСума замовлення:{this.date},\n\tАдреса доставки: {this.address},\n\tТокен: {this.token},\n\tСтан оплати:{this.payment_status},\n\tТовар: {(this.obj as Product).name}, Ціна:{(this.obj as Product).price}, Виробник:{(this.obj as Product).manufacturer} }}";
            }
        }

        class Program
        {
            static void Main(string[] args)
            {
                
                    var obj = new Product();
                    CustomProduct product = new CustomProduct(obj);
                    CustomProduct productClone = product.Clone();
                   
                    if (product.payment == true)
                        product.payment_status = "Так";
                    else
                        product.payment_status = "Ні";
                    Console.WriteLine(product.ToString());
                    Console.WriteLine(productClone.ToString());
                
            }
        }
    }

}



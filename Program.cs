using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_Plataformas_de_Desarrollo
{
    class Program
    {
        static void Main(string[] args)
        {
            Carro car = new Carro(15);

            car.AgregarProducto(2,  1);
            car.AgregarProducto(3, 5);
            car.AgregarProducto(5, 4);

            Console.WriteLine(car.ToString());
            Console.ReadLine();
        }
    }
}

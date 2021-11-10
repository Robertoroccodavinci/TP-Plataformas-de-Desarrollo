using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Windows.Forms;

namespace TP_Plataformas_de_Desarrollo
{
    class Mercado
    {
        
       // private const int MaxCategorias = 10;
       // private int CantCategorias = 0;

        private MyContext contexto;

        public Mercado()
        {
            inicializarAtributos();
        }

        private void inicializarAtributos()
        {
            try
            {
                //creo un contexto
                nContexto = new MyContext();

                //cargo las entidades
                contexto.categorias.Load();
                contexto.productos.Load();
                contexto.usuarios.Load();
                contexto.carros.Load();
                contexto.compras.Load();

                foreach (Usuario u in contexto.usuarios) 
                {
                    u.miCarro = new Carro(u);
                    u.compras = new List<Compra>();
                    if (contexto.compras.Where(c => c.idUsuario == u.idUsuario) != null) 
                    {
                        u.compras = contexto.compras.Where(c => c.idUsuario == u.idUsuario).ToList();
                    }
                }

                contexto.SaveChanges();
            }
            catch (Exception)
            {
                Console.WriteLine("ERROR AL INICIALIZAR ATRIBUTOS");
            }
        }

        public MyContext nContexto
        {
            get { return contexto; }
            set { contexto = value; }
        }

        // ######################################################################################
        //                                  METODOS DE USUARIO
        // ######################################################################################
        //                                  AGREGAR USUARIO
        //                                  MODIFICAR USUARIO
        //                                  ELIMINAR USUARIO
        //                                  MOSTRAR USUARIO
        // ######################################################################################
        public bool AgregarUsuario(int DNI, string nombre, string apellido, string Mail, string password, int CUIT_CUIL, int rol)
        {
            bool res = false;

            try 
            {
                if (contexto.usuarios.Where(U => U.dni == DNI && U.password == password).FirstOrDefault() != null)
                {
                    Usuario aux = new Usuario(DNI, nombre, apellido, Mail, password, CUIT_CUIL, rol);
                    contexto.usuarios.Add(aux);
                    Carro auxC = new Carro(aux);
                    contexto.carros.Add(auxC);

                    contexto.SaveChanges();
                    res = true;
                }
                else 
                {
                    MessageBox.Show("ERROR ya existe el usuario");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("ERROR no se pudo agregar el usuarios");
            }

            return res;
        }









        // #######################################################################################
        //                                  INICIAR SESION
        // #######################################################################################
        
        public Usuario IniciarSesion(int DNI, string pass)
        {
            Usuario aux = contexto.usuarios.Where(U => U.dni == DNI && U.password == pass).FirstOrDefault();
            if (aux != null)
            {
                return aux;
            }
            else 
            {
                return null;
            }
        }

        // #######################################################################################
        //                                  ES ADMIN
        // #######################################################################################
        
        public bool esAdmin(int ID)
        {
            bool res = false;
            if (nContexto.usuarios.Where(u => u.idUsuario == ID).FirstOrDefault().rol == 1) //preguntamos si usuario con ID es admin
            { 
                res= true; 
            }
            return res;
        }


        // #######################################################################################
        //                                  CERRAR CONTEXTO
        // #######################################################################################
        public void cerrarContexto()
        {
            contexto.Dispose();
        }
    }
}

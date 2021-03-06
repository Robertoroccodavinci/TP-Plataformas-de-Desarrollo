
using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace TP_Plataformas_de_Desarrollo
{
    class MyContext : DbContext
    {
        // se encargara de todo lo que es el manejo de datos
        // crearemos instancia de esta clase, solo tiene que haber una, en Mercado
        // hara un seguimiento
        // no guardara los cambios hasta que se llame al Save Changes
        // luego cerraremos la instancia

        // Excepcion: InvalidOperationException
        // si salta: algo esta mal en este codigo
        public DbSet<Usuario> usuarios { get; set; }
        public DbSet<Producto> productos { get; set; }
        public DbSet<Categoria> categorias { get; set; }
        public DbSet<Carro> carros { get; set; }
        public DbSet<Compra> compras { get; set; }

        public MyContext() { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionbuilder es el objeto que permite setear opciones para la configuracion de la base de datos
            //usa SQL Server pasandole el connectionString
            optionsBuilder.UseSqlServer(Properties.Resource.ConnectionString);
        }

        //hacemos un modelo para las clases que ya tenemos
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //##############################################################
            //       USUARIO
            // Creacion de la tabla, primary key y columnas
            //##############################################################

            //nombre de la tabla y primary key
            modelBuilder.Entity<Usuario>()
                .ToTable("Usuario")
                .HasKey(u => u.idUsuario);
            
            //propiedades de los datos
            modelBuilder.Entity<Usuario>(
                usr =>
                {
                    usr.Property(u => u.dni).HasColumnType("int");
                    usr.Property(u => u.dni).IsRequired(true);
                    usr.Property(u => u.nombre).HasColumnType("varchar(50)");
                    usr.Property(u => u.nombre).IsRequired(true);
                    usr.Property(u => u.apellido).HasColumnType("varchar(50)");
                    usr.Property(u => u.apellido).IsRequired(true);
                    usr.Property(u => u.mail).HasColumnType("varchar(50)");
                    usr.Property(u => u.mail).IsRequired(true);
                    usr.Property(u => u.password).HasColumnType("varchar(50)");
                    usr.Property(u => u.password).IsRequired(true);
                    usr.Property(u => u.cuit_cuil).HasColumnType("bigint");
                    usr.Property(u => u.cuit_cuil).IsRequired(true);
                    usr.Property(u => u.rol).HasColumnType("int");//tinyint
                    usr.Property(u => u.rol).IsRequired(true);
                });


            //##############################################################
            //       CARRO
            // Creacion de la tabla, primary key
            //##############################################################
            
            //nombre de la tabla y primary key
            modelBuilder.Entity<Carro>()
                .ToTable("Carro")
                .HasKey(pk => pk.idCarro);

            //#######################################
            // relacion: 1 a 1 con Usuario
            //#######################################
            modelBuilder.Entity<Carro>()
                .HasOne(c => c.usuario)
                .WithOne(u => u.miCarro)
                .HasForeignKey<Carro>(c => c.idUsuario)
                //.HasConstraintName("ForeignName_Usuario_Carro") //podemos dejarle el nombre por defecto quitando el string
                .OnDelete(DeleteBehavior.Cascade);
                ;

            //##############################################################
            //       PRODUCTO
            // Creacion de la tabla, primary key y columnas
            //##############################################################

            //nombre de la tabla y primary key
            modelBuilder.Entity<Producto>()
                .ToTable("Producto")
                .HasKey(P => P.idProducto);

            //propiedades de los datos
            modelBuilder.Entity<Producto>(
                prod =>{ 
                    prod.Property(P => P.nombre).HasColumnType("varchar(50)");
                    prod.Property(P => P.nombre).IsRequired(true);
                    prod.Property(P => P.precio).HasColumnType("decimal(8,2)");
                    prod.Property(P => P.precio).IsRequired(true);
                    prod.Property(P => P.cantidad).HasColumnType("int");
                    prod.Property(P => P.cantidad).IsRequired(true);
                    prod.Property(P => P.idCategoria).HasColumnType("int");
                    prod.Property(P => P.idCategoria).IsRequired(true);
                });

            //#######################################
            // relacion: 1 a muchos con Categoria
            //#######################################
            modelBuilder.Entity<Producto>()
                .HasOne(P => P.cat)
                .WithMany(C => C.productos)
                .HasForeignKey(P => P.idCategoria)
                .OnDelete(DeleteBehavior.Cascade);

            //#######################################
            // relacion: muchos a muchos con CompraProducto
            //#######################################
            modelBuilder.Entity<Producto>()
                .HasMany(P => P.compras)
                .WithMany(C => C.productos)
                .UsingEntity<CompraProducto>(
                    ecp => ecp.HasOne(cp => cp.compra).WithMany(p => p.compraProducto).HasForeignKey(cp => cp.idCompra),
                    ecp => ecp.HasOne(cp => cp.producto).WithMany(p => p.compraProducto).HasForeignKey(cp => cp.idProducto),
                    ecp => ecp.HasKey(pk => pk.idCompraProducto)
                )
                ;
            //#######################################
            // relacion: muchos a muchos con CarroProducto
            //#######################################
            modelBuilder.Entity<Producto>()
                .HasMany(P => P.carros)
                .WithMany(C => C.productos)
                .UsingEntity<CarroProducto>(
                    ecp => ecp.HasOne(cp => cp.carro).WithMany(p => p.carroProducto).HasForeignKey(cp => cp.idCarro),
                    ecp => ecp.HasOne(cp => cp.producto).WithMany(p => p.carroProducto).HasForeignKey(cp => cp.idProducto),
                    ecp => ecp.HasKey(pk => pk.idCarroProducto)
                );

            //##############################################################
            //       CARRO PRODUCTO
            // propiedades
            //##############################################################

            //propiedades de los datos
            modelBuilder.Entity<CarroProducto>(
                cp => { cp.Property(cp => cp.cantidad).HasColumnType("tinyint");
                        cp.Property(cp => cp.cantidad).IsRequired(true); }
                );

            //##############################################################
            //       CATEGORIA
            // Creacion de la tabla, primary key y columnas
            //##############################################################

            //nombre de la tabla y primary key
            modelBuilder.Entity<Categoria>()
                .ToTable("Categoria")
                .HasKey(C => C.idCategoria);

            //propiedades de los datos
            modelBuilder.Entity<Categoria>(
                cat => { cat.Property(C => C.nombre).HasColumnType("varchar(50)");
                         cat.Property(C => C.nombre).IsRequired(true); }
                );


            //##############################################################
            //       COMPRA
            // Creacion de la tabla, primary key y columnas
            //##############################################################

            //nombre de la tabla y primary key
            modelBuilder.Entity<Compra>()
                .ToTable("Compra")
                .HasKey(Co => Co.idCompra);

            //propiedades de los datos
            modelBuilder.Entity<Compra>(
                c => { c.Property(Co => Co.total).HasColumnType("decimal(8,2)");
                       c.Property(Co => Co.total).IsRequired(true);}
                );

            //#######################################
            // relacion: muchos a 1 con Usuario
            //#######################################
            modelBuilder.Entity<Compra>()
                .HasOne(Co => Co.usuario)
                .WithMany(U => U.compras)
                .HasForeignKey(Co => Co.idUsuario)
                .OnDelete(DeleteBehavior.Cascade);

            //##############################################################
            //       COMPRA PRODUCTO
            // propiedades
            //##############################################################

            //propiedades de los datos
            modelBuilder.Entity<CompraProducto>(
                cp => { cp.Property(cp => cp.cantidad).HasColumnType("tinyint");
                        cp.Property(cp => cp.cantidad).IsRequired(true); }
                );

            //###############################
            //      IGNORAMOS
            //###############################
            modelBuilder.Ignore<Mercado>();

            //##############################################################
            //      AGREGAMOS DATOS
            //##############################################################
            
            modelBuilder.Entity<Categoria>().HasData(
                new { idCategoria = 1, nombre = "Comida" },
                new { idCategoria = 2, nombre = "Bebida" },
                new { idCategoria = 3, nombre = "Ropa" },
                new { idCategoria = 4, nombre = "Articulos de limpieza" },
                new { idCategoria = 5, nombre = "Electrodomesticos" },
                new { idCategoria = 6, nombre = "Informatica" },
                new { idCategoria = 7, nombre = "Herramientas" },
                new { idCategoria = 8, nombre = "Electronica" },
                new { idCategoria = 9, nombre = "Mascotas" },
                new { idCategoria = 10, nombre = "Libreria" }
                );
            
            modelBuilder.Entity<Producto>().HasData(
                new { idProducto = 1,  nombre = "Gaseosa", precio = 125.00, cantidad = 100, idCategoria = 2 },
                new { idProducto = 2,  nombre = "Cerveza", precio = 120.00, cantidad = 50,  idCategoria = 2 },
                new { idProducto = 3,  nombre = "Agua",    precio = 78.00,  cantidad = 100, idCategoria = 2 },
                new { idProducto = 4,  nombre = "Papas",   precio = 250.00, cantidad = 100, idCategoria = 1 },
                new { idProducto = 5,  nombre = "Palitos", precio = 126.00, cantidad = 100, idCategoria = 1 },
                new { idProducto = 6,  nombre = "Chizitos", precio = 138.00, cantidad = 100, idCategoria = 1 },
                new { idProducto = 7,  nombre = "TV", precio = 80000.00, cantidad = 150, idCategoria = 8 },
                new { idProducto = 8,  nombre = "PC", precio = 60000.00, cantidad = 30, idCategoria = 6 },
                new { idProducto = 9,  nombre = "Pantalon Deportivo", precio = 6500.00, cantidad = 50, idCategoria = 3 },
                new { idProducto = 10, nombre = "Camiseta Deportiva", precio = 6500.00, cantidad = 50, idCategoria = 3 },
                new { idProducto = 11, nombre = "Lavandina", precio = 49.00, cantidad = 50, idCategoria = 4 },
                new { idProducto = 12, nombre = "Escoba", precio = 340.00, cantidad = 50, idCategoria = 4 },
                new { idProducto = 13, nombre = "Mouse", precio = 1500.00, cantidad = 20, idCategoria = 6 },
                new { idProducto = 14, nombre = "Teclado", precio = 3500.00, cantidad = 20, idCategoria = 6 },
                new { idProducto = 15, nombre = "Monitor", precio = 28000.00, cantidad = 20, idCategoria = 6 },
                new { idProducto = 16, nombre = "Notebook", precio = 95000.00, cantidad = 20, idCategoria = 6 },
                new { idProducto = 17, nombre = "Leche", precio = 95.00, cantidad = 100, idCategoria = 2 },
                new { idProducto = 18, nombre = "Energizante", precio = 108.00, cantidad = 100, idCategoria = 2 },
                new { idProducto = 19, nombre = "Mani", precio = 121.00, cantidad = 100, idCategoria = 1 },
                new { idProducto = 20, nombre = "Nachos", precio = 241.00, cantidad = 100, idCategoria = 1 },
                new { idProducto = 21, nombre = "Campera", precio = 6000.00, cantidad = 50, idCategoria = 3 },
                new { idProducto = 22, nombre = "Sweater", precio = 3000.00, cantidad = 50, idCategoria = 3 },
                new { idProducto = 23, nombre = "Jean", precio = 2000.00, cantidad = 50, idCategoria = 3 },
                new { idProducto = 24, nombre = "Detergente", precio = 87.00, cantidad = 50, idCategoria = 4 },
                new { idProducto = 25, nombre = "Pala", precio = 300.00, cantidad = 50, idCategoria = 4 },
                new { idProducto = 26, nombre = "Secador", precio = 800.00, cantidad = 50, idCategoria = 4 },
                new { idProducto = 27, nombre = "Heladera", precio = 83000.00, cantidad = 20, idCategoria = 5 },
                new { idProducto = 28, nombre = "Lavarropa", precio = 78000.00, cantidad = 20, idCategoria = 5 },
                new { idProducto = 29, nombre = "Cocina", precio = 50000.00, cantidad = 20, idCategoria = 5 },
                new { idProducto = 30, nombre = "Microondas", precio = 25000.00, cantidad = 20, idCategoria = 5 },
                new { idProducto = 31, nombre = "Licuadora", precio = 6000.00, cantidad = 20, idCategoria = 5 },
                new { idProducto = 32, nombre = "Taladro", precio = 15000.00, cantidad = 150, idCategoria = 7 },
                new { idProducto = 33, nombre = "Amoladora", precio = 7000.00, cantidad = 150, idCategoria = 7 },
                new { idProducto = 34, nombre = "Soldadora", precio = 20000.00, cantidad = 150, idCategoria = 7 },
                new { idProducto = 35, nombre = "Sierra", precio = 8000.00, cantidad = 150, idCategoria = 7 },
                new { idProducto = 36, nombre = "Hidrolavadora", precio = 7000.00, cantidad = 150, idCategoria = 8 },
                new { idProducto = 37, nombre = "Parlantes", precio = 10000.00, cantidad = 150, idCategoria = 8 },
                new { idProducto = 38, nombre = "Auriculares", precio = 4500.00, cantidad = 150, idCategoria = 8 },
                new { idProducto = 39, nombre = "Celular", precio = 50000.00, cantidad = 150, idCategoria = 8 },
                new { idProducto = 40, nombre = "Proyector", precio = 45000.00, cantidad = 150, idCategoria = 8 },
                new { idProducto = 41, nombre = "Alimento para Perros", precio = 282.00, cantidad = 200, idCategoria = 9 },
                new { idProducto = 42, nombre = "Alimento para Gatos",  precio = 144.00, cantidad = 200, idCategoria = 9 },
                new { idProducto = 43, nombre = "Cuchas", precio = 3000.00, cantidad = 200, idCategoria = 9 },
                new { idProducto = 44, nombre = "Correas", precio = 1200.00, cantidad = 200, idCategoria = 9 },
                new { idProducto = 45, nombre = "Juguetes", precio = 600.00, cantidad = 200, idCategoria = 9 },
                new { idProducto = 46, nombre = "Cuaderno", precio = 250.00, cantidad = 200, idCategoria = 10 },
                new { idProducto = 47, nombre = "Marcadores", precio = 1200.00, cantidad = 200, idCategoria = 10 },
                new { idProducto = 48, nombre = "Calculadora", precio = 1500.00, cantidad = 200, idCategoria = 10 },
                new { idProducto = 49, nombre = "Resma", precio = 700.00, cantidad = 200, idCategoria = 10 },
                new { idProducto = 50, nombre = "Tablero dibujo", precio = 3000.00, cantidad = 200, idCategoria = 10 }
                );

            modelBuilder.Entity<Usuario>().HasData(
                new { idUsuario = 1, dni = 123456, nombre = "Admin", apellido = "Admin",
                      mail = "admin@gmail.com", password = "123456", cuit_cuil = 34865218, rol = 1},
                new { idUsuario = 2, dni = 654321, nombre = "Pepito", apellido = "Lopez",
                    mail = "pepitolopez@gmail.com", password = "654321", cuit_cuil = 25689475, rol = 2},
                new { idUsuario = 3, dni = 32154869, nombre = "José", apellido = "Perez",
                    mail = "joseperez@hotmail.com", password = "123456", cuit_cuil = 20321548, rol = 3}
                );

            modelBuilder.Entity<Carro>().HasData(
                new { idCarro = 1, idUsuario = 1 },
                new { idCarro = 2, idUsuario = 2 },
                new { idCarro = 3, idUsuario = 3 }
                );

            modelBuilder.Entity<Compra>().HasData(
               new { idCompra = 1, idUsuario = 2, total= 0.0 }
               );

            /*
            modelBuilder.Entity<CarroProducto>().HasData(
               new { idCarroProducto = 1, idCarro = 2, idProducto = 47, cantidad = 2 }
               );

            modelBuilder.Entity<CompraProducto>().HasData(
               new { idCompraProducto = 1, idCompra = 1, idProducto = 20, cantidad = 3 }
               );
            */
            
            
        }
    }
}


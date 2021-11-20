using System;
using System.Collections.Generic;
using System.Text;

namespace TP_Plataformas_de_Desarrollo
{
    class Carro
    {

        public int idCarro { get; set; }
        public Usuario usuario { get; set; }
        public int idUsuario { get; set; }
        public List<CarroProducto> carroProducto { get; set; } = new List<CarroProducto>();
        public ICollection<Producto> productos { get; } = new List<Producto>();
        public Carro() { }
        public Carro(Usuario usuario)
        {
            this.usuario    = usuario;
            this.idUsuario  = usuario.idUsuario;
        }
       
    }
}

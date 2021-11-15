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
        public ICollection<CarroProducto> carroProducto { get; set; }

        public Carro() { }
        public Carro(Usuario usuario)
        {
            this.usuario    = usuario;
            this.idUsuario  = usuario.idUsuario;
        }
       
    }
}

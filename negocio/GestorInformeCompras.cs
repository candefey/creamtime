using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using entidades;
using daos;
namespace negocio
{
    public class GestorInformeCompras
    {
        public static List<Compra> listarCompraPorProveedor(int id)
        {
            
            return DaoCompras.listarCompraPorProveedor(id);
        }

        public static List<Compra> listarCompraPorMateria(int id)
        {

            return DaoCompras.listarCompraPorMp(id);
        }

        public static List<Compra> listarCompraMonto(float desde, float hasta)
        {

            return DaoCompras.listarCompraMonto(desde, hasta);
        }

        public static List<DetalleCompra> listarDetalleCompra(int id)
        {
            return DaoDetalleCompra.listarDetalleCompra(id);

        }
    }
}

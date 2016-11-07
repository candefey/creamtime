using daos;
using entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace negocio
{
    public class GestorProducto
    {
        public static void registrarProducto(Producto producto)
        {
            DaoProducto dao = new DaoProducto();
            dao.insertarProducto(producto);
        }

        public static List<ProductoView> listarProductos(string nombre)
        {
            List<ProductoView> listaFiltrada = new List<ProductoView>();
            
            foreach (var prod in DaoProducto.obtenerProductos(nombre))
            {
                ProductoView producto = new ProductoView();

                producto.Id = prod.Id;
                producto.Nombre = prod.Nombre;
                producto.NombreTipo = prod.Tipo_Producto.Nombre;
                producto.CodigoProducto = prod.Codigo_Producto;
                producto.Precio = "$" + prod.Precio.ToString();
                if (prod.Vigente)
                    producto.Vigente = "SI";
                else
                    producto.Vigente = "NO";

                listaFiltrada.Add(producto);
            }

            return listaFiltrada;
        }

        public static List<TipoProducto> listarTiposProductos()
        {
            return DaoTipoProducto.obtenerTiposProducto();
        }

        public static Boolean existeProducto(int codigo)
        {
            return DaoProducto.existeProducto(codigo);
        }

        public static Producto buscarProducto(int codigo)
        {
            return DaoProducto.buscarProducto(codigo);
        }

        public static Boolean eliminarProducto(int codigo)
        {
            return DaoProducto.eliminarProducto(codigo);
        }

        public static Boolean modificarProducto(Producto producto)
        {
            return DaoProducto.modificarProducto(producto);
        }

        public static List<Producto> listaSabores()
        {
            return DaoProducto.obtenerSabores();
        }
        
        public static List<Producto> listaProductosVendibles()
        {
            return DaoProducto.obtenerProductosVenta();
        }

        public static int obtenerAgregados(int id)
        {
            Producto prod = DaoProducto.buscarAgregados(id);

            return prod.Agregados;
        }

        public static Producto obtenerProductoPorID(int id)
        {
            return DaoProducto.obtenerProductoPorID(id);
        }
       
    }
}

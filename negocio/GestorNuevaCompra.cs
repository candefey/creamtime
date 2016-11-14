using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using entidades;
using daos;
namespace negocio
{
    public class GestorNuevaCompra
    {
        public static List<MateriaPrima> listarMP()
        {
            return DaoMateriaPrima.listarMateriasPrimas();
        }

        public static List<MateriaPrima> listarMPFiltro(int id)
        {
            return DaoMateriaPrima.listarMateriasPrimas(id);
        }

        public static MateriaPrima buscarMateriaPrima(int id)
        {
            return DaoMateriaPrima.buscarMateriaPrima(id);
        }

        public static void insertarCompra(List<DetalleCompraView> detalles)
        {
            float suma = 0;
            foreach (var d in detalles)
            {
                suma += d.Monto;
            }
            DaoMateriaPrima.insertarCompras(detalles,suma);
        }
    }
}

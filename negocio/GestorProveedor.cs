using daos;
using entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace negocio
{
    public class GestorProveedor
    {
        public static void insertarProveedor(Proveedor p)
        {
            DaoProveedor dao = new DaoProveedor();
            dao.insertarProveedor(p);
        }
        public static List<Localidad> listarLocalidades()
        {
            return DaoDomicilio.listarLocalidades();
        }

        public static List<Barrio> listarBarrios(int id)
        {
            return DaoDomicilio.listarBarrios(id);
        }
        public static List<Sexo> listarSexo()
        {
            return DaoCliente.listarSexo();
        }

        public static List<ProveedorView> listarProveedores()
        {
            List<ProveedorView> proveedoresView = new List<ProveedorView>();
            List<Proveedor> proveedores = DaoProveedor.listarProveedores();
            foreach (var p in proveedores)
            {
                ProveedorView pro = new ProveedorView();
                pro.Id = p.Id;
                pro.RazonSocial = p.RazonSocial;
                pro.Telefono = p.Telefono;
                pro.Cuit = p.Cuit;
                pro.Email = p.Email;
                proveedoresView.Add(pro);

            }
            return proveedoresView;
        }

        public static bool existeProveedor(long cuit)
        {
            return DaoProveedor.existeProveedor(cuit);
        }

        public static bool eliminarProveedor(long cuit)
        {
            return DaoProveedor.eliminarProveedor(cuit);
        }

        public static bool actualizarProveedor(Proveedor p)
        {
           return DaoProveedor.actualizarProveedor(p);
        }

        public static Proveedor buscarProveedor(long cuit)
        {
            return DaoProveedor.buscarProveedor(cuit);
        }
    }
}

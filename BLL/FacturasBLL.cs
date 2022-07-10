using Microsoft.EntityFrameworkCore;
using SistemaFacturacion.DAL;
using SistemaFacturacion.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SistemaFacturacion.BLL
{
    class FacturasBLL
    {
        public static bool Existe(int id)
        {
            bool encontrado = false;
            Contexto contexto = new Contexto();

            try
            {
                encontrado = contexto.Facturas.Any(e => e.FacturaId == id);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return encontrado;
        }

        public static bool Insertar(Facturas facturas)
        {
            bool paso = false;
            Contexto contexto = new Contexto();

            try
            {
                contexto.Facturas.Add(facturas);
                foreach (var detalle in facturas.Detalle)
                {
                    contexto.Entry(detalle).State = EntityState.Added;
                    contexto.Entry(detalle.Productos).State = EntityState.Modified;

                    detalle.Facturas.Total += detalle.Productos.Precio * detalle.Cantidad;
                    detalle.Productos.Stock -= detalle.Facturas.Cantidad;
                }
                
                paso = contexto.SaveChanges() > 0;

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return paso;
        }

        public static bool Modificar(Facturas facturas)
        {
            bool paso = false;
            Contexto contexto = new Contexto();

            try
            {
                var facturaAnterior = contexto.Facturas
                     .Where(x => x.FacturaId == facturas.FacturaId)
                     .Include(x => x.Detalle)
                     .ThenInclude(x => x.Productos)
                     .AsNoTracking()
                     .SingleOrDefault();

                contexto.Database.ExecuteSqlRaw($"Delete FROM FacturasDetalles where FacturaId = {facturas.FacturaId}");

                foreach (var item in facturas.Detalle)
                {
                    contexto.Entry(item).State = EntityState.Added;
                    contexto.Entry(item.Productos).State = EntityState.Modified;

                }

                contexto.Entry(facturas).State = EntityState.Modified;
                paso = contexto.SaveChanges() > 0;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return paso;
        }

        public static bool Guardar(Facturas facturas)
        {
            if (!Existe(facturas.FacturaId))
                return Insertar(facturas);
            else
                return Modificar(facturas);
        }

        public static Facturas Buscar(int id)
        {
            Contexto contexto = new Contexto();
            Facturas facturas = new Facturas();

            try
            {
                facturas = contexto.Facturas.Include(x => x.Detalle)
                    .Where(x => x.FacturaId == id)
                    .Include(x => x.Detalle)
                    .ThenInclude(x => x.Productos)
                    .SingleOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return facturas;
        }

        public static bool Eliminar(int id)
        {
            bool paso = false;
            Contexto contexto = new Contexto();

            try
            {
                var facturas = contexto.Facturas.Find(id);

                foreach (var item in facturas.Detalle)
                {
                    contexto.Entry(item.Facturas).State = EntityState.Modified;
                    contexto.Entry(item.Productos).State = EntityState.Modified;
                }


                contexto.Entry(facturas).State = EntityState.Deleted;

                paso = contexto.SaveChanges() > 0;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return paso;
        }

        public static List<Facturas> GetList(Expression<Func<Facturas, bool>> criterio)
        {
            var lista = new List<Facturas>();
            Contexto contexto = new Contexto();

            try
            {
                lista = contexto.Facturas.Where(criterio).ToList();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return lista;
        }

        public static List<Facturas> GetFacturas()
        {
            List<Facturas> lista = new List<Facturas>();
            Contexto contexto = new Contexto();
            try
            {
                lista = contexto.Facturas.ToList();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return lista;
        }
    }
}

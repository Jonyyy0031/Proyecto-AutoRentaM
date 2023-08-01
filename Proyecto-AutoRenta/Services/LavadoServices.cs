using Microsoft.EntityFrameworkCore;
using Proyecto_AutoRenta.Context;
using Proyecto_AutoRenta.Entities;
using Proyecto_AutoRenta.Vistas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Proyecto_AutoRenta.Services
{
    public class LavadoServices
    {
        public void AddLavado(Lavadoo request)
        {
            try
            {
                if (request != null)
                {
                    using (var _context = new ApplicationDbContext())
                    {
                        Lavadoo res = new Lavadoo();
                        res.Estado = request.Estado;
                        res.FkVehiculos = request.FkVehiculos;

                        _context.lavados.Add(res);
                        _context.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Sucedio un error" + ex.Message);
            }
        }

        public void DeleteUserL(int id)
        {
            try
            {
                using (var _context = new ApplicationDbContext())
                {
                    Lavadoo lavado = _context.lavados.Find(id);

                    if (lavado != null)
                    {
                        _context.lavados.Remove(lavado);
                        _context.SaveChanges();
                        MessageBox.Show("El registro ha sido eliminado correctamente.");
                    }
                    else
                    {
                        MessageBox.Show("No se encontró ningún resgistro con el ID especificado.");
                    }
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Succedio un error" + ex.Message);
            }
        }


        public void UpdateLavado(Lavadoo request)
        {
            try
            {
                using (var _context = new ApplicationDbContext())
                {
                    Lavadoo lavadoo = new Lavadoo();

                    lavadoo = _context.lavados.Find(request.PkLavado);
                    lavadoo.Estado = request.Estado;
                    lavadoo.Vehiculos = request.Vehiculos;
                    
                    _context.Entry(lavadoo).State = EntityState.Modified;
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrio un error " + ex.Message);
            }
        }
         //----------------------
        public List<Lavadoo> GetLavado()
        {
            try
            {
                using (var _context = new ApplicationDbContext())
                {
                    List<Lavadoo> lavado = _context.lavados.Include(x => x.Vehiculos).ToList();

                    if (lavado.Count > 0)
                    {
                        return lavado;
                    }

                    return lavado;
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrio un error " + ex.Message);
            }
        }
        //----------------
        public List<Vehiculos> GetVehiculo()
        {
            try
            {
                using (var _context = new ApplicationDbContext())
                {
                    List<Vehiculos> vehiculos = _context.Vehiculo.ToList();
                    return vehiculos;
                }
            }
            catch (Exception ex)
            {

                throw new Exception("Ocurrio un error " + ex.Message);
            }
        }

    }
}

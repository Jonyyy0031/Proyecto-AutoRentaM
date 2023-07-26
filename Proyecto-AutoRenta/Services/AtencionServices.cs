using Microsoft.EntityFrameworkCore;
using Proyecto_AutoRenta.Context;
using Proyecto_AutoRenta.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Proyecto_AutoRenta.Services
{
    public class AtencionServices
    {
       
        public void AddUser(AtenCliente request)
        {
            try
            {
                if (request != null)
                {
                    using (var _context = new ApplicationDbContext())
                    {
                        AtenCliente res = new AtenCliente();
                        res.NombreCliente = request.NombreCliente;
                        res.ApellidoCliente = request.ApellidoCliente;
                        res.Descripcion = request.Descripcion;
                        res.Estatus = request.Estatus;

                        _context.AtencionAlCliente.Add(res);
                        _context.SaveChanges();
                    }
                }


            }
            catch (Exception ex)
            {
                throw new Exception("Sucedio un error" + ex.Message);
            }
        }

        public void DeleteCliente(int id)
        {
            try
            {
                using (var _context = new ApplicationDbContext())
                {
                    AtenCliente atencion = _context.AtencionAlCliente.Find(id);

                    if (atencion != null)
                    {
                        _context.Entry(atencion).State = EntityState.Deleted;
                        _context.SaveChanges();
                        MessageBox.Show("El usuario ha sido eliminado correctamente.");
                    }
                    else
                    {
                        MessageBox.Show("No se encontró ningún usuario con el ID especificado.");
                    }
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Succedio un error" + ex.Message);
            }
        }

        public List<AtenCliente> GetAtencionCliente()
        {
            try
            {
                using (var _context = new ApplicationDbContext())
                {
                    List<AtenCliente> atencion = _context.AtencionAlCliente.ToList();

                    if (atencion.Count > 0)
                    {

                        return atencion;

                    }

                    return atencion;
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrio un error " + ex.Message);
            }
        }

        public void UpdateUserAtencion(AtenCliente request)
        {
            try
            {
                using (var _context = new ApplicationDbContext())
                {
                    AtenCliente atencion = new AtenCliente();
                    atencion = _context.AtencionAlCliente.Find(request.IDTicket);
                    atencion.NombreCliente = request.NombreCliente;
                    atencion.ApellidoCliente = request.ApellidoCliente;
                    atencion.Descripcion = request.Descripcion;
                    atencion.Estatus=request.Estatus;
                   
                    _context.Entry(atencion).State = EntityState.Modified;
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {

                throw new Exception("Ocurrio un error " + ex.Message);
            }
        }

       
        

    }
}

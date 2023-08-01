﻿using Proyecto_AutoRenta.Context;
using Proyecto_AutoRenta.Entities;
using Proyecto_AutoRenta.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Proyecto_AutoRenta.Vistas
{
    /// <summary>
    /// Lógica de interacción para Reserva.xaml
    /// </summary>
    public partial class Reserva : Window
    {
        public Reserva()
        {
            InitializeComponent();
            GetrenTable();
            GetVehiculos();
            GetUser();
            if (App.UsuarioAutenticado != null)
            {
                Usuario usuarioAutenticado = App.UsuarioAutenticado;
                MostrarBotonSegunRol(usuarioAutenticado);
            }
        }

        Reserve reserve = new Reserve();
        ReservaServices services = new ReservaServices();

        private void MostrarBotonSegunRol(Usuario usuario)
        {
            // Verificar si el usuario es "SuperAdmin" y mostrar u ocultar el botón según el rol.
            if (usuario.Roles != null && usuario.Roles.Nombre == "SuperAdmin")
            {
                btngobackadmin.Visibility = Visibility.Visible;
            }
            else
            {
                btngobackadmin.Visibility = Visibility.Collapsed;
            }
        }

        private void btnReserva_Click(object sender, RoutedEventArgs e)
        {
            
                DateTime fechaSalida = datePickerSalida.SelectedDate.HasValue ? datePickerSalida.SelectedDate.Value : DateTime.MinValue;
                DateTime fechaRegreso = datePickerRegreso.SelectedDate.HasValue ? datePickerRegreso.SelectedDate.Value : DateTime.MinValue;
                int ID;
            if (string.IsNullOrWhiteSpace(txtNombre.Text) || string.IsNullOrWhiteSpace(txtCorreo.Text) || string.IsNullOrWhiteSpace(txtTelefono.Text) ||
            SelectVehiculo.SelectedItem == null || SelectUser.SelectedItem == null || fechaSalida == DateTime.MinValue || fechaRegreso == DateTime.MinValue)
            {
                MessageBox.Show("Por favor, completa todos los campos obligatorios.");
                return;
            }
            if (fechaSalida < DateTime.Today || fechaRegreso < DateTime.Today)
            {
                MessageBox.Show("Las fechas de salida y regreso no pueden ser anteriores al día de hoy.");
                return;
            }
            if (int.TryParse(txtPkReserva_.Text, out ID))
            {

                Reserve reserve = new Reserve();
                reserve.PkReserva = ID;
                reserve.Nombre = txtNombre.Text;
                reserve.Correo = txtCorreo.Text;
                reserve.Telefono = txtTelefono.Text;
                reserve.FechaSalida = fechaSalida;
                reserve.FechaRegreso = fechaRegreso;

                Vehiculos seleccionado = SelectVehiculo.SelectedItem as Vehiculos;
                reserve.FkVehiculos = seleccionado.PkVehiculo;

                Usuario seleccionadoo = SelectUser.SelectedItem as Usuario;
                reserve.FkUsuario = seleccionadoo.PkUsuario;


                services.Updatereser(reserve);

                txtPkReserva_.Clear();
                txtCorreo.Clear();
                txtTelefono.Clear();
                datePickerSalida.SelectedDate = null;
                datePickerRegreso.SelectedDate = null;

                MessageBox.Show("Reserva Actualizada");
                GetrenTable();
                GetVehiculos();
                GetUser();

            }

            else
            {
                reserve.Nombre = txtNombre.Text;
                reserve.Correo = txtCorreo.Text;
                reserve.Telefono = txtTelefono.Text;
                reserve.FkVehiculos = int.Parse(SelectVehiculo.SelectedValue.ToString());
                reserve.FkUsuario = int.Parse(SelectUser.SelectedValue.ToString());
                reserve.FechaSalida = fechaSalida;
                reserve.FechaRegreso = fechaRegreso;
                services.Addreser(reserve);
                txtPkReserva_.Clear();
                txtNombre.Clear();
                txtCorreo.Clear();
                txtTelefono.Clear();
                datePickerSalida.SelectedDate = null;
                datePickerRegreso.SelectedDate = null;
                MessageBox.Show("Reserva agregada");
                GetrenTable();
                GetVehiculos();
                GetUser();
            }
        }

        private void DeleteItem(object sender, RoutedEventArgs e)
        {
            Reserve reserve = new Reserve();
            reserve = (sender as FrameworkElement).DataContext as Reserve;
            int ID = int.Parse(reserve.PkReserva.ToString());
            services.Deletereser(ID);
            txtPkReserva_.Clear();
            txtNombre.Clear();
            txtCorreo.Clear();
            txtTelefono.Clear();
            datePickerSalida.SelectedDate = null;
            datePickerRegreso.SelectedDate = null;
            GetrenTable();
            GetVehiculos();
            GetUser();
        }

        private void EditItem(object sender, RoutedEventArgs e)
        {
            Reserve reserve = new Reserve();
            reserve = (sender as FrameworkElement).DataContext as Reserve;

            txtPkReserva_.Text = reserve.PkReserva.ToString();
            txtNombre.Text = reserve.Nombre.ToString();
            txtCorreo.Text = reserve.Correo.ToString();
            txtTelefono.Text = reserve.Telefono.ToString();
            datePickerSalida.SelectedDate = reserve.FechaSalida;
            datePickerRegreso.SelectedDate = reserve.FechaRegreso;
        }

        public void GetrenTable()
        {
            UserTable.ItemsSource = services.GetReserva();
        }

        public void GetVehiculos()
        {
            SelectVehiculo.ItemsSource = services.GetVehiculo();
            SelectVehiculo.DisplayMemberPath = "Modelo";
            SelectVehiculo.SelectedValuePath = "PkVehiculo";
        }
        public void GetUser()
        {
            SelectUser.ItemsSource = services.GetUsuario();
            SelectUser.DisplayMemberPath = "Nombre";
            SelectUser.SelectedValuePath = "PkUsuario";
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btngoback_Click(object sender, RoutedEventArgs e)
        {
            Login login = new Login();

            login.Show();
            this.Close();
        }

        private void btngobackadmin_Click(object sender, RoutedEventArgs e)
        {
            VistaSuperAdmin StartViewSA = new VistaSuperAdmin();
            this.Close();
            StartViewSA.Show();
        }
    }
}

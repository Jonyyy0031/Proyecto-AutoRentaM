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
    /// Lógica de interacción para AtencionCliente.xaml
    /// </summary>
    public partial class AtencionCliente : Window
    {
        public AtencionCliente()
        {
            InitializeComponent();
            GetUserTableA();
            CargarEstatus();
            if (App.UsuarioAutenticado != null)
            {
                Usuario usuarioAutenticado = App.UsuarioAutenticado;
                MostrarBotonSegunRol(usuarioAutenticado);
            }


        }
        AtenCliente atencionc = new AtenCliente();
        AtencionServices servicesA = new AtencionServices();

        private void MostrarBotonSegunRol(Usuario usuario)
        {
            // Verificar si el usuario es "SuperAdmin" y mostrar u ocultar el botón según el rol.
            if (usuario.Roles != null && usuario.Roles.Nombre == "SuperAdmin")
            {
                btngobackadmin.Visibility = Visibility.Visible;
                btngoback.Visibility = Visibility.Collapsed; 
            }
            else
            {
                btngobackadmin.Visibility = Visibility.Collapsed;
            }
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {

            if (txtIDTicket.Text == "")
            {
                atencionc.NombreCliente = txtNCliente.Text;
                atencionc.ApellidoCliente = txtACliente.Text;
                atencionc.Descripcion = txtDescripcion.Text;

                atencionc.Estatus = SelecEstatus.Text;

                servicesA.AddUser(atencionc);

                txtNCliente.Clear();
                txtACliente.Clear();
                txtDescripcion.Clear();
                SelecEstatus.SelectedIndex = -1;


                MessageBox.Show("Se creo el ticket correctamente");
                GetUserTableA();
            }

            else if (txtIDTicket.Text != "")
            {
                int id = int.Parse(txtIDTicket.Text);
                atencionc.IDTicket = id;
                atencionc.NombreCliente = txtNCliente.Text;
                atencionc.ApellidoCliente = txtACliente.Text;
                atencionc.Descripcion = txtDescripcion.Text;
                atencionc.Estatus = SelecEstatus.Text;
                servicesA.UpdateUserAtencion(atencionc);

                MessageBox.Show("¡Se modifico el ticket correctamente!");
                txtNCliente.Clear();
                txtACliente.Clear();
                txtIDTicket.Clear();

                txtDescripcion.Clear();
                SelecEstatus.SelectedIndex = -1;
                GetUserTableA();

                LTicket.Visibility = Visibility.Collapsed;
                txtIDTicket.Visibility = Visibility.Collapsed;
            }


        }

        private void CargarEstatus()
        {

            List<string> Estatus = new List<string>
        {
            "Activo",
            "Cerrado"
        };

            SelecEstatus.ItemsSource = Estatus;

        }


        public void GetUserTableA()
        {
            UserTable.ItemsSource = servicesA.GetAtencionCliente();
        }


        private void EditItem(object sender, RoutedEventArgs e)
        {
            if (txtIDTicket.Visibility == Visibility.Collapsed && LTicket.Visibility == Visibility.Collapsed)
            {
                LTicket.Visibility = Visibility.Visible;
                txtIDTicket.Visibility = Visibility.Visible;
               


                atencionc = (sender as FrameworkElement).DataContext as AtenCliente;

                txtIDTicket.Text = atencionc.IDTicket.ToString();
                txtNCliente.Text = atencionc.NombreCliente.ToString();
                txtACliente.Text = atencionc.ApellidoCliente.ToString();
                txtDescripcion.Text = atencionc.Descripcion.ToString();
                SelecEstatus.Text = atencionc.Descripcion.ToString();


            }
            else
            {
                LTicket.Visibility = Visibility.Collapsed;
                txtIDTicket.Visibility = Visibility.Collapsed;
            }

        }
        public void DeleteItem(object sender, RoutedEventArgs e)
        {
            atencionc = (sender as FrameworkElement).DataContext as AtenCliente;
            int ID = int.Parse(atencionc.IDTicket.ToString());
            servicesA.DeleteCliente(ID);
            GetUserTableA();
        }


        private void btngoback_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            Login StartLogin = new Login();
            StartLogin.Show();
        }

        private void BtnMinimizar_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void BtnCerrar_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void btngobackadmin_Click(object sender, RoutedEventArgs e)
        {
            VistaSuperAdmin StartViewSA = new VistaSuperAdmin();
            this.Close();
            StartViewSA.Show();
        }
    }
}

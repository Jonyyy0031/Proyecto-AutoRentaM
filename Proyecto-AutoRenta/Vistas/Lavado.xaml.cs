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
    /// Lógica de interacción para Lavado.xaml
    /// </summary>
    public partial class Lavado : Window
    {
        Lavadoo lavado = new Lavadoo();
        LavadoServices Services = new LavadoServices();

        public Lavado()
        {
            InitializeComponent();
            GetVehiculos();
            GetUserTable();
            if (App.UsuarioAutenticado != null)
            {
                Usuario usuarioAutenticado = App.UsuarioAutenticado;
                MostrarBotonSegunRol(usuarioAutenticado);
            }
        }

        private void MostrarBotonSegunRol(Usuario usuario)
        {
            // Verificar si el usuario es "SuperAdmin" y mostrar u ocultar el botón según el rol.
            if (usuario.Roles != null && usuario.Roles.Nombre == "SuperAdmin")
            {
                btnFlechaIzquierdaadmin.Visibility = Visibility.Visible;
            }
            else
            {
                btnFlechaIzquierdaadmin.Visibility = Visibility.Collapsed;
            }
        }
        private void BtnAgregar_Click(object sender, RoutedEventArgs e)
        {
            int ID;
            if (int.TryParse(txtPkLavado.Text, out ID))
            {
                Lavadoo lavado = new Lavadoo();

                lavado.PkLavado = ID;
                lavado.Estado = txtEstado.Text;
                Services.UpdateReporte(lavado);

                MessageBox.Show("Reporte actualizado");
                GetUserTable();
                GetVehiculos();
            }
            else
            {
                lavado.Estado = txtEstado.Text;
                lavado.FkVehiculos = int.Parse(SelectVehiculo.SelectedValue.ToString());
                Services.AddReporte(lavado);

                txtEstado.Clear();

                MessageBox.Show("Reporte creado correctamente");
                GetUserTable();
                GetVehiculos();
            }
        }
        private void DeleteItem(object sender, RoutedEventArgs e)
        {
            Lavadoo lavadoo = new Lavadoo();
            lavadoo = (sender as FrameworkElement).DataContext as Lavadoo;
            int ID = int.Parse(lavadoo.PkLavado.ToString());
            Services.DeleteReporte(ID);
            GetUserTable();
            GetVehiculos();

        }
        private void EditItem(object sender, RoutedEventArgs e)
        {
            Lavadoo lavadoo = new Lavadoo();
            lavadoo = (sender as FrameworkElement).DataContext as Lavadoo;
            txtPkLavado.Text = lavadoo.PkLavado.ToString();
            txtEstado.Text = lavadoo.Estado.ToString();

        }
        public void GetUserTable()
        {
            UserTable.ItemsSource = Services.GetLavado();
        }

        public void GetVehiculos()
        {
            SelectVehiculo.ItemsSource = Services.GetVehiculo();
            SelectVehiculo.DisplayMemberPath = "Modelo";
            SelectVehiculo.SelectedValuePath = "PkVehiculo";
        }
        private void BtnMinimizar_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void BtnCerrar_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        public void BtnFlechaIzquierda_Click(object sender, RoutedEventArgs e)
        {
            Login StartLogin = new Login();
            this.Close();
            StartLogin.Show();
        }
        private void btnFlechaIzquierdaadmin_Click(object sender, RoutedEventArgs e)
        {
            VistaSuperAdmin StartViewSA = new VistaSuperAdmin();
            this.Close();
            StartViewSA.Show();
        }
    }
}

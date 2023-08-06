using Microsoft.EntityFrameworkCore;
using Proyecto_AutoRenta.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using Proyecto_AutoRenta.Services;

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
            

            
        }
        AtenCliente atencionc=new AtenCliente();
        AtencionServices servicesA = new AtencionServices();
        


        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {

            if (string.IsNullOrEmpty(txtNCliente.Text) || string.IsNullOrEmpty(txtACliente.Text) || string.IsNullOrEmpty(txtDescripcion.Text) || SelecEstatus.SelectedIndex == -1)
            {
                // Mostrar un mensaje de advertencia si alguno de los campos requeridos está vacío.
                MessageBox.Show("Por favor, complete todos los campos antes de agregar o editar el ticket.");
            }
            else
            {
                if (txtIDTicket.Text == "")
                {
                    // Código para agregar un nuevo ticket...
                    atencionc.NombreCliente = txtNCliente.Text;
                    atencionc.ApellidoCliente = txtACliente.Text;
                    atencionc.Descripcion = txtDescripcion.Text;
                    atencionc.Estatus = SelecEstatus.Text;

                    servicesA.AddTicket(atencionc);

                    txtNCliente.Clear();
                    txtACliente.Clear();
                    txtDescripcion.Clear();
                    SelecEstatus.SelectedIndex = -1;

                    // Notificar al usuario que el ticket se creó correctamente.
                    MessageBox.Show("Se creó el ticket correctamente");
                    GetUserTableA();
                }
                else
                {
                    // Código para editar un ticket existente...
                    int id = int.Parse(txtIDTicket.Text);
                    atencionc.IDTicket = id;
                    atencionc.NombreCliente = txtNCliente.Text;
                    atencionc.ApellidoCliente = txtACliente.Text;
                    atencionc.Descripcion = txtDescripcion.Text;
                    atencionc.Estatus = SelecEstatus.Text;

                    servicesA.UpdateUserAtencion(atencionc);

                    // Notificar al usuario que el ticket se modificó correctamente.
                    MessageBox.Show("¡Se modificó el ticket correctamente!");
                    txtNCliente.Clear();
                    txtACliente.Clear();
                    txtDescripcion.Clear();
                    SelecEstatus.SelectedIndex = -1;
                    GetUserTableA();
                }
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
            if (txtIDTicket.Visibility == Visibility.Collapsed )
            {

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
            VistaSuperAdmin StartViewSA = new VistaSuperAdmin();
            StartViewSA.Show();
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

        
    }

}

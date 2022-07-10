using SistemaFacturacion.BLL;
using SistemaFacturacion.Entidades;
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

namespace SistemaFacturacion.UI.Registros
{
    /// <summary>
    /// Interaction logic for rClientes.xaml
    /// </summary>
    public partial class rClientes : Window
    {
        private Clientes cliente = new Clientes();
        public rClientes()
        {
            InitializeComponent();
            this.DataContext = cliente;
        }

        private void Limpiar()
        {
            this.cliente = new Clientes();
            this.DataContext = cliente;
        }

        private void BuscarButton_Click(object sender, RoutedEventArgs e)
        {
            var cliente = ClientesBLL.Buscar(Utilidades.ToInt(ClienteIdtbox.Text));

            if (cliente != null)
            {
                this.cliente = cliente;
            }
            else
            {
                this.cliente = new Clientes();
                MessageBox.Show("No fue encontrado", "No encontrado",
                   MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            this.DataContext = this.cliente;
        }

        private void NuevoButton_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
        }

        private void GuardarButton_Click(object sender, RoutedEventArgs e)
        {
            var paso = ClientesBLL.Guardar(cliente);

            if (paso)
            {
                Limpiar();
                MessageBox.Show("Guardado con exito", "Exito",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("No se pudo guardar", "Fallo",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void EliminarButton_Click(object sender, RoutedEventArgs e)
        {
            if (ClientesBLL.Eliminar(Utilidades.ToInt(ClienteIdtbox.Text)))
            {
                Limpiar();
                MessageBox.Show("Registro Eliminado", "Exito",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("No fue posible Eliminar", "Fallo",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}

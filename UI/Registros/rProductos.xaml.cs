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
    /// Interaction logic for rProductos.xaml
    /// </summary>
    public partial class rProductos : Window
    {
        private Productos producto = new Productos();
        public rProductos()
        {
            InitializeComponent();
            this.DataContext = producto;
        }

        private void Limpiar()
        {
            this.producto = new Productos();
            this.DataContext = producto;
        }

        private void BuscarButton_Click(object sender, RoutedEventArgs e)
        {
            var producto = ProductosBLL.Buscar(Utilidades.ToInt(ProductoIdtbox.Text));

            if (producto != null)
            {
                this.producto = producto;
            }
            else
            {
                this.producto = new Productos();

                MessageBox.Show("No fue encontrado", "No encontrado",
                   MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            this.DataContext = this.producto;
        }

        private void NuevoButton_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
        }

        private void GuardarButton_Click(object sender, RoutedEventArgs e)
        {
            var paso = ProductosBLL.Guardar(producto);

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
            if (ProductosBLL.Eliminar(Utilidades.ToInt(ProductoIdtbox.Text)))
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

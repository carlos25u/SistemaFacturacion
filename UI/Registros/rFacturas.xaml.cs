using SistemaFacturacion.BLL;
using SistemaFacturacion.Entidades;
using SistemaFacturacion.UI.Consultas;
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
    /// Interaction logic for rFacturas.xaml
    /// </summary>
    public partial class rFacturas : Window
    {
        private Facturas factura = new Facturas();
        public rFacturas()
        {
            InitializeComponent();
            this.DataContext = factura;

            ClienteComboxbox.ItemsSource = ClientesBLL.GetClientes();
            ClienteComboxbox.SelectedValuePath = "ClienteId";
            ClienteComboxbox.DisplayMemberPath = "Nombres";

            ProductoComboxbox.ItemsSource = ProductosBLL.GetProductos();
            ProductoComboxbox.SelectedValuePath = "ProductoId";
            ProductoComboxbox.DisplayMemberPath = "Nombre";
        }

        private void Cargar()
        {
            this.DataContext = null;
            this.DataContext = factura;
        }

        private void Limpiar()
        {
            this.factura = new Facturas();
            this.DataContext = factura;
        }

        private void BuscarButton_Click(object sender, RoutedEventArgs e)
        {
            Facturas encontrado = FacturasBLL.Buscar(Utilidades.ToInt(FacturaId.Text));

            if (encontrado != null)
            {
                factura = encontrado;
                Cargar();
            }
            else
            {
                Limpiar();
                MessageBox.Show("La facrura no existe", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AgregarButton_Click(object sender, RoutedEventArgs e)
        {
            factura.Detalle.Add(new FacturasDetalles
            {
                FacturaId = factura.FacturaId,
                Facturas = factura,
                Productos = (Productos)ProductoComboxbox.SelectedItem,
                Cantidad = Utilidades.ToInt(Cantidadtbox.Text)
            });

            Productos productos = (Productos)ProductoComboxbox.SelectedItem;

            int total = 0;
            total += Utilidades.ToInt(Cantidadtbox.Text) * productos.Precio;

            Cargar();
            TotalTextBox.Text = total.ToString();
        }

        private void RemoverButton_Click(object sender, RoutedEventArgs e)
        {
            if (DetalleDataGrid.Items.Count >= 1 && DetalleDataGrid.SelectedIndex <= DetalleDataGrid.Items.Count - 1)
            {
                factura.Detalle.RemoveAt(DetalleDataGrid.SelectedIndex);
                Cargar();
            }
        }

        private void ImprimirButton_Click(object sender, RoutedEventArgs e)
        {
            new vFacturas().Show();
        }

        private void NuevoButton_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
        }

        private void GuardarButton_Click(object sender, RoutedEventArgs e)
        {
            var paso = FacturasBLL.Guardar(factura);

            if (paso)
            {
                Limpiar();
                MessageBox.Show("Guardado con exito", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("No se pudo guardar", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void EliminarButton_Click(object sender, RoutedEventArgs e)
        {
            if (FacturasBLL.Eliminar(Utilidades.ToInt(FacturaId.Text)))
            {
                MessageBox.Show("Se elimino con exito", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("No fue posible Eliminar", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}

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

namespace SistemaFacturacion.UI.Consultas
{
    /// <summary>
    /// Interaction logic for Inventario.xaml
    /// </summary>
    public partial class Inventario : Window
    {
        public Inventario()
        {
            InitializeComponent();
        }

        private void BuscarButton_Click(object sender, RoutedEventArgs e)
        {
            var listado = new List<Productos>();
            if (CriterioTextBox.Text.Trim().Length > 0)
            {
                switch (FiltroComboBox.SelectedIndex)
                {
                    case 0: //Nombre
                        listado = ProductosBLL.GetList(e => e.ProductoId == Utilidades.ToInt(CriterioTextBox.Text));
                        break;
                    case 1: //Apellido
                        listado = ProductosBLL.GetList(e => e.Nombre.ToLower().Contains(CriterioTextBox.Text.ToLower()));
                        break;
                    case 2: //NombreUsuario
                        listado = ProductosBLL.GetList(e => e.Descripcion.ToLower().Contains(CriterioTextBox.Text.ToLower()));
                        break;
                }
            }
            else
            {
                listado = ProductosBLL.GetList(e => true);

            }

            DatosDataGrid.ItemsSource = null;
            DatosDataGrid.ItemsSource = listado;
        }
    }
}

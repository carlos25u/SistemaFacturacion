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
    /// Interaction logic for vFacturas.xaml
    /// </summary>
    public partial class vFacturas : Window
    {
        public vFacturas()
        {
            InitializeComponent();
        }

        private void BuscarButton_Click(object sender, RoutedEventArgs e)
        {
            var listado = new List<Facturas>();
            if (CriterioTextBox.Text.Trim().Length > 0)
            {
                switch (FiltroComboBox.SelectedIndex)
                {
                    case 0: 
                        listado = FacturasBLL.GetList(e => e.FacturaId == Utilidades.ToInt(CriterioTextBox.Text));
                        break;
                }
            }
            else
            {
                listado = FacturasBLL.GetList(e => true);

            }

            DatosDataGrid.ItemsSource = null;
            DatosDataGrid.ItemsSource = listado;
        }
    }
}

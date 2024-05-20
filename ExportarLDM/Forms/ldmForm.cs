using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EPDM.Interop.epdm;
using ExportarLDM.Utilities;


// -TODO Se recomienda utilizar bloques try-catch para manejar posibles excepciones.
// -TODO Se recomienda no utilizar los nombres predeterminados de los controles, ya que puede dificultar la comprensión del código.
// -TODO Se recomienda utilizar comentarios para explicar el propósito de las clases y métodos.

namespace ExportarLDM.Forms
{
    public partial class ldmForm : Form
    {
        IEdmBomView3 bomView = null;
        IEdmFile7 afile = null;

        // Constructor para el formulario
        public ldmForm(EdmBomLayout2[] bomLayouts, IEdmFile7 file)
        {  
            // -TODO Falta habilitar/deshabilitar los botones según corresponda
            InitializeComponent();
            afile = file;      
            
            // Lleno un combobox con todas las ldm obtenidas
            foreach (var layout in bomLayouts)
            {
                comboBoxBoms.Items.Add(layout.mbsLayoutName);
            }            
            comboBoxBoms.SelectedIndexChanged += ComboBoxBoms_SelectedIndexChanged;
            // Desactiva el boton Exportar al inicio
            buttonExportar.Enabled = false;
        }

        //Metedo que se ejecuta cuando se cambia la seleccion del combobox
        private void ComboBoxBoms_SelectedIndexChanged(object sender, EventArgs e)
        {
            string messageError = "";
            string messageOk = "";
            // Verifico si hay un item seleccionado en el combobox y habilito el boton Exportar segun corresponda
            bool itemSeleccionado = comboBoxBoms.SelectedItem != null;            
            buttonExportar.Enabled = itemSeleccionado;
            try
            {
                // -TODO Si no se selecciona ningún ítem en el ComboBox, bomView será null y no se está tomando en cuenta en el método button1_Click.
                if (itemSeleccionado)
                {
                //Obtengo la ldm seleccionada por medio de su nombre
                object ldmToExport = comboBoxBoms.SelectedItem;
                
                    bomView = (IEdmBomView3)afile.GetComputedBOM(ldmToExport, 0, "", (int)EdmBomFlag.EdmBf_AsBuilt + (int)EdmBomFlag.EdmBf_ShowSelected);  
                    if(bomView == null)
                    {
                        messageError = $"Error al obtener la lista de materiales seleccionada {Environment.NewLine}";
                    }
                    else
                    {
                        messageOk = $"Lista de materiales obtenida correctamente: {ldmToExport} {Environment.NewLine}";
                        Errors.ShowMessage(messageOk);    
                    }
                }
            }            
            catch (Exception ex)
            {
                MessageBox.Show(messageError, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Errors.ShowError(ex);
            }
        }

        //Metodo que se ejecuta cuando se hace click en el boton Exportar
        private void buttonExportar_Click(object sender, EventArgs e)
        {
            string messageError = "";
            string messageOk = "";
            // Guardo la ldm en la ubicacion que desee el usuario
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Archivos CSV (*.csv)|*.csv";
            saveFileDialog.FileName = Path.GetFileNameWithoutExtension(afile.Name) + "BOM.csv";

            DialogResult result = saveFileDialog.ShowDialog();

            if (result == DialogResult.OK)
            {   
                // -TODO Se usa bomView sin verificar si es null, lo que podría causar una excepción si es null.
                try
                {
                    if (bomView != null)
                    {
                        bomView.SaveToCSV(saveFileDialog.FileName, true);
                        messageOk = $"La lista de materiales se guardó correctamente en: {saveFileDialog.FileName} {Environment.NewLine}";
                        MessageBox.Show(messageOk);
                        Errors.ShowMessage(messageOk);
                    }else messageError = $"Error al guardar la lista de materiales {Environment.NewLine}";
                }
                catch (Exception ex)
                {
                    MessageBox.Show(messageError, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Errors.ShowError(ex);
                }
            }
        }
        //Metodo para cerrar formulario
        private void buttonClose_Click(object sender, EventArgs e)
        {
            // -TODO Verificar que en release no cierre el explorer o la utilización del add-in. En caso afirmativo cambiar a this.Close().
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cerrar la aplicación {Environment.NewLine}");
                Errors.ShowError(ex);
            }
        }        
    }
}

﻿using System;
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

namespace ExportarLDM.Forms
{
    public partial class ldmForm : Form
    {
        IEdmBomView3 bomView = null;
        IEdmFile7 afile = null;
        public ldmForm(EdmBomLayout2[] ppoRetLayouts, IEdmFile7 file)
        {
            InitializeComponent();
            afile = file;
            //Lleno un combobox con todas las ldm obtenidas
            foreach (var ldm in ppoRetLayouts)
            {
                comboBox1.Items.Add(ldm.mbsLayoutName);
            }
            comboBox1.SelectedIndexChanged += ComboBox1_SelectedIndexChanged;
        }
        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Obtengo la ldm seleccionada por medio de su nombre
            object ldmToExport = comboBox1.SelectedItem;
            if (ldmToExport != null)
            {
                bomView = (IEdmBomView3)afile.GetComputedBOM(ldmToExport, 0, "", (int)EdmBomFlag.EdmBf_AsBuilt + (int)EdmBomFlag.EdmBf_ShowSelected);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Guardo la ldm en la ubicacion que desee el usuario
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Archivos CSV (*.csv)|*.csv";
            saveFileDialog.FileName = Path.GetFileNameWithoutExtension(afile.Name) + "BOM.csv";

            DialogResult result = saveFileDialog.ShowDialog();

            if (result == DialogResult.OK)
            {             
                bomView.SaveToCSV(saveFileDialog.FileName, true);
                MessageBox.Show("La lista de materiales se guardo correctamente en: " + saveFileDialog.FileName);                
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
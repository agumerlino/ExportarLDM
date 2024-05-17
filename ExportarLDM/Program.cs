using EPDM.Interop.epdm;
using ExportarLDM.Forms;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExportarLDM.Clases
{
    // Los console.writeline se van a agregar a un metodo posteriormente


    // -TODO Se sugiere modularizar el código para mejorar la mantenibilidad y la legibilidad.
    // -TODO Se recomienda dividir el código en componentes más pequeños y especializados.
    // -TODO Por ejemplo, se podría crear una clase para manejar la lógica de exportación de la lista de materiales.
    // -TODO También se podría crear una clase para manejar la lógica de interacción con el usuario.
    // -TODO Además, acciones repetitivas como mostrar mensajes de error o loguear excepciones podrían ser encapsuladas en métodos.
    [ComVisible(true)]
    [Guid("3A601AFC-7007-46A7-9E71-D3BD41B5E2E2")]
    public class Program : IEdmAddIn5
    {        
        private BOMExporterController bomExporter;

        public void GetAddInInfo(ref EdmAddInInfo poInfo, IEdmVault5 poVault, IEdmCmdMgr5 poCmdMgr)
        {
            try
            {
                poInfo.mbsAddInName = "Exportar LDM";
                poInfo.mbsCompany = "Disegno Soft";
                poInfo.mbsDescription = "Exporta lista de materiales en archivo .csv";
                poInfo.mlAddInVersion = 1;
                poInfo.mlRequiredVersionMajor = 17;
                poInfo.mlRequiredVersionMinor = 0;
                poCmdMgr.AddHook(EdmCmdType.EdmCmd_CardButton);
            }
            // TODO Se recomienda loguear las excepciones para el desarrollador y proporcionar mensajes más amigables para el usuario final.
            
            catch (Exception ex)
            {
                MessageBox.Show($"Error en la carga del complemento","", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine($"Error: {ex.Message}");
            }            
        }   

        public void OnCmd(ref EdmCmd poCmd, ref EdmCmdData[] ppoData)
        {
            string message = "";
            try
            {
                IEdmVault11 vault = (IEdmVault11)poCmd.mpoVault;
                if (vault == null)
                {
                    message = $"No se pudo obtener el vault {Environment.NewLine}";
                }
            
                if (poCmd.meCmdType == EdmCmdType.EdmCmd_CardButton)
                {
                    //Obtengo el id del ensamblaje seleccionado
                    int idFile = ppoData[0].mlObjectID1;
                    if (vault == null)
                    {
                        message += $"No se pudo obtener el id del archivo {Environment.NewLine}";
                    }
                    //Envio el id del archivo y el vault a la clase BOMExporter 
                    bomExporter = new BOMExporterController(vault);
                    bomExporter.ExportBOM(idFile);                         
                }
            }
            // TODO Se recomienda loguear las excepciones para el desarrollador y proporcionar mensajes más amigables para el usuario final.
            catch (Exception ex)
            {
                MessageBox.Show(message, $"Error en la ejecucion del complemento", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine($"Error: {ex.Message}");
            }            
        }        
    }    
}

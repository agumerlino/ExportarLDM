using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EPDM.Interop.epdm;
using ExportarLDM.Forms;
using ExportarLDM.Utilities;

namespace ExportarLDM.Clases
{
    public class BOMExporterController
    {
        IEdmBomMgr2 bomMgr;
        IEdmVault11 vault;

        public BOMExporterController(IEdmVault11 vault)
        {          
            this.vault = vault;                   
        }

        //Metodo para abrir y enviar datos al formulario 
        public void ExportBOM(int idFile)
        {
            string messageError = "";
            string messageOk = "";
            try
            {
                //Obtengo el archivo por su id
                IEdmFile7 file = (IEdmFile7)vault.GetObject(EdmObjectType.EdmObject_File, idFile);
                if (file == null)
                {
                    messageError = $"No se pudo obtener el archivo {Environment.NewLine}";
                }
                else
                {
                    messageOk = $"Archivo obtenido correctamente: {file.Name}{Environment.NewLine}";
                    Errors.ShowMessage(messageOk);
                }

                //Obtengo todas las ldm que esten disponibles
                bomMgr = (IEdmBomMgr2)vault.CreateUtility(EdmUtility.EdmUtil_BomMgr);
                EdmBomLayout2[] ppoRetLayouts = null;
                bomMgr.GetBomLayouts2(out ppoRetLayouts);
                if (ppoRetLayouts == null)
                {
                    messageError += $"No se pudieron obtener listas de materiales {Environment.NewLine}";
                }
                else
                {
                    messageOk = $"Arreglo de listas de materiales obtenido correctamente: {ppoRetLayouts}{Environment.NewLine}";
                    Errors.ShowMessage(messageOk);
                }

                //Envio las ldm y el archivo a un formulario 
                ldmForm ldmForm = new ldmForm(ppoRetLayouts, file);
                ldmForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(messageError, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Errors.ShowError(ex);
            }
        }
    }
}

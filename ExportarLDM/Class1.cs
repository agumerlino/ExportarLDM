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

namespace ExportarLDM
{
    [ComVisible(true)]
    [Guid("3A601AFC-7007-46A7-9E71-D3BD41B5E2E2")]
    public class Class1 : IEdmAddIn5
    {        
        IEdmBomMgr2 bomMgr;        
        
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
            catch (COMException ex)
            {
                MessageBox.Show("HRESULT = 0x" + ex.ErrorCode.ToString("X") + " " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void OnCmd(ref EdmCmd poCmd, ref EdmCmdData[] ppoData)
        {           
            IEdmVault11 vault2 = (IEdmVault11)poCmd.mpoVault;
            try
            {
                if (poCmd.meCmdType == EdmCmdType.EdmCmd_CardButton)
                {   
                    //Obtengo el id del ensamblaje seleccionado
                    int idFile = ppoData[0].mlObjectID1;

                    //Obtengo el archivo por su id
                    IEdmFile7 file = (IEdmFile7)vault2.GetObject(EdmObjectType.EdmObject_File, idFile);         
                    
                    //Obtengo todas las ldm que esten disponibles
                    bomMgr = (IEdmBomMgr2)vault2.CreateUtility(EdmUtility.EdmUtil_BomMgr);
                    EdmBomLayout2[] ppoRetLayouts = null;                    
                    bomMgr.GetBomLayouts2(out ppoRetLayouts);               

                    //Envio las ldm y el archivo a un formulario 
                    ldmForm ldmForm = new ldmForm(ppoRetLayouts, file);
                    ldmForm.ShowDialog();          
                }
            }
            catch (COMException ex)
            {
                MessageBox.Show("HRESULT = 0x" + ex.ErrorCode.ToString("X") + " " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

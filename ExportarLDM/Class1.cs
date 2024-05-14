using EPDM.Interop.epdm;
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
            IEdmBomView3 bomView = null;
            IEdmVault11 vault2 = (IEdmVault11)poCmd.mpoVault;
            try
            {
                if (poCmd.meCmdType == EdmCmdType.EdmCmd_CardButton)
                {   
                    int idFile = ppoData[0].mlObjectID1;

                    IEdmFile7 file = (IEdmFile7)vault2.GetObject(EdmObjectType.EdmObject_File, idFile);                    
                    int arrSize = 0;                   
                    int i = 0;                                                       
                    
                    // Get a BOM view with the specified layout
                    bomMgr = (IEdmBomMgr2)vault2.CreateUtility(EdmUtility.EdmUtil_BomMgr);
                    EdmBomLayout2[] ppoRetLayouts = null;
                    EdmBomLayout2 ppoRetLayout = default;
                    bomMgr.GetBomLayouts2(out ppoRetLayouts);                    
                    arrSize = ppoRetLayouts.Length;

                    //De esta manera se especifica que ldm se va a exportar segun su nombre
                    while (i < arrSize)
                    {
                        ppoRetLayout = ppoRetLayouts[i];

                        if (ppoRetLayout.mbsLayoutName == "ldmPrueba")
                        {
                            bomView = (IEdmBomView3)file.GetComputedBOM(ppoRetLayout.mbsLayoutName, 0, "", (int)EdmBomFlag.EdmBf_AsBuilt + (int)EdmBomFlag.EdmBf_ShowSelected);
                        }
                        i++; ;
                    }

                    SaveFileDialog saveFileDialog = new SaveFileDialog();
                    saveFileDialog.Filter = "Archivos CSV (*.csv)|*.csv";
                    saveFileDialog.FileName = Path.GetFileNameWithoutExtension(file.Name) + "BOM.csv";

                    DialogResult result = saveFileDialog.ShowDialog();
                    
                    if(result == DialogResult.OK)
                    {
                        bomView.SaveToCSV(saveFileDialog.FileName, true);
                        MessageBox.Show("La lista de materiales se guardo correctamente en: " + saveFileDialog.FileName);
                    } 
                    //Metodo para modificar la ldm completa y que me muestre solo las columnas que pide el ejercicio (no esta terminado, ya que me cree una ldm con esas columnas
                    
                    //SaveCustomBOM(file, bomView, vault2);                    
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


        //public void SaveCustomBOM(IEdmFile7 file, IEdmBomView3 bomView, IEdmVault11 vault2)
        //{
        //    IEdmVariableMgr5 variableMgr = (IEdmVariableMgr5)vault2;
        //    int descripcionID = variableMgr.GetVariable("Description").ID;
        //    try
        //    {
        //        // Verificar que hay un archivo y una vista de BOM disponibles
        //        if (file != null && bomView != null)
        //        {
        //            // Obtener las filas de la vista de BOM
        //            object[] ppoRows = null;
        //            bomView.GetRows(out ppoRows);
        //            EdmBomColumn[] ppoColumns = null;
        //            bomView.GetColumns(out ppoColumns);

        //            List<(int treeLevel, string description, string quantity)> rowData = new List<(int, string, string)>();
        //            foreach (var row in ppoRows)
        //            {
        //                IEdmBomCell bomCell = (IEdmBomCell)row;

        //                // Obtener el nivel del árbol
        //                int treeLevel = bomCell.GetTreeLevel();
        //                // Obtener la descripción y cantidad (puedes ajustar esto según la estructura de tu BOM)
        //                bomCell.GetVar(descripcionID, ppoColumns[4].meType, out object poValue, out object poComputedValue, out string pbsConfiguration, out bool pbReadOnly);
        //                string description = (string)poValue;// Aquí deberías obtener la descripción del elemento
        //                bomCell.GetVar(0, ppoColumns[7].meType, out object poValue2, out object poComputedValue2, out string pbsConfiguration2, out bool pbReadOnly2);
        //                string quantity = (string)poValue2; // Aquí deberías obtener la cantidad del elemento
        //                rowData.Add((treeLevel, description, quantity));
        //            }
        //            rowData.Sort((x, y) => x.treeLevel.CompareTo(y.treeLevel));

        //            StringBuilder csvContent = new StringBuilder();

        //            // Agregar datos al CSV
        //            csvContent.AppendLine("Nivel,Descripcion,Cantidad de referencia");
        //            foreach (var row in rowData)
        //            {
        //                csvContent.AppendLine($"{row.treeLevel},{row.description},{row.quantity}");
        //            }
 
        //            // Guardar el CSV en un archivo 
        //            string filePath = "C:\\Users\\Disegno soft\\Desktop\\" + Path.GetFileNameWithoutExtension(file.Name) + "BOM.csv"; 
        //            System.IO.File.WriteAllText(filePath, csvContent.ToString());

        //            // Mostrar un mensaje de éxito
        //            MessageBox.Show("La lista de materiales se ha guardado correctamente en " + filePath);
        //        }
        //        else
        //        {
        //            MessageBox.Show("No hay archivo seleccionado o vista de lista de materiales disponible.");
        //        }
        //    }
        //    catch (System.Runtime.InteropServices.COMException ex)
        //    {
        //        MessageBox.Show("HRESULT = 0x" + ex.ErrorCode.ToString("X") + " " + ex.Message);
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}
    }
}

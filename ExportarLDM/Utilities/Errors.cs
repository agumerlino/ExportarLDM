using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExportarLDM.Utilities
{
    public static class Errors
    {
        //Metodo para mostrar por consola un error
        public static void ShowError(Exception ex)
        {
            ShowConsoleError(ex);
        }
        //Metodo para mostrar por consola un mensaje de que salio algo correctamente
        public static void ShowMessage(string message)
        {            
            ShowConsoleMessage(message);
        }

        private static void ShowConsoleMessage(string message)
        {
            Console.WriteLine(message);
        }
        private static void ShowConsoleError(Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}

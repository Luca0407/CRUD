using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Simple_Login_FORM
{
    static class Program
    {
        /// <summary>
        /// Point d'entrée principal de l'application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
			DBLoginForm loginForm = new DBLoginForm();
			if(loginForm.ShowDialog() == DialogResult.OK) {
				// Si se ingresaron datos válidos, abrir el resto de la app
				Application.Run(new LoginForm()); // o tu Form inicial
			} else {
				Application.Exit();
			}
		}
    }
}

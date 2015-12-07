using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Spell_Checker
{
    static class Program
    {
        /// <summary>
        /// main program
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new User_Interface());
        }
    }
}

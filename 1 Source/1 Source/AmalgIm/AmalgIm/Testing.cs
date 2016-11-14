/* Testing.cs
   Author: Mike Hurley
   This file serves as an initialization for the form 
   designed in TestPanel.cs.
*/

using System;
using System.Windows.Forms;

namespace AmalgIm
{
    public class Testing
    {
        [STAThread]
        static void Main()
        {
            //don't touch stuff below
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            TestPanel test = new TestPanel();
            //here i run methods to set up form display
            Application.Run(test);
        }
    }
}

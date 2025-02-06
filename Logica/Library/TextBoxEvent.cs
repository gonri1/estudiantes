using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Logica.Library
{
    public class TextBoxEvent
    {
        //Metodo que captura el texto presionado en el TextBox (e) y da permisos o no (e.Handled)
        public void TextKeyPress(KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar))//Si el caracter es una letra
            {
                e.Handled = false;//Permite escribir
            }
            else if (char.IsControl(e.KeyChar))//Si el caracter es un control
            {
                e.Handled = false;//Permite escribir
            }
            else if (char.IsSeparator(e.KeyChar))//Si el caracter es un espacio
            {
                e.Handled = false;//Permite escribir
            }
            else
            {
                e.Handled = true;//No permite escribir
            }
        }   

    }
}

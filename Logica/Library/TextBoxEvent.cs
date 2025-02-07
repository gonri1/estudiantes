using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Logica.Library
{
    public class TextBoxEvent
    {

        // METODOS QUE CAPTURAN EL TEXTO PRESIONADO EN EL TEXTBOX (E) Y DA PERMISOS O NO (E.HANDLED)

        // Metodo que permite escribir solo letras en un TextBox

        public void TextKeyPress(KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar)) // si el caracter es una letra
            {
                e.Handled = false; // permite escribir
            }
            else if (char.IsControl(e.KeyChar)) // si el caracter es un control
            {
                e.Handled = false; // permite escribir
            }
            else if (char.IsSeparator(e.KeyChar)) // si el caracter es un espacio
            {
                e.Handled = false; // permite escribir
            }
            else
            {
                e.Handled = true; // no permite escribir
            }
        }


        // Metodo que permite escribir solo números en un TextBox

        public void NumberKeyPress(KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar)) // si el caracter es un dígito
            {
                e.Handled = false; // permite escribir
            }
            else if (char.IsControl(e.KeyChar)) // si el caracter es un control
            {
                e.Handled = false; // permite escribir
            }
            else if (char.IsSeparator(e.KeyChar)) // si el caracter es un espacio
            {
                e.Handled = false; // permite escribir
            }
            else
            {
                e.Handled = true; // no permite escribir
            }
        }

        // Metodo que permite escribir textos tipo mail

        public void EmailKeyPress(KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar)) // si el caracter es una letra
            {
                e.Handled = false; // permite escribir
            }
            else if (char.IsDigit(e.KeyChar)) // si el caracter es un dígito
            {
                e.Handled = false; // permite escribir
            }
            else if (char.IsControl(e.KeyChar)) // si el caracter es un control
            {
                e.Handled = false; // permite escribir
            }
            else if (char.IsSeparator(e.KeyChar)) // si el caracter es un espacio
            {
                e.Handled = false; // permite escribir
            }
            else if (e.KeyChar == '@' || e.KeyChar == '.') // si el caracter es @ o .
            {
                e.Handled = false; // permite escribir
            }
            else
            {
                e.Handled = true; // no permite escribir
            }
        }


        //Método que permite validar un emmail

        public bool comprobarFormatoEmail(string formatoEmail)
        {
            return new EmailAddressAttribute().IsValid(formatoEmail);
        }
    }
}

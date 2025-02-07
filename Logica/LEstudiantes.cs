using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Logica.Library;//Referencia a la capa Logica para usar la clase Uploadimage

namespace Logica//Este nanespace coincide con el nombre de la capa
{
    public class LEstudiantes : LibreriaClases
    {
        private List<TextBox> listTextBox;
        private List<Label> listLabel;
        private PictureBox image;

        public LEstudiantes(List<TextBox> listTextBox, List<Label> listLabel, object[] objetos)
        {
            this.listTextBox = listTextBox;
            this.listLabel = listLabel;
            image = (PictureBox)objetos[0];
        }

        public void Registrar()//Metodo para registrar un estudiante, boton agregar
        {
            bool allFieldsFilled = true;

            // campos NID

            if (listTextBox[0].Text.Equals(""))
            {
                listLabel[0].Text = "rellene NID";
                listLabel[0].ForeColor = System.Drawing.Color.Red;
                listTextBox[0].Focus();
                allFieldsFilled = false;
            }
            else
            {
                listLabel[0].Text = "NID";
                listLabel[0].ForeColor = System.Drawing.Color.Green;
            }

            // campo Nombre

            if (listTextBox[1].Text.Equals(""))
            {
                listLabel[1].Text = "rellene Nombre";
                listLabel[1].ForeColor = System.Drawing.Color.Red;
                listTextBox[1].Focus();
                allFieldsFilled = false;
            }
            else
            {
                listLabel[1].Text = "Nombre";
                listLabel[1].ForeColor = System.Drawing.Color.Green;
            }

            // campo Apellido

            if (listTextBox[2].Text.Equals(""))
            {
                listLabel[2].Text = "rellene Apellido";
                listLabel[2].ForeColor = System.Drawing.Color.Red;
                listTextBox[2].Focus();
                allFieldsFilled = false;
            }
            else
            {
                listLabel[2].Text = "Apellido";
                listLabel[2].ForeColor = System.Drawing.Color.Green;
            }

            // campo Email

            if (listTextBox[3].Text.Equals(""))
            {
                listLabel[3].Text = "rellene Email";
                listLabel[3].ForeColor = System.Drawing.Color.Red;
                listTextBox[3].Focus();
                allFieldsFilled = false;
            }
            else
            {
                

                   var imageArray= uploadimage.ImageToByte(image.Image);
               
            }



            // Si todos los campos están llenos, realizar alguna acción


            if (allFieldsFilled)
            {
                
            }
        }
    }
}

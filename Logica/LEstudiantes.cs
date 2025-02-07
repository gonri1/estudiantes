using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Data;
using LinqToDB;
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

            if (listTextBox[3].Text.Equals(""))//Si no hay nada en el text box mail
            {
                listLabel[3].Text = "rellene Email";
                listLabel[3].ForeColor = System.Drawing.Color.Red;
                listTextBox[3].Focus();
                allFieldsFilled = false;
            }



            else //********* CUANDO TODOS LOS CAMPOS ESTEN BIEN HACEMOS EL INSERT DEL CRUD*******
            {

                //Insertamos las imagenes en un array, convirtiendolo a bytes

                var imageArray = uploadimage.ImageToByte(image.Image);


                // Insertamos los valores del formulario en el objeto _Estudiante

                _Estudiante.Value(e => e.nid, listTextBox[0].Text)
                .Value(e => e.nombre, listTextBox[1].Text)
                .Value(e => e.apellido, listTextBox[2].Text)
                .Value(e => e.email, listTextBox[3].Text).Insert();



            }



            // Si todos los campos están llenos, realizar alguna acción


            if (allFieldsFilled)
            {
                
            }
        }
    }
}

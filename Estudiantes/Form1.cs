using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Logica;//Referencia a la capa Logica para usar la clase LEstudiantes

namespace Estudiantes
{
    public partial class Form1 : Form
    {

        private LEstudiantes estudiante = new LEstudiantes();//Instancia de la clase Estudiantes


        public Form1()
        {
            InitializeComponent();
        }

        private void pictureBoxImage_Click(object sender, EventArgs e)
        {
            estudiante.uploadimage.CargarImagen(pictureBoxImage);//Carga la imagen en el PictureBoxImage (este es su nombre de logica)
        }



        //EVENTOS PARA EL TEXTBOX NOMBRE DEL FORMULARIO

        private void textBoxNombre_TextChanged(object sender, EventArgs e)//Evento TextChanged, cogido en propiedades
        {
            if (textBoxNombre.Text.Equals(""))
            {
                labelNombre.ForeColor = Color.Green;
            }
            else
            {
                labelNombre.ForeColor = Color.Red;
            }

        }


        private void textBoxNombre_KeyPress(object sender, KeyPressEventArgs e)//Evento KeyPress, cogido en propiedades
        {
          estudiante.textBoxEvent.TextKeyPress(e);//Llama al metodo TextKeyPress de la clase TextBoxEvent

        }



        //EVENTOS PARA EL TEXTBOX APELLIDO DEL FORMULARIO

        private void textBoxApellido_TextChanged(object sender, EventArgs e)
        {
            if (textBoxApellido.Text.Equals(""))
            {
                labelApellido.ForeColor = Color.Green;
            }
            else
            {
                labelApellido.ForeColor = Color.Red;
            }

        }

        private void textBoxApellido_KeyPress(object sender, KeyPressEventArgs e)
        {
            estudiante.textBoxEvent.TextKeyPress(e);

        }


        //EVENTOS PARA EL TEXTBOX NID DEL FORMULARIO
        private void textBoxNid_TextChanged(object sender, EventArgs e)
        {
            if (textBoxNid.Text.Equals(""))
            {
                labelNid.ForeColor = Color.Green;
            }
            else
            {
                labelNid.ForeColor = Color.Red;
            }
        }

        private void textBoxNid_KeyPress(object sender, KeyPressEventArgs e)
        {
             estudiante.textBoxEvent.NumberKeyPress(e);
        }


        //EVENTOS PARA EL TEXTBOX EMAIL DEL FORMULARIO
        private void textBoxEmail_TextChanged(object sender, EventArgs e)
        {
            if (textBoxEmail.Text.Equals(""))
            {
                labelEmail.ForeColor = Color.Green;
            }
            else
            {
                labelEmail.ForeColor = Color.Red;
            }

        }

        private void textBoxEmail_KeyPress(object sender, KeyPressEventArgs e)
        {
            estudiante.textBoxEvent.TextKeyPress(e);

        }




    }
}

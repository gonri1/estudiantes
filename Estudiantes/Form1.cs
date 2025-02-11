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
using Logica;//Referencia a la capa lógica para usar sus clases
using Logica.Library;//Referencia a la capa Logica para usar la clase LEstudiantes



namespace Estudiantes
{
    public partial class Form1 : Form
    {

        private LEstudiantes estudiante;
    


        public Form1()
        {
           

            //inicializa el formulario

            InitializeComponent();



            //Captura los TextBox del formulario en una lista


            var listTextBox = new List<TextBox>();//Lista de TextBox

            listTextBox.Add(textBoxNid);//Agrega el TextBoxNid a la lista
            listTextBox.Add(textBoxNombre);//Agrega el TextBoxNombre a la lista
            listTextBox.Add(textBoxApellido);//Agrega el TextBoxApellido a la lista
            listTextBox.Add(textBoxEmail);//Agrega el TextBoxEmail a la lista

          


            //Captura los Labels del formulario en una lista

            var listLabel = new List<Label>();//Lista de Labels

            listLabel.Add(labelNid);//Agrega el LabelNid a la lista
            listLabel.Add(labelNombre);//Agrega el LabelNombre a la lista
            listLabel.Add(labelApellido);//Agrega el LabelApellido a la lista
            listLabel.Add(labelEmail);//Agrega el LabelEmail a la lista
            listLabel.Add(labelPaginas);//Agrega el Label de la paginación a la lista



            // Lista de imagenes en bytes

            Object[] objetos = { 
                pictureBoxImage,
                dataGridView1,
                numericUpDown1
            };
            



            //****INSTANCIA PRINCIPAL***** con sus parametros,elementos de texto del formulario y las labels****

            estudiante = new LEstudiantes(listTextBox, listLabel, objetos);

            //***************************************************************

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
            estudiante.textBoxEvent.EmailKeyPress(e);

        }


        //EVENTO QUE COMPRUEBA AL HACER CLICK EN AGREGAR, SI SE HA PASADO POR TODOS LOS CAMPOS OBLIGATORIOS
        private void buttonAgregar_Click(object sender, EventArgs e)
        {
            estudiante.Registrar();//Llama al metodo Registrar de la clase LEstudiantes
        }
        

        //EVENTO QUE BUSCA AL ESTUDIANTE Y LO MUESTRA EN EL 
        private void textBoxBuscar_TextChanged(object sender, EventArgs e)
        {
            estudiante.SearchEstudiante(textBoxBuscar.Text);
        }


        //********EVENTOS DEL PAGINADOR*************************


        //EVENTO QUE NOS PERMITE IR AL PRIMER REGISTRO
        private void buttonPrimero_Click(object sender, EventArgs e)
        {
            estudiante.Paginador("Primero");
        }

        //EVENTO QUE NOS PERMITE IR AL REGISTRO ANTERIOR
        private void buttonAnterior_Click(object sender, EventArgs e)
        {
            estudiante.Paginador("Anterior");
        }

        //EVENTO QUE NOS PERMITE IR AL SIGUIENTE REGISTRO
        private void buttonSiguiente_Click(object sender, EventArgs e)
        {
            estudiante.Paginador("Siguiente");
        }

        //EVENTO QUE NOS PERMITE IR AL ULTIMO REGISTRO
        private void buttonUltimo_Click(object sender, EventArgs e)
        {
            estudiante.Paginador("Ultimo");
        }

        //EVENTO QUE NOS PERMITE CAMBIAR EN LA INTERFAZ EL VALOR DE numericUpdown, QUE ES DONDE PODEMOS ELEGIR EL NUMERO DE REGISTROS POR PAGINA DE MANERA DINAMICA
        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            estudiante.Registro_Paginas();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;//Par usar an
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
        private DataGridView _dataGridView;//Objeto del data grid del diséño (donde van a ir el listado impreso de los estudiantes)

        //Paginado

        private int _reg_por_pagina=2;
        private int _num_pagina=1;

        public LEstudiantes(List<TextBox> listTextBox, List<Label> listLabel, object[] objetos)
        {
            this.listTextBox = listTextBox;
            this.listLabel = listLabel;
            image = (PictureBox)objetos[0];
            _dataGridView = (DataGridView)objetos[1];
            Restablecer();
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
            else
            {
                listLabel[3].Text = "Email";
                listLabel[3].ForeColor = System.Drawing.Color.Green;
            }

            // Verificar si el email ya está registrado en la base de datos

            var emailExists = _Estudiante.Any(e => e.email.Equals(listTextBox[3].Text));// devuelve bool si hay un mail registrado

            if (emailExists)
            {
                listLabel[3].Text = "Email ya registrado";
                listLabel[3].ForeColor = System.Drawing.Color.Red;
                listTextBox[3].Focus();
                allFieldsFilled = false;
            }

            if (allFieldsFilled)//Si todos los campos estan en TRUE
            {
                BeginTransactionAsync();//Indicamos a la libreria LinqToBd que vamos a usar transacciones

                try
                {
                    //Insertamos las imagenes en un array, convirtiendolo a bytes

                    var imageArray = uploadimage.ImageToByte(image.Image);

                    // Insertamos los valores del formulario en el objeto _Estudiante
                    _Estudiante.Value(e => e.nid, listTextBox[0].Text)
                    .Value(e => e.nombre, listTextBox[1].Text)
                    .Value(e => e.apellido, listTextBox[2].Text)
                    .Value(e => e.email, listTextBox[3].Text)
                    .Value(e => e.image, imageArray).Insert();

                    // Confirmamos la transacción si no hubo errores
                    CommitTransaction();

                    //Restablecemos Labes y limpiamos campos del formulario
                    Restablecer();
                }
                catch (Exception)
                {
                    RollbackTransaction();//Revierte la transaccion
                }
            }
        }

        private void SearchEstudiante(string campoABuscar)
        {
            List<Estudiante> query = new List<Estudiante>();

            int inicio = (_num_pagina - 1) * _reg_por_pagina;

            if (campoABuscar.Equals(""))
            {
                query = _Estudiante.ToList(); // Obtenemos en forma de lista, toda informacion del objeto _Estudiante
            }
            else
            {
                // Filtrar estudiantes por NID, apellido o nombre que comiencen con el valor de campoABuscar
                query = _Estudiante.Where(dato => dato.nid.StartsWith(campoABuscar) || dato.apellido.StartsWith(campoABuscar) || dato.nombre.StartsWith(campoABuscar)).ToList();//toList(), lo convierte en una lista
            }

            //Verificamos si el valor tiene registros y inicializamos un paginador

            if (query.Count > 0) {

                // Asignamos la fuente de datos del DataGridView a una lista de estudiantes filtrada
                // Seleccionamos solo los campos id, nombre, apellido y email para mostrar en el DataGridView
                // A partir de .Skip es cuando empieza el paginador, 
                _dataGridView.DataSource = query.Select(dato => new { dato.id, dato.nombre, dato.apellido, dato.email }).Skip(10).Take(_reg_por_pagina).ToList();
            
            }
        }

        private void Restablecer()
        {
            // Cambiamos  los textos de las Label y le damos color

            listLabel[0].Text = "Nid";
            listLabel[0].ForeColor = System.Drawing.Color.LightSlateGray;
            listLabel[1].Text = "Nombre";
            listLabel[1].ForeColor = System.Drawing.Color.LightSlateGray;
            listLabel[2].Text = "Apellido";
            listLabel[2].ForeColor = System.Drawing.Color.LightSlateGray;
            listLabel[3].Text = "Email";
            listLabel[3].ForeColor = System.Drawing.Color.LightSlateGray;

            // Limpiamos los textBox del formulario

            listTextBox[0].Text = string.Empty;
            listTextBox[1].Text = string.Empty;
            listTextBox[2].Text = string.Empty;
            listTextBox[3].Text = string.Empty;

            //Imprimimos la lista en pantalla con SearchEstudiante()

            SearchEstudiante("");
        }
    }
}

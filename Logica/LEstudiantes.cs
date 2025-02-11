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
        private NumericUpDown _numericUpDown;
        private Paginador<Estudiante> _paginador; //** Creamos un atributo de la clase generica Paginador que va a contener la clase de modelo Estudiante

        //Paginado

        private int _reg_por_pagina=10;
        private int _num_pagina=1;
        private List<Estudiante> listEstudiante;

        public LEstudiantes(List<TextBox> listTextBox, List<Label> listLabel, object[] objetos)
        {
            this.listTextBox = listTextBox;
            this.listLabel = listLabel;

            image = (PictureBox)objetos[0];
            _dataGridView = (DataGridView)objetos[1];
            _numericUpDown = (NumericUpDown)objetos[2];

            Restablecer();
        }




        //MÉTODO QUE REGUSTRA UN ESTUDIANTE CON EL BOTON AGREGAR()
        public void Registrar()
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





        //MÉTODO QUE BUSCA UN ESTUDIANTE Y LO MUESTRA EN EL DataGridView

        public void SearchEstudiante(string campoABuscar)
        {
            // Calculamos el índice de inicio para la paginación

            int inicio = (_num_pagina - 1) * _reg_por_pagina;

            // Filtramos la lista de estudiantes:
            // - Si 'campoABuscar' está vacío, devolvemos todos los estudiantes.
            // - Si tiene un valor, filtramos por 'nid', 'apellido' o 'nombre'.

            var query = _Estudiante.Where(dato =>
                string.IsNullOrEmpty(campoABuscar) || // Si es vacío, devuelve todos
                dato.nid.Contains(campoABuscar) ||   // Si no, busca coincidencias en 'nid'
                dato.apellido.Contains(campoABuscar) || // O en 'apellido'
                dato.nombre.Contains(campoABuscar));    // O en 'nombre'

            // Aplicamos paginación y seleccionamos solo los campos necesarios

            var listaFiltrada = query
                .Select(dato => new { dato.id, dato.nid, dato.nombre, dato.apellido, dato.email }) // Seleccionamos solo estos campos
                .Skip(inicio)  // Omitimos los registros de páginas anteriores
                .Take(_reg_por_pagina) // Tomamos solo los registros de la página actual
                .ToList(); // Convertimos la consulta en una lista

            // Verificamos si hay resultados antes de asignar la lista al DataGridView

            if (listaFiltrada.Count > 0)
            {
                _dataGridView.DataSource = listaFiltrada; // Asignamos la lista filtrada al DataGridView

                // Ocultamos la columna de ID, ya que no es necesario mostrarla al usuario

                _dataGridView.Columns[0].Visible = false;

                // Definimos qué columnas queremos modificar en el DataGridView

                var columnas = new[] { 1, 2, 3, 4 }; // Índices de las columnas visibles

                var colores = new[] { Color.WhiteSmoke, Color.Beige, Color.WhiteSmoke, Color.Beige }; // Colores alternados

                // Aplicamos estilos a las columnas de manera dinámica
                for (int i = 0; i < columnas.Length; i++)
                {
                    _dataGridView.Columns[columnas[i]].HeaderCell.Style.Font = new Font(_dataGridView.Font, FontStyle.Bold); // Fuente en negrita
                    _dataGridView.Columns[columnas[i]].HeaderCell.Style.ForeColor = Color.Black; // Color de la fuente negro
                    _dataGridView.Columns[columnas[i]].HeaderCell.Style.BackColor = colores[i]; // Color de fondo
                    _dataGridView.Columns[columnas[i]].HeaderText = _dataGridView.Columns[columnas[i]].HeaderText.ToUpper(); // Convertimos el texto en mayúsculas
                }
            }
        }



        public void Paginador(string metodo)
        {
            switch (metodo)
            {
                case "Primero":
                    _num_pagina = _paginador.primero();
                    break;
                case "Anterior":
                    _num_pagina = _paginador.anterior();
                    break;
                case "Siguiente":
                    _num_pagina = _paginador.siguiente();
                    break;
                case "Ultimo":
                    _num_pagina = _paginador.ultimo();
                    break;
                default:
                    throw new ArgumentException("Método no válido", nameof(metodo));
            }

            SearchEstudiante("");
        }



        
        //Método para restablecer el formulario y que quede vacio cuando se envia todo
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



            // FRAGMENTO QUE SIRVE AL PAGINADOR
                
            listEstudiante = _Estudiante.ToList();//Creamos una lista con los objetos que hay en Estudiante

            if (0 < listEstudiante.Count)//Comprobamos que haya elmentos
            {
                //Si, los hay, creamos una instancia de Paginador, que es una clase Generica y que admite objetos de la clase Estudiante
               _paginador= new Paginador<Estudiante>(listEstudiante, listLabel[4], _reg_por_pagina);
                
            }

            //Imprimimos la lista en pantalla con SearchEstudiante()

            SearchEstudiante("");
        }
    }
}

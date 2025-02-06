using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
            estudiante.CargarImagen(pictureBoxImage);//Carga la imagen en el PictureBoxImage (este es su nombre de logica)
        }
    }
}

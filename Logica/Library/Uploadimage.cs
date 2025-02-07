using System;
using System.Collections.Generic;
using System.Drawing;//Para usar la clase Image
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;//Referencia que permite usar el método OpenFileDialog()

namespace Logica.Library
{
    public class Uploadimage
    {

        private OpenFileDialog fd = new OpenFileDialog();//Permite buscar archivos en el directorio del PC



        // Metodo permite cargar una imagen en un PictureBox abriendo un cuadro de dialogo

        public void CargarImagen(PictureBox pictureBox)
        {
            pictureBox.WaitOnLoad = true;//carga asincrona

            fd.Filter = "Imagenes|*.jpg;*.gif;*.png";//Filtro para buscar solo imagenes

            fd.ShowDialog();//Muestra el cuadro de dialogo

            if (fd.FileName != string.Empty)// Si el archivo seleccionado no esta vacio
            {
                    pictureBox.ImageLocation = fd.FileName;//Carga la imagen en el PictureBox
            }

        }

        //  Método que retorna un array de bytes y recibe una imagen del picture box

        public byte[] ImageToByte(Image img)
        {

            var converter= new ImageConverter();

            return (byte[])converter.ConvertTo(img, typeof(byte[]));
            
        }
    }
}

using System;
using System.Collections.Generic;
using System.Windows.Forms; // Necesario para el control Label

namespace Logica
{
    public class Paginador<T>
    {
        // Lista de datos a paginar, al ser generico PODEMOS PAGINAR CUALQUIER REGISTRO DE CUALQUIER TABLA QUE LE PASEMOS
        private List<T> dataList;

        // Etiqueta donde se mostrará la información de la paginación
        private Label label;

        // Variables estáticas para manejar la paginación
        private static int maxReg;          // Total de registros en la lista
        private static int reg_por_pagina;  // Cantidad de registros por página
        private static int pageCount;       // Total de páginas disponibles
        private static int numPagi = 1;     // Página actual (comienza en 1)

        // Constructor que recibe la lista de datos, el Label y el número de registros por página
        public Paginador(List<T> dataList, Label label, int reg_por_pagina)
        {
            this.dataList = dataList; // Guarda la lista de datos
            this.label = label; // Guarda el Label donde se mostrará la paginación
            Paginador<T>.reg_por_pagina = reg_por_pagina; // Asigna el número de registros por página
            cargarDatos(); // Se llama al método para calcular las páginas inicialmente
        }

        // Método que calcula la cantidad de páginas y actualiza la etiqueta
        private void cargarDatos()
        {
            numPagi = 1; // Reinicia la página actual a 1
            maxReg = dataList.Count; // Obtiene el total de registros en la lista

            // Calcula el número total de páginas (división entera)
            pageCount = maxReg / reg_por_pagina;

            // Si hay registros adicionales, se agrega una página extra
            if (maxReg % reg_por_pagina > 0)
            {
                pageCount += 1;
            }

            // Verifica que el Label no sea null antes de asignarle el texto
            if (label != null)
            {
                label.Text = $"Páginas {numPagi}/{pageCount}"; // Muestra la página actual y el total
            }
        }




        // Método para ir a la primera página
        public int primero()
        {
            numPagi = 1; // Se establece en la primera página
            label.Text = $"Páginas {numPagi}/{pageCount}"; // Actualiza la etiqueta
            return numPagi; // Retorna la página actual
        }



        // Método para ir a la página anterior
        public int anterior()
        {
            if (numPagi > 1) // Solo se puede retroceder si no estamos en la primera página
            {
                numPagi--;
                label.Text = $"Páginas {numPagi}/{pageCount}";
            }
            return numPagi;
        }


        // Método para ir a la siguiente página
        public int siguiente()
        {
            if (numPagi < pageCount) // Solo avanza si no está en la última página
            {
                numPagi++;
                label.Text = $"Páginas {numPagi}/{pageCount}";
            }
            return numPagi;
        }



        // Método para ir a la última página
        public int ultimo()
        {
            numPagi = pageCount; // Se establece en la última página
            label.Text = $"Páginas {numPagi}/{pageCount}"; // Actualiza la etiqueta
            return numPagi;
        }
    }

         
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinqToDB.Mapping;

namespace Data
{
    //Esta clase tiene que tener unas propiedades que tienen que tener el mismo nombre que la tabla correspodiente
    //de la BBDD estudiante
    public class Estudiante
    {
        [PrimaryKey, Identity]//Marcamos "id" como clave primaria
        public int id { get; set; }
        public string nid { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string email { get; set; }
        public byte[] image { get; set; }
    }
}

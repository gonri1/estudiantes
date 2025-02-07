using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinqToDB;
using LinqToDB.Data;// using de la clase DataConnection (paquete linq)
using LinqToDB.Mapping;

namespace Data
{
    public class Conexion : DataConnection
    {
        // Constructor que inicializa la conexión con la cadena "conn1"
        public Conexion() : base("conn1") { }


        // Proporciona acceso a la tabla Estudiante
        // La propiedad _Estudiante utiliza el método GetTable<T>() de DataConnection para obtener una tabla de la base de datos
        // El tipo genérico T es la clase Estudiante, que representa la tabla en la base de datos
        // Con este objeto _Estudiante podremos hacer un CRUD
        public ITable<Estudiante> _Estudiante
        {
            get
            {
                return this.GetTable<Estudiante>();
            }
        }
    }
}

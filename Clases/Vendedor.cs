using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Vendedor
    {
        #region Atributos
        private string nombre;
        #endregion

        #region Accesores
        public string Nombre
        {
            get
            {
                return nombre;
            }

            set
            {
                nombre = value;
            }
        }
        #endregion

        #region Constructor
        public Vendedor(string nombre)
        {
            this.Nombre = nombre;
        }
        #endregion

    }
}

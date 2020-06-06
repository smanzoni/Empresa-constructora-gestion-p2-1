using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
   public class Cliente
    {
        #region Atributos
        private string nombre;
        private string apellido;
        private string documento;
        private string direccion;
        private int telefono;
        private List<Compra> aptosComprados = new List<Compra>(); // no entiendo xq tira esa advertencia
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
        public string Apellido
        {
            get
            {
                return apellido;
            }

            set
            {
                apellido = value;
            }
        }
        public string Documento
        {
            get
            {
                return documento;
            }

            set
            {
                documento = value;
            }
        }
        public string Direccion
        {
            get
            {
                return direccion;
            }

            set
            {
                direccion = value;
            }
        }
        public int Telefono
        {
            get
            {
                return telefono;
            }

            set
            {
                telefono = value;
            }
        }
        public List<Compra> AptosComprados
        {
            get
            {
                return aptosComprados;
            }

            set
            {
                aptosComprados = value;
            }
        }
        #endregion

        #region Cliente

        // no se si habria que pasarle una lista de aptosComprados como parametro
        public Cliente(string nombre, string apellido, string documento, string direccion, int telefono, List<Compra> aptosComprados )
        {
            this.Nombre = nombre;
            this.Apellido = apellido;
            this.Documento = documento;
            this.Direccion = direccion;
            this.Telefono = telefono;
            this.AptosComprados = aptosComprados;
        }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Edificio
    {
        #region Atributos
        private string nombre;
        private string direccion;
        private List<Apartamento> misApartamentos;
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
        public List<Apartamento> MisApartamentos
        {
            get
            {
                return misApartamentos;
            }
            set
            {
                misApartamentos = value;
            }
        }
        #endregion

        #region Constructor
        public Edificio(string nombre, string direccion, List<Apartamento> misApartamentos)
        {
            this.Nombre = nombre;
            this.Direccion = direccion;
            this.MisApartamentos = misApartamentos;
        }
        #endregion

        #region Validaciones
        //Validaciones de Edificio

        public bool ValidarEdificio()
        {
            //mientras que el nombre ingresado y la cedula no esten vacios 
            //y se haya seleccionado al menos un apartamento, retorna true
            return this.Nombre.Length > 0 
                && this.Direccion.Length > 0
                && this.misApartamentos.Count>0;
        }
        #endregion

        #region Métodos de object redefinidos    
        
        // Verificamos que no exista otro Edificio con ese nombre a traves de Equals
        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            Edificio elEdificio = obj as Edificio;
            if (elEdificio == null)
            {
                return false;
            }
            else
            {
                return this.Nombre == elEdificio.Nombre;
            }
                
        }

        //Asi vamos a visualizar los edificios cuando se desplieguen en la lista
        public override string ToString()
        {
            return "Nombre: " + this.Nombre + ", Dirección: " + this.Direccion;
        }


        #endregion

    }
}



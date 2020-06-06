using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public abstract class Apartamento
    {
        #region Atributos
        private int piso;
        private int numero;
        private int metrajeTotal;
        private decimal precioBaseXm2;
        private string orientacion;
        #endregion

        #region Accesores
        public int Piso
        {
            get
            {
                return piso;
            }

            set
            {
                piso = value;
            }
        }
        public int Numero
        {
            get
            {
                return numero;
            }

            set
            {
                numero = value;
            }
        }
        public int MetrajeTotal
        {
            get
            {
                return metrajeTotal;
            }

            set
            {
                metrajeTotal = value;
            }
        }
        public decimal PrecioBaseXm2
        {
            get
            {
                return precioBaseXm2;
            }

            set
            {
                precioBaseXm2 = value;
            }
        }
        public string Orientacion
        {
            get
            {
                return orientacion;
            }

            set
            {
                orientacion = value;
            }
        }

        //public Edificio MiEdificio
        //{
        //    get
        //    {
        //        return miEdificio;
        //    }
        //    set
        //    {
        //        miEdificio = value;
        //    }
        //}
        #endregion

        #region Constructor
        public Apartamento(int piso, int numero, int metrajeTotal, decimal precioBaseXm2, string orientacion)
        {
            this.Piso = piso;
            this.Numero = numero;
            this.MetrajeTotal = metrajeTotal;
            this.PrecioBaseXm2 = precioBaseXm2;
            this.Orientacion = orientacion;
            //this.MiEdificio = unEdificio;
        }
        #endregion

        #region Metodos
        //metodo abstracto para calcular precio
        //al ser clase abstract y metodo abstract, hay que redefinirlo especificamente en sus clases derivadas
        //OVERRIDE en oficina y vivienda
        public abstract decimal calcularPrecio();

        #endregion

        #region Validaciones
        //Valido que los datos ingresados sean correctos
        public bool ValidarApartamento()
        { 
            return this.Piso > 0 && this.Piso < 500
                && this.Numero > 0 && this.Numero < 500
                && this.MetrajeTotal > 0 && this.MetrajeTotal < 300000
                && this.Orientacion != "";
        }

        #endregion

        #region Métodos de object redefinidos  
        //Con este metodo voy a visualizar el Objeto Apartamento con todos los datos que le pida
        //El metodo getType me devuelve el tipo de apartamento (vivienda, oficina)
        public override string ToString()
        {
            string datosApto = "";
            datosApto += "Tipo: " + this.GetType().Name + Environment.NewLine;
            datosApto += ", Piso: " + this.Piso + Environment.NewLine;
            datosApto += ", Numero: " + this.Numero + Environment.NewLine;
            datosApto += ", Metraje Total: " + this.MetrajeTotal + " m2"+ Environment.NewLine;
            datosApto += ", Precio: $ " + this.calcularPrecio() + Environment.NewLine;
            datosApto += ", Orientacion: " + this.Orientacion;
            return datosApto;
        }

        public override bool Equals(object obj)
        {
            bool existe = false;

            Apartamento elApto = obj as Apartamento;

            if (elApto == null) return false;
            else
            {
                if (elApto.Piso == this.Piso && elApto.Numero == this.Numero)
                {
                    existe = true;
                }
                else if (elApto.Piso == this.Piso && elApto.Orientacion == this.Orientacion)
                {
                    existe = true;
                }
            }
            
            return existe;
        }

        #endregion
    }


}

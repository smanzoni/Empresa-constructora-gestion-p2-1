using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Oficina : Apartamento
    {
        #region Atributos
        private int puestosTrabajo;
        private bool equipamiento;
        #endregion

        #region Accesores
        public int PuestosTrabajo
        {
            get
            {
                return puestosTrabajo;
            }

            set
            {
                puestosTrabajo = value;
            }
        }
        public bool Equipamiento
        {
            get
            {
                return equipamiento;
            }

            set
            {
                equipamiento = value;
            }
        }

        #endregion

        #region Constructor
        public Oficina(int piso, int numero, int metrajeTotal, decimal precioBaseXm2, string orientacion, int puestosTrabajo, bool equipamiento)
            : base(piso, numero, metrajeTotal, precioBaseXm2, orientacion)
        {
            this.PuestosTrabajo = puestosTrabajo;
            this.Equipamiento = equipamiento;
        } 
        #endregion

        #region Metodos
         public override decimal calcularPrecio()
         {
             //calcular monto base
             decimal precioTotal = this.PrecioBaseXm2 * this.MetrajeTotal; ;

             //monto fijo x unidad oficina que pueda ocupar en la vivienda
             decimal montoFijoXoficina = 600;

             precioTotal += (puestosTrabajo * montoFijoXoficina);

             //calculamos porcentaje extra por si la of va equipada o no
             if (this.Equipamiento)
             {
                 precioTotal = (10 * precioTotal) / 100;
             }

             return precioTotal;
         }
         #endregion
    }
}

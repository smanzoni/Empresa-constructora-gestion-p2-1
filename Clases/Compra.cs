using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Compra
    {
        #region Atributos
        private DateTime fechaCompra;
        private Vendedor vendedorApto;
        private Apartamento datosApto;
        private decimal precio;
        #endregion

        #region Accesores
        public DateTime FechaCompra
        {
            get
            {
                return fechaCompra;
            }

            set
            {
                fechaCompra = value;
            }
        }
        public Vendedor VendedorApto
        {
            get
            {
                return vendedorApto;
            }

            set
            {
                vendedorApto = value;
            }
        }
        public Apartamento DatosApto
        {
            get
            {
                return datosApto;
            }

            set
            {
                datosApto = value;
            }
        }
        public decimal Precio
        {
            get
            {
                return precio;
            }

            set
            {
                precio = value;
            }
        }
        #endregion

        #region Constructor
        public Compra(DateTime fechaCompra, Vendedor vendedorApto, Apartamento datosApto, decimal precio)
        {
            this.FechaCompra = fechaCompra;
            this.VendedorApto = vendedorApto;
            this.DatosApto = datosApto;
            this.Precio = precio;
        }
        #endregion
    }
}

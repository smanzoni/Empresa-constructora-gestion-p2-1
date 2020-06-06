using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Sistema
    {

        #region SINGLETON
        // atributo y metodo para dejar instancia de las listas globales. Similar al del ejercicio del banco.
        // al ser static la podemos llamar desde cualquier lado sin necesidad de instanciar al sistema
        private static Sistema instanciaSistema;
        public static Sistema InstanciaSistema
        {
            get
            {
                if (instanciaSistema == null)
                {
                    instanciaSistema = new Sistema();
                }
                return instanciaSistema;
            }
        }
        #endregion

        #region Atributos
        //Desde Sistema manejamos tanto a los edificios como a los apartamentos que hay
        private List<Edificio> listaEdificios;
        private List<Apartamento> listaApartamentos;
        //private List<Cliente> clientes;
        //private List<Compra> compras;
        //private List<Vendedor> vendedores;
        #endregion

        #region Accesores
        public List<Edificio> ListaEdificios
        {
            get
            {
                return listaEdificios;
            }

            private set
            {
                listaEdificios = value;
            }
        }
        public List<Apartamento> ListaApartamentos
        {
            get
            {
                return listaApartamentos;
            }

            private set
            {
                listaApartamentos = value;
            }
        }

        /* #region Accesores cliente, compra y vendedor
        public List<Cliente> Clientes
        {
            get
            {
                return clientes;
            }

            private set
            {
                clientes = value;
            }
        }
        public List<Compra> Compras
        {
            get
            {
                return compras;
            }

            private set
            {
                compras = value;
            }
        }
        public List<Vendedor> Vendedores
        {
            get
            {
                return vendedores;
            }

            private set
            {
                vendedores = value;
            }
        }
        #endregion */

        #endregion

        #region Constructor
        //instancia del sistema, este contiene las listas de Edificios y Apartamentos por ahora
        public Sistema()
        {
            this.ListaEdificios = new List<Edificio>();
            this.ListaApartamentos = new List<Apartamento>();

            //funcion de precarga de apartamentos
            //AgregarAptosPrueba();

            //this.Clientes = new List<Cliente>();
            //this.Compras = new List<Compra>();
            //this.Vendedores = new List<Vendedor>();

        }
        #endregion

        #region Metodos

        //Agrego un Edificio completo a la lista de edificios global.
        public bool AgregarEdificio(Edificio unEdificio)
        {
            if (unEdificio == null) return false;
            //validacion que esta en edificio
            if (!unEdificio.ValidarEdificio()) return false;
            //Verifica que no haya dos objetos edificio con el mismo nombre, lo hicimos con el Equals
            if (this.ListaEdificios.Contains(unEdificio))
            {
                return false;
            }
            else
            {
                ListaEdificios.Add(unEdificio);
                return true;
            }
        }

        //Agregar apartamento a la lista global
        public bool AgregarApto(Apartamento unApto)
        {
            if (unApto == null) return false;
            //validacion que esta en la clase apto
            if (!unApto.ValidarApartamento()) return false;
            //Verifica que no haya dos objetos apartamento con el mismo nombre, lo hicimos con el Equals
            if (this.ListaApartamentos.Contains(unApto))
            {
                return false;
            }
            else
            {
                ListaApartamentos.Add(unApto);
                return true;
            }
        }

        // Agrego un apartamento a un edificio ya creado.
        public bool AgregarApartamentoAedificio(Edificio unEdificio, Apartamento unApartamento)
        {
            bool devolver;
            //falta validacion de Apartamento   
            if (!unEdificio.MisApartamentos.Contains(unApartamento) && unApartamento.ValidarApartamento())
            {
                //agrega el apartamento a la lista de apartamentos 'global'.
                this.ListaApartamentos.Add(unApartamento);
                //agrega el apartamento a la lista de apartamentos, de un edificio especifico
                unEdificio.MisApartamentos.Add(unApartamento);
                devolver = true;
            }
            else
            {
                devolver = false;
            }
            return devolver;
        }

        #region precarga de edificios
        //Precarga de datos
        public void DatosPrecargados()
        {
            {
                //Creo aptos
                Vivienda vi1 = new Vivienda(1, 33, 120, 500, "S", 3, 2, false);
                Vivienda vi2 = new Vivienda(2, 20, 90, 500, "N", 1, 1, true);
                Oficina of1 = new Oficina(2, 22, 50, 500, "NE", 10, true);
                Oficina of2 = new Oficina(3, 23, 80, 500, "O", 6, true);

                List<Apartamento> aptos1 = new List<Apartamento>();
                aptos1.Add(vi1);
                aptos1.Add(vi2);
                aptos1.Add(of1);
                aptos1.Add(of2);

                //Creo edificio
                Edificio edi1 = new Edificio("Complejo Torre Sur", "direcc1", aptos1);

                //Agrego edificio
                AgregarEdificio(edi1);

                //Agrego aptos ( global )
                AgregarApto(vi1);
                AgregarApto(vi2);
                AgregarApto(of1);
                AgregarApto(of2);
            }
            {
                //Creo aptos
                Vivienda vi3 = new Vivienda(3, 33, 110, 500, "NO", 3, 2, false);
                Vivienda vi4 = new Vivienda(4, 20, 120, 500, "NE", 3, 1, true);
                Oficina of3 = new Oficina(6, 22, 60, 500, "N", 4, true);
                Oficina of4 = new Oficina(10, 23, 40, 500, "SE", 9, true);

                List<Apartamento> aptos2 = new List<Apartamento>();
                aptos2.Add(vi3);
                aptos2.Add(vi4);
                aptos2.Add(of3);
                aptos2.Add(of4);

                //Creo edificio
                Edificio edi2 = new Edificio("Ed. Prado", "direcc2", aptos2);

                //Agrego edificio
                AgregarEdificio(edi2);

                //Agrego aptos ( global )
                AgregarApto(vi3);
                AgregarApto(vi4);
                AgregarApto(of3);
                AgregarApto(of4);
            }
            {
                //Creo aptos
                Vivienda vi5 = new Vivienda(10, 33, 140, 500, "N", 3, 2, false);
                Vivienda vi6 = new Vivienda(9, 20, 100, 500, "N", 2, 1, true);
                Oficina of5 = new Oficina(5, 22, 100, 500, "NE", 15, true);
                Oficina of6 = new Oficina(3, 23, 40, 500, "SO", 20, true);

                List<Apartamento> aptos3 = new List<Apartamento>();
                aptos3.Add(vi5);
                aptos3.Add(vi6);
                aptos3.Add(of5);
                aptos3.Add(of6);

                //Creo edificio
                Edificio edi3 = new Edificio("Un edificio ahí", "direcc3", aptos3);

                //Agrego edificio
                AgregarEdificio(edi3);
                //Agrego aptos ( global )
                AgregarApto(vi5);
                AgregarApto(vi6);
                AgregarApto(of5);
                AgregarApto(of6);
            }
        }
        #endregion

        #endregion

        #region Validaciones de campos
        //Si el campo no quede vacio
        public bool campoVacio(string campoEvaluar)
        {
            if (campoEvaluar != "")
            {
                return false;
            }
            return true;
        }

        //Si el campo es numerico o no
        public bool esNumerico(string campoEvaluar)
        {
            try
            {
                Convert.ToInt32(campoEvaluar);
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

    }
}

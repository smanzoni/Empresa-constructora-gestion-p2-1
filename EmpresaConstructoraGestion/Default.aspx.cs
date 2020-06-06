using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;



namespace EmpresaConstructoraGestion
{
    public partial class Default : System.Web.UI.Page
    {
        //Singleton
        Sistema elSistema = Sistema.InstanciaSistema;

        protected void Page_Load(object sender, EventArgs e)
        {

            //precarga de edificios
            elSistema.DatosPrecargados();

            //Espacio en Session para guardar una lista auxiliar "flotante", que será la que se cargue de aptos
            //para luego pasarla como parametro a un Edificio cuando este se vaya a crear.
            if (!Page.IsPostBack)
            {
                if (Session["listaAux"] == null)
                {
                    Session["listaAux"] = new List<Apartamento>();
                }
            }
        }

        #region Eventos Click
        //Agregar apto en creación de edificio.
        protected void btnAltaApto_Click(object sender, EventArgs e)
        {
            //Redireccionamos al espacio en Sessssssion
            List<Apartamento> listaAuxiliar = Session["listaAux"] as List<Apartamento>;

            Apartamento nuevoApto = EvaluarYdevolverApto();

            int i = 0;
            bool encontreApto = false;

            //Buscamos que el apto no exista en la lista auxiliar
            while (i < listaAuxiliar.Count && !encontreApto)
            {
                if (nuevoApto.Equals(listaAuxiliar[i]))
                {
                    encontreApto = true;
                }
                else
                {
                    i++;
                }
            }

            //si el apartamento noExiste, agrego
            if (!encontreApto)
            {
                listaAuxiliar.Add(nuevoApto);
                lblMensajeApartamento.Text = "Apartamento ingresado con éxito.";
                btnAltaEdificio.Enabled = true;

                lstAptosEdificio.DataSource = listaAuxiliar;
                lstAptosEdificio.DataBind();
                pInfoVivienda.Visible = false;
                pInfoOficina.Visible = false;

                LimpiarCampos();
            }
            else //sino, msj de error
            {
                lblMensajeApartamento.Text = "<b>Error</b><br> No pueden haber dos aptos. con <em>mismo piso - misma orientación</em> / <em>mismo piso - mismo nro. de apto.</em>";
            }
        }

        //Agregar apto a edificio ya creado.
        protected void btnAltaApto2_Click(object sender, EventArgs e)
        {
            // Llamamos al apto ya validado anteriormente y lo guardamos en la variable nuevoApto
            Apartamento nuevoApto = EvaluarYdevolverApto();

            Edificio edificioSeleccionado = null;
            bool encontreEdificio = false;
            int i = 0;
            //Se busca el edificio por el nombre.
            while (i < elSistema.ListaEdificios.Count && !encontreEdificio)
            {
                if (elSistema.ListaEdificios[i].Nombre == lstListaEdificios.SelectedValue)
                {
                    encontreEdificio = true;
                    // Se lo asigno al edificio instanciado en null.
                     edificioSeleccionado = elSistema.ListaEdificios[i];
                }
                else
                {
                    i++;
                    lblMensajeApartamento.Text = "Error en edificio seleccionado";
                }
            }

            

            //Agregamos el apto a la lista del edificio seleccionado, y a la lista global de aptos.
            if (elSistema.AgregarApartamentoAedificio(edificioSeleccionado, nuevoApto))
            {
                lblMensajeApartamento.Text = "Apartamento agregado con éxito.";
                LimpiarCampos();
            }
            else
            {
                lblMensajeApartamento.Text = "Ya has ingresado un apartamento en el mismo piso con la misma orientación.";
            }
        }

        //Agregar Edificio.
        protected void btnAltaEdificio_Click(object sender, EventArgs e)
        {
            if (!elSistema.campoVacio(txtNombreEdificio.Text) && !elSistema.esNumerico(txtNombreEdificio.Text))
            {
                string nombreEdificio = txtNombreEdificio.Text;

                if (!elSistema.esNumerico(txtDirecEdificio.Text))
                {
                    string direccionEdifico = txtDirecEdificio.Text;

                    //Nos redireccionamos a la lista auxiliar de Session
                    List<Apartamento> apartamentosEdificio = Session["listaAux"] as List<Apartamento>;

                    Edificio nuevoEdificio = new Edificio(nombreEdificio, direccionEdifico, apartamentosEdificio);

                    //Si todos los campos son correctos y el Edificio no esta repetido
                    if (elSistema.AgregarEdificio(nuevoEdificio))
                    {
                        lblMensajeEdificio.Text = "<em><b>Edificio agregado con éxito.</b></em>";
                        lblMensajeApartamento.Text = "";

                        //Si esta todo ok, aparte de agregar el edificio, agregamos todos los apartamentos de la listaEdificio
                        for (int i = 0; i < apartamentosEdificio.Count; i++)
                        {
                            elSistema.AgregarApto(apartamentosEdificio[i]);
                        }

                        //Limpiamos la lista guardada en session para que quede limpia para el prox ingreso de edificio.
                        apartamentosEdificio.Clear();
                    }

                }
                else
                {
                    lblMensajeEdificio.Text = "<b><u>Error.</u></b> Direccion invalida.";
                }
            }
            else
            {
                lblMensajeEdificio.Text = "<b><u>Error.</u></b> Nombre invalido.";
            }
        }

        //Despliega campos para ingreso de vivienda u oficina.
        protected void rbTipoApto_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rbTipoApto.SelectedValue == "vivienda")
            {
                pInfoOficina.Visible = false;
                pInfoVivienda.Visible = true;

            }
            else if (rbTipoApto.SelectedValue == "oficina")
            {
                pInfoVivienda.Visible = false;
                pInfoOficina.Visible = true;
            }
        }

        //Logica para filtrar segun rango de m2 + orientacion.
        protected void btnFiltroM2_Click(object sender, EventArgs e)
        {
            lblImprimirEdificios.Text = "";

            int cantMin = Convert.ToInt32(txtCantMinM2.Text);
            int cantMax = Convert.ToInt32(txtCantMaxM2.Text);


            for (int i = 0; i < elSistema.ListaEdificios.Count; i++)
            {
                foreach (Apartamento apto in elSistema.ListaEdificios[i].MisApartamentos)
                {
                    if (apto.MetrajeTotal >= cantMin && apto.MetrajeTotal <= cantMax)
                    {
                        if (apto.Orientacion == ddlOrientacionFiltros.SelectedValue)
                        {
                            lblImprimirEdificios.Text += "Nombre edificio: " + elSistema.ListaEdificios[i].Nombre + "<br>";
                            lblErrorFiltro.Text = "";
                            break;
                        }
                    }
                    else
                    {
                        LimpiarCamposFiltros();
                        lblErrorFiltro.Text = "No hay edificios en el rango que buscas.";
                    }
                }
            }
        }

        //Logica para filtrar segun rango de precios.
        protected void btnFiltrarPrecios_Click(object sender, EventArgs e)
        {
            //Este if lo podriamos obviar xq esta la lista precargada
            if (elSistema.ListaApartamentos.Count > 0)
            {
                if (elSistema.esNumerico(txtPrecioMin.Text) && !elSistema.campoVacio(txtPrecioMax.Text))
                {
                    int precioMin = Convert.ToInt32(txtPrecioMin.Text);
                    int precioMax = Convert.ToInt32(txtPrecioMax.Text);

                    //recorremos cada uno de los edificios
                    for (int i = 0; i < elSistema.ListaEdificios.Count; i++)
                    {
                        //recorremos cada apto en cada listaDeAptos de cada edificio
                        foreach (Apartamento apto in elSistema.ListaEdificios[i].MisApartamentos)
                        {
                            decimal precioApto = apto.calcularPrecio();

                            //Si el precio esta dentro del rango
                            if (precioApto >= precioMin && precioApto <= precioMax)
                            {
                                //Imprimimos el nombre del edificio en posicion I + la info del apto 
                                lblImprimirAptosPrecio.Text += "Edificio: " + elSistema.ListaEdificios[i].Nombre + "//Apto " + apto.ToString() + "<br>";
                            }
                        }
                        //Salto de linea entre edificios.
                        lblImprimirAptosPrecio.Text += "<br>";
                    }
                }
                else
                {
                    LimpiarCamposFiltros();
                    lblErrorFiltro.Text = "Revisar valores ingresados";
                }
            }
            else
            {
                LimpiarCamposFiltros();
                lblErrorFiltro.Text = "Primero debes ingresar un apartamento en el sistema";
            }
        }

        //Comprobar si existen aptos dentro del rango de metraje.
        protected void ComprobarRango_Click(object sender, EventArgs e)
        {
            bool existe = false;

            if (elSistema.esNumerico(txtCantMinM2.Text) && !elSistema.campoVacio(txtCantMinM2.Text) && elSistema.esNumerico(txtCantMaxM2.Text) && !elSistema.campoVacio(txtCantMaxM2.Text))
            {
                int metrajeMin = Convert.ToInt32(txtCantMinM2.Text);
                int metrajeMax = Convert.ToInt32(txtCantMaxM2.Text);


                for (int i = 0; i < elSistema.ListaEdificios.Count; i++)
                {
                    foreach (Apartamento apto in elSistema.ListaEdificios[i].MisApartamentos)
                    {
                        if (apto.MetrajeTotal >= metrajeMin && apto.MetrajeTotal <= metrajeMax)
                        {
                            existe = true;
                        }
                        //Disculpas jejeje
                        break;
                    }
                    //Otra vez D:
                    break;
                }

                if (existe)
                {
                    lblImprimirEdificios.Text = "Existen apartamentos dentro del rango de metraje especificado.";
                }
                else
                {
                    lblImprimirEdificios.Text = "<b>No</b> existen apartamentos dentro del rango de metraje especificado.";
                }
            }
        }

        //Navegación en menú principal
        protected void menuNavegacionPrincipal_MenuItemClick(object sender, MenuEventArgs e)
        {
            if (menu.SelectedValue == "cEdificio")
            {
                LimpiarCampos();

                //Muestra y oculta las secciones correspondientes del HTML
                IngresarEdificio.Visible = true;
                IngresarApartamento.Visible = true;
                contenedorListaEdificios.Visible = false;
                btnAltaApto.Visible = true;
                btnAltaApto2.Visible = false;
                pFiltros.Visible = false;
            }
            else if (menu.SelectedValue == "cApartamento")
            {
                LimpiarCampos();

                //Desplegamos edificios cargados en la ListaEdificios
                DesplegarEdificios();

                //Muestra y oculta las secciones correspondientes del HTML
                IngresarEdificio.Visible = false;
                IngresarApartamento.Visible = true;
                contenedorListaEdificios.Visible = true;
                btnAltaApto.Visible = false;
                btnAltaApto2.Visible = true;
                pFiltros.Visible = false;
            }
            else if (menu.SelectedValue == "buscador")
            {
                LimpiarCampos();

                //Muestra y oculta las secciones correspondientes del HTML
                IngresarEdificio.Visible = false;
                IngresarApartamento.Visible = false;
                pFiltros.Visible = true;

            }
        }

        //Navegación en menú de filtros.
        protected void menuBuscadores_MenuItemClick(object sender, MenuEventArgs e)
        {
            if (menuBuscadores.SelectedValue == "filtroM2")
            {
                //Muestra y oculta las secciones correspondientes del HTML
                divRangoM2.Visible = true;
                divRangoPrecio.Visible = false;
                LimpiarCamposFiltros();
            }
            if (menuBuscadores.SelectedValue == "filtroRangoPrecios")
            {
                //Muestra y oculta las secciones correspondientes del HTML
                divRangoM2.Visible = false;
                divRangoPrecio.Visible = true;
                LimpiarCamposFiltros();
            }
        }

        #endregion

        #region Validación y creación de aptos.
        //Valida los campos de apartamento y crea+devuelve el objeto( tanto oficina como vivienda )
        protected Apartamento EvaluarYdevolverApto()
        {
            Apartamento nuevoApto = null;
            if (elSistema.esNumerico(txtPiso.Text) && !elSistema.campoVacio(txtPiso.Text))
            {
                int pisoApto = Convert.ToInt32(txtPiso.Text);

                if (elSistema.esNumerico(txtNumero.Text) && !elSistema.campoVacio(txtNumero.Text))
                {
                    int numeroApto = Convert.ToInt32(txtNumero.Text);

                    if (elSistema.esNumerico(txtMetraje.Text) && !elSistema.campoVacio(txtMetraje.Text))
                    {
                        int metrajeApto = Convert.ToInt32(txtMetraje.Text);

                        if (ddlOrientacion.SelectedValue != "vacio")
                        {
                            string orientacionApto = ddlOrientacion.SelectedValue;

                            if (rbTipoApto.SelectedValue != "")
                            {
                                if (rbTipoApto.SelectedValue == "vivienda")
                                {
                                    if (elSistema.esNumerico(txtCantDormitorios.Text) && !elSistema.campoVacio(txtCantDormitorios.Text))
                                    {
                                        int cantDormitoriosApto = Convert.ToInt32(txtCantDormitorios.Text);
                                        if (elSistema.esNumerico(txtCantBanios.Text) && !elSistema.campoVacio(txtCantBanios.Text))
                                        {
                                            int cantBaniosApto = Convert.ToInt32(txtCantBanios.Text);
                                            if (rbGarage.SelectedValue != "")
                                            {
                                                bool poseeGarageApto = false;
                                                if (rbGarage.SelectedValue == "si")
                                                {
                                                    poseeGarageApto = true;
                                                }
                                                // luego de todas las validaciones pertinentes, crea el nuevo apartamento
                                                nuevoApto = new Vivienda(pisoApto, numeroApto, metrajeApto, 500, orientacionApto, cantDormitoriosApto, cantBaniosApto, poseeGarageApto);

                                                // la funcion devuelve el apartamento creado

                                            }
                                            else
                                            {
                                                lblMensajeApartamento.Text = "<b><u>Error.</u></b><br> Selecciona si el apto posee garage o no.";
                                            }
                                        }
                                        else
                                        {
                                            lblMensajeApartamento.Text = "<b><u>Error.</u></b><br> Especifica cantidad de baños.";
                                        }
                                    }
                                    else
                                    {
                                        lblMensajeApartamento.Text = "<b><u>Error.</u></b><br> Especifica cantidad de dormitorios.";
                                    }
                                }
                                else if (rbTipoApto.SelectedValue == "oficina")
                                {
                                    if (elSistema.esNumerico(txtCantPuestos.Text) && !elSistema.campoVacio(txtCantPuestos.Text))
                                    {
                                        int cantidadPuestosApto = Convert.ToInt32(txtCantPuestos.Text);

                                        if (rbEquipado.SelectedValue != "")
                                        {
                                            bool oficinaEquipadaApto = false;
                                            if (rbEquipado.SelectedValue == "si")
                                            {
                                                oficinaEquipadaApto = true;
                                            }

                                            nuevoApto = new Oficina(pisoApto, numeroApto, metrajeApto, 100, orientacionApto, cantidadPuestosApto, oficinaEquipadaApto);
                                        }
                                        else
                                        {
                                            lblMensajeApartamento.Text = "<b><u>Error.</u></b><br> Especifica si la oficina será equipada o no.";
                                        }
                                    }
                                    else
                                    {
                                        lblMensajeApartamento.Text = "<b><u>Error.</u></b><br> No has especificado que cantidad de puestos habra en la oficina.";
                                    }
                                }
                            }
                            else
                            {
                                lblMensajeApartamento.Text = "<b><u>Error.</u></b><br> No has seleccionado típo de apartamento.";
                            }
                        }
                        else
                        {
                            lblMensajeApartamento.Text = "<b><u>Error.</u></b><br> Orientación no valida.";
                        }
                    }
                    else
                    {
                        lblMensajeApartamento.Text = "<b><u>Error.</u></b><br> El campo 'Metraje total' solo acepta valores numéricos.";
                    }
                }
                else
                {
                    lblMensajeApartamento.Text = "<b><u>Error.</u></b><br> El campo 'Numero apto.' solo acepta valores numéricos.";
                }
            }
            else
            {
                lblMensajeApartamento.Text = "<b><u>Error.</u></b><br>El campo 'Piso' solo acepta valores numéricos.";
            }

            // devolvemos el apartamento creado, sea oficina o vivienda

            return nuevoApto;



        }
        #endregion

        #region Metodos auxiliares
        //Limpia campos en la sección de agregar apto
        protected void LimpiarCampos()
        {
            txtPiso.Text = "";
            txtNumero.Text = "";
            txtMetraje.Text = "";
            ddlOrientacion.ClearSelection();
            rbTipoApto.ClearSelection();
            txtCantDormitorios.Text = "";
            txtCantBanios.Text = "";
            rbGarage.ClearSelection();
            txtCantPuestos.Text = "";
            rbEquipado.ClearSelection();
        }

        //desplegamos edificios en listbox en la sección de Agregar apartamento.
        private void DesplegarEdificios()
        {
            lstListaEdificios.DataSource = elSistema.ListaEdificios;

            //muestra lo que queremos que se vea del edificio ( muestra el valor del atributo, nombre en este caso )
            lstListaEdificios.DataTextField = "Nombre";
            //dataValueField le da al item, como value, lo que tiene la propiedad que le indicamos, en este caso Nombre;
            lstListaEdificios.DataValueField = "Nombre";
            lstListaEdificios.DataBind();
        }

        //Limpia los campos en la sección de filtros.
        protected void LimpiarCamposFiltros()
        {
            txtCantMinM2.Text = "";
            txtCantMaxM2.Text = "";
            ddlOrientacionFiltros.SelectedValue = "vacio";
            txtPrecioMin.Text = "";
            txtPrecioMax.Text = "";
            lblImprimirAptosPrecio.Text = "";
            lblImprimirEdificios.Text = "";
            lblErrorFiltro.Text = "";
        }

        #endregion


    }
}


<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="EmpresaConstructoraGestion.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Empresa Constructora - Edificios</title>
</head>

<body>

    <form id="form1" runat="server">

        <%-- Menú de navegación--%>
        <div>
            <asp:Menu ID="menu" runat="server" BackColor="#E5E5E5" BorderStyle="Solid" BorderWidth="1px" OnMenuItemClick="menuNavegacionPrincipal_MenuItemClick">
                <Items>
                    <asp:MenuItem Text="Crear edificio" Value="cEdificio"></asp:MenuItem>
                    <asp:MenuItem Text="Agregar apto. a edificio existente" Value="cApartamento"></asp:MenuItem>
                    <asp:MenuItem Text="Buscador" Value="buscador"></asp:MenuItem>
                </Items>
            </asp:Menu>
        </div>

        <%-- Ingreso de información de edificio, + lista de apartamentos agregados al mismo --%>
        <div id="IngresarEdificio" runat="server" visible="False">
            <h2>Ingresar Edificio:</h2>
            <hr />
            <asp:Panel ID="pEdificio" runat="server" GroupingText="Datos Edificio">
                <br />
                <asp:Label ID="Label1" runat="server" Text="Nombre:"></asp:Label><asp:TextBox ID="txtNombreEdificio" runat="server"></asp:TextBox>
                <br />
                <asp:Label ID="Label2" runat="server" Text="Direccion: "></asp:Label><asp:TextBox ID="txtDirecEdificio" runat="server"></asp:TextBox>
                <br />
                <br />
                <asp:Label ID="Label13" runat="server" Text="Apartamentos en edificio:"></asp:Label>
                <br />
                <asp:ListBox ID="lstAptosEdificio" runat="server" Width="850px" Height="160px"></asp:ListBox>
                <br />
                <br />
                <asp:Button ID="btnAltaEdificio" runat="server" Enabled="False" Text="Ingresar Edificio" OnClick="btnAltaEdificio_Click" />
                <br />
                <br />
                <asp:Label ID="lblMensajeEdificio" runat="server"></asp:Label>
                <br />
                <br />
            </asp:Panel>
        </div>

        <%-- Ingreso de información de apartamento --%>
        <div id="IngresarApartamento" runat="server" visible="False">
            <h2>Ingresar Apartamento:</h2>
            <hr />
            <asp:Panel ID="pApartamento" runat="server" GroupingText="Datos Apartamento">
                <%-- Esto lo englobamos a un div para poder ocultarlo. --%>
                <div id="contenedorListaEdificios" runat="server">
                    <br />
                    <asp:Label ID="Label14" runat="server" Text="Seleccionar edificio:"></asp:Label>
                    <br />
                    <asp:ListBox ID="lstListaEdificios" runat="server" Height="164px" Width="311px"></asp:ListBox>
                    <br />
                </div>

                <br />
                <br />
                <asp:Label ID="Label4" runat="server" Text="Piso: "></asp:Label><asp:TextBox ID="txtPiso" runat="server"></asp:TextBox><br />
                <asp:Label ID="Label6" runat="server" Text="Numero apto. : "></asp:Label><asp:TextBox ID="txtNumero" runat="server"></asp:TextBox><br />
                <asp:Label ID="Label7" runat="server" Text="Metraje Total: "></asp:Label><asp:TextBox ID="txtMetraje" runat="server" Width="130px"></asp:TextBox><br />
                <br />
                <asp:Label ID="Label19" runat="server" Text="Orientación: "></asp:Label>
                <asp:DropDownList ID="ddlOrientacion" runat="server">
                    <asp:ListItem Value="vacio">Seleccionar</asp:ListItem>
                    <asp:ListItem Value="N">Norte</asp:ListItem>
                    <asp:ListItem Value="NE">Norte-Este</asp:ListItem>
                    <asp:ListItem Value="E">Este</asp:ListItem>
                    <asp:ListItem Value="SE">Sur-Este</asp:ListItem>
                    <asp:ListItem Value="S">Sur</asp:ListItem>
                    <asp:ListItem Value="SO">Sur-Oeste</asp:ListItem>
                    <asp:ListItem Value="O">Oeste</asp:ListItem>
                    <asp:ListItem Value="NO">Norte-Oeste</asp:ListItem>
                </asp:DropDownList>
                <br />
                <br />
                <asp:Label ID="Label9" runat="server" Text="Tipo "></asp:Label>
                <%--Seleccionar si es vivienda u oficina--%>
                <asp:RadioButtonList ID="rbTipoApto" runat="server" AutoPostBack="true" OnSelectedIndexChanged="rbTipoApto_SelectedIndexChanged">
                    <asp:ListItem Value="vivienda">Vivienda</asp:ListItem>
                    <asp:ListItem Value="oficina">Oficina</asp:ListItem>
                </asp:RadioButtonList>
                <%--Información para ingresar una vivienda--%>
                <asp:Panel ID="pInfoVivienda" runat="server" BackColor="#F0F0F0" BorderStyle="Solid" BorderWidth="1px" GroupingText="Detalle vivienda" Visible="False">
                    <br />
                    <asp:Label ID="Label11" runat="server" Text="Cantidad Dormitorios: "></asp:Label>
                    <asp:TextBox ID="txtCantDormitorios" runat="server" Enabled="True"></asp:TextBox>
                    <br />
                    <asp:Label ID="Label18" runat="server" Text="Cantidad Baños: "></asp:Label>
                    <asp:TextBox ID="txtCantBanios" runat="server" Enabled="True"></asp:TextBox>
                    <br />
                    <asp:Label ID="Label12" runat="server" Text="Garage"></asp:Label>
                    <asp:RadioButtonList ID="rbGarage" runat="server" Enabled="True">
                        <asp:ListItem Value="si">Si</asp:ListItem>
                        <asp:ListItem Value="no">No</asp:ListItem>
                    </asp:RadioButtonList>
                    <br />
                </asp:Panel>
                <%--Información para ingresar una oficina--%>
                <asp:Panel ID="pInfoOficina" runat="server" BackColor="#F0F0F0" BorderStyle="Solid" BorderWidth="1px" GroupingText="Detalle oficina" Visible="False">
                    <br />
                    <asp:Label ID="Label3" runat="server" Text="Cantidad Puestos de Trabajo: "></asp:Label>
                    <asp:TextBox ID="txtCantPuestos" runat="server" Enabled="True"></asp:TextBox>
                    <br />
                    <br />
                    <asp:Label ID="Label10" runat="server" Text="Equipado"></asp:Label>
                    <asp:RadioButtonList ID="rbEquipado" runat="server" Enabled="True">
                        <asp:ListItem Value="si">Si</asp:ListItem>
                        <asp:ListItem Value="no">No</asp:ListItem>
                    </asp:RadioButtonList>
                    <br />
                    <br />
                    &nbsp;
                    <br />
                </asp:Panel>

                <br />
                <asp:Label ID="lblMensajeApartamento" runat="server"></asp:Label>
                <br />

                <br />
                <%--Boton para ingresar aptos en creación de edificio--%> 
                <asp:Button ID="btnAltaApto" runat="server" Enabled="True" Height="29px" OnClick="btnAltaApto_Click" Text="Ingresar Apartamento" />
                <%--Botón para ingresar aptos a edificios ya creados--%>
                <asp:Button ID="btnAltaApto2" runat="server" Enabled="True" OnClick="btnAltaApto2_Click" Text="Ingresar Apartamento" />
                <br />
            </asp:Panel>

            <%--Panel de filtros--%>
        </div>
        <asp:Panel ID="pFiltros" runat="server" BorderStyle="None" BorderWidth="0px" GroupingText="Filtros" Height="16px" Style="margin-top: 0px" Visible="false">
            <br />
            <asp:Menu ID="menuBuscadores" runat="server" OnMenuItemClick="menuBuscadores_MenuItemClick">
                <Items>
                    <asp:MenuItem Text="Filtrar por m2 y orientación" Value="filtroM2"></asp:MenuItem>
                    <asp:MenuItem Text="Filtrar por rango de precios" Value="filtroRangoPrecios" Selected="True"></asp:MenuItem>
                </Items>
            </asp:Menu>
            <%--Filtrar por m2--%>
            <div id="divRangoM2" runat="server" visible="False">
                <br />
&nbsp;<asp:Label ID="Label20" runat="server" Text="Comprobar si aptos edificios dentro del rango de precio especificado"></asp:Label>
&nbsp;
                <asp:Button ID="btnComprobarRango" runat="server" OnClick="ComprobarRango_Click" Text="Comprobar" />
                <br />
                <br />
                <asp:Label ID="Label5" runat="server" Text="Cantidad mínima de m2:"></asp:Label>
                <asp:TextBox ID="txtCantMinM2" runat="server"></asp:TextBox>
                <br />
                <asp:Label ID="Label8" runat="server" Text="Cantidad máxima de m2:"></asp:Label>
                <asp:TextBox ID="txtCantMaxM2" runat="server"></asp:TextBox>
                <br />
                <asp:Label ID="Label15" runat="server" Text="Orientación:"></asp:Label>
                <asp:DropDownList ID="ddlOrientacionFiltros" runat="server">
                    <asp:ListItem Value="vacio">Seleccionar</asp:ListItem>
                    <asp:ListItem Value="N">Norte</asp:ListItem>
                    <asp:ListItem Value="NE">Norte-Este</asp:ListItem>
                    <asp:ListItem Value="E">Este</asp:ListItem>
                    <asp:ListItem Value="SE">Sur-Este</asp:ListItem>
                    <asp:ListItem Value="S">Sur</asp:ListItem>
                    <asp:ListItem Value="SO">Sur-Oeste</asp:ListItem>
                    <asp:ListItem Value="O">Oeste</asp:ListItem>
                    <asp:ListItem Value="NO">Norte-Oeste</asp:ListItem>
                </asp:DropDownList>
                <br />
                <br />
                <asp:Button ID="btnFiltroM2" runat="server" OnClick="btnFiltroM2_Click" Text="Filtrar edificios" />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <br />
                <br />

                <asp:Label ID="lblImprimirEdificios" runat="server" Text=""></asp:Label>
                <%--Filtrar por precio--%>
            </div>
            <div id="divRangoPrecio" runat="server" visible="False">
                <br />
                <asp:Label ID="Label16" runat="server" Text="Precio mínimo:"></asp:Label>
                <asp:TextBox ID="txtPrecioMin" runat="server"></asp:TextBox>
                <br />
                <asp:Label ID="Label17" runat="server" Text="Precio máximo:"></asp:Label>
                <asp:TextBox ID="txtPrecioMax" runat="server"></asp:TextBox>
                <br />
                <asp:Button ID="btnFiltrarPrecios" runat="server" Text="Filtrar aptos." OnClick="btnFiltrarPrecios_Click" />
                <br />
                <br />

                <asp:Label ID="lblImprimirAptosPrecio" runat="server" Text=""></asp:Label>
            </div>
            <%--Mensaje de error para los filtros--%>
            <asp:Label ID="lblErrorFiltro" runat="server" Text=""></asp:Label>
        </asp:Panel>
    </form>
</body>

</html>

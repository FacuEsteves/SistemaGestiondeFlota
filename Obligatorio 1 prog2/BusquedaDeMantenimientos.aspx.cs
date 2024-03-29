﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Obligatorio_1_prog2
{
    public partial class BusquedaDeMantenimientos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < Global.transitoMaritimo.usuarios.Count; i++)
            {
                if (Global.transitoMaritimo.idUsuario == Global.transitoMaritimo.usuarios[i].nombreUsuario)
                {
                    this.Master.FindControl("Asignar_Tripulacion").Visible = Global.transitoMaritimo.usuarios[i].AsignarTripulacion;
                    this.Master.FindControl("IngresoCargos").Visible = Global.transitoMaritimo.usuarios[i].IngresarCargos;
                    this.Master.FindControl("IngresarTripulantes").Visible = Global.transitoMaritimo.usuarios[i].IngresarTripulantes;
                    this.Master.FindControl("IngresoEncargados").Visible = Global.transitoMaritimo.usuarios[i].IngresarEncargados;
                    this.Master.FindControl("IngresoMantenimiento").Visible = Global.transitoMaritimo.usuarios[i].IngresoMantenimiento;
                    this.Master.FindControl("IngresoTipoMantenimiento").Visible = Global.transitoMaritimo.usuarios[i].IngresoTipoMantenimiento;
                    this.Master.FindControl("IngresoUsuario").Visible = Global.transitoMaritimo.usuarios[i].IngresoUsuarios;
                    this.Master.FindControl("RegistroBarco").Visible = Global.transitoMaritimo.usuarios[i].RegistroBarco;
                    this.Master.FindControl("BusquedaDeMantenimientos").Visible = Global.transitoMaritimo.usuarios[i].BusquedaMant;
                    this.Master.FindControl("HistorialCambiosAccesos").Visible = Global.transitoMaritimo.usuarios[i].Historial;
                }
            }

            if (!Page.IsPostBack)
            {
                DD_Barco.DataSource = Persistencia.ListaBarcos();
                DD_Barco.DataTextField = "nombre";
                DD_Barco.DataValueField = "nombre";
                DD_Barco.DataBind();
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            //Errores

            if (DD_Barco.SelectedIndex == 0)
            {
                LabelError.Text = "Datos faltantes para realizar la busqueda";
                return;
            }

            if (DDMes.SelectedIndex == 0)
            {
                LabelError.Text = "Datos faltantes para realizar la busqueda";
                return;
            }

            if (txtAño.Text == "")
            {
                LabelError.Text = "Datos faltantes para realizar la busqueda";
                return;
            }

            //Creacion Tabla
            DataTable tabla1 = new DataTable();
            tabla1.Columns.Add("Fecha", typeof(string));
            tabla1.Columns.Add("Tipo", typeof(string));
            tabla1.Columns.Add("Precio", typeof(string));
            tabla1.Columns.Add("Total", typeof(int));

            //Variables Usadas
            string barco = DD_Barco.SelectedValue;
            string mes = DDMes.SelectedValue;
            int año = Convert.ToInt32(txtAño.Text);
            int TOTAL = 0;
            int comp = 0;

            //Comienzo Busqueda
            for (int i = 0; i < Global.transitoMaritimo.mantenimientos.Count; i++)
            {
                if (barco==Global.transitoMaritimo.mantenimientos[i].nombreBarco && mes == Convert.ToString(Global.transitoMaritimo.mantenimientos[i].fechaMantenimiento.Month) && año == Global.transitoMaritimo.mantenimientos[i].fechaMantenimiento.Year)
                {
                    //Asignacion de Valores a la Tabla
                    string fecha = Convert.ToString(Global.transitoMaritimo.mantenimientos[i].fechaMantenimiento);
                    string tipo = Global.transitoMaritimo.mantenimientos[i].descripcion;
                    string precio = Convert.ToString(Global.transitoMaritimo.mantenimientos[i].precio);
                    TOTAL = Global.transitoMaritimo.mantenimientos[i].precio + TOTAL;

                    tabla1.Rows.Add(fecha, tipo, precio);
                    comp = 1;

                }
            }
            tabla1.Rows.Add(' ', ' ', ' ', TOTAL);

            if (comp == 0)
            {
                LabelError.Text = "Mantenimiento No Encontrado";
            }
            else
            {
                LabelError.Text = "";
            }

            //Mostrar
            GridView1.DataSource = tabla1;
            GridView1.DataBind();
        }
    }
}
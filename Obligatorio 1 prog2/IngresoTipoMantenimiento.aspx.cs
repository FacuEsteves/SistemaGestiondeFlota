﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Obligatorio_1_prog2
{
    public partial class IngresoTipoMantenimiento : System.Web.UI.Page
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

            GridTiposMantenimiento.DataSource = Global.transitoMaritimo.tiposMantenimiento;
            GridTiposMantenimiento.DataBind();
        }

        protected void BtnGuardar_Click(object sender, EventArgs e)
        {
            LabelError.Text = "";


            //COMIENZO ERRORES
            if (TxtCodigo.Text == "")
            {
                LabelError.Text = "Ingrese el codigo";
                return;
            }
            if (TxtPrecioBase.Text == "")
            {
                LabelError.Text = "Ingrese el precio base";
                return;
            }
            if (TxtDescripcion.Text == "")
            {
                LabelError.Text = "Ingrese la descripción";
                return;
            }
            //FIN ERRORES

            Tipo_de_Mantenimiento tm = new Tipo_de_Mantenimiento();
            bool existe = false;

            //BUSCAR TIPOS DE MANTENIMEINTOS REGISTRADOS
            for (int i = 0; i < Global.transitoMaritimo.tiposMantenimiento.Count; i++)
            {
                if (Global.transitoMaritimo.tiposMantenimiento[i] != null)
                {
                    if (Convert.ToInt32(TxtCodigo.Text) == Global.transitoMaritimo.tiposMantenimiento[i].codigo)
                    {
                        LabelError.Text = "Ya se encuentra ingresado este tipo de mantenimiento";
                        tm = Global.transitoMaritimo.tiposMantenimiento[i];
                        tm.codigo = Convert.ToInt32(TxtCodigo.Text);
                        tm.precioBase = Convert.ToInt32(TxtPrecioBase.Text);
                        tm.descripcion = TxtDescripcion.Text;
                        existe = true;
                        break;
                    }
                }
            }
            //FIN BUSCAR

            //COMIENZO GUARDADO
            if (existe == false)
            {
                tm.codigo = Convert.ToInt32(TxtCodigo.Text);
                tm.precioBase = Convert.ToInt32(TxtPrecioBase.Text);
                tm.descripcion = TxtDescripcion.Text;
                Global.transitoMaritimo.tiposMantenimiento.Add(tm);
            }
            //FIN GUARDADO
            Persistencia.RegistroCambio(Global.transitoMaritimo.idUsuario, "Ingreso tipo mantenimiento");
            Persistencia.guardarDatos();

            //cargar grid 
            GridTiposMantenimiento.DataSource = Global.transitoMaritimo.tiposMantenimiento;
            GridTiposMantenimiento.DataBind();



            //LIMPIAR CAMPOS
            TxtCodigo.Text = "";
            TxtPrecioBase.Text = "";
            TxtDescripcion.Text = "";
            LabelError.Text = "";
        }

        protected void GridTiposMantenimiento_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            String codigo = e.Values["codigo"].ToString();

            for (int i = 0; i < Global.transitoMaritimo.tiposMantenimiento.Count; i++)
            {
                if (Global.transitoMaritimo.tiposMantenimiento[i] != null)
                {
                    if (Convert.ToInt32(codigo) == Global.transitoMaritimo.tiposMantenimiento[i].codigo)
                    {
                        LabelError.Text = "Se elimino el tipo de mantenimiento " + Global.transitoMaritimo.tiposMantenimiento[i].descripcion;
                        Global.transitoMaritimo.tiposMantenimiento.Remove(Global.transitoMaritimo.tiposMantenimiento[i]);
                        break;
                    }
                }
            }

            Persistencia.RegistroCambio(Global.transitoMaritimo.idUsuario, "Borrar tipo de mantenimiento");
            Persistencia.guardarDatos();

            GridTiposMantenimiento.DataBind();
        }
    }
}
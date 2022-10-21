﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Obligatorio_1_prog2
{
    public partial class IngresoUsuario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            GridUsuario.DataSource = Global.transitoMaritimo.usuarios;
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            Usuario us = new Usuario();
            us.nombre = txtNombre.Text;
            us.cedula = Convert.ToInt32(txtCedula.Text);
            us.nombreUsuario = txtID.Text;
            us.contraseña = txtContraseña.Text;
            us.correo = txtCorreo.Text;
            Global.transitoMaritimo.usuarios.Add(us);
            GridUsuario.DataBind();
        }
    }
}
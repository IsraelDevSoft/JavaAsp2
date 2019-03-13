using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VideotiendaWFApp.Models;

namespace VideotiendaWFApp.Views
{
    public partial class FrmGestionarDominios : Form
    {
        dominios oDominio = null;
        private String tipoDominio;
        private String idDominio;

        public FrmGestionarDominios(String tipoDominio, string idDominio)
        {
            //dibujar el formulario
            InitializeComponent();
            //recibir los datos de la pk(si son nulos estamos insertando. si hay datos estamos editando)
            this.tipoDominio = tipoDominio;
            this.idDominio = idDominio;            
            //si hay datos (edicion), llamamos a cargarDatos()
            if(!string.IsNullOrEmpty(this.idDominio) && !string.IsNullOrEmpty(this.tipoDominio))
            {
                //si es modo edicion bloqueamos los texbox de la llave PK
                cargarDatos();
                this.txtTipo.ReadOnly = true;
                this.txtId.ReadOnly = true;
            }
            else
            {
                //si es modo insercion habilitamos los texbox de la PK
                this.txtTipo.ReadOnly = false;
                this.txtId.ReadOnly = false;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void FrmGestionarDominios_Load(object sender, EventArgs e)
        {
            this.txtTipo.Select();
        }

        private void cargarDatos()
        {
            using(videotiendaEntities db = new videotiendaEntities())
            {
                oDominio = db.dominios.Find(tipoDominio, idDominio);
                txtTipo.Text = oDominio.TIPO_DOMINIO;
                txtId.Text = oDominio.ID_DOMINIO;
                txtValor.Text = oDominio.VLR_DOMINIO;
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(this.txtTipo.Text) || string.IsNullOrEmpty(this.txtId.Text) || string.IsNullOrEmpty(this.txtValor.Text))
            {
                MessageBox.Show("Los campos marcados con (*) son obligatorios");
            }
            else
            {
                //establecer conexion con la BD a traves de EF
                using (videotiendaEntities db = new videotiendaEntities())
                {
                    //si estamos en modo insercion inicializamos el objeto dominio
                    if(this.tipoDominio == null && this.idDominio == null)
                    {
                        oDominio = new dominios();
                    }
                    
                    oDominio.TIPO_DOMINIO = this.txtTipo.Text;
                    oDominio.ID_DOMINIO = this.txtId.Text;
                    oDominio.VLR_DOMINIO = this.txtValor.Text;
                    
                    //en modo insercion adicionamos un nuevo registro 
                    if(this.tipoDominio == null && this.idDominio == null)
                    {
                        db.dominios.Add(oDominio);
                    }

                    else
                    {
                        //en modo edicion cambiamos el estado del objeto a modificacion
                        db.Entry(oDominio).State = System.Data.Entity.EntityState.Modified;            
                      
                        
                        
                    }
                    //confirmar cambios en la BD
                    db.SaveChanges();
                    //cerrar el fomulario y volver al formulario de datos
                    this.Close();

                }

            }
        }
    }
}

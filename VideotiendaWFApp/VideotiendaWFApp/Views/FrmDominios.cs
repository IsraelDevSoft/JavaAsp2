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
using VideotiendaWFApp.Views;

namespace VideotiendaWFApp.Views
{
    public partial class FrmDominios : Form
    {
        public FrmDominios()
        {
            InitializeComponent();
        }

        private void FrmDominios_Load(object sender, EventArgs e)
        {
            refrescarTabla();
            this.txtTipo.Select();
        }

        #region Helper

        public void refrescarTabla()
        {
            using (videotiendaEntities db = new videotiendaEntities())
            {
                var lstDominios = from d in db.dominios
                                  select d;
                grdDatos.DataSource = lstDominios.ToList();

            }


        }

        private dominios getSelectedItem()
        {
            dominios d = new dominios();
            try
            {
                d.TIPO_DOMINIO = grdDatos.Rows[grdDatos.CurrentRow.Index].Cells[0].Value.ToString();
                d.ID_DOMINIO = grdDatos.Rows[grdDatos.CurrentRow.Index].Cells[1].Value.ToString();
                return d;
            }catch{
                 return null;
            }
        }
        #endregion

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            using (videotiendaEntities db = new videotiendaEntities())
            {
                //consultar todos los dominios
                var lstDominios = from d in db.dominios select d;
                //aplicar filtros ingresados por el usuario
                if (!string.IsNullOrEmpty(this.txtTipo.Text))
                {
                    lstDominios = lstDominios.Where(d => d.TIPO_DOMINIO.Contains(this.txtTipo.Text));
                }
                if (!string.IsNullOrEmpty(this.txtId.Text))
                {
                    lstDominios = lstDominios.Where(d => d.ID_DOMINIO.Contains(this.txtId.Text));
                }
                if (!string.IsNullOrEmpty(this.txtValor.Text))
                {
                    lstDominios = lstDominios.Where(d => d.VLR_DOMINIO.Contains(this.txtValor.Text));
                }

                grdDatos.DataSource = lstDominios.ToList();               
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            this.txtTipo.Text = "";
            this.txtId.Text = "";
            this.txtValor.Text = "";
            refrescarTabla();

        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Views.FrmGestionarDominios frmGestionarDominios = new Views.FrmGestionarDominios(null, null);
            frmGestionarDominios.ShowDialog();

            refrescarTabla();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            //obtener dominio que se selecciono
            dominios d = getSelectedItem();
            if(d != null)
            {
                Views.FrmGestionarDominios frmGestionarDominios = 
                    new Views.FrmGestionarDominios(d.TIPO_DOMINIO, d.ID_DOMINIO);
                frmGestionarDominios.ShowDialog();
                refrescarTabla();
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            //obtener el dominio que se va eleminar 
            dominios d = this.getSelectedItem();
            //hubo seleccion
            if (d != null)
            {
                if (MessageBox.Show("Esta seguro que desea eliminar este registro?", "Confimacion",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
                {
                    //establecer conexion con la BD a traves de EF
                    using (videotiendaEntities db = new videotiendaEntities())
                    {
                        //buscar el dominio en la BD
                        dominios dEliminar = db.dominios.Find(d.TIPO_DOMINIO, d.ID_DOMINIO);
                        //eliminar el dominio de la tabla
                        db.dominios.Remove(dEliminar);
                        //confirmar cambios en la BD
                        db.SaveChanges();
                    }
                    //actualizar la tabla de datos
                    this.refrescarTabla();
                }
            }
        }
    }
}

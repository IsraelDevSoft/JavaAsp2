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
            using(videotiendaEntities db = new videotiendaEntities())
            {
                var lstDominios = from d in db.dominios
                                  select d;
                grdDatos.DataSource = lstDominios.ToList();

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
        }
    }
}

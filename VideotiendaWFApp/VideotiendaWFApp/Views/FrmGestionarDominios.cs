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
            InitializeComponent();
            this.tipoDominio = tipoDominio;
            this.idDominio = idDominio;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void FrmGestionarDominios_Load(object sender, EventArgs e)
        {
            this.txtTipo.Select();
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
                using(videotiendaEntities db = new videotiendaEntities())
                {
                    oDominio = new dominios();
                    oDominio.TIPO_DOMINIO = this.txtTipo.Text;
                    oDominio.ID_DOMINIO = this.txtId.Text;
                    oDominio.VLR_DOMINIO = this.txtValor.Text;
                    db.dominios.Add(oDominio);
                    db.SaveChanges();
                    this.Close();
                }

            }
        }
    }
}

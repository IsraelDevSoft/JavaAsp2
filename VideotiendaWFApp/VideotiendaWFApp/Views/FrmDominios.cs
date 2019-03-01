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
    public partial class FrmDominios : Form
    {
        public FrmDominios()
        {
            InitializeComponent();
        }

        private void FrmDominios_Load(object sender, EventArgs e)
        {
            refrescarTabla();
        }

        #region Helper

        public void refrescarTabla()
        {
            using(videotiendaEntities db = new videotiendaEntities())
            {
                var lstDominios = from d in db.dominios
                                  select d;
                grdDominios.DataSource = lstDominios.ToList();

            }
        }
        #endregion
    }
}

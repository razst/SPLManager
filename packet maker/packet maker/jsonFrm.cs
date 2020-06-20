using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace packet_maker
{
    public partial class jsonFrm : Form
    {
        public jsonFrm()
        {
            InitializeComponent();
        }

        public TypeList usedJson = new TypeList();
        public static jsonFrm curJsonFrm = null;

        private void typeLibx_SelectedIndexChanged(object sender, EventArgs e)
        {
            subtypeLibx.Items.Clear();
            paramsLisbx.Items.Clear();

            foreach (SubType subTy in usedJson.typenum[typeLibx.SelectedIndex])
            {
                subtypeLibx.Items.Add(subTy.name);
            }

        }

        private void jsonFrm_Load(object sender, EventArgs e)
        {
            curJsonFrm = this;
            foreach (Type ty in usedJson)
            {
                typeLibx.Items.Add(ty.name);
            }
        }

        private void subtypeLibx_SelectedIndexChanged(object sender, EventArgs e)
        {
            paramsLisbx.Items.Clear();

            foreach (Params par in usedJson.typenum[typeLibx.SelectedIndex].subTypes[subtypeLibx.SelectedIndex])
            {
                paramsLisbx.Items.Add(par.name);
            }

        }

        private void typeLibx_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            jsonValSetFrm frm = new jsonValSetFrm();
            frm.SetObject(usedJson.typenum[typeLibx.SelectedIndex]);
            frm.ShowDialog();
        }

        private void subtypeLibx_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            jsonValSetFrm frm = new jsonValSetFrm();
            frm.SetObject(usedJson.typenum[typeLibx.SelectedIndex].subTypes[subtypeLibx.SelectedIndex]);
            frm.ShowDialog();
        }

        private void paramsLisbx_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            jsonValSetFrm frm = new jsonValSetFrm();
            frm.SetObject(usedJson.typenum[typeLibx.SelectedIndex].subTypes[subtypeLibx.SelectedIndex].parmas[paramsLisbx.SelectedIndex]);
            frm.ShowDialog();
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SPL_Manager.Models.SatPacketModels.JsonModels;
using SPL_Manager.Presenters.QueryTabPresenters;
using SPL_Manager.Views.QueryTabViews;

namespace SPL_Manager.Views.QueryTabViews
{
    public partial class QueryTab : UserControl,
        IQuerySelectionView,
        IQueryPacketDisplayView
    {
        public QueryTab()
        {
            InitializeComponent();
        }
        public void Init()
        {
            QuerySelctionPresenter = new QuerySelctionPresenter(this);
            QueryPacketDisplayPresenter = new QueryPacketDisplayPresenter(this);

            JsonService service = new JsonService(ProgramProps.PacketJsonFiles["Rx"]);

            QrySubtypeCB.Items.AddRange(service.GetAllSubtypes().ConvertAll(sub => sub.Name).ToArray());

            QryGroupsCB.Items.AddRange(ProgramProps.groups.ToArray());
            QryGroupsCB.SelectedIndex = (int)ProgramProps.settings.defaultSatGroup;
            QryLimitCB.SelectedIndex = 0;
        }

        private void TxPacQryLibx_SelectedIndexChanged(object sender, EventArgs e)
        {
            QueryPacketDisplayPresenter.UpdateDisplayedPacket("Tx");
        }
        private void RxPacQryLibx_SelectedIndexChanged(object sender, EventArgs e)
        {
            QueryPacketDisplayPresenter.UpdateDisplayedPacket("Rx");
        }

        private void QryFieldChbx_CheckedChanged(object sender, EventArgs e) => FieldOptionsPanel.Visible = QryFieldChbx.Checked;
        private void QryIdChbx_CheckedChanged(object sender, EventArgs e) => QryIdTxb.ReadOnly = !QryIdChbx.Checked;
        private void QryFieldCB_SelectedIndexChanged(object sender, EventArgs e) => QuerySelctionPresenter.UpdateFieldType();
        private void QrySubtypeCB_SelectedIndexChanged(object sender, EventArgs e) => QuerySelctionPresenter.UpdateSubtype();
        private void QryStartBtn_Click(object sender, EventArgs e) => QueryPacketDisplayPresenter.AddAllPacketsToView();
        private void QryClearBtn_Click(object sender, EventArgs e) => QueryPacketDisplayPresenter.ClearQueryTab();


        public void ClearPacketsOnDisplay()
        {
            QryPacketDetailsTxb.Clear();
            RxPacQryLibx.Items.Clear();
            TxPacQryLibx.Items.Clear();
        }

        public QuerySelctionPresenter QuerySelctionPresenter { get; set; }

        public QueryPacketDisplayPresenter QueryPacketDisplayPresenter { get; set; }


        public DateTime QueryMinTime => QryMinDateDtp.Value.ToUniversalTime();
        public DateTime QueryMaxTime => QryMaxDateDtp.Value.ToUniversalTime();

        public int QuerySatGroupIndex => QryGroupsCB.SelectedIndex;
        public string QuerySubtype => QrySubtypeCB.Text;
        public string QuerySizeLimit => QryLimitCB.Text;

        public bool IsTxQuery => qryTxChbx.Checked;
        public bool IsRxQuery => qryRxChbx.Checked;
        public bool IsIdQuery => QryIdChbx.Checked;
        public bool IsFieldSpecificQuery => QryFieldChbx.Checked;

        public string QueryTargetId => QryIdTxb.Text;
        public List<string> QueryFileds 
        { 
            set 
            {
                QryFieldCB.Items.Clear();
                QryFieldCB.Items.AddRange(value.ToArray());
            } 
        }

        public string QueryTargetField => QryFieldCB.Text;
        public bool TargetFieldIsDate 
        {
            get => QryConditionValueDtp.Visible;
            set
            {
                QryConditionValueDtp.Visible = value;
                QryConditionValueTxb.Visible = !value;
            }
        }
        public string QueryFieldOprt => QryFiledConditionCB.Text;
        public string QueryTargetFieldValueStr => QryConditionValueTxb.Text;
        public DateTime QueryTargetFieldValueDate => QryConditionValueDtp.Value.ToUniversalTime();



        // Displaying Packets
        public int SelectedRxIndex { get => RxPacQryLibx.SelectedIndex; set => RxPacQryLibx.SelectedIndex = value; }
        public int SelectedTxIndex { get => TxPacQryLibx.SelectedIndex; set => TxPacQryLibx.SelectedIndex = value; }
        public string PacketDetails { set => QryPacketDetailsTxb.Text = value; }
        
        public void AddRxPacket(string displayPacket)
        {
            RxPacQryLibx.Items.Add(displayPacket);
        }

        public void AddTxPacket(string displayPacket)
        {
            TxPacQryLibx.Items.Add(displayPacket);
        }


    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using SPL_Manager.Library.PacketLifecycle.Query.Simple;
using SPL_Manager.Library.PacketModel;
using SPL_Manager.Library.PacketLifecycle.Query.Advanced;
using SPL_Manager.Library.Shared;

namespace SPL_Manager.UI.Views.QueryTabViews
{
    public partial class QueryTab : UserControl,
        IAdvancedQueryView,
        IQueryResultsView
    {
        public QueryTab()
        {
            InitializeComponent();

            if(!DesignMode)
            {
                QuerySelctionPresenter = new AdvancedQueryPresenter();
                QueryPacketDisplayPresenter = new AdvancedQueryResultsPresenter();

                QuerySelctionPresenter.SetView(this);
                QueryPacketDisplayPresenter.SetView(this);

                PacketProtocolService service = new PacketProtocolService(ProgramProps.PacketJsonFiles["Rx"]);

                QrySubtypeCB.Items.AddRange(service.GetAllSubtypes().ConvertAll(sub => sub.Name).ToArray());

                QryGroupsCB.Items.AddRange(ProgramProps.groups.ToArray());
                QryGroupsCB.SelectedIndex = (int)ProgramProps.settings.defaultSatGroup;
                QryLimitCB.SelectedIndex = 0;
            }
        }




        public void AlertUser(string title, string message)
        {
            CustomViewImplementation.AlertUser(title, message);
        }

        public void NotifyUser(string title, string message)
        {
            CustomViewImplementation.NotifyUser(title, message);
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

        public AdvancedQueryPresenter QuerySelctionPresenter { get; set; }

        public AdvancedQueryResultsPresenter QueryPacketDisplayPresenter { get; set; }


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

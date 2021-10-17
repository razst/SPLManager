using SPL_Manager.Presenters.MainTabPresenters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SPL_Manager.Views.MainTabViews
{
    public partial class MainTab : UserControl,
        IRadioPassesView,
        ISatDataView
    {
        public MainTab()
        {
            InitializeComponent();
        }
        public void Init()
        {
            HandleDestroyed += new EventHandler(OnClosing);

            RadioPassesPresenter = new RadioPassesPresenter(this);
            SatDataPresenter = new SatDataPresenter(this);

            MainGroupsCB.Items.AddRange(ProgramProps.groups.ToArray());


            //Gauges Setup
            VBatGauge.ColorOfG = new Dictionary<string, AquaControls.valuesOfColors>
            {
                {"healthy", new AquaControls.valuesOfColors{color = Color.LawnGreen , minVal = 7.5F,maxVal = 9 , threshVal = 8.25F, threshPrecent = 16.66F }},
                {"bad", new AquaControls.valuesOfColors{color = Color.Red , minVal = 0,maxVal = 7 , threshVal = 3.5F , threshPrecent = 77.77F}},
                {"notgreat", new AquaControls.valuesOfColors{color = Color.OrangeRed, minVal = 7,maxVal = 7.5F , threshVal = 7.25F, threshPrecent = 5.55F}}


            };
            OBCTempGauge.ColorOfG = new Dictionary<string, AquaControls.valuesOfColors>
            {
                {"healthy", new AquaControls.valuesOfColors{color = Color.LawnGreen , minVal = -15F,maxVal = 30, threshVal = 7.5F, threshPrecent= 39 }},
                {"bad", new AquaControls.valuesOfColors{color = Color.Red , minVal = 45,maxVal = 80 , threshVal = 62.5F , threshPrecent = 32 }},
                {"notgreat", new AquaControls.valuesOfColors{color = Color.OrangeRed, minVal = 30,maxVal = 45 , threshVal = 37.5F , threshPrecent = 13}},
                {"bad2", new AquaControls.valuesOfColors{color = Color.Red , minVal = -40,maxVal = -20 , threshVal = -30F , threshPrecent = 20 }},
                {"notgreat2", new AquaControls.valuesOfColors{color = Color.OrangeRed, minVal = -20,maxVal = -15 , threshVal = -17.5F , threshPrecent = 4.6F}},
            };
            BatTempGauge.ColorOfG = new Dictionary<string, AquaControls.valuesOfColors>
            {
                {"healthy", new AquaControls.valuesOfColors{color = Color.LawnGreen , minVal = -15F,maxVal = 30, threshVal = 7.5F, threshPrecent= 39 }},
                {"bad", new AquaControls.valuesOfColors{color = Color.Red , minVal = 45,maxVal = 80 , threshVal = 62.5F , threshPrecent = 32 }},
                {"notgreat", new AquaControls.valuesOfColors{color = Color.OrangeRed, minVal = 30,maxVal = 45 , threshVal = 37.5F , threshPrecent = 13}},
                {"bad2", new AquaControls.valuesOfColors{color = Color.Red , minVal = -40,maxVal = -20 , threshVal = -30F , threshPrecent = 20 }},
                {"notgreat2", new AquaControls.valuesOfColors{color = Color.OrangeRed, minVal = -20,maxVal = -15 , threshVal = -17.5F , threshPrecent = 4.6F}},
            };
            FreeSpaceGauge.ColorOfG = new Dictionary<string, AquaControls.valuesOfColors>
            {
                {"healthy", new AquaControls.valuesOfColors{color = Color.LawnGreen , minVal = 800,maxVal = 2200, threshVal = 1500, threshPrecent= 63.63F }},
                {"bad", new AquaControls.valuesOfColors{color = Color.Red , minVal = 0,maxVal = 300 , threshVal = 150 , threshPrecent = 13.63F }},
                {"notgreat", new AquaControls.valuesOfColors{color = Color.OrangeRed, minVal = 300 ,maxVal = 800 , threshVal = 550 , threshPrecent = 22.72F}},
            };
        }

        public int MainTabGroupIndex => MainGroupsCB.SelectedIndex;
        private async void MainGroupsCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            ((ISatDataView)this).Clear();
            ((IRadioPassesView)this).Clear();

            await RadioPassesPresenter.LoadNextRadioPasses();
            await SatDataPresenter.UpdateSatDataView();
        }
        private void OnClosing(object sender, EventArgs e)
        {
            RadioPassesPresenter.Dispose();
        }

        // Radio Pass
        public string TimeTillNextPass { set => MainTimeTillPassLbl.Text = value; }
        public List<string> NextPassesDates
        {
            set
            {
                MainNextPassLbl1.Text = value[1];
                MainNextPassLbl2.Text = value[2];
                MainNextPassLbl3.Text = value[3];
                MainNextPassLbl4.Text = value[4];
            }
        }
        public string RadioPassStatus { set => MainPassStatusLbl.Text = value; }
        public string LastPassDate { set => MainLastPassLbl.Text = value; }

        public string UtcDate { set => MainUtcTimeLbl.Text = value; }

        public RadioPassesPresenter RadioPassesPresenter { private get; set; }


        //Sat Data
        public string LastRxFrameDate { set => MainLastRxLbl.Text = value; }
        public string LastTxFrameDate { set => MainLastTxLbl.Text = value; }
        public string LastBeaconDate { set => MainLastBeaconLbl.Text = value; }
        public string OBCTimeDate { set => MainSatTimeLbl.Text = value; }

        public string NumberOfSatResets { set => MainSatResetsLbl.Text = value; }
        public string NumberOfCmdResets { set => MainCmdRestesLbl.Text = value; }
        public string NumberOfCorruptBytes { set => MainCorruptBytesLbl.Text = value; }

        public string SatUptimeStr { set => MainSatUptimeLbl.Text = value; }

        public float FreeSpaceValue { set => FreeSpaceGauge.Value = value; }
        public float BatTempValue { set => BatTempGauge.Value = value; }
        public float OBCTempValue { set => OBCTempGauge.Value = value; }
        public float VBatValue { set => VBatGauge.Value = value; }

        public SatDataPresenter SatDataPresenter { private get; set; }

    }
}

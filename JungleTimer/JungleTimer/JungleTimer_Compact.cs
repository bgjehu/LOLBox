using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;


namespace JungleTimer
{
    public partial class JungleTimer_Compact : Form
    {
        //Variables
        int[] CampsCountDownTimers = new int[14];
        Button[] Camps = new Button[14];
        bool isStarted = false;
        bool isPaused = false;
        bool isTimeMin = true;
        int GameTime = 0;
        SoundPlayer Live = new SoundPlayer(Resource1.Live);

        //Main Thread
        public JungleTimer_Compact()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            Map();
            TimersReset();
            
        }

        //Functions
        //Map
        private void Map()
        {
            Camps[0] = button1;
            Camps[1] = button2;
            Camps[2] = button3;
            Camps[3] = button4;
            Camps[4] = button5;
            Camps[5] = button6;
            Camps[6] = button7;
            Camps[7] = button8;
            Camps[8] = button9;
            Camps[9] = button10;
            Camps[10] = button11;
            Camps[11] = button12;
            Camps[12] = button13;
            Camps[13] = button14;
        }
        //TimersReset
        private void TimersReset()
        {
            for (int i = 0; i < 8; i++) { CampsCountDownTimers[i] = 125; }
            for (int i = 8; i < 12; i++) { CampsCountDownTimers[i] = 115; }
            CampsCountDownTimers[12] = 150;
            CampsCountDownTimers[13] = 900;
            for (int i = 0; i < 14; i++)
            {
                Camps[i].Text = NTT(CampsCountDownTimers[i]);
                Camps[i].Image = null;
            }
        }
        private void TimersReset(int i)
        {
            switch (i)
            {
                case 0:
                case 1:
                case 2:
                case 3:
                case 4:
                case 5:
                case 6:
                case 7:
                    CampsCountDownTimers[i] = 50;
                    break;
                case 8:
                case 9:
                case 10:
                case 11:
                    CampsCountDownTimers[i] = 300;
                    break;
                case 12:
                    CampsCountDownTimers[i] = 360;
                    break;
                case 13:
                    CampsCountDownTimers[i] = 420;
                    break;

            }
        }
        //NumberToTime
        private string NTT(int a)
        {
            string tmp = null;
            if (a > 60 && isTimeMin == true)
            {
                tmp = (a / 60).ToString() + ":" + (a % 60).ToString("D2");
            }
            else { tmp = a.ToString(); }
            return tmp;
        }
        //GameTimeFormatting
        private string GTF(int a)
        {
            string tmp = null;
            tmp = (a / 60).ToString("D2") + ":" + (a % 60).ToString("D2");
            return tmp;
        }

        //Events
        private void Button_Mode_Click(object sender, EventArgs e)
        {
            GlobalVar.Mode = 2;
            GlobalVar.isSwitchingMode = true;
            this.Close();
        }

        private void JungleTimer_Compact_Load(object sender, EventArgs e)
        {
            
        }

        private void Button_Start_Click(object sender, EventArgs e)
        {
            if (!isStarted)
            {
                Label_GameTime.Visible = true;
                Button_Start.Text = "STOP";
                isStarted = true;
                Timer_Compact.Start();
                
            }
            else
            {
                Label_GameTime.Visible = false;
                Button_Start.Text = "START";
                isStarted = false;
                Timer_Compact.Stop();
                isPaused = false;
                Button_Pause.Text = "Pause";
                TimersReset();
            }
        }

        private void Timer_Compact_Tick(object sender, EventArgs e)
        {
            GameTime++;
            Label_GameTime.Text = GTF(GameTime);
            for (int i = 0; i < 14; i++)
            {
                if (CampsCountDownTimers[i] > 0)
                {
                    CampsCountDownTimers[i]--;
                    Camps[i].Text = NTT(CampsCountDownTimers[i]);
                    switch (i)
                    {
                        case 0:
                        case 2:
                        case 4:
                        case 6:
                            Camps[i].BackgroundImage = Properties.Resources.Ghost_Gray;
                            break;
                        case 1:
                        case 5:
                            Camps[i].BackgroundImage = Properties.Resources.Wolve_Gray;
                            break;
                        case 3:
                        case 7:
                        case 9:
                        case 11:
                            Camps[i].BackgroundImage = Properties.Resources.Blue_Gray;
                            break;
                        case 8:
                        case 10:
                            Camps[i].BackgroundImage = Properties.Resources.Red_Gray;
                            break;
                        case 12:
                            Camps[i].BackgroundImage = Properties.Resources.Dragon_Gray;
                            break;
                        case 13:
                            Camps[i].BackgroundImage = Properties.Resources.Baron_Gray;
                            break;
                    }
                }
                else if (CampsCountDownTimers[i] == 0)
                {
                    Live.Play();
                    switch (i)
                    {
                        case 0:
                        case 2:
                        case 4:
                        case 6:
                            Camps[i].BackgroundImage = Properties.Resources.Ghost;
                            break;
                        case 1:
                        case 5:
                            Camps[i].BackgroundImage = Properties.Resources.Wolve;
                            break;
                        case 3:
                        case 7:
                        case 9:
                        case 11:
                            Camps[i].BackgroundImage = Properties.Resources.Blue;
                            break;
                        case 8:
                        case 10:
                            Camps[i].BackgroundImage = Properties.Resources.Red;
                            break;
                        case 12:
                            Camps[i].BackgroundImage = Properties.Resources.Dragon;
                            break;
                        case 13:
                            Camps[i].BackgroundImage = Properties.Resources.Baron;
                            break;
                    }
                    Camps[i].Text = "Live";
                    CampsCountDownTimers[i]--;
                }
                else { }
            }
        }

        private void Button_Pause_Click(object sender, EventArgs e)
        {
            if (!isPaused)
            {
                Timer_Compact.Stop();
                isPaused = true;
                Button_Pause.Text = "RESUME";
            }
            else
            {
                Timer_Compact.Start();
                isPaused = false;
                Button_Pause.Text = "PAUSE";
            }
        }

        private void Button_TimeFormat_Click(object sender, EventArgs e)
        {
            if (isTimeMin)
            {
                isTimeMin = false;
                Button_TimeFormat.Text = "MIN";
            }
            else
            {
                isTimeMin = true;
                Button_TimeFormat.Text = "SEC";
            }
        }

        private void Button_Backward_Click(object sender, EventArgs e)
        {
            GameTime = GameTime + 5;
            Label_GameTime.Text = GTF(GameTime);
            for (int i = 0; i < 14; i++)
            {
                CampsCountDownTimers[i] = CampsCountDownTimers[i] - 5;
                if (CampsCountDownTimers[i] < 0) { CampsCountDownTimers[i] = 0; }
                Camps[i].Text = NTT(CampsCountDownTimers[i]);
            }
        }

        private void Button_Forward_Click(object sender, EventArgs e)
        {
            GameTime = GameTime - 5;
            if (GameTime < 0) { GameTime = 0; }
            Label_GameTime.Text = GTF(GameTime);
            for (int i = 0; i < 14; i++)
            {
                CampsCountDownTimers[i] = CampsCountDownTimers[i] + 5;
                Camps[i].Text = NTT(CampsCountDownTimers[i]);
            }
        }

        private void Button_Help_Click(object sender, EventArgs e)
        {
            MessageBox.Show(Resource1.help_info, "HELP");
        }

        private void Button_About_Click(object sender, EventArgs e)
        {
            MessageBox.Show(Resource1.about_info, "ABOUT");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            TimersReset(0);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            TimersReset(1);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            TimersReset(2);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            TimersReset(3);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            TimersReset(4);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            TimersReset(5);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            TimersReset(6);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            TimersReset(7);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            TimersReset(8);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            TimersReset(9);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            TimersReset(10);
        }

        private void button12_Click(object sender, EventArgs e)
        {
            TimersReset(11);
        }

        private void button13_Click(object sender, EventArgs e)
        {
            TimersReset(12);
        }

        private void button14_Click(object sender, EventArgs e)
        {
            TimersReset(13);
        }

        
    }
}

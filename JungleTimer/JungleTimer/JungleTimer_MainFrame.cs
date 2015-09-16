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
    public partial class JungleTimer : Form
    {
        /*          Index List
         * 0    -   Blue Side Ghost
         * 1    -   Blue Side Wolves
         * 2    -   Blue Side Ghosts
         * 3    -   Blue Side Golems
         * 4    -   Purple Side Ghost   
         * 5    -   Purple Side Wolves  
         * 6    -   Purple Side Ghosts
         * 7    -   Purple Side Golems
         * ----------------------------
         * 8    -   Blue Side Red
         * 9    -   Blue Side Blue
         * 10   -   Purple Side Red
         * 11   -   Purple Side Blue
         * ----------------------------
         * 12   -   Dragon
         * 13   -   Baron
         */

        //Initialize Variables
        int[] CampsCountDownTimers = new int[14];
        Label[] CampsLabel = new Label[14];
        PictureBox[] CampsPictureBox = new PictureBox[14];
        bool isStarted = false;
        bool isPaused = false;
        bool isMapOn = true;
        bool isTimeMin = true;
        SoundPlayer Live = new SoundPlayer(Resource1.Live);
        int GameTime = 0;
        
        
        //Main Thread
        public JungleTimer()
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
            CampsLabel[0] = Label_Blue_Ghost;
            CampsLabel[1] = Label_Blue_Wolves;
            CampsLabel[2] = Label_Blue_Ghosts;
            CampsLabel[3] = Label_Blue_Golems;
            CampsLabel[4] = Label_Purple_Ghost;
            CampsLabel[5] = Label_Purple_Wolves;
            CampsLabel[6] = Label_Purple_Ghosts;
            CampsLabel[7] = Label_Purple_Golems;
            CampsLabel[8] = Label_Blue_Red;
            CampsLabel[9] = Label_Blue_Blue;
            CampsLabel[10] = Label_Purple_Red;
            CampsLabel[11] = Label_Purple_Blue;
            CampsLabel[12] = Label_Dragon;
            CampsLabel[13] = Label_Baron;
            //
            CampsPictureBox[0] = PictureBox_Blue_Ghost;
            CampsPictureBox[1] = PictureBox_Blue_Wolves;
            CampsPictureBox[2] = PictureBox_Blue_Ghosts;
            CampsPictureBox[3] = PictureBox_Blue_Golems;
            CampsPictureBox[4] = PictureBox_Purple_Ghost;
            CampsPictureBox[5] = PictureBox_Purple_Wolves;
            CampsPictureBox[6] = PictureBox_Purple_Ghosts;
            CampsPictureBox[7] = PictureBox_Purple_Golems;
            CampsPictureBox[8] = PictureBox_Blue_Red;
            CampsPictureBox[9] = PictureBox_Blue_Blue;
            CampsPictureBox[10] = PictureBox_Purple_Red;
            CampsPictureBox[11] = PictureBox_Purple_Blue;
            CampsPictureBox[12] = PictureBox_Dragon;
            CampsPictureBox[13] = PictureBox_Baron;

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
                CampsLabel[i].Text = NTT(CampsCountDownTimers[i]);
                CampsPictureBox[i].Image = CampsPictureBox[i].ErrorImage;
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
            if (a > 60&&isTimeMin==true)
            {
                tmp = (a/60).ToString() + ":" + (a%60).ToString("D2");
            }
            else { tmp = a.ToString(); }
            return tmp ;
        }
            //GameTimeFormatting
        private string GTF(int a)
        {
            string tmp = null;
            tmp = (a / 60).ToString("D2") + ":" + (a % 60).ToString("D2");
            return tmp;
        }

        //Events
        private void Button_Start_Click(object sender, EventArgs e)
        {
            if (!isStarted)
            {
                Label_GameTime.Visible = true;
                Button_Start.Text = "Stop";
                isStarted = true;
                Timer_MainFrame.Start();
                for (int i = 0; i < 14; i++)
                {
                    CampsLabel[i].Visible = true;
                }
            }
            else
            {
                Label_GameTime.Visible = false;
                Button_Start.Text = "Start";
                isStarted = false;
                Timer_MainFrame.Stop();
                isPaused = false;
                Button_Pause.Text = "Pause";
                TimersReset();
            }
            

        }

        private void Jungle_Timer_Tick(object sender, EventArgs e)
        {
            GameTime++;
            Label_GameTime.Text = GTF(GameTime);
            for (int i = 0; i < 14; i++)
            {
                if (CampsCountDownTimers[i] > 0)
                {
                    CampsCountDownTimers[i]--;
                    CampsLabel[i].Text = NTT(CampsCountDownTimers[i]);
                    CampsPictureBox[i].Image = CampsPictureBox[i].ErrorImage;
                }
                else if (CampsCountDownTimers[i] == 0)
                {
                    Live.Play();
                    CampsPictureBox[i].Image = CampsPictureBox[i].InitialImage;
                    CampsLabel[i].Text = "Live";
                    CampsCountDownTimers[i]--;
                }
                else { }
            }
        }

        private void Button_Pause_Click(object sender, EventArgs e)
        {
            if (!isPaused)
            {
                Timer_MainFrame.Stop();
                isPaused = true;
                Button_Pause.Text = "Resume";
            }
            else
            {
                Timer_MainFrame.Start();
                isPaused = false;
                Button_Pause.Text = "Pause";
            }
            
        }

        private void Button_MapSwith_Click(object sender, EventArgs e)
        {
            if (isMapOn)
            {
                isMapOn = false;
                Button_MapSwith.Text = "Map On";
                PictureBox_JungleMap.Visible = false;
            }
            else
            {
                isMapOn = true;
                Button_MapSwith.Text = "Map Off";
                PictureBox_JungleMap.Visible = true;
            }
        }

        private void Button_TimeFormat_Click(object sender, EventArgs e)
        {
            if (isTimeMin)
            {
                isTimeMin = false;
                Button_TimeFormat.Text = "Show Min";
            }
            else 
            {
                isTimeMin = true;
                Button_TimeFormat.Text = "Show Sec";
            }
        }

        private void Button_Test02_Click(object sender, EventArgs e)
        {
            Timer_MainFrame.Interval = 1000;
        }

        private void Button_Test01_Click(object sender, EventArgs e)
        {
            Timer_MainFrame.Interval = 10;
        }

        private void PictureBox_Blue_Ghost_Click(object sender, EventArgs e)
        {
            TimersReset(0);
        }

        private void PictureBox_Blue_Wolves_Click(object sender, EventArgs e)
        {
            TimersReset(1);
        }

        private void PictureBox_Blue_Ghosts_Click(object sender, EventArgs e)
        {
            TimersReset(2);
        }

        private void PictureBox_Blue_Golems_Click(object sender, EventArgs e)
        {
            TimersReset(3);
        }

        private void PictureBox_Purple_Ghost_Click(object sender, EventArgs e)
        {
            TimersReset(4);
        }

        private void PictureBox_Purple_Wolves_Click(object sender, EventArgs e)
        {
            TimersReset(5);
        }

        private void PictureBox_Purple_Ghosts_Click(object sender, EventArgs e)
        {
            TimersReset(6);
        }

        private void PictureBox_Purple_Golems_Click(object sender, EventArgs e)
        {
            TimersReset(7);
        }

        private void PictureBox_Blue_Red_Click(object sender, EventArgs e)
        {
            TimersReset(8);
        }

        private void PictureBox_Blue_Blue_Click(object sender, EventArgs e)
        {
            TimersReset(9);
        }

        private void PictureBox_Purple_Red_Click(object sender, EventArgs e)
        {
            TimersReset(10);
        }

        private void PictureBox_Purple_Blue_Click(object sender, EventArgs e)
        {
            TimersReset(11);
        }

        private void PictureBox_Dragon_Click(object sender, EventArgs e)
        {
            TimersReset(12);
        }

        private void PictureBox_Baron_Click(object sender, EventArgs e)
        {
            TimersReset(13);
        }

        private void Button_PlayAudio01_Click(object sender, EventArgs e)
        {
            Live.Play();
        }

        private void Button_About_Click(object sender, EventArgs e)
        {
            MessageBox.Show(Resource1.about_info, "About");
        }

        private void Button_Help_Click(object sender, EventArgs e)
        {
            MessageBox.Show(Resource1.help_info, "Help");
        }

        private void Button_Forward_Click(object sender, EventArgs e)
        {
            GameTime = GameTime + 5;
            Label_GameTime.Text = GTF(GameTime);
            for (int i = 0; i < 14; i++)
            {
                CampsCountDownTimers[i] = CampsCountDownTimers[i] - 5;
                if (CampsCountDownTimers[i] < 0) { CampsCountDownTimers[i] = 0; }
                CampsLabel[i].Text = NTT(CampsCountDownTimers[i]);
            }
        }

        private void Button_Backward_Click(object sender, EventArgs e)
        {
            GameTime = GameTime - 5;
            if (GameTime < 0) { GameTime = 0; }
            Label_GameTime.Text = GTF(GameTime);
            for (int i = 0; i < 14; i++)
            {
                CampsCountDownTimers[i] = CampsCountDownTimers[i] + 5;
                CampsLabel[i].Text = NTT(CampsCountDownTimers[i]);
            }
        }

        private void Button_Mode_Click(object sender, EventArgs e)
        {
            GlobalVar.Mode = 1;
            GlobalVar.isSwitchingMode = true;
            this.Close();
        }
        
        

        

        
        


        
    }
}

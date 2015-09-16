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
    public partial class JungleTimer_Hanger : Form
    {
        //Variables
        string[] CampsName = new string[6];
        Label[] Camps = new Label[6];
        int[] CampsTimers = new int[6];
        int GameTime = 0;
        SoundPlayer Live = new SoundPlayer(Resource1.Live);
        bool isStarted = false;
        bool isPaused = false;
        bool isTimeMin = true;

        //Boardless Form
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.FormBorderStyle = FormBorderStyle.None;
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Button == MouseButtons.Left)
            {
                this.Capture = false;
                Message msg = Message.Create(this.Handle, 0XA1, new IntPtr(2), IntPtr.Zero);
                this.WndProc(ref msg);
            }
        }

        void pictureBox1_OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Button == MouseButtons.Left)
            {
                this.Capture = false;
                Message msg = Message.Create(this.Handle, 0XA1, new IntPtr(2), IntPtr.Zero);
                this.WndProc(ref msg);
            }
        }


        //Boardless Form[END]

        //Main Thread
        public JungleTimer_Hanger()
        {
            MessageBox.Show(Resource1.help_info_hanger, "HELP");
            InitializeComponent();
            map();
            TimersReset();
        }

        //Funtions
        private void map()
        {
            Camps[0] = label1;
            Camps[1] = label2;
            Camps[2] = label3;
            Camps[3] = label4;
            Camps[4] = label5;
            Camps[5] = label6;
        }
        private void TimersReset()
        {
            for (int i = 0; i < 6; i++)
            {
                TimersReset(i, true);
            }
            Camps[0].Text = "Blue Side Blue";
            Camps[1].Text = "Blue Side Red";
            Camps[2].Text = "Purple Side Blue";
            Camps[3].Text = "Purple Side Red";
            Camps[4].Text = "Dragon";
            Camps[5].Text = "Baron";
            Label_GameTime.Text = "00:00";
            GameTime = 0;
        }
        private void TimersReset(int i, bool initiate)
        {
            if (initiate)
            {
                switch (i)
                {
                    case 0:
                    case 1:
                    case 2:
                    case 3:
                        CampsTimers[i] = 115;
                        break;
                    case 4:
                        CampsTimers[i] = 150;
                        break;
                    case 5:
                        CampsTimers[i] = 900;
                        break;
                }
            }
            else 
            {
                switch (i)
                {
                    case 0:
                    case 1:
                    case 2:
                    case 3:
                        CampsTimers[i] = 300;
                        break;
                    case 4:
                        CampsTimers[i] = 360;
                        break;
                    case 5:
                        CampsTimers[i] = 420;
                        break;
                }
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
            else if (a == 0) { tmp = "Live"; }
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

        

        //KeyPress Event
        void JungleTimer_Hanger_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 49)    //[1]Blue Blue
            {
                TimersReset(0, false);
            }
            if (e.KeyChar == 50)    //[2]Blue Red
            {
                TimersReset(1, false);
            }
            if (e.KeyChar == 51)    //[3]Purple Blue
            {
                TimersReset(2, false);
            }
            if (e.KeyChar == 52)    //[4]Purple Red
            {
                TimersReset(3, false);
            }
            if (e.KeyChar == 53)    //[5]Dragon
            {
                TimersReset(4, false);
            }
            if (e.KeyChar == 54)    //[6]Baron
            {
                TimersReset(5, false);
            }
            if (e.KeyChar == 83)    //[S]START/STOP
            {
                
                if (!isStarted)
                {
                    isStarted = true;
                    timer.Start();
                    this.TransparencyKey = System.Drawing.Color.Black;
                    Camps[0].Text = "1:55";
                    Camps[1].Text = "1:55";
                    Camps[2].Text = "1:55";
                    Camps[3].Text = "1:55";
                    Camps[4].Text = "2:30";
                    Camps[5].Text = "15:00";
                    
                }
                else
                {
                    isStarted = false;
                    timer.Stop();
                    isPaused = false;
                    TimersReset();
                    this.TransparencyKey = System.Drawing.Color.Empty;

                }
            }
            if (e.KeyChar == 65)    //[A]PAUSE/RESUME
            {
                if (isStarted) 
                {
                    if (!isPaused)
                    {
                        timer.Stop();
                        isPaused = true;
                        this.TransparencyKey = System.Drawing.Color.Empty;
                    }
                    else
                    {
                        timer.Start();
                        isPaused = false;
                        this.TransparencyKey = System.Drawing.Color.Black;
                    }
                }
            }
            if (e.KeyChar == 44)    //[,]BACKWARD
            {
                if (isStarted)
                {
                    GameTime = GameTime - 1;
                    if (GameTime < 0) { GameTime = 0; }
                    Label_GameTime.Text = GTF(GameTime);
                    for (int i = 0; i < 6; i++)
                    {
                        CampsTimers[i] = CampsTimers[i] + 1;
                        Camps[i].Text = NTT(CampsTimers[i]);
                    }
                }
            }
            if (e.KeyChar == 46)    //[.]FORWARD
            {
                if (isStarted)
                {
                    GameTime = GameTime + 1;
                    Label_GameTime.Text = GTF(GameTime);
                    for (int i = 0; i < 6; i++)
                    {
                        CampsTimers[i] = CampsTimers[i] - 1;
                        if (CampsTimers[i] < 0) { CampsTimers[i] = 0; }
                        Camps[i].Text = NTT(CampsTimers[i]);

                    }
                }
            }
            if (e.KeyChar == 77)    //[M]SWITCH MODE
            {
                GlobalVar.Mode = 0;
                GlobalVar.isSwitchingMode = true;
                this.Close();
            }
            if (e.KeyChar == 70)    //[F]SWITCH TIME FORMAT
            {
                if (isTimeMin)
                {
                    isTimeMin = false;
                }
                else
                {
                    isTimeMin = true;
                }
            }
            if (e.KeyChar == 72)    //[H]HELP
            {
                MessageBox.Show(Resource1.help_info_hanger, "HELP");
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            
            GameTime++;
            Label_GameTime.Text = GTF(GameTime);
            for (int i = 0; i < 6; i++)
            {
                if (CampsTimers[i] > 0)
                {
                    CampsTimers[i]--;
                    if (CampsTimers[i] == 0) { Live.Play(); }
                    Camps[i].Text = NTT(CampsTimers[i]); 
                    
                }
                else if (CampsTimers[i] == 0)
                {
                    CampsTimers[i]--;
                }
                else { }
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            TimersReset(0, false);
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            TimersReset(1, false);
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            TimersReset(2, false);
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            TimersReset(3, false);
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            TimersReset(4, false);
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            TimersReset(5, false);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!isStarted)
            {
                isStarted = true;
                timer.Start();
                this.TransparencyKey = System.Drawing.Color.Black;
                Camps[0].Text = "1:55";
                Camps[1].Text = "1:55";
                Camps[2].Text = "1:55";
                Camps[3].Text = "1:55";
                Camps[4].Text = "2:30";
                Camps[5].Text = "15:00";
                button1.BackgroundImage = Properties.Resources.Stop;

            }
            else
            {
                isStarted = false;
                timer.Stop();
                isPaused = false;
                TimersReset();
                this.TransparencyKey = System.Drawing.Color.Empty;
                button1.BackgroundImage = Properties.Resources.Play;
            }
        }

      
        //KeyPress Event[END]
        
    }
}

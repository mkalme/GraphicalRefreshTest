using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;

namespace GraphicalRefreshTest {
    public partial class Form1 : Form {
        private static BackgroundWorker BackgroundWorker = new BackgroundWorker();

        private Render Render { get; set; }

        public Form1()
        {
            InitializeComponent();

            Render = new Render(pictureBox1.Width, pictureBox1.Height);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            StartbackgroundWorker();

            //TimeSpan timeSpan = new TimeSpan();
            //DateTime dateTime = DateTime.Now;

            //for (int i = 0; i < 1000; i++) {
            //    Render.DrawBlock(Block.DefaultBlock, 0, 0);
            //}

            //TimeSpan time = DateTime.Now - dateTime;
            //Debug.WriteLine(time.TotalSeconds + " seconds");
        }

        //Background worker
        private void StartbackgroundWorker() {
            BackgroundWorker.DoWork += BackgroundWorker_DoWork;
            BackgroundWorker.ProgressChanged += BackgroundWorker_ProgressChanged;
            BackgroundWorker.WorkerReportsProgress = true;

            BackgroundWorker.RunWorkerAsync();
        }

        private void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            int loop = 0;

            while (true) {
                //SetImage();
                Render.Clear(Color.Black.ToArgb());
                for (int i = 0; i < 100; i++) {
                    Render.DrawBlock(Block.DefaultBlock, 0, 0);
                }
                Render.ConvertToImage();

                BackgroundWorker.ReportProgress(1);

                loop++;
            }
        }
        private void BackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            pictureBox1.Image = Render.Image;

            UpdateFPS();
        }

        //FPS
        private static DateTime LastTimeUpdated = DateTime.Now;
        private static int FPS = 0;
        private static double UpdateTimeInSeconds = 1;

        private void UpdateFPS() {
            FPS++;
            TimeSpan timeSpan = DateTime.Now - LastTimeUpdated;

            if (timeSpan.TotalSeconds >= UpdateTimeInSeconds) {
                double fps = (1.0 / timeSpan.TotalSeconds) * FPS;

                Text = "Form1 | FPS = " + (int)fps;

                FPS = 0;
                LastTimeUpdated = DateTime.Now;
            }
        }

        //Get Image
        private static Random Random = new Random();
        private static int BlockSize = Properties.Resources.Block.Width;

        private void SetImage() {
            Render.Clear(Color.Black.ToArgb());

            for (int x = 0; x < (int)Math.Ceiling((double)pictureBox1.Width / BlockSize); x++) {
                for (int y = 0; y < (int)Math.Ceiling((double)pictureBox1.Height / BlockSize); y++) {
                    Block[] blocks = {Block.DefaultBlock, Block.HiddenBlock, Block.Brick};

                    Render.DrawBlock(blocks[Random.Next(blocks.Length)], x * BlockSize, y * BlockSize);
                }
            }

            Render.ConvertToImage();
        }
    }
}

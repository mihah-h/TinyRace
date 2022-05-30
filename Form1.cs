using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace TinyRace
{
    public partial class Form1 : Form
    {
        readonly static string SavedRecord = File.ReadAllText("C:\\Users\\Mike\\OneDrive\\Рабочий стол\\Tiny Race\\TinyRace\\Record.txt");
        readonly StreamWriter sw = new StreamWriter("C:\\Users\\Mike\\OneDrive\\Рабочий стол\\Tiny Race\\TinyRace\\Record.txt");
        readonly PlayerCar playerCar = new PlayerCar(140, 520,Convert.ToInt32(SavedRecord));
        readonly Point formSize = new Point(356, 698);
        readonly List<BadCar> badCars = new List<BadCar>();
        Image beck = TinyCarImage.Background1;
        readonly Random typeAndLineBadCar = new Random();
        int moveBackgroundFlag = 0;
        int screwRotatioFlag = 0;
        
        Timer renderingTimer;
        Timer movePlayerCarTimer;
        Timer collisionTimer;
        Timer moveBedCarsTimer;
        Timer creationBadCarsTimer;
        Timer deleteBadCarsTimer;
        Timer screwRotationTimer;

        readonly Button restartButton = new Button();
        readonly Button pauseButton = new Button();
        readonly Button resumingButton = new Button();

        readonly Label scoreLabel = new Label();
        readonly Label jumpLabel = new Label();
        readonly Label recordLabel = new Label();
        public Form1()
        {
            KeyPreview = true;
            DoubleBuffered = true;
            InitializeComponent();
            Size = new Size(formSize);
            MinimumSize = MaximumSize = new Size(formSize);
            KeyDown += new KeyEventHandler(DownKey);
            KeyUp += new KeyEventHandler(UpKey);
            FormClosed += (sender, e) =>
            {
                sw.WriteLine(playerCar.Record);
                sw.Close();
            };

            restartButton.Location = new Point(100, 400);
            restartButton.Width = 140;
            restartButton.Height = 80;
            restartButton.Image = TinyCarImage.Go;
            restartButton.Click += (sender, e) =>
            {
                Start();
            };
            Controls.Add(restartButton);

            pauseButton.Location = new Point(24, 10);
            pauseButton.Width = 50;
            pauseButton.Height = 50;
            pauseButton.Image = TinyCarImage.Pause;
            pauseButton.Visible = false;
            pauseButton.Click += (sender, e) =>
            {
                EnablePause();
            };
            Controls.Add(pauseButton);

            resumingButton.Location = new Point(24, 10);
            resumingButton.Width = 50;
            resumingButton.Height = 50;
            resumingButton.Image = TinyCarImage.Resuming;
            resumingButton.Visible = false;
            resumingButton.Click += (sender, e) =>
            {
                TurnPause();
            };
            Controls.Add(resumingButton);

            scoreLabel.Location = new Point(276, 30);
            scoreLabel.Width = 64;
            scoreLabel.Height = 20;
            scoreLabel.Text = "score: " + playerCar.Score.ToString();
            Controls.Add(scoreLabel);

            jumpLabel.Location = new Point(276, 50);
            jumpLabel.Width = 64;
            jumpLabel.Height = 20;
            jumpLabel.Text = "jump: " + playerCar.JumpsQuantity.ToString();
            Controls.Add(jumpLabel);

            recordLabel.Location = new Point(276, 10);
            recordLabel.Width = 64;
            recordLabel.Height = 20;
            recordLabel.Text = "record: " + playerCar.Record;
            Controls.Add(recordLabel);
        }

        private void TurnPause()
        {
            renderingTimer.Start();
            movePlayerCarTimer.Start();
            collisionTimer.Start();
            moveBedCarsTimer.Start();
            creationBadCarsTimer.Start();
            screwRotationTimer.Start();
            resumingButton.Visible = false;
            pauseButton.Visible = true;
        }

        private void EnablePause()
        {
            renderingTimer.Stop();
            movePlayerCarTimer.Stop();
            collisionTimer.Stop();
            moveBedCarsTimer.Stop();
            creationBadCarsTimer.Stop();
            screwRotationTimer.Stop();
            resumingButton.Visible = true;
            pauseButton.Visible = false;
        }

        private void DownKey(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A)
                playerCar.GoLeft = true;
            if (e.KeyCode == Keys.D)
                playerCar.GoRight = true;
            if (e.KeyCode == Keys.W)
                playerCar.GoUp = true;
            if (e.KeyCode == Keys.S)
                playerCar.GoDown = true;
            if (e.KeyCode == Keys.L)
                playerCar.Jump = true;
        }

        private void UpKey(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A)
                playerCar.GoLeft = false;
            if (e.KeyCode == Keys.D)
                playerCar.GoRight = false;
            if (e.KeyCode == Keys.W)
                playerCar.GoUp = false;
            if (e.KeyCode == Keys.S)
                playerCar.GoDown = false;
            if (e.KeyCode == Keys.L)
                playerCar.Jump = false;
        }

        public void Start()
        {
            
            playerCar.X = 140;
            playerCar.Y = 520;
            restartButton.Visible = false;
            pauseButton.Visible = true;
            playerCar.Score = 0;
            scoreLabel.Text = "score: " + playerCar.Score.ToString();
            playerCar.JumpsQuantity = 0;
            jumpLabel.Text = "jump: " + playerCar.JumpsQuantity.ToString();
            beck = TinyCarImage.Background1;
            playerCar.Image = TinyCarImage.PlayerCar1;
            badCars.Clear();

            renderingTimer = new Timer { Interval = 10 };
            renderingTimer.Tick += (sender, e) =>
            {
                Invalidate();
            };
            renderingTimer.Start();

            movePlayerCarTimer = new Timer { Interval = 10 };
            movePlayerCarTimer.Tick += (sender, e) =>
            {
                if (playerCar.GoLeft && playerCar.X > 0)
                    playerCar.X -= 8;
                if (playerCar.GoRight && playerCar.X + playerCar.Width < formSize.X - 16)
                    playerCar.X += 8;
                if (playerCar.GoUp && playerCar.Y > 0)
                    playerCar.Y -= 8;
                if (playerCar.GoDown && playerCar.Y + playerCar.Height < formSize.Y - 39)
                    playerCar.Y += 8;
            };
            movePlayerCarTimer.Start();

            collisionTimer = new Timer { Interval = 10 };
            collisionTimer.Tick += (sender, e) =>
            {
                if (playerCar.Jump && playerCar.JumpsQuantity > 0)
                    MemakeJump();
                else
                {
                    screwRotationTimer.Stop();
                    for (var i = 0; i < badCars.Count; i++)
                        if (!(playerCar.Y + playerCar.Height - 10 < badCars[i].Y || playerCar.Y > badCars[i].Y + badCars[i].Height - 10 ||
                            playerCar.X + playerCar.Width - 10 < badCars[i].X || playerCar.X > badCars[i].X + badCars[i].Width - 10))
                        {
                            EnablePause();
                            resumingButton.Visible = false;
                            restartButton.Visible = true;
                            if (playerCar.Score > Convert.ToInt32(playerCar.Record))
                            {
                                playerCar.Record = playerCar.Score;
                                recordLabel.Text = "record: " + playerCar.Record.ToString();
                            }
                        }
                }

            };
            collisionTimer.Start();

            moveBedCarsTimer = new Timer { Interval = 26 };

            moveBedCarsTimer.Tick += (sender, e) =>
            {
                MoveBadCars();
                MoveBackground();
            };
            moveBedCarsTimer.Start();

            creationBadCarsTimer = new Timer { Interval = 576 };
            creationBadCarsTimer.Tick += (sender, e) =>
            {
                badCars.Add(CreateBadCar(typeAndLineBadCar));
            };
            creationBadCarsTimer.Start();

            deleteBadCarsTimer = new Timer { Interval = 100 };
            deleteBadCarsTimer.Tick += (sender, e) =>
            {
                for (var i = 0; i < badCars.Count; i++)
                {
                    if (badCars[i].Y > formSize.Y)
                    {
                        playerCar.Score++;
                        scoreLabel.Text = "score: " + playerCar.Score.ToString();
                        playerCar.JumpsQuantity++;
                        jumpLabel.Text = "jump: " + playerCar.JumpsQuantity.ToString();
                        badCars.RemoveAt(i);
                        if (playerCar.Score < 200)
                            SpeedUpGame();   
                    }
                }
            };
            deleteBadCarsTimer.Start();

            screwRotationTimer = new Timer { Interval = 20 };
            screwRotationTimer.Tick += (sender, e) =>
            {
                if (screwRotatioFlag == 0)
                    playerCar.Image = TinyCarImage.PlayerCar1;
                if (screwRotatioFlag == 1)
                    playerCar.Image = TinyCarImage.PlayerCar2;
                if (screwRotatioFlag == 2)
                {
                    playerCar.Image = TinyCarImage.PlayerCar3;
                    screwRotatioFlag = -1;
                }
                screwRotatioFlag++;
            };
            
        }

        private void SpeedUpGame()
        {
            creationBadCarsTimer.Interval = 600 - (playerCar.Score / 20) * 26;
            moveBedCarsTimer.Interval = 20 - playerCar.Score / 20;
        }

        private void MoveBackground()
        {
            if (moveBackgroundFlag == 0)
                beck = TinyCarImage.Background1;
            if (moveBackgroundFlag == 1)
                beck = TinyCarImage.Background2;
            if (moveBackgroundFlag == 2)
                beck = TinyCarImage.Background3;
            if (moveBackgroundFlag == 3)
            {
                beck = TinyCarImage.Background4;
                moveBackgroundFlag = -1;
            }
            moveBackgroundFlag++;
        }

        private void MoveBadCars()
        {
            for (var i = 0; i < badCars.Count; i++)
            {
                if (i == 0)
                    badCars[i].Y += badCars[i].Spead;
                else if (badCars[i].Y + badCars[i].Height + 24 > badCars[i - 1].Y && badCars[i].X == badCars[i - 1].X)
                {
                    badCars[i - 1].Spead = badCars[i].Spead;
                    badCars[i].Y += badCars[i].Spead;
                }
                else
                    badCars[i].Y += badCars[i].Spead;
            }
        }

        private void MemakeJump()
        {
            playerCar.JumpsQuantity--;
            jumpLabel.Text = playerCar.JumpsQuantity.ToString();
            screwRotationTimer.Start();
        }

        private static BadCar CreateBadCar(Random TypeAndLineBadCar)
        {
            var tipeBadCar = TypeAndLineBadCar.Next(0, 4);
            var lyneBadCar = TypeAndLineBadCar.Next(0, 3);
            
            if (tipeBadCar == 0)
                return new BadCar(lyneBadCar, TinyCarImage.BadCar1, 12);  
            if (tipeBadCar == 1)
                return new BadCar(lyneBadCar, TinyCarImage.BadCar2, 12);
            if (tipeBadCar == 2)
                return new BadCar(lyneBadCar, TinyCarImage.BadCar3, 14);
            return new BadCar(lyneBadCar, TinyCarImage.BadCar4, 14);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var graphics = e.Graphics;
            graphics.DrawImage(beck, 0, 0, formSize.X - 16, formSize.Y - 39);
            graphics.DrawImage(playerCar.Image, playerCar.X, playerCar.Y, playerCar.Width, playerCar.Height);
            for (var i = 0; i < badCars.Count; i++)
                graphics.DrawImage(badCars[i].Image, badCars[i].X, badCars[i].Y, badCars[i].Width, badCars[i].Height) ;
        }
    }
}

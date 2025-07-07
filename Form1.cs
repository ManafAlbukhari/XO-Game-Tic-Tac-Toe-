using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XOGame.Properties;

namespace XOGame
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        enPlayer PlayerTurn = enPlayer.Player1;
        stGameStatus GameStatus;
        
        enum enPlayer { Player1,  Player2 };

        enum enWinner { Player1,  Player2, Draw, GameInProgress };

        struct stGameStatus
        {
            public enWinner Winner;
            public byte PlayCount;
            public bool GameOver;
        }

        void EndGame()
        {
            lblTurn.Text = "Game Over";

            switch(GameStatus.Winner)
            {
                case enWinner.Player1:
                    lblWinner.Text = "Player 1";
                    break;

                case enWinner.Player2:
                    lblWinner.Text = "Player 2";
                    break;

                default:
                    lblWinner.Text = "Draw";
                    break;
            }

            MessageBox.Show("Game Over", "Game Over", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public bool CheckValues(Button btn1 , Button btn2, Button btn3)
        {
            if (btn1.Tag.ToString() != "?" && btn1.Tag.ToString() == btn2.Tag.ToString()
                && btn1.Tag.ToString() == btn3.Tag.ToString())
            {
                btn1.BackColor = Color.YellowGreen;
                btn2.BackColor = Color.YellowGreen;
                btn3.BackColor = Color.YellowGreen;

                if (btn1.Tag.ToString() == "X")
                {
                    GameStatus.Winner = enWinner.Player1;
                    GameStatus.GameOver = true;
                    EndGame();
                    return true;

                }
                else
                {
                    GameStatus.Winner = enWinner.Player2;
                    GameStatus.GameOver = true;
                    EndGame();
                    return true;
                }

            }
            GameStatus.GameOver = false;
            return false;
           
        }

        public void CheckWinner()
        {
            //check rows
            if (CheckValues(button1, button2, button3))
                return;
            if (CheckValues(button4, button5, button6))
                return;
            if (CheckValues(button7, button8, button9))
                return;

            //check cols
            if (CheckValues(button1, button4, button7))
                return;
            if (CheckValues(button2, button5, button8))
                return;
            if (CheckValues(button3, button6, button9))
                return;

            //check diagonal
            if (CheckValues(button1, button5, button9))
                return;
            if (CheckValues(button3, button5, button7))
                return;
        }

        void ChangeImage(Button btn)
        {
            if (btn.Tag.ToString() == "?")
            {
                switch(PlayerTurn)
                {
                    case enPlayer.Player1:

                        btn.Image = Resources.X;
                        btn.Tag = "X";
                        PlayerTurn = enPlayer.Player2;
                        lblTurn.Text = "Player 2";
                        GameStatus.PlayCount++;
                        CheckWinner();
                        break;

                    case enPlayer.Player2:

                        btn.Image = Resources.O;
                        btn.Tag = "O";
                        PlayerTurn = enPlayer.Player1;
                        lblTurn.Text = "Player 1";
                        GameStatus.PlayCount++;
                        CheckWinner();
                        break;
                }
            }
            else
            {
                MessageBox.Show("Wrong Choice", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (GameStatus.PlayCount == 9 && !GameStatus.GameOver)
            {

                GameStatus.Winner = enWinner.Draw;
                GameStatus.PlayCount = 0;
                GameStatus.GameOver = true;
                EndGame();
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Color White = Color.White;
            Pen MyPen = new Pen(White);
            MyPen.Width = 15;

            MyPen.StartCap = System.Drawing.Drawing2D.LineCap.Round;
            MyPen.EndCap = System.Drawing.Drawing2D.LineCap.Round;

            //draw horizontal line
            e.Graphics.DrawLine(MyPen, 400, 300, 1050, 300);
            e.Graphics.DrawLine(MyPen, 400, 460, 1050, 460);

            //draw vertical line
            e.Graphics.DrawLine(MyPen, 610, 140, 610, 620);
            e.Graphics.DrawLine(MyPen, 840, 140, 840, 620);

        }

        private void button_Click(object sender, EventArgs e)
        {
            ChangeImage((Button)sender);
        }

        private void ResetButton(Button btn)
        {
            btn.Image = Resources.question_mark_96;
            btn.Tag = "?";
            btn.BackColor = Color.Transparent;
        }

        private void RestartGame()
        {
            ResetButton(button1);
            ResetButton(button2);
            ResetButton(button3);
            ResetButton(button4);
            ResetButton(button5);
            ResetButton(button6);
            ResetButton(button7);
            ResetButton(button8);
            ResetButton(button9);

            PlayerTurn = enPlayer.Player1;
            lblTurn.Text = "Player 1";
            GameStatus.PlayCount = 0;
            GameStatus.GameOver = false;
            GameStatus.Winner = enWinner.GameInProgress;
            lblWinner.Text = "In Progress";

        }

        private void btnRestart_Click(object sender, EventArgs e)
        {
            RestartGame();
        }

    }
}
  
      
   


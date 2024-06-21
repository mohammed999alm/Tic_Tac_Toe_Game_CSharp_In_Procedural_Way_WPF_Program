using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Tic_Tac_Toe_Game
{

    struct Player
    {
        string name;
        string gameSymbol;
        static List<int> pressedButtons = new List<int>();

        int press;

        public Player(string name, string gameSymbol)
        {
            this.name = name;
            this.gameSymbol = gameSymbol;
            press = 0;

            //pressedButtons = new List<int>();
        }


        public string Name
        {
            get { return name; }
        }

        public int Pressed 
        {
            get { return press; }
            set { press = value; }
        }

        public static void addClickedButtons(int index) 
        {
            pressedButtons.Add(index);
        }

        public static bool search(int index) 
        {
            
            
            foreach (int  button in pressedButtons) 
            {
                if (button == index) 
                {
                    return true;
                }
            }

            return false;
        }

        public static void clearGame() 
        {
            pressedButtons.Clear();
        }

        public String GameSymbol
        { 
            get { return gameSymbol; }
        }
        
    }
    public partial class Form1 : Form
    {

        static byte  rounds = 0;

        Button[] ticToeGame = new Button[9];


        Player roundPlayer;

        private bool flag = false;
        void fillArray() 
        {
            ticToeGame[0] = btn1;
            ticToeGame[1] = btn2;
            ticToeGame[2] = btn3;
            ticToeGame[3] = btn4;
            ticToeGame[4] = btn5;
            ticToeGame[5] = btn6;
            ticToeGame[6] = btn7;
            ticToeGame[7] = btn8;
            ticToeGame[8] = btn9;
        }
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {


            fillArray();

        }


        bool playGame(int buttonTag) 
        {
            Player player1 = new Player("Player1", "X");

            Player player2 = new Player("Player2", "O");

              if (rounds % 2 == 0)
              {
                  player1.Pressed = buttonTag;

                  lblPlayerRound.Text = player1.Name;


                 if (!playRound(player1)) 
                 {
                     MessageBox.Show("The Button Already Pressed", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                     return false;
                 }
               }

             else
             {
                 player2.Pressed = buttonTag;

                 lblPlayerRound.Text = player2.Name;

                 if (!playRound(player2))
                 {
                      MessageBox.Show("The Button Already Pressed", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                      return false;
                 }

             }

             return true;   
        }



        bool playRound(Player player)
        {
            roundPlayer = player;

            Button currentButton = ticToeGame[roundPlayer.Pressed];


            if (Player.search(roundPlayer.Pressed))
            {
                return false;
            }


            Player.addClickedButtons(roundPlayer.Pressed);


            return true;
        }


        private string  checkWinner(Player player) 
        {
            if (ticToeGame[0].Text == player.GameSymbol && ticToeGame[1].Text == player.GameSymbol && ticToeGame[2].Text == player.GameSymbol) 
            {
                return "first row";
            }

            if (ticToeGame[0].Text == player.GameSymbol && ticToeGame[3].Text == player.GameSymbol && ticToeGame[6].Text == player.GameSymbol)
            {
                return "first col";
            }

            if (ticToeGame[1].Text == player.GameSymbol && ticToeGame[4].Text == player.GameSymbol && ticToeGame[7].Text == player.GameSymbol)
            {
                return "second col";
            }

            if (ticToeGame[3].Text == player.GameSymbol && ticToeGame[4].Text == player.GameSymbol && ticToeGame[5].Text == player.GameSymbol)
            {
                return "second row";
            }

            if (ticToeGame[6].Text == player.GameSymbol && ticToeGame[7].Text == player.GameSymbol && ticToeGame[8].Text == player.GameSymbol)
            {
                return "third row";
            }

            if (ticToeGame[2].Text == player.GameSymbol && ticToeGame[5].Text == player.GameSymbol && ticToeGame[8].Text == player.GameSymbol)
            {
                return "third col";
            }

            if (ticToeGame[2].Text == player.GameSymbol && ticToeGame[4].Text == player.GameSymbol && ticToeGame[6].Text == player.GameSymbol)
            {
                return "second diagonal";
            }

            if (ticToeGame[0].Text == player.GameSymbol && ticToeGame[4].Text == player.GameSymbol && ticToeGame[8].Text == player.GameSymbol)
            {
                return "main diagonal";
            }


            return "";
        }


        void checkWinner(string winnerGamePosition) 
        {


            switch (winnerGamePosition) 
            {
                case "first row":
                    resultMaker(0, 2, 1);
                    break;

                case "first col":
                    resultMaker(0, 6, 3);
                    break;

                case "second row":
                    resultMaker(3, 5, 1);
                    break;

                case "second col":
                    resultMaker(1, 7, 3);
                    break;

                case "third row":
                    resultMaker(6, 8, 1);
                    break;


                case "third col":
                    resultMaker(2, 8, 3);
                    break;

                case "main diagonal":
                    resultMaker(0, 8, 4);
                    break;

                case "second diagonal":
                    resultMaker(2, 6, 2);
                    break;
                

                case "":
                    if (rounds == 9)
                    {
                        lblWinner.Text = "Draw";

                        resultMaker(0, 8, 1, "yellow", "Draw");
                    }

                    break;
            }
        }

        void resultMaker(byte startPoint, byte endPoint, byte increament, string color = "green", string winner = "Player")
        {
            if (winner != "Draw")
            {
                lblWinner.Text = roundPlayer.Name;
            }

            disableButtons(true);

            for (byte i = startPoint; i <= endPoint; i += increament) 
            {
                if (color.ToLower() == "green")
                    ticToeGame[i].BackColor = Color.Green;

                else
                    ticToeGame[i].BackColor = Color.Yellow;
            }

            MessageBox.Show("Game Over", "Game Over", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        
        }

        void whoWinTheGame(Player player) 
        {

            checkWinner(checkWinner(player));
        }

        

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Color white = Color.White;

            Pen pen = new Pen(white, 10);


            pen.StartCap = System.Drawing.Drawing2D.LineCap.Round;
            pen.EndCap = System.Drawing.Drawing2D.LineCap.Round;

            //Vrtical Lines
            e.Graphics.DrawLine(pen, 400, 30, 400, 430);

            
            e.Graphics.DrawLine(pen, 550, 30, 550, 430);

            //Horizontal Lines
            e.Graphics.DrawLine(pen, 275, 150, 675, 150);

            e.Graphics.DrawLine(pen, 275, 300, 675, 300);
        }


        private string changeRoundLabel()
        {
            if (roundPlayer.Name.ToLower() == "player1")
            {
                return "Player2";
            }

            else
            {
                return "Player1";
            }
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            clickOnButton(btn1);
        }




        private void btn2_Click(object sender, EventArgs e)
        {
            clickOnButton(btn2);
        }


        private void btn3_Click(object sender, EventArgs e)
        {
            clickOnButton(btn3);
        }

        private void btn4_Click(object sender, EventArgs e)
        {
            clickOnButton(btn4);
        }

    private void btn5_Click(object sender, EventArgs e)
        {
            clickOnButton(btn5);
        }

        private void btn6_Click(object sender, EventArgs e)
        {
            clickOnButton(btn6);
        }

        private void btn7_Click(object sender, EventArgs e)
        {
            clickOnButton(btn7);
        }

        private void btn8_Click(object sender, EventArgs e)
        {
            clickOnButton(btn8);
        }

        private void btn9_Click(object sender, EventArgs e)
        {
            clickOnButton(btn9);
        }


        private void clickOnButton(Button button) 
        {
            if (!playGame(Convert.ToInt32(button.Tag)))
            {
                return;
            }


            rounds++;



            button.Text = roundPlayer.GameSymbol;

            button.ForeColor = Color.Aqua;

            lblPlayerRound.Text = changeRoundLabel();

            whoWinTheGame(roundPlayer);
        }

        private void disableButtons(bool disable)
        {

            if (disable)
            {
                for (byte i = 0; i < ticToeGame.Length; i++)
                {
                    ticToeGame[i].Enabled = false;
                    ticToeGame[i].BackColor = Color.Red;
                }
            }

            else
            {
                Player.clearGame();

                lblWinner.Text = "In Progress";

                lblPlayerRound.Text = "Player1";

                for (byte i = 0; i < ticToeGame.Length; i++)
                {
                    ticToeGame[i].Enabled = true;
                    ticToeGame[i].BackColor = Color.Black;
                    ticToeGame[i].ForeColor = Color.Red;
                    ticToeGame[i].Text = "?";
                }

                rounds = 0;
            }
        }

        private void restart_Click(object sender, EventArgs e)
        {
            disableButtons(false);
        }
    }
}

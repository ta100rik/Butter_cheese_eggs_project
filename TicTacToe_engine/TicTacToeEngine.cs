using System;

namespace TicTacToe_engine

{
    public class TicTacToeEngine
    {
        public enum GameStatus { PlayerOPlays, PlayerXPlays, Equal, PlayerOWins, PlayerXwins };
        /* 
            Board represent the board in a Array.
            -------------
             0 | 1 | 2
             3 | 4 | 5
             6 | 7 | 8
            -------------
            The number stands for the index
            the value can be 0 , 1 , 2
            0 = nothing filled in
            1 = x is placed
            2 = o is placed
        */
        int[] Boardarr = { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        GameStatus status = GameStatus.PlayerOPlays;
        public string Board()
        {
            string result = "";
            int counter = 0;
            for (int x = 1; x <= 7; x++)
            {
                if ((x % 2) == 1)
                {
                    result += "---------\n";
                }
                else
                {
                    result += "|";
                    for (int y = 0; y <= 2; y++)
                    {
                        result += convertCelToString(counter) + "|";
                        counter++;
                    }
                    result += "\n";
                }
            }
            return result;
        }
        public String convertCelToString(int cel)
        {
            String RESULT = "0";
            if (Boardarr[cel] == 1)
            {
                RESULT = "X";
            }
            else if (Boardarr[cel] == 2) 
            {
                RESULT = "O";
            }
            return RESULT;
        }
        public Boolean ChooseCell(int cel)
        {
            int selectedCell = Boardarr[cel];
            Boolean RESULT = false;
            if (selectedCell == 0)
            {
                if (status == GameStatus.PlayerXPlays)
                {
                    Boardarr[cel] = 1;
                    RESULT = true;
                }else if (status == GameStatus.PlayerOPlays)
                {
                    Boardarr[cel] = 2;
                    RESULT = true;
                }
            }
            checkWin();
            return RESULT;
        }
        public String getGameStatusStringified() {
            string RESULT = "";
            switch (status) {
                case GameStatus.PlayerOPlays:
                    RESULT = "Player 0 turn";
                break;
                case GameStatus.PlayerXPlays:
                    RESULT = "Player X turn";
                break;
                case GameStatus.PlayerOWins:
                    RESULT = "Player O has won";
                break;
                case GameStatus.PlayerXwins:
                    RESULT = "Player X has won";
                break;
                case GameStatus.Equal:
                    RESULT = "Equal game";
                break;
            }
            return RESULT;
        }
        public Boolean getGameover() {
            Boolean RESULT = false;
            switch (status)
            {

                case GameStatus.PlayerOWins:
                    RESULT = true;
                    break;
                case GameStatus.PlayerXwins:
                    RESULT = true;
                    break;
                case GameStatus.Equal:
                    RESULT = true;
                    break;
            }
            return RESULT;

        }
        public Boolean checkWin() {
            Boolean winstatus = false; //status that defines if there is a winning solution found
            Boolean drawstatus = false;
            int currentplayericon = 0; // default but it on 0 because that is empty
            if (status == GameStatus.PlayerXPlays) {
                currentplayericon = 1;

            } else if (status == GameStatus.PlayerOPlays) {
                currentplayericon = 2;
            }
            /*
                 here we are checking if the corner are having a solution
                 a solution is equals to 3 of the same icon in horizontal or vertical order
             */
            int horizontalwinCounter = 0; //if win counter come to 3 the solution is achieved
            int verticalWincounter   = 0;
            for (int i = 0; i <= 2; i++)
            {
                if (Boardarr[i] == currentplayericon && Boardarr[i] != 0) {
                    verticalWincounter++;
                }
                if (Boardarr[i * 3] == currentplayericon && Boardarr[i * 3] != 0)
                {
                    horizontalwinCounter++;
                }
            }
            // checking if there is a winning part in the game and setting winstatus to true if it is.
            if (horizontalwinCounter == 3 || verticalWincounter == 3)
            {
                winstatus = true;
            }
            // resetting the win paramters to do a second
            if (!winstatus)
            {
                horizontalwinCounter = 0; //if win counter come to 3 he has a combination
                verticalWincounter = 0;

                for (int i = 6; i <= 8; i++)
                {
                    if (Boardarr[i] == currentplayericon && Boardarr[i] != 0)
                    {
                        verticalWincounter++;                        
                    }
                }

                for (int i = 2; i <= 8; i += 3)
                {
                    if (Boardarr[i] == currentplayericon && Boardarr[i] != 0)
                    {
                        horizontalwinCounter++;
                    }
                    
                }
                // checking if there is a winning part in the game and setting winstatus to true if it is.
                if (horizontalwinCounter == 3 || verticalWincounter == 3)
                {
                    winstatus = true;
                }
            }
            /*
                now that we checked all the corners,
                we need to check the middle if it got 2 straight to each other neigbours
                to do this i made a calcuation:  
                    4 * 2 && 4 - 4
                    4 -3 && 4+ 3
                    4 + 1 && 4 - 1
                    4 + 2 && 4 - 2
                but because this value are fixed we can also say
                    8 && 0
                    1 && 7
                    5 && 3
                    6 && 2
                first we check if 4 is filled afterwards we check the combinations above
             */
            if (!winstatus && Boardarr[4] == currentplayericon) {
                if (Boardarr[8] == currentplayericon && Boardarr[0] == currentplayericon) {
                    winstatus = true;
                } else if (Boardarr[1] == currentplayericon && Boardarr[7] == currentplayericon)
                {
                    winstatus = true;
                } else if (Boardarr[5] == currentplayericon && Boardarr[3] == currentplayericon) {
                    winstatus = true;
                } else if (Boardarr[6] == currentplayericon && Boardarr[2] == currentplayericon) {
                    winstatus = true;
                }
            }
        
            
            /* 
             * before we return the status if it is won we also want to change the status
             * we want to change the game status to next player or to player won
             */

            if (status == GameStatus.PlayerOPlays)
            {
                status = (winstatus) ? GameStatus.PlayerOWins : GameStatus.PlayerXPlays;
            }
            else if(status == GameStatus.PlayerXPlays) {
                status = (winstatus) ? GameStatus.PlayerXwins: GameStatus.PlayerOPlays;
            }
            int filledCelsCounter = 0;
            foreach (int numb in Boardarr)
            {
                if (numb != 0)
                {
                    filledCelsCounter++;
                }
            }
            /*
             * when there is no winner check if it is equal.
             * if there are no 0 left on the board the game 
             */
            drawstatus = (filledCelsCounter == 9) ? true : false;
            status = (drawstatus) ? GameStatus.Equal : status;
            winstatus = (drawstatus) ? true : winstatus;
            return winstatus;
        }
        public void Reset()
        {
            this.Boardarr = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            status = GameStatus.PlayerOPlays;
        }
    }
}

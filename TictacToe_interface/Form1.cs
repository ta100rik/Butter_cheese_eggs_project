using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TictacToe_interface
{
    public partial class Form1 : Form
    {
        TicTacToe_engine.TicTacToeEngine engine = new TicTacToe_engine.TicTacToeEngine();
        public Form1()
        {
            InitializeComponent();
           
        }

        private void cellchooser(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;
            int tabindex = clickedButton.TabIndex;
            Boolean validstep = engine.ChooseCell(tabindex);
            if (validstep) {
                clickedButton.Text = engine.convertCelToString(tabindex);
                statusLabel.Text = engine.getGameStatusStringified();
                if (engine.getGameover()) {
                    resetboard();
                }
            }
            else {
                //MessageBox.Show("You already clicked this box");
                MessageBox.Show(engine.getGameStatusStringified());
            }
        }
        private void resetboard() {
            engine.Reset();
            MessageBox.Show("reseting board");
            foreach (var button in this.Controls.OfType<Button>())
            {
                if (button.Text != "Reset")
                {
                    button.Text = "";
                }
            }
            statusLabel.Text = engine.getGameStatusStringified();
        }

        private void resetBoardHandler(object sender, EventArgs e) {
            resetboard();
        }

        
    }
}

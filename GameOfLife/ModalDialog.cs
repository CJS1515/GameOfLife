using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameOfLife
{
    public partial class ModalDialog : Form
    {
        public ModalDialog()
        {
            InitializeComponent();
        }

        public int GetNumber()
        {
            return (int)Timer.Value;
        }
        public void SetNumber(int number)
        {
            Timer.Value = number;
        }

        private void Timer_ValueChanged(object sender, EventArgs e)
        {
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void WidthCounter_ValueChanged(object sender, EventArgs e)
        {

        }

        private void HeightCounter_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}

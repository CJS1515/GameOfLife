using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameOfLife
{
    public partial class Form1 : Form
    {
        // The universe array
        bool[,] universe = new bool[30,30];
        bool[,] scratchPad = new bool[30, 30];

        // Drawing colors
        Color gridColor = Color.Black;
        Color cellColor = Color.Gray;

        // The Timer class
        Timer timer = new Timer();

        // Generation count
        int generations = 0;

        bool isToroidal = true;

        public Form1()
        {
            InitializeComponent();

            // Setup the timer
            timer.Interval = 1000; // milliseconds
            timer.Tick += Timer_Tick;
            timer.Enabled = false; // start timer running
            //Read the property
            graphicsPanel1.BackColor = Properties.Settings.Default.PanelColor;
            cellColor = Properties.Settings.Default.CellColor;
            gridColor = Properties.Settings.Default.GridColor;
            //timer = Properties.Settings.Default.Timer;
            graphicsPanel1.Invalidate();

        }

        private void RandomizeTime()
        {
            Random rand = new Random();
            for (int y = 0; y < universe.GetLength(1); y++)
            {
                for (int x = 0; x < universe.GetLength(0); x++)
                {
                    //rand.Next(0,2);
                    if (rand.Next(0, 2) == 0)
                    {
                        universe[x, y] = true;
                        graphicsPanel1.Invalidate();
                    }
                }
            }
        }
        private void RandomizeSeed(int input)
        {
            int seed = input;
            Random rand = new Random(seed);
            for (int y = 0; y < universe.GetLength(1); y++)
            {
                for (int x = 0; x < universe.GetLength(0); x++)
                {
                    //rand.Next(0,2);
                    if (rand.Next(0, 2) == 0)
                    {
                        universe[x, y] = true;
                        graphicsPanel1.Invalidate();
                    }
                }
            }
        }
        // Calculate the next generation of cells
        private void NextGeneration()
        {
            for (int y = 0; y < universe.GetLength(1); y++)
            {
                for (int x = 0; x < universe.GetLength(0); x++)
                {
                    int count = CountNeighborsToroidal(x, y);
                    // Apply the rules
                    if(universe[x,y] == true && count < 2)
                    {
                        scratchPad[x,y] = false;
                        continue;
                    }
                    if(universe[x,y] == true && count > 3)
                    {
                        scratchPad[x, y] = false;
                        continue;
                    }
                    if(universe[x,y] == true && count == 2 || count == 3)
                    {
                        scratchPad[x, y] = true;
                        continue;
                    }
                    if(universe[x,y] == false && count ==3)
                    {
                        scratchPad[x, y] = true;
                        continue;
                    }
                    if(universe[x,y] == false && count != 3)
                    {
                        scratchPad[x, y] = false;
                        continue;
                    }
                }
            }
            CountCells();
            // copy from scratchPad to universe
            bool[,] temp = universe;
            universe = scratchPad;
            scratchPad = temp;

            // Increment generation count
            generations++;

            // Update status strip generations
            toolStripStatusLabelGenerations.Text = "Generations = " + generations.ToString();

            //Invalidate graphicsPanel
            graphicsPanel1.Invalidate();
        }
        //private int CountCells

        private int CountNeighborsFinite(int x, int y)
        {
            int count = 0;
            int xLen = universe.GetLength(0);
            int yLen = universe.GetLength(1);

            for (int yOffset = -1; yOffset <= 1; yOffset++)
            {
                for (int xOffset = -1; xOffset <= 1; xOffset++)
                {
                    int xCheck = x + xOffset;
                    int yCheck = y + yOffset;
                    if (xOffset == 0 && yOffset == 0) { continue; }
                    if (xCheck < 0) { continue; }
                    if(yCheck < 0) { continue; }
                    if(xCheck >= xLen) { continue; }
                    if(yCheck >= yLen) { continue; }

                    if (universe[xCheck, yCheck] == true) count++;
                }
            }
            return count;
        }

        private int CountCells()
        {
            int cells = 0;
            for (int y = 0; y < universe.GetLength(1); y++)
            {
                for (int x = 0; x < universe.GetLength(0); x++)
                {
                   if(universe[x,y] == true)
                   {
                        cells++;
                   }
                }
            }
            toolStripStatusLabelLivingCell.Text = "Living Cells = " + cells.ToString();
            return cells;
        }

        private int CountNeighborsToroidal(int x, int y)
        {
            int count = 0;
            int xLen = universe.GetLength(0);
            int yLen = universe.GetLength(1);

            for (int yOffset = -1; yOffset <= 1; yOffset++)
            {
                for (int xOffset = -1; xOffset <= 1; xOffset++)
                {
                    int xCheck = x + xOffset;
                    int yCheck = y + yOffset;
                    if (xOffset == 0 & yOffset == 0) { continue; }
                    if (xCheck < 0) { xLen = -1; }
                    if (yCheck < 0) { yLen = -1; }
                    if (xCheck >= xLen) { xCheck = 0; }
                    if (yCheck >= yLen) { yCheck = 0; }

                    if (universe[xCheck, yCheck] == true) count++;
                }
            }
            return count;
        }

        // The event called by the timer every Interval milliseconds.
        private void Timer_Tick(object sender, EventArgs e)
        {
            NextGeneration();
        }

        private void graphicsPanel1_Paint(object sender, PaintEventArgs e)
        {
            // Calculate the width and height of each cell in pixels
            // CELL WIDTH = WINDOW WIDTH / NUMBER OF CELLS IN X
            int cellWidth = graphicsPanel1.ClientSize.Width / universe.GetLength(0);
            // CELL HEIGHT = WINDOW HEIGHT / NUMBER OF CELLS IN Y
            int cellHeight = graphicsPanel1.ClientSize.Height / universe.GetLength(1);

            // A Pen for drawing the grid lines (color, width)
            Pen gridPen = new Pen(gridColor, 1);

            // A Brush for filling living cells interiors (color)
            Brush cellBrush = new SolidBrush(cellColor);

            // Iterate through the universe in the y, top to bottom
            for (int y = 0; y < universe.GetLength(1); y++)
            {
                // Iterate through the universe in the x, left to right
                for (int x = 0; x < universe.GetLength(0); x++)
                {
                    // A rectangle to represent each cell in pixels
                    Rectangle cellRect = Rectangle.Empty;
                    cellRect.X = x * cellWidth;
                    cellRect.Y = y * cellHeight;
                    cellRect.Width = cellWidth;
                    cellRect.Height = cellHeight;

                    // Fill the cell with a brush if alive
                    if (universe[x, y] == true)
                    {
                        e.Graphics.FillRectangle(cellBrush, cellRect);
                    }

                    // Outline the cell with a pen
                    e.Graphics.DrawRectangle(gridPen, cellRect.X, cellRect.Y, cellRect.Width, cellRect.Height);
                }
            }

            // Cleaning up pens and brushes
            gridPen.Dispose();
            cellBrush.Dispose();

        }

        private void graphicsPanel1_MouseClick(object sender, MouseEventArgs e)
        {
            // If the left mouse button was clicked
            if (e.Button == MouseButtons.Left)
            {
                // Calculate the width and height of each cell in pixels
                int cellWidth = graphicsPanel1.ClientSize.Width / universe.GetLength(0);
                int cellHeight = graphicsPanel1.ClientSize.Height / universe.GetLength(1);

                // Calculate the cell that was clicked in
                // CELL X = MOUSE X / CELL WIDTH
                int x = e.X / cellWidth;
                // CELL Y = MOUSE Y / CELL HEIGHT
                int y = e.Y / cellHeight;

                // Toggle the cell's state
                universe[x, y] = !universe[x, y];
                CountCells();

                // Tell Windows you need to repaint
                graphicsPanel1.Invalidate();
            }
        }
        //Exit Option
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        // Start Button
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            // set timer true
            timer.Enabled = true;
            graphicsPanel1.Invalidate();
        }
        //Stop Button
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            // set timer false
            timer.Enabled = false;
            graphicsPanel1.Invalidate();

        }
        //Next Button
        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            // call next generation once
            NextGeneration();
            graphicsPanel1.Invalidate();

        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int y = 0; y < universe.GetLength(1); y++)
            {
                // Iterate through the universe in the x, left to right
                for (int x = 0; x < universe.GetLength(0); x++)
                {
                    universe[x, y] = false;
                    scratchPad[x, y] = false;
                    CountCells();
                    generations = 0;
                    toolStripStatusLabelGenerations.Text = "Generations = " + generations.ToString();
                }
            }
            graphicsPanel1.Invalidate();
        }

        
        private void colorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog dlg = new ColorDialog();

            dlg.Color = graphicsPanel1.BackColor;

            if (DialogResult.OK == dlg.ShowDialog())
            {
                graphicsPanel1.BackColor = dlg.Color;

                graphicsPanel1.Invalidate();
            }
        }
        //universe Options
        private void modalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ModalDialog dlg = new ModalDialog();
           
            dlg.SetNumber(timer.Interval);
            dlg.SetHeight(universe.GetLength(1));
            dlg.SetWidth(universe.GetLength(0));
            if (DialogResult.OK == dlg.ShowDialog())
            {
                int newW = dlg.GetWidth();
                int newH = dlg.GetHeight();
                universe = new bool[newW, newH];
                scratchPad = new bool[newW, newH];
                timer.Interval = dlg.GetNumber();
                graphicsPanel1.Invalidate();
            }
        }
        //Color Menu Option
        private void colorToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ColorDialog dlg = new ColorDialog();
            dlg.Color = graphicsPanel1.BackColor;

            if(DialogResult.OK == dlg.ShowDialog())
            {
                graphicsPanel1.BackColor = dlg.Color;
            }
        }
        //Form Closed
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            //Update Property
            Properties.Settings.Default.PanelColor = graphicsPanel1.BackColor;
            Properties.Settings.Default.CellColor = cellColor;
            Properties.Settings.Default.GridColor = gridColor;
            Properties.Settings.Default.Timer = timer;
            Properties.Settings.Default.Save();
        }
        //Reset setting
        private void resetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Reset();

            graphicsPanel1.BackColor = Properties.Settings.Default.PanelColor;
            cellColor = Properties.Settings.Default.CellColor;
            gridColor = Properties.Settings.Default.GridColor;
            timer = Properties.Settings.Default.Timer;
        }
        //Reload Setting
        private void reloadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Reload();

            graphicsPanel1.BackColor = Properties.Settings.Default.PanelColor;
            cellColor = Properties.Settings.Default.CellColor;
            gridColor = Properties.Settings.Default.GridColor;
            timer = Properties.Settings.Default.Timer;
        }

        private void customizeToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void backgroundColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog dlg = new ColorDialog();
            dlg.Color = graphicsPanel1.BackColor;

            if (DialogResult.OK == dlg.ShowDialog())
            {
                graphicsPanel1.BackColor = dlg.Color;
            }
        }

        private void cellColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog dlg = new ColorDialog();
            dlg.Color = cellColor;

            if (DialogResult.OK == dlg.ShowDialog())
            {
                cellColor = dlg.Color;
            }
        }

        private void gridColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog dlg = new ColorDialog();
            dlg.Color = gridColor;
            if (DialogResult.OK == dlg.ShowDialog())
            {
                gridColor = dlg.Color;
            }
        }

        private void fromTimeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RandomizeTime();
        }

        private void fromSeedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int result = 0;
            RandomSeed dlg = new RandomSeed();
            dlg.Seed = result;
            if(DialogResult.OK == dlg.ShowDialog())
            {
                result = dlg.Seed;
                RandomizeSeed(result);
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "All Files|*.*|Cells|*.cells";
            dlg.FilterIndex = 2; dlg.DefaultExt = "cells";
            if(DialogResult.OK == dlg.ShowDialog())
            {
                StreamWriter writer = new StreamWriter(dlg.FileName);
                for (int y = 0; y < universe.GetLength(1); y++)
                {
                    String currentRow = String.Empty;
                    for (int x = 0; x < universe.GetLength(0); x++)
                    {
                        if(universe[x,y] == true)
                        {
                            currentRow +='O';
                        }
                        else if(universe[x,y] == false)
                        {
                            currentRow += '.';
                        }
                        writer.WriteLine(currentRow);
                    }
                }
                writer.Close();
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "All Files|*.*|Cells|*.cells";
            dlg.FilterIndex = 2;
            if(DialogResult.OK == dlg.ShowDialog())
            {
                StreamReader reader = new StreamReader(dlg.FileName);
                int maxWidth = 0;
                int maxHeight = 0;
                while(!reader.EndOfStream)
                {
                    string row = reader.ReadLine();
                    if (row[0] == '!') 
                    {
                        continue;
                    }
                    if(row[0] != '!')
                    {
                        maxHeight ++;
                        maxWidth = row.Length;

                    }
                }
                universe = new bool[maxWidth, maxHeight];
                scratchPad = new bool[maxWidth, maxHeight];
                reader.BaseStream.Seek(0, SeekOrigin.Begin);
                int yPos = 0;
                while (!reader.EndOfStream)
                {
                    string row = reader.ReadLine();
                    if(row[0] == '!') { continue; }
                    if(row[0] != '!')
                    {
                        for (int xPos = 0; xPos < row.Length; xPos++)
                        {
                            if(row[xPos] == 'O')
                            {
                                universe[xPos,yPos ] = true;
                            }
                            else if(row[xPos] == '.')
                            {
                                universe[xPos,yPos] = false;
                            }
                            
                        }
                    }
                }
                reader.Close();
            }
        }
    }
}

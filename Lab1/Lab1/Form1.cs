using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace Lab1
{
    public partial class Form1 : Form

    {
        public PictureBox[,] pbs = new PictureBox[50, 50];
        public byte[,] pix = new byte[50, 50];
        public Bitmap bmp;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            

        }

        private PictureBox Cr_PB(int i, int j, bool fl)
        {
            PictureBox pictureBox2 = new PictureBox();
            if (fl)
            {
                pictureBox2.BackColor = System.Drawing.Color.Black;
            }
            else
            {
                pictureBox2.BackColor = System.Drawing.Color.White;
            }
            pictureBox2.Location = new System.Drawing.Point(10*i, 10*j);
            pictureBox2.Name = "PB_"+Convert.ToString(i)+"_"+Convert.ToString(j);
            pictureBox2.Size = new System.Drawing.Size(10, 10);
            pictureBox2.TabIndex = 0;
            pictureBox2.TabStop = false;
            pictureBox2.Click += new System.EventHandler(pictureBox2_Click);
            pictureBox2.MouseHover += new System.EventHandler(pictureBox2_MouseHover);
            return pictureBox2; 
        }
       

        private void button1_Click(object sender, EventArgs e)
        {
            
            
            if(openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = Image.FromFile(openFileDialog1.FileName);
            } 
            bmp = new Bitmap(pictureBox1.Image);
            
            
            PictureBox pb;
            for(int i = 0; i < 50; i++)
            {
                for(int j = 0; j < 50; j++)
                {
                    
                    if (bmp.GetPixel(i, j).ToArgb() == -1)
                    {
                        pix[i, j] = 0;
                    }
                    else
                    {
                        pix[i, j] = 1;
                    }

                }
            }


            for(int i = 0; i < 50; i++)
            {
                for(int j = 0; j < 50; j++)
                {
                    pb = Cr_PB(i, j, pix[i, j] == 1);
                    pbs[i, j] = pb;
                    panel1.Controls.Add(pbs[i,j]);
                }
            }

            pictureBox2.Image = bmp;


        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            var pb = sender as PictureBox;
            if(pb.BackColor == Color.White)
            {
                pb.BackColor = Color.Black;
            }
            else
            {
                pb.BackColor = Color.White;
            }
            int x; int y;

            String[] l = pb.Name.Remove(0, 3).Split('_');
            x = Convert.ToInt16(l[0]);
            y = Convert.ToInt16(l[1]);

            if (pix[x, y]==0)
            {
                pix[x, y] = 1;
                bmp.SetPixel(x, y, Color.Black);
            }
            else
            {
                pix[x, y] = 0;
                bmp.SetPixel(x, y, Color.White);
            }
            pictureBox2.Image = bmp;



        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK) //если в диалоговом окне нажата кнопка "ОК"
            {
                try
                {
                    bmp.Save(saveFileDialog1.FileName, System.Drawing.Imaging.ImageFormat.Bmp);
                }
                catch
                {
                    MessageBox.Show("Невозможно сохранить изображение", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void pictureBox2_MouseHover(object sender, EventArgs e)
        {
            int x;int y;
            var pb = sender as PictureBox;
            String[] l = pb.Name.Remove(0, 3).Split('_');
            x = Convert.ToInt16(l[0]);
            y = Convert.ToInt16(l[1]);

            int k4 = 0;
            int k8 = 0;
            try
            {
                if (pbs[x - 1, y - 1].BackColor == Color.Black) { k8++; }
                
            }
            catch (IndexOutOfRangeException){ }
            try
            {
                if (pbs[x - 1, y].BackColor == Color.Black) {k4++; k8++;}
            
                
            }
            catch (IndexOutOfRangeException) { }
            try
            {
                if (pbs[x - 1, y + 1].BackColor == Color.Black) k8++;
            }
            catch (IndexOutOfRangeException) { }
            try
            {
                if (pbs[x, y - 1].BackColor == Color.Black) { k4++; k8++; }
            }
            catch (IndexOutOfRangeException) { }
            try
            {
                if (pbs[x , y + 1].BackColor == Color.Black){ k4++; k8++;}
            }
            catch (IndexOutOfRangeException) { }
            try
            {
                if (pbs[x + 1, y - 1].BackColor == Color.Black) k8++;
            }
            catch (IndexOutOfRangeException) { }
            try
            {
                if (pbs[x + 1, y].BackColor == Color.Black) {k4++; k8++;
            }
            }
            catch (IndexOutOfRangeException) { }
            try
            {
                if (pbs[x + 1, y + 1].BackColor == Color.Black)  k8++;
            }
            catch (IndexOutOfRangeException) { }

            textBox1.Text = "K4: " + Convert.ToString(k4) + "  k8: " + Convert.ToString(k8);


        }
    }
}

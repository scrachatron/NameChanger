using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

// This is the code for your desktop app.
// Press Ctrl+F5 (or go to Debug > Start Without Debugging) to run your app.

namespace NameChanger
{
    public partial class Form1 : Form
    {
        public static Bitmap image = new Bitmap(1920, 1080);
        private string name = "";


        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            name = textBox1.Text;

            string name2 = StringToHex(name);
            textBox1.Text = "";
            if (name2 == "")
            {
                MessageBox.Show("Name cannot be blank");
                return;
            }

            while (name2.Length % 6 != 0)
            {
                name2 += "8";
            }

            string[] colors = new string[name2.Length / 6];
            for (int i = 0; i < colors.Length; i++)
            {
                colors[i] = name2.Substring(i * 6, 6);
            }


            Color[] sets = new Color[colors.Length];

            for (int i = 0; i < sets.Length; i++)
            {
                sets[i] = System.Drawing.ColorTranslator.FromHtml("#" + colors[i]);
            }


            try
            {

                int rectwidth = image.Width / sets.Length;
                int currpos = rectwidth;
                int setnumber = 0;
                // Loop through the images pixels to reset color.
                for (int x = 0; x < image.Width; x++)
                {
                    if (x > currpos)
                    {
                        currpos += rectwidth;
                        setnumber++;
                        if (setnumber > sets.Length - 1)
                            setnumber = sets.Length - 1;
                    }
                    for (int y = 0; y < image.Height; y++)
                    {
                        image.SetPixel(x, y, sets[setnumber]);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            pictureBox1.BackgroundImage = image;
            pictureBox1.Refresh();


        }

        private string StringToHex(string hexstring)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char t in hexstring)
            {
                //Note: X for upper, x for lower case letters
                sb.Append(Convert.ToInt32(t).ToString("x"));
            }
            return sb.ToString();
        }



        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            SaveFileDialog SaveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.FileName = name;      
            saveFileDialog1.CheckFileExists = false;
            saveFileDialog1.CheckPathExists = true;
            saveFileDialog1.DefaultExt = "bmp";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;
            saveFileDialog1.Filter = "Bitmap Image|*.bmp";
            saveFileDialog1.Title = "Save an Image File";
            saveFileDialog1.ShowDialog();

            // If the file name is not an empty string open it for saving.  
            if (saveFileDialog1.FileName != "")
            {
                // Saves the Image via a FileStream created by the OpenFile method.  
                System.IO.FileStream fs =
                   (System.IO.FileStream)saveFileDialog1.OpenFile();
                // Saves the Image in the appropriate ImageFormat based upon the  
                // File type selected in the dialog box.  
                // NOTE that the FilterIndex property is one-based.  
                switch (saveFileDialog1.FilterIndex)
                {
                    case 1:
                        image.Save(fs,
                            System.Drawing.Imaging.ImageFormat.Bmp);
                        break;
                }

                fs.Close();
            }
        }
    }
}

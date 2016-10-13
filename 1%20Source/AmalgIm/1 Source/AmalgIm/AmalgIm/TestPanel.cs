/* TestPanel.cs
   Author: Mike Hurley
   This file contains the form's graphical attributes 
   and the code that will be run before it is displayed.
*/

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace AmalgIm
{
    public partial class TestPanel : Form
    {
        private VScrollBar vScroll;
        private Panel display;
        private List<DirectoryInfo> imageDirs;
        public DirectoryInfo[] dropdownList;

        public TestPanel()
        {//here is the in-house form constructor; contained is a Microsoft method to instantiate the form
            InitializeComponent();
        }

        private void TestPanel_Load(object sender, EventArgs e)
        {//this code is called when the windows form is loaded after a successful build
            imageDirs = new List<DirectoryInfo>();

            //Set up the form. Code courtesy of Microsoft tutorial
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.BackColor = Color.White;
            this.ForeColor = Color.Black;
            this.Size = new System.Drawing.Size(800, 800);
            this.Text = "AmalgIm Native Image Collection";
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterScreen;

            //set up scrollbar and display panel
            vScroll = new VScrollBar();
            display = new Panel();

            this.display.Size = new System.Drawing.Size(800, 800);

            this.vScroll.LargeChange = 268;
            this.vScroll.Location = new System.Drawing.Point(680, 0);
            this.vScroll.Maximum = 800;
            this.vScroll.Size = new System.Drawing.Size(13, 750);
            this.vScroll.ValueChanged += new System.EventHandler(this.vScroll_ValueChanged);

            //grab files from base directory
            //String path = "./../../../../..";//amalgim base folder
            String path = "C:\\Users\\Owner\\Desktop";

            DirectoryInfo d = new DirectoryInfo(path);

            try
            {
                ReturnImageDir(d);
                dropdownList = new DirectoryInfo[imageDirs.Count];
                int count = 0;

                foreach(var f in imageDirs)
                {
                    count++;
                    dropdownList[count-1] = f;
                }


                this.vScroll.Maximum = this.display.Height;
                this.Controls.Add(vScroll);
            }
            catch (DirectoryNotFoundException ex)
            {//notify user of invalid directory supplied for image search
                Label warning1 = new Label();
                Label warning2 = new Label();

                warning1.Text = "Directory " + path + " not found";
                warning1.BackColor = Color.LawnGreen;
                warning1.Location = new System.Drawing.Point(50, 100);
                warning1.Size = new System.Drawing.Size(600, 25);

                warning2.Text = ex.Message;
                warning2.BackColor = Color.LawnGreen;
                warning2.Location = new System.Drawing.Point(50, 150);
                warning2.Size = new System.Drawing.Size(600, 25);

                this.display.Controls.Add(warning1);
                this.display.Controls.Add(warning2);
            }//end try-catch
            DropDown.Items.Clear();

            foreach (var f in imageDirs)
            {
                DropDown.Items.Add(f.Name);
            }

            this.Controls.Add(display);
        }//end TestPanel_Load

        private void ReturnImageDir(DirectoryInfo dir)
        {//load image directories into running list
            if (isImageDir(dir))
            {
                imageDirs.Add(dir);
            }
            foreach (var d in dir.GetDirectories())
            {//recursive call to iterate through subdirectories
                ReturnImageDir(d);
            }
        }//end ReturnImageDir

        private Boolean isImageDir(DirectoryInfo dir)
        {//stops on first nonimage found
            bool val = false;

            foreach (var f in dir.GetFiles())
            {
                if (isImage(f))
                {
                    val = true;
                    break;
                }
            }

            return val;
        }

        private void vScroll_ValueChanged(object sender, System.EventArgs e)
        {//method courtesy of Microsoft scrollbar tutorial - event handler for scrollbar
            this.display.Top = -this.vScroll.Value;
        }

        private Boolean isImage(FileInfo f)
        {//see if passed file is of appropriate type
            //must account for periods in file names before extension is separated
            String extension = reverseStr(reverseStr(f.Name).Split(new char[] { '.' }[0])[0]);
            return (caseInsensEqual(extension, "jpg") ||
                    caseInsensEqual(extension, "jpeg") ||
                    caseInsensEqual(extension, "png") ||
                    caseInsensEqual(extension, "gif") ||
                    caseInsensEqual(extension, "bmp")
                    );
        }//end isImage

        private String reverseStr(String s)
        {//reverse a given string
            char[] letters = s.ToCharArray();
            int incr = 0;
            if (s.Length % 2 == 0)
            {
                incr = s.Length / 2;
            }
            else
            {//incrementing from two directions, leaves odd(middle) letter intact
                incr = (s.Length - 1) / 2;
            }
            //swap char by char

            //array max index is Length - 1
            int arrLength = letters.Length - 1;
            for (int i = 0; i < incr; i++)
            {
                char temp = letters[i];
                letters[i] = letters[arrLength - i];
                letters[arrLength - i] = temp;
            }

            String val = new String(letters);
            return val;
        }//end reverseStr

        private Boolean caseInsensEqual(String orig, String patt)
        {//compare strings by value in upper/lower case
            Boolean match = false;
            if (orig.Length == patt.Length)
            {
                char[] origL = orig.ToCharArray();
                char[] pattL = patt.ToCharArray();

                for (int i = 0; i < origL.Length; i++)
                {
                    if (origL[i] + "" == pattL[i] + "" || (origL[i] + "").ToLower() == (pattL[i] + "").ToLower())
                    {//check multiple cases, in this case my args are lowercase so I only check for uppercase extensions
                        match = true;
                    }
                    else
                    {//stop on first nonmatching char
                        match = false;
                        break;
                    }
                }
            }
            return match;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {//for dropdown list items
            int n = DropDown.SelectedIndex;
            this.display.Controls.Clear();
            int imageCount = 0;

            foreach (var x in dropdownList[n].GetFiles())
            {
                Label temp = new Label();

                temp.Text = x.Name;
                temp.BackColor = Color.Gray;
                //first label starts at y = 20, adjust successive labels accordingly
                temp.Location = new System.Drawing.Point(50, (imageCount > 0 ? 20 + (imageCount * 30) : 20) + 30);
                temp.Size = new System.Drawing.Size(600, 25);

                this.display.Controls.Add(temp);
                imageCount++;

                if (temp.Location.Y >= 750)
                {
                    this.display.Height += 30;
                    this.vScroll.Maximum = this.display.Height;
                }
            }//end foreach
        }
    }
}

//this is used when reading files not directories
//String[] pathSegments = f.DirectoryName.Split(new char[] { '\\' }[0]);
//temp.Text = imageCount + " " + (pathSegments[pathSegments.Length - 1]) + " : " + f.Name;

//temp.Text = dropdownList[n][i].Name;
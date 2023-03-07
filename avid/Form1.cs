using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Media;
using System.Reflection;
using System.IO;

namespace avid
{
    public partial class Form1 : Form
    {
        String appDir = "";

        List<string> paths;
        List<string> events;
        List<string> mn;

        List<string> ob;
        List<string> post;
        List<string> random_yes;

        bool next = false;
        bool complete = false;

        public Form1()
        {
            InitializeComponent();

            appDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase);

            paths = new List<string>();
            events = new List<string>();
            mn = new List<string>();

            ob = new List<string>();
            post = new List<string>();
            random_yes = new List<string>();

            cbPath.SelectedIndex = 0;

            ob.Clear();
            var path = Path.Combine(appDir, @"ob.txt");
            FileInfo fi = new FileInfo(path);
            if (fi.Exists)
            {
                using (StreamReader sr = fi.OpenText())
                {
                    string s = "";
                    while ((s = sr.ReadLine()) != null)
                    {
                        ob.Add(s);
                    }
                }
            }

            random_yes.Clear();
            path = Path.Combine(appDir, @"random_yes.txt");
            fi = new FileInfo(path);
            if (fi.Exists)
            {
                using (StreamReader sr = fi.OpenText())
                {
                    string s = "";
                    while ((s = sr.ReadLine()) != null)
                    {
                        random_yes.Add(s);
                    }
                }
            }

            post.Clear();
            path = Path.Combine(appDir, @"post.txt");
            fi = new FileInfo(path);
            if (fi.Exists)
            {
                using (StreamReader sr = fi.OpenText())
                {
                    string s = "";
                    while ((s = sr.ReadLine()) != null)
                    {
                        post.Add(s);
                    }
                }
            }

            
            


            paths.Clear();
            cbRecord.Items.Clear();

            path = Path.Combine(appDir, @"path.txt");
            fi = new FileInfo(path);
            if (fi.Exists)
            {

                using (StreamReader sr = fi.OpenText())
                {
                    bool b = false;
                    int state = 0;
                    string s = "";
                    string res = "";
                    while ((s = sr.ReadLine()) != null)
                    {
                        for (int i = 0; i < s.Length; i++)
                        {
                            char c = s[i];

                            if (c == ';')
                            {
                                
                                

                                if (state == 0)
                                {
                                    cbRecord.Items.Add(res);
                                }
                                else if (state == 1)
                                {
                                    paths.Add(res);
                                }
                                else if (state == 2)
                                {
                                    events.Add(res);
                                    b = true;
                                    state = -1;
                                }
                                res = "";
                                state++;
                            }
                            else
                            {
                                res = res + c;
                            }    
                        }
                        if (b)
                        {
                            mn.Add(res);
                            res = "";
                            b = false;
                        }
                    }
                }

                cbRecord.SelectedIndex = 0;
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (complete)
            {
                if (cbPath.SelectedIndex == 0)
                {
                    if (cbRecord.SelectedIndex > 0)
                    {
                        cbRecord.SelectedIndex = cbRecord.SelectedIndex - 1;
                    }
                }
                else
                {
                    if (cbRecord.SelectedIndex < cbRecord.Items.Count - 1)
                    {
                        cbRecord.SelectedIndex = cbRecord.SelectedIndex + 1;
                    }
                }
            }

            if (!next)
            {
                var fullPath = Path.Combine(appDir, @"waves\" + paths[cbRecord.SelectedIndex]);
                SoundPlayer player = new SoundPlayer(fullPath);
                player.PlaySync();
                complete = true;
                next = true;
            }
            else
            {

                for (int j = 0; j < ob.Count; j++)
                {
                    var fullPath1 = Path.Combine(appDir, @"waves\" + ob[j]);
                    SoundPlayer player1 = new SoundPlayer(fullPath1);
                    player1.PlaySync();
                }

                var fullPath = Path.Combine(appDir, @"waves\" + events[cbRecord.SelectedIndex]);
                SoundPlayer player = new SoundPlayer(fullPath);
                player.PlaySync();

                for (int j = 0; j < ob.Count; j++)
                {
                    var fullPath1 = Path.Combine(appDir, @"waves\" + post[j]);
                    SoundPlayer player1 = new SoundPlayer(fullPath1);
                    player1.PlaySync();
                }

                if (mn[cbRecord.SelectedIndex] == "yes")
                {
                    Random rnd = new Random();
                    int i = rnd.Next(0, random_yes.Count);
                    fullPath = Path.Combine(appDir, @"waves\"+random_yes[i]);
                    player = new SoundPlayer(fullPath);
                    player.PlaySync();
                }

                complete = false;
                next = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var fullPath = Path.Combine(appDir, @"waves\" + paths[cbRecord.SelectedIndex]);
            SoundPlayer player = new SoundPlayer(fullPath);
            player.PlaySync();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (complete)
            {
                if (cbPath.SelectedIndex == 0)
                {
                    if (cbRecord.SelectedIndex < cbRecord.Items.Count - 1)
                    {
                        cbRecord.SelectedIndex = cbRecord.SelectedIndex + 1;
                    }
                }
                else
                {
                    if (cbRecord.SelectedIndex > 0)
                    {
                        cbRecord.SelectedIndex = cbRecord.SelectedIndex - 1;
                    }
                }
            }
            if (!next)
            {
                var fullPath = Path.Combine(appDir, @"waves\" + paths[cbRecord.SelectedIndex]);
                SoundPlayer player = new SoundPlayer(fullPath);
                player.PlaySync();
                complete = true;
                next = true;
            }
            else
            {
                
                for (int j = 0; j < ob.Count; j++)
                {
                    var fullPath1 = Path.Combine(appDir, @"waves\"+ob[j]);
                    SoundPlayer player1 = new SoundPlayer(fullPath1);
                    player1.PlaySync();
                }

                var fullPath = Path.Combine(appDir, @"waves\" + events[cbRecord.SelectedIndex]);
                SoundPlayer player = new SoundPlayer(fullPath);
                player.PlaySync();

                for (int j = 0; j < ob.Count; j++)
                {
                    var fullPath1 = Path.Combine(appDir, @"waves\" + post[j]);
                    SoundPlayer player1 = new SoundPlayer(fullPath1);
                    player1.PlaySync();
                }

                if (mn[cbRecord.SelectedIndex] == "yes")
                {
                    Random rnd = new Random();
                    int i = rnd.Next(0, random_yes.Count);
                    fullPath = Path.Combine(appDir, @"waves\" + random_yes[i]);
                    player = new SoundPlayer(fullPath);
                    player.PlaySync();
                }

                complete = false;
                next = false;
            }
            
        }
    }
}
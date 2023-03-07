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

        List<string> greeting;

        List<string> all;

        List<string> end;
        List<string> post_ost;

        List<string> replay;


        bool next = false;
        bool complete = false;

        bool started = false;

        //bool selected = false;

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

            greeting = new List<string>();

            all = new List<string>();

            end = new List<string>();

            post_ost = new List<string>();

            replay = new List<string>();

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

            greeting.Clear();
            path = Path.Combine(appDir, @"greeting.txt");
            fi = new FileInfo(path);
            if (fi.Exists)
            {
                using (StreamReader sr = fi.OpenText())
                {
                    string s = "";
                    while ((s = sr.ReadLine()) != null)
                    {
                        greeting.Add(s);
                    }
                }
            }


            all.Clear();
            path = Path.Combine(appDir, @"all.txt");
            fi = new FileInfo(path);
            if (fi.Exists)
            {
                using (StreamReader sr = fi.OpenText())
                {
                    string s = "";
                    while ((s = sr.ReadLine()) != null)
                    {
                        all.Add(s);
                    }
                }
            }

            end.Clear();
            path = Path.Combine(appDir, @"end.txt");
            fi = new FileInfo(path);
            if (fi.Exists)
            {
                using (StreamReader sr = fi.OpenText())
                {
                    string s = "";
                    while ((s = sr.ReadLine()) != null)
                    {
                        end.Add(s);
                    }
                }
            }

            post_ost.Clear();
            path = Path.Combine(appDir, @"post_ost.txt");
            fi = new FileInfo(path);
            if (fi.Exists)
            {
                using (StreamReader sr = fi.OpenText())
                {
                    string s = "";
                    while ((s = sr.ReadLine()) != null)
                    {
                        post_ost.Add(s);
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

                replay.Clear();
                replay.Add(paths[0]);
            }

        }

        private void Speach(String path)
        {
            SpeachNoReplay(path);
            replay.Add(path);
        }

        private void SpeachNoReplay(String path)
        {
            var fullPath1 = Path.Combine(appDir, @"waves\" + path);
            SoundPlayer player1 = new SoundPlayer(fullPath1);
            player1.PlaySync();    
        }


        private void button2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < replay.Count; i++)
            {
                SpeachNoReplay(replay[i]);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            replay.Clear();
            if ((complete) || (!started))
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

            for (int j = 0; j < all.Count; j++)
            {
                Speach(all[j]);
            }

            if (!next)
            {
                Speach(paths[cbRecord.SelectedIndex]);
                if ((cbRecord.SelectedIndex == 0) || (cbRecord.SelectedIndex == cbRecord.Items.Count-1))
                {
                    started = false;
                }

                if (cbPath.SelectedIndex == 0)
                {
                    if (cbRecord.SelectedIndex == cbRecord.Items.Count - 1)
                    {
                        for (int j = 0; j < end.Count; j++)
                        {
                            Speach(end[j]);
                        }
                    }
                }
                else
                {
                    if (cbRecord.SelectedIndex == 0)
                    {
                        for (int j = 0; j < end.Count; j++)
                        {
                            Speach(end[j]);
                        }
                    }
                }

                if (mn[cbRecord.SelectedIndex] == "yes")
                {
                    if (post_ost.Count > 0)
                    {
                        Random rnd = new Random();
                        int i = rnd.Next(0, post_ost.Count);
                        Speach(post_ost[i]);
                    }
                }

                if (cbPath.SelectedIndex == 0)
                {
                    if (cbRecord.SelectedIndex == cbRecord.Items.Count - 1)
                    {
                        cbPath.SelectedIndex = 1;
                    }
                }
                else
                {
                    if (cbRecord.SelectedIndex == 0)
                    {
                        cbPath.SelectedIndex = 0;
                    }
                }

                complete = true;
                next = true;
            }
            else
            {
                
                for (int j = 0; j < ob.Count; j++)
                {
                    Speach(ob[j]);
                }

                Speach(events[cbRecord.SelectedIndex]);

                if (cbPath.SelectedIndex == 0)
                {
                    if (cbRecord.SelectedIndex == cbRecord.Items.Count - 1)
                    {
                        for (int j = 0; j < end.Count; j++)
                        {
                            Speach(end[j]);
                        }
                    }
                }
                else
                {
                    if (cbRecord.SelectedIndex == 0)
                    {
                        for (int j = 0; j < end.Count; j++)
                        {
                            Speach(end[j]);
                        }
                    }
                }

                for (int j = 0; j < ob.Count; j++)
                {
                    Speach(post[j]);
                }

                if (mn[cbRecord.SelectedIndex] == "yes")
                {
                    if (random_yes.Count > 0)
                    {
                        Random rnd = new Random();
                        int i = rnd.Next(0, random_yes.Count);
                        Speach(random_yes[i]);
                    }
                }

                

                complete = false;
                next = false;
            }
        }

      

        private void button4_Click(object sender, EventArgs e)
        {
            replay.Clear();
            for (int j = 0; j < all.Count; j++)
            {
                Speach(all[j]);
            }

            if (greeting.Count>0)
            {
                Random rnd = new Random();
                int i = rnd.Next(0, greeting.Count);
                Speach(greeting[i]);
            }
        }

        private void cbRecord_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!started)
            {
                next = true;
                started = true;
                complete = true;
            }
        }

        private void cbPath_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbPath.SelectedIndex == 0)
            {
                button3.Text = ">>";
            }
            else
            {
                button3.Text = "<<";
            }

        }
    }
}
/* @author:  nekitgam (Дубровский Никита Николаевич)
 * @date:    6.03.2023
 * @license: GNU GPL v3
 */

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
        //Путь до папки exe файла
        String appDir = "";

        //Списки параметров из конфигурационных файлов
        List<string> paths;       //Остановки
        List<string> events;      //События перед остановками (Типа "Следующая остановка - такая-то")
        List<string> mn;          //Воспроизводить ли random_yes на данной остановке, бывает yes или no
        List<string> ob;          //Обязательное воспроизвдение перед events (Например "осторожно, двери закрываются", после выполняется events "Следующая остановка такая-то")
        List<string> post;        //Обязательное вопроизведение после events
        List<string> random_yes;  //Воспроизведение случайной записи из списка, при условии что на данной остановке стоит yes в paths
        List<string> greeting;    //Приветствие - воспроизводит случайное приветствие из списка при нажатии на кнопку
        List<string> all;         //Обязательно воспроизводятся все записи перед path, ob или events (короче, всегда в начале, типа звук "ты-дын")
        List<string> end;         //Объявляет, что остановка конечная (воспроизводятся все записи в списке)
        List<string> post_ost;    //Воспроизводит случайную запись после paths, если в звписи есть yes

        //Список повторяемых записей
        List<string> replay;      //Пополяется после каждого воспроизведения служит для повторения последнего воспроизведения (кнопка "повторить")


        bool next = false;        //Переключатель между paths и events, чтобы сначало говорило "следующая остановка такая-то", а в следующий раз "остановка такая-то"
        bool complete = false;    //Переключатель, определяющий препоследнюю запись при переключении вручную позиции остановки

        bool started = false;     //Переключтель, нужен для определния 1-го нажатия

        /*Основаня функция инициализации формы как экземпляра класса*/
        public Form1()
        {
            InitializeComponent();

            //Присваеиваем appDir путь до папки с программой
            appDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase);

            //Инициализируем списки
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

            //Указываем, что движение идет вперед
            cbPath.SelectedIndex = 0;

            //Очищаем списки и загружаем данные из файлов
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

            //Очищаем список и выпадающее меню с выбором остановки
            paths.Clear();
            cbRecord.Items.Clear();
            //Считываем записи с файла path.txt - запихивая в paths и events пути до wav файлов, 
            //в mn - yes или no записи, и текст в выпадающий список выбора остановки
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

        /*Функция воспроизведения записей с добавлением их в replay список*/
        private void Speach(String path)
        {
            SpeachNoReplay(path);
            replay.Add(path);
        }

        /*Функция воспроизведения записей*/
        private void SpeachNoReplay(String path)
        {
            var fullPath1 = Path.Combine(appDir, @"waves\" + path);
            SoundPlayer player1 = new SoundPlayer(fullPath1);
            player1.PlaySync();    
        }

        /*Кнопка "Повторить" - посторяет последние записи из replay списка*/
        private void button2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < replay.Count; i++)
            {
                SpeachNoReplay(replay[i]);
            }
        }

        /*Кнопка ">>" или "<<" (если обратное направление). Воспроизводит записи в зависимости от условий*/
        private void button3_Click(object sender, EventArgs e)
        {
            //Чистим replay список
            replay.Clear();
            //Проверяем направление или конечную остановку, чтобы переключить остановку на следующую или предыдущую
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

            //Воспроизводим обязательный "ты-дын"
            for (int j = 0; j < all.Count; j++)
            {
                Speach(all[j]);
            }

            if (!next) //Воспроизводим если мы на остановке
            {
                //Воспроизводим остановку
                Speach(paths[cbRecord.SelectedIndex]);
                if ((cbRecord.SelectedIndex == 0) || (cbRecord.SelectedIndex == cbRecord.Items.Count-1))
                {
                    started = false;
                }

                //Если конечная - говорим, что она конечная (с условием направления, конечно)
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

                //Если в path.txt на этйо позиции указано yes - то воспроизводим сообщение после названия остановки
                if (mn[cbRecord.SelectedIndex] == "yes")
                {
                    if (post_ost.Count > 0)
                    {
                        Random rnd = new Random();
                        int i = rnd.Next(0, post_ost.Count);
                        Speach(post_ost[i]);
                    }
                }

                //Если остановка конечная - разворачиваемся
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

                //Переключаемся на режим "Выезжаем к остановке" и ждем еще одного нажатия кнопки
                complete = true;
                next = true;
            }
            else //Воспроизводим - если выезжаем к остановке
            {
                //Воспроизводим запись перед отправкой (осторожно, двери закрываются)
                for (int j = 0; j < ob.Count; j++)
                {
                    Speach(ob[j]);
                }

                //Воспроизводим сообщение об отправке (следующая остановка такая-то)
                Speach(events[cbRecord.SelectedIndex]);

                //Если следующая остановка конечная - говорим об этом
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

                //Воспроизводим после объявления конечной
                for (int j = 0; j < ob.Count; j++)
                {
                    Speach(post[j]);
                }

                //Если в path.txt в данной записи стоит yes - воспроизводим случайную запись
                if (mn[cbRecord.SelectedIndex] == "yes")
                {
                    if (random_yes.Count > 0)
                    {
                        Random rnd = new Random();
                        int i = rnd.Next(0, random_yes.Count);
                        Speach(random_yes[i]);
                    }
                }

                
                //Переключаем режим на "приехали на остановку" и ждем следующего нажатия кнопки
                complete = false;
                next = false;
            }
        }

      
        /*Кнопка воспроизвдения приветствия*/
        private void button4_Click(object sender, EventArgs e)
        {
            //Чистим replay список
            replay.Clear();

            //Воспроизводим обязательный ты-дын
            for (int j = 0; j < all.Count; j++)
            {
                Speach(all[j]);
            }

            //Воспроизводим случайное приветствие
            if (greeting.Count>0)
            {
                Random rnd = new Random();
                int i = rnd.Next(0, greeting.Count);
                Speach(greeting[i]);
            }
        }

        /*Функция выбора записи в списке*/
        private void cbRecord_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!started)
            {
                //Включаем режим "следующая остановка"
                next = true;
                started = true;
                complete = true;
            }
        }

        /*Функция выбьра направления*/
        private void cbPath_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Просто меняем значок на кнопке в зависимости от направления
            if (cbPath.SelectedIndex == 0)
            {
                button3.Text = ">>";
            }
            else
            {
                button3.Text = "<<";
            }

        }

        /*Делаем кнопки одинакового размера на любом экране*/
        private void Form1_Load(object sender, EventArgs e)
        {
            button2.Width = this.Width / 2;
            button3.Width = this.Width / 2;
        }
    }
}
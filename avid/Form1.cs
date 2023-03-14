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

        //Контроллер маршрутов
        RoutesController rc;

        //Список повторяемых записей
        List<string> replay;      //Пополяется после каждого воспроизведения служит для повторения последнего воспроизведения (кнопка "повторить")

        //Параметры управления переключения маршрута
        bool next = false;        //Переключатель между paths и events, чтобы сначало говорило "следующая остановка такая-то", а в следующий раз "остановка такая-то"
        bool complete = false;    //Переключатель, определяющий препоследнюю запись при переключении вручную позиции остановки

        bool started = false;     //Переключтель, нужен для определния 1-го нажатия

        /*Основаня функция инициализации формы как экземпляра класса*/
        public Form1()
        {
            InitializeComponent();

            //Присваеиваем appDir путь до папки с программой
            appDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase);

            //Инициализируем реплей список
            replay = new List<string>();

            //Инициализируем контроллер маршрутов
            rc = new RoutesController();
            
            //Чистим выпадающее меню выбора маршрута и заполняем его из записей контроллера
            cbRoute.Items.Clear();
            for (int i = 0; i < rc.GetCount(); i++)
            {
                cbRoute.Items.Add(rc.GetName(i));
            }

            if (cbRoute.Items.Count > 0)
            {
                cbRoute.SelectedIndex = 0;
            }
        }

        /*Функция воспроизведения записей с добавлением их в replay список*/
        private void Speach(String path)
        {
            if (path == "") return;
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
            button2.Visible = false;
            button3.Visible = false;
            button4.Visible = false;
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
            for (int j = 0; j < rc.GetAllCount(cbRoute.SelectedIndex); j++)
            {
                Speach(rc.GetAllPath(cbRoute.SelectedIndex,j));
            }

            if (!next) //Воспроизводим если мы на остановке
            {
                //Воспроизводим перед остановкой
                for (int i = 0; i < rc.routes[cbRoute.SelectedIndex].general[rc.routes[cbRoute.SelectedIndex].GetIndexFromGeneralName(cbRecord.Items[cbRecord.SelectedIndex].ToString())].prev_ost.Count; i++)
                {
                    Speach(rc.routes[cbRoute.SelectedIndex].general[rc.routes[cbRoute.SelectedIndex].GetIndexFromGeneralName(cbRecord.Items[cbRecord.SelectedIndex].ToString())].prev_ost[i]);
                }

                //Воспроизводим остановку
                for (int i = 0; i < rc.routes[cbRoute.SelectedIndex].general[rc.routes[cbRoute.SelectedIndex].GetIndexFromGeneralName(cbRecord.Items[cbRecord.SelectedIndex].ToString())].ost_record.Count; i++)
                {
                    Speach(rc.routes[cbRoute.SelectedIndex].general[rc.routes[cbRoute.SelectedIndex].GetIndexFromGeneralName(cbRecord.Items[cbRecord.SelectedIndex].ToString())].ost_record[i]);
                }

                //Воспроизводим после остановки
                for (int i = 0; i < rc.routes[cbRoute.SelectedIndex].general[rc.routes[cbRoute.SelectedIndex].GetIndexFromGeneralName(cbRecord.Items[cbRecord.SelectedIndex].ToString())].post_ost.Count; i++)
                {
                    Speach(rc.routes[cbRoute.SelectedIndex].general[rc.routes[cbRoute.SelectedIndex].GetIndexFromGeneralName(cbRecord.Items[cbRecord.SelectedIndex].ToString())].post_ost[i]);
                }

                if ((cbRecord.SelectedIndex == 0) || (cbRecord.SelectedIndex == cbRecord.Items.Count-1))
                {
                    started = false;
                }

                //Если конечная - говорим, что она конечная (с условием направления, конечно)
                if (cbPath.SelectedIndex == 0)
                {
                    if (cbRecord.SelectedIndex == cbRecord.Items.Count - 1)
                    {
                        for (int j = 0; j < rc.GetEndCount(cbRoute.SelectedIndex); j++)
                        {
                            Speach(rc.GetEndPath(cbRoute.SelectedIndex, j));
                        }
                    }
                }
                else
                {
                    if (cbRecord.SelectedIndex == 0)
                    {
                        for (int j = 0; j < rc.GetEndCount(cbRoute.SelectedIndex); j++)
                        {
                            Speach(rc.GetEndPath(cbRoute.SelectedIndex, j));
                        }
                    }
                }

                //Если в path.txt на этой позиции указано yes - то воспроизводим сообщение после названия остановки
                if (rc.routes[cbRoute.SelectedIndex].general[cbRecord.SelectedIndex].random_play == true)
                {
                    if (rc.GetPostOstCount(cbRoute.SelectedIndex) > 0)
                    {
                        Random rnd = new Random();
                        int i = rnd.Next(0, rc.GetPostOstCount(cbRoute.SelectedIndex));
                        Speach(rc.GetPostOstPath(cbRoute.SelectedIndex, i));
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
                for (int j = 0; j < rc.GetObCount(cbRoute.SelectedIndex); j++)
                {
                    Speach(rc.GetObPath(cbRoute.SelectedIndex, j));
                }

                //Если в path.txt в данной записи стоит yes - воспроизводим случайную запись
                if ((cbPath.SelectedIndex == 0) && (cbRecord.SelectedIndex > 0))
                {
                    //Воспроизводим перед выездом с остановки
                    for (int i = 0; i < rc.routes[cbRoute.SelectedIndex].general[rc.routes[cbRoute.SelectedIndex].GetIndexFromGeneralName(cbRecord.Items[cbRecord.SelectedIndex-1].ToString())].prev_next.Count; i++)
                    {
                        Speach(rc.routes[cbRoute.SelectedIndex].general[rc.routes[cbRoute.SelectedIndex].GetIndexFromGeneralName(cbRecord.Items[cbRecord.SelectedIndex - 1].ToString())].prev_next[i]);
                    }
                }
                else if ((cbPath.SelectedIndex == 1) && (cbRecord.SelectedIndex < cbRecord.Items.Count - 1))
                {
                    //Воспроизводим перед выездом с остановки
                    for (int i = 0; i < rc.routes[cbRoute.SelectedIndex].general[rc.routes[cbRoute.SelectedIndex].GetIndexFromGeneralName(cbRecord.Items[cbRecord.SelectedIndex+1].ToString())].prev_next.Count; i++)
                    {
                        Speach(rc.routes[cbRoute.SelectedIndex].general[rc.routes[cbRoute.SelectedIndex].GetIndexFromGeneralName(cbRecord.Items[cbRecord.SelectedIndex + 1].ToString())].prev_next[i]);
                    }
                }


                //Воспроизводим сообщение об отправке (следующая остановка такая-то)
                for (int i = 0; i < rc.routes[cbRoute.SelectedIndex].general[rc.routes[cbRoute.SelectedIndex].GetIndexFromGeneralName(cbRecord.Items[cbRecord.SelectedIndex].ToString())].next_record.Count; i++)
                {
                    Speach(rc.routes[cbRoute.SelectedIndex].general[rc.routes[cbRoute.SelectedIndex].GetIndexFromGeneralName(cbRecord.Items[cbRecord.SelectedIndex].ToString())].next_record[i]);
                }

                

                //Если следующая остановка конечная - говорим об этом
                if (cbPath.SelectedIndex == 0)
                {
                    if (cbRecord.SelectedIndex == cbRecord.Items.Count - 1)
                    {
                        for (int j = 0; j < rc.GetEndCount(cbRoute.SelectedIndex); j++)
                        {
                            Speach(rc.GetEndPath(cbRoute.SelectedIndex, j));
                        }
                    }
                }
                else
                {
                    if (cbRecord.SelectedIndex == 0)
                    {
                        for (int j = 0; j < rc.GetEndCount(cbRoute.SelectedIndex); j++)
                        {
                            Speach(rc.GetEndPath(cbRoute.SelectedIndex, j));
                        }
                    }
                }

                //Воспроизводим после объявления конечной
                for (int j = 0; j < rc.GetObCount(cbRoute.SelectedIndex); j++)
                {
                    Speach(rc.GetPostPath(cbRoute.SelectedIndex, j));
                }

                //Если в path.txt в данной записи стоит yes - воспроизводим случайную запись
                if ((cbPath.SelectedIndex == 0) && (cbRecord.SelectedIndex > 0))
                {
                    if (rc.routes[cbRoute.SelectedIndex].general[cbRecord.SelectedIndex-1].random_play == true)
                    {
                        if (rc.GetRandomYesCount(cbRoute.SelectedIndex) > 0)
                        {
                            Random rnd = new Random();
                            int i = rnd.Next(0, rc.GetRandomYesCount(cbRoute.SelectedIndex));
                            Speach(rc.GetRandomYesPath(cbRoute.SelectedIndex, i));
                        }
                    }

                    //Воспроизводим после выезда с остановки
                    for (int i = 0; i < rc.routes[cbRoute.SelectedIndex].general[rc.routes[cbRoute.SelectedIndex].GetIndexFromGeneralName(cbRecord.Items[cbRecord.SelectedIndex - 1].ToString())].post_next.Count; i++)
                    {
                        Speach(rc.routes[cbRoute.SelectedIndex].general[rc.routes[cbRoute.SelectedIndex].GetIndexFromGeneralName(cbRecord.Items[cbRecord.SelectedIndex - 1].ToString())].post_next[i]);
                    }
                }
                else if ((cbPath.SelectedIndex == 1) && (cbRecord.SelectedIndex < cbRecord.Items.Count - 1))
                {
                    if (rc.routes[cbRoute.SelectedIndex].general[cbRecord.SelectedIndex+1].random_play == true)
                    {
                        if (rc.GetRandomYesCount(cbRoute.SelectedIndex) > 0)
                        {
                            Random rnd = new Random();
                            int i = rnd.Next(0, rc.GetRandomYesCount(cbRoute.SelectedIndex+1));
                            Speach(rc.GetRandomYesPath(cbRoute.SelectedIndex, i));
                        }
                    }

                    //Воспроизводим после выезда с остановки
                    for (int i = 0; i < rc.routes[cbRoute.SelectedIndex].general[rc.routes[cbRoute.SelectedIndex].GetIndexFromGeneralName(cbRecord.Items[cbRecord.SelectedIndex + 1].ToString())].post_next.Count; i++)
                    {
                        Speach(rc.routes[cbRoute.SelectedIndex].general[rc.routes[cbRoute.SelectedIndex].GetIndexFromGeneralName(cbRecord.Items[cbRecord.SelectedIndex + 1].ToString())].post_next[i]);
                    }
                }
                

                
                //Переключаем режим на "приехали на остановку" и ждем следующего нажатия кнопки
                complete = false;
                next = false;
            }
            button2.Visible = true;
            button3.Visible = true;
            button4.Visible = true;
        }

      
        /*Кнопка воспроизвдения приветствия*/
        private void button4_Click(object sender, EventArgs e)
        {
            //Чистим replay список
            replay.Clear();

            //Воспроизводим обязательный ты-дын
            for (int j = 0; j < rc.GetAllCount(cbRoute.SelectedIndex); j++)
            {
                Speach(rc.GetAllPath(cbRoute.SelectedIndex, j));
            }

            //Воспроизводим случайное приветствие
            if (rc.GetGreetingCount(cbRoute.SelectedIndex) > 0)
            {
                Random rnd = new Random();
                int i = rnd.Next(0, rc.GetGreetingCount(cbRoute.SelectedIndex));
                Speach(rc.GetGreetingPath(cbRoute.SelectedIndex, i));
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
                UpldateTrackList();
                next = true;
                complete = false;
                started = false;
            }
            else
            {
                button3.Text = "<<";
                UpldateTrackList();
                cbRecord.SelectedIndex = cbRecord.Items.Count-1;
                next = true;
                complete = false;
                started = false;
            }
        }

        /*Делаем кнопки одинакового размера на любом экране*/
        private void Form1_Load(object sender, EventArgs e)
        {
            button2.Width = this.Width / 2;
            button3.Width = this.Width / 2;
            panel5.Width = this.Width / 2;
            panel3.Width = this.Width / 2;
        }

        private void UpldateTrackList()
        {
            cbRecord.Items.Clear();

            if ((cbRoute.SelectedIndex != -1) && (cbRoute.SelectedIndex < cbRoute.Items.Count))
            {
                for (int j = 0; j < rc.routes[cbRoute.SelectedIndex].general.Count; j++)
                {
                    if (cbPath.SelectedIndex == 0)
                    {
                        if ((rc.routes[cbRoute.SelectedIndex].general[j].route == "front")
                            || (rc.routes[cbRoute.SelectedIndex].general[j].route == "all"))
                        {
                            cbRecord.Items.Add(rc.routes[cbRoute.SelectedIndex].general[j].name);
                        }
                    }
                    else
                    {
                        if ((rc.routes[cbRoute.SelectedIndex].general[j].route == "back")
                            || (rc.routes[cbRoute.SelectedIndex].general[j].route == "all"))
                        {
                            cbRecord.Items.Add(rc.routes[cbRoute.SelectedIndex].general[j].name);
                        }
                    }
                }
            }
            cbRecord.SelectedIndex = 0;
        }

        /*Подгружаем остановки в выпадающий список на основе маршрута*/
        private void RouteInit()
        {
            UpldateTrackList();

            //Очищаем параметры
            cbPath.SelectedIndex = 0;
            
            next = true;
            complete = false;
            started = false;

            replay.Clear();
            replay.Add(rc.routes[cbRoute.SelectedIndex].general[cbRecord.SelectedIndex].ost_record[0]);

        }

        /*Функция выбра маршрута*/
        private void cbRoute_SelectedIndexChanged(object sender, EventArgs e)
        {
            RouteInit();
        }
    }
}
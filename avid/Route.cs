using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace avid
{
    class Route
    {
        //Путь до папки exe файла
        String appDir = "";

        //Имя Маршрута
        String RouteName = "";

        //Списки параметров из конфигурационных файлов
        public List<string> names;       //Имена остановок для выпадающего списка
        public List<string> paths;       //Остановки
        public List<string> events;      //События перед остановками (Типа "Следующая остановка - такая-то")
        public List<string> mn;          //Воспроизводить ли random_yes на данной остановке, бывает yes или no
        public List<string> ob;          //Обязательное воспроизвдение перед events (Например "осторожно, двери закрываются", после выполняется events "Следующая остановка такая-то")
        public List<string> post;        //Обязательное вопроизведение после events
        public List<string> random_yes;  //Воспроизведение случайной записи из списка, при условии что на данной остановке стоит yes в paths
        public List<string> greeting;    //Приветствие - воспроизводит случайное приветствие из списка при нажатии на кнопку
        public List<string> all;         //Обязательно воспроизводятся все записи перед path, ob или events (короче, всегда в начале, типа звук "ты-дын")
        public List<string> end;         //Объявляет, что остановка конечная (воспроизводятся все записи в списке)
        public List<string> post_ost;    //Воспроизводит случайную запись после paths, если в звписи есть yes

        /*Конструктор класса Маршрута*/
        public Route(String name, string config_prefix)
        {
            SetRoute(name, config_prefix);
        }

        /*ПОлучение имени маршрута*/
        public String GetName()
        {
            return RouteName;
        }

        /*Настройка мршрута по конфигурационным файлам*/
        public void SetRoute(String name, string config_prefix)
        {
            RouteName = name;

            //Присваеиваем appDir путь до папки с программой
            appDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase);

            //Инициализируем списки
            names = new List<string>();
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
            

            //Очищаем списки и загружаем данные из файлов
            ob.Clear();
            var path = Path.Combine(appDir, @"" + config_prefix + "_ob.txt");
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
            path = Path.Combine(appDir, @"" + config_prefix + "_random_yes.txt");
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
            path = Path.Combine(appDir, @"" + config_prefix + "_post.txt");
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
            path = Path.Combine(appDir, @"" + config_prefix + "_greeting.txt");
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
            path = Path.Combine(appDir, @"" + config_prefix + "_all.txt");
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
            path = Path.Combine(appDir, @"" + config_prefix + "_end.txt");
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
            path = Path.Combine(appDir, @"" + config_prefix + "_post_ost.txt");
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
            names.Clear();
            //Считываем записи с файла path.txt - запихивая в paths и events пути до wav файлов, 
            //в mn - yes или no записи, и текст в выпадающий список выбора остановки
            path = Path.Combine(appDir, @"" + config_prefix + "_path.txt");
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
                                    names.Add(res);
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
            }
        }
    }
}

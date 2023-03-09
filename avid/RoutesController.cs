using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.IO;
using System.Windows.Forms;

namespace avid
{
    class RoutesController
    {
        //Путь до папки exe файла
        String appDir = "";

        //Список маршрутов
        List<Route> routes;

        /*Конструктор класса Контроллера маршрутов*/
        public RoutesController()
        {
            //Присваеиваем appDir путь до папки с программой
            appDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase);

            //Инициализируем список маршрутов
            routes = new List<Route>();

            //Чистим список маршрутов и считываем данные из файла конфигурации маршрутов (чтобы заполнить список)
            routes.Clear();
            var path = Path.Combine(appDir, @"routes.txt");
            FileInfo fi = new FileInfo(path);
            if (fi.Exists)
            {
                using (StreamReader sr = fi.OpenText())
                {
                    string s = "";
                    string s1 = "";
                    string nm = "";
                    string pr = "";
                    while ((s = sr.ReadLine()) != null)
                    {
                        for (int i = 0; i < s.Length; i++)
                        {
                            char c = s[i];

                            if (c == ';')
                            {
                                nm = s1;
                                s1 = "";
                            }
                            else
                            {
                                s1 = s1 + c;
                            }
                        }
                        pr = s1;
                        routes.Add(new Route(nm,pr));
                        s1 = "";
                    }
                }
            }
        }

        /*Получение размера спсика маршрутов*/
        public int GetCount()
        {
            return routes.Count;
        }

        /*Добавление маршрута в список*/
        public void AddRoute(String name, string config_prefix)
        {
            //name - заголовок маршрута
            //config_prefix - имя первой чатси конфигурационных файлов (например 10, а конфиги будут читаться
            //как 10_paths.txt)
            routes.Add(new Route(name, config_prefix));
        }

        /*Получаем имя маршрута*/
        public String GetName(int index)
        {
            if ((index != -1) && (index < routes.Count))
            {
                return routes[index].GetName();
            }

            return "";
        }

        /*Получаем имя остановки из path.txt*/
        public String GetPathsName(int route_index, int index)
        {
            if ((route_index != -1) && (route_index < routes.Count))
            {
                if ((index != -1) && (index < routes[route_index].names.Count))
                {
                    return routes[route_index].names[index];
                }
                else
                {
                    return "";
                }
            }

            return "";
        }

        /*Получаем размер списка записей*/
        public int GetPathsNamesCount(int index)
        {
            if ((index != -1) && (index < routes.Count))
            {
                return routes[index].names.Count;
            }
            return 0;
        }

        /*Получаем путь до файла из path.txt*/
        public string GetPathsPath(int route_index, int index)
        {
            if ((route_index != -1) && (route_index < routes.Count))
            {
                if ((index != -1) && (index < routes[route_index].paths.Count))
                {
                    return routes[route_index].paths[index];
                }
                else
                {
                    return "";
                }
            }
            return "";
        }

        /*Получаем размер списка записей*/
        public int GetPathsCount(int index)
        {
            if ((index != -1) && (index < routes.Count))
            {
                return routes[index].paths.Count;
            }
            return 0;
        }

        /*Получаем второй путь до файла из path.txt*/
        public string GetEventsPath(int route_index, int index)
        {
            if ((route_index != -1) && (route_index < routes.Count))
            {
                if ((index != -1) && (index < routes[route_index].events.Count))
                {
                    return routes[route_index].events[index];
                }
                else
                {
                    return "";
                }
            }
            return "";
        }

        /*Получаем размер списка записей*/
        public int GetEventsCount(int index)
        {
            if ((index != -1) && (index < routes.Count))
            {
                return routes[index].events.Count;
            }
            return 0;
        }

        /*Получаем информацию об людных местах из path.txt*/
        public string GetMnPath(int route_index, int index)
        {
            if ((route_index != -1) && (route_index < routes.Count))
            {
                if ((index != -1) && (index < routes[route_index].mn.Count))
                {
                    return routes[route_index].mn[index];
                }
                else
                {
                    return "";
                }
            }
            return "";
        }

        /*Получаем размер списка записей*/
        public int GetMnCount(int index)
        {
            if ((index != -1) && (index < routes.Count))
            {
                return routes[index].mn.Count;
            }
            return 0;
        }

        /*Получаем путь до файла из ob.txt*/
        public string GetObPath(int route_index, int index)
        {
            if ((route_index != -1) && (route_index < routes.Count))
            {
                if ((index != -1) && (index < routes[route_index].ob.Count))
                {
                    return routes[route_index].ob[index];
                }
                else
                {
                    return "";
                }
            }
            return "";
        }

        /*Получаем размер списка записей*/
        public int GetObCount(int index)
        {
            if ((index != -1) && (index < routes.Count))
            {
                return routes[index].ob.Count;
            }
            return 0;
        }

        /*Получаем путь до файла из post.txt*/
        public string GetPostPath(int route_index, int index)
        {
            if ((route_index != -1) && (route_index < routes.Count))
            {
                if ((index != -1) && (index < routes[route_index].post.Count))
                {
                    return routes[route_index].post[index];
                }
                else
                {
                    return "";
                }
            }
            return "";
        }

        /*Получаем размер списка записей*/
        public int GetPostCount(int index)
        {
            if ((index != -1) && (index < routes.Count))
            {
                return routes[index].post.Count;
            }
            return 0;
        }

        /*Получаем путь до файла из random_yes.txt*/
        public string GetRandomYesPath(int route_index, int index)
        {
            if ((route_index != -1) && (route_index < routes.Count))
            {
                if ((index != -1) && (index < routes[route_index].random_yes.Count))
                {
                    return routes[route_index].random_yes[index];
                }
                else
                {
                    return "";
                }
            }
            return "";
        }

        /*Получаем размер списка записей*/
        public int GetRandomYesCount(int index)
        {
            if ((index != -1) && (index < routes.Count))
            {
                return routes[index].random_yes.Count;
            }
            return 0;
        }

        /*Получаем путь до файла из greeting.txt*/
        public string GetGreetingPath(int route_index, int index)
        {
            if ((route_index != -1) && (route_index < routes.Count))
            {
                if ((index != -1) && (index < routes[route_index].greeting.Count))
                {
                    return routes[route_index].greeting[index];
                }
                else
                {
                    return "";
                }
            }
            return "";
        }

        /*Получаем размер списка записей*/
        public int GetGreetingCount(int index)
        {
            if ((index != -1) && (index < routes.Count))
            {
                return routes[index].greeting.Count;
            }
            return 0;
        }

        /*Получаем путь до файла из all.txt*/
        public string GetAllPath(int route_index, int index)
        {
            if ((route_index != -1) && (route_index < routes.Count))
            {
                if ((index != -1) && (index < routes[route_index].all.Count))
                {
                    return routes[route_index].all[index];
                }
                else
                {
                    return "";
                }
            }
            return "";
        }

        /*Получаем размер списка записей*/
        public int GetAllCount(int index)
        {
            if ((index != -1) && (index < routes.Count))
            {
                return routes[index].all.Count;
            }
            return 0;
        }

        /*Получаем путь до файла из end.txt*/
        public string GetEndPath(int route_index, int index)
        {
            if ((route_index != -1) && (route_index < routes.Count))
            {
                if ((index != -1) && (index < routes[route_index].end.Count))
                {
                    return routes[route_index].end[index];
                }
                else
                {
                    return "";
                }
            }
            return "";
        }

        /*Получаем размер списка записей*/
        public int GetEndCount(int index)
        {
            if ((index != -1) && (index < routes.Count))
            {
                return routes[index].end.Count;
            }
            return 0;
        }

        /*Получаем путь до файла из ost.txt*/
        public string GetPostOstPath(int route_index, int index)
        {
            if ((route_index != -1) && (route_index < routes.Count))
            {
                if ((index != -1) && (index < routes[route_index].post_ost.Count))
                {
                    return routes[route_index].post_ost[index];
                }
                else
                {
                    return "";
                }
            }
            return "";
        }

        /*Получаем размер списка записей*/
        public int GetPostOstCount(int index)
        {
            if ((index != -1) && (index < routes.Count))
            {
                return routes[index].post_ost.Count;
            }
            return 0;
        }
    }
}

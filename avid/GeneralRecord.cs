/* @author:  nekitgam (Дубровский Никита Николаевич)
 * @date:    6.03.2023
 * @license: GNU GPL v3
 */

using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace avid
{
    class GeneralRecord
    {
        public string name;

        //Воспроизвести на остановке
        public List<string> ost_record;

        //Воспроизвести на остановке перед основной записью и после нее
        public List<string> post_ost;
        public List<string> prev_ost;

        //Воспроизвести при движении на остановку
        public List<string> next_record;

        //Воспроизвести при движении на остановку перед основной записью и после нее
        public List<string> post_next;
        public List<string> prev_next;

        //Маршрут в обе стороны
        public string route = "all";

        //Воспроизводить рандомные записи?
        public bool random_play = false;

        /*Конструктор класса*/
        public GeneralRecord(string line)
        {
            ost_record = new List<string>();

            post_ost = new List<string>();
            prev_ost = new List<string>();

            next_record = new List<string>();

            post_next = new List<string>();
            prev_next = new List<string>();

            route = "all";

            random_play = false;

            Parse(line);
        }

        /*Получаем данные о свойствах из строки*/
        public string GetMode(string text)
        {
            if ((text.Length > 5) && (text.Substring(0,5)=="name="))
            {
                return "name";
            }
            else if ((text.Length > 4) && (text.Substring(0, 4) == "ost="))
            {
                return "ost";
            }
            else if ((text.Length > 5) && (text.Substring(0, 5) == "next="))
            {
                return "next";
            }
            else if ((text.Length > 7) && (text.Substring(0, 7) == "random="))
            {
                return "random";
            }
            else if ((text.Length > 6) && (text.Substring(0, 6) == "route="))
            {
                return "route";
            }
            else if ((text.Length > 10) && (text.Substring(0, 10) == "post_next="))
            {
                return "post_next";
            }
            else if ((text.Length > 10) && (text.Substring(0, 10) == "prev_next="))
            {
                return "prev_next";
            }
            else if ((text.Length > 9) && (text.Substring(0, 9) == "post_ost="))
            {
                return "post_ost";
            }
            else if ((text.Length > 9) && (text.Substring(0, 9) == "prev_ost="))
            {
                return "prev_ost";
            }
            return "";
        }

        /*Получаем значение из строки*/
        public string GetValue(string text)
        {
            string s = "";
            for (int i = 0; i < text.Length; i++)
            {
                if (text[i] == '=')
                {
                    s = "";
                }
                else
                {
                    s = s + text[i];
                }
            }
            return s;
        }

        /*Считываем строку и генерируем списки*/
        public void Parse(string line)
        {
            string mode = "";
            string s1 = "";
            string value = "";
            for (int i = 0; i < line.Length; i++)
            {
                char c = line[i];
                if (c == ';')
                {
                    if (GetMode(s1) != "")
                    {
                        mode = GetMode(s1);
                    }
                    value = GetValue(s1);

                    if (mode == "name")
                    {
                        name = value;
                    }
                    else if (mode == "ost")
                    {
                        ost_record.Add(value);
                    }
                    else if (mode == "next")
                    {
                        next_record.Add(value);
                    }
                    else if (mode == "random")
                    {
                        if (value == "yes")
                        {
                            random_play = true;
                        }
                        else
                        {
                            random_play = false;
                        }
                    }
                    else if (mode == "route")
                    {
                        route = value;
                        if (route == "") route = "all";
                    }
                    else if (mode == "post_next")
                    {
                        post_next.Add(value);
                    }
                    else if (mode == "prev_next")
                    {
                        prev_next.Add(value);
                    }
                    else if (mode == "post_ost")
                    {
                        post_ost.Add(value);
                    }
                    else if (mode == "prev_ost")
                    {
                        prev_ost.Add(value);
                    }

                    s1 = "";
                }
                else if (c == '|')
                {
                    if (GetMode(s1) != "")
                    {
                        mode = GetMode(s1);
                    }
                    value = GetValue(s1);

                    if (mode == "name")
                    {
                        name = value;
                    }
                    else if (mode == "ost")
                    {
                        ost_record.Add(value);
                    }
                    else if (mode == "next")
                    {
                        next_record.Add(value);
                    }
                    else if (mode == "random")
                    {
                        if (value == "yes")
                        {
                            random_play = true;
                        }
                        else
                        {
                            random_play = false;
                        }
                    }
                    else if (mode == "route")
                    {
                        route = value;
                        if (route == "") route = "all";
                    }
                    else if (mode == "post_next")
                    {
                        post_next.Add(value);
                    }
                    else if (mode == "prev_next")
                    {
                        prev_next.Add(value);
                    }
                    else if (mode == "post_ost")
                    {
                        post_ost.Add(value);
                    }
                    else if (mode == "prev_ost")
                    {
                        prev_ost.Add(value);
                    }

                    s1 = "";
                }
                else
                {
                    s1 = s1 + c;
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace MuseumTxtToX
{
    public class TextElements
    {
        
        public List<TextElement> textElements;
        public TextElements()
        {
            textElements = new List<TextElement>();
        }
        public TextElements(string fileName)
        {
            textElements = new List<TextElement>();
            int len = 0;
            string[] lens = new string[3];
            StreamReader stream = new StreamReader(fileName);
            while (!stream.EndOfStream)
            {
                
                string s = stream.ReadLine();
                if(s != "" && s != "\n" && s != " ")
                {
                    string sm = String.Join(' ', s.Split(' ')[1..]);
                    switch (len)
                    {
                        case 0:
                            lens[0] = sm;
                            break;
                        case 1:
                            lens[1] = sm;
                            break;
                        case 2:
                            lens[2] = sm;
                            break;

                    }
                    len++;
                }
                else
                {
                    lens[0] = lens[0].Replace("&", "&amp;");
                    lens[1] = lens[1].Replace("&", "&amp;");
                    lens[2] = lens[2].Replace("&", "&amp;");
                    textElements.Add(new TextElement(lens[0],lens[1],lens[2]));
                    len = 0;
                }
                
            }
            lens[0] = "-";
            lens[1] = "-";
            lens[2] = "-";
            textElements.Add(new TextElement(lens[0], lens[1], lens[2]));
            stream.Close();
        }
        public void Print()
        {
            foreach(TextElement el in textElements)
            {
                el.Print();
            }
        }
        public void CreateTextFile()
        {
            StreamWriter sr = new StreamWriter(@"C:\Users\Георгій\Desktop\Музеї\some.txt",false,System.Text.Encoding.UTF8);
            foreach(TextElement el in textElements)
            {
                sr.WriteLine("англ.: " + el.English);
                sr.WriteLine("фр.: " + el.Franche);
                sr.WriteLine("укр.: " + el.Ukraine);
                sr.WriteLine(" ");
            }
            sr.Close();
        }
    }
}

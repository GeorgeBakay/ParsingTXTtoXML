using System;
using System.Collections.Generic;
using System.Text;

namespace MuseumTxtToX
{
    public class TextElement
    {
        public string English;
        public string Franche;
        public string Ukraine;
        public TextElement(string _English,string _Franche,string _Ukraine)
        {
            English = _English;
            Franche = _Franche;
            Ukraine = _Ukraine;
        }
        public void Print()
        {
            Console.WriteLine(English + " " + Franche + " " + Ukraine);
        }
    }
}

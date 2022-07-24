using System;
using System.Xml;
using System.Collections.Generic;
namespace MuseumTxtToX
{
    class Program
    {
        
       
        static void Main(string[] args)
        {
            

            XmlDocument xml = new XmlDocument();
            xml.Load(@"C:\Users\Георгій\Desktop\Музеї\spectrum.xml");
        

            TextElements textel = new TextElements(@"C:\Users\Георгій\Desktop\Музеї\translate.txt");
            textel.Print();
            TextElements translates = new TextElements();
            
            XmlNodeList elList = xml.GetElementsByTagName("labels");
            foreach(XmlNode node in elList)//перебіл всіх labels
            {
                XmlElement labelEl = xml.CreateElement("label");
                XmlAttribute attrEl = xml.CreateAttribute("locale");
                
                Console.WriteLine(node.Name);
                foreach(XmlNode node2 in node.ChildNodes)//перебір елементів в labels
                {
                    XmlNode attr = node2.Attributes.GetNamedItem("locale");
                    
                    if(attr != null) Console.WriteLine(" " + attr.Value);
                    if(attr.Value == "en_US")//елементи label на англійській мові
                    {
                        Console.WriteLine("  " + node2.Name);
                        Console.WriteLine("  " + node2.InnerText);
                        if (node2.Attributes["preferred"] != null)
                        {
                            XmlAttribute attrPreferred = xml.CreateAttribute("preferred");
                            XmlText textAttrPref = xml.CreateTextNode(node2.Attributes["preferred"].Value);
                            attrPreferred.AppendChild(textAttrPref);
                            labelEl.Attributes.Append(attrPreferred);
                        }
                        //створеня нового лейблу на укр перекладу
                        //створення атрибуту мови
                        //створення списку внутрішніх елементів

                        List<XmlElement> elements = new List<XmlElement>();


                        foreach(XmlNode nodeEn in node2.ChildNodes)//перебір перекладів на англійській
                        {
                            bool test = false;
                            foreach (TextElement textEl in textel.textElements)// перебір перекладів з тексту
                            {        
                                if(nodeEn.InnerText == textEl.English)
                                {
                                    Console.WriteLine(nodeEn.InnerText + "=>" + textEl.Ukraine);
                                    XmlElement item = xml.CreateElement(nodeEn.Name);
                                    XmlText trans = xml.CreateTextNode(textEl.Ukraine);
                                    item.AppendChild(trans);
                                    elements.Add(item);
                                    test = true;
                                    translates.textElements.Add(new TextElement(nodeEn.InnerText, "Franche", textEl.Ukraine));
                                    break;
                                }
                            }
                            if(test == false)
                            {
                                Console.WriteLine(nodeEn.InnerText + "Not have traslate ");
                                XmlElement item = xml.CreateElement(nodeEn.Name);
                                XmlText trans = xml.CreateTextNode(nodeEn.InnerText + "(eng)");
                                item.AppendChild(trans);
                                elements.Add(item);
                                translates.textElements.Add(new TextElement(nodeEn.InnerText, "Franche","need translate"));
                            }
                        }
                        
                        XmlText attrText = xml.CreateTextNode("uk_UA");
                        attrEl.AppendChild(attrText);
                        labelEl.Attributes.Append(attrEl);
                        foreach(XmlElement el in elements)
                        {
                            labelEl.AppendChild(el);
                        }

                    }
                    
                }
                node.AppendChild(labelEl);
            }
            xml.Save(@"C:\Users\Георгій\Desktop\Музеї\Ok\some.xml");
            translates.CreateTextFile();
            

        }
    }
}

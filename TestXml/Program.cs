using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq; //XDocument 클래스
using TestXml;

namespace Section02
{
    class Program
    {
        static void Main(string[] args)
        {
           SampleCode sampleCode=new SampleCode();
            sampleCode.Projection();
        }
    }

    class SampleCode
    {
        public static string path = "C:\\Users\\user1004\\source\\repos\\TestXml\\TestXml\\bin\\Debug\\net6.0\\novelist.xml";

        //특정 요소를 구하고 싶을 때
        public void GetAllElements1()
        {
            var xdoc = XDocument.Load(path);//from using System.Xml.Linq, XDocument 클래스
            var xelements = xdoc.Root.Elements(); //IEnumerable<XElement> 반환형식으로 Root 아래에 있는걸 다 가져옴
            foreach(var xnovelist in xelements)
            {
                XElement xname = xnovelist.Element("name");
                Console.WriteLine(xname.Value);
            }
        }
        //특정 요소를 형변환 해서 구할 때
        public void GetAllElements2()
        {
            var xdoc = XDocument.Load(path);
            foreach (var xnovelist in xdoc.Root.Elements())
            {
                var xname= xnovelist.Element("name");// 이렇게 꺼내서는 string 형만 가능함
                var birth = (DateTime)xnovelist.Element("birth");
                Console.WriteLine("{0} {1}", xname.Value, birth.ToShortDateString());
            }
        }
        //조건을 지정해서 XML요소를 구할 때
        public void ExtractElements()
        {
            var xdoc = XDocument.Load(path);
            var xnovelists = xdoc.Root.Elements().Where(x => ((DateTime)x.Element("birth")).Year >= 1900);
            foreach (var xnovelist in xnovelists)
            {
                var xname = xnovelist.Element("name");// 이렇게 꺼내서는 string 형만 가능함
                var birth = (DateTime)xnovelist.Element("birth");
                Console.WriteLine("{0} {1}", xname.Value, birth.ToShortDateString());
            }
        }
        //XML요소를 정렬하고 싶을 때
        public void SortElements()
        {
            var xdoc = XDocument.Load(path);
            var xnovelists = xdoc.Root.Elements().OrderBy(x => (string)(x.Element("name").Attribute("eng")));
            foreach(var xnovelist in xnovelists)
            {
                var xname = xnovelist.Element("name");
                var birth = (DateTime)xnovelist.Element("birth");
                Console.WriteLine("{0} {1}", xname.Value, birth.ToShortDateString());
            }
        }
        //중첩되어 있는 자식요소를 구할 때
        public void GetNestingElements()
        {
            var xdoc = XDocument.Load(path);
            foreach(var xnovelist in xdoc.Root.Elements())
            {
                var xname = xnovelist.Element("name");
                var works = xnovelist.Element("masterpieces").Elements("title").Select(x => x.Value);//변수형 IEnumerable<string>
                Console.WriteLine("{0} - {1}", xname.Value, string.Join(", ", works));
            }
        }
        //익명클래스의 객체 형태로 Element를 받고 싶을 때
        public void Projection()
        {
            var xdoc=XDocument.Load(path);
            var novelists = xdoc.Root.Elements().Select(x => new {
                Name = (string)x.Element("name"), 
                Birth = (DateTime)x.Element("birth"), 
                Death = (DateTime)x.Element("death") }); //익명클래스로 가져옴
            foreach(var novelist in novelists)
            {
                Console.WriteLine("{0} ({1}-{2})", novelist.Name, novelist.Birth, novelist.Death.Year);
            }
        }
        
    }
}
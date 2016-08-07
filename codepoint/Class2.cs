using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace codepoint.Model
{
    [XmlRoot("font")]
    public class font
    {
        [XmlElement("chars")]
        public chars chars { get; set; }

        [XmlElement("common")]
        public common common { get; set; }
    }
    public class common
    {
        [XmlAttribute("scaleW")]
        public int scaleW { get; set; }

        [XmlAttribute("scaleH")]
        public int scaleH { get; set; }
    }
    public class chars
    {
        [XmlAttribute("count")]
        public int count { get; set; }

        [XmlElement("char")]
        public List<chr> chrs { get; set; }
    }
    public class chr
    {
        [XmlAttribute("id")]
        public int id { get; set; }

        [XmlAttribute("x")]
        public int x { get; set; }

        [XmlAttribute("y")]
        public int y { get; set; }

        [XmlAttribute("width")]
        public int width { get; set; }

        [XmlAttribute("height")]
        public int height { get; set; }

        [XmlAttribute("xoffset")]
        public int xoffset { get; set; }

        [XmlAttribute("yoffset")]
        public int yoffset { get; set; }

        [XmlAttribute("xadvance")]
        public int xadvance { get; set; }

        [XmlAttribute("page")]
        public int page { get; set; }

        [XmlAttribute("chnl")]
        public int chnl { get; set; }
    }
}

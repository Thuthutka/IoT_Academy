using System;

namespace IoT_Academy
{
    class Section
    {
        public Element StartElement { get; set; }
        public Element EndElement { get; set; }
        public TimeSpan Time { get; set; }

        public Section(Element startElement, Element endElement, TimeSpan time)
        {
            StartElement = startElement;
            EndElement = endElement;
            Time = time;
        }
    }
}

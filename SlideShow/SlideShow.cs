using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlideShow
{
    [Serializable]
    public class MySlideShow
    {
        public Head Head { get; set; }
        public List<Slide> Body { get; set; }
    }

    [Serializable]
    public class Head 
    {
        public string Title { get; set; }
    }

    [Serializable]
    public class Slide 
    {
        public string Type { get; set; }
        public string FileInfo { get; set; }
        public int Time { get; set; }
    }
}

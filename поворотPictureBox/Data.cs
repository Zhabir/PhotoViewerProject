using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace поворотPictureBox
{
    [Serializable]
    public class Data
    {
        public int actualIndex { get; set; }
        public int TrackBarR { get; set; }
        public int TrackBarG { get; set; }
        public int TrackBarB { get; set; }
        public int Bright { get; set; }
        public int contrast { get; set; }
        public int rgbFlag { get; set; }
        public int angle { get; set; }
        public int bitmaps_count { get; set; }
        public int name_index { get; set; }
        public List<int> angles { get; set; }
    }
}

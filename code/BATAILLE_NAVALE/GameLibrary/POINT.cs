using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIBLIOTHEQUE_LOGIQUE_JEU
{
    internal class POINT
    {
        private int _X, _Y;
        private bool _TOUCHE;

        public POINT(int x, int y, bool touche)
        {
            _X = x;
            _Y = y;
            _TOUCHE = touche;
        }

        public POINT() : this(0, 0, false) { }

        public POINT(int x, int y) : this(x, y, false) { }

        public POINT(POINT P) : this(P.X, P.Y, P.TOUCHE) { }

        public int X
        {
            get { return _X; }
            set { _X = value; }
        }

        public int Y
        {
            get { return _Y; }
            set { _Y = value; }
        }

        public bool TOUCHE
        {
            get { return _TOUCHE; }
            set { _TOUCHE = value; }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIBLIOTHEQUE_LOGIQUE_JEU
{
    internal class PIECE_DE_JEU
    {
        private int _ID;
        private BATEAU _BATEAU;
        private List<POINT> _VECTEUR;

        public PIECE_DE_JEU()
        {
            this._ID = 0;
            this._BATEAU = new BATEAU();
            this._VECTEUR = new List<POINT>();
        }

        public PIECE_DE_JEU(int id, BATEAU bateau, List<POINT> vecteur)
        {
            this._ID = id;
            this._BATEAU = bateau;
            this._VECTEUR = vecteur;
        }

        public int ID
        {
            get { return this._ID; }
            set { this._ID = value; }
        }

        public BATEAU BATEAU
        {
            get { return this._BATEAU; }
            set { this._BATEAU = value; }
        }

        public List<POINT> VECTEUR
        {
            get { return this._VECTEUR; }
            set { this._VECTEUR = value; }
        }
    }
}
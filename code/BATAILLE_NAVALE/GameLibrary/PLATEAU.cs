using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIBLIOTHEQUE_LOGIQUE_JEU
{
    internal class PLATEAU
    {
        private GRILLE _GRILLE_JOUEUR_A, _GRILLE_JOUEUR_B;
        private int[,] _GRILLE_ENEMI_DE_A, _GRILLE_ENEMI_DE_B;
        private bool _WIN;

        public PLATEAU(GRILLE grille_joueur_a, GRILLE grille_joueur_b, int[,] grille_enemi_de_a, int[,] grille_enemi_de_b, bool win)
        {
            _GRILLE_JOUEUR_A = grille_joueur_a;
            _GRILLE_JOUEUR_B = grille_joueur_b;
            _GRILLE_ENEMI_DE_A = grille_enemi_de_a;
            _GRILLE_ENEMI_DE_B = grille_enemi_de_b;
            _WIN = win;
        }

        public PLATEAU() : this(new GRILLE(), new GRILLE(), new int[0,0], new int[0,0], false) { }

        public PLATEAU(PLATEAU P) : this(P.GRILLE_JOUEUR_A, P.GRILLE_JOUEUR_B, P.GRILLE_ENEMI_DE_A, P.GRILLE_ENEMI_DE_B, P.WIN) { }

        public GRILLE GRILLE_JOUEUR_A
        {
            get { return _GRILLE_JOUEUR_A; }
            set { _GRILLE_JOUEUR_A = value; }
        }

        public GRILLE GRILLE_JOUEUR_B
        {
            get { return _GRILLE_JOUEUR_B; }
            set { _GRILLE_JOUEUR_B = value; }
        }

        public int[,] GRILLE_ENEMI_DE_A
        {
            get { return _GRILLE_ENEMI_DE_A; }
            set { _GRILLE_ENEMI_DE_A = value; }
        }

        public int[,] GRILLE_ENEMI_DE_B
        {
            get { return _GRILLE_ENEMI_DE_B; }
            set { _GRILLE_ENEMI_DE_B = value; }
        }

        public bool WIN
        {
            get { return _WIN; }
            set { _WIN = value; }
        }
    }
}

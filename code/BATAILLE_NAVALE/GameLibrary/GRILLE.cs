using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIBLIOTHEQUE_LOGIQUE_JEU
{
    internal class GRILLE
    {
        private int _HAUTEUR, _LARGEUR, _TAILLE;

        private bool[,] _DISPONIBILITES;
        private int[,] _POSITONS_IDS;
        private List<PIECE_DE_JEU> _PIECES_DE_JEU;

        public GRILLE(int hauteur, int largeur, int taille, bool[,] disponibilites, int[,] positions_ids, List<PIECE_DE_JEU> pieces_de_jeu)
        {
            _HAUTEUR= hauteur;
            _LARGEUR= largeur;
            _TAILLE= taille;
            _DISPONIBILITES= disponibilites;
            _POSITONS_IDS= positions_ids;
            _PIECES_DE_JEU= pieces_de_jeu;
        }

        public GRILLE() : this(0, 0, 0, new bool[0,0], new int[0,0], new List<PIECE_DE_JEU>(0)) { }
        public GRILLE(GRILLE G) : this(G.HAUTEUR, G.LARGEUR, G.TAILLE, G.DISPONIBILITES, G._POSITONS_IDS, G.PIECES_DE_JEU) { }

        public int HAUTEUR
        {
            get { return this._HAUTEUR; }
            set { this._HAUTEUR = value; }
        }

        public int LARGEUR
        {
            get { return this._LARGEUR; }
            set { this._LARGEUR = value; }
        }

        public int TAILLE
        {
            get { return this._TAILLE; }
            set { this._TAILLE = value; }
        }

        public bool[,] DISPONIBILITES
        {
            get { return this._DISPONIBILITES; }
            set { this._DISPONIBILITES = value; }
        }

        public int[,] POSITONS_IDS
        {
            get { return this._POSITONS_IDS; }
            set { this._POSITONS_IDS = value; }
        }

        public List<PIECE_DE_JEU> PIECES_DE_JEU
        {
            get { return this._PIECES_DE_JEU; }
            set { this._PIECES_DE_JEU = value; }
        }
    }
}

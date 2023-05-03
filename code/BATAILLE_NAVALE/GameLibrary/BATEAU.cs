using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIBLIOTHEQUE_LOGIQUE_JEU
{
    internal class BATEAU
    {
        private int _TAILLE;
        private string _NOM;

        public BATEAU()
        {
            _TAILLE = 0;
            _NOM = "EAU";
        }

        public BATEAU(int taille, string nom)
        {
            _TAILLE = taille;
            _NOM = nom;
        }
        
        public int TAILLE
        {
            get { return _TAILLE; }
            set { _TAILLE = value; }
        }

        public string NOM
        {
            get { return _NOM; }
            set { _NOM = value; }
        }
    }
}

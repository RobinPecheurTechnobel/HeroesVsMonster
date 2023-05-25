using HeroesVsMonster.Models.Heros;
using HeroesVsMonster.Models.Monstres;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace HeroesVsMonster.Models
{
    public class Plateau
    {
        public static int tickDeJeu =0;

		private Object?[,] _cases;

		public Object?[,] PlateauDeJeu
		{
			get { return _cases; }
			set { _cases = value; }
		}

        private bool EnnemiVisible { get; set; }

		private List<Personnage> Ennemis = new List<Personnage>();
        private Hero _Joueur;

		public Plateau(bool EnnemiAffiché=false) : this(15,15,EnnemiAffiché) {}
		public Plateau(int cotéDePlateauCarré, bool EnnemiAffiché = false) : this(cotéDePlateauCarré, cotéDePlateauCarré, EnnemiAffiché){}
		public Plateau(int largeurPlateau, int longueurPlateau, bool EnnemiAffiché = false)
		{
			PlateauDeJeu = new Object?[15, 15];
			for(int i = 0; i <PlateauDeJeu.GetLength(0);i++)
			{
                for (int j = 0; j < PlateauDeJeu.GetLength(1); j++)
                {
					PlateauDeJeu[i,j] = null;
                }
            }
            EnnemiVisible = EnnemiAffiché;
		}

		public void AfficherPlateau()
		{
			Console.Clear();
            for (int i = 0; i < PlateauDeJeu.GetLength(0); i++)
            {

                for (int j = 0; j < PlateauDeJeu.GetLength(1); j++)
                {
					Console.Write("[");
					switch (PlateauDeJeu[i,j])
					{
						case Hero:
							Console.ForegroundColor = ConsoleColor.Blue;
                            Console.Write("H");
							Console.ResetColor();
                            break;
						case Loup:
                            if (EstVisible(i,j))
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.Write("L");
                                Console.ResetColor();
                            }
                            else
                            {
                                AfficherVide();
                            }
                            break;
                        case Orque:
                            if (EstVisible(i, j))
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.Write("O");
                                Console.ResetColor();
                            }
                            else
                            {
                                AfficherVide();
                            }
                            break;
                        case Dragonnet:
                            if (EstVisible(i, j))
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.Write("D");
                                Console.ResetColor();
                            }
                            else
                            {
                                AfficherVide();
                            }
                            break;
                        default:
                            AfficherVide();
                            break;

					}
					Console.Write("]");
                }
                Console.Write("\n");
            }
            tickDeJeu++;
        }
        /// <summary>
        /// Condition de visibilité
        /// S'appliquer pour les monstre
        /// </summary>
        /// <param name="ligne">ligne du plateau</param>
        /// <param name="colonne">colonne du plateau</param>
        /// <returns>true = visible</returns>
        private bool EstVisible(int ligne, int colonne)
        {
            bool visibilité = EnnemiVisible;
            if(!visibilité)
            {
                int x = _Joueur.X, y = _Joueur.Y;
                if((x == colonne &&( MathF.Abs(y-ligne)==1)) ||
                    (y == ligne && (MathF.Abs(x - colonne) == 1)))
                {
                    return true;
                }
                //TODO vérifier si héro a coté
            }
            return visibilité;
        }
        private void AfficherVide()
        {
            string valeurParDefaut ="?";
            
            switch(tickDeJeu%4)
            {
                case 1:
                    valeurParDefaut = "/";
                    break;
                case 3:
                    valeurParDefaut = "\\";
                    break;
                default:
                    valeurParDefaut = "|";
                    break;
            }

            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write(valeurParDefaut);
            Console.ResetColor();
        }
        
        public void AjouterPersonnage(Personnage personnageAAjouté, int ligne, int colonne)
        {
            personnageAAjouté.Y = ligne;
            personnageAAjouté.X = colonne;
            this.AjouterPersonnage(personnageAAjouté);
        }

        public void AjouterPersonnage(Personnage personnageAAjouté)
		{
            int y , x;
            int largeur = PlateauDeJeu.GetLength(1), hauteur = PlateauDeJeu.GetLength(0);

            y = personnageAAjouté.Y;
            x = personnageAAjouté.X;

                if (y < 0 || y >= hauteur)
            {
                Random monRandom = new Random();
                int nouvelY = monRandom.Next(PlateauDeJeu.GetLength(0));
                personnageAAjouté.Y = nouvelY;
                y = nouvelY;

            }
            if (x < 0 || x >= largeur)
            {
                Random monRandom = new Random();
                int nouvelX = monRandom.Next(PlateauDeJeu.GetLength(1));
                personnageAAjouté.X = nouvelX;
                x = nouvelX;
            }

            while (!estLibre(y, x,2)) 
            {
                Random monRandom = new Random();
                x = monRandom.Next(PlateauDeJeu.GetLength(1));
                personnageAAjouté.X = x;
                y = monRandom.Next(PlateauDeJeu.GetLength(0));
                personnageAAjouté.Y = y;
            };

            //Vérification si c'est un monstre
            if (personnageAAjouté is Monstre)
            {
                Ennemis.Add(personnageAAjouté);
            }
			else
			{
                _Joueur = (Hero)personnageAAjouté;
			}
			//Ajout sur le plateau
			PlateauDeJeu[personnageAAjouté.Y, personnageAAjouté.X] = personnageAAjouté;
        }
        /// <summary>
        /// Méthode de vérification de la case observé
        /// </summary>
        /// <param name="ligne">ligne du plateau</param>
        /// <param name="colonne">colonne du plateau</param>
        /// <returns>vrai si libre (null) / Faux si occupée</returns>
        private bool estLibre (int ligne, int colonne, int distance = 1)
        {
            bool libre = true;
            int centralLigne = ligne, centralColonne = colonne;

            for (int i =centralLigne- distance < 0?0: centralLigne - distance;
                libre && i<(centralLigne+ distance+1 <= PlateauDeJeu.GetLength(0)? centralLigne + distance+1 : PlateauDeJeu.GetLength(0));
                i++)
            {
                for (int j = centralColonne - distance < 0 ? 0 : centralColonne - distance;
                libre && j < (centralColonne + distance+1 <= PlateauDeJeu.GetLength(1) ? centralColonne + distance+1 : PlateauDeJeu.GetLength(1));
                j++)
                {
                    if (PlateauDeJeu[i, j] is not null) libre = false;
                }
            }
            return libre;
        }
        /// <summary>
        /// Méthode qui indique le nombre d'ennemi sur le plateau
        /// </summary>
        /// <returns>le nombre d'ennemi enregistré sur le plateau</returns>
		public int EnnemisCount()
		{
			return Ennemis.Count;
		}
        /// <summary>
        /// Attend l'entrée d'une touche pour tenter un mouvement
        /// </summary>
        public void InputJoueur()
        {
            ConsoleKey key = Console.ReadKey().Key;
            switch(key.ToString())
            {
                case "UpArrow":
                case "z":
                case "Z":
                case "NumPad8":
                    BougerJoueur(-1, 0);
                    break;
                case "DownArrow":
                case "s":
                case "S":
                case "NumPad2":
                    BougerJoueur(1,0);
                    break;
                case "LeftArrow":
                case "q":
                case "Q":
                case "NumPad4":

                    BougerJoueur(0, -1);
                    break;
                case "RightArrow":
                case "d":
                case "D":
                case "NumPad6":
                    BougerJoueur(0, 1);
                    break;

                default:
                    Console.WriteLine("pas reconnu "+ key.ToString());
                    break;
            }
        }
        /// <summary>
        /// Vérifier si le mouvement est correct et l'applique
        /// </summary>
        /// <param name="y">mouvement en terme de ligne</param>
        /// <param name="x">mouvement en terme de colonne</param>
        private void BougerJoueur(int y,int x)
        {
            if(_Joueur.X+x >=0 &&
                _Joueur.X + x < PlateauDeJeu.GetLength(1) &&
                _Joueur.Y + y >= 0 &&
                _Joueur.Y + y < PlateauDeJeu.GetLength(0) &&
                estLibre(_Joueur.Y + y,_Joueur.X + x,0)
                )
            {
                PlateauDeJeu[_Joueur.Y, _Joueur.X] = null;
                PlateauDeJeu[_Joueur.Y + y, _Joueur.X + x] = _Joueur;
                _Joueur.X = _Joueur.X + x;
                _Joueur.Y = _Joueur.Y + y;
            }
        }

        //TODO déclencher combat
        public bool EnCombat()
        {
            int x = _Joueur.X, y = _Joueur.Y;
            if((x-1 >= 0 && PlateauDeJeu[y,x-1] is Monstre)||
                (y - 1 >= 0 && PlateauDeJeu[y - 1,x] is Monstre) || 
                (x + 1 < PlateauDeJeu.GetLength(1) && PlateauDeJeu[y, x + 1] is Monstre) ||
                (y + 1 < PlateauDeJeu.GetLength(0) && PlateauDeJeu[y+1, x] is Monstre))
            {
                return true;
            }
            return false;
        }
        public void Batail()
        {
            Monstre? Challenger = null;
            bool trouvé = false;
            for(int i = 0; i < Ennemis.Count && !trouvé;i++)
            {
                if ((MathF.Abs(_Joueur.X - Ennemis[i].X) == 1 && MathF.Abs(_Joueur.Y - Ennemis[i].Y) == 0) ||
                    (MathF.Abs(_Joueur.Y - Ennemis[i].Y) == 1 && MathF.Abs(_Joueur.X - Ennemis[i].X) == 0))
                {
                    trouvé = true;
                    Challenger = (Monstre)Ennemis[i];
                }
            }
            if(Challenger is not null)
            {
                int round = 1;
                do
                {
                    Console.WriteLine("round"+round++);
                    _Joueur.Frappe(Challenger);
                    if (Challenger.EstVivant())
                    {
                        Challenger.Frappe(_Joueur);
                    }

                }
                while (Challenger.EstVivant() && _Joueur.EstVivant());
                if(!Challenger.EstVivant())
                {
                    PlateauDeJeu[Challenger.Y, Challenger.X] = null;
                    Ennemis.Remove(Challenger);
                }
            }

        }
    }
}

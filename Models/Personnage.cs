using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static HeroesVsMonster.Models.Dés;

namespace HeroesVsMonster.Models
{
    public abstract class Personnage
    {
        #region Position sur Plateau

        public int X { get; set; }
        public int Y { get; set; }
        #endregion
        #region Stat de base
        protected int _Endurance;
		public virtual int End
		{
			get { return _Endurance; }
			protected set { _Endurance = value; }
		}
		public int ModEnd
		{
			get { return getModificateur(End); }
		}

		protected int _Force;

        public virtual int For
		{
			get { return _Force; }
			protected set { _Force = value; }
		}
		public int ModFor 
		{ 
			get { return getModificateur(For); }
		}

		private int PointDeVie;
		private int PointDeVieMax;
		public int Pv
		{
			get { return PointDeVie; }
			protected set { PointDeVie = value; }
		}
		public int PvMax { get { return PointDeVieMax; } }
        #endregion

        /// <summary>
        /// Constructeur par lancé de dé
        /// </summary>
        public Personnage() 
		{
            this.End = Dés.LancerDésTronqué(4, Dés.Faces.Six, 3);
			this.For = Dés.LancerDésTronqué(4, Dés.Faces.Six, 3);
            this.Pv = End + this.ModEnd;
			this.PointDeVieMax = this.Pv;

            this.X = -1;
            this.Y = -1;

        }
		/// <summary>
		///Constructeur classique
		/// </summary>
		/// <param name="Endurance">Valeur de base en Endurance du personnage</param>
		/// <param name="Force">Valeur de base en Force du personnage</param>
		protected Personnage(int Endurance, int Force)
		{
			this.End = Endurance;
			this.For = Force;
			this.Pv = _Endurance + End;

            this.X = -1;
            this.Y = -1;
        }

		/// <summary>
		/// Méthode sortant le modificateur d'une caractèristique du personnage
		/// </summary>
		/// <param name="value">Valeur à partir de laquelle le modificateur est calculé</param>
		/// <returns>Le modificateur de la caractéristique</returns>
        protected int getModificateur(int value)
		{
			/*
			int result = value - 10;
			return result / 5;
			/**/
			if (value < 5) return -1;
			if (value < 10) return 0;
			if (value < 15) return 1;
			return 2;
		}

        /// <summary>
        /// Methode qui attaque une cible pour lui infliger des dégats
        /// </summary>
        /// <param name="Cible">Personnage cible de l'attaque</param>
        public virtual void Frappe(Personnage Cible)
		{
			int degat = 0;
			degat += Dés.LancerDé4();
			degat += this.ModFor;

			Cible.RecevoirDegat(degat);

        }
		/// <summary>
		/// Méthode qui sera appelé quand ce personnage se prend un coup
		/// </summary>
		/// <param name="value">nombre de dégat de cette attaque</param>
		/// <returns>renvoit true si la cible meurt sur cette attaque</returns>
		public bool RecevoirDegat(int value)
		{
			if(value < this.Pv)
            {
                this.Pv -= value;
				return false;
            }
			else
			{
				this.Pv = 0;
				return true;
			}
		}
		/// <summary>
		/// Méthode vérifiant si le personnage est encore vivant
		/// </summary>
		/// <returns>Vrai si les pv du personnage sont positif Sinon renvoit Faux</returns>
		public bool EstVivant()
		{
			if (Pv > 0) return true;
			return false;
		}
	}
}

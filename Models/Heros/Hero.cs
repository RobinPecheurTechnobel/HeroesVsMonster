using HeroesVsMonster.Models.Monstres;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HeroesVsMonster.Models.Heros
{
    public abstract class Hero : Personnage
    {
        private List<string> _Inventaire = new List<string>();

        public List<string> Inventaire
        {
            get { return _Inventaire; }
            set { _Inventaire = value; }
        }

        public override void Frappe(Personnage Cible)
        {
            int degat = 0;
            degat += Dés.LancerDé4();
            degat += ModFor;

            if (Cible.RecevoirDegat(degat))
            {
                //fin de combat victorieux
                //loot sur monstre
                if (Cible is Monstre)
                    foreach (Delegate methode in ((Monstre)Cible).SonLoot.GetInvocationList())
                    {
                        string loot = (string)methode.GetMethodInfo().Invoke((Monstre)Cible, null);
                        if (loot is not null)
                        {
                            Console.WriteLine("    "+loot);
                            this.Inventaire.Add(loot);
                        }
                    }
                //Repos pour récupérer sa vie
                Repos();
            }
        }
        protected void Repos()
        {
            Pv = PvMax;
        }
    }
}

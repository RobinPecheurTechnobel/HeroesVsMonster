using HeroesVsMonster.Models.Monstres;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Channels;
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

        public string Name { get; set; }

        public Hero (string Name) :base()
        {
            this.Name = Name;
        }

        public override void Frappe(Personnage Cible)
        {
            int degat = 0;
            degat += Dés.LancerDé4();
            degat += ModFor;

            string message = this.Name + $" frappe et inflige {degat} à ";
            if (Cible is Hero)
                message += ((Hero)Cible).Name;
            else message += Cible.GetType().Name;
            Console.WriteLine(message);

            if (Cible.RecevoirDegat(degat))
            {
                if (Cible is Hero)
                    Console.WriteLine(((Hero)Cible).Name+" s'éffondre");
                else Console.WriteLine(Cible.GetType().Name + " s'éffondre");

                //fin de combat victorieux
                //loot sur monstre
                if (Cible is Monstre)
                    foreach (Delegate methode in ((Monstre)Cible).SonLoot.GetInvocationList())
                    {
                        string loot = (string)methode.GetMethodInfo().Invoke((Monstre)Cible, null);
                        if (loot is not null)
                        {
                            Console.WriteLine(this.Name+" ramasse 1 "+loot);
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

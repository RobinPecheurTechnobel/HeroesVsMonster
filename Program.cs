using HeroesVsMonster.Models;
using HeroesVsMonster.Models.Heros;
using HeroesVsMonster.Models.Monstres;

namespace HeroesVsMonster
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine(Dés.LancerDés(2,1,true));

            //Console.WriteLine(Dés.LancerDésTronqué(4,Dés.Faces.Six,3,true,true));
            /*
            Monstre monEnnemi = NouveauMonstre();
            Hero monHero = new Humain();

            int nbMonstretué = 0;
            do
            {


                monHero.Frappe(monEnnemi);
                if (nbMonstretué + 1 < 10 && !monEnnemi.EstVivant())
                {
                    nbMonstretué++;
                    monEnnemi = NouveauMonstre();
                }

            } while (monEnnemi.EstVivant() && monHero.EstVivant() && nbMonstretué<10);

            Console.WriteLine("inventaire final");
            foreach (string objet in monHero.Inventaire)
                Console.WriteLine(objet);
            /**/
            
            Hero monHero = new Humain("Conan");


            Plateau monPlateau = new Plateau(true);
            monPlateau.AjouterPersonnage(monHero ,14,14);
            for(int i = 0; i< 10;i++)
            {
                monPlateau.AjouterPersonnage(NouveauMonstre());
            }

            do
            {
                monPlateau.AfficherPlateau();
                Console.WriteLine($"Il reste {monPlateau.EnnemisCount()} monstre(s) sur la map");
                Console.WriteLine();
                if (!monPlateau.EnCombat())
                    monPlateau.InputJoueur();
                else 
                {
                    monPlateau.Batail();
                    Console.ReadLine();
                }
            } while (monHero.EstVivant() && monPlateau.EnnemisCount() > 0);
            if (monHero.EstVivant())
                Console.WriteLine("Bien joué!");
            else
                Console.WriteLine("Game Over");




        }
        public static Monstre NouveauMonstre()
        {
            Random monRandom = new Random();
            int valeurAleatoire = monRandom.Next(3);

            Monstre nouveauMonstre;

            switch(valeurAleatoire)
            {
                case 0:
                    nouveauMonstre = new Loup();
                    Console.WriteLine("Loup");
                    break;
                case 1:
                    nouveauMonstre = new Orque();
                    Console.WriteLine("Orque");
                    break;
                case 2:
                    nouveauMonstre = new Dragonnet();
                    Console.WriteLine("Dragonnet");
                    break;
                default:
                    nouveauMonstre = new Loup();
                    Console.WriteLine("Loup");
                    break;
            }
            return nouveauMonstre;
        }
    }
}
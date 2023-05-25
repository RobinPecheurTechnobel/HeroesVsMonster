using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeroesVsMonster.Models.Heros
{
    public class Nain : Hero
    {
        //---------------ENDURANCE------------
        protected int _BonusRacialEndurance = 2;
        public new int End
        {
            get { return base.End + _BonusRacialEndurance; }

            protected set { _Endurance = value; }
        }
        public new int ModEnd
        {
            get { return getModificateur(End); }
        }

        //---------------FORCE------------
        protected int _BonusRacialForce = 0;
        public new int For
        {
            get { return base.For + _BonusRacialForce; }
            protected set { _Force = value; }
        }
        public new int ModFor
        {
            get { return getModificateur(For); }
        }

        public Nain() : base()
        {
            Pv = _Endurance + ModEnd;
        }

        public override string ToString()
        {
            return $"Nain < PV : {Pv} | For {For} | End {End}>;";
        }
    }
}

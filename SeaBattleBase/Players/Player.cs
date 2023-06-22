using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SeaBattleBase.Players
{
    public delegate Point SetShot();
    public delegate void SetFlotilla();
    /*
    public abstract class Player
    {
        public string Name { set; get; }
        public Field Field { set; get; } = new Field(10);
        public List<Ship> Flotilla { get; set; } = new List<Ship>();
        public Player(string name)
        {
            Name = name;
        }

        public Player() : base() { Name = "Computer"; }
        public HitType GetShot(Point point)
        {
            HitType hitType;
            hitType = Field.CheckShot(point);
            Field.SetCellShot(point);

            if (hitType == HitType.Beside)
                return HitType.Beside;
            else
            {
                for (int i = 0; i < Flotilla.Count; i++)
                    if (Flotilla[i].IsPoint(point))
                    {
                        Flotilla[i].Damage++;
                        if (Flotilla[i].IsDead)
                            hitType = HitType.Destroy;
                        else
                            hitType = HitType.Wound;
                        break;
                    }
                return hitType;
            }
        }
        public int FlotillaSize
        {
            get
            {
                int count = 0;
                foreach (Ship ship in Flotilla)
                    if (!ship.IsDead)
                        count++;
                return count;
            }
        }
        public abstract void SetFlotiila();
        public abstract Point SetShot();
    }
    */

    public class Player
    {
        public string Name { set; get; }
        public Field Field { set; get; } = new Field(10);
        public List<Ship> Flotilla { get; set; } = new List<Ship>();
        public Player(string name) => Name = name;
        public Player() : base() { Name = "Computer"; }
        public SetFlotilla Commander { get; set; }
        public SetShot Shooter { get; set; }

        public HitType GetShot(Point point)
        {
            HitType hitType;
            hitType = Field.CheckShot(point);
            Field.SetCellShot(point);

            if (hitType == HitType.Beside)
                return HitType.Beside;
            else
            {
                for (int i = 0; i < Flotilla.Count; i++)
                    if (Flotilla[i].IsPoint(point))
                    {
                        Flotilla[i].Damage++;
                        if (Flotilla[i].IsDead)
                            hitType = HitType.Destroy;
                        else
                            hitType = HitType.Wound;
                        break;
                    }
                return hitType;
            }
        }
        public int FlotillaSize
        {
            get
            {
                int count = 0;
                foreach (Ship ship in Flotilla)
                    if (!ship.IsDead)
                        count++;
                return count;
            }
        }
    }
}

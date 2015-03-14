using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AE2.Tools.Views.EditorViews
{
    public struct MapPosition
    {

        public sbyte X;
        public sbyte Y;

        public MapPosition(MapPosition p)
        {
            X = p.X;
            Y = p.Y;
        }

        public MapPosition(sbyte x, sbyte y)
        {
            X = x;
            Y = y;
        }

        public MapPosition(int x, int y) : this((sbyte)x, (sbyte)y) { }

        public override bool Equals(object obj)
        {
            if (!(obj is MapPosition)) return false;
            MapPosition o = (MapPosition)obj;
            return (o.X == X && o.Y == Y);            
        }

        public bool IsWithin(sbyte x, sbyte y, sbyte w, sbyte h)
        {
            return (X >= x && X < (x + w)) && (Y >= y && Y < (y + h));
        }

        public bool IsWithin(int x, int y, int w, int h)
        {
            return IsWithin((sbyte)x, (sbyte)y, (sbyte)w, (sbyte)h);
        }
    }
}

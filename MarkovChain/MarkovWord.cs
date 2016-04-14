using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkovChain
{
    class MarkovWord : System.Object
    {
        public string Text;
        public string POS;

        public MarkovWord(string textAndPOS)
        {
            int underscoreIndex = textAndPOS.IndexOf("_");
            Text = textAndPOS.Substring(0, underscoreIndex);
            POS = textAndPOS.Substring(underscoreIndex + 1, textAndPOS.Length - (underscoreIndex + 1));
        }

        public MarkovWord(string text, string pos)
        {
            Text = text;
            POS = pos;
        }

        public static bool operator ==(MarkovWord a, MarkovWord b)
        {
            // If both are null, or both are same instance, return true.
            if (System.Object.ReferenceEquals(a, b))
            {
                return true;
            }

            // If one is null, but not both, return false.
            if (((object)a == null) || ((object)b == null))
            {
                return false;
            }

            // Return true if the fields match:
            return a.Text == b.Text && a.POS == b.POS;
        }

        public static bool operator !=(MarkovWord a, MarkovWord b)
        {
            return !(a == b);
        }

        public override bool Equals(System.Object obj)
        {
            // If parameter is null return false.
            if (obj == null)
            {
                return false;
            }

            // If parameter cannot be cast to Point return false.
            MarkovWord p = obj as MarkovWord;
            if ((System.Object)p == null)
            {
                return false;
            }

            // Return true if the fields match:
            return (Text == p.Text) && (POS == p.POS);
        }

        public bool Equals(MarkovWord x)
        {
            return ((x.Text == Text) && (x.POS == POS));
        }

        public override int GetHashCode()
        {
            string combined = Text + "|" + POS;
            return (combined.GetHashCode());
        }

    }

    class MarkovWordEqualityComparer : IEqualityComparer<MarkovWord>
    {
        public bool Equals(MarkovWord x, MarkovWord y)
        {
            return ((x.Text == y.Text) && (x.POS == y.POS));
        }

        public int GetHashCode(MarkovWord obj)
        {
            string combined = obj.Text + "|" + obj.POS;
            return (combined.GetHashCode());
        }

        
    }
}

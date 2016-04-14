using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace MarkovChain
{
    class MarkovKey
    {
        public List<MarkovWord> Words;

        public MarkovKey(List<MarkovWord> words)
        {
            Words = words;
        }
    }

    //reference - https://social.msdn.microsoft.com/Forums/vstudio/en-US/41fd15a3-6fda-4855-afe5-73498fd1a067/example-of-dictionary-collection-with-custom-class-as-key?forum=netfxbcl
    class MarkovKeyEqualityComparer : IEqualityComparer<MarkovKey>
    {
        public bool Equals(MarkovKey x, MarkovKey y)
        {
            if(x.Words.Count == y.Words.Count)
            {
                for(int i = 0; i < x.Words.Count; i++)
                {
                    if(x.Words[i] != y.Words[i]) {
                        return false;
                    }
                }
                return true;
            } else
            {
                return false;
            }
        }

        public int GetHashCode(MarkovKey obj)
        {
            string combined = "";
            foreach (MarkovWord word in obj.Words)
            {
                combined += word.Text + word.POS;
            }
            return (combined.GetHashCode());
        }
    }
}

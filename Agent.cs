using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lea
{
    public class Agent
    {
        private List<string> learnedCombinations = new List<string>();
        public Agent ()
        {

        }

        public List<string> LearnedCombinations
        {
            get { return this.learnedCombinations; }
        }

        public void AddCombination(List<string> learnedCombinations, int[] combination, int combNumber)
        {
            if (!IsLearnedComb(learnedCombinations, combination))
            {
                learnedCombinations.Add(string.Join("", combination));
            }
        }

        public bool IsLearnedComb(List<string> learnedCombinations, int[] combination)
        {
            bool learned = false;
            if (learnedCombinations.Count == 0)
            {
                return false;
            }
            for (int i = 0; i < learnedCombinations.Count; i++)
            {
                if (learnedCombinations[i] == string.Join("", combination))
                {
                    learned = true;
                    break;
                }
            }

            return learned;
        }
    }
}

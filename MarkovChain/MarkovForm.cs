using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MarkovChain
{
    public partial class MarkovForm : Form
    {
        public MarkovForm()
        {
            InitializeComponent();
        }

        private void AnalyzeButton_Click(object sender, EventArgs e)
        {
            Dictionary<string, Dictionary<string, int>> wordDict = BuildWordDict(InputBox.Text);

            int outputLength = 1000;
            StringBuilder chain = new StringBuilder("");
            string currentWord = "I";
            Random r = new Random();
            for (int i = 0; i < outputLength; i++)
            {
                chain.Append(currentWord + " ");
                currentWord = RetrieveRandomWord(wordDict[currentWord],r);
            }

            OutputBox.Text = chain.ToString();
        }

        private int WordListSum(Dictionary<string, int> wordList)
        {
            int sum = 0;
            foreach(KeyValuePair<string,int> word in wordList)
            {
                sum += word.Value;
            }
            return sum;
        }

        private string RetrieveRandomWord(Dictionary<string,int> wordList, Random r)
        {
            int randIndex = r.Next(1, WordListSum(wordList));

            foreach(KeyValuePair<string,int> word in wordList)
            {
                randIndex -= word.Value;
                if(randIndex <= 0)
                {
                    return word.Key;
                }
            }
            return null;
        }

        private Dictionary<string, Dictionary<string, int>> BuildWordDict(string text)
        {
            text = text.Replace("\n", " ");
            text = text.Replace("\"", "");

            string[] punctuation = new string[4] { ",", ".", ";", ":" };
            foreach (string symbol in punctuation)
            {
                text = text.Replace(symbol, " " + symbol + " ");
            }

            string[] words = text.Split(null);
            words = words.Where(x => !string.IsNullOrEmpty(x)).ToArray();

            Dictionary<string, Dictionary<string,int>> wordDict = new Dictionary<string, Dictionary<string,int>>();
            for (int i = 1; i < words.Count(); i++) 
            {
                if (!wordDict.ContainsKey(words[i-1]))
                {
                    wordDict.Add(words[i-1], new Dictionary<string, int>());
                }
                if (!wordDict[words[i - 1]].ContainsKey(words[i])){
                    wordDict[words[i - 1]][words[i]] = 0;
                }
                wordDict[words[i - 1]][words[i]] += 1;
            }
            return wordDict;
        }
    }
}

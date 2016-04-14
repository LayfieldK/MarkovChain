using com.sun.tools.javac.util;
using edu.stanford.nlp.ling;
using edu.stanford.nlp.pipeline;
using edu.stanford.nlp.semgraph;
using edu.stanford.nlp.tagger.maxent;
using edu.stanford.nlp.trees;
using edu.stanford.nlp.util;
using java.io;
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
using static edu.stanford.nlp.ling.CoreAnnotations;
using static edu.stanford.nlp.trees.TreeCoreAnnotations;
using Console = System.Console;

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

            wordDict = BuildWordDict(InputBox2.Text, wordDict);

            int outputLength = 1000;
            StringBuilder chain = new StringBuilder("");
            System.Random r = new System.Random();
            // string currentWords[] = new string[]{ "In", "a" };
            Queue<string> currentWords = new Queue<string>();
            currentWords.Enqueue("In");
            currentWords.Enqueue("a");

            for (int i = 0; i < outputLength; i++)
            {
                string dequeuedItem = currentWords.Dequeue();
                chain.Append(dequeuedItem + " ");
                currentWords.Enqueue(RetrieveRandomWord(wordDict[dequeuedItem + " " + currentWords.ElementAt(0)],r));
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

        private string RetrieveRandomWord(Dictionary<string,int> wordList, System.Random r)
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
            return BuildWordDict(text, new Dictionary<string, Dictionary<string, int>>());
        }

        private Dictionary<string, Dictionary<string, int>> BuildWordDict(string text, Dictionary<string, Dictionary<string, int>> wordDict)
        {
            text = text.Replace("\n", " ");
            text = text.Replace("\"", "");

            string[] punctuation = new string[5] { ",", ".", ";", ":", "-" };
            foreach (string symbol in punctuation)
            {
                text = text.Replace(symbol, " " + symbol + " ");
            }

            string[] words = text.Split(null);
            words = words.Where(x => !string.IsNullOrEmpty(x)).ToArray();

            for (int i = 2; i < words.Count(); i++) 
            {
                if (!wordDict.ContainsKey(words[i-2] + " " + words[i-1]))
                {
                    wordDict.Add(words[i - 2] + " " + words[i - 1], new Dictionary<string, int>());
                }
                if (!wordDict[words[i - 2] + " " + words[i - 1]].ContainsKey(words[i])){
                    wordDict[words[i - 2] + " " + words[i - 1]][words[i]] = 0;
                }
                wordDict[words[i - 2] + " " + words[i - 1]][words[i]] += 1;
            }
            return wordDict;
        }
        

    }
}


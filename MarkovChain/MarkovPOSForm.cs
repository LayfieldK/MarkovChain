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
    public partial class MarkovPOSForm : Form
    {
        public MarkovPOSForm()
        {
            InitializeComponent();
        }

        private void AnalyzeButton_Click(object sender, EventArgs e)
        {
            Dictionary<MarkovKey, Dictionary<MarkovWord, int>> wordDict = new Dictionary<MarkovKey, Dictionary<MarkovWord, int>>(new MarkovKeyEqualityComparer());
            int wordOrder = 2;
            wordDict = BuildWordDict(InputBox.Text, wordOrder);
            wordDict = BuildWordDict(InputBox2.Text, wordDict, wordOrder);

            int outputLength = 1000;
            StringBuilder chain = new StringBuilder("");
            System.Random r = new System.Random();
            // string currentWords[] = new string[]{ "In", "a" };
            Queue<MarkovWord> currentWords = new Queue<MarkovWord>();
            for(int i = 0; i < wordOrder; i++)
            {
                currentWords.Enqueue(wordDict.ElementAt(0).Key.Words.ElementAt(i));
            }

            for (int i = 0; i < outputLength; i++)
            {
                MarkovWord dequeuedItem = currentWords.Dequeue();
                chain.Append(dequeuedItem.Text + " ");
                MarkovKey newKey = new MarkovKey(new List<MarkovWord>());
                newKey.Words.Add(dequeuedItem);
                for (int j = 0; j < wordOrder - 1; j++)
                {
                    newKey.Words.Add(currentWords.ElementAt(j));
                }
                
                currentWords.Enqueue(RetrieveRandomWord(wordDict[newKey], r));
            }

            OutputBox.Text = chain.ToString();

            //Stanford.NLP.CoreNLP.CSharp.TaggerDemo.TagText(InputBox.Text);
        }

        private int WordListSum(Dictionary<MarkovWord, int> wordList)
        {
            int sum = 0;
            foreach(KeyValuePair<MarkovWord, int> word in wordList)
            {
                sum += word.Value;
            }
            return sum;
        }

        private MarkovWord RetrieveRandomWord(Dictionary<MarkovWord,int> wordList, System.Random r)
        {
            int randIndex = r.Next(1, WordListSum(wordList));

            foreach(KeyValuePair<MarkovWord, int> word in wordList)
            {
                randIndex -= word.Value;
                if(randIndex <= 0)
                {
                    return word.Key;
                }
            }
            return null;
        }

        private Dictionary<MarkovKey, Dictionary<MarkovWord, int>> BuildWordDict(string text, int wordOrder)
        {
            return BuildWordDict(text, new Dictionary<MarkovKey, Dictionary<MarkovWord, int>>(new MarkovKeyEqualityComparer()), wordOrder);
        }

        private Dictionary<MarkovKey, Dictionary<MarkovWord, int>> BuildWordDict(string text, Dictionary<MarkovKey, Dictionary<MarkovWord, int>> wordDict, int wordOrder)
        {
            text = text.Replace("\n", " ");
            text = text.Replace("\"", "");

            string taggedText = Stanford.NLP.CoreNLP.CSharp.TaggerDemo.TagText(text);

            List<string> words = taggedText.Split(null).ToList<string>();
            words = words.Where(x => !string.IsNullOrEmpty(x)).ToList<string>();

            List<MarkovWord> markovWords = new List<MarkovWord>();
            foreach(string word in words)
            {
                markovWords.Add(new MarkovWord(word));
            }

            for (int i = wordOrder; i < markovWords.Count(); i++)
            {
                MarkovKey currentKey = new MarkovKey(markovWords.GetRange(i - wordOrder, wordOrder));
                if (!wordDict.ContainsKey(currentKey))
                {
                    wordDict.Add(currentKey, new Dictionary<MarkovWord, int>());
                }
                if (!wordDict[currentKey].ContainsKey(markovWords[i]))
                {
                    wordDict[currentKey][markovWords[i]] = 0;
                }
                wordDict[currentKey][markovWords[i]] += 1;
            }
            return wordDict;
        }


    }
}

namespace Stanford.NLP.CoreNLP.CSharp
{
    public static class TaggerDemo
    {
        public static string TagText(string text)
        {
            var jarRoot = "F:\\Visual Studio Projects\\MarkovChain\\MarkovChain\\obj\\Debug\\stanford-postagger-full-2015-12-09";
            var modelsDirectory = jarRoot + "\\models";

            // Loading POS Tagger
            var tagger = new MaxentTagger(modelsDirectory + "\\wsj-0-18-bidirectional-nodistsim.tagger");

            return tagger.tagString(text);
            //var sentences = MaxentTagger.tokenizeText(new StringReader(text)).toArray();
            //foreach (java.util.ArrayList sentence in sentences)
            //{

            //    var taggedSentence = tagger.tagSentence(sentence);
            //    Console.WriteLine(Sentence.listToString(taggedSentence, false));
            //}
        }
    }
}
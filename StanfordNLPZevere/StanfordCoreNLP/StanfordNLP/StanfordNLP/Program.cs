using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using java.util;
using java.io;
using edu.stanford.nlp.pipeline;
using Console = System.Console;
using System.Text.RegularExpressions;

namespace StanfordNLP
{
    class Program
    {
        static void Main(string[] args)
        {
            //var jarRoot = @"..\..\..\..\data\paket-files\nlp.stanford.edu\models";
            //var jarRoot = @"C:\StanfordNLP\edu\stanford\nlp\models";
            // Text for processing
           // var text = "I am Md Mamunur Rahman. I have been working in Philips for the last 2 years.";
            var text = Console.ReadLine();
            // Annotation pipeline configuration
            var props = new Properties();
            props.setProperty("annotators", "tokenize, ssplit, pos, lemma, ner, parse, dcoref");
         //   props.setProperty("annotators", "tokenize, ssplit, parse");
         //   props.setProperty("annotators", "tokenize, ssplit, parse");
            //props.setProperty("annotators", "pos");
            //props.setProperty("annotators", "lemma");
            //props.setProperty("annotators", "ner");
            //props.setProperty("annotators", "parse");
            //props.setProperty("annotators", "dcoref");
            //props.setProperty("ner.useSUTime", "0");
            props.setProperty("pos.model",
            @"C:\StanfordNLP\edu\stanford\nlp\models\pos-tagger\english-bidirectional\english-bidirectional-distsim.tagger");
            props.setProperty("ner.model", @"C:\StanfordNLP\edu\stanford\nlp\models\ner\english.all.3class.distsim.crf.ser.gz");
            props.setProperty("parse.model", @"C:\StanfordNLP\edu\stanford\nlp\models\lexparser\englishPCFG.ser.gz");
            props.setProperty("dcoref.demonym", @"C:\StanfordNLP\edu\stanford\nlp\models\dcoref\demonyms.txt");
            props.setProperty("dcoref.states", @"C:\StanfordNLP\edu\stanford\nlp\models\dcoref\state-abbreviations.txt");
            props.setProperty("dcoref.animate", @"C:\StanfordNLP\edu\stanford\nlp\models\dcoref\animate.unigrams.txt");
            props.setProperty("dcoref.inanimate", @"C:\StanfordNLP\edu\stanford\nlp\models\dcoref\inanimate.unigrams.txt");
            props.setProperty("dcoref.male", @"C:\StanfordNLP\edu\stanford\nlp\models\dcoref\male.unigrams.txt");
            props.setProperty("dcoref.neutral", @"C:\StanfordNLP\edu\stanford\nlp\models\dcoref\neutral.unigrams.txt");
            props.setProperty("dcoref.female", @"C:\StanfordNLP\edu\stanford\nlp\models\dcoref\female.unigrams.txt");
            props.setProperty("dcoref.plural", @"C:\StanfordNLP\edu\stanford\nlp\models\dcoref\plural.unigrams.txt");
            props.setProperty("dcoref.singular", @"C:\StanfordNLP\edu\stanford\nlp\models\dcoref\singular.unigrams.txt");
            props.setProperty("dcoref.countries", @"C:\StanfordNLP\edu\stanford\nlp\models\dcoref\countries");
            props.setProperty("dcoref.extra.gender", @"C:\StanfordNLP\edu\stanford\nlp\models\dcoref\namegender.combine.txt");
            props.setProperty("dcoref.states.provinces", @"C:\StanfordNLP\edu\stanford\nlp\models\dcoref\statesandprovinces");
            props.setProperty("dcoref.singleton.predictor", @"C:\StanfordNLP\edu\stanford\nlp\models\dcoref\singleton.predictor.ser");
            props.setProperty("dcoref.big.gender.number", @"C:\StanfordNLP\edu\stanford\nlp\models\dcoref\gender.map.ser.gz");
            props.setProperty("sutime.rules", @"C:\StanfordNLP\edu\stanford\nlp\models\sutime\defs.sutime.txt, C:\StanfordNLP\edu\stanford\nlp\models\sutime\english.holidays.sutime.txt, C:\StanfordNLP\edu\stanford\nlp\models\sutime\english.sutime.txt");
            props.setProperty("sutime.binders", "0");
            props.setProperty("ner.useSUTime", "0");
            // We should change current directory, so StanfordCoreNLP could find all the model files automatically
            //var curDir = Environment.CurrentDirectory;
            // Console.WriteLine(curDir);
            // Directory.SetCurrentDirectory(jarRoot);




            var pipeline = new StanfordCoreNLP(props);
          //  Directory.SetCurrentDirectory(curDir);

            // Annotation
            var annotation = new Annotation(text);
            pipeline.annotate(annotation);

           
            // Result - Pretty Print
            Console.WriteLine("INPUT");
            Console.WriteLine("------------------------------------------------------------------------------------");
            Console.WriteLine(text);
            Console.WriteLine("------------------------------------------------------------------------------------");
            Console.WriteLine("OUTPUT");
            List<string> verb = new List<string>();
            List<string> common_noun = new List<string>();
            List<string> proper_noun = new List<string>();
            using (var stream = new ByteArrayOutputStream())
            {
                
                //  pipeline.prettyPrint(annotation, new PrintWriter(stream));
                pipeline.conllPrint(annotation, new PrintWriter(stream));
                string output = stream.toString();
                Console.WriteLine(output);


                string[] lines = Regex.Split(output, "[\r\n]+");
               // Console.WriteLine(lines.Length);
                string[][] wordMatrix = new string[lines.Length][];
                for (var i = 0; i < wordMatrix.Length; i++)
                {
                    wordMatrix[i] = new string[10];
                    string[] words = Regex.Split(lines[i], "[^a-zA-Z0-9]+");
                   // Console.WriteLine(words.Length);
                    for (int ii = 0; ii < words.Length; ii++)
                    {
                        wordMatrix[i][ii] = words[ii];
                    }

                }



                for (int i = 0; i < lines.Length; i++)
                {
                    for (int ii = 0; ii < wordMatrix[i].Length; ii++)
                    {
                       // Console.WriteLine(wordMatrix[i][ii]);
                        if (wordMatrix[i][ii] == "VB")
                        { verb.Add(wordMatrix[i][ii - 1] + " " + wordMatrix[i][6]);
                        }
                        if (wordMatrix[i][ii] == "NN")
                        {
                            common_noun.Add(wordMatrix[i][ii - 1] + " " + wordMatrix[i][6]);
                        }
                        if (wordMatrix[i][ii] == "NNP")
                        {
                            proper_noun.Add(wordMatrix[i][ii - 1] + " " + wordMatrix[i][6]);
                        }
                    }

                }



                stream.close();
            }
            foreach (var item in verb)
            {
                Console.WriteLine("This is verb: " + item);
            }
            foreach (var item in common_noun)
            {
                Console.WriteLine("This is a common noun: " + item);
            }
            foreach (var item in proper_noun)
            {
                Console.WriteLine("This is a proper noun: " + item);
            }

            Console.ReadLine();
        }
    }
}

﻿using System;
using System.Linq;
using WikiTrivia.QuestionGenerator.Model;

namespace WikiTrivia.QuestionGenerator.Generators
{
    public static class BasedOnDOBJQGenerator
    {
        public static GeneratedQuestion TreatSentenceWithDOBJ(SentenceInformationDto sentence,
           SentenceDependencyDto sentenceDOBJ)
        {
            var answer = sentenceDOBJ.DependentGloss;
            var answerWord = Helper.FindWordInList(sentence.Words, answer);

            answer = AnswerGenerator.GenerateAnswer(sentence, sentenceDOBJ);
            var answerWords = answer.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            var verbe = Helper.FindWordInList(sentence.Words, sentenceDOBJ.GovernorGloss);

            var firstWord = sentence.Words.FirstOrDefault();
            if (firstWord == null)
            {
                return null;
            }
            string question;
            var questionText = sentence.SentenceText
                .Replace(firstWord.Word, firstWord.Lemma);
            foreach (var word in answerWords)
            {
                questionText = questionText.Replace($" {word}", " ");
            }


            var answerNMODOF = sentence.Dependencies.FirstOrDefault(d => d.Dep.ToLower() == "nmod:of" &&
                    d.GovernorGloss == answerWord.Word);
            var nmodOfString = " ";
            if (answerNMODOF != null)
            {
                questionText = questionText.Replace(answerNMODOF.DependentGloss, "");
                nmodOfString = $" of {answerNMODOF.DependentGloss} ";
            }

            var dobjNUMMode = sentence.Dependencies.FirstOrDefault(d => d.Dep.ToLower() == "nummod" &&
                    d.GovernorGloss == sentenceDOBJ.DependentGloss);
            if (dobjNUMMode != null)
            {
                if (verbe.PartOfSpeech.ToLower() == "vbd")
                {
                    questionText = questionText.Replace(verbe.Word, verbe.Lemma);
                    question = $"How many {answerWord.Word}{nmodOfString}did {questionText}";
                    return new GeneratedQuestion { Answer = answer, Question = question };
                }
                question = $"How many {answerWord.Word}{nmodOfString}{questionText}";
                return new GeneratedQuestion { Answer = answer, Question = question };
            }



            if (answerWord.PartOfSpeech.ToLower() == "nn" ||
                   answerWord.PartOfSpeech.ToLower() == "nns")
            {
                question = TreatObjectCase(sentence, verbe, questionText);
                return new GeneratedQuestion { Answer = answer, Question = question };
            }

            var answerPOS = Helper.FindWordInList(sentence.Words, answerWord.Word);
            if (answerPOS.PartOfSpeech.ToLower() == "nnp" ||
                answerPOS.PartOfSpeech.ToLower() == "nn")
            {
                if (verbe.PartOfSpeech.ToLower() == "vbd")
                {
                    questionText = questionText.Replace(verbe.Word, verbe.Lemma);
                    question = TreatPersonCase(sentence, questionText, sentenceDOBJ, answerWord, true);
                }
                else
                {
                    question = TreatPersonCase(sentence, questionText, sentenceDOBJ, answerWord);
                }
                return new GeneratedQuestion { Answer = answer, Question = question };
            }
            return null;
        }


        private static string TreatObjectCase(SentenceInformationDto sentence, WordInformationDto verbe, string questionText)
        {
            questionText = questionText.Replace(verbe.Word, verbe.Lemma);

            var verbeAux =
                sentence.Dependencies.FirstOrDefault(d => d.Dep.ToLower() == "aux" && d.GovernorGloss == verbe.Word);
            if (verbeAux != null)
            {
                questionText = questionText.Replace(verbeAux.DependentGloss, "");
            }

            switch (verbe.PartOfSpeech.ToLower())
            {
                case "vbd":
                    return $"What did {questionText}";
                case "vbz":
                    return $"What does {questionText}";
                case "vbp":
                    return $"What do {questionText}";
                default:
                    return null;
            }
        }

        private static string TreatPersonCase(SentenceInformationDto sentence, string questionText,
            SentenceDependencyDto sentenceDependency, WordInformationDto baseAnswer, bool isPast = false)
        {

            if (baseAnswer.NamedEntityRecognition.ToLower() == "person" ||
                baseAnswer.PartOfSpeech.ToLower() == "nnp")
            {
                return isPast ?
                    $"Who did {questionText}" :
                    $"Who {questionText}";
            }

            var dobjAux = sentence.Dependencies
                .FirstOrDefault(d => d.Dep.ToLower() == "auxpass" && d.GovernorGloss == sentenceDependency.GovernorGloss);
            if (dobjAux != null)
            {
                questionText = questionText.Replace(dobjAux.DependentGloss, "");
                return $"How {dobjAux.DependentGloss} {questionText}";
            }
            return $"How is {questionText}";
        }
    }
}

//Solo Project- Quiz Game


using System;
using System.Collections.Generic;
using System.Threading;

namespace QuizGame
{
    //Abstract Class - Parent class

    public abstract class Question
    {
        public string Text { get; }

        public Question(string text)
        {
            Text = text;
        }
        public abstract bool Ask();      //check condition true or false       
        public void ShowQuestion()
        {
            Console.WriteLine($"\n Question: {Text}");
        }

    }

    //Derived Class for Multiple choice questions

    public class MultipleChoiceQuestion : Question
    {
        public List<string> Options { get; }
        public int CorrectOptionIndex { get; }

        public MultipleChoiceQuestion(string text, List<string> options, int correctOptionIndex) : base(text)
        {
            Options = options;
            CorrectOptionIndex = correctOptionIndex;
        }

        public override bool Ask()
        {
            ShowQuestion();
            for (int i = 0; i < Options.Count; i++)            //loops through option list
            {
                Console.WriteLine($"{i + 1}. {Options[i]}");
            }

            Console.Write($"Enter your choice (1 to {Options.Count}):  ");

            string input = Console.ReadLine();
            

            if (!int.TryParse(input, out int choice) || choice < 1 || choice > Options.Count)   //NOT & OR condition is used & check within valid range
              {
                  Console.WriteLine(" Invalid input! Moving to next question.");
                  return false;
              }

              if (choice - 1 == CorrectOptionIndex)
              {
                  Console.WriteLine(" Correct!");
                  return true;
              }
              else
              {
                  Console.WriteLine($" Incorrect! The correct answer is: {Options[CorrectOptionIndex]}");
                  return false;
              }
              
        }
    }

    //Derived class for True or False
    public class TrueFalseQuestion : Question
    {
        public bool CorrectAnswer { get; }
        public TrueFalseQuestion(string text, bool correctAnswer) : base(text)
        {
            CorrectAnswer = correctAnswer;
        }

        public override bool Ask()
        {
            ShowQuestion();
            Console.Write("Enter 'TRUE' or 'FALSE': ");

            string input = Console.ReadLine();
            
            if (!bool.TryParse(input.ToLower(), out bool userAnswer))
            {
                Console.WriteLine(" Invalid input! Moving to next question.");
                return false;
            }

            if (userAnswer == CorrectAnswer)
            {
                Console.WriteLine(" Correct!");
                return true;
            }
            else
            {
                Console.WriteLine($" Incorrect! The correct answer is: {CorrectAnswer.ToString().ToUpper()}");
                return false;
            }


        }
    }
    
    //Main Program
    class Program
    {
        public static void Main(string[] args)
        {
            int score = 0;

            Console.WriteLine(" **** QUIZ GAME **** \n");

            var quiz = new List<Question>
            {
                //Multiple Choice Questions
                

                new MultipleChoiceQuestion("Who wrote 'Harry Potter'?",
                    new List<string> { "Mark Twain", "Jane Austen", "J. K. Rowling", "Charles Dickens" }, 2),

                new MultipleChoiceQuestion("Which planet is known as the Red Planet?",
                    new List<string> { "Earth", "Mars", "Jupiter", "Venus" }, 1),

                new MultipleChoiceQuestion("What is the currency of India?",
                    new List<string> { "Rupee'₹'", "Pound'£'", "Dollar'$'", "Yen'¥'" }, 0),

                new MultipleChoiceQuestion("What is the capital of France?",
                    new List<string> { "Berlin", "Madrid", "Paris", "Rome" }, 2),


                // True or False Questions

                new TrueFalseQuestion("The tallest mountain in the world is Mount Everest.", true),

                new TrueFalseQuestion("James Bond code number is '001'.", false),

                new TrueFalseQuestion("There are 8 continents are on Earth.", false),

                new TrueFalseQuestion("Chemical Symbol for Water is H2O.", true),


            };

            foreach (var question in quiz)
            {
                bool isCorrect = question.Ask();
                if (isCorrect)
                {
                    score++;

                }
                Thread.Sleep(1000);

            }

            Console.WriteLine($"\n Quiz Completed! Your final score is: {score}/{quiz.Count}\n");

            Console.WriteLine($"*** Thank you for playing! ***\n");
        }
    }

}
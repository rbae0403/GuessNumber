using System;
using System.Linq;

namespace GuessNumber
{
    /// <summary>
    /// Program
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// Mains the specified arguments.
        /// </summary>
        /// <param name="args">The arguments.</param>
        private static void Main(string[] args)
        {
            var question = default(int[]);
            while (true)
            {
                Console.WriteLine("請填入欲猜謎數字題目(4個數字且不重複)");
                var questionString = Console.ReadLine();
                if (ValidateRule(questionString))
                {
                    question = questionString.Select(item => (int)item).ToArray();
                    break;
                }
                else
                {
                    ShowMessage("輸入的題目必須為4個數字且不重複");
                }
            }

            while (true)
            {
                Console.WriteLine("請輸入解答(4個數字且不重複)");
                var answerString = Console.ReadLine();
                if (ValidateRule(answerString))
                {
                    var answer = answerString.Select(item => (int)item).ToArray();
                    var result = GetMappingResult(question, answer);
                    ShowMessage($"您輸入的結果為：{result}");
                    if (result.ToLower() == "4a0b")
                    {
                        break;
                    }
                }
                else
                {
                    ShowMessage("輸入的答案必須為4個數字且不重複");
                }
            }

            Console.WriteLine("恭喜你答對，請按任意鍵離開");
            Console.Read();
        }

        /// <summary>
        /// 取得使用者輸入解答的結果
        /// </summary>
        /// <param name="question">題目</param>
        /// <param name="answer">使用者輸入的解答</param>
        /// <returns>驗證結果</returns>
        private static string GetMappingResult(int[] question, int[] answer)
        {
            int hitACount = default(int);
            var hitBCount = question.Intersect(answer).Count();
            for (int i = 0; i < answer.Count(); i++)
            {
                if (answer[i] == question[i])
                {
                    hitACount += 1;
                }
            }

            hitBCount -= hitACount;
            return $"{hitACount}A{hitBCount}B";
        }

        /// <summary>
        /// 驗證題目規則
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>驗證結果</returns>
        private static bool ValidateRule(string input)
        {
            if (input.Length != 4)
            {
                return false;
            }

            var tmp = default(int);
            foreach (var item in input.Select(item => item))
            {
                if (!int.TryParse(item.ToString(), out tmp))
                {
                    return false;
                }
            }

            var tmpAnswer = new string(input.Select(item => item).Distinct().ToArray());
            return input == tmpAnswer;
        }

        /// <summary>
        /// 顯示文字
        /// </summary>
        /// <param name="message">The message.</param>
        private static void ShowMessage(string message)
        {
            Console.WriteLine("========================================");
            Console.WriteLine(message);
            Console.WriteLine("========================================");
        }
    }
}
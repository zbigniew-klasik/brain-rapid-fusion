using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BrainRapidFusion.Multiplication.Components
{
    public class MultiplicationBase : ComponentBase
    {
        public string Question { get; private set; }
        public List<Tuple<int,bool>> Answers { get; private set; }

        public MultiplicationBase()
        {
            Question = "2 * 3 = ?";
            Answers = new List<Tuple<int, bool>>
            {
                new Tuple<int, bool>(4, false),
                new Tuple<int, bool>(6, true),
                new Tuple<int, bool>(7, false),
                new Tuple<int, bool>(10, false)
            };
        }

        public void AnswerSelected(int answer)
        {
            Console.WriteLine(answer);
        }
    }
}

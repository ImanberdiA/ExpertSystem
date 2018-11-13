using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductionSystem
{
    public class ProductionInferenceEngine
    {
        public List<ProductionRule> Rules { get; set; }

        private string Goal { get; set; }

        private Stack<ProductionRule> WorkSpace { get; set; }

        public ProductionInferenceEngine(List<ProductionRule> rules)
        {
            Rules = rules;
        }

        public ProductionInferenceResult ExecuteForwardChaining(string fact, string goal)
        {
            Goal = goal;

            WorkSpace = new Stack<ProductionRule>();
            ProductionInferenceResult result;
            if (CheckGoal(fact, 0))    // recursion starts here
                result = new ProductionInferenceResult(true, WorkSpace.Reverse().ToList());
            else
                result = new ProductionInferenceResult(false, null);

            return result;
        }

        /// <param name="count">Count of tries to use given left part</param>
        private bool CheckGoal(string left, int count)
        {
            var rule = TryGetNextRule(left, count);
            if (rule == null)
                return false;

            WorkSpace.Push(rule);

            if (rule.Right == Goal)
                return true;


            if (NoCycles() && CheckGoal(rule.Right, 0))
                return true;
            else
            {
                WorkSpace.Pop();
                return CheckGoal(left, count + 1);
            }
        }

        private ProductionRule TryGetNextRule(string left, int index)
        {
            var rules = Rules.Where(r => r.Left == left)?.ToList();
            if (rules == null || rules.Count <= index)
                return null;
            else
                return rules[index];
        }

        private bool NoCycles()
        {
            return (WorkSpace.Count(r => r.IsEqual(WorkSpace.Peek())) == 1) ? true : false;
        }

        public ProductionInferenceResult ExecuteBackwardChaining(string fact, string goal)
        {
            Goal = fact;

            var tempRules = Rules;
            Rules = Rules.Select(r => new ProductionRule(r.Right, r.Left)).ToList();

            WorkSpace = new Stack<ProductionRule>();
            ProductionInferenceResult result;
            if (CheckGoal(goal, 0))    // recursion starts here
                result = new ProductionInferenceResult(true, WorkSpace.Reverse().ToList());
            else
                result = new ProductionInferenceResult(false, null);

            Rules = tempRules;
            return result;
        }
    }
}

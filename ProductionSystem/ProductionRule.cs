using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductionSystem
{
    public class ProductionRule
    {
        public string Left { get; set; }

        public string Right { get; set; }

        public ProductionRule(string leftPart, string rightPart)
        {
            Left = leftPart;
            Right = rightPart;
        }

        public bool IsEqual(ProductionRule rule)
        {
            return (rule.Left == Left && rule.Right == Right) ? true : false;
        }
    }
}

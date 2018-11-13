using System.Collections.Generic;

namespace ProductionSystem
{
    public class ProductionInferenceResult
    {
        public bool HasSolution { get; set; }

        public List<ProductionRule> Explanation { get; set; }

        public ProductionInferenceResult(bool hasSolution, List<ProductionRule> explanation)
        {
            HasSolution = hasSolution;
            Explanation = explanation;
        }
    }
}
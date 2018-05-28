namespace Complexitytheory.SubsetSum
{
    public class SubSetSumInfo
    {
        public double[] Weights { get; private set; }

        public double TargetSum { get; private set; }

        public SubSetSumInfo(double[] pWeights, double pTargetSum)
        {
            this.Weights = pWeights;
            this.TargetSum = pTargetSum;
        }
    }
}
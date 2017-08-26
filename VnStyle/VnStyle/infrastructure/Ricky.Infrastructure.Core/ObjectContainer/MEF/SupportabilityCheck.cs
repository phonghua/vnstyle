namespace Ricky.Infrastructure.Core.ObjectContainer.MEF
{
    public static class SupportabilityCheck
    {
        public static void CheckParameters(Parameter[] parameters)
        {
            if (parameters != null && parameters.Length > 0)
            {
                throw new NotSupportedFeaturesException();
            }
        }
    }
}

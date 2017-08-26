namespace Ricky.Infrastructure.Core.ObjectContainer.MEF
{
    public class MEFEngine : EngineBase, IEngine
    {
        #region Ctor
        public MEFEngine()
            : this(new AppDomainTypeFinder())
        {

        }
        public MEFEngine(ITypeFinder typeFinder)
            : this(typeFinder, new MEFContainerManager())
        { }
        public MEFEngine(ITypeFinder typeFinder, MEFContainerManager containerManager)
            : base(typeFinder, containerManager)
        {


        }
        #endregion






    }
}

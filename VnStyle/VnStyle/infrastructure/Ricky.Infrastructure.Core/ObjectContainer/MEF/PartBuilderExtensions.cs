using System.ComponentModel.Composition.Registration;
using Ricky.Infrastructure.Core.ObjectContainer.Dependency;

namespace Ricky.Infrastructure.Core.ObjectContainer.MEF
{
    public static class PartBuilderExtensions
    {
        public static PartBuilder LifeStyle(this PartBuilder pb, ComponentLifeStyle lifeStyle)
        {
            switch (lifeStyle)
            {
                case ComponentLifeStyle.Singleton:
                    pb = pb.SetCreationPolicy(System.ComponentModel.Composition.CreationPolicy.Shared);
                    break;
                case ComponentLifeStyle.InRequestScope:
                case ComponentLifeStyle.InThreadScope:
                    throw new NotSupportedFeaturesException();
                case ComponentLifeStyle.Transient:
                default:
                    pb = pb.SetCreationPolicy(System.ComponentModel.Composition.CreationPolicy.NonShared);
                    break;
            }

            return pb;
        }
    }
}

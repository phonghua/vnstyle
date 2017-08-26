using System;

namespace Ricky.Infrastructure.Core.ObjectContainer.MEF
{
    public class NotSupportedFeaturesException : NotSupportedException
    {
        const string Msg = @"In order to avoid the use of small projects and the introduction of more complex  third party IoC container, reduce dependence on third-party DLL's. ObjectContainer default implementation of the MEF adapter as a simple object container. Restrictions, the following features are not supported in MEF MEF inside：IResolvingObserver,InThreadScope/InRequestScope生命周期,泛型注册,InjectProperties,params Parameter[] parameters,AddComponentInstance";
        public NotSupportedFeaturesException() :
            base(Msg)
        {

        }
    }
}

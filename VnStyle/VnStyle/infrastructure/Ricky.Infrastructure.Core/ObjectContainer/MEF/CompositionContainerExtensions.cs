using System;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;

namespace Ricky.Infrastructure.Core.ObjectContainer.MEF
{
    public static class CompositionContainerExtensions
    {
        public static object GetExportedValue(this CompositionContainer container, Type type, string contractName = null)
        {
            var export = container.GetExports(type, null, contractName).FirstOrDefault();
            if (export == null)
            {
                throw new ImportCardinalityMismatchException();
            }
            return export.Value;
        }
        public static object GetExportedValueOrDefault(this CompositionContainer container, Type type, string contractName = null)
        {
            var export = container.GetExports(type, null, contractName).FirstOrDefault();
            if (export == null)
            {
                return null;
            }
            return export.Value;
        }
        public static object[] GetExportedValues(this CompositionContainer container, Type type, string contractName = null)
        {
            var exports = container.GetExports(type, null, contractName);
            return exports.Select(it => it.Value).ToArray();
        }
    }
}

// no copyright - borrowed from glass mapper

using Castle.MicroKernel;
using Castle.MicroKernel.Context;
using Castle.MicroKernel.Lifestyle;

namespace Cardinal.IoC.Windsor
{
    /// <summary>
    /// This manager deliberately does not track
    /// 
    /// </summary>
    public class NoTrackLifestyleManager : AbstractLifestyleManager
    {
        public override void Dispose()
        {
        }

        public override object Resolve(CreationContext context, IReleasePolicy releasePolicy)
        {
            return CreateInstance(context, true).Instance;
        }
    }
}

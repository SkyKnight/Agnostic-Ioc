using Cardinal.IoC.UnitTests.Helpers;
using Cardinal.IoC.Windsor;

namespace Cardinal.IoC.UnitTests.TestAdapters
{
    public class EmptytWindsorContainerAdapter : WindsorContainerAdapter
    {
        public override string Name
        {
            get { return TestConstants.EmptyWindsorContainerName; }
        }

        public override void Setup()
        {
        }
    }
}

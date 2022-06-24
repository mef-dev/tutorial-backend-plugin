using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestPluginPackage;

namespace TestProject
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Action()
        {
            var entity = new TestEntity();

            var ans = entity.PostEntityAction(
                "en", 
                new TestEntityRequest_TestAction{
                    Message= "Hello!" 
                },
                "testaction");

            Assert.IsTrue("Echo 'Hello!'".Equals((ans as ResponseRowBaseEntity).Description));

        }
    }
}

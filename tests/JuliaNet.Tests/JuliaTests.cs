using NUnit.Framework;
using JuliaNet;


namespace JuliaNet.Tests
{
    public class JuliaTests
    {
        [OneTimeSetUp]
        public void Setup()
        {
            Julia.Initialize();
        }

        [Test]
        public void Initialize_Julia_should_succeed()
        {
            // Julia has been initialized in [SetUp]
            bool initialized = Julia.IsInitialized;
            Assert.IsTrue(initialized);
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            Julia.Exit(0);
        }
    }
}
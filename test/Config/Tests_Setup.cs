using FluentSharp.CoreLib.API;
using NUnit.Framework;

namespace FluentSharp.CoreLib.Test
{
    [SetUpFixture]
    public class Tests_Setup
    {
        [OneTimeSetUp]
        public void RunBeforeAnyTests()
        {
            O2ConfigSettings.CheckForTempDirMaxSizeCheck = false;
        }

        [OneTimeTearDown]
        public void RunAfterAnyTests()
        {
            O2ConfigSettings.CheckForTempDirMaxSizeCheck = true;
        }
    }
}

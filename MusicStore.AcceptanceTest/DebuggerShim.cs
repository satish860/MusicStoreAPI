using NSpec;
using NSpec.Domain;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

/*
 * Howdy,
 *
 * This is NSpec's DebuggerShim.  It will allow you to use TestDriven.Net or Resharper's test runner to run
 * NSpec tests.
 *
 * It's DEFINITELY worth trying specwatchr (http://nspec.org/continuoustesting). Specwatchr automatically
 * runs tests for you.
 *
 * If you ever want to debug a test when using Specwatchr, simply put the following line in your test:
 *
 *     System.Diagnostics.Debugger.Launch()
 *
 * Visual Studio will detect this and will give you a window which you can use to attach a debugger.
 */

//[TestFixture]
public class DebuggerShim
{
    private IEnumerable<string> nspecCollection;
    private string targetLocation;

    /*****************************************************************************
        Use the below methods to find code coverage of all the nspec tests.
     *****************************************************************************/

    [TestFixtureSetUp]
    public void GetSpecifications()
    {
        var targetAssembly = Assembly.GetExecutingAssembly();
        targetLocation = targetAssembly.Location;

        nspecCollection = (from type in targetAssembly.GetTypes()
                           where typeof(nspec).IsAssignableFrom(type)
                           select type.Name);
    }
    [Test]
    public void AcceptanceTest()
    {
        if (nspecCollection != null)
        {
            foreach (var tagOrClassName in nspecCollection)
            {
                var invocation = new RunnerInvocation(targetLocation, tagOrClassName);

                var contexts = invocation.Run();

                // Uncomment the below line if the test invocation is to be stopped
                // as soon as a failure is detected.

                //contexts.Failures().Count().should_be(0);
            }
        }

        //assert that there aren't any failure
    }
}
using FluentSharp.NUnit;
using NUnit.Framework;

namespace FluentSharp.CoreLib.Test
{
    [TestFixture]
    public class Test_Misc_ExtensionMethods
    {
        [Test] public void isDouble()
        {
            "123"                            .isDouble().assert_Is_True();
            "123123123123213"                .isDouble().assert_Is_True();
            "123123123123213123123"          .isDouble().assert_Is_True();
            "123123123123213123123123123213" .isDouble().assert_Is_True();
            "123123123123213123123123123213123123123213123123123123211341412341234213421342134".isDouble().assert_Is_True();
            "a"                              .isDouble().assert_Is_False();
            "a123"                           .isDouble().assert_Is_False();
            (null as string)                 .isDouble().assert_Is_False();

            Assert.IsTrue ("123".isDouble());
            Assert.IsTrue ("123123123123213".isDouble(  ));
            Assert.IsTrue ("123123123123213123123".isDouble());
            Assert.IsTrue ("123123123123213123123123123213".isDouble());
            Assert.IsTrue ("123123123123213123123123123213123123123213123123123123211341412341234213421342134".isDouble());
            Assert.IsFalse("a".isDouble());            
            Assert.IsFalse("a123".isDouble());
            Assert.IsFalse((null as string).isDouble());
        }
    }
}

﻿
namespace CsLuaTest
{
    using AmbigousMethods;
    using Arrays;
    using CsLua.Collection;
    using CsLuaAttributes;
    using Generics;
    using DefaultValues;
    using General;
    using Interfaces;
    using Lua;
    using Override;
    using Params;
    using Serialization;
    using TryCatchFinally;
    using Wrap;
    using Static;
    using Type;
    using TypeMethods;

    [CsLuaAddOn("CsLuaTest", "CsLua Test", 60200, Author = "Pilus", Notes = "Unit tests for the CsLua framework.")]
    public class CsLuaTest : ICsLuaAddOn
    {
        public void Execute()
        {
            var tests = new CsLuaList<ITestSuite>()
            {
                new TryCatchFinallyTests(),
                new GeneralTests(),
                new AmbigousMethodsTests(),
                new OverrideTest(),
                new SerializationTests(),
                new DefaultValuesTests(),
                new InterfacesTests(),
                new WrapTests(),
                new GenericsTests(),
                new StaticTests(),
                new TypeMethodsTests(),
                new ParamsTests(),
                new ArraysTests(),
                new TypeTests(),
            };

            tests.Foreach(test => test.PerformTests(new IndentedLineWriter()));
            Core.print("CsLua test completed.");
            Core.print(BaseTest.TestCount, "tests run.", BaseTest.FailCount, "failed.");
        }
    }
}

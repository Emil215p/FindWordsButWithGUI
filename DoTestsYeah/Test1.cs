namespace DoTestsYeah
{
    [TestClass]
    public sealed class Test1
    {
        [TestMethod]
        public void TestMethod1()
        {
            const string expectedOutput = "Hello, World!";
            using (var sw = new System.IO.StringWriter())
            {
                System.Console.SetOut(sw);
                System.Console.WriteLine("Hello, World!");
                Assert.AreEqual(expectedOutput, sw.ToString().Trim());
            }
        }
    }
}

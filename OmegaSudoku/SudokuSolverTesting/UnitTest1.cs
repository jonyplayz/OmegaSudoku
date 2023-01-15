using Microsoft.VisualStudio.TestTools.UnitTesting;
using OmegaSudoku;

namespace SudokuSolverTesting
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Test4x4()
        {
            string board = "100000027000304015500170683430962001900007256006810000040600030012043500058001000";
            string trueAnswer = "193586427867324915524179683435962871981437256276815349749658132612743598358291764";
            Controller c = new Controller();
            string actualAnswer = c.beginSolving(board);
            Assert.AreEqual(trueAnswer, actualAnswer);

        }
    }
}

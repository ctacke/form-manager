using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenNETCF.FormManager;
using System.IO;

namespace FormManager.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestFormCreationInMemory()
        {
            var form = new Form("Electric Crane Daily Report");

            // intro/header
            form.Header.Add("Crane");
            form.Header.Add("Date", "%DATE%");
            form.Header.Add("Shift", "%SHIFT%");

            // questions
            form.Questions.Add("Warning Device", QuestionType.PassFail);
            form.Questions.Add("Bridge Lights", QuestionType.PassFail);
            form.Questions.Add("Fire Extinguisher On Crane", QuestionType.PassFail);
            form.Questions.Add("Fire Extinguisher Full", QuestionType.PassFail);
            form.Questions.Add("Housekeeping", QuestionType.PassFail);
            form.Questions.Add("Crane Capacity Sign", QuestionType.PassFail);
            form.Questions.Add("Windows", QuestionType.PassFail);
            form.Questions.Add("Crane Lubricated", QuestionType.PassFail);
            form.Questions.Add("Grease Time", QuestionType.Input, units: "Hours");

            form.Questions.Add("Hoist Limit Test",  QuestionType.PassFail);
            form.Questions.Add("Hoist Brakes", QuestionType.PassFail);
            form.Questions.Add("Crane Hook", QuestionType.PassFail);
            form.Questions.Add("Crane Cables", QuestionType.PassFail);

            Assert.IsTrue(form.Questions.Count == 13);

            // footer/out
            form.Footer.Add("Employee", "%USER%");

            form.RequiresSignature = true;
        }

        [TestMethod]
        public void TestFormStoreSQLite()
        {
            var form = new Form("Electric Crane Daily Report");

            // intro/header
            form.Header.Add("Crane");
            form.Header.Add("Date", "%DATE%");
            form.Header.Add("Shift", "%SHIFT%");

            // questions
            form.Questions.Add("Warning Device", QuestionType.PassFail);
            form.Questions.Add("Bridge Lights", QuestionType.PassFail);
            form.Questions.Add("Fire Extinguisher On Crane", QuestionType.PassFail);
            form.Questions.Add("Fire Extinguisher Full", QuestionType.PassFail);
            form.Questions.Add("Housekeeping", QuestionType.PassFail);
            form.Questions.Add("Crane Capacity Sign", QuestionType.PassFail);
            form.Questions.Add("Windows", QuestionType.PassFail);
            form.Questions.Add("Crane Lubricated", QuestionType.PassFail);
            form.Questions.Add("Grease Time", QuestionType.Input, units: "Hours");

            form.Questions.Add("Hoist Limit Test", QuestionType.PassFail);
            form.Questions.Add("Hoist Brakes", QuestionType.PassFail);
            form.Questions.Add("Crane Hook", QuestionType.PassFail);
            form.Questions.Add("Crane Cables", QuestionType.PassFail);

            // footer/out
            form.Footer.Add("Employee", "%USER%");

            form.RequiresSignature = true;

            var store = new FormsStore();
            
            store.StoreForm(form);

            var f = store.GetForm(form.FormName);

            store.DeleteForm(f);
        }

    }
}

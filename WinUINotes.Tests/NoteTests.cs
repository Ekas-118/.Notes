using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using WinUINotes.Tests.Fakes;

namespace WinUINotes.Tests
{
    [TestClass]
    public partial class NoteTests
    {
        [TestMethod]
        public void TestCreateUnsavedNote()
        {
            var noteVm = new ViewModels.NoteViewModel(new FakeFileService());
            Assert.IsNotNull(noteVm);
            Assert.IsTrue(noteVm.Date > DateTime.Now.AddHours(-1));
            Assert.IsTrue(noteVm.Filename.EndsWith(".txt"));
            Assert.IsTrue(noteVm.Filename.StartsWith("notes"));
            noteVm.Text = "Sample Note";
            Assert.AreEqual("Sample Note", noteVm.Text);
            noteVm.SaveCommand.Execute(null);
            Assert.AreEqual("Sample Note", noteVm.Text);
        }
    }
}
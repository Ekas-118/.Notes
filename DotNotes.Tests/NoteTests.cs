using DotNotes.Bus.ViewModels;
using DotNotes.Tests.Fakes;

namespace DotNotes.Tests
{
    [TestClass]
    public partial class NoteTests
    {
        [TestMethod]
        public void TestCreateUnsavedNote()
        {
            var noteVm = new NoteViewModel(new FakeFileService());
            Assert.IsNotNull(noteVm);
            Assert.IsGreaterThan(DateTime.Now.AddHours(-1), noteVm.Date);
            Assert.EndsWith(".txt", noteVm.Filename);
            Assert.StartsWith("notes", noteVm.Filename);
            noteVm.Text = "Sample Note";
            Assert.AreEqual("Sample Note", noteVm.Text);
            noteVm.SaveCommand.Execute(null);
            Assert.AreEqual("Sample Note", noteVm.Text);
        }
    }
}

using CommunityToolkit.Mvvm.Messaging.Messages;
using DotNotes.Bus.Models;

namespace DotNotes.Bus
{
    public class NoteDeletedMessage : ValueChangedMessage<Note>
    {
        public NoteDeletedMessage(Note note) : base(note)
        {
        }
    }
}
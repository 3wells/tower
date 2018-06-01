using System.Collections.Generic;
using Slight.Alexa.Framework.Models.Requests;

namespace Tower.Alexa
{
    public interface ISlotManager
    {
        bool SlotHasValue(Dictionary<string, Slot> slots, string slotName);
        string GetSlotValue(Dictionary<string, Slot> slots, string slotName);
    }

    public class SlotManager : ISlotManager
    {
        public bool SlotHasValue(Dictionary<string, Slot> slots, string slotName)
        {
            return slots != null && slots.ContainsKey(slotName) && !string.IsNullOrEmpty(slots[slotName].Value);
        }

        public string GetSlotValue(Dictionary<string, Slot> slots, string slotName)
        {
            return !SlotHasValue(slots, slotName) ? "" : slots[slotName].Value;
        }
    }
}

using System;

namespace InventorySystem
{
    public class InventoryUIElementNotFoundException : Exception
    {
        public InventoryUIElementNotFoundException()
        {
        }

        public InventoryUIElementNotFoundException(string message) : base(message)
        {
        }

        public InventoryUIElementNotFoundException(string message, Exception innerException) : base(message,
            innerException)
        {
        }
    }
}
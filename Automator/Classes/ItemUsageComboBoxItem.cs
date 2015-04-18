using System;
using Automator.Enums;

namespace Automator.Classes
{
    class ItemUsageComboBoxItem
    {
        public ItemUsageComboBoxItem(ItemUsage Usage)
        {
            this.Usage = Usage;
        }

        public ItemUsage Usage { get; protected set; }

        public override string ToString()
        {
            try
            {
                string value = string.Empty;

                value = UI.Manager.GetString(Usage.ToString());
                
                return string.IsNullOrEmpty(value) ? string.Empty : value;
            }
            catch (Exception) { }

            return base.ToString();
        }
    }
}

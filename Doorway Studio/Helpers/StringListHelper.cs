using System.Collections.Generic;
using System.Text;

namespace Doorway_Studio.Helpers
{
    static class StringListHelper
    {
        public static string AsString(this List<string> Value)
        {
            StringBuilder builder = new StringBuilder(Value.Count*100);

            for (int i = 0; i < Value.Count; i++)
            {
                builder.Append(Value[i]);
                builder.Append(" ");
            }

            return builder.ToString().Trim();
        }
    }
}

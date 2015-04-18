using System.Collections.Generic;

namespace Doorway_Studio.Classes
{
    public class RandomKeywordCollection
    {
        public RandomKeywordCollection()
        {
            RandomKeywords = new List<string>();
            RandomBigKeywords = new List<string>();
        }

        public List<string> RandomKeywords { get; protected set; }
        public List<string> RandomBigKeywords { get; protected set; }
    }
}

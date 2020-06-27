using System.Collections.Generic;
using System.Linq;

namespace BrainRapidFusion.Shared
{
    public class CssClass
    {
        private readonly List<string> cssClasses = new List<string>();

        public CssClass()
        {
        }

        public CssClass(string cssClass)
        {
            Add(cssClass);
        }

        public CssClass(IEnumerable<string> cssClasses)
        {
            cssClasses.ToList().ForEach(x => Add(x));
        }

        public void Add(string cssClass)
        {
            if (string.IsNullOrWhiteSpace(cssClass))
                return;

            if (cssClasses.Contains(cssClass))
                return;

            cssClasses.Add(cssClass);
        }

        public void Remove(string cssClass)
        {
            if (string.IsNullOrWhiteSpace(cssClass))
                return;

            cssClasses.Remove(cssClass);
        }

        public override string ToString()
        {
            return string.Join(" ", cssClasses);
        }
    }
}

using System.IO;
using System.Text;

namespace Doorway_Studio.Classes
{
    public class TemplatePage
    {
        public TemplatePage(string Content)
        {
            this.Path = string.Empty;
            this.Content = Content;
            this.UsagePercent = 0;

            this.CustomKeywords = new string[0];
            this.CustomName = string.Empty;
        }

        public TemplatePage(string TemplatePageParameterValue, Encoding Encoding)
        {
            this.Path = TemplatePageParameterValue;
            this.Content = string.Empty;
            this.UsagePercent = 0;

            // Load
            if (TemplatePageParameterValue.Contains("|"))
            {
                this.Path = TemplatePageParameterValue.Substring(0, TemplatePageParameterValue.IndexOf("|"));

                int percentage = 0;
                if (int.TryParse(TemplatePageParameterValue.Substring(TemplatePageParameterValue.IndexOf("|") + 1), out percentage))
                {
                    this.UsagePercent = percentage;
                }
            }

            this.Content = File.ReadAllText(this.Path, Encoding);

        }

        public string Path { get; set; }
        public string Content { get; set; }
        public int UsagePercent { get; set; }

        #region Static pages
        public string[] CustomKeywords { get; set; }
        public string CustomName { get; set; }
        #endregion
    }
}

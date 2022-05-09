namespace WrathModdingHelper
{
    public class TutorialLink
    {
        public string Text { get; set; } = "";
        public string Link { get; set; } = "";

        public TutorialLink() { }
        public TutorialLink(string link, string text) {
            Link = link;
            Text = text;
        }
    };

}

using System.Collections.Generic;

namespace AutoForumReader
{
    /// <summary>
    /// This class contains all of the properties that are part of being a ForumPost object
    /// </summary>
    public class ForumPostAttributes
    {
        public string website { get; set; }
        public string mainForumTitle { get; set; }
        public string postSite { get; set; }
        public string forumID { get; set; }
        public string forumTitle { get; set; }
        public string forumTimeStamp { get; set; }
        public string forumPreview { get; set; }
        public string forumPoster { get; set; }
        public List<string> posterSpec { get; set; }
        public int tankCounter { get; set; }
        public int dpsCounter { get; set; }
        public int healerCounter { get; set; }
    }
}

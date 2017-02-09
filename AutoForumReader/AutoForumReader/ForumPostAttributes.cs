using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using HtmlAgilityPack;

namespace AutoForumReader
{
    public class ForumPostAttributes
    {
        public string website { get; set; }
        public string postSite { get; set; }
        //public HtmlDocument webPage { get; set; }
        public string forumID { get; set; }
        public string forumTitle { get; set; }
        public string forumTimeStamp { get; set; }
        public string forumPreview { get; set; }
        public string forumPoster { get; set; }
    }
}

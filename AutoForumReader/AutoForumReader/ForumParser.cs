using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace AutoForumReader
{
    /// <summary>
    /// Parses through ForumPostAttribute object and determines whether the post is 
    /// a post from a prospective person looking for a guild
    /// </summary>
    class ForumParser
    {
        GetAppSettings appSettings = new GetAppSettings();
        
        /// <summary>
        /// Title parser will parse through titles of posts to determine if they are guild recruitment posts
        /// or if they are people looking for a guild
        /// </summary>
        /// <param name="forumPosts"></param>
        /// <returns></returns>
        public List<ForumPostAttributes> TitleParser(List<ForumPostAttributes> forumPosts)
        {
            try
            {

                Boolean isGuildRecruitmentPost = false;
                Boolean isProspectiveRecruit = false;

                int removeCounter;
                int iRemove;

                int readIndex = 0;
                List<int> readFlag = new List<int>();

                //Check to see if the post is a guild recruitment post.  If it is then remove it from the list
                readFlag.Clear();
                foreach (ForumPostAttributes post in forumPosts)
                {
                    isGuildRecruitmentPost = IsPost(post, appSettings.GFilters);

                    if (isGuildRecruitmentPost)
                    {
                        readFlag.Add(readIndex);
                    }
                    readIndex++;
                }

                removeCounter = 0;
                iRemove = 0;
                foreach (int removeIndex in readFlag)
                {
                    iRemove = removeIndex - removeCounter;
                    forumPosts.RemoveAt(iRemove);
                    removeCounter++;
                }

                //Check to see if the post matches a prospective raider looking for a guild.  if not then remove it from the list
                readFlag.Clear();
                readIndex = 0;
                foreach (ForumPostAttributes post in forumPosts)
                {
                    isProspectiveRecruit = IsPost(post, appSettings.LFFilters);

                    if (!isProspectiveRecruit)
                    {
                        readFlag.Add(readIndex);
                    }
                    readIndex++;
                }

                removeCounter = 0;
                iRemove = 0;
                foreach (int removeIndex in readFlag)
                {
                    iRemove = removeIndex - removeCounter;
                    forumPosts.RemoveAt(iRemove);
                    removeCounter++;
                }
                return forumPosts;
            }
            catch (Exception Ex)
            {
                string localError = "Error while parsing title of post!: ";
                //serverLog.Error(localError + Ex.Message);
                throw new Exception("-- FPARSE001 " + localError + Ex.Message.ToString());
            }
        }

        /// <summary>
        /// IsPost will use regular expressions defined in appConfig to determine what kind of post
        /// it is looking at
        /// </summary>
        /// <param name="post"></param>
        /// <param name="filters"></param>
        /// <returns></returns>
        private Boolean IsPost(ForumPostAttributes post, List<string> filters)
        {
            try
            {
                Boolean isFound = false;

                foreach (string filter in filters)
                {
                    //Uppercase title
                    string upperForumTitle = post.forumTitle.ToUpper();

                    //Set Regex
                    Regex regex = new Regex(filter);
                    Match match = regex.Match(upperForumTitle);

                    if (match.Success)
                    {
                        //Set to true if match is found
                        isFound = true;
                    }
                }
                return isFound;
            }
            catch (Exception Ex)
            {
                string localError = "Error while using regular expressions on title!: ";
                //serverLog.Error(localError + Ex.Message);
                throw new Exception("-- FPARSE002 " + localError + Ex.Message.ToString());
            }
        }
    }
}

using System;
using System.Collections.Generic;
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
        
        /// <summary>
        /// This method will parse through the email title first and look for what spec the poster is
        /// if the method is unable to find the spec of the poster in the title it will move on to
        /// the body of the message.  
        /// </summary>
        /// <param name="forumPosts"></param>
        /// <returns></returns>
        public List<ForumPostAttributes> CheckSpec(List<ForumPostAttributes> forumPosts)
        {
            try
            {
            foreach (ForumPostAttributes post in forumPosts)
            {
                    string upperForumTitle = post.forumTitle.ToUpper();
                    string upperForumTooltip = post.forumPreview.ToUpper();
                    List<string> posterSpecListInit = new List<string>();
                    post.posterSpec = posterSpecListInit;

                    //Set poster spec as tank if any of the keywords for tanks are found
                    foreach (string tankFilter in appSettings.TankFilters)
                    {
                        Regex regex = new Regex(tankFilter);
                        Match match = regex.Match(upperForumTitle);

                        if (match.Success)
                        {
                            post.posterSpec = Tankpost(post, "#Tank");
                            post.tankCounter++;
                        }
                        else
                        {
                            match = regex.Match(upperForumTooltip);

                            if (match.Success)
                            {
                                post.posterSpec = Tankpost(post, "#Tank");
                                post.tankCounter++;
                            }
                        }

                    }//END For Each #Tank

                    //Set poster spec as DPS if any of the keywords for DPS are found
                    foreach (string dpsFilter in appSettings.DpsFilters)
                    {
                        Regex regex = new Regex(dpsFilter);
                        Match match = regex.Match(upperForumTitle);

                        if (match.Success)
                        {
                            post.posterSpec = DPSpost(post, "#DPS");
                            post.dpsCounter++;
                        }
                        else
                        {
                            match = regex.Match(upperForumTooltip);

                            if (match.Success)
                            {
                                post.posterSpec = DPSpost(post, "#DPS");
                                post.dpsCounter++;
                            }
                        }

                    }//END For Each #DPS

                    //Set poster spec as Healer if any of the keywords for Healers are found
                    foreach (string healFilter in appSettings.HealsFilters)
                    {
                        Regex regex = new Regex(healFilter);
                        Match match = regex.Match(upperForumTitle);

                        if (match.Success)
                        {
                            post.posterSpec = Healerpost(post, "#Healer");
                            post.healerCounter++;
                        }
                        else
                        {
                            match = regex.Match(upperForumTooltip);

                            if (match.Success)
                            {
                                post.posterSpec = Healerpost(post, "#Healer");
                                post.healerCounter++;
                            }
                        }

                    }//END For Each #DPS

                    if (post.tankCounter == 0 && post.dpsCounter == 0 && post.healerCounter == 0)
                    {
                        post.posterSpec.Add("#Unknown");
                    }
                }
                //Found Nothing
                return forumPosts;
            }
            catch (Exception Ex)
            {
                string localError = "Error while adding tag identifier to post object: ";
                //serverLog.Error(localError + Ex.Message);
                throw new Exception("-- FPARSE003 " + localError + Ex.Message.ToString());
            }
        }

        /// <summary>
        /// This method will add in the tag for tank spec if called by the finder method above
        /// </summary>
        /// <param name="postIn"></param>
        /// <param name="spec"></param>
        /// <returns></returns>
        private List<string> Tankpost (ForumPostAttributes postIn, string spec)
        {
            try
            {
                if (postIn.tankCounter == null || postIn.tankCounter < 1)
                {
                    postIn.posterSpec.Add(spec);
                }
                return postIn.posterSpec;
            }
            catch (Exception Ex)
            {
                string localError = "Error while adding tag Tank identifier to post object: ";
                //serverLog.Error(localError + Ex.Message);
                throw new Exception("-- FPARSE004 " + localError + Ex.Message.ToString());
            }
        }

        /// <summary>
        /// This method will add in the tag for DPS spec if called by the finder method above
        /// </summary>
        /// <param name="postIn"></param>
        /// <param name="spec"></param>
        /// <returns></returns>
        private List<string> DPSpost(ForumPostAttributes postIn, string spec)
        {
            try
            {
                if (postIn.dpsCounter == null || postIn.dpsCounter < 1)
                {
                    postIn.posterSpec.Add(spec);
                }
                return postIn.posterSpec;
            }
            catch (Exception Ex)
            {
                string localError = "Error while adding tag DPS identifier to post object: ";
                //serverLog.Error(localError + Ex.Message);
                throw new Exception("-- FPARSE005 " + localError + Ex.Message.ToString());
            }
        }

        /// <summary>
        /// This method will add in the tag for Healer spec if called by the finder method above
        /// </summary>
        /// <param name="postIn"></param>
        /// <param name="spec"></param>
        /// <returns></returns>
        private List<string> Healerpost(ForumPostAttributes postIn, string spec)
        {
            try
            {
                if (postIn.healerCounter == null || postIn.healerCounter < 1)
                {
                    postIn.posterSpec.Add(spec);
                }
                return postIn.posterSpec;
            }
            catch (Exception Ex)
            {
                string localError = "Error while adding tag Healer identifier to post object: ";
                //serverLog.Error(localError + Ex.Message);
                throw new Exception("-- FPARSE006 " + localError + Ex.Message.ToString());
            }
        }

    }
}

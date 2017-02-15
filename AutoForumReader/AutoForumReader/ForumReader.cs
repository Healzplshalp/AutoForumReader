using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using HtmlAgilityPack;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using System.Xml.Linq;

namespace AutoForumReader
{
    /// <summary>
    /// This is the main firing class.  This class will call other classes and methods to process each forum post
    /// This method also handles all logging of errors and major events throughout execution of the program.  
    /// </summary>
    class ForumReader
    {
        GetAppSettings appSettings = new GetAppSettings();
        ForumParser parser = new ForumParser();
        Email sendEmail = new Email();
        Log_Win.Log serverLog = new Log_Win.Log();

        /// <summary>
        /// Initialize configuration file and load variables
        /// If all initialization events were successful launch the forum reader method that will
        /// process the forum posts.
        /// </summary>
        public void forumReaderInit()
        {
            try
            {
                appSettings.GetAllAppSettings();

                //Initialize server log
                serverLog.LogInit();
                if (serverLog.StrErrorMessage.Length != 0)
                {
                    string localError = "Error during runtime of Auto Forum Reader!: ";
                    throw new Exception("-- AFR00 " + localError);
                }
                serverLog.RunTime("AutoForumReader - Version: " + System.Reflection
                                                                     .Assembly.GetExecutingAssembly()
                                                                     .GetName().Version.ToString());
            }
            catch (Exception Ex)
            {
                string localError = "Error initializing Auto Forum Reader!: ";
                //serverLog.Error(localError + Ex.Message);
                throw new Exception("-- AFR00 " + localError + Ex.Message.ToString());
            }
            try
            {
                //Begin processing of forum posts
                serverLog.RunTime("Beginning polling websites");
                forumReader();
            }
            catch (Exception Ex)
            {
                string localError = "Error during runtime of Auto Forum Reader!: ";
                serverLog.Error(localError + Ex.Message);
                throw new Exception("-- AFR01 " + localError + Ex.Message.ToString());
            }
        }

        /// <summary>
        /// Main processing method.  Will open webpage, parse through html code to find nodes
        /// returns back forumPosts list of forumAttribute objects
        /// </summary>
        private void forumReader()
        {
            try
            {
                serverLog.RunTime("Initializing webclient, HTML handler, and ForumAttributes Object");
                WebClient web = new WebClient();
                HtmlDocument websiteHTML = new HtmlDocument();
                List<ForumPostAttributes> forumPosts = new List<ForumPostAttributes>();

                foreach (string website in appSettings.WebsiteURL)
                {
                    websiteHTML.LoadHtml(web.DownloadString(website));
                    serverLog.Info("Looking up website: " + website);

                    //Clear list per website
                    serverLog.Info("Clear forum posts object list");
                    forumPosts.Clear();

                    //Select all parent nodes with class=forumtopic
                    serverLog.RunTime("Querying for forum topic nodes");
                    HtmlNodeCollection collection = websiteHTML.DocumentNode
                                                               .SelectNodes(appSettings.ForumNodeQuery);

                    //Get title of main forum page ie: Stormrage
                    HtmlNode titleNode = websiteHTML.DocumentNode.SelectSingleNode(appSettings.ForumTitleQuery);

                    serverLog.RunTime("Checking for all posts");
                    //Retrieve all forums posts
                    forumPosts = ReadChildNode(collection,
                                               website,
                                               titleNode,
                                               websiteHTML,
                                               forumPosts);
                    serverLog.Info("Found: " + forumPosts.Count + " topics in forum.");

                    serverLog.RunTime("Checking for new posts");
                    //Check for new posts only
                    forumPosts = CheckForNewPosts(forumPosts);
                    serverLog.Info("Found: " + forumPosts.Count + " new topics in forum.");

                    serverLog.RunTime("Checking for prospective posts only");
                    //Parse through new post titles for potential recruits
                    forumPosts = parser.TitleParser(forumPosts);
                    serverLog.Info("Found: " + forumPosts.Count + " prospective topics in forum.");

                    forumPosts = parser.CheckSpec(forumPosts);

                    //If new posts with new potentials are found, send email with information
                    if (forumPosts.Count > 0)
                    {
                        serverLog.RunTime("Sending email... ");
                        sendEmail.SendEmail(forumPosts);
                        serverLog.Info("Email notification sent ");
                    }
                }
            }
            catch (Exception Ex)
            {
                string localError = "Error during runtime of Auto Forum Reader!: ";
                serverLog.Error(localError + Ex.Message);
                throw new Exception("-- AFR01 " + localError + Ex.Message.ToString());
            }
        }

        /// <summary>
        /// Reads through child nodes inside of topic nodes for properties.  
        /// Then sets properties to ForumPostAttributes object
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="website"></param>
        /// <param name="websiteHtml"></param>
        /// <param name="forumPosts"></param>
        /// <returns></returns>
        private List<ForumPostAttributes> ReadChildNode(HtmlNodeCollection collection, 
                                     string website,
                                     HtmlNode titleNode,
                                     HtmlDocument websiteHtml,
                                     List<ForumPostAttributes> forumPosts)
        {
            try
            {
                foreach (HtmlNode link in collection)
                {
                    //Parse each property

                    if (link.Attributes[appSettings.ForumIDQuery] == null)
                    { 
                        //Do not add topic if reference is null
                    }
                    else
                    {
                        string mainForumTitle = titleNode.InnerHtml;



                        string idString = link.Attributes[appSettings.ForumIDQuery].Value;
                        string idOnly = CleanIDString(idString);
                        string postWebsite = CleanWebsiteString(idOnly, website);

                        HtmlNode childNode = link.SelectSingleNode(appSettings.ChildNodeQuery);
                        string titleString = childNode.InnerHtml.ToString();
                        string cleanTitle = CleanString(titleString);

                        string tooltip = childNode.Attributes[appSettings.TooltipQuery].Value;

                        string cleanTooltip = CleanString(tooltip);

                        //TODO create parser to determine what spec a post is here


                        //Create object for post
                        ForumPostAttributes post = Post(website,
                                                        mainForumTitle,
                                                        postWebsite,
                                                        websiteHtml,
                                                        idOnly,
                                                        cleanTitle,
                                                        cleanTooltip);
                        //Add post to global list
                        forumPosts.Add(post);
                    }
                }
                return forumPosts;
            }
            catch (Exception Ex)
            {
                string localError = "Error during runtime, reading child nodes!: ";
                serverLog.Error(localError + Ex.Message);
                throw new Exception("-- AFR02 " + localError + Ex.Message.ToString());
            }
        }

        /// <summary>
        /// Checks a locally stored XML file for topic ID number.  If ID number
        /// alreay exists, remove entry from list of posts.  if ID number is not found
        /// keep post in list for processing.
        /// </summary>
        /// <param name="forumPosts"></param>
        /// <returns></returns>
        private List<ForumPostAttributes> CheckForNewPosts(List<ForumPostAttributes> forumPosts)
        {
            try
            {
                //If the XML file already exists, use the existing one
                if (File.Exists(appSettings.XMLDir))
                {
                    XElement element = XElement.Load(appSettings.XMLDir);
                    XDocument document = XDocument.Load(appSettings.XMLDir);

                    int readIndex = 0;
                    List<int> readFlag = new List<int>();

                    //Check each post forum ID number against XML document
                    foreach (ForumPostAttributes post in forumPosts)
                    {
                        Boolean isPostread = IsPostRead(post, element);

                        if (isPostread)
                        {
                            readFlag.Add(readIndex);
                        }
                        readIndex++;
                    }

                    //Remove any posts that are already in the XML document
                    int removeCounter = 0;
                    int iRemove = 0;
                    foreach (int removeIndex in readFlag)
                    {
                        iRemove = removeIndex - removeCounter;
                        forumPosts.RemoveAt(iRemove);
                        removeCounter++;
                    }

                    //Append new posts to the XML document
                    AppendRecordToXML(forumPosts, document);
                }
                //If XML document does not exist, create it and treat all posts as new
                else
                {
                    StreamWriter fileXML =
                        new StreamWriter(appSettings.XMLDir);

                    XmlSerializer writer =
                        new XmlSerializer(forumPosts.GetType());

                    writer.Serialize(fileXML, forumPosts);
                    fileXML.Close();
                }
                return forumPosts;
            }
            catch (Exception Ex)
            {
                string localError = "Error during runtime checking for new posts!: ";
                serverLog.Error(localError + Ex.Message);
                throw new Exception("-- AFR03 " + localError + Ex.Message.ToString());
            }
        }
        
        /// <summary>
        /// Appends XML file with new posts
        /// </summary>
        /// <param name="forumPosts"></param>
        /// <param name="document"></param>
        private void AppendRecordToXML(List<ForumPostAttributes> forumPosts, XDocument document)
        {
            try
            {
                //For each new post append to XML file
                foreach (ForumPostAttributes post in forumPosts)
                {
                    XElement forumPost = document.Element("ArrayOfForumPostAttributes");
                    forumPost.Add(new XElement("ForumPostAttributes",
                                  new XElement("website", post.website),
                                  new XElement("forumID", post.forumID),
                                  new XElement("forumTitle", post.forumTitle),
                                  new XElement("forumPreview", post.forumPreview)));

                }
                document.Save(appSettings.XMLDir);
            }
            catch (Exception Ex)
            {
                string localError = "Error during runtime, appending XML file!: ";
                serverLog.Error(localError + Ex.Message);
                throw new Exception("-- AFR04 " + localError + Ex.Message.ToString());
            }
        }

        /// <summary>
        /// Checks if a post was already read earlier by comparing the fourm ID number 
        /// to a locally stored XML file
        /// </summary>
        /// <param name="post"></param>
        /// <param name="element"></param>
        /// <returns></returns>
        private Boolean IsPostRead(ForumPostAttributes post, XElement element)
        {
            try
            {
                int queryHits = 0;

                //Select All elements of forumID
                var forumPosts =
                    from el in element.Elements("ForumPostAttributes")
                    where el.Element("forumID").Value == post.forumID
                    select el;

                //Check if ID already exists in XML
                foreach (XElement el in forumPosts)
                { queryHits++; }

                if (queryHits > 0)
                { return true; }
                else
                { return false; }
            }
            catch (Exception Ex)
            {
                string localError = "Error during runtime, checking if post was read!: ";
                serverLog.Error(localError + Ex.Message);
                throw new Exception("-- AFR05 " + localError + Ex.Message.ToString());
            }
        }

        /// <summary>
        /// Cleans up the ID number of the forum post
        /// </summary>
        /// <param name="idString"></param>
        /// <returns></returns>
        private string CleanIDString(string idString)
        {
            try
            {
                int indexOfID = idString.IndexOf("id");
                int indexOfComma = idString.IndexOf(",");
                int idLength = indexOfComma - indexOfID;
                string idOnly = idString.Substring((indexOfID + 4), (idLength - 4));

                return idOnly;
            }
            catch (Exception Ex)
            {
                string localError = "Error during runtime, cleaning forum ID number!: ";
                serverLog.Error(localError + Ex.Message);
                throw new Exception("-- AFR06 " + localError + Ex.Message.ToString());
            }
        }

        /// <summary>
        /// Creates the website string for the actual forum post and not the main forum site
        /// </summary>
        /// <param name="idString"></param>
        /// <param name="website"></param>
        /// <returns></returns>
        private string CleanWebsiteString(string idString, string website)
        {
            try
            {
                int indexOfWow = website.IndexOf("wow");
                int lengthOfWebsite = website.Length;
                string websiteToWow = website.Substring(0, (lengthOfWebsite - (lengthOfWebsite-(indexOfWow + 3))));
                websiteToWow = websiteToWow + "/topic/" + idString;

                return websiteToWow;
            }
            catch (Exception Ex)
            {
                string localError = "Error during runtime, cleaning website string!: ";
                serverLog.Error(localError + Ex.Message);
                throw new Exception("-- AFR09 " + localError + Ex.Message.ToString());
            }
        }

        /// <summary>
        /// Cleans up string of new line characters
        /// </summary>
        /// <param name="topicString"></param>
        /// <returns></returns>
        private string CleanString(string topicString)
        {
            try
            {
                //Remove newline
                string replacement = Regex.Replace(topicString, @"\t|\n|\r", "");
                //Decode HTML to plaintext
                replacement = System.Web.HttpUtility.HtmlDecode(replacement);
                //Remove other unwanted characters
                replacement = Regex.Replace(replacement, "[^0-9a-zA-z/\\\\:'# -]", "");
                return replacement;
            }
            catch (Exception Ex)
            {
                string localError = "Error during runtime, cleaning up newline and spaces!: ";
                serverLog.Error(localError + Ex.Message);
                throw new Exception("-- AFR07 " + localError + Ex.Message.ToString());
            }
        }

        /// <summary>
        /// Create post attributes object and populate its properties
        /// </summary>
        /// <param name="site"></param>
        /// <param name="websiteHtml"></param>
        /// <param name="id"></param>
        /// <param name="title"></param>
        /// <param name="tooltip"></param>
        /// <returns></returns>
        private ForumPostAttributes Post (string site,
                                          string forumTitle,
                                          string postWebsite,
                                          HtmlDocument websiteHtml, 
                                          string id, 
                                          string title, 
                                          string tooltip)
        {
            try
            {
                ForumPostAttributes newPost = new ForumPostAttributes()
                {
                    website = site,
                    mainForumTitle = forumTitle,
                    postSite = postWebsite,
                    //webPage = websiteHtml,
                    forumID = id,
                    forumTitle = title,
                    forumPreview = tooltip
                };
                return newPost;
            }
            catch (Exception Ex)
            {
                string localError = "Error during runtime, problem creating forumAttributes object!: ";
                serverLog.Error(localError + Ex.Message);
                throw new Exception("-- AFR08 " + localError + Ex.Message.ToString());
            }
        }

        /// <summary>
        /// This method call the class Log to Close the log file.  
        /// </summary>
        public void CloseLogFile()
        {
            serverLog.Close();

        }

    }
}

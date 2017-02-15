using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using DCSit;

namespace AutoForumReader
{
    class GetAppSettings
    {
        /// <summary>
        /// All fields for all application settings
        /// </summary>
        #region Fields

        private static List<string> websiteURLS;
        private static List<string> lfFilters;
        private static List<string> gFilters;
        private static List<string> tankFilters;
        private static List<string> dpsFilters;
        private static List<string> healsFilters;

        private static string forumNodeQuery;
        private static string childNodeQuery;
        private static string forumIDQuery;
        private static string tooltipQuery;
        private static string forumTitleQuery;
        private static string xmlDir;

        private static string emailFrom;
        private static string emailTo;
        private static string emailsubject;
        private static string emailPW;

        private static string serverLogLocation;
        private static string serverLogName;
        private static string serverLogType;
        private static string serverLogJobSW;
        private static string serverDebugTypeSW;

        #endregion 

        /// <summary>
        /// All getters for application 
        /// </summary>
        #region Getters

        public List<string> WebsiteURL
        { get { return websiteURLS; } }

        public List<string> LFFilters
        { get { return lfFilters; } }

        public List<string> GFilters
        { get { return gFilters; } }

        public List<string> TankFilters
        { get { return tankFilters; } }

        public List<string> DpsFilters
        { get { return dpsFilters; } }

        public List<string> HealsFilters
        { get { return healsFilters; } }

        public string ForumNodeQuery
        { get { return forumNodeQuery; } }

        public string ChildNodeQuery
        { get { return childNodeQuery; } }

        public string ForumIDQuery
        { get { return forumIDQuery; } }

        public string TooltipQuery
        { get { return tooltipQuery; } }

        public string ForumTitleQuery
        { get { return forumTitleQuery; } }

        public string XMLDir
        { get { return xmlDir; } }

        public string EmailFrom
        { get { return emailFrom; } }

        public string EmailPW
        { get { return emailPW; } }

        public string EmailTo
        { get { return emailTo; } }

        public string EmailSubject
        { get { return emailsubject; } }

        public string ServerLogLocation
        { get { return serverLogLocation; } }

        public string ServerLogName
        { get { return serverLogName; } }

        public string ServerLogType
        { get { return serverLogType; } }

        public string ServerLogJobSW
        { get { return serverLogJobSW; } }

        public string ServerLogDebugTypeSW
        { get { return serverDebugTypeSW; } }

        #endregion

        /// <summary>
        /// This is the initializer that gets called when the all applications settings are to be loaded in
        /// it returns all the fields above
        /// </summary>
        public void GetAllAppSettings()
        {
            try
            {
                websiteURLS = GetWebsiteURL();
                lfFilters = GetLFFilters();
                gFilters = GetGuildFilters();
                tankFilters = GetTankFilters();
                dpsFilters = GetDPSFilters();
                healsFilters = GetHealsFilters();
                forumNodeQuery = GetForumNodeQuery();
                childNodeQuery = GetChildNodeQuery();
                forumIDQuery = GetForumIDQuery();
                tooltipQuery = GetTooltipQuery();
                forumTitleQuery = GetForumTitleQuery();
                xmlDir = GetXMLDirectory();
                emailFrom = GetEmailFrom();
                emailTo = GetEmailTo();
                emailPW = GetEmailPW();
                emailsubject = GetEmailSubject();
                serverLogLocation = GetServerLogLocation();
                serverLogName = GetServerLogName();
                serverLogType = GetServerLogType();
                serverLogJobSW = GetServerLogJobSW();
                serverDebugTypeSW = GetServerLogDebugSW();
            }
            catch (Exception Ex)
            {
                string localError = "Problem reading application settings: ";
                throw new Exception("-- APS00 " + localError + Ex.Message.ToString());
            }
        }

        /// <summary>
        /// Gets all the websites that are to be parsed through.  This is a list
        /// </summary>
        /// <returns></returns>
        private static List<string> GetWebsiteURL()
        {

            List<string> listOfWebsites = new List<string>();

            try
            {
                var websites = System.Configuration.ConfigurationManager
                                              .GetSection("websites") 
                                              as NameValueCollection;

                foreach (string website in websites.AllKeys)
                {
                    listOfWebsites.Add(websites[website]);
                }

                if (listOfWebsites.Count < 1)
                {
                    throw new Exception("--APS023 website list is not populated in config file");
                }

                return listOfWebsites;
            }
            catch (Exception Ex)
            {
                string localError = "Encountered problem reading list of websites: ";
                throw new Exception("--APS0024 " + localError + Ex.Message.ToString());
            }
        }

        /// <summary>
        /// Gets all the filters for posts that are looking for a guild
        /// </summary>
        /// <returns></returns>
        private static List<string> GetLFFilters()
        {

            List<string> filters = new List<string>();

            try
            {
                var lfFilters = System.Configuration.ConfigurationManager
                                              .GetSection("lookingfilters")
                                              as NameValueCollection;

                foreach (string filter in lfFilters.AllKeys)
                {
                    filters.Add(lfFilters[filter]);
                }

                if (filters.Count < 1)
                {
                    throw new Exception("--APS091 looking for filters list is not populated in config file");
                }

                return filters;
            }
            catch (Exception Ex)
            {
                string localError = "Encountered problem reading list of filters for prospective players: ";
                throw new Exception("--APS0044 " + localError + Ex.Message.ToString());
            }
        }

        /// <summary>
        /// Gets all the filters that will be used to remove guilds looking for people posts
        /// </summary>
        /// <returns></returns>
        private static List<string> GetGuildFilters()
        {

            List<string> filters = new List<string>();

            try
            {
                var lfFilters = System.Configuration.ConfigurationManager
                                              .GetSection("guildfilters")
                                              as NameValueCollection;

                foreach (string filter in lfFilters.AllKeys)
                {
                    filters.Add(lfFilters[filter]);
                }

                if (filters.Count < 1)
                {
                    throw new Exception("--APS099 looking for guild filters list is not populated in config file");
                }

                return filters;
            }
            catch (Exception Ex)
            {
                string localError = "Encountered problem reading list of filters for ignoring guild posts: ";
                throw new Exception("--APS0084 " + localError + Ex.Message.ToString());
            }
        }

        /// <summary>
        /// Gets all the filters that will be used to look for posts that are related to tanks
        /// </summary>
        /// <returns></returns>
        private static List<string> GetTankFilters()
        {

            List<string> filters = new List<string>();

            try
            {
                var lfFilters = System.Configuration.ConfigurationManager
                                              .GetSection("tankfilters")
                                              as NameValueCollection;

                foreach (string filter in lfFilters.AllKeys)
                {
                    filters.Add(lfFilters[filter]);
                }

                if (filters.Count < 1)
                {
                    throw new Exception("--APS056 tank filters list is not populated in config file");
                }

                return filters;
            }
            catch (Exception Ex)
            {
                string localError = "Encountered problem reading list of filters for tagging tanks: ";
                throw new Exception("--APS0057 " + localError + Ex.Message.ToString());
            }
        }

        /// <summary>
        /// Gets all the filters that will be used to look for posts taht are related to DPS
        /// </summary>
        /// <returns></returns>
        private static List<string> GetDPSFilters()
        {

            List<string> filters = new List<string>();

            try
            {
                var lfFilters = System.Configuration.ConfigurationManager
                                              .GetSection("dpsfilters")
                                              as NameValueCollection;

                foreach (string filter in lfFilters.AllKeys)
                {
                    filters.Add(lfFilters[filter]);
                }

                if (filters.Count < 1)
                {
                    throw new Exception("--APS058 dps filters list is not populated in config file");
                }

                return filters;
            }
            catch (Exception Ex)
            {
                string localError = "Encountered problem reading list of filters for tagging dps: ";
                throw new Exception("--APS0059 " + localError + Ex.Message.ToString());
            }
        }

        /// <summary>
        /// Gets all the filters that will be used to look for posts that are related to Healers
        /// </summary>
        /// <returns></returns>
        private static List<string> GetHealsFilters()
        {

            List<string> filters = new List<string>();

            try
            {
                var lfFilters = System.Configuration.ConfigurationManager
                                              .GetSection("healsfilters")
                                              as NameValueCollection;

                foreach (string filter in lfFilters.AllKeys)
                {
                    filters.Add(lfFilters[filter]);
                }

                if (filters.Count < 1)
                {
                    throw new Exception("--APS060 heals filters list is not populated in config file");
                }

                return filters;
            }
            catch (Exception Ex)
            {
                string localError = "Encountered problem reading list of filters for tagging healers: ";
                throw new Exception("--APS0061 " + localError + Ex.Message.ToString());
            }
        }

        /// <summary>
        /// Gets all the setting in the config file that will be used as a key to distinguish nodes in the HTML page
        /// This is left configurable in the event that this application be used on more than just the Bnet forums
        /// </summary>
        /// <returns></returns>
        private static string GetForumNodeQuery()
        {
            string query;

            try
            {
                query = System.Configuration.ConfigurationManager
                                            .AppSettings["ForumNodeQuery"]
                                            .ToString();

                if (String.IsNullOrEmpty(query))
                {
                    throw new Exception("-- APS0004 Query for forum node is blank");
                }

                return query;
            }
            catch (Exception Ex)
            {
                string localError = "Encountered problem reading forum query from config file: ";
                throw new Exception("--APS0027 " + localError + Ex.Message.ToString());
            }
        }

        /// <summary>
        /// Gets the key for distinguishing child nodes
        /// This is left configurable in the event that this application be used on more than just the Bnet forums
        /// </summary>
        /// <returns></returns>
        private static string GetChildNodeQuery()
        {
            string query;

            try
            {
                query = System.Configuration.ConfigurationManager
                                            .AppSettings["ChildNodeQuery"]
                                            .ToString();

                if (String.IsNullOrEmpty(query))
                {
                    throw new Exception("-- APS0005 Query for child node is blank");
                }

                return query;
            }
            catch (Exception Ex)
            {
                string localError = "Encountered problem reading child query from config file: ";
                throw new Exception("--APS0028 " + localError + Ex.Message.ToString());
            }
        }

        /// <summary>
        /// Gets the key for finding the forum ID tag.  This is probably proprietary to Bnet, but is also left configurable
        /// </summary>
        /// <returns></returns>
        private static string GetForumIDQuery()
        {
            string query;

            try
            {
                query = System.Configuration.ConfigurationManager
                                            .AppSettings["ForumIDQuery"]
                                            .ToString();

                if (String.IsNullOrEmpty(query))
                {
                    throw new Exception("-- APS0006 Query for forum ID node is blank");
                }

                return query;
            }
            catch (Exception Ex)
            {
                string localError = "Encountered problem reading forum ID query from config file: ";
                throw new Exception("--APS0029 " + localError + Ex.Message.ToString());
            }
        }

        /// <summary>
        /// Gets the key for finding the forum tooltip tag.  This is probably proprietary to Bnet, but is also left configurable
        /// </summary>
        /// <returns></returns>
        private static string GetTooltipQuery()
        {
            string query;

            try
            {
                query = System.Configuration.ConfigurationManager
                                            .AppSettings["ToolTipQuery"]
                                            .ToString();

                if (String.IsNullOrEmpty(query))
                {
                    throw new Exception("-- APS0007 Query for forum ID node is blank");
                }

                return query;
            }
            catch (Exception Ex)
            {
                string localError = "Encountered problem reading forum ID query from config file: ";
                throw new Exception("--APS0030 " + localError + Ex.Message.ToString());
            }
        }

        /// <summary>
        /// Gets the key for finding the forum title tag.  This is probably proprietary to Bnet, but is also left configurable
        /// </summary>
        /// <returns></returns>
        private static string GetForumTitleQuery()
        {
            string query;

            try
            {
                query = System.Configuration.ConfigurationManager
                                            .AppSettings["ForumTitle"]
                                            .ToString();

                if (String.IsNullOrEmpty(query))
                {
                    throw new Exception("-- APS0337 Query for forum title node is blank");
                }

                return query;
            }
            catch (Exception Ex)
            {
                string localError = "Encountered problem reading forum title query from config file: ";
                throw new Exception("--APS0338 " + localError + Ex.Message.ToString());
            }
        }

        /// <summary>
        /// Gets the path of the XML file that is used as a flat database file for saving all posts that have already been read
        /// </summary>
        /// <returns></returns>
        private static string GetXMLDirectory()
        {
            string Dir;

            try
            {
                Dir = System.Configuration.ConfigurationManager
                                            .AppSettings["XMLDir"]
                                            .ToString();

                if (String.IsNullOrEmpty(Dir))
                {
                    throw new Exception("-- APS0008 Directory for XML forum file is blank");
                }

                return Dir;
            }
            catch (Exception Ex)
            {
                string localError = "Encountered problem reading XML directory from config file: ";
                throw new Exception("--APS0031 " + localError + Ex.Message.ToString());
            }
        }

        /// <summary>
        /// Get the sender's email address.  This can be an encrypted field
        /// </summary>
        /// <returns></returns>
        private static string GetEmailFrom()
        {
            string emailParam;

            try
            {
                emailParam = System.Configuration.ConfigurationManager
                                            .AppSettings["EmailFrom"]
                                            .ToString();

                if (String.IsNullOrEmpty(emailParam))
                {
                    throw new Exception("-- APS0118 No Email From Set");
                }
                if (emailParam.StartsWith("^"))
                {
                    SitDecoder sit = new SitDecoder();
                    emailParam = sit.Decrypt(emailParam).ToString();
                    return emailParam;
                }
                else
                {
                    return emailParam;
                }
            }
            catch (Exception Ex)
            {
                string localError = "Encountered problem reading email sender from config file: ";
                throw new Exception("--APS0032 " + localError + Ex.Message.ToString());
            }
        }

        /// <summary>
        /// Get the recipient's email address
        /// </summary>
        /// <returns></returns>
        private static string GetEmailTo()
        {
            string emailParam;

            try
            {
                emailParam = System.Configuration.ConfigurationManager
                                            .AppSettings["EmailTo"]
                                            .ToString();

                if (String.IsNullOrEmpty(emailParam))
                {
                    throw new Exception("-- APS0128 No Email To Set");
                }
                if (emailParam.StartsWith("^"))
                {
                    SitDecoder sit = new SitDecoder();
                    emailParam = sit.Decrypt(emailParam).ToString();
                    return emailParam;
                }
                else
                {
                    return emailParam;
                }
            }
            catch (Exception Ex)
            {
                string localError = "Encountered problem reading email recipient from config file: ";
                throw new Exception("--APS0033 " + localError + Ex.Message.ToString());
            }
        }

        /// <summary>
        /// Get the password for the sender's email account.  
        /// This can be encrypted
        /// </summary>
        /// <returns></returns>
        private static string GetEmailPW()
        {
            string emailParam;

            try
            {
                emailParam = System.Configuration.ConfigurationManager
                                            .AppSettings["EmailPW"]
                                            .ToString();

                if (String.IsNullOrEmpty(emailParam))
                {
                    throw new Exception("-- APS0138 No Email Password Set");
                }
                if (emailParam.StartsWith("^"))
                {
                    SitDecoder sit = new SitDecoder();
                    emailParam = sit.Decrypt(emailParam).ToString();
                    return emailParam;
                }
                else
                {
                    return emailParam;
                }
            }
            catch (Exception Ex)
            {
                string localError = "Encountered problem reading email password from config file: ";
                throw new Exception("--APS0033 " + localError + Ex.Message.ToString());
            }
        }

        /// <summary>
        /// Get the subject verbiage for the email
        /// </summary>
        /// <returns></returns>
        private static string GetEmailSubject()
        {
            string emailParam;

            try
            {
                emailParam = System.Configuration.ConfigurationManager
                                            .AppSettings["EmailSubject"]
                                            .ToString();

                if (String.IsNullOrEmpty(emailParam))
                {
                    throw new Exception("-- APS0134 Email Subject Set");
                }
                return emailParam;
            }
            catch (Exception Ex)
            {
                string localError = "Encountered problem reading email subject from config file: ";
                throw new Exception("--APS0063 " + localError + Ex.Message.ToString());
            }
        }

        /// <summary>
        /// Get the location of the server logs
        /// </summary>
        /// <returns></returns>
        private static string GetServerLogLocation()
        {
            string serverLogParam;

            try
            {
                serverLogParam = System.Configuration.ConfigurationManager
                                            .AppSettings["LogFolder"]
                                            .ToString();

                if (String.IsNullOrEmpty(serverLogParam))
                {
                    throw new Exception("-- APS0187 Server log location not set");
                }
                return serverLogParam;
            }
            catch (Exception Ex)
            {
                string localError = "Encountered problem reading server log location from config file: ";
                throw new Exception("--APS1063 " + localError + Ex.Message.ToString());
            }
        }

        /// <summary>
        /// Get the name of the server logs
        /// </summary>
        /// <returns></returns>
        private static string GetServerLogName()
        {
            string serverLogParam;

            try
            {
                serverLogParam = System.Configuration.ConfigurationManager
                                            .AppSettings["LogName"]
                                            .ToString();

                if (String.IsNullOrEmpty(serverLogParam))
                {
                    throw new Exception("-- APS1187 Server log name not set");
                }
                return serverLogParam;
            }
            catch (Exception Ex)
            {
                string localError = "Encountered problem reading server log name from config file: ";
                throw new Exception("--APS1163 " + localError + Ex.Message.ToString());
            }
        }

        /// <summary>
        /// Get the extension type of the server logs, ie .log or .txt
        /// </summary>
        /// <returns></returns>
        private static string GetServerLogType()
        {
            string serverLogParam;

            try
            {
                serverLogParam = System.Configuration.ConfigurationManager
                                            .AppSettings["LogFileExtention"]
                                            .ToString();

                if (String.IsNullOrEmpty(serverLogParam))
                {
                    throw new Exception("-- APS1287 Server log type not set");
                }
                return serverLogParam;
            }
            catch (Exception Ex)
            {
                string localError = "Encountered problem reading server log type from config file: ";
                throw new Exception("--APS1263 " + localError + Ex.Message.ToString());
            }
        }

        /// <summary>
        /// Get whether or not the switch for logging INFO type events is on or off
        /// </summary>
        /// <returns></returns>
        private static string GetServerLogJobSW()
        {
            string serverLogParam;

            try
            {
                serverLogParam = System.Configuration.ConfigurationManager
                                            .AppSettings["LogJob"]
                                            .ToString();

                if (String.IsNullOrEmpty(serverLogParam))
                {
                    throw new Exception("-- APS1387 Server log job switch not set");
                }
                return serverLogParam;
            }
            catch (Exception Ex)
            {
                string localError = "Encountered problem reading server log job switch from config file: ";
                throw new Exception("--APS1363 " + localError + Ex.Message.ToString());
            }
        }

        /// <summary>
        /// Get whether or not the switch for logging ERROR type events is on or off
        /// </summary>
        /// <returns></returns>
        private static string GetServerLogDebugSW()
        {
            string serverLogParam;

            try
            {
                serverLogParam = System.Configuration.ConfigurationManager
                                            .AppSettings["LogTypeDebug"]
                                            .ToString();

                if (String.IsNullOrEmpty(serverLogParam))
                {
                    throw new Exception("-- APS1487 Server log job debug switch not set");
                }
                return serverLogParam;
            }
            catch (Exception Ex)
            {
                string localError = "Encountered problem reading server log debug switch from config file: ";
                throw new Exception("--APS1463 " + localError + Ex.Message.ToString());
            }
        }
    }
}

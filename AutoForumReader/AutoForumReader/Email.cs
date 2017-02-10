using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;

namespace AutoForumReader
{
    class Email
    {
        GetAppSettings appSettings = new GetAppSettings();

        /// <summary>
        /// Send Email will send an email for each new post that contains a prospective raider
        /// </summary>
        /// <param name="forumPosts"></param>
        public void SendEmail(List<ForumPostAttributes> forumPosts)
        {
            try
            {
                foreach (ForumPostAttributes post in forumPosts)
                {
                    Emailer(post);
                }
            }
            catch (Exception Ex)
            {
                string localError = "Error while sending email!: ";
                //serverLog.Error(localError + Ex.Message);
                throw new Exception("-- EMAIL001 " + localError + Ex.Message.ToString());
            }
        }

        /// <summary>
        /// Emailer will actually send the email with the information from post
        /// </summary>
        /// <param name="post"></param>
        private void Emailer(ForumPostAttributes post)
        {
            MailMessage msg = new MailMessage();

            msg.From = new MailAddress(appSettings.EmailFrom);
            msg.To.Add(appSettings.EmailTo);
            msg.Subject = appSettings.EmailSubject + post.forumTitle;
            msg.Body = post.forumPreview + "\n \n"
                     + "Post from: " + post.postSite + "\n \n"
                     + "#" + post.mainForumTitle + "\n"
                     + post.posterSpec;

            SmtpClient client = new SmtpClient();
            client.UseDefaultCredentials = true;
            client.Host = "smtp.gmail.com";
            client.Port = 587;
            client.EnableSsl = true;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.Credentials = new NetworkCredential(appSettings.EmailFrom, appSettings.EmailPW);
            client.Timeout = 20000;
            try
            {
                client.Send(msg);
            }
            catch (Exception Ex)
            {
                string localError = "Encountered a problem sending email: ";
                throw new Exception("--EMAIL0000 " + localError + Ex.Message.ToString());
            }
            finally
            {
                msg.Dispose();
            }
        }
    }
}

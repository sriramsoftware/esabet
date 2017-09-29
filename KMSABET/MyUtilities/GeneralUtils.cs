using KMSABET.MyDaos;
using KMSABET.MyPocos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace KMSABET.MyUtilities
{
    public class GeneralUtils
    {
        public List<int> getCharacterOccurancesInString(String inputStr,  Char inputChar)
        {
            var foundIndexes = new List<int>();

            for (int i = inputStr.IndexOf(inputChar); i > -1; i = inputStr.IndexOf(inputChar, i + 1))
            {
                // for loop end when i=-1 ('a' not found)
                foundIndexes.Add(i);
            }

            return foundIndexes;
        }

        public List<int> getEndDifferenceValue(List<int> firstList, List<int> secondList)
        {
            var differenceValueList = new List<int>();
            for (int i = 0; i < firstList.Count; i++)
            {
                differenceValueList.Add(secondList[i] - firstList[i]);
            }
            return differenceValueList;
        }

        public List<String> getJSONStringAskUser(String inputStr, List<int> firstIndexList, List<int> endIndexList)
        {
            List<String> jSONStringList = new List<string>();
            for (int i = 0; i < firstIndexList.Count; i++)
            {
                jSONStringList.Add(inputStr.Substring(firstIndexList[i], endIndexList[i] + 1));
            }
            return jSONStringList;
        }

        public void sendEmail(AppEmailData emailDataObj)
        {
            try
            {
                AppDao appDaoObj = new AppDao();
                AppEmailConfiguration emailConfigObj = appDaoObj.getEmailConfiguration();
                SmtpClient SmtpServer = new SmtpClient(emailConfigObj.smtpHost);
                var mail = new System.Net.Mail.MailMessage();
                mail.From = new MailAddress(emailConfigObj.fromAddress);
                mail.To.Add(emailDataObj.toAddress);
                if (emailDataObj.ccAddress != null && !emailDataObj.ccAddress.Equals(""))
                {
                    mail.CC.Add(emailDataObj.ccAddress);
                }
                mail.Subject = emailDataObj.subject;
                mail.IsBodyHtml = true;
                string htmlBody;
                htmlBody = emailDataObj.bodyHtml;
                mail.Body = htmlBody;
                SmtpServer.UseDefaultCredentials = false;
                SmtpServer.Credentials = new System.Net.NetworkCredential(emailConfigObj.fromAddress, emailConfigObj.fromPassword);
                SmtpServer.EnableSsl = true;
                SmtpServer.Send(mail);
            }
            catch (Exception ex)
            {
                LogUtils.myLog.Info("Email while sending error : ", ex);
            }
        }

    }
}
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LL.mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LL.mail.Tests
{
    [TestClass()]
    public class MailTests
    {
        private Mail CreateMail()
        {
            return new Mail();
        }

        [TestMethod()]
        public void SendMailTest()
        {
            // Arrange
            var mail = this.CreateMail();
            string Host = "127.0.0.1";
            int Port = 1025;

            // Act
            try
            {
                var result = mail.mailConfig(
                               Host,
                               Port);
                MailModel model = new MailModel();
                model.FROM = "Salman <kinsta@mailhog.local>";
                model.TO = new List<string>() { "Test <test@mailhog.local>" };
                model.SUBJECT = "ok";
                model.BODY = "body";
                result.IsHtml(true);
                result.SendMail(model);
                Assert.AreEqual(1, 1);
            }
            catch (Exception e)
            {
                Assert.Fail();

            }

        }
    }
}
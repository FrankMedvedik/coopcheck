// <copyright file="EmailSvcTest.cs">Copyright ©  2015</copyright>
using System;
using CoopCheck.WPF.Services;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CoopCheck.WPF.Services.Tests
{
    /// <summary>This class contains parameterized unit tests for EmailSvc</summary>
    [PexClass(typeof(EmailSvc))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [TestClass]
    public partial class EmailSvcTest
    {
        /// <summary>Test stub for CreateOutlookEmail()</summary>
        [PexMethod]
        public void CreateOutlookEmailTest()
        {
            EmailSvc.CreateOutlookEmail();
            // TODO: add assertions to method EmailSvcTest.CreateOutlookEmailTest()
        }

        /// <summary>Test stub for SendEMailThroughOutlook()</summary>
        [PexMethod]
        public void SendEMailThroughOutlookTest()
        {
            EmailSvc.SendEMailThroughOutlook();
            // TODO: add assertions to method EmailSvcTest.SendEMailThroughOutlookTest()
        }
    }
}

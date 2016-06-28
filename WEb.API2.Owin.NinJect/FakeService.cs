using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEb.API2.Owin.NinJect
{
    public class FakeService : IFakeService
    {
        public bool GetAnswer(string question)
        {
            return question.Contains("AAA");
        }
    }
    public interface IFakeService
    {
        bool GetAnswer(string question);
    }
}
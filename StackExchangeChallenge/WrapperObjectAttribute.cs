using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2
{

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class WrapperObjectAttribute : Attribute
    {
        public WrapperObjectAttribute(string wrapperObject)
        {
            this.WrapperObject = wrapperObject;
        }

        public string WrapperObject { get; set; }
    }
}
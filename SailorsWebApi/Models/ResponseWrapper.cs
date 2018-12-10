using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SailorsWebApi.Models
{
    public class ResponseWrapper<T>
    {
        public T Result { get; set; }

        public bool ResultType { get; set; }

        public ResponseWrapper(T result, bool type)
        {
            Result = result;
            ResultType = type;
        }
    }
}
﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MiniMvc
{
    public interface IHttpRequestFeature
    {
        Uri Url { get; }

        NameValueCollection Headers { get; }

        Stream Body { get; }
    }
}

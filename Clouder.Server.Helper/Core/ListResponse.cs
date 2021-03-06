﻿using System;
using System.Collections.Generic;

namespace Clouder.Server.Helper.Core
{
    public class ListResponse<T>
    {
        public List<T> Result { get; set; } = new List<T>();
        public string ContinuationToken { get; set; }
        public bool HasMore { get; set; }
    }
}

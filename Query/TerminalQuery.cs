﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CuriousGremlin.Query
{
    public class TerminalQuery : GraphQuery
    {
        internal TerminalQuery(string query) : base(query) { }
    }
}

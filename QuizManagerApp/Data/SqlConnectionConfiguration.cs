﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizManagerApp.Data
{
    public class SqlConnectionConfiguration
    {
        public SqlConnectionConfiguration(string value) => Value = value;
        public string Value { get; }
    }
}

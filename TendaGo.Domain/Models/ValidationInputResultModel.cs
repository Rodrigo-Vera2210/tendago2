﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace TendaGo.Api.Models
{
    public class ValidationInputResultModel
    {
       
        public bool IsValid { get; set; }
        public string Observations { get; set; }
        public int IdProduct { get; set; }
        public int IdLocal { get; set; }
        public int IdUnitType { get; set; }
    }
}
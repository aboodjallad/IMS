﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Applecation
{
    internal interface iAuth
    {
        bool Register(string username, string password);
        int Login(string username, string password);
        void Logout();
    }
}

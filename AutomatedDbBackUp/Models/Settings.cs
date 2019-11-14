using System;
using System.Collections.Generic;
using System.Text;

namespace AutomatedDbBackUp.Models
{
    public class Settings
    {
        public string MasterConnString { get; set; }
        public string BlobConnString { get; set; }

    }
}

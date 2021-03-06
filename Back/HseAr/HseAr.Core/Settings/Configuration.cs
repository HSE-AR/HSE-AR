﻿using System;
using System.Collections.Generic;

namespace HseAr.Core.Settings
{
    public class Configuration
    {
        public ConnectionStrings ConnectionStrings { get; set; }
        
        public string STORAGE_PATH { get; set; }
        
        public ModelsDatabaseSettings ModelsDatabaseSettings { get; set; }

        public string Issuer { get; set; }

        public string Audience { get; set; }

        public string Key { get; set; }

        public string SuperAdminPassword { get; set; }

        public string SuperAdminEmail { get; set; }
        
        public string TestUserPassword { get; set; }
        
        public string TestUserEmail { get; set; }

        public List<ArClientsConfig> ArClients { get; set; }
        
        public string SceneExportAccessToken { get; set; }
    }

    public class ConnectionStrings
    {
        public string DataAccessPostgreSqlProvider { get; set; }
    }
    
    public class ArClientsConfig
    {
        public Guid Id { get; set; }
        
        public ArClientEnum Type { get; set; }
        
        public string Url { get; set; }
        
        public Guid UserId { get; set; }
    }

    public enum ArClientEnum
    {
        WebAr,
        MiemArApp
    }
}
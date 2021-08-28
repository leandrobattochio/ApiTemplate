using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Template.Infra
{
    /// <summary>
    /// Classe representa as configurações do JWT do appsettings.json
    /// </summary>
    public class JwtSettings
    {
        public string Key { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }

        public JwtSettings(string key, string issuer, string audience)
        {
            Key = key;
            Issuer = issuer;
            Audience = audience;
        }

        public JwtSettings()
        {

        }
    }
}

﻿namespace MTKPM_QuanLyKhachSan.Models
{
    public class PaypalSettings
    {
        public string Mode { get; set; }
        public int ConnectionTimeout { get; set; }
        public int RequestRetries { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
    }
}

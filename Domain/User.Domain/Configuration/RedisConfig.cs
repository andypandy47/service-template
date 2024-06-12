using System.ComponentModel.DataAnnotations;

namespace User.Domain.Configuration;

public class RedisConfig
{
    [Required] public string Password { get; set; }
    [Required] public string Host { get; set; }
    [Required] public string Port { get; set; }
    public bool SslActive { get; set; }
}
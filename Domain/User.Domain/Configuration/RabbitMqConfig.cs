using System.ComponentModel.DataAnnotations;

namespace User.Domain.Configuration;

public class RabbitMqConfig
{
    [Required] public string Host { get; set; }
    [Required]public string Port { get; set; }
    [Required]public string VirtualHost { get; set; }
    [Required]public string Username { get; set; }
    [Required]public string Password { get; set; }
    public string SslThumbPrint { get; set; }
    public bool SslActive { get; set; }
}
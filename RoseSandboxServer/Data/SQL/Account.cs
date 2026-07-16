using LinqToDB.Mapping;
using System;

namespace RoseSandboxServer.Data.SQL
{
    [Table(Name = "accounts")]
    public class Account
    {
        [PrimaryKey]
        [Column(Name = "id"), NotNull]
        public Guid Id { get; set; }

        [Column(Name = "username"), NotNull]
        public string Username { get; set; }

        [Column(Name = "email"), NotNull]
        public string Email { get; set; }

        [Column(Name = "password_hash"), NotNull]
        public string PasswordHash { get; set; }

        [Column(Name = "created_at")]
        public DateTime CreatedAt { get; set; }

        [Column(Name = "last_login")]
        public DateTime? LastLogin { get; set; }

        [Column(Name = "is_banned")]
        public bool IsBanned { get; set; }

        [Column(Name = "ban_reason")]
        public string BanReason { get; set; }

        [Column(Name = "access_level")]
        public int AccessLevel { get; set; }
    }
}

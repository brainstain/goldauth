using System;

namespace Goldstein.Authentication.Domain
{
    public class IdentityUser
    {
        public string UserName { get; set; }
        public string Email { get; private set; }
        public DateTime LastLogin { get; set; }
        public bool IsActive { get; set; }
        public bool IsLocked { get; set; }
        public int LoginAttemptCount { get; set; }
        public byte[] PasswordHash { get; private set; }
        public byte[] Salt { get; private set; }

        public IdentityUser()
        {
            IsActive = true;
        }

        public IdentityUser(string userName, string email) : this()
        {
            UserName = userName;
            Email = email;
        }

        public IdentityUser(string userName, string email, byte[] passwordHash, byte[] salt, bool isActive = true)
            : this(userName, email)
        {
            PasswordHash = passwordHash;
            Salt = salt;
            IsActive = isActive;
        }
    }
}

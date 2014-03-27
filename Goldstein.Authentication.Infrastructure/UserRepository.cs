using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Goldstein.Authentication.Domain;

namespace Goldstein.Authentication.Infrastructure
{
    public class UserRepository : IUserRepository
    {
        private readonly string _connection;
        private const string AddUserSP = "[dbo].[AddUserAsync]";
        private const string UpdateUserSP = "[dbo].[UpdateUserByUserName]";
        private const string GetUserSP = "[dbo].[GetUserByUserName]";
 
        public UserRepository()
        {
            _connection = ConfigurationManager.ConnectionStrings["goldsteinAuth"].ConnectionString;
        }

        public async Task<IdentityUser> GetUserByUsernameAsync(string username)
        {
            using (var connection = new SqlConnection(_connection))
            {
                try
                {
                    connection.Open();
                    var command = new SqlCommand()
                        {
                            CommandText = GetUserSP,
                            Connection = connection,
                            CommandType = CommandType.StoredProcedure
                        };
                    command.Parameters.AddRange(new[]
                        {
                            new SqlParameter("@SearchUserName", username),
                            new SqlParameter("@UserName", SqlDbType.VarChar, 50) {Direction = ParameterDirection.Output},
                            new SqlParameter("@Email", SqlDbType.VarChar, 100) {Direction = ParameterDirection.Output},
                            new SqlParameter("@LastLogin", SqlDbType.DateTime) {Direction = ParameterDirection.Output},
                            new SqlParameter("@IsActive", SqlDbType.Bit) {Direction = ParameterDirection.Output},
                            new SqlParameter("@IsLocked", SqlDbType.Bit) {Direction = ParameterDirection.Output},
                            new SqlParameter("@LoginAttemptCount", SqlDbType.Int){Direction = ParameterDirection.Output},
                            new SqlParameter("@PasswordHash", SqlDbType.VarBinary, 64){Direction = ParameterDirection.Output},
                            new SqlParameter("@Salt", SqlDbType.VarBinary, 64) {Direction = ParameterDirection.Output}
                        });
                    await command.ExecuteNonQueryAsync();

                    return ParametersToIdentityUser(command.Parameters);
                }
                catch (Exception e)
                {
                    //TODO: Log error
                    return null;
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public async Task UpdateUserAsync(IdentityUser user)
        {
            using (var connection = new SqlConnection(_connection))
            {
                try
                {
                    connection.Open();
                    var command = new SqlCommand()
                        {
                            CommandText = UpdateUserSP,
                            Connection = connection,
                            CommandType = CommandType.StoredProcedure
                        };
                    command.Parameters.AddRange(new[]
                        {
                            new SqlParameter("@UserName", user.UserName),
                            new SqlParameter("@Email", user.Email),
                            new SqlParameter("@LastLogin", user.LastLogin),
                            new SqlParameter("@IsActive", user.IsActive),
                            new SqlParameter("@IsLocked", user.IsLocked),
                            new SqlParameter("@LoginAttemptCount", user.LoginAttemptCount),
                            new SqlParameter("@PasswordHash", user.PasswordHash),
                            new SqlParameter("@Salt", user.Salt)
                        });
                    await command.ExecuteNonQueryAsync();
                }
                catch (Exception e)
                {
                    //TODO: Log error
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public async Task AddUserAsync(IdentityUser user)
        {
            using (var connection = new SqlConnection(_connection))
            {
                try
                {
                    connection.Open();
                    var command = new SqlCommand()
                    {
                        CommandText = AddUserSP,
                        Connection = connection,
                        CommandType = CommandType.StoredProcedure
                    };
                    command.Parameters.AddRange(new[]
                        {
                            new SqlParameter("@UserName", user.UserName),
                            new SqlParameter("@Email", user.Email),
                            new SqlParameter("@LastLogin", user.LastLogin),
                            new SqlParameter("@IsActive", user.IsActive),
                            new SqlParameter("@IsLocked", user.IsLocked),
                            new SqlParameter("@LoginAttemptCount", user.LoginAttemptCount),
                            new SqlParameter("@PasswordHash", user.PasswordHash),
                            new SqlParameter("@Salt", user.Salt)
                        });
                    await command.ExecuteNonQueryAsync();
                }
                catch (Exception e)
                {
                    //TODO: Log error
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public Task SetUserForPasswordResetAsync(string username, char[] resetToken)
        {
            throw new NotImplementedException();
        }

        public Task<char[]> GetPasswordResetTokenAsync(string username)
        {
            throw new NotImplementedException();
        }

        private IdentityUser ParametersToIdentityUser(SqlParameterCollection collection)
        {
            var name = collection["@UserName"].Value as string;
            if (name == null)
            {
                return null;
            }
            var email = collection["@Email"].Value as string;
            var lastLogin = collection["@LastLogin"].Value as DateTime?;
            var isActive = collection["@IsActive"].Value as bool?;
            var isLocked = collection["@IsLocked"].Value as bool?;
            var loginAttemptCount = collection["@LoginAttemptCount"].Value as int?;
            var passwordHash = collection["@PasswordHash"].Value as byte[];
            var salt = collection["@Salt"].Value as byte[];
            return new IdentityUser(name, email, passwordHash, salt)
                {
                    LastLogin = lastLogin ?? DateTime.MinValue,
                    IsActive = isActive ?? true,
                    IsLocked = isLocked ?? false,
                    LoginAttemptCount = loginAttemptCount ?? 0
                };
        }
    }
}

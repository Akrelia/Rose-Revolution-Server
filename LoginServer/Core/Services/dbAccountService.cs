using RevolutionCore.SQL;
using System;
using System.Threading.Tasks;

public class dbAccountService
{
    private readonly Database _database;

    public dbAccountService(Database database)
    {
        _database = database;
    }

    public async Task<User> GetUserAsync(string username)
    {
        Console.WriteLine(username.Length);
        string query = "SELECT username, password, data_s AS Salt FROM accounts WHERE username = @Username";

        return await _database.QuerySingleOrDefaultAsync<User>(query, new { Username = username });
    }

    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
    }
}
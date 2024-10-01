using BCrypt.Net;

public class PasswordManager
{
    // Hasher un mot de passe
    public string HashPassword(string password)
    {
        int saltRounds = 10;
        string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password, saltRounds);
        return hashedPassword;
    }

    // Vérifier un mot de passe avec son hash
    public bool VerifyPassword(string password, string hash)
    {
        return BCrypt.Net.BCrypt.Verify(password, hash);
    }
}
namespace Edu_System_BackEnd.Edu_System_BackEnd.Core.Utils
{
    public class PasswordValidator
    {
        public static bool IsValidPassword(string password)
        {
            // Check if the password is at least 8 characters long
            if (password.Length < 8)
                return false;
            // Check if the password contains at least one uppercase letter
            if (!password.Any(char.IsUpper))
                return false;
            // Check if the password contains at least one lowercase letter
            if (!password.Any(char.IsLower))
                return false;
            // Check if the password contains at least one digit
            if (!password.Any(char.IsDigit))
                return false;
            // Check if the password contains at least one special character
            string specialCharacters = "!@#$%^&*()_+[]{}|;:,.<>?";
            if (!password.Any(c => specialCharacters.Contains(c)))
                return false;
            return true;
        }
    }
}

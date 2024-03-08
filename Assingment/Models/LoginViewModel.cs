namespace Assingment.Models
{
    public class LoginViewModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public interface IUserRepository
    {
        User GetUserByUsernameAndPassword(string username, string password);
    }

    public class UserRepository : IUserRepository
    {
        private readonly List<User> _users;

        public UserRepository()
        {
            _users = new List<User>
            {
                new User { Username = "yusra", Password = "123" },
                new User { Username = "noura", Password = "112" },
                // Add more users as needed
            };
        }

        public User GetUserByUsernameAndPassword(string username, string password)
        {
            return _users.FirstOrDefault(u => u.Username == username && u.Password == password);
        }
    }

    public class User
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
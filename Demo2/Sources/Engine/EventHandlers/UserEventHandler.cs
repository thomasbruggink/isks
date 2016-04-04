using Business.Repositories.Users;

namespace Engine.EventHandlers
{
    public class UserEventHandler : IEventHandler
    {
        private readonly IUserRepository _userRepository;

        public UserEventHandler()
        {
            _userRepository = new UserRepository();
        }

        public void Handle(string key, dynamic data)
        {
            switch (key)
            {
                case "userCreated":
                {
                    UserCreated(data);
                    break;
                }
            }
        }

        private void UserCreated(dynamic data)
        {
            var name = (string) data.name;

            _userRepository.CreateUser(name);
        }
    }
}
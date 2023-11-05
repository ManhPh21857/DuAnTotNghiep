using Project.Core.Domain.Login;
using Project.Core.Domain.User;

namespace Project.Core.Domain
{
    public interface ISessionInfo
    {
        public UserId UserId { get; }
        public SessionId SessionId { get; }
    }
}

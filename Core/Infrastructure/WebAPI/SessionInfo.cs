using Project.Core.Domain;
using Project.Core.Domain.Login;
using Project.Core.Domain.User;

namespace Project.Core.Infrastructure.WebAPI
{
    internal class SessionInfo : ISessionInfo
    {
        public UserId UserId { get; private set; }
        public SessionId SessionId { get; private set; }

        public void Initialize(UserId userId, SessionId sessionId)
        {
            this.UserId = userId;
            this.SessionId = sessionId;
        }
    }
}

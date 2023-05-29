using Chat.Domain.Common;
using NetDevPack.Domain;

namespace Chat.Domain.Models
{
    public class DirectUser : BaseModel<int>, IAggregateRoot
    {
        public DirectUser(int userId, int directId)
        {
            //DestinationUserId = destinationUserId;
            UserId = userId;
            DirectId = directId;
            Show = true;
        }

        public bool Show { get; private set; }
        public int UserId { get; private set; }
        public int DirectId { get; private set; }
        public virtual User.User User { get; private set; }

        public virtual Direct Direct { get; private set; }

        public static DirectUser Create(int userId, int directId)
        {
            return new DirectUser(userId, directId);
        }
    }
}
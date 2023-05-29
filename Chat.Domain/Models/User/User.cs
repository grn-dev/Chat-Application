using System;
using System.Collections.Generic;
using Chat.Domain.Common;
using NetDevPack.Domain;

namespace Chat.Domain.Models.User
{
    public class User : BaseModel<int>, IAggregateRoot
    {
        private User(Guid userId,
            string userName,
            string firstName,
            string lastName,
            string nationalCode,
            string mobileNumber,
            Gender? gender,
            DateTime? birthDate,
            string password,
            DateTime lastActivity,
            ChatUserStatus status,
            string email,
            bool isBanned
        )
        {
            UserId = userId;
            UserName = userName;
            FirstName = firstName;
            LastName = lastName;
            NationalCode = nationalCode;
            MobileNumber = mobileNumber;
            Gender = gender;
            BirthDate = birthDate;
            Password = password;
            LastActivity = lastActivity;
            Status = status;
            Email = email;
            IsBanned = isBanned;
        }

        private User()
        {
        }

        public Guid UserId { get; private set; }
        public string UserName { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string NationalCode { get; private set; }
        public string MobileNumber { get; private set; }
        public Gender? Gender { get; private set; }
        public DateTime? BirthDate { get; private set; }
        public string Password { get; private set; }
        public DateTime LastActivity { get; private set; }
        public ChatUserStatus Status { get; private set; }
        public string Email { get; private set; }
        public bool IsBanned { get; private set; }


        public virtual ICollection<Message> Messages { get; private set; }
        public virtual ICollection<ChatRoomMessage> ChatRoomMessages { get; private set; }
        public virtual ICollection<ChatRoomUser> ChatRoomUsers { get; private set; }

        public virtual ICollection<DirectMessage> DirectMessages { get; private set; }
        public virtual ICollection<DirectUser> DirectUsers { get; private set; }


        public static User Create(Guid userId,
            string userName,
            string firstName,
            string lastName,
            string nationalCode,
            string mobileNumber,
            Gender? gender,
            DateTime? birthDate,
            string password,
            DateTime lastActivity,
            ChatUserStatus status,
            string email,
            bool isBanned
        )
        {
            return new User(userId, userName, firstName, lastName, nationalCode, mobileNumber, gender, birthDate,
                password, lastActivity, status, email, isBanned);
        }

        public void UpdateLastActivity()
        {
            LastActivity = DateTime.Now;
        }

        public void Update(Guid userId,
            string userName,
            string firstName,
            string lastName,
            string nationalCode,
            string mobileNumber,
            Gender? gender,
            DateTime? birthDate,
            string password,
            DateTime lastActivity,
            ChatUserStatus status,
            string email,
            bool isBanned
        )
        {
            UserId = userId;
            UserName = userName;
            FirstName = firstName;
            LastName = lastName;
            NationalCode = nationalCode;
            MobileNumber = mobileNumber;
            Gender = gender;
            BirthDate = birthDate;
            Password = password;
            LastActivity = lastActivity;
            Status = status;
            Email = email;
            IsBanned = isBanned;
        }

        public static User DeleteRegistered(int id) => new User() {Id = id};
    }
}
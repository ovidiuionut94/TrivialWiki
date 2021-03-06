﻿using Nancy.Security;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DatabaseManager.DatabaseModels
{
    public class User : IUserIdentity
    {
        public User()
        {
            this.Achievements = new HashSet<Achievement>();
            this.Claims = new HashSet<string>();
            this.Notifications = new List<Notification>();
            this.ProposedTopics = new List<ProposedTopic>();
            this.Friends = new List<User>();
        }

        [Key]
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public Role Role { get; set; }
        public ICollection<Achievement> Achievements { get; set; }
        public IEnumerable<string> Claims { get; set; }
        public ICollection<Notification> Notifications { get; set; }
        public ICollection<ProposedTopic> ProposedTopics { get; set; }
        public int Points { get; set; }
        public int Rank { get; set; }
        public string SecurityToken { get; set; }
        public Statistics Statistic { get; set; }
        public DateTime AccountCreationDate { get; set; }
        public ICollection<User> Friends { get; set; }
    }
}
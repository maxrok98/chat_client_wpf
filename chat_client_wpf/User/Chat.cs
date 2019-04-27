using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chat_client_wpf.User
{
     public class Chat
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public int CreatorId { get; private set; }
        public int AddedId { get; set; }
        public Chat(int id, string name, int creatorid, int addedid)
        {
            Id = id;
            Name = name;
            CreatorId = creatorid;
            AddedId = addedid;
        }
    }
}

using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace tinder_clone.Tables
{
    class RegUserTable
    {
        //model to create data frame
        public Guid MyProperty { get; set; }
        public int UserId { get; set; }
        public string Username { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public byte[] UploadedImage { get; set; }

        public Dictionary<int, bool> Matches { get; set; }

        public List<string> telephonenumbers { get; set; }

        public List<string> MatchNames { get; set; }

        public List<int> SuperLikes { get; set; }


    }
}

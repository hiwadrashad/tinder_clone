using System;
using System.Collections.Generic;

namespace tinder_clone.Models
{
    public class Item
    {
        public string Id { get; set; }
        public string Username { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public byte[] UploadedImage { get; set; }

        public Dictionary<string, bool> Matches { get; set; }

        public List<string> telephonenumbers { get; set; }

        public List<string> MatchNames { get; set; }

        public List<string> SuperLikes { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public double Distance { get; set; }
    }
}
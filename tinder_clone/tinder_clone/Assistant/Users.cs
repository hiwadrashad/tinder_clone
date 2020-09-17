using System;
using System.Collections.Generic;
using System.Text;
using tinder_clone.Models;
using tinder_clone.Services;

namespace tinder_clone.Assistant
{
    class Users
    {
        public static Item MainUser { get; set; }

        public static Item SwipeUser { get; set; }

        public static MockDataStore dataStore { get; set;}
    }
}

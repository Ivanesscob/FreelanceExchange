using System.Collections.ObjectModel;

namespace FreelanceExchange_desktop.Data
{
    /// <summary>
    /// Test
    /// </summary>
    static class DataClass
    {
        public static ObservableCollection<User> Users;
        public static User CurrentUser;
        public static ObservableCollection<Task> Tasks;
        public static List<Response> Responses;

        private static bool isCustomer;
        public static bool IsCustomer
        {
            get; set;
        }

        private static bool isfreelancer;
        public static bool Isfreelancer
        {
            get; set;
        }
    }
}

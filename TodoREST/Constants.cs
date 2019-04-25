namespace TodoREST
{
	public static class Constants
	{
		// URL of REST service
		public static string RestUrl = "http://www.koziuko.com/ws/post.php";
        public static string RestUrlPut = "http://www.koziuko.com/ws/post.php/todoitems/{0}";
        public static string RestUrlPost = "http://www.koziuko.com/ws/post.php";

        public static string RestDELETE = "http://www.koziuko.com/ws/post.php/DELETE";

        //public static string RestUrlDEL = "http://www.koziuko.com/ws/post.php?ID=17bbd3e0-86d5-480c-9e4d-2e2ca4b88368"; //funciona

        public static string RestUrlGetOne = "http://www.koziuko.com/ws/post.php?Name={0}"; //funciona

        public static string RestUrlDEL = "http://www.koziuko.com/ws/post.php?ID="; //funciona

        //public static string RestUrlDEL = "http://www.koziuko.com/ws/post.php/todoitems/{0}";

        // Credentials that are hard coded into the REST service
        public static string Username = "u896579528_koziu";
		public static string Password = "07Dece1941";
	}
}

using System;
namespace TokenAPITest.Entities
{
	public class User
	{
		public int ID { get; set; }
		public string? UserName { get; set; }
		public string? Password { get; set; }
		public int TokenID { get; set; }
	}
}


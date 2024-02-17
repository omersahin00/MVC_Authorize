using System;
namespace TokenAPITest.Entities
{
	public class Token
	{
		public int ID { get; set; }
		public int UserID { get; set; }
		public string? UserToken { get; set; }
	}
}


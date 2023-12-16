using System;
using System.ComponentModel.DataAnnotations;

namespace SessionTestProject.Entities
{
	public class Account
	{
		[Key]
		public int ID { get; set; }

		public string Email { get; set; }

		public string Password { get; set; }

		public bool isActive { get; set; }

	}
}


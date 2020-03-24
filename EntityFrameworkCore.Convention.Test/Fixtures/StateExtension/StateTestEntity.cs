using System.ComponentModel.DataAnnotations;
using EntityFrameworkCore.Convention.StateExtension;

namespace EntityFrameworkCore.Convention.Test.Fixtures.StateExtension
{
	public class StateTestEntity : IState
	{
		[Key]
		public long Id { get; set; }

		public string Message { get; set; }
		
		public State State { get; set; }
	}
}
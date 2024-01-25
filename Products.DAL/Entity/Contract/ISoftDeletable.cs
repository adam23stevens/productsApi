using System;
namespace Products.DAL.Entity.Contract
{
	public interface ISoftDeletable
	{
		public bool IsDeleted { get; set; }
	}
}


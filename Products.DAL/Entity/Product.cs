using System;
using System.ComponentModel.DataAnnotations.Schema;
using Products.DAL.Entity.Contract;

namespace Products.DAL.Entity
{
	public class Product : BaseGuidEntity, ISoftDeletable
	{
		public string Name { get; set; } = "";
		public bool IsDeleted { get; set; }

		[ForeignKey(nameof(ColourId))]
		public Guid ColourId { get; set; }
		public virtual Colour? Colour { get; set; }
	}
}


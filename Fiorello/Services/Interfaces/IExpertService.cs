using System;
using Fiorello.Models;

namespace Fiorello.Services.Interfaces
{
	public interface IExpertService
	{
		Task<IEnumerable<Expert>> GetAll();
		Task<Expert> GetById(int? id);
	}
}
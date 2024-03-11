using AutoMapper;
using ChoreographyBuilder.Core.Contracts;
using ChoreographyBuilder.Core.Models.Figure;
using ChoreographyBuilder.Core.Models.VerseType;
using ChoreographyBuilder.Infrastructure.Data.Common;
using ChoreographyBuilder.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace ChoreographyBuilder.Core.Services
{
	public class FigureService : IFigureService
	{
		private readonly IRepository repository;
		private readonly IMapper mapper;

		public FigureService(IRepository repository, IMapper mapper)
		{
			this.repository = repository;
			this.mapper = mapper;
		}

		public async Task<IEnumerable<FigureTableViewModel>> AllUserFiguresAsync(string userId)
		{
			return await repository.AllAsReadOnly<Figure>()
				.Where(f => f.UserId == userId)
				.Include(f => f.FigureOptions)
				.ThenInclude(fo=>fo.VerseChoreographyFigures)
				.Select(f => mapper.Map<FigureTableViewModel>(f))
				.ToListAsync();
		}
	}
}

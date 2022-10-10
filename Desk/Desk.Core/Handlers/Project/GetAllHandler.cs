using AutoMapper.QueryableExtensions;
using Desk.Core.Handlers.Project.Commands;
using Desk.Core.Handlers.Project.Models;
using Desk.Core.Handlers.Project.Queries;
using Desk.Core.Queries;
using Desk.Core.Queries.Filter;
using Desk.Domain;
using Microsoft.EntityFrameworkCore;

namespace Desk.Core.Handlers.Project;

public class GetAllHandler : IRequestHandler<GetAllCommand, FilteredResult<ProjectModel>>
    {
        private readonly IDbContextFactory<EntitiesDbContext> _contextFactory;
        private readonly IMapper _mapper;

        public GetAllHandler(
            IDbContextFactory<EntitiesDbContext> contextFactory,
            IMapper mapper)
        {
            _contextFactory = contextFactory;
            _mapper = mapper;
        }

        public async Task<FilteredResult<ProjectModel>> Handle(GetAllCommand command, CancellationToken cancellationToken)
        {
            using var context = await _contextFactory.CreateDbContextAsync(cancellationToken);

            return context.Projects
                .AsNoTracking()
                .ByQuery(_mapper.Map<GetAllQuery>(command))
                .ProjectTo<ProjectModel>(_mapper.ConfigurationProvider)
                .Paginate(command);
        }
    }
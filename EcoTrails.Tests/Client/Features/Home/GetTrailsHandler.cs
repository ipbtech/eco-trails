using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using EcoTrails.Shared.Features.Home.Shared;
using MediatR;

namespace EcoTrails.Tests.Client.Features.Home;

    public class GetTrailsHandler : IRequestHandler<GetTrailsRequest, GetTrailsRequest.Response>
    {
        public async Task<GetTrailsRequest.Response> Handle(GetTrailsRequest request, CancellationToken cancellationToken)
        {
            var fixture = new Fixture();
            var dummyTrails = fixture.CreateMany<GetTrailsRequest.Trail>();

            return new GetTrailsRequest.Response(dummyTrails);
        }
    }

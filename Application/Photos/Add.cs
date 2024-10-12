using Application.Core;
using Application.Interfaces;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Photos
{
    public class Add
    {
        public class Command : IRequest<Result<Photo>>
        {
            public IFormFile File { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<Photo>>
        {
            public IUserAccessor UserAccessor { get; }
            public IPhotoAccessor PhotoAccessor { get; }
            public DataContext Context { get; }
            public Handler(DataContext context, IPhotoAccessor photoAccessor, IUserAccessor userAccessor)
            {
                Context = context;
                PhotoAccessor = photoAccessor;
                UserAccessor = userAccessor;
            }
            public async Task<Result<Photo>> Handle(Command request, CancellationToken cancellationToken)
            {
                var user = await Context.Users.Include(p => p.Photos)
                .FirstOrDefaultAsync(x => x.UserName == UserAccessor.GetUsername());

                if (user == null) return null;

                var photoUploadResults = await PhotoAccessor.AddPhoto(request.File);

                var photo = new Photo
                {
                    Url = photoUploadResults.Url,
                    Id = photoUploadResults.PublicId
                };

                if (!user.Photos.Any(x => x.IsMain)) photo.IsMain = true;

                user.Photos.Add(photo);

                var result = await Context.SaveChangesAsync() > 0;

                if (result) return Result<Photo>.Success(photo);

                return Result<Photo>.Failure("Problem adding photo");
            }
        }
    }
}
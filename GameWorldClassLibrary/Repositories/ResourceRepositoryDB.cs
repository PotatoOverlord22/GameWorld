﻿using Microsoft.EntityFrameworkCore;
using GameWorldClassLibrary.Models;
using GameWorldClassLibrary.Utils;

namespace GameWorldClassLibrary.Repositories
{
    public class ResourceRepositoryDB : IResourceRepository
    {
        private readonly GamesContext context;

        public ResourceRepositoryDB(GamesContext context)
        {
            this.context = context;
        }

        public async Task<List<Resource>> GetAllResourcesAsync()
        {
            return await context.Resources.ToListAsync();
        }

        public async Task<Resource> GetResourceByIdAsync(Guid resourceId)
        {
            var resource = await context.Resources.FindAsync(resourceId) ?? throw new KeyNotFoundException("Resource not found");

            return resource;
        }
        public async Task<Resource> GetResourceByTypeAsync(ResourceType resourceType)
        {
            var resource = await context.Resources.FirstOrDefaultAsync(resource => resource.ResourceType == resourceType) ?? throw new KeyNotFoundException("Resource not found");
            return resource;
        }
        public async Task AddResourceAsync(Resource resource)
        {
            context.Resources.Add(resource);
            await context.SaveChangesAsync();
        }
        public async Task UpdateResourceAsync(Resource resource)
        {
            if (context.Resources.Find(resource.Id) == null)
            {
                throw new KeyNotFoundException("Achievement not found");
            }

            context.Resources.Update(resource);
            await context.SaveChangesAsync();
        }
        public async Task DeleteResourceAsync(Guid resourceId)
        {
            var resource = context.Resources.Find(resourceId);
            if (resource == null)
            {
                throw new KeyNotFoundException("Achievement not found");
            }
            context.Resources.Remove(resource);
            await context.SaveChangesAsync();
        }
    }
}

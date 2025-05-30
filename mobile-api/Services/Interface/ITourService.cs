﻿using mobile_api.Models;

namespace mobile_api.Services.Interface
{
    public interface ITourService
    {
        Task<bool> CreateTour(Tour tour);
        Task<Tour> GetTourById(string id);
        Task<IEnumerable<Tour>> GetTours();
        Task<bool> UpdateTour(Tour tour);
        Task<bool> UpdateTourCategoriesAndTags(string tourId, List<int> categoryIds, List<int> tagIds);
    }
}
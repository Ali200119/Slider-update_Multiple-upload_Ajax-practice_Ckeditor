using System;
using System.Collections.Generic;
using Fiorello.Data;
using Fiorello.Models;
using Fiorello.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Fiorello.Services
{
    public class SliderService : ISliderService
    {
        private readonly AppDbContext _context;

        public SliderService(AppDbContext context)
        {
            _context = context;
        }


        
        public async Task<IEnumerable<Slider>> GetAll()
        {
            return await _context.Sliders.ToListAsync();
        }

        public async Task<Slider> GetById(int? id)
        {
            return await _context.Sliders.FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<SliderInfo> GetInfo()
        {
            return await _context.SliderInfo.FirstOrDefaultAsync();
        }
    }
}
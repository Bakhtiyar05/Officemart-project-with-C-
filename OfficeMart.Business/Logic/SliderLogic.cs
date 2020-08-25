using Microsoft.EntityFrameworkCore;
using OfficeMart.Business.Dtos;
using OfficeMart.Business.Models;
using OfficeMart.Domain.Models.Entities;
using OfficeMart.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeMart.Business.Logic
{
    public class SliderLogic
    {
        public async Task<SliderDto> AddImage(string root,SliderDto sliderDto)
        {
            if (sliderDto.Image.IsImage())
            {
                var imageName = await sliderDto.Image.SaveImage(root, "Slider");
                sliderDto.ImageName = imageName;
            }

            using (var context = TransactionConfig.AppDbContext)
            {
                var sliderEntity = TransactionConfig.Mapper.Map<Slider>(sliderDto);
                await context.Sliders.AddAsync(sliderEntity);
                await context.SaveChangesAsync();
                sliderDto.IsSuccessfull = true;
                return sliderDto;
            }
        }

        public async Task<List<SliderDto>> GetAllImages()
        {
            var images = new List<SliderDto>();
            using(var context = TransactionConfig.AppDbContext)
            {
                var imagesEntity = await context
                    .Sliders
                    .ToListAsync();

                images = TransactionConfig.Mapper.Map<List<SliderDto>>(imagesEntity);

                return images;
            }
        }

        public async Task<SliderDto> GetImageById(int id)
        {
            var sliderDto = new SliderDto();
            using(var context = TransactionConfig.AppDbContext)
            {
                var image = await context
                    .Sliders
                    .Where(x => x.Id == id)
                    .FirstOrDefaultAsync();

                sliderDto = TransactionConfig.Mapper.Map<SliderDto>(image);
                return sliderDto;
            }
        }

        public async Task<SliderDto> EditImage(string root,SliderDto sliderDto)
        {
            using(var context  = TransactionConfig.AppDbContext)
            {
                var sliderEntity = await context
                    .Sliders
                    .FindAsync(sliderDto.Id);

                if (sliderDto.ImageForEdit != null)
                {
                    if (sliderDto.ImageForEdit.IsImage())
                    {
                        IFormFileExtensions.RemoveImage(root, sliderEntity.ImageName);
                        var imageName = await sliderDto.ImageForEdit.SaveImage(root, "Slider");
                        sliderDto.ImageName = imageName;
                    }
                }
                else
                    sliderDto.ImageName = sliderEntity.ImageName;

                sliderEntity = TransactionConfig.Mapper.Map(sliderDto, sliderEntity);
                context.Sliders.Update(sliderEntity);
                await context.SaveChangesAsync();
                sliderDto.IsSuccessfull = true;
                return sliderDto;
            }
        }

        public async Task<bool> RemoveImage(int id)
        {
            using(var context = TransactionConfig.AppDbContext)
            {
                var sliderEntity = await context
                    .Sliders
                    .FindAsync(id);

                context.Sliders.Remove(sliderEntity);
                await context.SaveChangesAsync();
                return true;
            }
        }
    }
}

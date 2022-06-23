using Core.Entites;
using Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;


namespace Services.ToDoService
{
    public class ToDoService : IToDoService
    {
        private readonly ApplicationDbContext _context;

        #region Contructor
        public ToDoService(IServiceProvider serviceProvider)
        {
            _context = serviceProvider.GetRequiredService<ApplicationDbContext>();
        }
        #endregion

        #region Create
        public async Task<ToDoEntity> Create(ToDoEntity entity)
        {
            entity.CreatedAt = DateTime.Now;
            entity.UpdatedAT = null;
            await _context.ToDo.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;


        }
        #endregion

        #region Update
        public async Task<ToDoEntity> UpdateItemTitle(ToDoEntity model)
        {
            ToDoEntity entity = await _context.ToDo.Where(item => item.Id == model.Id).FirstOrDefaultAsync();
            if(entity != null)
            {
                entity.Title = model.Title;
                entity.UpdatedAT = DateTime.Now;
                await _context.SaveChangesAsync();
                return entity;
            }
            else
            {
                throw new ArgumentException("Not Found Record");
            }
        }
        public async Task<ToDoEntity> CheckItem(ToDoEntity model)
        {
            try
            {
                ToDoEntity entity = await _context.ToDo.FindAsync(model.Id);
                entity.IsChecked = model.IsChecked;
                await _context.SaveChangesAsync();
                return entity;
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        #region Delete
        public void DeleteItem(int itemId)
        {
            try
            {
                ToDoEntity entity = new();
                entity.Id = itemId;
                _context.Remove(entity);
                _context.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        #region Get
        public async Task<List<ToDoEntity>> GetAllItems()
        {
            try
            {
                return await _context.ToDo.ToListAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<List<ToDoEntity>> GetUnCheckedItems()
        {
            try
            {
                return await _context.ToDo.Where(item => item.IsChecked == false).ToListAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<List<ToDoEntity>> GetCheckedItems()
        {
            try
            {
                return await _context.ToDo.Where(item => item.IsChecked == true).ToListAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<List<ToDoEntity>> GetItemsByDate(DateTime start, DateTime end, bool onlyChecked, bool onlyUnChecked)
        {
            try
            {
                List<ToDoEntity> result = new();

                if (onlyChecked)
                {
                    result = await _context.ToDo.Where(item => item.CreatedAt >= start && item.CreatedAt <= end && item.IsChecked).ToListAsync();
                }
                else if (onlyUnChecked)
                {
                    result = await _context.ToDo.Where(item => item.CreatedAt >= start && item.CreatedAt <= end && !item.IsChecked).ToListAsync();

                }
                else
                {
                    result = await _context.ToDo.Where(item => item.CreatedAt >= start && item.CreatedAt <= end).ToListAsync();
                }
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

    }
}

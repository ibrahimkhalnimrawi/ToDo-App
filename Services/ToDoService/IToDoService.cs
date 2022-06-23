using Core.Entites;

namespace Services.ToDoService
{
    public interface IToDoService
    {
        Task<ToDoEntity> CheckItem(ToDoEntity model);
        Task<ToDoEntity> Create(ToDoEntity entity);
        void DeleteItem(int itemId);
        Task<List<ToDoEntity>> GetAllItems();
        Task<List<ToDoEntity>> GetCheckedItems();
        Task<List<ToDoEntity>> GetItemsByDate(DateTime start, DateTime end, bool onlyChecked, bool onlyUnChecked);
        Task<List<ToDoEntity>> GetUnCheckedItems();
        Task<ToDoEntity> UpdateItemTitle(ToDoEntity model);
    }
}
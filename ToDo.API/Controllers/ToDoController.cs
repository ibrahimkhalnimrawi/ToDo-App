using AutoMapper;
using Core.Entites;
using Microsoft.AspNetCore.Mvc;
using Services.ToDoService;
using ToDo.API.Models;

namespace ToDo.API.Controllers
{
    [Route("api/[controller]")]
    public class ToDoController : Controller
    {
        private readonly IToDoService _toDoService;

        #region Constructor
        public ToDoController(IServiceProvider serviceProvider)
        {
            _toDoService = serviceProvider.GetRequiredService<IToDoService>();
        }
        #endregion

        #region POST Action
        [HttpPost("CreateItem")]
        public async Task<ActionResult> CreateItem([FromBody] ToDoCreationModel model)
        {
            try
            {

            var config = new MapperConfiguration(cfg =>
                   cfg.CreateMap<ToDoCreationModel, ToDoEntity>()
               );
            var mapper = new Mapper(config);
            ToDoEntity entity = mapper.Map<ToDoEntity>(model);
            var result = await _toDoService.Create(entity);
            return Ok(result);

            }
            catch (Exception ex)
            {
               return BadRequest(ex.Message);
            }
        }
        #endregion

        #region PUT Action

        [HttpPut("EditItemTitle")]
        public async Task<ActionResult> EditItemTitle([FromBody] ToDoUpdateTitleModel model)
        {
            try
            {
                var config = new MapperConfiguration(cfg =>
                 cfg.CreateMap<ToDoUpdateTitleModel, ToDoEntity>()
             );

                var mapper = new Mapper(config);
                ToDoEntity entity = mapper.Map<ToDoEntity>(model);
                var result = await _toDoService.UpdateItemTitle(entity);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("CheckItem")]
        public async Task<ActionResult> CheckItem([FromBody] ToDoCheckItemModel model)
        {
            try
            {
                var config = new MapperConfiguration(cfg =>
                  cfg.CreateMap<ToDoCheckItemModel, ToDoEntity>()
                 );

                var mapper = new Mapper(config);
                ToDoEntity entity = mapper.Map<ToDoEntity>(model);
                var result = await _toDoService.CheckItem(entity);
                return Ok(result);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region GET Action

        [HttpGet("GetAllItems")]
        public async Task<ActionResult> GetAllItems()
        {
            try
            {
                List<ToDoEntity> result = await _toDoService.GetAllItems();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("GetUnCheckedItems")]
        public async Task<ActionResult> GetUnCheckedItems()
        {
            try
            {
                var result = await _toDoService.GetUnCheckedItems();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetCheckedItems")]
        public async Task<ActionResult> GetCheckedItems()
        {
            try
            {
                var result = await _toDoService.GetCheckedItems();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetItemsByDate")]
        public async Task<ActionResult> GetItemsByDate(DateTime start, DateTime end , bool onlyChecked = false, bool onlyUnChecked = false) {
            try
            {
                var result = await _toDoService.GetItemsByDate(start, end, onlyChecked, onlyUnChecked);
                return Ok(result);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        #endregion

        #region Delete Action

        [HttpDelete("DeleteItem/{itemId}")]
        public async Task<ActionResult> DeleteItem(int itemId)
        {
            try
            {
                _toDoService.DeleteItem(itemId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        #endregion

    }
}

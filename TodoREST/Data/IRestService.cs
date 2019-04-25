using System.Collections.Generic;
using System.Threading.Tasks;

namespace TodoREST
{
	public interface IRestService
	{
		

        Task<List<TodoItem>> RefreshDataAsync(TodoItem item);

        Task SaveTodoItemAsync (TodoItem item, bool isNewItem);

		Task DeleteTodoItemAsync (string id);
        Task<List<TodoItem>> RefreshDataAsync();
        //Task<List<TodoItem>> RefreshDataAsync_cuit(string item);
    }
}

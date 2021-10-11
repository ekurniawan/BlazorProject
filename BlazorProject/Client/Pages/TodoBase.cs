using BlazorProject.Client.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorProject.Client.Pages
{
    public class TodoBase : ComponentBase
    {
        public List<TodoItem> todos { get; set; } = new();
        protected string newTodo;

        protected void AddTodo()
        {
            if (!string.IsNullOrWhiteSpace(newTodo))
            {
                todos.Add(new TodoItem { Title = newTodo });
                newTodo = string.Empty;
            }
        }
    }
}

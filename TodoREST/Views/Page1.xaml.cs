using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoREST;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ValerioInaes.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Page1 : ContentPage
	{

        bool isNewItem;
        public static string cuit ="";

        public Page1 ()
		{
			InitializeComponent ();
		}


        public Page1(bool isNew = false)
        {
            InitializeComponent();
            isNewItem = isNew;
        }

        //async void OnBuscarActivatedCuit(object sender, EventArgs e)
        //{
            

        //    var todoItem = (TodoItem)BindingContext;

            
        //    todoItem.Name = buscarEntry.Text.ToString();
        //    await App.TodoManager.GetTasksAsync_cuit(buscarEntry.Text.ToString());
        //    await Navigation.PopAsync();
            
        //}

        async void OnBuscarActivated(object sender, EventArgs e)
        {
            var todoItem = (TodoItem)BindingContext;

            cuit = buscarEntry.Text;

            todoItem.Name = cuit;
            //await App.TodoManager.GetTasksAsync(todoItem);


            await App.TodoManager.GetTasksAsync();

            //await Navigation.PopAsync();

            await Navigation.PushAsync(new TodoListPage());
        }

        async void OnSaveActivated(object sender, EventArgs e)
        {
            var todoItem = (TodoItem)BindingContext;
            await App.TodoManager.SaveTaskAsync(todoItem, isNewItem);
            await Navigation.PopAsync();
        }

        async void OnDeleteActivated(object sender, EventArgs e)
        {
            var todoItem = (TodoItem)BindingContext;
            await App.TodoManager.DeleteTaskAsync(todoItem);
            await Navigation.PopAsync();
        }

        void OnCancelActivated(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        void OnSpeakActivated(object sender, EventArgs e)
        {
            var todoItem = (TodoItem)BindingContext;
            App.Speech.Speak(todoItem.Name + " " + todoItem.Notes);
        }
    }
}
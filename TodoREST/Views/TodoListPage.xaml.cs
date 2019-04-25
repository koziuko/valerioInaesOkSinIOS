using System;
using ValerioInaes.Views;
using Xamarin.Forms;

namespace TodoREST
{
	public partial class TodoListPage : ContentPage
	{
		bool alertShown = false;

		public TodoListPage ()
		{
			InitializeComponent ();
		}


       


        protected async override void OnAppearing ()
		{
			base.OnAppearing ();

			if (Constants.RestUrl.Contains ("developer.xamarin.com")) {
				if (!alertShown) {
					await DisplayAlert (
						"Hosted Back-End",
						"This app is running against Xamarin's read-only REST service. To create, edit, and delete data you must update the service endpoint to point to your own hosted REST service.",
						"OK");
					alertShown = true;				
				}
			}
            if (RestService.HeBuscado == "si") {
                listView.ItemsSource = await App.TodoManager.GetTasksAsync();
            }
            else{
                RestService.HeBuscado = "no";
                listView.ItemsSource = await App.TodoManager.GetTasksAsync();

            }
			//listView.ItemsSource = await App.TodoManager.GetTasksAsync ();
		}

        public async void ListarTodoClicked(object sender, EventArgs e)
        {
            RestService.HeBuscado = "no";
            Page1.cuit = "";
            listView.ItemsSource = await App.TodoManager.GetTasksAsync();
        }

        void OnSearchItemClicked(object sender, EventArgs e)
        {
            //var todoItem = new TodoItem()
            //{
            //    ID = Guid.NewGuid().ToString()
            //};
            //var TabbedPage10 = new ValerioInaes.Views.TabbedPage1();
            //TabbedPage10.BindingContext = todoItem;
            //Navigation.PushAsync(TabbedPage10);


            var todoItem = new TodoItem()
            {
                ID = Guid.NewGuid().ToString()
            };
            var page11 = new Page1(true);
            page11.BindingContext = todoItem;
            Navigation.PushAsync(page11);
        }

        void OnSearchItemClickedSolo(object sender, EventArgs e)
        {
            //var todoItem = new TodoItem()
            //{
            //    ID = Guid.NewGuid().ToString()
            //};
            //var TabbedPage10 = new ValerioInaes.Views.TabbedPage1();
            //TabbedPage10.BindingContext = todoItem;
            //Navigation.PushAsync(TabbedPage10);


            var todoItem = new TodoItem()
            {
                ID = Guid.NewGuid().ToString()
            };
            var page11 = new Page1(true);
            page11.BindingContext = todoItem;
            Navigation.PushAsync(page11);
        }


        void OnAddItemClicked (object sender, EventArgs e)
		{
			var todoItem = new TodoItem () {
				ID = Guid.NewGuid ().ToString ()
			};
			var todoPage = new TodoItemPage (true);
			todoPage.BindingContext = todoItem;
			Navigation.PushAsync (todoPage);
		}

		void OnItemSelected (object sender, SelectedItemChangedEventArgs e)
		{
			var todoItem = e.SelectedItem as TodoItem;
			var todoPage = new TodoItemPage ();
			todoPage.BindingContext = todoItem;
			Navigation.PushAsync (todoPage);
		}
	}
}

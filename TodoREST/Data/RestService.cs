using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ValerioInaes.Views;

namespace TodoREST
{
	public class RestService : IRestService
	{
		HttpClient client;

        public static string HeBuscado="no";

		public List<TodoItem> Items { get; private set; }


        public static TodoItem ItemsEstatic { get; set; }

        public RestService ()
		{
			var authData = string.Format("{0}:{1}", Constants.Username, Constants.Password);
			var authHeaderValue = Convert.ToBase64String(Encoding.UTF8.GetBytes(authData));

			client = new HttpClient ();
			client.MaxResponseContentBufferSize = 256000;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authHeaderValue);
		}

		public async Task<List<TodoItem>> RefreshDataAsync ()
		{
			Items = new List<TodoItem> ();

            //string RestUrl = "http://www.koziuko.com/ws/post.php";
            var uri = new Uri(string.Format(Constants.RestUrl, string.Empty));


            if (Page1.cuit=="") {
                uri = new Uri(string.Format(Constants.RestUrl, string.Empty));
            }
            else  
            {
                 
                uri = new Uri(string.Format(Constants.RestUrlGetOne, Page1.cuit));
            }

            

			try {
				var response = await client.GetAsync (uri);
				if (response.IsSuccessStatusCode) {
					var content = await response.Content.ReadAsStringAsync ();

                    if (content.StartsWith("{")) {
                        content = "[" + content;
                    }
                    if (content.EndsWith("}"))
                    {
                        content = content + "]";
                    }
                    //string c2="[{"Done":"False","Name":"21_0_97","Notes":"sociedad militar 2","ID":"100"}]";

                    var output = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(content);

                    string s = output.ToString();

                  


                    dynamic dynJson = JsonConvert.DeserializeObject(s);
                    
                    foreach (var item in dynJson)
                    {
                        TodoItem itemsolo = new TodoItem();

                        string hecho = item.Done;
                        //itemsolo.Done = item.Done;

                        if(hecho =="1")
                        {
                            item.Done = true;
                        } else {
                            item.Done = false;
                        }
                        itemsolo.ID = item.ID;
                        itemsolo.Name = item.Name;
                        itemsolo.Notes = item.Notes;

                        




                        Items.Add(itemsolo);
                    }


                    //Items = JsonConvert.DeserializeObject <List<TodoItem>> (content);


                    //Items = JsonConvert.DeserializeObject<List<TodoItem>>(dynJson);
                }
			} catch (Exception ex) {
				Debug.WriteLine (@"				ERROR {0}", ex.Message);
			}
            //HeBuscado = "no";
			return Items;
		}

        public async Task<List<TodoItem>> RefreshDataAsync_cuit(TodoItem Items)
        {
            List<TodoItem> tuItem = new List<TodoItem>();

            string queItem = Items.Name;

            //string RestUrl = "http://www.koziuko.com/ws/post.php";
            //var uri = new Uri(string.Format(Constants.RestUrl, string.Empty));

            string RestUrlGET = "http://www.koziuko.com/ws/post.php?Name={0}"; //funciona

            var uri = new Uri(string.Format(RestUrlGET, queItem));

            try
            {
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();

                    var output = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(content);

                    string s = output.ToString();

                    string json = JsonConvert.SerializeObject(content, Formatting.Indented);



                    dynamic dynJson = JsonConvert.DeserializeObject(s);

                    foreach (var item in dynJson)
                    {
                        TodoItem itemsolo = new TodoItem();

                        bool hecho = item.Done;
                        //itemsolo.Done = item.Done;

                        //if (hecho == "1")
                        //{
                        //    item.Done = true;
                        //}
                        //else
                        //{
                        //    item.Done = false;
                        //}


                        string itemsolo_Name = item.Name;
                        string itemsolo_Notes = item.Notes;

                        string itemsolo_ID = item.ID;

                        //itemsolo.ID = Items.ID;
                        //itemsolo.Name = Items.Name ;
                        //itemsolo.Notes = Items.Notes;
                        //itemsolo.Done = Items.Done;




                        tuItem.Add(itemsolo);

                        //ItemsEstatic=itemsolo;

                    }


                    //Items = JsonConvert.DeserializeObject<List<TodoItem>>(content);
                    //HeBuscado = "si";
                    //return output;
                    //Items = JsonConvert.DeserializeObject<List<TodoItem>>(output);


                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
            }
            //HeBuscado = "si";





            return tuItem;
        }

        public async Task<List<TodoItem>> RefreshDataAsync(TodoItem Items)
        {
            List<TodoItem> tuItem = new List<TodoItem>();

            string queItem = Items.Name;

            //queItem = Page1.cuit;

            //string RestUrl = "http://www.koziuko.com/ws/post.php";
            //var uri = new Uri(string.Format(Constants.RestUrl, string.Empty));

            string RestUrlGET = "http://www.koziuko.com/ws/post.php?Name={0}"; //funciona

            var uri = new Uri(string.Format(RestUrlGET, queItem));

            try
            {
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();

                    var output = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(content);

                    string s = output.ToString();

                    string json = JsonConvert.SerializeObject(content, Formatting.Indented);

                    dynamic dynJson = JsonConvert.DeserializeObject(s);

                    foreach (var item in dynJson)
                    {
                        TodoItem itemsolo = new TodoItem();


                        string hecho = item.Done;
                        //itemsolo.Done = item.Done;

                        if (hecho == "1")
                        {
                            item.Done = true;
                        }
                        else
                        {
                            item.Done = false;
                        }


                        //itemsolo.ID = item.ID;
                        itemsolo.Name = item.Name;
                        itemsolo.Notes = item.Notes;

                        


                        //itemsolo.ID = Items.ID;
                        //itemsolo.Name = Items.Name ;
                        //itemsolo.Notes = Items.Notes;
                        //itemsolo.Done = Items.Done;




                        tuItem.Add(itemsolo);

                        //ItemsEstatic=itemsolo;

                    }


                    //Items = JsonConvert.DeserializeObject<List<TodoItem>>(content);


                    //Items = JsonConvert.DeserializeObject<List<TodoItem>>(dynJson);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
            }
            //HeBuscado = "si";

            

            

            return tuItem;
        }

        void _Handle_Clicked(object sender, System.EventArgs e)
        {
            //ProfileData profileData = new ProfileData()
            //{
            //    Nombre = entryNombre.Text,
            //    Empresa = entryEmpresa.Text
            //};

            //var jsonData = Newtonsoft.Json.JsonConvert.SerializeObject(profileData);
            //slPerfil.Children.Add(GenerateQR(jsonData));
        }

        //ZXingBarcodeImageView _GenerateQR(string codeValue)
        //{
        //    var qrCode = new ZXingBarcodeImageView
        //    {
        //        BarcodeFormat = BarcodeFormat.QR_CODE,
        //        BarcodeOptions = new QrCodeEncodingOptions
        //        {
        //            Height = 350,
        //            Width = 350
        //        },
        //        BarcodeValue = codeValue,
        //        VerticalOptions = LayoutOptions.CenterAndExpand,
        //        HorizontalOptions = LayoutOptions.CenterAndExpand
        //    };
        //    // Workaround for iOS
        //    qrCode.WidthRequest = 350;
        //    qrCode.HeightRequest = 350;
        //    return qrCode;
        //}


        public async Task SaveTodoItemAsync (TodoItem item, bool isNewItem = false)
		{
			// RestUrl = http://developer.xamarin.com:8081/api/todoitems
			var uri = new Uri (string.Format (Constants.RestUrl, string.Empty));

			try {
				var json = JsonConvert.SerializeObject (item);
				var content = new StringContent (json, Encoding.UTF8, "application/json");

				HttpResponseMessage response = null;
				if (isNewItem) {

                    //http://www.koziuko.com/ws/post.php?ID=148834ca-caca-4375-9d65-3ce9f7a85d11&Done=true&Name=valerio 2 houy&Notes=prueba 2 upadate creado

                    //genero codigo qr ini

                    //perfilTodoItemPage.slPerfil.Children.Add(GenerateQR(json));


                    //genero codigo qr fin

                    string RestUrlPOST = "http://www.koziuko.com/ws/post.php?ID={0}&Done={1}&Name={2}&Notes={3}"; //funciona

                    var ss = new Uri(string.Format(RestUrlPOST, item.ID, item.Done, item.Name, item.Notes));

                    response = await client.PostAsync (ss, content);
				} else {

                    string RestUrlPOST = "http://www.koziuko.com/ws/post.php?ID={0}&Done={1}&Name={2}&Notes={3}"; //funciona

                    var ss = new Uri(string.Format(RestUrlPOST, item.ID, item.Done, item.Name, item.Notes));

                    response = await client.PutAsync (ss, content);
                    //response = await client.PutAsync(uri, content);//original
                }
				
				if (response.IsSuccessStatusCode) {
					Debug.WriteLine (@"				TodoItem successfully saved.");
				}
				
			} catch (Exception ex) {
				Debug.WriteLine (@"				ERROR {0}", ex.Message);
			}
		}

		public async Task DeleteTodoItemAsync (string id)
		{
			// RestUrl = "http://developer.xamarin.com:8081/api/todoitems/{0}";

            string RestUrlDEL = "http://www.koziuko.com/ws/post.php?ID={0}"; //funciona

            //http://www.koziuko.com/ws/post.php?id=248834ca-ccca-4375-9d65-3ce9f7a85d68

            var uri = new Uri (string.Format (RestUrlDEL, id));

            

            //var uri = new Uri(string.Format(Constants.RestUrlDEL, id)); //funciona

            try {
				var response = await client.DeleteAsync (uri);

				if (response.IsSuccessStatusCode) {
					Debug.WriteLine (@"				TodoItem successfully deleted.");	
				}
				
			} catch (Exception ex) {
				Debug.WriteLine (@"				ERROR {0}", ex.Message);
			}
		}

        public async Task BuscarTodoItemAsync(string id)
        {
            // RestUrl = "http://developer.xamarin.com:8081/api/todoitems/{0}";

            string RestUrlDEL = "http://www.koziuko.com/ws/post.php?ID={0}"; //funciona

            //http://www.koziuko.com/ws/post.php?id=248834ca-ccca-4375-9d65-3ce9f7a85d68

            var uri = new Uri(string.Format(RestUrlDEL, id));



            //var uri = new Uri(string.Format(Constants.RestUrlDEL, id)); //funciona

            try
            {
                var response = await client.GetAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine(@"				TodoItem successfully buscado.");
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
            }
        }

       
    }
}

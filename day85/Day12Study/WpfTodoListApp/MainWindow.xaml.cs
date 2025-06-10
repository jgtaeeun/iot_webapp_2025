using ControlzEx.Standard;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using WebApiApp01.Models;
using WpfTodoListApp.Model.cs;

namespace WpfTodoListApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        HttpClient client = new HttpClient();   
        TodoItemsCollection todoItems = new TodoItemsCollection();  

        public MainWindow()
        {
            InitializeComponent();
        }

        private void MetroWindow_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            var comboPairs = new List<KeyValuePair<string, int>> {
                                                                    new KeyValuePair<string, int>("완료", 1),
                                                                    new KeyValuePair<string, int>("미완료", 0),
                                                                };
            CboIsComplete.ItemsSource = comboPairs;


            client.BaseAddress = new System.Uri("http://localhost:6200");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

           
            GetDatas();
        }

        private async Task GetDatas()
        {
            //get메서드
            GrdTodoItems.ItemsSource = todoItems;

            try
            {
                HttpResponseMessage? response = await client.GetAsync("/api/TodoItems");
                response.EnsureSuccessStatusCode(); //상태코드 출력

                var items = await response.Content.ReadAsAsync<IEnumerable<TodoItem>>();
                todoItems.CopyFrom(items);

                await this.ShowMessageAsync("API호출", "로드 성공!");
            }
            catch (Exception ex)
            {
                await this.ShowMessageAsync("API호출 에러", ex.Message);

            }

        }

        //async시 Task가 return값이지만 버튼클릭이벤트메서드와 연결할 때는 Task를 void로 변경해줘야지 연결이 가능하다.
        private async void BtnInsert_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                var todoItem = new TodoItem
                {
                    Id = 0, //Id는 테이블에서 AUTO_INCREMENT되므로
                    Title = TxtTitle.Text,
                    TodoDate = Convert.ToDateTime(DtpTodoDate.SelectedDate).ToString("yyyyMMdd"),
                    IsComplete = Convert.ToBoolean(CboIsComplete.SelectedValue)
                };

                var response = await client.PostAsJsonAsync("/api/TodoItems", todoItem);
                response.EnsureSuccessStatusCode(); //상태코드 출력

                InitGroupBox();
                await GetDatas();


            }
            catch (Exception ex)
            {
               await this.ShowMessageAsync("API호출 에러", ex.Message);

            }
        }

        private async void GrdTodoItems_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
           

            try
            {
                var Id = (GrdTodoItems.SelectedItem as TodoItem)?.Id;

                if (Id == null) return;

                var response = await client.GetAsync($"/api/TodoItems/{Id}");
                response.EnsureSuccessStatusCode();

                var item = await response.Content.ReadAsAsync<TodoItem>();
                TxtId.Text = item.Id.ToString();
                TxtTitle.Text = item.Title.ToString();
                DtpTodoDate.SelectedDate = DateTime.Parse(item.TodoDate.Insert(4, "-").Insert(7, "-"));
                CboIsComplete.SelectedValue = item.IsComplete;
            }
            catch (Exception ex)
            {
                await this.ShowMessageAsync("API호출 에러", ex.Message);
            }
        }

        private async void BtnUpdate_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                var todoItem = new TodoItem
                {
                    Id = Int32.Parse(TxtId.Text), //Id는 테이블에서 AUTO_INCREMENT되므로
                    Title = TxtTitle.Text,
                    TodoDate = Convert.ToDateTime(DtpTodoDate.SelectedDate).ToString("yyyyMMdd"),
                    IsComplete = Convert.ToBoolean(CboIsComplete.SelectedValue)
                };

                var response = await client.PutAsJsonAsync($"/api/TodoItems/{todoItem.Id}", todoItem);
                response.EnsureSuccessStatusCode(); //상태코드 출력


                InitGroupBox();
                await GetDatas();

              

            }
            catch (Exception ex)
            {
                await this.ShowMessageAsync("API호출 에러", ex.Message);

            }
        }

        private async void BtnDelete_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                var Id = (GrdTodoItems.SelectedItem as TodoItem)?.Id;

                if (Id == null) return;

                var response = await client.DeleteAsync($"/api/TodoItems/{Id}");
                response.EnsureSuccessStatusCode(); //상태코드 출력

                InitGroupBox();
                await GetDatas();

      
            }
            catch (Exception ex)
            {
                await this.ShowMessageAsync("API호출 에러", ex.Message);
            }
        }

        private void InitGroupBox() 
        {
            TxtId.Text = string.Empty;
            TxtTitle.Text = string.Empty;
            DtpTodoDate.Text = string.Empty;
            CboIsComplete.Text = string.Empty;
        }
    }
}
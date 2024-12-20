using System;
using System.Linq;
using System.Windows;
using System.Data.Entity;

namespace RepairApp
{
    public partial class StatisticsWindow : Window
    {
        private Entities _context;

        public StatisticsWindow()
        {
            InitializeComponent();
            _context = new Entities();
            LoadStatistics();
        }

        // Метод для загрузки статистики
        private void LoadStatistics()
        {
            try
            {
                // Извлекаем заявки в статусах "В ожидании", "В работе" и "Выполнено"
                var requests = _context.Request
                    .Where(r => r.RequestStatus.StatusName == "В ожидании" ||
                                r.RequestStatus.StatusName == "В работе" ||
                                r.RequestStatus.StatusName == "Выполнено")
                    .ToList();  // Извлекаем все заявки в память для дальнейшей обработки

                // Проверка на наличие заявок
                if (requests.Any())
                {
                    // Общее количество заявок
                    int totalRequests = requests.Count();
                    TotalRequestsCount.Text = $"Общее количество заявок: {totalRequests}";

                    // Количество завершенных заявок (статус "Выполнено")
                    var completedRequests = requests.Count(r => r.RequestStatus.StatusName == "Выполнено");
                    CompletedRequestsCount.Text = $"Завершенные заявки: {completedRequests}";

                    // Количество открытых заявок (статусы "В ожидании" и "В работе")
                    var openRequests = requests.Count(r => r.RequestStatus.StatusName == "В ожидании" || r.RequestStatus.StatusName == "В работе");
                    OpenRequestsCount.Text = $"Открытые заявки: {openRequests}";

                    // Среднее время обработки заявок в секундах (от заявки до текущего времени)
                    var averageProcessingTimeInSeconds = requests
                        .Where(r => r.RequestDate != null)  // Если есть дата заявки
                        .Select(r => (DateTime.Now - r.RequestDate).TotalSeconds)
                        .DefaultIfEmpty(0)  // Если заявок нет, подставляем 0
                        .Average();

                    // Преобразуем среднее время в TimeSpan
                    var averageProcessingTime = TimeSpan.FromSeconds(averageProcessingTimeInSeconds);

                    // Форматируем вывод среднего времени в Часы, Минуты, Секунды
                    string formattedAverageProcessingTime = $"{averageProcessingTime.Hours}ч {averageProcessingTime.Minutes}м {averageProcessingTime.Seconds}с";
                    AverageProcessingTime.Text = $"Среднее время обработки: {formattedAverageProcessingTime}";

                    // Статистика по типам неисправности
                    var faultTypes = requests
                        .Where(r => r.FaultType != null)  // Предполагаем, что FaultType - это свойство в модели Request
                        .GroupBy(r => r.FaultType)  // Группируем по типам неисправности
                        .Select(g => new
                        {
                            FaultTypeName = g.Key,  // Название типа неисправности
                            RequestCount = g.Count()  // Количество заявок с этим типом неисправности
                        })
                        .ToList();

                    // Заполняем ListBox данными о типах неисправности
                    FaultTypeListBox.ItemsSource = faultTypes.Select(f => $"{f.FaultTypeName}: {f.RequestCount} заявок").ToList();
                }
                else
                {
                    // Сообщение, если нет заявок в системе
                    AverageProcessingTime.Text = "Нет заявок в системе";
                    CompletedRequestsCount.Text = "Завершенные заявки: 0";
                    OpenRequestsCount.Text = "Открытые заявки: 0";
                }
            }
            catch (Exception ex)
            {
                // Обработка ошибок при загрузке статистики
                MessageBox.Show($"Ошибка при загрузке статистики: {ex.Message}");
            }
        }
    }
}

namespace Diplom_Pogodel.Models
{
#nullable disable
    public class WardrobeItem
    {
        public int Id { get; set; }         //Параметры одежды
        public string Name { get; set; }
        public int StandartItemId { get; set; }
        public int UserId { get; set; } //За каким пользователем закреплен гардероб
        //Другие свойства гардероба

    }
}

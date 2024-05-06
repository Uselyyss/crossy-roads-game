using System;
using System.Collections.Generic;
using Game.Game;



namespace Game.Menu
{

    /// <summary>
    /// Создание экземпляров экранов
    /// </summary>
    public class ScreenFactory
    {
        
        /// <summary>
        /// Вторая строчка текста инструкции
        /// </summary>
        private const string GUIDE_TEXT_1 = "use WASD to move";

        /// <summary>
        /// Третья строчка текста инструкции
        /// </summary>
        private const string GUIDE_TEXT_2 = "the task of this game: ";

        /// <summary>
        /// Четвертая строчка текста инструкции
        /// </summary>
        private const string GUIDE_TEXT_3 = "Reach the top edge of the field without colliding with enemy (red square) ";
        
        /// <summary>
        /// Четвертая строчка текста инструкции
        /// </summary>
        private const string GUIDE_TEXT_4 = "as many time as possible";

        /// <summary>
        /// Текст для отображения очков
        /// </summary>
        private const string RECORD_SAVEING_SCORE_TEXT = "Your score: ";

        /// <summary>
        /// Заголовок экрана рекордов
        /// </summary>
        private const string RECORDS_TITILE = "Top Score";

        /// <summary>
        /// Заголовок экрана с сохранением рекордов
        /// </summary>
        private const string RECORD_SAVING_TITILE = "Save record";

        /// <summary>
        /// Заголовок экрана инструкции
        /// </summary>
        private const string GUIDE_TITILE = "Guide";

        /// <summary>
        /// Экземпляр класса (singletone)
        /// </summary>
        private static readonly ScreenFactory _instance = new ScreenFactory();

        /// <summary>
        /// Экземпляр класса (singletone)
        /// </summary>
        public static ScreenFactory Instance => _instance;

        /// <summary>
        /// Конструктор
        /// </summary>
        private ScreenFactory() { }


        /// <summary>
        /// Создать экран инструкции
        /// </summary>
        /// <returns>Экран инструкции</returns>
        public Screen CreateGuideScreen()
        {
            List<TextComponent> guide = new List<TextComponent>();
            guide.Add(new TextComponent(GUIDE_TEXT_1));
            guide.Add(new TextComponent(GUIDE_TEXT_2));
            guide.Add(new TextComponent(GUIDE_TEXT_3));
            guide.Add(new TextComponent(GUIDE_TEXT_4));
            return new Screen(new TextComponent(GUIDE_TITILE), guide);
        }


        /// <summary>
        /// Создать экран с рекордами
        /// </summary>
        /// <returns>Экран рекордов</returns>
        public Screen CreateRecordScreen()
        {
            
            List<TextComponent> recordsTextComponents = new List<TextComponent>();
            List<Player> players = RecordsFileUtils.Instance.ReadRecordsFromFile();
            players.Sort((r1, r2) => r2.Score.CompareTo(r1.Score));
            for (int i = 0; i < players.Count && i < 5; i++)
            {
                recordsTextComponents.Add(new TextComponent(i + 1 + ". " + players[i].Nickname + "  -  " + players[i].Score));
            }
            return new Screen(new TextComponent(RECORDS_TITILE), recordsTextComponents);
        
            //return null;
        }

        /// <summary>
        /// Создать экран сохранения рекорда
        /// </summary>
        /// <param name="parScore">Количество очков</param>
        /// <returns>Экран сохранения рекордов</returns>
        public Screen CreateRecordSavingScreen(int parScore)
        {
            List<TextComponent> recordsTextComponents = new List<TextComponent>();
            recordsTextComponents.Add(new TextComponent(RECORD_SAVEING_SCORE_TEXT + parScore));
            return new Screen(new TextComponent(RECORD_SAVING_TITILE), recordsTextComponents);

        }
    }
}

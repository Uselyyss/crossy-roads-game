using System;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace GameConsole
{
    /// <summary>
    /// Класс для вывода в консоль
    /// </summary>
    public class FastOutput
    {
        /// <summary>
        /// Экземпляр класса (singletone)
        /// </summary>
        private static readonly FastOutput _instance = new FastOutput();

        /// <summary>
        /// Получение объекта
        /// </summary>
        /// <returns>Инстанс</returns>
        public static FastOutput GetInstance()
        {
            return _instance;
        }
         /// <summary>
        /// Функция "Создать файл"
        /// </summary>
        /// <param name="fileName">Имя файла</param>
        /// <param name="fileAccess">Код доступа к файлу</param>
        /// <param name="fileShare">Код файлового ресурса</param>
        /// <param name="securityAttributes">Атрибут безопасности</param>
        /// <param name="creationDisposition">Способ открытия файла</param>
        /// <param name="flags">Код флага маршалинга</param>
        /// <param name="template">Шаблон дескриптора</param>
        /// <returns>Успешность создания файла</returns>
        [DllImport("Kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern SafeFileHandle CreateFile(
            string fileName,
            [MarshalAs(UnmanagedType.U4)] uint fileAccess,
            [MarshalAs(UnmanagedType.U4)] uint fileShare,
            IntPtr securityAttributes,
            [MarshalAs(UnmanagedType.U4)] FileMode creationDisposition,
            [MarshalAs(UnmanagedType.U4)] int flags,
            IntPtr template);

        /// <summary>
        /// Файловый дескриптор
        /// </summary>
        public SafeFileHandle h;

        /// <summary>
        /// Функция "Консольный вывод"
        /// </summary>
        /// <param name="hConsoleOutput">Файловый дескриптор</param>
        /// <param name="lpBuffer">Буфер символов</param>
        /// <param name="dwBufferSize">Размер буфера символов</param>
        /// <param name="dwBufferCoord">Расположение буфера символов</param>
        /// <param name="lpWriteRegion">Координаты вершин прямоугольника вывода</param>
        /// <returns>Успешность вывода</returns>
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool WriteConsoleOutput(
          SafeFileHandle hConsoleOutput,
          CharInfo[] lpBuffer,
          Coord dwBufferSize,
          Coord dwBufferCoord,
          ref SmallRect lpWriteRegion);

        /// <summary>
        /// Структура Координаты
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct Coord
        {
            public short X;
            public short Y;

            public Coord(short X, short Y)
            {
                this.X = X;
                this.Y = Y;
            }
        };

        /// <summary>
        /// Структура Наборы символов
        /// </summary>
        [StructLayout(LayoutKind.Explicit)]
        public struct CharUnion
        {
            [FieldOffset(0)] public char UnicodeChar;
            [FieldOffset(0)] public byte AsciiChar;
        }

        /// <summary>
        /// Структура Информация о символе
        /// </summary>
        [StructLayout(LayoutKind.Explicit)]
        public struct CharInfo
        {
            [FieldOffset(0)] public CharUnion Char;
            [FieldOffset(2)] public short Color;
        }

        /// <summary>
        /// Структура Маленький прямоугольник
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct SmallRect
        {
            public short Left;
            public short Top;
            public short Right;
            public short Bottom;
        }

        /// <summary>
        /// Конструктор
        /// </summary>
        public FastOutput()
        {
            h = CreateFile("CONOUT$", 0x40000000, 2, IntPtr.Zero, FileMode.Open, 0, IntPtr.Zero);
        }
        
        /// <summary>
        /// Вывод символов
        /// </summary>
        /// <param name="parGlyph">Выводимый символ</param>
        /// <param name="parBackgroundColor">Цвет заднего фона</param>
        /// <param name="parFontColor">Цвет шрифта</param>
        /// <param name="parCoordinateX">Координата x</param>
        /// <param name="parCoordinateY">Координата y</param>
        /// <param name="parWidth">Высота</param>
        /// <param name="parHeight">Ширина</param>
        public void OutputCharacter(char parGlyph, int parBackgroundColor, int parFontColor, int parCoordinateX, int parCoordinateY, int parWidth, int parHeight)
        {
            if (!h.IsInvalid)
            {
                CharInfo[] buf = new CharInfo[parWidth * parHeight];
                SmallRect rect = new SmallRect()
                {
                    Left = (short)parCoordinateX,
                    Top = (short)parCoordinateY,
                    Right = (short)(parWidth + parCoordinateX),
                    Bottom = (short)(parHeight + parCoordinateY)
                };

                bool b;

                for (int i = 0; i < buf.Length; ++i)
                {
                    buf[i].Color = Convert.ToInt16((parFontColor & 0x0f) + (parBackgroundColor & 0xf0));
                    buf[i].Char.AsciiChar = CorrectEncoding(parGlyph);

                }

                b = WriteConsoleOutput(h, buf,
                    new Coord() { X = (short)parWidth, Y = (short)parHeight },
                    new Coord() { X = 0, Y = 0 },
                    ref rect);
            }
        }
        
        
        /// <summary>
        /// Выполнить правильную кодировку
        /// </summary>
        /// <param name="parCharacter">Символ</param>
        /// <returns>Код символа в ASCII</returns>
        public byte CorrectEncoding(char parCharacter)
        {
            int numberOfSymbol = Convert.ToInt32(parCharacter);

            if (Convert.ToInt32(parCharacter) < 256)
                numberOfSymbol = Convert.ToInt32(parCharacter);
            else
            {
                if ((Convert.ToInt32(parCharacter) >= 1040) && (Convert.ToInt32(parCharacter) <= 1087))
                    numberOfSymbol = Convert.ToInt32(parCharacter) - 912;

                if ((Convert.ToInt32(parCharacter) >= 1088) && (Convert.ToInt32(parCharacter) <= 1103))
                    numberOfSymbol = Convert.ToInt32(parCharacter) - 864;

                if (Convert.ToInt32(parCharacter) == 1025)
                    numberOfSymbol = 240;

                if (Convert.ToInt32(parCharacter) == 1105)
                    numberOfSymbol = 241;

                if (Convert.ToInt32(parCharacter) == 9553)
                    numberOfSymbol = 186;

                if (Convert.ToInt32(parCharacter) == 9559)
                    numberOfSymbol = 187;

                if (Convert.ToInt32(parCharacter) == 9565)
                    numberOfSymbol = 188;

                if (Convert.ToInt32(parCharacter) == 9562)
                    numberOfSymbol = 200;

                if (Convert.ToInt32(parCharacter) == 9556)
                    numberOfSymbol = 201;

                if (Convert.ToInt32(parCharacter) == 9577)
                    numberOfSymbol = 202;

                if (Convert.ToInt32(parCharacter) == 9552)
                    numberOfSymbol = 205;

                if (Convert.ToInt32(parCharacter) == 9580)
                    numberOfSymbol = 206;

                if (Convert.ToInt32(parCharacter) == 9474)
                    numberOfSymbol = 179;

                if (Convert.ToInt32(parCharacter) == 9508)
                    numberOfSymbol = 180;

                if (Convert.ToInt32(parCharacter) == 9488)
                    numberOfSymbol = 191;

                if (Convert.ToInt32(parCharacter) == 9492)
                    numberOfSymbol = 192;

                if (Convert.ToInt32(parCharacter) == 9524)
                    numberOfSymbol = 193;

                if (Convert.ToInt32(parCharacter) == 9516)
                    numberOfSymbol = 194;

                if (Convert.ToInt32(parCharacter) == 9500)
                    numberOfSymbol = 195;

                if (Convert.ToInt32(parCharacter) == 9472)
                    numberOfSymbol = 196;

                if (Convert.ToInt32(parCharacter) == 9532)
                    numberOfSymbol = 197;

                if (Convert.ToInt32(parCharacter) == 9496)
                    numberOfSymbol = 217;

                if (Convert.ToInt32(parCharacter) == 9484)
                    numberOfSymbol = 218;

                if (Convert.ToInt32(parCharacter) == 9608)
                    numberOfSymbol = 219;
            }
            return (byte)numberOfSymbol;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parString">Строка</param>
        /// <param name="parBackgroundColor">ЗАдний фон</param>
        /// <param name="parFontColor">Цвет шрифта</param>
        /// <param name="parCoordinateX">Координата по х</param>
        /// <param name="parCoordinateY">Координата по у</param>
        public void OutputString(string parString, int parBackgroundColor, int parFontColor, int parCoordinateX,
            int parCoordinateY)
        {

            string[] words = parString.Split(new char[] { ';' });

            for (int j = 0; j < words.Length; j++)
            {
                char[] characters = words[j].ToCharArray();

                for (int i = 0; i < characters.Length; i++)
                    OutputCharacter(characters[i], parBackgroundColor, parFontColor, parCoordinateX + i,
                        parCoordinateY + j, 1, 1);
            }
        }
        /// <summary>
        /// Очитстка экрана
        /// </summary>
        public void ClearScreen()
        {
            OutputCharacter(' ', 0x00, 0x00, 0, 0, 300, 50);
        }
    }
}
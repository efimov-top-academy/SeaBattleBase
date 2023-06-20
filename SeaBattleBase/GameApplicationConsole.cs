using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Terminal.Gui;

namespace SeaBattleBase
{
    public class GameApplicationConsole
    {
        public const char Box100 = '\u2588';
        public const char Box75 = '\u2593';
        public const char Box50 = '\u2592';
        public const char Box25 = '\u2591';

        ColorScheme colorWater = new ColorScheme
        {
            Normal = new Terminal.Gui.Attribute(foreground: Color.White, background: Color.Blue),
            HotNormal = new Terminal.Gui.Attribute(foreground: Color.Gray, background: Color.Black),
            Focus = new Terminal.Gui.Attribute(foreground: Color.Cyan, background: Color.Black),
        };

        ColorScheme colorWaterShot = new ColorScheme
        {
            Normal = new Terminal.Gui.Attribute(foreground: Color.Red, background: Color.Blue),
            HotNormal = new Terminal.Gui.Attribute(foreground: Color.Gray, background: Color.Black),
            Focus = new Terminal.Gui.Attribute(foreground: Color.Cyan, background: Color.Black),
        };

        public Game Game = new();
        public string Name { get; set; } = "";

        Window win = null!;
        MenuBar menuMain = null!;

        public void Execute()
        {
            Application.Init();
            menuMain = new MenuBar(new MenuBarItem[] {
                new MenuBarItem ("Игра", new MenuItem []
                {
                    new MenuItem ("_Новая", "Создать новую игру", () => {
                        this.NewGame();
                    }),
                    new MenuItem ("_Сохранить", "Сохранить текущую игру", () => {
                        Application.RequestStop ();
                    }),
                    new MenuItem ("_Загрузить", "Загрузить игру с диска", () => {
                        //Application.RequestStop ();
                        //OpenFile();
                    }),
                    new MenuItem ("_Выход", "", () => {
                        Application.RequestStop ();
                    })
                }),
                new MenuBarItem("_Помощь", new MenuItem[]
                {
                    new MenuItem("_О игре", string.Empty, () =>
                    {
                        MessageBox.Query("Music Sharp 0.2.0", "\nMusic Sharp is a lightweight CLI\n music player written in C#.\n\nDeveloped by Mark-James McDougall\nand licensed under the GPL v3.\n ", "Close");
                    }),
                }),
            });

            win = new Window("Морской бой")
            {
                X = 0,
                Y = 1,
                Width = Dim.Fill(),
                Height = Dim.Fill() - 1,
            };

            Application.Top.Add(menuMain, win);
            Application.Run();
            Application.Shutdown();
        }
        private void NewGame()
        {
            GetNamePlayer();
            win.Title += $" - Игрок: {this.Name}";
            Game.Players[0].Name = this.Name;

            ViewField();
        }

        private void ViewField()
        {
            var fieldFrame = new FrameView($"Игрове поле игрока {Name}")
            {
                X = 1,
                Y = 1,
                Width = Resource.SizeField * Resource.SizeCell * 2 + Resource.SizeCell * 2 + 2,
                Height = Resource.SizeField * Resource.SizeCell + 4,
                CanFocus = false,
            };

            string cellFill = "";
            for (int i = 0; i < Resource.SizeCell; i++)
            {
                for (int j = 0; j < Resource.SizeCell; j++)
                    cellFill += $"{Box50}{Box50}";
                cellFill += "\n";
            }
            cellFill = cellFill.Remove(cellFill.Length - 1);



            for (int r = 0; r < Resource.SizeField; r++)
                for(int c = 0;  c < Resource.SizeField; c++)
                {
                    var label = new Terminal.Gui.Label()
                    {
                        Width = Resource.SizeCell * 2,
                        Height = Resource.SizeCell,
                        X = Resource.SizeCell + c * Resource.SizeCell * 2,
                        Y = Resource.SizeCell / 2 + r * Resource.SizeCell,
                        Text = $"{cellFill}",
                    };
                    if (Game.Players[0].Field.GetCell(r, c).IsShot)
                        label.ColorScheme = colorWaterShot;
                    else
                        label.ColorScheme = colorWater;

                    fieldFrame.Add(label);
                }
            win.Add(fieldFrame);
        }
        private void GetNamePlayer()
        {
            var getNameDialog = new Dialog("Имя игрока", 50, 15);

            var nameLabel = new Terminal.Gui.Label("Введите имя нового игрока")
            {
                X = 0,
                Y = 0,
                Width = Dim.Fill(),
            };

            var nameTextBox = new TextField(string.Empty)
            {
                X = 3,
                Y = 4,
                Width = 42,
            };

            var saveButton = new Button(12, 7, "Сохранить");
            saveButton.Clicked += () =>
            {
                if (nameTextBox.Text.ToString().Trim() != String.Empty)
                {
                    this.Name = nameTextBox.Text.ToString();
                    Application.RequestStop();
                }
                else
                    MessageBox.Query("Имя игрока", "Имя игрока не введено", "Понятно");
            };

            var cancelButton = new Button(29, 7, "Отменить");
            cancelButton.Clicked += () =>
            {
                Application.RequestStop();
            };

            getNameDialog.AddButton(saveButton);
            getNameDialog.AddButton(cancelButton);
            getNameDialog.Add(nameLabel, nameTextBox);
            Application.Run(getNameDialog);

            
        }
    }
}

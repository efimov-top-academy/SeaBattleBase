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
            Disabled = new Terminal.Gui.Attribute(foreground: Color.White, background: Color.Blue),
        };

        ColorScheme colorWaterShot = new ColorScheme
        {
            Normal = new Terminal.Gui.Attribute(foreground: Color.Red, background: Color.Blue),
            HotNormal = new Terminal.Gui.Attribute(foreground: Color.Gray, background: Color.Black),
            Focus = new Terminal.Gui.Attribute(foreground: Color.Cyan, background: Color.Black),
        };

        ColorScheme colorDesk = new ColorScheme
        {
            Normal = new Terminal.Gui.Attribute(foreground: Color.BrightMagenta, background: Color.Blue),
            HotNormal = new Terminal.Gui.Attribute(foreground: Color.Gray, background: Color.Black),
            Focus = new Terminal.Gui.Attribute(foreground: Color.Cyan, background: Color.Black),
            Disabled = new Terminal.Gui.Attribute(foreground: Color.BrightMagenta, background: Color.Blue),
        };

        ColorScheme colorDeskEmpty = new ColorScheme
        {
            Normal = new Terminal.Gui.Attribute(foreground: Color.Gray, background: Color.Blue),
            HotNormal = new Terminal.Gui.Attribute(foreground: Color.Gray, background: Color.Black),
            Focus = new Terminal.Gui.Attribute(foreground: Color.Cyan, background: Color.Black),
        };

        public Game Game = new();
        public string Name { get; set; } = "";

        Window win = null!;
        MenuBar menuMain = null!;

        static int widthFrame = (Resource.SizeField + 1) * Resource.SizeCell * 2 + 2;
        int heightFrame = Resource.SizeField * Resource.SizeCell + 4;
        int xFirstFrame = 3;
        int xSecondFrame = widthFrame + Resource.SizeCell * 5;

        public void Execute()
        {
            Application.Init();
            menuMain = new MenuBar(new MenuBarItem[] {
                new MenuBarItem ("_Игра", new MenuItem []
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
            if (!GetNamePlayer()) return;

            win.Title += $" - Игрок: {this.Name}";
            Game.Players[0].Name = this.Name;
            Game.Players[0].Commander = SetFlotillaConsole;

            Game.Players[0].Commander();
        }

        private void SetFlotillaConsole()
        {
            var setFlotillaDialog = new Window("Установка кораблей");
            setFlotillaDialog.Y = 0;
            setFlotillaDialog.ColorScheme = colorWater;
            setFlotillaDialog.KeyDown += (e) =>
            {
                if (e.KeyEvent.Key == Key.Esc) Application.RequestStop();
            };
            FrameView fieldFrame = null;
            FrameView setupShipsFrame = null;

            Terminal.Gui.Button[,] buttonsField = new Terminal.Gui.Button[Resource.SizeField, Resource.SizeField];
            Terminal.Gui.Button[] buttonsShip = new Terminal.Gui.Button[4];

            int sizeCurrent = 0;
            int[] shipsSize = { 0, 4, 3, 2, 1 };

            int row = 0;
            int column = 0;

            List<Ship> flotilla = Game.Players[0].Flotilla;
            Field field = Game.Players[0].Field;

            CreateGui();

            Application.Run(setFlotillaDialog);

            

            void CreateGui()
            {
                fieldFrame = new FrameView($"Игрове поле игрока {Name}")
                {
                    X = xFirstFrame,
                    Y = 1,
                    Width = widthFrame + Resource.SizeCell * 2,
                    Height = heightFrame,
                    CanFocus = true,
                    Enabled = false,
                };
                
                fieldFrame.KeyPress += (e) =>
                {
                    
                    buttonsField[row, column].SetFocus();
                    switch (e.KeyEvent.Key)
                    {
                        case Key.CursorUp:
                            if (row > 0) row--;
                            break;
                        case Key.CursorDown:
                            if (row < Resource.SizeField - 1) row++;
                            break;
                        case Key.CursorLeft:
                            if (column > 0) column--;
                            break;
                        case Key.CursorRight:
                            if (column < Resource.SizeField - 1) column++;
                            break;
                    }
                };
                

                for (int i = 0; i < Resource.SizeField; i++)
                {
                    var labelChar = new Terminal.Gui.Label()
                    {
                        Width = Resource.SizeCell * 2,
                        Height = Resource.SizeCell,
                        X = Resource.SizeCell * 2 + i * Resource.SizeCell * 2,
                        Y = 0,
                        Text = ((char)(i + 65)).ToString()
                    };
                    fieldFrame.Add(labelChar);

                    var labelDigit = new Terminal.Gui.Label()
                    {
                        Width = Resource.SizeCell * 2,
                        Height = Resource.SizeCell,
                        X = 0,
                        Y = Resource.SizeCell / 2 + i * Resource.SizeCell,
                        Text = (i + 1).ToString()
                    };
                    fieldFrame.Add(labelDigit);
                }

                // Fill one cell
                string cellFill = "";
                for (int i = 0; i < Resource.SizeCell; i++)
                {
                    for (int j = 0; j < Resource.SizeCell; j++)
                        cellFill += $"{Box50}{Box50}";
                    cellFill += "\n";
                }
                cellFill = cellFill.Remove(cellFill.Length - 1);

                //
                for (int r = 0; r < Resource.SizeField; r++)
                    for (int c = 0; c < Resource.SizeField; c++)
                    {
                        var buttonCell = new Terminal.Gui.Button()
                        {
                            Width = Resource.SizeCell * 2,
                            Height = Resource.SizeCell,
                            X = Resource.SizeCell * 2 + c * Resource.SizeCell * 2,
                            Y = Resource.SizeCell / 2 + r * Resource.SizeCell,
                        };
                        buttonCell.TextFormatter.Text = $"{cellFill}";
                        buttonCell.KeyPress += (e) =>
                        {
                            
                        };
                        buttonsField[r, c] = buttonCell;
                        //buttonCell.Enabled = false;
                        if (Game.Players[0].Field.GetCell(r, c).Type == CellType.Water)
                            buttonCell.ColorScheme = colorWater;
                        else
                            buttonCell.ColorScheme = colorDesk;


                        fieldFrame.Add(buttonCell);
                    }
                setFlotillaDialog.Add(fieldFrame);

                setupShipsFrame = new FrameView($"Корабли игрока {Name}")
                {
                    Width = widthFrame,
                    Height = heightFrame,
                    X = xSecondFrame,
                    Y = 1,
                    CanFocus = false,
                };

                for (int s = 1; s < shipsSize.Length; s++)
                {
                    var labelDigit = new Terminal.Gui.Label()
                    {
                        Width = Resource.SizeCell * 2,
                        Height = Resource.SizeCell,
                        X = 2,
                        Y = Resource.SizeCell / 2 + (s - 1) * (Resource.SizeCell + 1),
                        Text = shipsSize[s].ToString()
                    };
                    setupShipsFrame.Add(labelDigit);

                    string textButtonDeskShip = "";
                    for (int r = 0; r < Resource.SizeCell; r++)
                        textButtonDeskShip += new String(Box100, Resource.SizeCell * 2 * s) + "\n";
                    textButtonDeskShip = textButtonDeskShip.Remove(textButtonDeskShip.Length - 1);

                    var buttonDeskCell = new Terminal.Gui.Button()
                    {
                        Width = Resource.SizeCell * 2 * s,
                        Height = Resource.SizeCell,
                        X = 2 + 2,
                        Y = Resource.SizeCell / 2 + (s - 1) * (Resource.SizeCell + 1),
                    };
                    buttonDeskCell.TextFormatter.Text = textButtonDeskShip;
                    buttonsShip[s - 1] = buttonDeskCell;
                    buttonDeskCell.Clicked += () =>
                    {
                        buttonDeskCell.GetCurrentWidth(out sizeCurrent);
                        sizeCurrent /= Resource.SizeCell * 2;

                        row = 0;
                        column = 0;

                        setupShipsFrame.Enabled = false;
                        fieldFrame.Enabled = true;
                        fieldFrame.SetFocus();


                        //buttonsField[0,0].SetFocus();

                        //MessageBox.Query("", sizeCurrent.ToString(), "Понятно");
                        //Application.RequestStop();
                    };
                    if (shipsSize[s] != 0)
                        buttonDeskCell.ColorScheme = colorDesk;
                    else
                    {
                        buttonDeskCell.ColorScheme = colorDeskEmpty;
                        buttonDeskCell.Enabled = false;
                    }

                    setupShipsFrame.Add(buttonDeskCell);
                }

                setFlotillaDialog.Add(setupShipsFrame);
            }
        }

        
        

        
        private bool GetNamePlayer()
        {
            this.Name = "";

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
                TabIndex = 0,
            };

            var saveButton = new Button(12, 7, "Сохранить");
            saveButton.IsDefault = true;
            saveButton.TabIndex = 1;
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
            cancelButton.TabIndex = 2;
            cancelButton.Clicked += () =>
            {
                Application.RequestStop();
            };

            getNameDialog.Add(nameLabel, nameTextBox);
            getNameDialog.AddButton(saveButton);
            getNameDialog.AddButton(cancelButton);
            
            Application.Run(getNameDialog);

            return this.Name.Trim() != String.Empty;
        }
    }
}

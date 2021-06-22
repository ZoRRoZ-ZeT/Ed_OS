using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp7.Resources;

namespace WindowsFormsApp7
{
    public partial class Windows : Form
    {
        private bool WindowsOpenPanelIsOpen { get; set; }
        int MouseX = 0, MouseY = 0;
        int MouseinX = 0, MouseinY = 0;
        bool IsMouseDown;

        public Color folderColor;

        public abstract class Files
        {
            public abstract string name { get; set; }
            public abstract PictureBox picture { get; set; }
            public abstract Color fileColor { get; set; }
            public abstract Label text { get; set; }
            public abstract Point location { get; set; }
            public abstract Control.ControlCollection control { get; set; }
            public abstract int id { get; set; }

            public abstract Panel ChangeNamePanel { get; set; }
            public abstract Panel panel1 { get; set; }
            public abstract Panel panel2 { get; set; }
            public abstract Label label1 { get; set; }
            public abstract Label label2 { get; set; }

            public abstract bool isFileUp();

            public abstract Point _getPrevGrid { get; set; }

            public abstract Point GetPrevGrid();

            public abstract Point _prevPos { get; set; }
            public abstract Point _newPos { get; set; }

            public abstract void setPos(Point _new);

            public abstract Point getPos();

            public abstract Panel getChangeNamePanel();

            public abstract bool GetStatus();

            public abstract bool GetMoved();

            public abstract void setMoved(bool _sets);

            public abstract bool HideAnotherOne();

            public abstract void setHideAnotherOne(bool _bool);

            public abstract void setChangeNamePanel(bool _visible);

            public abstract void changeLocation(Point _location);

            public abstract void Content_Click(object sender, EventArgs e);

            public abstract void panel1_Click(object sender, EventArgs e);

            public abstract void SetNameClosedClass(object sender, FormClosedEventArgs e);

            public abstract void panel2_Click(object sender, EventArgs e);

            public abstract void MoveDown(object sender, MouseEventArgs e);

            public abstract void MoveUp(object sender, MouseEventArgs e);

            public abstract void MoveMove(object sender, MouseEventArgs e);

            public abstract void Delete();
        }

        public class Folder
        {
            //---Вся концепция данного класса--//
            public List<Files> files { get; set; }

            //---Основные поля файла---//
            public string name { get; set; }
            public PictureBox picture { get; set; }
            public Color fileColor { get; set; }
            public Label text { get; set; }
            public Point location { get; set; }
            public Control.ControlCollection control { get; set; }


            //---Панелька редактирования файла--//
            private Panel ChangeNamePanel { get; set; }
            private Panel panel1 { get; set; }
            private Panel panel2 { get; set; }
            private Label label1 { get; set; }
            private Label label2 { get; set; }


            //---Панель открытия папки---//
            private Panel FolderPanel { get; set; }
            private Panel panelUp { get; set; }
            private Panel panelUpLine { get; set; }
            private Panel panelContent { get; set; }
            private Label FolderName { get; set; }
            private Label CloseFolder { get; set; }


            //---Панель создание файла---//
            private Panel CreateFilesPanel { get; set; }
            private Label labelCreate { get; set; }
            private Panel CreateTextFile { get; set; }
            private Label LabelTextFile { get; set; }
            private PictureBox PictureTextFile { get; set; }
            private Panel CreateImage { get; set; }
            private Label LabelImage { get; set; }
            private PictureBox PictureImage { get; set; }

            private PictureBox justPB { get; set; }

            private Point _getPrevGrid { get; set; }

            public Point GetPrevGrid()
            {
                return _getPrevGrid;
            }

            private bool isMoved { get; set; }

            public bool GetMoved()
            {
                return isMoved;
            }

            public void setMoved(bool _sets)
            {
                isMoved = _sets;
            }

            private bool isDeleted { get; set; }

            public bool GetStatus()
            {
                return isDeleted;
            }

            int MouseX = 0, MouseY = 0;
            int MouseinX = 0, MouseinY = 0;
            public bool IsMouseDown;

            bool HideAnother = false;


            public Panel getPanelContent()
            {
                return this.panelContent;
            }

            public Panel getChangeNamePanel()
            {
                return ChangeNamePanel;
            }

            public bool HideAnotherOne()
            {
                return HideAnother;
            }

            public void setHideAnotherOne(bool _bool)
            {
                HideAnother = _bool;
            }

            public void setChangeNamePanel(bool _visible)
            {
                ChangeNamePanel.Visible = _visible;
            }

            public void setCreateFilesPanel(bool _visible)
            {
                CreateFilesPanel.Visible = _visible;
            }

            public bool isFileUp()
            {
                return IsMouseDown;
            }

            public void changeLocation(Point _location)
            {
                this.picture.Location = new Point(_location.X + 5, _location.Y);
                this.text.Location = new Point(_location.X + 6, _location.Y + 51);
            }

            public Folder(string _name, Color _color, Point _location, Control.ControlCollection _control,PictureBox desktop, int counter)
            {
                isMoved = true;

                _getPrevGrid = _location;

                isDeleted = false;

                //---КОНЦЕПЦИЯ!!!---//
                this.files = new List<Files>();

                //---Заполнение главной информации---///
                this.name = _name;
                this.fileColor = _color;
                this.location = _location;
                this.control = _control;
                this.justPB = desktop;

                this.picture = new PictureBox
                {
                    BackgroundImage = Properties.Resources._8352bafcba3d,
                    Size = new Size(48, 48),
                    BackColor = Color.Transparent,
                    Location = new Point(this.location.X + 5, this.location.Y),
                    Name = "Picture" + this.name + counter.ToString(),
                    BackgroundImageLayout = ImageLayout.Zoom
                };

                this.control.Add(this.picture);
                this.picture.BringToFront();
                this.picture.MouseDown += new System.Windows.Forms.MouseEventHandler(MoveDown);
                this.picture.MouseMove += new System.Windows.Forms.MouseEventHandler(MoveMove);
                this.picture.MouseUp += new System.Windows.Forms.MouseEventHandler(MoveUp);
                this.picture.DoubleClick += this.Folder_DoubleClick;
                this.picture.Parent = desktop;

                this.text = new Label
                {
                    AutoSize = true,
                    BackColor = Color.Transparent,
                    Font = new System.Drawing.Font("Arial Narrow", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204))),
                    ForeColor = Color.White,
                    Location = new Point(this.location.X + 6, this.location.Y + 51),
                    Name = "label" + this.name + counter.ToString(),
                    Size = new System.Drawing.Size(33, 25),
                    Text = this.name
                };
                this.control.Add(this.text);
                this.text.BringToFront();
                this.text.Parent = desktop;

                //---Создание панели редактирования файла---//
                this.ChangeNamePanel = new Panel()
                {
                    BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle,
                    Location = new System.Drawing.Point(0, 0),
                    Name = "ChangeFileName" + this.name + counter.ToString(),
                    Size = new System.Drawing.Size(155, 167),
                    TabIndex = 11,
                };

                this.label1 = new Label()
                {
                    AutoSize = true,
                    Location = new System.Drawing.Point(3, 6),
                    Name = "label1" + this.name + counter.ToString(),
                    Size = new System.Drawing.Size(81, 13),
                    TabIndex = 0,
                    Text = "Изменить имя",
                };

                this.label2 = new Label()
                {
                    AutoSize = true,
                    Location = new System.Drawing.Point(2, 7),
                    Name = "label2" + this.name + counter.ToString(),
                    Size = new System.Drawing.Size(50, 13),
                    TabIndex = 0,
                    Text = "Удалить",
                };

                this.panel1 = new Panel()
                {
                    Cursor = System.Windows.Forms.Cursors.Hand,
                    Location = new System.Drawing.Point(0, 2),
                    Name = "panel1" + this.name + counter.ToString(),
                    Size = new System.Drawing.Size(155, 26),
                    TabIndex = 4,
                };
                this.panel1.Click += new System.EventHandler(this.panel1_Click);
                this.label1.Click += new System.EventHandler(this.panel1_Click);
                this.panel1.Controls.Add(this.label1);

                this.panel2 = new Panel()
                {
                    Cursor = System.Windows.Forms.Cursors.Hand,
                    Location = new System.Drawing.Point(0, 28),
                    Name = "panel2" + this.name + counter.ToString(),
                    Size = new System.Drawing.Size(155, 26),
                    TabIndex = 4,
                };
                this.panel2.Click += new System.EventHandler(this.panel2_Click);
                this.label2.Click += new System.EventHandler(this.panel2_Click);
                this.panel2.Controls.Add(this.label2);

                this.ChangeNamePanel.Controls.Add(this.panel1);
                this.ChangeNamePanel.Controls.Add(this.panel2);

                this.control.Add(this.ChangeNamePanel);
                this.ChangeNamePanel.BringToFront();

                this.ChangeNamePanel.Hide();

                //---Панелька содержимого папки---//
                this.FolderPanel = new Panel()
                {
                    BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle,
                    Location = new System.Drawing.Point(309, 125),
                    Name = "FolderPanel" + this.name + counter.ToString(),
                    Size = new System.Drawing.Size(631, 326),
                    TabIndex = 9
                };

                this.panelUp = new Panel()
                {
                    Dock = System.Windows.Forms.DockStyle.Top,
                    Location = new System.Drawing.Point(0, 0),
                    Name = "panel1" + this.name + counter.ToString(),
                    Size = new System.Drawing.Size(629, 25),
                    TabIndex = 0
                };
                this.panelUp.MouseDown += PanelUp_Down;
                this.panelUp.MouseMove += PanelUp_Move;
                this.panelUp.MouseUp += PanelUp_Up;

                this.panelUpLine = new Panel()
                {
                    BackColor = System.Drawing.SystemColors.ActiveCaptionText,
                    Location = new System.Drawing.Point(0, 25),
                    Name = "panel4" + this.name + counter.ToString(),
                    Size = new System.Drawing.Size(629, 2),
                    TabIndex = 1
                };

                this.FolderName = new Label()
                {
                    AutoSize = true,
                    Location = new System.Drawing.Point(9, 6),
                    Name = "label13" + this.name + counter.ToString(),
                    Size = new System.Drawing.Size(36, 13),
                    TabIndex = 1,
                    Text = this.text.Text
                };
                this.FolderName.MouseDown += PanelUp_Down;
                this.FolderName.MouseMove += PanelUp_Move;
                this.FolderName.MouseUp += PanelUp_Up;

                this.CloseFolder = new Label()
                {
                    AutoSize = true,
                    BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle,
                    Location = new System.Drawing.Point(609, 5),
                    Name = "FolderClose" + this.name + counter.ToString(),
                    Size = new System.Drawing.Size(16, 15),
                    TabIndex = 0,
                    Text = "X"
                };
                this.CloseFolder.MouseDown += PanelUp_Down;
                this.CloseFolder.MouseMove += PanelUp_Move;
                this.CloseFolder.MouseUp += PanelUp_Up;
                this.CloseFolder.Click += CloseFile_Click;
                this.panelContent = new Panel()
                {
                    AutoScroll = true,
                    BackColor = System.Drawing.Color.White,
                    Location = new System.Drawing.Point(0, 27),
                    Name = "FolderContent" + this.name + counter.ToString(),
                    Size = new System.Drawing.Size(629, 297),
                    TabIndex = 2
                };
                this.panelContent.MouseDown += this.Content_Click;
                this.panelContent.BringToFront();
                this.panelUp.Controls.Add(this.FolderName);
                this.panelUp.Controls.Add(this.CloseFolder);

                this.CreateFilesPanel = new Panel()
                {
                    BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle,
                    Location = new System.Drawing.Point(1109, 475),
                    Name = "CreateFilesPanel",
                    Size = new System.Drawing.Size(155, 167),
                    TabIndex = 10
                };

                this.CreateTextFile = new Panel()
                {
                    Cursor = System.Windows.Forms.Cursors.Hand,
                    Location = new System.Drawing.Point(0, 28),
                    Name = "CreateTextFile",
                    Size = new System.Drawing.Size(155, 26),
                    TabIndex = 3
                };
                this.CreateTextFile.Click += CreateTextFile_Click;


                this.labelCreate = new Label()
                {
                    AutoSize = true,
                    Location = new System.Drawing.Point(3, 10),
                    Name = "label6",
                    Size = new System.Drawing.Size(52, 13),
                    TabIndex = 0,
                    Text = "Создать:"
                };

                this.LabelTextFile = new Label()
                {
                    AutoSize = true,
                    Location = new System.Drawing.Point(2, 7),
                    Name = "label7",
                    Size = new System.Drawing.Size(114, 13),
                    TabIndex = 1,
                    Text = "Текстовый документ"
                };
                this.LabelTextFile.Click += CreateTextFile_Click;


                this.PictureTextFile = new PictureBox()
                {
                    BackgroundImage = global::WindowsFormsApp7.Properties.Resources.w256h2561380453888Documents256x25632,
                    BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom,
                    Location = new System.Drawing.Point(123, 0),
                    Name = "pictureBox1",
                    Size = new System.Drawing.Size(20, 26),
                    TabIndex = 2,
                    TabStop = false
                };
                this.PictureTextFile.Click += CreateTextFile_Click;


                this.CreateImage = new Panel()
                {
                    Cursor = System.Windows.Forms.Cursors.Hand,
                    Location = new System.Drawing.Point(0, 55),
                    Name = "CreateTextFile",
                    Size = new System.Drawing.Size(155, 26),
                    TabIndex = 3
                };
                this.CreateImage.Click += CreateImage_Click;

                this.LabelImage = new Label()
                {
                    AutoSize = true,
                    Location = new System.Drawing.Point(2, 7),
                    Name = "label7",
                    Size = new System.Drawing.Size(77, 13),
                    TabIndex = 1,
                    Text = "Изображение"
                };
                this.LabelImage.Click += CreateImage_Click;


                this.PictureImage = new PictureBox()
                {
                    BackgroundImage = global::WindowsFormsApp7.Properties.Resources.image,
                    BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom,
                    Location = new System.Drawing.Point(123, 0),
                    Name = "pictureBox1",
                    Size = new System.Drawing.Size(20, 26),
                    TabIndex = 2,
                    TabStop = false
                };
                this.PictureImage.Click += CreateImage_Click;

                this.CreateImage.Controls.Add(this.LabelImage);
                this.CreateImage.Controls.Add(this.PictureImage);

                this.CreateTextFile.Controls.Add(LabelTextFile);
                this.CreateTextFile.Controls.Add(PictureTextFile);

                this.CreateFilesPanel.Controls.Add(this.labelCreate);
                this.CreateFilesPanel.Controls.Add(this.CreateTextFile);
                this.CreateFilesPanel.Controls.Add(this.CreateImage);

                this.CreateFilesPanel.BringToFront();
                this.CreateFilesPanel.Hide();



                this.FolderPanel.Controls.Add(this.panelUp);
                this.FolderPanel.Controls.Add(this.panelUpLine);
                this.FolderPanel.Controls.Add(this.panelContent);
                this.FolderPanel.Controls.Add(this.CreateFilesPanel);
                this.control.Add(FolderPanel);
                this.FolderPanel.BringToFront();

                this.FolderPanel.Hide();
            }

            private void CreateTextFile_Click(object sender, EventArgs e)
            {
                SetNameOfFile setName = new SetNameOfFile();
                setName.FormClosed += SetNameClosed;
                setName.ShowDialog();
                CreateFilesPanel.Hide();
            }

            private void SetNameClosed(object sender, FormClosedEventArgs e)
            {
                if ((sender as SetNameOfFile).name != "")
                {
                    TextFile textfile = new TextFile((sender as SetNameOfFile).name, this.FolderPanel.BackColor, new Point(CreateFilesPanel.Location.X, CreateFilesPanel.Location.Y), this.getPanelContent().Controls,this.control, this.justPB, files.Count);
                    textfile.picture.Parent = this.getPanelContent();
                    textfile.text.Parent = this.getPanelContent();
                    textfile.text.ForeColor = Color.Black;
                    files.Add(textfile);
                }
            }

            private void CreateImage_Click(object sender, EventArgs e)
            {
                SetNameOfFile setName = new SetNameOfFile();
                setName.FormClosed += SetNameClosedImage;
                setName.ShowDialog();
                CreateFilesPanel.Hide();
            }

            private void SetNameClosedImage(object sender, FormClosedEventArgs e)
            {
                if ((sender as SetNameOfFile).name != "")
                {
                    Picture picture = new Picture((sender as SetNameOfFile).name, this.FolderPanel.BackColor, new Point(CreateFilesPanel.Location.X, CreateFilesPanel.Location.Y), this.getPanelContent().Controls, this.control, this.justPB, files.Count);
                    picture.picture.Parent = this.getPanelContent();
                    picture.text.Parent = this.getPanelContent();
                    picture.text.ForeColor = Color.Black;
                    files.Add(picture);
                }
            }

            public void Content_Click(object sender, MouseEventArgs e)
            {
                this.setChangeNamePanel(false);
                this.setHideAnotherOne(true);
                if (e.Button == MouseButtons.Right)
                {
                    CreateFilesPanel.Show();
                    CreateFilesPanel.Location = new Point(e.Location.X, e.Location.Y);
                    CreateFilesPanel.BringToFront();
                }
                if (e.Button == MouseButtons.Left)
                {
                    CreateFilesPanel.Hide();
                }
            }

            public void CloseFile_Click(object sender, EventArgs e)
            {
                this.FolderPanel.Hide();
            }

            public void PanelUp_Down(object sender, MouseEventArgs e)
            {
                IsMouseDown = true;
                MouseinX = MousePosition.X - this.FolderPanel.Bounds.X;
                MouseinY = MousePosition.Y - this.FolderPanel.Bounds.Y;
                this.FolderPanel.BringToFront();
                this.HideAnother = true;
                this.setChangeNamePanel(false);
                this.setCreateFilesPanel(false);
            }
            public void PanelUp_Move(object sender, MouseEventArgs e)
            {
                if (IsMouseDown)
                {
                    MouseX = MousePosition.X - MouseinX;
                    MouseY = MousePosition.Y - MouseinY;

                    this.FolderPanel.Location = new Point(MouseX, MouseY);
                }
            }
            public void PanelUp_Up(object sender, MouseEventArgs e)
            {
                IsMouseDown = false;
            }


            public void Folder_DoubleClick(object sender, EventArgs e)
            {
                if (!this.FolderPanel.Visible)
                    this.FolderPanel.Show();

                this.FolderPanel.BringToFront();
            }

            public void MoveDown(object sender, MouseEventArgs e)
            {
                _getPrevGrid = new Point(this.picture.Left, this.picture.Top);
                this.setHideAnotherOne(true);
                if (e.Button == MouseButtons.Left)
                {
                    IsMouseDown = true;
                    MouseinX = MousePosition.X - this.picture.Bounds.X;
                    MouseinY = MousePosition.Y - this.picture.Bounds.Y;
                    this.picture.BringToFront();
                    this.text.BringToFront();
                    this.setChangeNamePanel(false);
                    this.setCreateFilesPanel(false);
                }
                else if (e.Button == MouseButtons.Right)
                {
                    this.ChangeNamePanel.Location = new Point(this.picture.Left + 48, this.picture.Top);
                    this.setChangeNamePanel(true);
                    this.ChangeNamePanel.BringToFront();
                }
            }
            public void MoveUp(object sender, MouseEventArgs e)
            {
                IsMouseDown = false;
                isMoved = true;
            }
            public void MoveMove(object sender, MouseEventArgs e)
            {
                if (IsMouseDown)
                {
                    MouseX = MousePosition.X - MouseinX;
                    MouseY = MousePosition.Y - MouseinY;

                    this.picture.Location = new Point(MouseX, MouseY);
                    this.text.Location = new Point(this.picture.Location.X + 6, this.picture.Location.Y + 51);
                }
            }

            public void panel1_Click(object sender, EventArgs e)
            {
                SetNameOfFile setName = new SetNameOfFile();
                setName.FormClosed += SetNameClosedClass;
                setName.ShowDialog();
            }

            public void panel2_Click(object sender, EventArgs e)
            {
                this.Delete();
                this.ChangeNamePanel.Dispose();
                this.CreateFilesPanel.Dispose();
                this.FolderPanel.Dispose();
            }

            public void SetNameClosedClass(object sender, FormClosedEventArgs e)
            {
                if ((sender as SetNameOfFile).name != "")
                {
                    this.name = (sender as SetNameOfFile).name;
                    this.text.Text = (sender as SetNameOfFile).name;
                    this.FolderName.Text = this.text.Text;
                    this.ChangeNamePanel.Hide();
                }
            }

            public void Delete()
            {
                this.picture.Dispose();
                this.control.Remove(this.picture);
                this.text.Dispose();
                this.control.Remove(this.text);
                isDeleted = true;
            }
        }

        public class TextFile : Files
        {
            //---Основные поля файла---//
            public override string name { get; set; }
            public override PictureBox picture { get; set; }
            public override Color fileColor { get; set; }
            public override Label text { get; set; }
            public override Point location { get; set; }
            public override Control.ControlCollection control { get; set; }
            public override int id { get; set; }
            //---Панелька редактирования файла--//
            public override Panel ChangeNamePanel { get; set; }
            public override Panel panel1 { get; set; }
            public override Panel panel2 { get; set; }
            public override Label label1 { get; set; }
            public override Label label2 { get; set; }

            //---Панелька изменения содержимого файла---///
            private Panel OpenTextFilePanel { get; set; }
            private Panel panelUp { get; set; }
            private Panel panelUpLine { get; set; }
            private Label TextFileName { get; set; }
            private Label CloseTextFile { get; set; }
            private TextBox content { get; set; }

            private bool isDeleted { get; set; }

            public override bool GetStatus()
            {
                return isDeleted;
            }

            int MouseX = 0, MouseY = 0;
            int MouseinX = 0, MouseinY = 0;
            bool IsMouseDown;

            bool HideAnother = false;

            public override Point _getPrevGrid { get; set; }

            public override Point GetPrevGrid()
            {
                return _getPrevGrid;
            }

            private bool isMoved { get; set; }

            public override bool GetMoved()
            {
                return isMoved;
            }

            public override void setMoved(bool _sets)
            {
                isMoved = _sets;
            }

            public void ChangeContentText(string _newText)
            {
                this.content.Text = _newText;
            }
            public string getContentText()
            {
                return this.content.Text;
            }
            public override bool isFileUp()
            {
                return IsMouseDown;
            }

            public override Point _prevPos { get; set; }
            public override Point _newPos { get; set; }

            public override void setPos(Point _new)
            {
                this._newPos = _new;
            }

            public override Point getPos()
            {
                return this._newPos;
            }

            public override Panel getChangeNamePanel()
            {
                return ChangeNamePanel;
            }

            public override bool HideAnotherOne()
            {
                return HideAnother;
            }

            public override void setHideAnotherOne(bool _bool)
            {
                HideAnother = _bool;
            }

            public override void setChangeNamePanel(bool _visible)
            {
                ChangeNamePanel.Visible = _visible;
            }

            public override void changeLocation(Point _location)
            {
                this.picture.Location = _location;
                this.text.Location = new Point(_location.X - 2, _location.Y + 51);
            }

            public TextFile(string _name, Color _color, Point _location, Control.ControlCollection _control, Control.ControlCollection _mainControl, PictureBox desktop, int counter)
            {
                isMoved = true;

                _getPrevGrid = _location;

                isDeleted = false;

                this.id = counter;
                this._prevPos = _location;
                this._newPos = new Point(-100,-100);
                //---Заполнение главной информации---///
                this.name = _name;
                this.fileColor = _color;
                this.location = _location;
                this.control = _control;

                this.picture = new PictureBox
                {
                    BackgroundImage = Properties.Resources.w256h2561383075021Documenttxticon,
                    Size = new Size(48, 48),
                    BackColor = Color.Transparent,
                    Location = new Point(this.location.X + 5, this.location.Y),
                    Name = "Picture" + this.name + counter.ToString(),
                    BackgroundImageLayout = ImageLayout.Zoom
                };
                this.control.Add(this.picture);
                this.picture.BringToFront();
                this.picture.MouseDown += new System.Windows.Forms.MouseEventHandler(MoveDown);
                this.picture.MouseMove += new System.Windows.Forms.MouseEventHandler(MoveMove);
                this.picture.MouseUp += new System.Windows.Forms.MouseEventHandler(MoveUp);
                this.picture.DoubleClick += File_DoubleClick;
                this.picture.Parent = desktop;
                this.text = new Label
                {
                    AutoSize = true,
                    BackColor = Color.Transparent,
                    Font = new System.Drawing.Font("Arial Narrow", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204))),
                    Location = new Point(this.location.X - 2, this.location.Y + 51),
                    ForeColor = Color.White,
                    Name = "label" + this.name + counter.ToString(),
                    Size = new System.Drawing.Size(33, 25),
                    Text = this.name + ".txt"
                };
                this.control.Add(this.text);
                this.text.BringToFront();
                this.text.Parent = desktop;

                //---Создание панели редактирования файла---//
                this.ChangeNamePanel = new Panel()
                {
                    BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle,
                    Location = new System.Drawing.Point(0, 0),
                    Name = "ChangeFileName" + this.name + counter.ToString(),
                    Size = new System.Drawing.Size(155, 167),
                    TabIndex = 11
                };

                this.label1 = new Label()
                {
                    AutoSize = true,
                    Location = new System.Drawing.Point(3, 6),
                    Name = "label1" + this.name + counter.ToString(),
                    Size = new System.Drawing.Size(81, 13),
                    TabIndex = 0,
                    Text = "Изменить имя"
                };
                this.label2 = new Label()
                {
                    AutoSize = true,
                    Location = new System.Drawing.Point(2, 7),
                    Name = "label2" + this.name + counter.ToString(),
                    Size = new System.Drawing.Size(50, 13),
                    TabIndex = 0,
                    Text = "Удалить"
                };

                this.panel1 = new Panel()
                {
                    Cursor = System.Windows.Forms.Cursors.Hand,
                    Location = new System.Drawing.Point(0, 2),
                    Name = "panel1" + this.name + counter.ToString(),
                    Size = new System.Drawing.Size(155, 26),
                    TabIndex = 4
                };
                this.panel1.Click += new System.EventHandler(this.panel1_Click);
                this.label1.Click += new System.EventHandler(this.panel1_Click);
                this.panel1.Controls.Add(this.label1);

                this.panel2 = new Panel()
                {
                    Cursor = System.Windows.Forms.Cursors.Hand,
                    Location = new System.Drawing.Point(0, 28),
                    Name = "panel2" + this.name + counter.ToString(),
                    Size = new System.Drawing.Size(155, 26),
                    TabIndex = 4
                };
                this.panel2.Click += new System.EventHandler(this.panel2_Click);
                this.label2.Click += new System.EventHandler(this.panel2_Click);
                this.panel2.Controls.Add(this.label2);

                this.ChangeNamePanel.Controls.Add(this.panel1);
                this.ChangeNamePanel.Controls.Add(this.panel2);

                this.control.Add(this.ChangeNamePanel);
                this.ChangeNamePanel.BringToFront();
                
                this.ChangeNamePanel.Hide();

                //---Создания панели изменения содержимого файла---//
                this.OpenTextFilePanel = new Panel()
                {
                    BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle,
                    Location = new System.Drawing.Point(415, 67),
                    Name = "OpenTextFilePanel" + this.name + counter.ToString(),
                    Size = new System.Drawing.Size(474, 457),
                    TabIndex = 12
                };

                this.panelUp = new Panel()
                {
                    Dock = System.Windows.Forms.DockStyle.Top,
                    Location = new System.Drawing.Point(0, 0),
                    Name = "panelUp" + this.name + counter.ToString(),
                    Size = new System.Drawing.Size(472, 31),
                    TabIndex = 0
                };

                this.panelUp.MouseDown += new System.Windows.Forms.MouseEventHandler(PanelUp_Down);
                this.panelUp.MouseMove += new System.Windows.Forms.MouseEventHandler(PanelUp_Move);
                this.panelUp.MouseUp += new System.Windows.Forms.MouseEventHandler(PanelUp_Up);

                this.panelUpLine = new Panel()
                {
                    BackColor = System.Drawing.SystemColors.ActiveCaptionText,
                    Location = new System.Drawing.Point(0, 31),
                    Name = "panelUpLine" + this.name + counter.ToString(),
                    Size = new System.Drawing.Size(472, 2),
                    TabIndex = 1
                };

                this.panelUpLine.BringToFront();
                this.TextFileName = new Label()
                {
                    AutoSize = true,
                    Location = new System.Drawing.Point(9, 9),
                    Name = "TextFileName" + this.name + counter.ToString(),
                    Size = new System.Drawing.Size(77, 13),
                    TabIndex = 2,
                    Text = this.text.Text
                };
                this.TextFileName.MouseDown += new System.Windows.Forms.MouseEventHandler(PanelUp_Down);
                this.TextFileName.MouseMove += new System.Windows.Forms.MouseEventHandler(PanelUp_Move);
                this.TextFileName.MouseUp += new System.Windows.Forms.MouseEventHandler(PanelUp_Up);

                this.CloseTextFile = new Label()
                {
                    AutoSize = true,
                    BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle,
                    Location = new System.Drawing.Point(449, 9),
                    Name = "CloseTextFile" + this.name + counter.ToString(),
                    Size = new System.Drawing.Size(16, 15),
                    TabIndex = 1,
                    Text = "X"
                };
                this.CloseTextFile.MouseDown += new System.Windows.Forms.MouseEventHandler(PanelUp_Down);
                this.CloseTextFile.MouseMove += new System.Windows.Forms.MouseEventHandler(PanelUp_Move);
                this.CloseTextFile.MouseUp += new System.Windows.Forms.MouseEventHandler(PanelUp_Up);
                this.CloseTextFile.Click += CloseFile_Click;

                this.content = new TextBox()
                {
                    Location = new System.Drawing.Point(0, 33),
                    Multiline = true,
                    Name = "content" + this.name + counter.ToString(),
                    Size = new System.Drawing.Size(472, 422),
                    TabIndex = 2
                };
                this.content.Click += Content_Click;

                this.panelUp.Controls.Add(this.TextFileName);
                this.panelUp.Controls.Add(this.CloseTextFile);

                this.OpenTextFilePanel.Controls.Add(this.panelUp);
                this.OpenTextFilePanel.Controls.Add(this.panelUpLine);
                this.OpenTextFilePanel.Controls.Add(this.content);

                _mainControl.Add(this.OpenTextFilePanel);
                this.OpenTextFilePanel.BringToFront();
                this.OpenTextFilePanel.Hide();

            }


            public override void Content_Click(object sender, EventArgs e)
            {
                this.HideAnother = true;
            }
            public override void panel1_Click(object sender, EventArgs e)
            {
                SetNameOfFile setName = new SetNameOfFile();
                setName.FormClosed += SetNameClosedClass;
                setName.ShowDialog();
            }

            public override void SetNameClosedClass(object sender, FormClosedEventArgs e)
            {
                if( (sender as SetNameOfFile).name != "")
                {
                    this.name = (sender as SetNameOfFile).name;
                    this.text.Text = (sender as SetNameOfFile).name + ".txt";
                    this.TextFileName.Text = this.text.Text;
                    this.ChangeNamePanel.Hide();
                }
            }
            public override void panel2_Click(object sender, EventArgs e)
            {
                this.Delete();
                this.ChangeNamePanel.Dispose();
                this.OpenTextFilePanel.Dispose();
            }
            public override void MoveDown(object sender, MouseEventArgs e)
            {
                _getPrevGrid = new Point(this.picture.Left, this.picture.Top);
                this._prevPos = this._newPos;
                this.setHideAnotherOne(true);
                if (e.Button == MouseButtons.Left)
                {
                    IsMouseDown = true;
                    MouseinX = MousePosition.X - this.picture.Bounds.X;
                    MouseinY = MousePosition.Y - this.picture.Bounds.Y;
                    this.picture.BringToFront();
                    this.text.BringToFront();
                    this.ChangeNamePanel.Hide();
                }
                else if(e.Button == MouseButtons.Right)
                {
                    this.ChangeNamePanel.Location = new Point(this.picture.Left + 48, this.picture.Top);
                    this.setChangeNamePanel(true);
                    this.ChangeNamePanel.BringToFront();
                }
            }
            public override void MoveUp(object sender, MouseEventArgs e)
            {
                IsMouseDown = false;
                this._newPos = this.picture.Location;
                isMoved = true;

            }
            public override void MoveMove(object sender, MouseEventArgs e)
            {

                if (IsMouseDown)
                {
                    MouseX = MousePosition.X - MouseinX;
                    MouseY = MousePosition.Y - MouseinY;

                    this.picture.Location = new Point(MouseX, MouseY);
                    this.text.Location = new Point(this.picture.Location.X - 2, this.picture.Location.Y + 51);
                }
                else
                {
                    this._newPos = new Point(-100, -100);
                }
            }

            public override void Delete()
            {
                this.picture.Dispose();
                this.control.Remove(this.picture);
                this.text.Dispose();
                this.control.Remove(this.text);
                isDeleted = true;
            }

            public void File_DoubleClick(object sender, EventArgs e)
            {
                if(!this.OpenTextFilePanel.Visible)
                    this.OpenTextFilePanel.Show();

                this.OpenTextFilePanel.BringToFront();
            }

            public void CloseFile_Click(object sender, EventArgs e)
            {
                this.OpenTextFilePanel.Hide();
            }

            public void PanelUp_Down(object sender, MouseEventArgs e)
            {
                IsMouseDown = true;
                MouseinX = MousePosition.X - this.OpenTextFilePanel.Bounds.X;
                MouseinY = MousePosition.Y - this.OpenTextFilePanel.Bounds.Y;
                this.OpenTextFilePanel.BringToFront();
                this.HideAnother = true;
                this.setChangeNamePanel(false);
            }
            public void PanelUp_Move(object sender, MouseEventArgs e)
            {
                if (IsMouseDown)
                {
                    MouseX = MousePosition.X - MouseinX;
                    MouseY = MousePosition.Y - MouseinY;

                    this.OpenTextFilePanel.Location = new Point(MouseX, MouseY);
                }
            }
            public void PanelUp_Up(object sender, MouseEventArgs e)
            {
                IsMouseDown = false;
            }
        }

        public class Picture : Files
        {
            //---Основные поля файла---//
            public override string name { get; set; }
            public override PictureBox picture { get; set; }
            public override Color fileColor { get; set; }
            public override Label text { get; set; }
            public override Point location { get; set; }
            public override Control.ControlCollection control { get; set; }
            public override int id { get; set; }
            //---Панелька редактирования файла--//
            public override Panel ChangeNamePanel { get; set; }
            public override Panel panel1 { get; set; }
            public override Panel panel2 { get; set; }
            public override Label label1 { get; set; }
            public override Label label2 { get; set; }
            //---Панелька изменения содержимого файла---///
            private Panel PicturePanel { get; set; }
            private Panel PicturePanelUp { get; set; }
            private Panel PicturePanelDown { get; set; }
            private Panel PicturePanelLeft { get; set; }
            private PictureBox PictureContentPanel { get; set; }

            private Label PictureName { get; set; }             //
            private Label ClosePicture { get; set; }            //  Верхняя панель
            private Panel PicturePanelUpLine { get; set; }      //

            private PictureBox PencilChoose { get; set; }       //
            private PictureBox PencilBG { get; set; }           //  Левая панель
            private Label PencilLabel { get; set; }             //

            private PictureBox BrushChoose { get; set; }        //
            private PictureBox BrushBG { get; set; }            //  Левая панель
            private Label BrushLabel { get; set; }              //

            private PictureBox LastikChoose { get; set; }       //
            private PictureBox LastikBG { get; set; }           //  Левая панель
            private Label LastikLabel { get; set; }             //

            private Panel PicturePanelLeftLine { get; set; }    //  Левая панель

            private PictureBox MainColor { get; set; }          //
            private PictureBox color1 { get; set; }             //
            private PictureBox color2 { get; set; }             //
            private PictureBox color3 { get; set; }             //
            private PictureBox color4 { get; set; }             //
            private PictureBox color5 { get; set; }             //
            private PictureBox color6 { get; set; }             //
            private PictureBox color7 { get; set; }             //
            private PictureBox color8 { get; set; }             //  Нижнаяя панель
            private PictureBox color9 { get; set; }             //
            private PictureBox color10 { get; set; }            //
            private PictureBox color11 { get; set; }            //
            private PictureBox color12 { get; set; }            //
            private PictureBox color13 { get; set; }            //
            private PictureBox color14 { get; set; }            //
            private PictureBox color15 { get; set; }            //
            private PictureBox color16 { get; set; }            //

            private Panel down_vertical_panel1 { get; set; }    //  Нижнаяя панель

            private TextBox SizeOfPen { get; set; }             //  Нижнаяя панель
            private Label LabelSize { get; set; }               //  Нижнаяя панель

            private Panel down_vertical_panel2 { get; set; }    //  Нижнаяя панель

            private Button OpenPictureBtn { get; set; }         //  Нижнаяя панель

            private Panel down_vertical_panel3 { get; set; }    //  Нижнаяя панель

            private Button ClearPictureBtn { get; set; }        //  Нижнаяя панель

            private Panel PicturePanelDownLine { get; set; }    //  Нижнаяя панель

            public override Point _getPrevGrid { get; set; }

            public override Point GetPrevGrid()
            {
                return _getPrevGrid;
            }

            private bool isDeleted { get; set; }

            public override bool GetStatus()
            {
                return isDeleted;
            }

            private bool isMoved { get; set; }

            public override bool GetMoved()
            {
                return isMoved;
            }

            public override void setMoved(bool _sets)
            {
                isMoved = _sets;
            }
            private bool isPencil = false;
            private int width;

            private Point CurrentPoint;
            private Point PrevPoint;
            private Graphics g;

            
            public void setPictureImage(Bitmap _image)
            {
                this.PictureContentPanel.Image = _image;
            }

            public PictureBox getPictureImage()
            {
                return this.PictureContentPanel;
            }

            public Picture(string _name, Color _color, Point _location, Control.ControlCollection _control, Control.ControlCollection _mainControl, PictureBox desktop, int counter)
            {
                isMoved = true;

                _getPrevGrid = _location;

                isDeleted = false;

                this.id = counter;
                this._prevPos = _location;
                this._newPos = new Point(-100, -100);
                //---Заполнение главной информации---///
                this.name = _name;
                this.fileColor = _color;
                this.location = _location;
                this.control = _control;

                this.picture = new PictureBox
                {
                    BackgroundImage = Properties.Resources.image,
                    Size = new Size(48, 48),
                    BackColor = Color.Transparent,
                    Location = new Point(this.location.X+5, this.location.Y),
                    Name = "Picture" + this.name + counter.ToString(),
                    BackgroundImageLayout = ImageLayout.Zoom
                };
                this.control.Add(this.picture);
                this.picture.BringToFront();
                this.picture.MouseDown += new System.Windows.Forms.MouseEventHandler(MoveDown);
                this.picture.MouseMove += new System.Windows.Forms.MouseEventHandler(MoveMove);
                this.picture.MouseUp += new System.Windows.Forms.MouseEventHandler(MoveUp);
                this.picture.DoubleClick += File_DoubleClick;
                this.picture.Parent = desktop;

                this.text = new Label
                {
                    AutoSize = true,
                    BackColor = Color.Transparent,
                    Font = new System.Drawing.Font("Arial Narrow", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204))),
                    Location = new Point(this.location.X + 6, this.location.Y + 51),
                    ForeColor = Color.White,
                    Name = "label" + this.name + counter.ToString(),
                    Size = new System.Drawing.Size(33, 25),
                    Text = this.name + ".png"
                };
                this.control.Add(this.text);
                this.text.BringToFront();
                this.text.Parent = desktop;

                //---Создание панели редактирования файла---//
                this.ChangeNamePanel = new Panel()
                {
                    BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle,
                    Location = new System.Drawing.Point(0, 0),
                    Name = "ChangeFileName" + this.name + counter.ToString(),
                    Size = new System.Drawing.Size(155, 167),
                    TabIndex = 11
                };

                this.label1 = new Label()
                {
                    AutoSize = true,
                    Location = new System.Drawing.Point(3, 6),
                    Name = "label1" + this.name + counter.ToString(),
                    Size = new System.Drawing.Size(81, 13),
                    TabIndex = 0,
                    Text = "Изменить имя"
                };
                this.label2 = new Label()
                {
                    AutoSize = true,
                    Location = new System.Drawing.Point(2, 7),
                    Name = "label2" + this.name + counter.ToString(),
                    Size = new System.Drawing.Size(50, 13),
                    TabIndex = 0,
                    Text = "Удалить"
                };

                this.panel1 = new Panel()
                {
                    Cursor = System.Windows.Forms.Cursors.Hand,
                    Location = new System.Drawing.Point(0, 2),
                    Name = "panel1" + this.name + counter.ToString(),
                    Size = new System.Drawing.Size(155, 26),
                    TabIndex = 4
                };
                this.panel1.Click += new System.EventHandler(this.panel1_Click);
                this.label1.Click += new System.EventHandler(this.panel1_Click);
                this.panel1.Controls.Add(this.label1);

                this.panel2 = new Panel()
                {
                    Cursor = System.Windows.Forms.Cursors.Hand,
                    Location = new System.Drawing.Point(0, 28),
                    Name = "panel2" + this.name + counter.ToString(),
                    Size = new System.Drawing.Size(155, 26),
                    TabIndex = 4
                };
                this.panel2.Click += new System.EventHandler(this.panel2_Click);
                this.label2.Click += new System.EventHandler(this.panel2_Click);
                this.panel2.Controls.Add(this.label2);

                this.ChangeNamePanel.Controls.Add(this.panel1);
                this.ChangeNamePanel.Controls.Add(this.panel2);

                this.control.Add(this.ChangeNamePanel);
                this.ChangeNamePanel.BringToFront();

                this.ChangeNamePanel.Hide();

                this.PicturePanel = new Panel()
                {
                    BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle,
                    Location = new System.Drawing.Point(503, 80),
                    Name = "PicturePanel" + this.name + counter.ToString(),
                    Size = new System.Drawing.Size(600, 400),
                    TabIndex = 13
                };

                this.PicturePanelUp = new Panel()
                {
                    Location = new System.Drawing.Point(0, 0),
                    Name = "PicturePanelUp" + this.name + counter.ToString(),
                    Size = new System.Drawing.Size(598, 27),
                    TabIndex = 2
                };
                this.PicturePanelUp.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PicturePanelUp_MouseDown);
                this.PicturePanelUp.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PicturePanelUp_MouseMove);
                this.PicturePanelUp.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PicturePanelUp_MouseUp);

                this.PicturePanelDown = new Panel()
                {
                    Location = new System.Drawing.Point(0, 347),
                    Name = "PicturePanelDown" + this.name + counter.ToString(),
                    Size = new System.Drawing.Size(600, 52),
                    TabIndex = 38
                };

                this.PicturePanelLeft = new Panel()
                {
                    Location = new System.Drawing.Point(0, 27),
                    Name = "PicturePanelLeft" + this.name + counter.ToString(),
                    Size = new System.Drawing.Size(75, 320),
                    TabIndex = 0
                };
                this.PictureContentPanel = new PictureBox()
                {
                    BackColor = System.Drawing.Color.White,
                    Image = global::WindowsFormsApp7.Properties.Resources.white,
                    Location = new System.Drawing.Point(71, 24),
                    Name = "PictureContentPanel" + this.name + counter.ToString(),
                    Size = new System.Drawing.Size(527, 323),
                    TabIndex = 3,
                    TabStop = false
                };

                this.PicturePanel.Controls.Add(this.PicturePanelLeft);
                this.PicturePanel.Controls.Add(this.PicturePanelDown);
                this.PicturePanel.Controls.Add(this.PicturePanelUp);
                this.PicturePanel.Controls.Add(this.PictureContentPanel);

                this.PictureName = new Label()
                {
                    AutoSize = true,
                    Location = new System.Drawing.Point(8, 6),
                    Name = "PictureName" + this.name + counter.ToString(),
                    Size = new System.Drawing.Size(61, 13),
                    TabIndex = 3,
                    Text = this.text.Text
                };
                this.PictureName.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PicturePanelUp_MouseDown);
                this.PictureName.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PicturePanelUp_MouseMove);
                this.PictureName.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PicturePanelUp_MouseUp);

                this.ClosePicture = new Label()
                {
                    AutoSize = true,
                    BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle,
                    Location = new System.Drawing.Point(577, 6),
                    Name = "ClosePicture" + this.name + counter.ToString(),
                    Size = new System.Drawing.Size(16, 15),
                    TabIndex = 2,
                    Text = "X"
                };
                this.ClosePicture.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PicturePanelUp_MouseDown);
                this.ClosePicture.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PicturePanelUp_MouseMove);
                this.ClosePicture.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PicturePanelUp_MouseUp);
                this.ClosePicture.Click += this.CloseFile_Click;

                this.PicturePanelUpLine = new Panel()
                {
                    BackColor = System.Drawing.SystemColors.ActiveCaptionText,
                    Dock = System.Windows.Forms.DockStyle.Bottom,
                    Location = new System.Drawing.Point(0, 25),
                    Name = "PicturePanelUpLine" + this.name + counter.ToString(),
                    Size = new System.Drawing.Size(598, 2),
                    TabIndex = 1
                };

                this.PicturePanelUp.Controls.Add(this.PictureName);
                this.PicturePanelUp.Controls.Add(this.ClosePicture);
                this.PicturePanelUp.Controls.Add(this.PicturePanelUpLine);

                this.PencilChoose = new PictureBox()
                {
                    BackgroundImage = global::WindowsFormsApp7.Properties.Resources.Pencil,
                    BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom,
                    Cursor = System.Windows.Forms.Cursors.Hand,
                    Location = new System.Drawing.Point(12, 29),
                    Name = "PencilChoose" + this.name + counter.ToString(),
                    Size = new System.Drawing.Size(45, 42),
                    TabIndex = 7,
                    TabStop = false
                };
                this.PencilChoose.Click += new System.EventHandler(this.Pencil_Click);

                this.PencilBG = new PictureBox()
                {
                    BackColor = System.Drawing.SystemColors.ActiveCaptionText,
                    BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom,
                    Location = new System.Drawing.Point(11, 28),
                    Name = "PencilBG" + this.name + counter.ToString(),
                    Size = new System.Drawing.Size(47, 44),
                    TabIndex = 8,
                    TabStop = false
                };

                this.PencilLabel = new Label()
                {
                    AutoSize = true,
                    Location = new System.Drawing.Point(7, 75),
                    Name = "PencilLabel" + this.name + counter.ToString(),
                    Size = new System.Drawing.Size(58, 13),
                    TabIndex = 10,
                    Text = "Карандаш"
                };

                this.BrushChoose = new PictureBox()
                {
                    BackgroundImage = global::WindowsFormsApp7.Properties.Resources.Brush,
                    BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom,
                    Cursor = System.Windows.Forms.Cursors.Hand,
                    Location = new System.Drawing.Point(12, 120),
                    Name = "BrushChoose" + this.name + counter.ToString(),
                    Size = new System.Drawing.Size(45, 42),
                    TabIndex = 11,
                    TabStop = false
                };
                this.BrushChoose.Click += new System.EventHandler(this.Brush_Click);

                this.BrushBG = new PictureBox()
                {
                    BackColor = System.Drawing.SystemColors.ActiveCaptionText,
                    BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom,
                    Location = new System.Drawing.Point(11, 119),
                    Name = "BrushBG" + this.name + counter.ToString(),
                    Size = new System.Drawing.Size(47, 44),
                    TabIndex = 12,
                    TabStop = false
                };

                this.BrushLabel = new Label()
                {
                    AutoSize = true,
                    Location = new System.Drawing.Point(15, 165),
                    Name = "BrushLabel" + this.name + counter.ToString(),
                    Size = new System.Drawing.Size(37, 13),
                    TabIndex = 13,
                    Text = "Кисть"
                };

                this.LastikChoose = new PictureBox()
                {
                    BackgroundImage = global::WindowsFormsApp7.Properties.Resources.Lastik2,
                    BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom,
                    Cursor = System.Windows.Forms.Cursors.Hand,
                    Location = new System.Drawing.Point(12, 211),
                    Name = "LastikChoose" + this.name + counter.ToString(),
                    Size = new System.Drawing.Size(45, 42),
                    TabIndex = 14,
                    TabStop = false
                };
                this.LastikChoose.Click += new System.EventHandler(this.Lastik_Click);

                this.LastikBG = new PictureBox()
                {
                    BackColor = System.Drawing.SystemColors.ActiveCaptionText,
                    BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom,
                    Location = new System.Drawing.Point(11, 210),
                    Name = "LastikBG" + this.name + counter.ToString(),
                    Size = new System.Drawing.Size(47, 44),
                    TabIndex = 15,
                    TabStop = false
                };

                this.LastikLabel = new Label()
                {
                    AutoSize = true,
                    Location = new System.Drawing.Point(13, 256),
                    Name = "LastikLabel" + this.name + counter.ToString(),
                    Size = new System.Drawing.Size(44, 13),
                    TabIndex = 16,
                    Text = "Ластик"
                };

                this.PicturePanelLeftLine = new Panel()
                {
                    BackColor = System.Drawing.SystemColors.ActiveCaptionText,
                    Location = new System.Drawing.Point(72, -4),
                    Name = "panel12" + this.name + counter.ToString(),
                    Size = new System.Drawing.Size(2, 326),
                    TabIndex = 4
                };

                this.PicturePanelLeft.Controls.Add(PencilChoose);
                this.PicturePanelLeft.Controls.Add(PencilBG);
                this.PicturePanelLeft.Controls.Add(PencilLabel);

                this.PicturePanelLeft.Controls.Add(BrushChoose);
                this.PicturePanelLeft.Controls.Add(BrushBG);
                this.PicturePanelLeft.Controls.Add(BrushLabel);

                this.PicturePanelLeft.Controls.Add(LastikChoose);
                this.PicturePanelLeft.Controls.Add(LastikBG);
                this.PicturePanelLeft.Controls.Add(LastikLabel);

                this.PicturePanelLeft.Controls.Add(this.PicturePanelLeftLine);


                this.MainColor = new PictureBox()
                {
                    BackColor = System.Drawing.Color.Black,
                    BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle,
                    Location = new System.Drawing.Point(10, 8),
                    Name = "MainColor" + this.name + counter.ToString(),
                    Size = new System.Drawing.Size(40, 38),
                    TabIndex = 33,
                    TabStop = false
                };

                this.color1 = new PictureBox()
                {
                    BackColor = System.Drawing.Color.White,
                    BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle,
                    Location = new System.Drawing.Point(56, 8),
                    Name = "color1" + this.name + counter.ToString(),
                    Size = new System.Drawing.Size(16, 16),
                    TabIndex = 17,
                    TabStop = false
                };

                this.color2 = new PictureBox()
                {
                    BackColor = System.Drawing.Color.Black,
                    BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle,
                    Location = new System.Drawing.Point(77, 8),
                    Name = "color2" + this.name + counter.ToString(),
                    Size = new System.Drawing.Size(16, 16),
                    TabIndex = 17,
                    TabStop = false
                };

                this.color3 = new PictureBox()
                {
                    BackColor = System.Drawing.Color.Yellow,
                    BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle,
                    Location = new System.Drawing.Point(98, 8),
                    Name = "color3" + this.name + counter.ToString(),
                    Size = new System.Drawing.Size(16, 16),
                    TabIndex = 17,
                    TabStop = false
                };

                this.color4 = new PictureBox()
                {
                    BackColor = System.Drawing.Color.Lime,
                    BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle,
                    Location = new System.Drawing.Point(119, 8),
                    Name = "color4" + this.name + counter.ToString(),
                    Size = new System.Drawing.Size(16, 16),
                    TabIndex = 17,
                    TabStop = false
                };

                this.color5 = new PictureBox()
                {
                    BackColor = System.Drawing.Color.Aqua,
                    BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle,
                    Location = new System.Drawing.Point(140, 8),
                    Name = "color5" + this.name + counter.ToString(),
                    Size = new System.Drawing.Size(16, 16),
                    TabIndex = 17,
                    TabStop = false
                };

                this.color6 = new PictureBox()
                {
                    BackColor = System.Drawing.Color.Blue,
                    BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle,
                    Location = new System.Drawing.Point(161, 8),
                    Name = "color6" + this.name + counter.ToString(),
                    Size = new System.Drawing.Size(16, 16),
                    TabIndex = 17,
                    TabStop = false
                };

                this.color7 = new PictureBox()
                {
                    BackColor = System.Drawing.Color.Fuchsia,
                    BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle,
                    Location = new System.Drawing.Point(182, 8),
                    Name = "color7" + this.name + counter.ToString(),
                    Size = new System.Drawing.Size(16, 16),
                    TabIndex = 17,
                    TabStop = false
                };

                this.color8 = new PictureBox()
                {
                    BackColor = System.Drawing.Color.Red,
                    BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle,
                    Location = new System.Drawing.Point(203, 8),
                    Name = "color8" + this.name + counter.ToString(),
                    Size = new System.Drawing.Size(16, 16),
                    TabIndex = 17,
                    TabStop = false
                };

                this.color9 = new PictureBox()
                {
                    BackColor = System.Drawing.Color.Silver,
                    BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle,
                    Location = new System.Drawing.Point(56, 30),
                    Name = "color9" + this.name + counter.ToString(),
                    Size = new System.Drawing.Size(16, 16),
                    TabIndex = 17,
                    TabStop = false
                };

                this.color10 = new PictureBox()
                {
                    BackColor = System.Drawing.Color.DimGray,
                    BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle,
                    Location = new System.Drawing.Point(77, 30),
                    Name = "color10" + this.name + counter.ToString(),
                    Size = new System.Drawing.Size(16, 16),
                    TabIndex = 17,
                    TabStop = false
                };

                this.color11 = new PictureBox()
                {
                    BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192))))),
                    BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle,
                    Location = new System.Drawing.Point(98, 30),
                    Name = "color11" + this.name + counter.ToString(),
                    Size = new System.Drawing.Size(16, 16),
                    TabIndex = 17,
                    TabStop = false
                };

                this.color12 = new PictureBox()
                {
                    BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192))))),
                    BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle,
                    Location = new System.Drawing.Point(119, 30),
                    Name = "color12" + this.name + counter.ToString(),
                    Size = new System.Drawing.Size(16, 16),
                    TabIndex = 17,
                    TabStop = false
                };

                this.color13 = new PictureBox()
                {
                    BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255))))),
                    BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle,
                    Location = new System.Drawing.Point(140, 30),
                    Name = "color13" + this.name + counter.ToString(),
                    Size = new System.Drawing.Size(16, 16),
                    TabIndex = 17,
                    TabStop = false
                };

                this.color14 = new PictureBox()
                {
                    BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255))))),
                    BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle,
                    Location = new System.Drawing.Point(161, 30),
                    Name = "color14" + this.name + counter.ToString(),
                    Size = new System.Drawing.Size(16, 16),
                    TabIndex = 17,
                    TabStop = false
                };

                this.color15 = new PictureBox()
                {
                    BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(255))))),
                    BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle,
                    Location = new System.Drawing.Point(182, 30),
                    Name = "color15" + this.name + counter.ToString(),
                    Size = new System.Drawing.Size(16, 16),
                    TabIndex = 17,
                    TabStop = false
                };

                this.color16 = new PictureBox()
                {
                    BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192))))),
                    BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle,
                    Location = new System.Drawing.Point(203, 30),
                    Name = "color16" + this.name + counter.ToString(),
                    Size = new System.Drawing.Size(16, 16),
                    TabIndex = 17,
                    TabStop = false
                };


                this.color1.Click += color_Click;
                this.color2.Click += color_Click;
                this.color3.Click += color_Click;
                this.color4.Click += color_Click;
                this.color5.Click += color_Click;
                this.color6.Click += color_Click;
                this.color7.Click += color_Click;
                this.color8.Click += color_Click;
                this.color9.Click += color_Click;
                this.color10.Click += color_Click;
                this.color11.Click += color_Click;
                this.color12.Click += color_Click;
                this.color13.Click += color_Click;
                this.color14.Click += color_Click;
                this.color15.Click += color_Click;
                this.color16.Click += color_Click;

                this.down_vertical_panel1 = new Panel()
                {
                    BackColor = System.Drawing.SystemColors.ActiveCaptionText,
                    Location = new System.Drawing.Point(227, 1),
                    Name = "down_vertical_panel1" + this.name + counter.ToString(),
                    Size = new System.Drawing.Size(2, 55),
                    TabIndex = 5
                };

                this.SizeOfPen = new TextBox()
                {
                    Enabled = false,
                    Location = new System.Drawing.Point(238, 9),
                    Name = "SizeOfPen" + this.name + counter.ToString(),
                    Size = new System.Drawing.Size(35, 20),
                    TabIndex = 34,
                    Text = "1",
                    TextAlign = System.Windows.Forms.HorizontalAlignment.Center
                };
                this.SizeOfPen.TextChanged += SizeChanged;

                this.LabelSize = new Label()
                {
                    AutoSize = true,
                    Location = new System.Drawing.Point(232, 32),
                    Name = "LabelSize" + this.name + counter.ToString(),
                    Size = new System.Drawing.Size(46, 13),
                    TabIndex = 35,
                    Text = "Размер"
                };

                this.down_vertical_panel2 = new Panel()
                {
                    BackColor = System.Drawing.SystemColors.ActiveCaptionText,
                    Location = new System.Drawing.Point(282, 1),
                    Name = "down_vertical_panel2" + this.name + counter.ToString(),
                    Size = new System.Drawing.Size(2, 55),
                    TabIndex = 6
                };

                this.OpenPictureBtn = new Button()
                {
                    Location = new System.Drawing.Point(290, 10),
                    Name = "OpenPictureBtn" + this.name + counter.ToString(),
                    Size = new System.Drawing.Size(203, 36),
                    TabIndex = 37,
                    Text = "Открыть фотографию",
                    UseVisualStyleBackColor = true,
                };
                this.OpenPictureBtn.Click += OpenPicture_Click;

                this.down_vertical_panel3 = new Panel()
                {
                    BackColor = System.Drawing.SystemColors.ActiveCaptionText,
                    Location = new System.Drawing.Point(499, 1),
                    Name = "panel15" + this.name + counter.ToString(),
                    Size = new System.Drawing.Size(2, 55),
                    TabIndex = 7
                };

                this.ClearPictureBtn = new Button()
                {
                    Location = new System.Drawing.Point(513, 10),
                    Name = "ClearPictureBtn" + this.name + counter.ToString(),
                    Size = new System.Drawing.Size(73, 36),
                    TabIndex = 36,
                    Text = "Очистить",
                    UseVisualStyleBackColor = true,
                };
                this.ClearPictureBtn.Click += ClearPicture_Click;

                this.PicturePanelDownLine = new Panel()
                {
                    BackColor = System.Drawing.SystemColors.ActiveCaptionText,
                    Location = new System.Drawing.Point(0, 1),
                    Name = "panel9" + this.name + counter.ToString(),
                    Size = new System.Drawing.Size(600, 2),
                    TabIndex = 0
                };

                this.PicturePanelDown.Controls.Add(MainColor);
                this.PicturePanelDown.Controls.Add(color1);
                this.PicturePanelDown.Controls.Add(color2);
                this.PicturePanelDown.Controls.Add(color3);
                this.PicturePanelDown.Controls.Add(color4);
                this.PicturePanelDown.Controls.Add(color5);
                this.PicturePanelDown.Controls.Add(color6);
                this.PicturePanelDown.Controls.Add(color7);
                this.PicturePanelDown.Controls.Add(color8);
                this.PicturePanelDown.Controls.Add(color9);
                this.PicturePanelDown.Controls.Add(color10);
                this.PicturePanelDown.Controls.Add(color11);
                this.PicturePanelDown.Controls.Add(color12);
                this.PicturePanelDown.Controls.Add(color13);
                this.PicturePanelDown.Controls.Add(color14);
                this.PicturePanelDown.Controls.Add(color15);
                this.PicturePanelDown.Controls.Add(color16);

                this.PicturePanelDown.Controls.Add(this.down_vertical_panel1);

                this.PicturePanelDown.Controls.Add(this.SizeOfPen);
                this.PicturePanelDown.Controls.Add(this.LabelSize);

                this.PicturePanelDown.Controls.Add(this.down_vertical_panel2);

                this.PicturePanelDown.Controls.Add(this.OpenPictureBtn);

                this.PicturePanelDown.Controls.Add(this.down_vertical_panel3);

                this.PicturePanelDown.Controls.Add(this.ClearPictureBtn);

                this.PicturePanelDown.Controls.Add(this.PicturePanelDownLine);

                this.PictureContentPanel.MouseDown += this.PictureContentPanel_MouseDown;
                this.PictureContentPanel.MouseMove += this.PictureContentPanel_MouseMove;
                this.PictureContentPanel.MouseUp += this.PictureContentPanel_MouseUp;

                _mainControl.Add(this.PicturePanel);

                this.PicturePanel.BringToFront();

                this.PicturePanel.Hide();

            }


            private void PictureContentPanel_MouseDown(object sender, MouseEventArgs e)
            {
                IsMouseDown = true;
                CurrentPoint = e.Location;
            }

            private void PictureContentPanel_MouseMove(object sender, MouseEventArgs e)
            {
                if (IsMouseDown)
                {
                    PrevPoint = CurrentPoint;
                    CurrentPoint = e.Location;
                    Image pic = this.PictureContentPanel.Image;
                    Bitmap bmp = new Bitmap(pic);
                    g = Graphics.FromImage(bmp);                    
                    for_paint();
                    this.PictureContentPanel.Image = bmp;
                }

            }

            private void PictureContentPanel_MouseUp(object sender, MouseEventArgs e)
            {
                IsMouseDown = false;
            }

            private void ClearPicture_Click(object sender, EventArgs e)
            {
                PictureContentPanel.Image = Properties.Resources.white;
                Image pic = this.PictureContentPanel.Image;
                Bitmap bmp = new Bitmap(pic);
                g = Graphics.FromImage(bmp);
                this.PictureContentPanel.Image = bmp;
            }
            private void OpenPicture_Click(object sender, EventArgs e)
            {
                var ofd = new OpenFileDialog();
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    Bitmap bmp = new Bitmap(Image.FromFile(ofd.FileName), this.PictureContentPanel.Size);
                    PictureContentPanel.Image = bmp;
                }

            }


            private void SizeChanged(object sender, EventArgs e)
            {
                int result;
                if (Int32.TryParse(SizeOfPen.Text, out result))
                {
                    result = Convert.ToInt16(SizeOfPen.Text);
                    if (result < 1)
                        result = 1;
                    else if (result > 10)
                        result = 10;

                    width = result;
                    SizeOfPen.Text = width.ToString();
                }
                else
                {
                    SizeOfPen.Text = "1";
                }
            }

            private void color_Click(object sender, EventArgs e)
            {
                MainColor.BackColor = (sender as PictureBox).BackColor;
            }

            public void CloseFile_Click(object sender, EventArgs e)
            {
                this.PicturePanel.Hide();
                //Bitmap bmp = new Bitmap("image.EdOS");

                //this.PictureContentPanel.Image = bmp;
            }

            public void File_DoubleClick(object sender, EventArgs e)
            {
                if (!this.PicturePanel.Visible)
                    this.PicturePanel.Show();

                this.PicturePanel.BringToFront();
            }

            private void for_paint()
            {
                Color color;

                if (isPencil)
                {
                    color = Color.White;
                }
                else
                {
                    color = MainColor.BackColor;
                }
                Pen p = new Pen(color, width);
                g.DrawLine(p, PrevPoint, CurrentPoint);
            }

            private void Pencil_Click(object sender, EventArgs e)
            {
                isPencil = false;
                SizeOfPen.Enabled = false;
                width = 1;
                SizeOfPen.Text = "1";
            }

            private void Brush_Click(object sender, EventArgs e)
            {
                isPencil = false;
                SizeOfPen.Enabled = true;
                width = 3;
                SizeOfPen.Text = "3";
            }

            private void Lastik_Click(object sender, EventArgs e)
            {
                isPencil = true;
                SizeOfPen.Enabled = true;
                width = 5;
                SizeOfPen.Text = "5";
            }
            

            private void PicturePanelUp_MouseDown(object sender, MouseEventArgs e)
            {
                IsMouseDown = true;
                MouseinX = MousePosition.X - this.PicturePanel.Bounds.X;
                MouseinY = MousePosition.Y - this.PicturePanel.Bounds.Y;
                this.PicturePanel.BringToFront();
            }

            private void PicturePanelUp_MouseMove(object sender, MouseEventArgs e)
            {
                if (IsMouseDown)
                {
                    MouseX = MousePosition.X - MouseinX;
                    MouseY = MousePosition.Y - MouseinY;

                    PicturePanel.Location = new Point(MouseX, MouseY);
                }
            }

            private void PicturePanelUp_MouseUp(object sender, MouseEventArgs e)
            {
                IsMouseDown = false;
            }

            int MouseX = 0, MouseY = 0;
            int MouseinX = 0, MouseinY = 0;
            bool IsMouseDown;

            bool HideAnother = false;

            public override bool isFileUp()
            {
                return IsMouseDown;
            }

            public override Point _prevPos { get; set; }
            public override Point _newPos { get; set; }

            public override void setPos(Point _new)
            {
                this._newPos = _new;
            }

            public override Point getPos()
            {
                return this._newPos;
            }

            public override Panel getChangeNamePanel()
            {
                return ChangeNamePanel;
            }

            public override bool HideAnotherOne()
            {
                return HideAnother;
            }

            public override void setHideAnotherOne(bool _bool)
            {
                HideAnother = _bool;
            }

            public override void setChangeNamePanel(bool _visible)
            {
                ChangeNamePanel.Visible = _visible;
            }

            public override void changeLocation(Point _location)
            {
                this.picture.Location = new Point(_location.X, _location.Y);
                this.text.Location = new Point(_location.X + 2, _location.Y + 51);
            }

            public override void Content_Click(object sender, EventArgs e)
            {
                this.HideAnother = true;
            }
            public override void panel1_Click(object sender, EventArgs e)
            {
                SetNameOfFile setName = new SetNameOfFile();
                setName.FormClosed += SetNameClosedClass;
                setName.ShowDialog();
            }

            public override void SetNameClosedClass(object sender, FormClosedEventArgs e)
            {
                if ((sender as SetNameOfFile).name != "")
                {
                    this.name = (sender as SetNameOfFile).name;
                    this.text.Text = (sender as SetNameOfFile).name + ".png";
                    this.PictureName.Text = this.text.Text;
                    this.ChangeNamePanel.Hide();
                }
            }

            public override void panel2_Click(object sender, EventArgs e)
            {
                this.Delete();
                this.ChangeNamePanel.Dispose();
                this.PicturePanel.Dispose();
            }
            public override void MoveDown(object sender, MouseEventArgs e)
            {
                _getPrevGrid = new Point(this.picture.Left, this.picture.Top);
                this._prevPos = this._newPos;
                this.setHideAnotherOne(true);
                if (e.Button == MouseButtons.Left)
                {
                    IsMouseDown = true;
                    MouseinX = MousePosition.X - this.picture.Bounds.X;
                    MouseinY = MousePosition.Y - this.picture.Bounds.Y;
                    this.picture.BringToFront();
                    this.text.BringToFront();
                    this.ChangeNamePanel.Hide();
                }
                else if (e.Button == MouseButtons.Right)
                {
                    this.ChangeNamePanel.Location = new Point(this.picture.Left + 48, this.picture.Top);
                    this.setChangeNamePanel(true);
                    this.ChangeNamePanel.BringToFront();
                }
            }
            public override void MoveUp(object sender, MouseEventArgs e)
            {
                IsMouseDown = false;
                this._newPos = this.picture.Location;
                isMoved = true;
            }
            public override void MoveMove(object sender, MouseEventArgs e)
            {

                if (IsMouseDown)
                {
                    MouseX = MousePosition.X - MouseinX;
                    MouseY = MousePosition.Y - MouseinY;

                    this.picture.Location = new Point(MouseX, MouseY);
                    this.text.Location = new Point(this.picture.Location.X - 2, this.picture.Location.Y + 51);
                }
                else
                {
                    this._newPos = new Point(-100, -100);
                }
            }

            public override void Delete()
            {
                this.picture.Dispose();
                this.control.Remove(this.picture);
                this.text.Dispose();
                this.control.Remove(this.text);
                isDeleted = true;
            }

        }
        public Windows()
        {
            InitializeComponent();
            SettingsPanel.Hide();
            this.IsMdiContainer = true;
            WindowsOpenPanelIsOpen = true;
            this.LoginPicture.Parent = this.LoginBG;
            this.LabelHint.Parent = this.LoginBG;
            this.UsernameLogin.Parent = this.LoginBG;
            this.Shutdown.Parent = this.LoginBG;

            this.label47.Parent = this.BlockedMenu;
            this.label48.Parent = this.BlockedMenu;

            timer1.Interval = 1;
            //timer1.Start();
            SettingsPanel.Location = new Point(485, 120);
            BGColorComboBox.DataSource = Enum.GetNames(typeof(KnownColor));
            WinColorComboBox.DataSource = Enum.GetNames(typeof(KnownColor));
            WinPanelColComboBox.DataSource = Enum.GetNames(typeof(KnownColor));

            BGColorComboBox.SelectedIndex= BGColorComboBox.FindString("PaleTurquoise");
            WinColorComboBox.SelectedIndex = WinColorComboBox.FindString("ActiveCaptionText");
            WinPanelColComboBox.SelectedIndex = WinPanelColComboBox.FindString("DarkSlateGray");

            CreateFilesPanel.Hide();
            FolderPanel.Hide();
            PicturePanel.Hide();

            this.WebBrowserPanel.Hide();
            this.isActiveBrowser.Hide();
            this.BrowserOnPanel.BackColor = Color.Black;

            this.pictureBox3.Parent = this.DesktopBG;
            this.label19.Parent = this.DesktopBG;

            this.pictureBox6.Parent = this.DesktopBG;
            this.label20.Parent = this.DesktopBG;
        }

        public int TextFileCounter { get; set; }
        public List<Files> files;
        public List<Folder> folders;

        Point CurrentPoint;
        Point PrevPoint;
        Graphics g;

        int[,] desktopGrid;


        public string password { get; set; }
        public string passwordHint { get; set; }
        public string username { get; set; }

        private void Form1_Load(object sender, EventArgs e)
        {
            desktopGrid = new int[17, 9];
            for (int i = 0; i < 17; ++i)
                for (int j = 0; j < 9; ++j)
                {
                    desktopGrid[i, j] = 0;
                }
            TextFileCounter = 0;
            files = new List<Files>();
            folders = new List<Folder>();
            StreamReader sr;
            try
            {
                sr = new StreamReader(@"Program Files\data.edOS");
                sr.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Отсутсвует файл с данными! Проверьте целостность ED_OS");
                g = PictureContentPanel.CreateGraphics();
                return;
            }
             sr = new StreamReader(@"Program Files\data.edOS");

            try
            {
                string text = "";
                string word = "";
                bool EncodingAlive = true;
                bool goUp = false;
                int i = 0, k = 15, changing = 40;

                while (!sr.EndOfStream)
                {
                    
                    text = sr.ReadLine();
                    word = "";
                    EncodingAlive = true;
                    goUp = false;
                    i = 0;
                    k = 15;
                    changing = 40;

                    while (EncodingAlive)
                    {
                        if (i == k)
                        {

                            if (i < text.Length)
                            {
                                word += text[i];
                            }
                            else
                            {
                                EncodingAlive = false;
                                break;
                            }
                            k += changing;
                            if (!goUp)
                            {
                                if (changing < 15)
                                {
                                    goUp = true;
                                    changing += 10;
                                }
                                else
                                {
                                    changing -= 10;
                                }
                            }
                            else
                            {
                                if (changing > 35)
                                {
                                    goUp = false;
                                    changing -= 10;
                                }
                                else
                                {
                                    changing += 10;
                                }
                            }
                        }
                        i++;
                    }
                    string[] words = word.Split();
                    if (words[0] == "TextFile")
                    {
                        int start = 1;
                        string name = "";
                        if ((words[start][0] == '$') && (words[start][0] == words[start][words[start].Length - 1]))
                            name = words[start];
                        else
                        {
                            while (words[start][words[start].Length - 1] != '$')
                            {
                                name += words[start++] + " ";
                            }
                            name += words[start];
                        }
                        string realName = "";
                        for (int end = 1; end < name.Length - 1; ++end)
                            realName += name[end];
                        string content = "";
                        files.Add(new TextFile(realName, Color.FromName(BGColorComboBox.SelectedValue.ToString()), new Point(Convert.ToInt32(words[start+1]), Convert.ToInt32(words[start+2])), this.Controls, this.Controls, this.DesktopBG, TextFileCounter++));
                        for (int m = start + 3; m < words.Length; ++m)
                        {
                            if (words[m] == "♥")
                                content += "\r\n";
                            else
                                content += words[m] + " ";
                        }
                        (files[files.Count - 1] as TextFile).ChangeContentText(content);
                    }
                    else if (words[0] == "Picture")
                    {
                        int start = 1;
                        string name = "";
                        if ((words[start][0] == '$') && (words[start][0] == words[start][words[start].Length - 1]))
                            name = words[start];
                        else
                        {
                            while (words[start][words[start].Length - 1] != '$')
                            {
                                name += words[start++] + " ";
                            }
                            name += words[start];
                        }
                        string realName = "";
                        for (int end = 1; end < name.Length - 1; ++end)
                            realName += name[end];
                        files.Add(new Picture(realName, Color.FromName(BGColorComboBox.SelectedValue.ToString()), new Point(Convert.ToInt32(words[start+1]), Convert.ToInt32(words[start+2])), this.Controls, this.Controls, this.DesktopBG, TextFileCounter++));
                        FileStream fs = new FileStream(@"Program Files\" + realName + ".edOS", FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                        Bitmap bmp = new Bitmap(fs);
                        fs.Dispose();
                        fs.Close();
                        (files[files.Count - 1] as Picture).setPictureImage((Bitmap)bmp.Clone());
                        bmp.Dispose();
                    }
                    else if(words[0] == "Folder")
                    {
                        int start = 1;
                        string name = "";
                        if ((words[start][0] == '$') && (words[start][0] == words[start][words[start].Length - 1]))
                            name = words[start];
                        else
                        {
                            while (words[start][words[start].Length - 1] != '$')
                            {
                                name += words[start++] + " ";
                            }
                            name += words[start];
                        }
                        string realName = "";
                        for (int end = 1; end < name.Length - 1; ++end)
                            realName += name[end];
                        folders.Add(new Folder(realName, Color.FromName(BGColorComboBox.SelectedValue.ToString()), new Point(Convert.ToInt32(words[start+1]), Convert.ToInt32(words[start+2])), this.Controls, this.DesktopBG, TextFileCounter++));
                    }
                    else if(words[0] == "Folder:TextFile")
                    {
                        
                        int start = 1;
                        string name = "";
                        if ((words[start][0] == '$') && (words[start][0] == words[start][words[start].Length - 1]))
                            name = words[start];
                        else
                        {
                            while (words[start][words[start].Length - 1] != '$')
                            {
                                name += words[start++] + " ";
                            }
                            name += words[start];
                        }
                        string realName = "";
                        for (int end = 1; end < name.Length - 1; ++end)
                            realName += name[end];
                        string content = "";
                        folders[folders.Count - 1].files.Add(new TextFile(realName, folders[folders.Count - 1].getPanelContent().BackColor, new Point(Convert.ToInt32(words[start+1]), Convert.ToInt32(words[start+2])), folders[folders.Count - 1].getPanelContent().Controls, this.Controls, this.DesktopBG, TextFileCounter++));
                        for (int m = start + 3; m < words.Length; ++m)
                        {
                            if (words[m] == "♥")
                                content += "\r\n";
                            else
                                content += words[m] + " ";
                        }
                        (folders[folders.Count - 1].files[folders[folders.Count - 1].files.Count - 1] as TextFile).ChangeContentText(content);
                        (folders[folders.Count - 1].files[folders[folders.Count - 1].files.Count - 1] as TextFile).picture.Parent = folders[folders.Count - 1].getPanelContent();
                        (folders[folders.Count - 1].files[folders[folders.Count - 1].files.Count - 1] as TextFile).text.Parent = folders[folders.Count - 1].getPanelContent();
                        (folders[folders.Count - 1].files[folders[folders.Count - 1].files.Count - 1] as TextFile).text.ForeColor = Color.Black;
                    }
                    else if (words[0] == "Folder:Picture")
                    {
                        int start = 1;
                        string name = "";
                        if ((words[start][0] == '$') && (words[start][0] == words[start][words[start].Length - 1]))
                            name = words[start];
                        else
                        {
                            while (words[start][words[start].Length - 1] != '$')
                            {
                                name += words[start++] + " ";
                            }
                            name += words[start];
                        }
                        string realName = "";
                        for (int end = 1; end < name.Length - 1; ++end)
                            realName += name[end];
                        folders[folders.Count - 1].files.Add(new Picture(realName, folders[folders.Count - 1].getPanelContent().BackColor, new Point(Convert.ToInt32(words[start+1]), Convert.ToInt32(words[start+2])), folders[folders.Count - 1].getPanelContent().Controls, this.Controls, this.DesktopBG, TextFileCounter++));
                        (folders[folders.Count - 1].files[folders[folders.Count - 1].files.Count - 1] as Picture).picture.Parent = folders[folders.Count - 1].getPanelContent();
                        (folders[folders.Count - 1].files[folders[folders.Count - 1].files.Count - 1] as Picture).text.Parent = folders[folders.Count - 1].getPanelContent();
                        (folders[folders.Count - 1].files[folders[folders.Count - 1].files.Count - 1] as Picture).text.ForeColor = Color.Black;
                        FileStream fs = new FileStream(@"Program Files\" + folders[folders.Count - 1].name + @"\" + realName + ".edOS", FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                        Bitmap bmp = new Bitmap(fs);
                        fs.Dispose();
                        fs.Close();
                        (folders[folders.Count - 1].files[folders[folders.Count - 1].files.Count - 1] as Picture).setPictureImage((Bitmap)bmp.Clone());
                        bmp.Dispose();
                    }
                    else if (words[0] == "Password")
                    {
                        int start = 1;
                        string name = "";
                        if ((words[start][0] == '$') && (words[start][0] == words[start][words[start].Length - 1]))
                            name = words[start];
                        else
                        {
                            while (words[start][words[start].Length - 1] != '$')
                            {
                                name += words[start++] + " ";
                            }
                            name += words[start];
                        }
                        string realName = "";
                        for (int end = 1; end < name.Length - 1; ++end)
                            realName += name[end];

                        this.password = realName;
                    }
                    else if (words[0] == "PasswordHint")
                    {
                        int start = 1;
                        string name = "";
                        if ((words[start][0] == '$') && (words[start][0] == words[start][words[start].Length - 1]))
                            name = words[start];
                        else
                        {
                            while (words[start][words[start].Length - 1] != '$')
                            {
                                name += words[start++] + " ";
                            }
                            name += words[start];
                        }
                        string realName = "";
                        for (int end = 1; end < name.Length - 1; ++end)
                            realName += name[end];

                        this.passwordHint = realName;
                    }
                    else if (words[0] == "Username")
                    {
                        int start = 1;
                        string name = "";
                        if ((words[start][0] == '$') && (words[start][0] == words[start][words[start].Length - 1]))
                            name = words[start];
                        else
                        {
                            while (words[start][words[start].Length - 1] != '$')
                            {
                                name += words[start++] + " ";
                            }
                            name += words[start];
                        }
                        string realName = "";
                        for (int end = 1; end < name.Length - 1; ++end)
                            realName += name[end];

                        this.username = realName;
                    }
                }
                sr.Close();
                g = PictureContentPanel.CreateGraphics();
                this.UsernameLogin.Text = this.username;
                this.ChangeNameUsername.Text = this.username;
                this.TextUsername.Text = this.username;
                this.label46.Text = this.username;
                this.label38.Text = this.username;
                this.TextUsername.Location = new Point((216 - this.TextUsername.Size.Width) / 2 + 40, this.TextUsername.Location.Y);
                this.UsernameLogin.Location = new Point((145 - this.UsernameLogin.Size.Width) / 2 + this.LoginPicture.Location.X, this.UsernameLogin.Location.Y);
            }
            catch (Exception)
            {
                MessageBox.Show("Отсутсвует файл с данными! Проверьте целостность ED_OS");
                sr.Close();
                g = PictureContentPanel.CreateGraphics();
            }

            sr.Dispose();


            //TextFile file1 = new TextFile("ReadMe", Color.FromName(BGColorComboBox.SelectedValue.ToString()), new Point(15, 15 + (80 * TextFileCounter)), this.Controls, this.Controls, this.DesktopBG, TextFileCounter++);
            //Picture picture1 = new Picture("Photo", Color.FromName(BGColorComboBox.SelectedValue.ToString()), new Point(15,15 + (80 * TextFileCounter)), this.Controls, this.Controls, this.DesktopBG, TextFileCounter++);
            //Folder folder = new Folder("Folder", Color.FromName(BGColorComboBox.SelectedValue.ToString()), new Point(15, 15+ (80 * TextFileCounter)), this.Controls, this.DesktopBG, TextFileCounter++);

            //file1.ChangeContentText("Добро пожаловать! Это стартовый текстовый документ, который будет появляется каждый раз, при запуске виндовса, пока я не найду способ сохранять данные, чтобы при перезапуске приложения, все данные сохранялись!\r\n" +
            //    "Ну а пока можете насладиться тем, что есть. Поклацайте, поперемещайте, поизменяйте, порисуйте как никак)");
            //files.Add(file1);
            //picture1.setPictureImage(Properties.Resources.Loading);
            //files.Add(picture1);

            //folders.Add(folder);
            //folder.files.Add(new TextFile("LookMe", folder.getPanelContent().BackColor, new Point(15, 15), folder.getPanelContent().Controls, this.Controls, this.DesktopBG, TextFileCounter++));
            //(folder.files[0] as TextFile).ChangeContentText("Очень странно, что ты меня нашёл...");
            //(folder.files[0] as TextFile).picture.Parent = folder.getPanelContent();
            //(folder.files[0] as TextFile).text.Parent = folder.getPanelContent();
            //(folder.files[0] as TextFile).text.ForeColor = Color.Black;

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            TimeLable.Text = DateTime.Now.ToShortTimeString();
            DateLabel.Text = DateTime.Now.ToShortDateString();

            Language.Text = InputLanguage.CurrentInputLanguage.Culture.ThreeLetterISOLanguageName.ToUpper();

            List <Files> DeletedFiles= new List<Files>();
            List<Files> DeletedFilesInFolders = new List<Files>();
            List<Folder> DeletedFolders = new List<Folder>();
            for (int i = 0; i < files.Count; ++i)
            {
                if (files[i].GetStatus())
                    DeletedFiles.Add(files[i]);
            }
            for (int i = 0; i < folders.Count; ++i)
            {
                if (folders[i].GetStatus())
                    DeletedFolders.Add(folders[i]);
                for(int j = 0; j < folders[i].files.Count; ++j)
                {
                    if (folders[i].files[j].GetStatus())
                        DeletedFilesInFolders.Add(folders[i].files[j]);
                }
            }
            for (int i = 0; i < DeletedFiles.Count; ++i)
            {
                files.Remove(DeletedFiles[i]);
            }
            for (int i = 0; i < DeletedFolders.Count; ++i)
            {
                folders.Remove(DeletedFolders[i]);
            }
            for(int i = 0; i < folders.Count; ++i)
            {
                for(int j = 0; j < DeletedFilesInFolders.Count; ++j)
                    if(folders[i].files.Contains(DeletedFilesInFolders[j]))
                    {
                        folders[i].files.Remove(DeletedFilesInFolders[j]);
                    }
            }
            foreach (var i in files)
            {
                if(i.HideAnotherOne())
                {
                    foreach (var j in files)
                    {
                        if(i!=j)
                        {
                            j.setChangeNamePanel(false);
                            j.setHideAnotherOne(false);
                        }
                    }
                    foreach (Folder j in folders)
                    {
                        j.setCreateFilesPanel(false);
                        j.setChangeNamePanel(false);
                        j.setHideAnotherOne(false);
                    }
                    i.setHideAnotherOne(false);
                    CreateFilesPanel.Hide();
                    break;
                }
            }
            foreach (Folder i in folders)
            {
                if(i.HideAnotherOne())
                {
                    foreach (Folder j in folders)
                    {
                        if (i != j)
                        {
                            j.setCreateFilesPanel(false);
                            j.setChangeNamePanel(false);
                            j.setHideAnotherOne(false);
                        }
                    }
                    foreach (var j in files)
                    {
                            j.setChangeNamePanel(false);
                            j.setHideAnotherOne(false);
                    }
                    i.setHideAnotherOne(false);
                    CreateFilesPanel.Hide();
                    break;
                }
            }


           
            bool isBreak = false;
                
            foreach (Folder i in folders)
            {
                foreach (var j in files)
                {
                    if (i.picture.Bounds.Contains(j.getPos()))
                    {
                        i.getPanelContent().Controls.Add(j.picture);
                        i.getPanelContent().Controls.Add(j.text);
                        i.getPanelContent().Controls.Add(j.ChangeNamePanel);
                        i.files.Add(j);
                        j.picture.Parent = i.getPanelContent();
                        j.text.Parent = i.getPanelContent();
                        j.text.ForeColor = Color.Black;
                        j.setPos(new Point(-100, -100));
                        this.Controls.Remove(j.picture);
                        this.Controls.Remove(j.text);
                        this.Controls.Remove(j.ChangeNamePanel);
                        j.control = i.getPanelContent().Controls;
                        this.desktopGrid[j.GetPrevGrid().X / 75, j.GetPrevGrid().Y / 75] = 0;
                        j.changeLocation(new Point(15 + (64 * (i.files.Count - 1)), 15));
                        TextFileCounter--;
                        int counter = 0;
                        foreach (var m in i.files)
                        {
                            m.changeLocation(new Point(15 + (64 * counter), 15));
                            counter++;
                        }
                        files.Remove(j);
                        //updatePosition();
                        j.setMoved(false);
                        isBreak = true;
                        break;
                    }

                }
                if (isBreak)
                    break;
                foreach (var j in i.files)
                {
                    if (j.picture.Location.X < -20 || j.picture.Location.X > 640 || j.picture.Location.Y < -20 || j.picture.Location.Y > 320)
                    {
                        if (!j.isFileUp())
                        {
                            i.files.Remove(j);
                            if(!files.Contains(j))
                                files.Add(j);
                            this.Controls.Add(j.picture);
                            this.Controls.Add(j.text);
                            this.Controls.Add(j.ChangeNamePanel);
                            j.picture.Parent = this.DesktopBG;
                            j.text.Parent = this.DesktopBG;
                            j.text.ForeColor = Color.White;
                            j.control = this.Controls;
                            bool isBreakGrid = false;
                            for (int m = 0; m < 17; m++)
                            {
                                for (int n = 0; n < 9; ++n)
                                {
                                    if (desktopGrid[m, n] == 0)
                                    {
                                        j.changeLocation(new Point(m * 75 + 5, n * 75 + 5));
                                        desktopGrid[m, n] = 1;
                                        isBreakGrid = true;
                                        break;
                                    }
                                }
                                if (isBreakGrid)
                                    break;
                            }
                            j.picture.BringToFront();
                            j.text.BringToFront();
                            j.setMoved(false);
                            int counter = 0;
                            foreach (var m in i.files)
                            {
                                m.changeLocation(new Point(15 + (64 * counter), 15));
                                counter++;
                            }
                            //updatePosition();
                            isBreak = true;
                            break;
                        }
                    }
                }

                if (isBreak)
                    break;
            }
            isBreak = false;

            foreach (var i in files)
            {
                if(i.GetMoved())
                {
                    if (this.desktopGrid[i.picture.Left / 75, i.picture.Top / 75] == 0)
                    {
                        this.desktopGrid[i.GetPrevGrid().X / 75, i.GetPrevGrid().Y / 75] = 0;
                        i.changeLocation(new Point((i.picture.Left / 75) * 75 + 5, (i.picture.Top / 75) * 75 + 5));
                        this.desktopGrid[i.picture.Left / 75, i.picture.Top / 75] = 1;
                    }
                    else
                    {
                        i.changeLocation(new Point((i.GetPrevGrid().X / 75) * 75 + 5, ((i.GetPrevGrid().Y / 75) * 75 + 5)));
                    }
                    i.setMoved(false);
                    break;
                }
            }
            foreach (var i in folders)
            {
                if (i.GetMoved())
                {
                    if (this.desktopGrid[i.picture.Left / 75, i.picture.Top / 75] == 0)
                    {
                        this.desktopGrid[i.GetPrevGrid().X / 75, i.GetPrevGrid().Y / 75] = 0;
                        i.changeLocation(new Point((i.picture.Left / 75) * 75 + 5, (i.picture.Top / 75) * 75 + 5));
                        this.desktopGrid[i.picture.Left / 75, i.picture.Top / 75] = 1;
                    }
                    else
                    {
                        i.changeLocation(new Point((i.GetPrevGrid().X / 75) * 75 + 5, ((i.GetPrevGrid().Y / 75) * 75 + 5)));
                    }
                    i.setMoved(false);
                    break;
                    //MessageBox.Show("клетка до: " + (i.GetPrevGrid().X / 75).ToString() + "," + (i.GetPrevGrid().Y / 75).ToString());
                    //MessageBox.Show("клетка после: " + (i.picture.Left / 75).ToString() + "," + (i.picture.Top / 75).ToString());
                }
            }


        }

        private void updatePosition()
        {
            int count = 0;

            foreach (var i in files)
            {
                i.changeLocation(new Point(15, 15 + (75 * count)));
                count++;
            }
            foreach (var i in folders)
            {
                i.changeLocation(new Point(5, 15 + (75 * count)));
                count++;
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            WindowsOpenPanel.Visible = !WindowsOpenPanel.Visible;
            while (WindowsOpenPanelIsOpen)
            {
                if (WindowsOpenPanel.Location.Y > (this.Height - 291))
                {
                    WindowsOpenPanel.Location = new Point(WindowsOpenPanel.Location.X, WindowsOpenPanel.Location.Y - 1);
                    continue;
                }
                WindowsOpenPanelIsOpen = false;
                return;
            }

            while (!WindowsOpenPanelIsOpen)
            {
                if (WindowsOpenPanel.Location.Y < (this.Height + 291))
                {
                    WindowsOpenPanel.Location = new Point(WindowsOpenPanel.Location.X, WindowsOpenPanel.Location.Y + 1);
                    continue;
                }
                WindowsOpenPanelIsOpen = true;
                return;
            }



        }

        private void button2_Click(object sender, EventArgs e)
        {

            //for (int i = 0; i < 256; ++i)
            //{
            //    if (i == 15)
            //    {
            //        sw.Write('T');
            //    }
            //    else if( i == 55)
            //    {
            //        sw.Write('e');
            //    }
            //    else if (i == 85)
            //    {
            //        sw.Write('x');
            //    }
            //    else if (i == 105)
            //    {
            //        sw.Write('t');
            //    }
            //    else if (i == 115)
            //    {
            //        sw.Write('F');
            //    }
            //    else if (i == 135)
            //    {
            //        sw.Write('i');
            //    }
            //    else if (i == 165)
            //    {
            //        sw.Write('l');
            //    }
            //    else if (i == 205)
            //    {
            //        sw.Write('e');
            //    }
            //    else
            //    {
            //        int number = x.Next(0, 52);
            //        sw.Write(symbols[number]);
            //    }
            //}

            if (Directory.Exists("Program Files"))
            {
                Directory.Delete("Program Files", true);
                Directory.CreateDirectory("Program Files");
            }
            else
            {
                Directory.CreateDirectory("Program Files");
            }
            StreamWriter sw = new StreamWriter(@"Program Files\data.edOS");
            string symbols = "AaBbCcDdEeFfGgHhIiJjKkLlMmNnOoPpQqRrSsTtUuVvWwXxYyZz";
            Random x = new Random();
            bool EncodingAlive = true;
            bool goUp = false;
            string word = "";
            int index = 0;
            int i = 0, k = 15, changing = 40;


            //---Saving Password---//
            EncodingAlive = true;

            word = "Password $" + this.password + "$ ";

            while (EncodingAlive)
            {
                if (i == k)
                {
                    if (index == word.Length)
                    {
                        EncodingAlive = false;
                        break;
                    }
                    else
                        sw.Write(word[index++]);

                    k += changing;
                    if (!goUp)
                    {
                        if (changing < 15)
                        {
                            goUp = true;
                            changing += 10;
                        }
                        else
                        {
                            changing -= 10;
                        }
                    }
                    else
                    {
                        if (changing > 35)
                        {
                            goUp = false;
                            changing -= 10;
                        }
                        else
                        {
                            changing += 10;
                        }
                    }
                }
                else
                {
                    int number = x.Next(0, 52);
                    sw.Write(symbols[number]);
                }
                i++;
            }
            sw.WriteLine();
            //---Saving Password---//


            //---Saving PasswordHint---//
            EncodingAlive = true;
            index = 0;
            i = 0;
            k = 15;
            changing = 40;

            word = "PasswordHint $" + this.passwordHint + "$ ";

            while (EncodingAlive)
            {
                if (i == k)
                {
                    if (index == word.Length)
                    {
                        EncodingAlive = false;
                        break;
                    }
                    else
                        sw.Write(word[index++]);

                    k += changing;
                    if (!goUp)
                    {
                        if (changing < 15)
                        {
                            goUp = true;
                            changing += 10;
                        }
                        else
                        {
                            changing -= 10;
                        }
                    }
                    else
                    {
                        if (changing > 35)
                        {
                            goUp = false;
                            changing -= 10;
                        }
                        else
                        {
                            changing += 10;
                        }
                    }
                }
                else
                {
                    int number = x.Next(0, 52);
                    sw.Write(symbols[number]);
                }
                i++;
            }
            sw.WriteLine();
            //---Saving PasswordHint---//

            //---Saving Username---//
            EncodingAlive = true;
            index = 0;
            i = 0;
            k = 15;
            changing = 40;

            word = "Username $" + this.username + "$ ";

            while (EncodingAlive)
            {
                if (i == k)
                {
                    if (index == word.Length)
                    {
                        EncodingAlive = false;
                        break;
                    }
                    else
                        sw.Write(word[index++]);

                    k += changing;
                    if (!goUp)
                    {
                        if (changing < 15)
                        {
                            goUp = true;
                            changing += 10;
                        }
                        else
                        {
                            changing -= 10;
                        }
                    }
                    else
                    {
                        if (changing > 35)
                        {
                            goUp = false;
                            changing -= 10;
                        }
                        else
                        {
                            changing += 10;
                        }
                    }
                }
                else
                {
                    int number = x.Next(0, 52);
                    sw.Write(symbols[number]);
                }
                i++;
            }
            sw.WriteLine();
            //---Saving Username---//


            foreach (var File in files)
            {
                EncodingAlive = true;

                if (File is TextFile)
                {
                    word = "TextFile $" + File.name + "$ " + File.picture.Left + " " + File.picture.Top + " " + (File as TextFile).getContentText().Replace(Environment.NewLine, " ♥ ");
                }
                else if (File is Picture)
                {
                    Bitmap bmp = (Bitmap)(File as Picture).getPictureImage().Image;
                    bmp.Save(@"Program Files\" + File.name + ".edOS");
                    bmp.Dispose();
                    word = "Picture $" + File.name + "$ " + File.picture.Left + " " + File.picture.Top;
                }

                index = 0;
                i = 0;
                k = 15;
                changing = 40;
                while (EncodingAlive)
                {
                    if (i == k)
                    {
                        if (index == word.Length)
                        {
                            EncodingAlive = false;
                            break;
                        }
                        else
                            sw.Write(word[index++]);

                        k += changing;
                        if (!goUp)
                        {
                            if (changing < 15)
                            {
                                goUp = true;
                                changing += 10;
                            }
                            else
                            {
                                changing -= 10;
                            }
                        }
                        else
                        {
                            if (changing > 35)
                            {
                                goUp = false;
                                changing -= 10;
                            }
                            else
                            {
                                changing += 10;
                            }
                        }
                    }
                    else
                    {
                        int number = x.Next(0, 52);
                        sw.Write(symbols[number]);
                    }
                    i++;
                }
                sw.WriteLine();
            }
            foreach (var Folder in folders)
            {
                EncodingAlive = true;

                word = "Folder $" + Folder.name + "$ " + Folder.picture.Left + " " + Folder.picture.Top;

                index = 0;
                i = 0;
                k = 15;
                changing = 40;
                while (EncodingAlive)
                {
                    if (i == k)
                    {
                        if (index == word.Length)
                        {
                            EncodingAlive = false;
                            break;
                        }
                        else
                            sw.Write(word[index++]);

                        k += changing;
                        if (!goUp)
                        {
                            if (changing < 15)
                            {
                                goUp = true;
                                changing += 10;
                            }
                            else
                            {
                                changing -= 10;
                            }
                        }
                        else
                        {
                            if (changing > 35)
                            {
                                goUp = false;
                                changing -= 10;
                            }
                            else
                            {
                                changing += 10;
                            }
                        }
                    }
                    else
                    {
                        int number = x.Next(0, 52);
                        sw.Write(symbols[number]);
                    }
                    i++;
                }
                sw.WriteLine();

                foreach (var File in Folder.files)
                {
                    bool EncodingAlive2 = true;
                    bool goUp2 = false;

                    if (File is TextFile)
                    {
                        word = "Folder:TextFile $" + File.name + "$ " + File.picture.Left + " " + File.picture.Top + " " + (File as TextFile).getContentText().Replace(Environment.NewLine, " ♥ ");
                    }
                    else if (File is Picture)
                    {
                        word = "Folder:Picture $" + File.name + "$ " + File.picture.Left + " " + File.picture.Top + " PICTURE_BMP";
                        Directory.CreateDirectory(@"Program Files\" + Folder.name);
                        Bitmap bmp = (Bitmap)(File as Picture).getPictureImage().Image;
                        bmp.Save(@"Program Files\" + Folder.name + @"\" + File.name + ".edOS");
                        bmp.Dispose();
                    }

                    index = 0;
                    i = 0;
                    k = 15;
                    changing = 40;
                    while (EncodingAlive2)
                    {
                        if (i == k)
                        {
                            if (index == word.Length)
                            {
                                EncodingAlive2 = false;
                                break;
                            }
                            else
                                sw.Write(word[index++]);

                            k += changing;
                            if (!goUp2)
                            {
                                if (changing < 15)
                                {
                                    goUp2 = true;
                                    changing += 10;
                                }
                                else
                                {
                                    changing -= 10;
                                }
                            }
                            else
                            {
                                if (changing > 35)
                                {
                                    goUp2 = false;
                                    changing -= 10;
                                }
                                else
                                {
                                    changing += 10;
                                }
                            }
                        }
                        else
                        {
                            int number = x.Next(0, 52);
                            sw.Write(symbols[number]);
                        }
                        i++;
                    }
                    sw.WriteLine();
                }
            }
            //word = "TextFile " + files[0].name + " " + files[0].location.X + " " + files[0].location.Y + " " + (files[0] as TextFile).getContentText().Replace(Environment.NewLine, "♥");
            //int index = 0;
            //int i = 0, k = 15, changing = 40;
            //while(EncodingAlive)
            //{
            //    if( i == k)
            //    {
            //        if (index == word.Length)
            //        {
            //            EncodingAlive =false;
            //            break;
            //        }
            //        else
            //            sw.Write(word[index++]);

            //        k += changing;
            //        if (!goUp)
            //        {
            //            if (changing < 15)
            //            {
            //                goUp = true;
            //                changing += 10;
            //            }
            //            else
            //            {
            //                changing -= 10;
            //            }
            //        }
            //        else
            //        {
            //            if (changing > 35)
            //            {
            //                goUp = false;
            //                changing -= 10;
            //            }
            //            else
            //            {
            //                changing += 10;
            //            }
            //        }
            //    }
            //    else
            //    {
            //        int number = x.Next(0, 52);
            //        sw.Write(symbols[number]);
            //    }
            //    i++;
            //}
            sw.Close();
            sw.Dispose();
            Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!SettingsPanel.Visible)
            {
                SettingsPanel.Show();
                SettingsPanel.Location = new Point(485, 120);
            }

            while (WindowsOpenPanelIsOpen)
            {
                if (WindowsOpenPanel.Location.Y > (this.Height - 291))
                {
                    WindowsOpenPanel.Location = new Point(WindowsOpenPanel.Location.X, WindowsOpenPanel.Location.Y - 1);
                    continue;
                }
                WindowsOpenPanelIsOpen = false;
                return;
            }
        }

        

        private void BGColorComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            BGColorSee.BackColor = Color.FromName(BGColorComboBox.SelectedValue.ToString());
           
        }

        private void WinColorComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            WinColorSee.BackColor = Color.FromName(WinColorComboBox.SelectedValue.ToString());
        }

        private void WinPanelColComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            WinPanelColorSee.BackColor = Color.FromName(WinPanelColComboBox.SelectedValue.ToString());
        }

        private void ApplySettings_Click(object sender, EventArgs e)
        {
            this.BackColor = Color.FromName(BGColorComboBox.SelectedValue.ToString());
            WindowsBG.BackColor = Color.FromName(WinColorComboBox.SelectedValue.ToString());
            WindowsOpenPanel.BackColor = Color.FromName(WinPanelColComboBox.SelectedValue.ToString());
        }

        private void label5_Click(object sender, EventArgs e)
        {
            SettingsPanel.Hide();
        }

        private void SettingsTopPanel_MouseDown(object sender, MouseEventArgs e)
        {
            IsMouseDown = true;
            MouseinX = MousePosition.X - SettingsPanel.Bounds.X;
            MouseinY = MousePosition.Y - SettingsPanel.Bounds.Y;
        }

        private void DesktopBG_DoubleClick(object sender, EventArgs e)
        {
        }

        private void CreateTextFile_Click(object sender, EventArgs e)
        {
            SetNameOfFile setName = new SetNameOfFile();
            setName.FormClosed += SetNameClosed;
            setName.ShowDialog();
            CreateFilesPanel.Hide();
        }

        private void SetNameClosed(object sender, FormClosedEventArgs e)
        {
            if ( (sender as SetNameOfFile).name != "")
            {
                TextFile file = new TextFile((sender as SetNameOfFile).name, Color.FromName(BGColorComboBox.SelectedValue.ToString()), new Point(CreateFilesPanel.Location.X, CreateFilesPanel.Location.Y), this.Controls, this.Controls, this.DesktopBG, TextFileCounter++);
                files.Add(file);
            }
        }

        private void DesktopBG_MouseDown(object sender, MouseEventArgs e)
        {
            foreach (var j in files)
            {
                j.setChangeNamePanel(false);
                j.setHideAnotherOne(false);
            }
            foreach (Folder j in folders)
            {
                j.setCreateFilesPanel(false);
                j.setChangeNamePanel(false);
                j.setHideAnotherOne(false);
            }
            if (e.Button == MouseButtons.Right)
            {
                CreateFilesPanel.Show();
                CreateFilesPanel.Location = new Point(e.Location.X, e.Location.Y);
                CreateFilesPanel.BringToFront();
            }
            else if(e.Button == MouseButtons.Left)
            {
                CreateFilesPanel.Hide();
            }
                
        }

        private void panel2_Click(object sender, EventArgs e)
        {

        }

        private void panel3_Click(object sender, EventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void FolderPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void CreateFolder_Click(object sender, EventArgs e)
        {
            SetNameOfFile setName = new SetNameOfFile();
            setName.FormClosed += SetNameClosedFolder;
            setName.ShowDialog();
            CreateFilesPanel.Hide();
        }

        private void SetNameClosedFolder(object sender, FormClosedEventArgs e)
        {
            if ((sender as SetNameOfFile).name != "")
            {
                Folder folder = new Folder((sender as SetNameOfFile).name, Color.FromName(BGColorComboBox.SelectedValue.ToString()), new Point(CreateFilesPanel.Location.X, CreateFilesPanel.Location.Y), this.Controls, this.DesktopBG, TextFileCounter++);
                folders.Add(folder);
            }
        }

        private void pictureBox3_MouseDown(object sender, MouseEventArgs e)
        {
            IsMouseDown = true;
            CurrentPoint = e.Location;
        }

        private void pictureBox3_MouseMove(object sender, MouseEventArgs e)
        {
            if (IsMouseDown)
            {
                PrevPoint = CurrentPoint;
                CurrentPoint = e.Location;
                for_paint();
            }

        }

        private void pictureBox3_MouseUp(object sender, MouseEventArgs e)
        {
            IsMouseDown = false;
        }
        bool isPencil = false;
        private void for_paint()
        {
            Color color;

            if (isPencil)
            {
                color = Color.White;
            }
            else
            {
                color = MainColor.BackColor;
            }
            Pen p = new Pen(color, width);
            g.DrawLine(p, PrevPoint, CurrentPoint);
        }

        private void panel11_MouseDown(object sender, MouseEventArgs e)
        {
            IsMouseDown = true;
            MouseinX = MousePosition.X - this.PicturePanel.Bounds.X;
            MouseinY = MousePosition.Y - this.PicturePanel.Bounds.Y;
        }

        private void panel11_MouseMove(object sender, MouseEventArgs e)
        {
            if (IsMouseDown)
            {
                MouseX = MousePosition.X - MouseinX;
                MouseY = MousePosition.Y - MouseinY;

                PicturePanel.Location = new Point(MouseX, MouseY);
            }
        }

        private void panel11_MouseUp(object sender, MouseEventArgs e)
        {
            IsMouseDown = false;
        }

        private void pictureBox3_Paint(object sender, PaintEventArgs e)
        {
        }

        private void pictureBox12_Click(object sender, EventArgs e)
        {
            MainColor.BackColor = Color.White;
        }

        private void pictureBox13_Click(object sender, EventArgs e)
        {
            MainColor.BackColor = Color.Black;
        }

        private void pictureBox14_Click(object sender, EventArgs e)
        {
            MainColor.BackColor = Color.Yellow;
        }

        private void pictureBox15_Click(object sender, EventArgs e)
        {
            MainColor.BackColor = Color.Lime;
        }

        private void pictureBox16_Click(object sender, EventArgs e)
        {
            MainColor.BackColor = Color.Aqua;
        }

        private void pictureBox17_Click(object sender, EventArgs e)
        {
            MainColor.BackColor = Color.Blue;
        }

        private void pictureBox18_Click(object sender, EventArgs e)
        {
            MainColor.BackColor = Color.Fuchsia;
        }

        private void pictureBox21_Click(object sender, EventArgs e)
        {
            MainColor.BackColor = Color.Red;
        }

        private void pictureBox27_Click(object sender, EventArgs e)
        {
            MainColor.BackColor = Color.Silver;
        }

        private void pictureBox26_Click(object sender, EventArgs e)
        {
            MainColor.BackColor = Color.DimGray;
        }

        private void pictureBox25_Click(object sender, EventArgs e)
        {
            MainColor.BackColor = Color.FromArgb(255, 255, 192);
        }

        private void pictureBox24_Click(object sender, EventArgs e)
        {
            MainColor.BackColor = Color.FromArgb(192, 255, 192);
        }

        private void pictureBox23_Click(object sender, EventArgs e)
        {
            MainColor.BackColor = Color.FromArgb(192, 255, 255);
        }

        private void pictureBox22_Click(object sender, EventArgs e)
        {
            MainColor.BackColor = Color.FromArgb(192, 192, 255);
        }

        private void pictureBox20_Click(object sender, EventArgs e)
        {
            MainColor.BackColor = Color.FromArgb(255, 192, 255);
        }

        private void pictureBox19_Click(object sender, EventArgs e)
        {
            MainColor.BackColor = Color.FromArgb(255, 192, 192);
        }


        public int width { get; set; }
        private void pictureBox6_Click(object sender, EventArgs e)
        {
            isPencil = false;
            SizeOfPen.Enabled = false;
            width = 1;
            SizeOfPen.Text = "1";
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            isPencil = false;
            SizeOfPen.Enabled = true;
            width = 3;
            SizeOfPen.Text = "3";
        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            isPencil = true;
            SizeOfPen.Enabled = true;
            width = 5;
            SizeOfPen.Text = "5";
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            int result;
            if(Int32.TryParse(SizeOfPen.Text,out result))
            {
                result = Convert.ToInt16(SizeOfPen.Text);
                if (result < 1)
                    result = 1;
                else if (result > 10)
                    result = 10;

                width = result;
                SizeOfPen.Text = width.ToString();
            }
            else
            {
                SizeOfPen.Text = "1";
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                PictureContentPanel.BackgroundImage = Image.FromFile(ofd.FileName);
            }
            
        
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if(PictureContentPanel.BackgroundImage != null)
                PictureContentPanel.BackgroundImage = null;
            else
            {
                g.Clear(Color.White);
            }
        }

        private void ClosePicture_Click(object sender, EventArgs e)
        {
            PicturePanel.Hide();
        }

        private void CreateImage_Click(object sender, EventArgs e)
        {
            SetNameOfFile setName = new SetNameOfFile();
            setName.FormClosed += SetNameClosedPicture;
            setName.ShowDialog();
            CreateFilesPanel.Hide();
        }

        private void SetNameClosedPicture(object sender, FormClosedEventArgs e)
        {
            if ((sender as SetNameOfFile).name != "")
            {
                Picture picture = new Picture((sender as SetNameOfFile).name, Color.FromName(BGColorComboBox.SelectedValue.ToString()), new Point(CreateFilesPanel.Location.X, CreateFilesPanel.Location.Y), this.Controls, this.Controls, this.DesktopBG, TextFileCounter++);
                files.Add(picture);
            }
        }
        int tabs = 0;
        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            WebBrowser web = new WebBrowser()
            {
                Visible = true,
                ScriptErrorsSuppressed = true,
                Dock = DockStyle.Fill,
            };
            web.DocumentCompleted += Web_DocumentCompleted;
            tabControl1.TabPages.Add("New Page");
            tabControl1.SelectTab(tabs++);
            tabControl1.SelectedTab.Controls.Add(web);
            ((WebBrowser)tabControl1.SelectedTab.Controls[0]).Navigate("bing.com");
        }

        private void Web_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            tabControl1.SelectedTab.Text = ((WebBrowser)tabControl1.SelectedTab.Controls[0]).DocumentTitle;
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            if(SearchTextBox.Text != "")
                ((WebBrowser)tabControl1.SelectedTab.Controls[0]).Navigate(SearchTextBox.Text);
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            ((WebBrowser)tabControl1.SelectedTab.Controls[0]).GoBack();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            ((WebBrowser)tabControl1.SelectedTab.Controls[0]).GoForward();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            ((WebBrowser)tabControl1.SelectedTab.Controls[0]).Refresh();
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            ((WebBrowser)tabControl1.SelectedTab.Controls[0]).Dispose();
            tabControl1.Controls.RemoveAt(tabControl1.SelectedIndex);
            if (tabControl1.TabPages.Count > 0)
            {
                tabControl1.SelectTab(tabControl1.TabPages.Count - 1);
                tabs--;
            }
            else
            {
                WebBrowser web = new WebBrowser()
                {
                    Visible = true,
                    ScriptErrorsSuppressed = true,
                    Dock = DockStyle.Fill,
                };
                web.DocumentCompleted += Web_DocumentCompleted;
                tabControl1.TabPages.Add("New Page");
                tabControl1.SelectedTab.Controls.Add(web);
                ((WebBrowser)tabControl1.SelectedTab.Controls[0]).Navigate("bing.com");

            }

        }

        private void label17_Click(object sender, EventArgs e)
        {
            this.WebBrowserPanel.Location = new Point(0, 0);
            this.WebBrowserPanel.Size = new Size(this.Size.Width, this.Size.Height - 44);
            this.WebBrowserPanel.BringToFront();
            this.label18.Left = this.WebBrowserPanel.Size.Width - 70;
            this.label17.Left = this.WebBrowserPanel.Size.Width - 46;
            this.label14.Left = this.WebBrowserPanel.Size.Width - 24;
            this.SearchTextBox.Size = new System.Drawing.Size(this.WebBrowserPanel.Size.Width - 190, 25);
        }

        private void WebBrowserPanel_MouseMove(object sender, MouseEventArgs e)
        {
            MouseinX = this.WebBrowserPanel.Left;
            MouseinY = this.WebBrowserPanel.Top;
            MouseX = MousePosition.X - MouseinX;
            MouseY = MousePosition.Y - MouseinY;

            if (((MouseX - this.Location.X) > (this.WebBrowserPanel.Width - 5)) && ((MouseY - this.Location.Y) > (this.WebBrowserPanel.Height - 5)))
            {
                this.WebBrowserPanel.Cursor = Cursors.SizeNWSE;
                //MessageBox.Show((MouseX - 326).ToString() + " " + (MouseY - 189).ToString());
            }
            else
            {
                this.WebBrowserPanel.Cursor = Cursors.Default;
            }
        }

        private void WebBrowserPanel_MouseUp(object sender, MouseEventArgs e)
        {
            if (IsMouseDown)
            {
                MouseinX = this.WebBrowserPanel.Left;
                MouseinY = this.WebBrowserPanel.Top;
                MouseX = MousePosition.X - MouseinX;
                MouseY = MousePosition.Y - MouseinY;
                this.WebBrowserPanel.Size = new Size(MouseX - this.Location.X, MouseY - this.Location.Y);
                this.label18.Left = this.WebBrowserPanel.Size.Width - 70;
                this.label17.Left = this.WebBrowserPanel.Size.Width - 46;
                this.label14.Left = this.WebBrowserPanel.Size.Width - 24;
                this.SearchTextBox.Size = new System.Drawing.Size(this.WebBrowserPanel.Size.Width - 200, 25);
                IsMouseDown = false;
            }
        }

        private void WebBrowserPanel_MouseDown(object sender, MouseEventArgs e)
        {
            if(this.WebBrowserPanel.Cursor == Cursors.SizeNWSE)
            {
                IsMouseDown = true;
            }
        }

        private void panel11_MouseMove_1(object sender, MouseEventArgs e)
        {
            if (IsMouseDown)
            {
                MouseX = MousePosition.X - MouseinX;
                MouseY = MousePosition.Y - MouseinY;

                this.WebBrowserPanel.Location = new Point(MouseX, MouseY);
            }
        }

        private void panel11_MouseUp_1(object sender, MouseEventArgs e)
        {
            IsMouseDown = false;
        }

        private void panel11_MouseDown_1(object sender, MouseEventArgs e)
        {
            IsMouseDown = true;
            MouseinX = MousePosition.X - this.WebBrowserPanel.Bounds.X;
            MouseinY = MousePosition.Y - this.WebBrowserPanel.Bounds.Y;
            this.WebBrowserPanel.BringToFront();
        }

        private void label14_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < tabControl1.TabPages.Count; ++i)
            {
                try
                {
                    //tabControl1.SelectedTab.Dispose();
                    tabControl1.TabPages[i].Dispose();
                    tabs--;
                }
                catch (Exception)
                {
                    MessageBox.Show("Error");
                }
                
            }
            tabControl1.TabPages.Clear();
            this.tabControl1.Controls.Clear();
            GC.Collect(100);
            tabs = 0;
            BrowserOnPanel.BackColor = WindowsBG.BackColor;
            isActiveBrowser.Hide();
            this.WebBrowserPanel.Hide();
        }

        private void label18_Click(object sender, EventArgs e)
        {
            this.WebBrowserPanel.Hide();
        }

        private void pictureBox3_DoubleClick(object sender, EventArgs e)
        {
            this.WebBrowserPanel.Show();
            if (tabControl1.Controls.Count == 0)
            {
                BrowserOnPanel.BackColor = Color.DimGray;
                isActiveBrowser.Show();
                WebBrowser web = new WebBrowser()
                {
                    Visible = true,
                    ScriptErrorsSuppressed = true,
                    Dock = DockStyle.Fill,
                };
                web.DocumentCompleted += Web_DocumentCompleted;
                tabControl1.TabPages.Add("New Page");
                tabControl1.SelectTab(tabs++);
                tabControl1.SelectedTab.Controls.Add(web);
                ((WebBrowser)tabControl1.SelectedTab.Controls[0]).Navigate("bing.com");
            }
        }

        private void pictureBox5_MouseMove(object sender, MouseEventArgs e)
        {
            if (IsMouseDown)
            {
                MouseX = MousePosition.X - MouseinX;
                MouseY = MousePosition.Y - MouseinY;

                this.Location = new Point(MouseX, MouseY);
            }
        }

        private void pictureBox5_MouseUp(object sender, MouseEventArgs e)
        {
            IsMouseDown = false;
        }

        private void pictureBox5_MouseDown(object sender, MouseEventArgs e)
        {
            IsMouseDown = true;
            MouseinX = MousePosition.X - this.Bounds.X;
            MouseinY = MousePosition.Y - this.Bounds.Y;
        }
        bool isActive = false;
        PictureBox choosed;
        private void pictureBox6_MouseEnter(object sender, EventArgs e)
        {
            choosed = (sender as PictureBox);
            isActive = true;
            timer2.Interval = 25;
            timer2.Start();
        }

        int directory = 0;

        int snakeLength = 1;
        int foodXPos;
        int foodYPos;

        private void checkIFOffBoard()  //Checks if the snake is not on the board, if it is the game ends
        {
            if (snakeXPos < 0 || snakeXPos > 31 || snakeYPos < 0 || snakeYPos > 17)
            {
                timer2.Stop();
                snakeXPos = 16;
                snakeYPos = 9;
                MessageBox.Show("Вы проиграли!");
                isReset = true;
                this.SnakePlay.Hide();
                this.SnakeGameMenu.Show();
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            switch (directory) //Changes snakes direction
            {
                case 2:
                    snakeXPos -= 1;
                    checkIFOffBoard();
                    Grid[snakeXPos, snakeYPos].BackColor = Color.Red;
                    break;
                case 0:
                    snakeXPos += 1;
                    checkIFOffBoard();
                    Grid[snakeXPos, snakeYPos].BackColor = Color.Red;
                    break;
                case 3:
                    snakeYPos -= 1;
                    checkIFOffBoard();
                    Grid[snakeXPos, snakeYPos].BackColor = Color.Red;
                    break;
                case 1:
                    snakeYPos += 1;
                    checkIFOffBoard();
                    Grid[snakeXPos, snakeYPos].BackColor = Color.Red;
                    break;
            }
            this.label26.Text = "Your score: " + score.ToString();
            if (snakeXPos == foodXPos && snakeYPos == foodYPos)
            {
                score+=10;
                snakeLength += 1;
                newFood();
            }

            for (int i = 99; i >= 1; i--)
            {
                if (i < snakeLength)
                {
                    snakeXPositions[i + 1] = snakeXPositions[i];
                    snakeYPositions[i + 1] = snakeYPositions[i];
                }
                else if (i > snakeLength)
                {
                    snakeXPositions[i] = -1;
                    snakeYPositions[i] = 0;
                }
            }                           

            snakeXPositions[1] = snakeXPos;
            snakeYPositions[1] = snakeYPos;

            try
            {

            }
            catch (Exception)
            {

                throw;
            }
            for (int y = 0; y < 18; y++)
            {
                for (int x = 0; x < 32; x++)
                {
                    if (Grid[x, y].BackColor != Color.Blue)
                    {
                        Grid[x, y].BackColor = Color.Black;
                    }

                }
            }

            for (int i = 1; i < 100; i++)
            {
                if (snakeXPositions[i] != -1)
                {
                    Grid[snakeXPositions[i], snakeYPositions[i]].BackColor = Color.Red;
                }

            }
        }

        int score = 0;
        private void newFood()
        {
            for (int y = 0; y < 18; y++)
            {
                for (int x = 0; x < 32; x++)
                {
                    if (Grid[x, y].BackColor == Color.Blue)
                    {
                        foodXPos = x;
                        foodYPos = y;
                        Grid[x, y].BackColor = Color.Black;
                    }
                }
            }                              

            bool foundBox = false;
            while (foundBox == false)
            {
                foodXPos = R.Next(0, 32);
                foodYPos = R.Next(0, 18);

                if (Grid[foodXPos, foodYPos].BackColor == Color.Black)
                {
                    Grid[foodXPos, foodYPos].BackColor = Color.Blue;
                    foundBox = true;
                }
                else
                {
                    foodXPos = R.Next(0, 32);
                    foodYPos = R.Next(0, 18);
                }
            }

        }
        private void pictureBox6_MouseLeave(object sender, EventArgs e)
        {
            choosed = (sender as PictureBox);
            isActive = false;
            timer2.Start();
        }


        int diffucult = 1;

        private void Easy_Click(object sender, EventArgs e)
        {
            EasyActive.BackColor = Color.White;
            MediumActive.BackColor = Color.Black;
            HardActive.BackColor = Color.Black;
            diffucult = 1;
        }

        private void Hard_Click(object sender, EventArgs e)
        {
            EasyActive.BackColor = Color.Black;
            MediumActive.BackColor = Color.Black;
            HardActive.BackColor = Color.White;
            diffucult = 3;
        }

        private void Medium_Click(object sender, EventArgs e)
        {
            EasyActive.BackColor = Color.Black;
            MediumActive.BackColor = Color.White;
            HardActive.BackColor = Color.Black;
            diffucult = 2;
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            SnakeGameMenu.Hide();
            timer1.Start();
        }

        public void MyKeyPressEventHandler(object sender, KeyPressEventArgs e)
        {

        }

        PictureBox[,] Grid;
        int snakeXPos;
        int snakeYPos;
        int[] snakeXPositions;
        int[] snakeYPositions;
        Random R = new Random();
        bool isReset = false;
        private void button4_Click_1(object sender, EventArgs e)
        {
            SnakeGameMenu.Hide();
            timer1.Stop();
            SnakePlay.Show();
            SnakePlay.Location = new Point(0,0);
            if (!isReset)
            {
                snakeXPositions = new int[100];
                snakeYPositions = new int[100];

                Grid = new PictureBox[32, 18];
                for (int y = 0; y < 18; y++)        // v Creates the 30x30 board of pictureboxes
                {
                    for (int x = 0; x < 32; x++)
                    {
                        Grid[x, y] = new PictureBox();
                        Grid[x, y].Left = x * 40;
                        Grid[x, y].Top = y * 40;
                        Grid[x, y].Width = 40;
                        Grid[x, y].Height = 40;
                        Grid[x, y].BackColor = Color.Black;

                        SnakePlay.Controls.Add(Grid[x, y]);
                    }
                }
                Grid[16, 9].BackColor = Color.Red;
                snakeXPos = 16;
                snakeYPos = 9;
                snakeXPositions[1] = 16;
                snakeYPositions[1] = 9;
                newFood();
                newFood();
                timer2.Start();
                timer2.Interval = 300 / diffucult;
            }
            else
            {
                score = 0;
                for (int y = 0; y < 18; y++)
                {
                    for (int x = 0; x < 32; x++)
                    {
                        Grid[x, y].BackColor = Color.Black;
                    }
                }
                for (int i = 1; i < 100; i++)
                {
                    snakeXPositions[i] = -1;
                    snakeYPositions[i] = -1;
                }

                snakeLength = 1;

                Grid[16, 9].BackColor = Color.Red;
                snakeXPos = 16;
                snakeYPos = 9;
                directory = 0;
                snakeXPositions[1] = 16;
                snakeYPositions[1] = 9;
                newFood();
                timer2.Start();
                timer2.Interval = 300 / diffucult;
            }
        }

        private void Windows_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.D)
            {
                if (directory != 0 && directory != 2)
                    directory = 0;

                //MessageBox.Show(directory.ToString());
            }
            else if (e.KeyCode == Keys.S)
            {
                if (directory != 1 && directory != 3)
                    directory = 1;

                //MessageBox.Show(directory.ToString());
            }
            else if (e.KeyCode == Keys.A)
            {
                if (directory != 2 && directory != 0)
                    directory = 2;

                //MessageBox.Show(directory.ToString());
            }
            else if (e.KeyCode == Keys.W)
            {
                if (directory != 3 && directory != 1)
                    directory = 3;

                //MessageBox.Show(directory.ToString());
            }
        }

        private void pictureBox6_DoubleClick(object sender, EventArgs e)
        {
            SnakeGameMenu.Show();
            SnakeGameMenu.Location = new Point(0, 0);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.AccountPanel.Show();
            this.WindowsOpenPanel.Location = new Point(0,690);
        }

        private void panel18_Paint(object sender, PaintEventArgs e)
        {

        }

        private void ChangeFinish_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Пароль изменён!");
            this.password = textBox3.Text;
            this.passwordHint = textBox5.Text;
            this.ChangePassFinish.Hide();
            this.ChangeFinish.Hide();
            this.AccountPanel.Hide();
            this.ChangePassPanel1.Hide();
            this.OldPass.Show();
        }

        private void OldPass_Click(object sender, EventArgs e)
        {
            if(textBox2.Text != password)
            {
                MessageBox.Show("Вы ввели не верный пароль!");
                return;
            }
            this.ChangePassNextPanel.Show();
            this.NewPass.Show();
            this.OldPass.Hide();
        }

        private void NewPass_Click(object sender, EventArgs e)
        {
            if(textBox3.Text != textBox4.Text)
            {
                MessageBox.Show("Пароли не совпадают!");
                return;
            }
            this.ChangePassNextPanel.Hide();
            this.NewPass.Hide();
            this.ChangePassFinish.Show();
            this.ChangeFinish.Show();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            this.ChangePassFinish.Hide();
            this.ChangeFinish.Hide();
            this.ChangePassPanel1.Hide();
            this.OldPass.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.ChangePassPanel1.Show();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            this.ChangeUsername.Show();
            this.ChangeNameUsername.Text = this.username;
            this.ChangeUsername.Location = new Point((216 - this.ChangeUsername.Size.Width) / 2 + 40, this.ChangeUsername.Location.Y);
        }

        private void pictureBox14_Click_1(object sender, EventArgs e)
        {
            if(LoginTextBox.Text != password)
            {
                this.LabelHint.Show();
                this.LabelHint.Text = "Подсказка для пароля: " + passwordHint;
                return;
            }
            this.Login.Hide();
            timer1.Start();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Login.Show();
            this.BlockedMenu.Show();
            BlockedMenuClicked = false;
            timer4.Start();
            this.label47.Location = new Point(78, 347);
            this.label48.Location = new Point(76, 480);
            this.Login.BringToFront();
            timer1.Stop();
        }

        private void pictureBox12_MouseDown(object sender, MouseEventArgs e)
        {
            this.pictureBox12.BackColor = Color.FromKnownColor(KnownColor.Highlight);
            this.LoginTextBox.PasswordChar = '\0';
        }

        private void pictureBox12_MouseUp(object sender, MouseEventArgs e)
        {
            this.pictureBox12.BackColor = Color.White;
            this.LoginTextBox.PasswordChar = '•';
        }

        private void button10_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Имя пользователя изменено!");
            this.ChangeUsername.Hide();
            this.username = textBox9.Text;
            this.UsernameLogin.Text = this.username;
            this.ChangeNameUsername.Text = this.username;
            this.TextUsername.Text = this.username;
            this.label46.Text = this.username;
            this.label38.Text = this.username;
            this.TextUsername.Location = new Point((216 - this.TextUsername.Size.Width) / 2 + 40, this.TextUsername.Location.Y);
            this.UsernameLogin.Location = new Point((145 - this.UsernameLogin.Size.Width) / 2 + this.LoginPicture.Location.X, this.UsernameLogin.Location.Y);
        }

        private void button13_Click(object sender, EventArgs e)
        {
            this.ChangeUsername.Hide();
        }

        private void label27_Click(object sender, EventArgs e)
        {
            this.AccountPanel.Hide();
        }

        private void panel18_MouseDown(object sender, MouseEventArgs e)
        {
            IsMouseDown = true;
            MouseinX = MousePosition.X - this.AccountPanel.Bounds.X;
            MouseinY = MousePosition.Y - this.AccountPanel.Bounds.Y;
            this.AccountPanel.BringToFront();
        }

        private void panel18_MouseUp(object sender, MouseEventArgs e)
        {
            IsMouseDown = false;
        }

        private void panel18_MouseMove(object sender, MouseEventArgs e)
        {
            if (IsMouseDown)
            {
                MouseX = MousePosition.X - MouseinX;
                MouseY = MousePosition.Y - MouseinY;

                this.AccountPanel.Location = new Point(MouseX, MouseY);
            }
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            GC.Collect();
        }

        private void timer4_Tick(object sender, EventArgs e)
        {
            label47.Text = DateTime.Now.ToShortTimeString();
            DateTime time = DateTime.Now;
            int month = DateTime.Now.Month;
            string monthStr = "";
            switch(month)
            {
                case 1:
                    monthStr = "января";
                    break;
                case 2:
                    monthStr = "февраля";
                    break;
                case 3:
                    monthStr = "марта";
                    break;
                case 4:
                    monthStr = "апреля";
                    break;
                case 5:
                    monthStr = "мая";
                    break;
                case 6:
                    monthStr = "июня";
                    break;
                case 7:
                    monthStr = "июля";
                    break;
                case 8:
                    monthStr = "августа";
                    break;
                case 9:
                    monthStr = "сентября";
                    break;
                case 10:
                    monthStr = "октября";
                    break;
                case 11:
                    monthStr = "ноября";
                    break;
                case 12:
                    monthStr = "декабря";
                    break;
            }
            label48.Text = time.ToString("dddd") + ", " + Convert.ToInt32(time.ToString("dd")) + " " + monthStr;

            if(BlockedMenuClicked)
            {
                //this.BlockedMenu.Hide();
                if (counterBlock < 30)
                {
                    this.label47.Top-=30;
                    this.label48.Top-=30;
                }
                else
                {
                    BlockedMenu.Hide();
                    timer4.Stop();
                }
                counterBlock++;
            }
        }
        int counterBlock = 0;
        bool BlockedMenuClicked = false;
        private void BlockedMenu_Click(object sender, EventArgs e)
        {
            counterBlock = 0;
            BlockedMenuClicked = true;
        }

        private void SettingsTopPanel_MouseUp(object sender, MouseEventArgs e)
        {
            IsMouseDown = false;
        }

        private void SettingsTopPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (IsMouseDown)
            {
                MouseX = MousePosition.X - MouseinX;
                MouseY = MousePosition.Y - MouseinY;

                SettingsPanel.Location = new Point(MouseX, MouseY);
            }
        }



    }
}

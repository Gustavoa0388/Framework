using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using GS.Core.UI.Theming;

namespace GS.Core.UI.Controls
{
    /// <summary>
    /// Barra de progresso padrão do GS Core.
    /// Utiliza tokens semânticos do tema e suporta estados visuais.
    /// </summary>
    public class GsProgressBar : Control, IThemedControl
    {
        private int _minimum = 0;
        private int _maximum = 100;
        private int _value = 0;

        private int _animatedValue = 0;
        private System.Windows.Forms.Timer _animationTimer;

        private GsTheme _theme;

        // ==========================================================
        // PROPRIEDADES PÚBLICAS
        // ==========================================================

        [Category("GS Core")]
        public int Minimum
        {
            get => _minimum;
            set
            {
                _minimum = value;
                if (_maximum < _minimum)
                    _maximum = _minimum;

                Value = _value;
                Invalidate();
            }
        }

        [Category("GS Core")]
        public int Maximum
        {
            get => _maximum;
            set
            {
                _maximum = value;
                if (_maximum < _minimum)
                    _maximum = _minimum;

                Value = _value;
                Invalidate();
            }
        }

        [Category("GS Core")]
        public int Value
        {
            get => _value;
            set
            {
                int newValue = Math.Max(_minimum, Math.Min(_maximum, value));
                _value = newValue;

                if (Animated)
                    StartAnimation();
                else
                {
                    _animatedValue = _value;
                    Invalidate();
                }
            }
        }

        [Category("GS Core")]
        [DefaultValue(false)]
        public bool Animated { get; set; } = false;

        [Category("GS Core")]
        [DefaultValue(ProgressState.Normal)]
        public ProgressState State { get; set; } = ProgressState.Normal;

        // ==========================================================
        // CONSTRUTOR
        // ==========================================================

        public GsProgressBar()
        {
            SetStyle(
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.UserPaint,
                true
            );

            Height = 12;
            MinimumSize = new Size(50, 8);

            _animationTimer = new System.Windows.Forms.Timer
            {
                Interval = 15 // animação leve (~60fps)
            };
            _animationTimer.Tick += AnimationTick;
        }

        // ==========================================================
        // ANIMAÇÃO
        // ==========================================================

        private void StartAnimation()
        {
            _animationTimer.Start();
        }

        private void AnimationTick(object sender, EventArgs e)
        {
            if (_animatedValue == _value)
            {
                _animationTimer.Stop();
                return;
            }

            int delta = Math.Sign(_value - _animatedValue);
            _animatedValue += delta;

            Invalidate();
        }

        // ==========================================================
        // THEME
        // ==========================================================

        public void ApplyTheme(GsTheme theme)
        {
            _theme = theme;
            BackColor = theme.ProgressBackground;
            Invalidate();
        }

        // ==========================================================
        // PAINT
        // ==========================================================

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (_theme == null)
                return;

            Graphics g = e.Graphics;
            g.Clear(BackColor);

            Rectangle rect = ClientRectangle;
            rect.Width -= 1;
            rect.Height -= 1;

            // Borda
            using (var borderPen = new Pen(_theme.ProgressBorder))
            {
                g.DrawRectangle(borderPen, rect);
            }

            // Percentual
            float percent = (_maximum - _minimum) == 0
                ? 0
                : (float)(_animatedValue - _minimum) / (_maximum - _minimum);

            int fillWidth = (int)(rect.Width * percent);
            if (fillWidth <= 0)
                return;

            Rectangle fillRect = new Rectangle(
                rect.X,
                rect.Y,
                fillWidth,
                rect.Height
            );

            Color fillColor = State switch
            {
                ProgressState.Success => _theme.ProgressSuccess,
                ProgressState.Error => _theme.ProgressError,
                _ => _theme.ProgressFill
            };

            using (var fillBrush = new SolidBrush(fillColor))
            {
                g.FillRectangle(fillBrush, fillRect);
            }
        }
    }

    /// <summary>
    /// Estados visuais da barra de progresso.
    /// </summary>
    public enum ProgressState
    {
        Normal,
        Success,
        Error
    }
}

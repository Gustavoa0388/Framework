using System;
using System.Windows.Forms;
using GS.Core.UI.Controls.Base;

namespace GS.Core.UI.Controls.Inputs
{
    /// <summary>
    /// TextBox padrão do GS Core.
    /// 
    /// RESPONSABILIDADES:
    /// - Encapsular TextBox padrão
    /// - Repassar eventos essenciais (TextChanged, KeyDown, etc.)
    /// - Repassar propriedade Text
    /// 
    /// OBS:
    /// Controles wrapper DEVEM propagar eventos,
    /// senão quebram UX avançada (debounce, binding, etc).
    /// </summary>
    public class GsTextBox : GsInputBase
    {
        private TextBox _inner;

        protected override TextBox CreateInnerTextBox()
        {
            _inner = new TextBox();

            // ============================
            // PROPAGA EVENTOS IMPORTANTES
            // ============================

            _inner.TextChanged += (s, e) =>
            {
                OnTextChanged(e);
            };

            _inner.KeyDown += (s, e) =>
            {
                OnKeyDown(e);
            };

            _inner.KeyPress += (s, e) =>
            {
                OnKeyPress(e);
            };

            _inner.KeyUp += (s, e) =>
            {
                OnKeyUp(e);
            };

            return _inner;
        }

        // ============================
        // PROPAGA PROPRIEDADE TEXT
        // ============================

        public override string Text
        {
            get => _inner?.Text ?? string.Empty;
            set
            {
                if (_inner != null)
                    _inner.Text = value;
            }
        }

        // ============================
        // GARANTE DISPARO CORRETO
        // ============================

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
        }
    }
}

using System;
using System.Drawing;
using System.Windows.Forms;
using GS.Core.UI.Controls.Base;

namespace GS.Core.UI.Controls.Inputs
{
    /// <summary>
    /// GsPasswordTextBox
    ///
    /// Input de senha padrão do GS Core.
    ///
    /// OBJETIVO:
    /// Encapsular um <see cref="TextBox"/> nativo configurado
    /// para entrada de senha, adicionando um botão de alternância
    /// de visibilidade (ícone de olho), sem quebrar o fluxo padrão
    /// de validação e UX do framework.
    ///
    /// ESTE CONTROLE:
    /// - Herda de <see cref="GsInputBase"/>
    /// - Usa TextBox nativo com UseSystemPasswordChar
    /// - Possui botão interno para exibir/ocultar senha
    /// - Participa do fluxo global de validação (Required)
    /// - Propaga eventos essenciais para UX avançada
    ///
    /// ESTE CONTROLE NÃO FAZ:
    /// - Não valida força de senha
    /// - Não criptografa dados
    /// - Não executa regra de negócio
    ///
    /// CASOS DE USO TÍPICOS:
    /// - Login
    /// - Cadastro de usuário
    /// - Alteração de senha
    ///
    /// OBSERVAÇÃO:
    /// Os ícones de exibição utilizam recursos embutidos
    /// (Properties.Resources) e poderão ser externalizados
    /// futuramente para o sistema de Theme.
    /// </summary>
    public class GsPasswordTextBox : GsInputBase
    {
        // ==========================================================
        // CONTROLES INTERNOS
        // ==========================================================

        /// <summary>
        /// Ícone de alternância de visibilidade da senha.
        /// </summary>
        private PictureBox _eyeIcon;

        /// <summary>
        /// Indica se a senha está visível.
        /// </summary>
        private bool _showPassword;

        // ==========================================================
        // CONSTANTES VISUAIS
        // ==========================================================

        /// <summary>
        /// Tamanho do ícone de visibilidade.
        /// </summary>
        private const int EyeIconSize = 20;

        /// <summary>
        /// Espaçamento entre o ícone de erro e o ícone do olho.
        /// </summary>
        private const int EyeIconSpacing = 6;

        // ==========================================================
        // CRIAÇÃO DO CONTROLE INTERNO
        // ==========================================================

        /// <summary>
        /// Cria o TextBox interno utilizado pelo input.
        /// </summary>
        protected override TextBoxBase CreateInnerTextBox()
        {
            var textBox = new TextBox
            {
                UseSystemPasswordChar = true
            };

            // ============================
            // PROPAGA EVENTOS ESSENCIAIS
            // ============================

            textBox.TextChanged += (s, e) => OnTextChanged(e);
            textBox.KeyDown += (s, e) => OnKeyDown(e);
            textBox.KeyPress += (s, e) => OnKeyPress(e);
            textBox.KeyUp += (s, e) => OnKeyUp(e);

            return textBox;
        }

        // ==========================================================
        // INICIALIZAÇÃO
        // ==========================================================

        /// <summary>
        /// Inicialização do controle após criação.
        /// Responsável por criar e posicionar o ícone de visibilidade.
        /// </summary>
        protected override void OnCreateControl()
        {
            base.OnCreateControl();

            _eyeIcon = new PictureBox
            {
                Size = new Size(EyeIconSize, EyeIconSize),
                SizeMode = PictureBoxSizeMode.CenterImage,
                Cursor = Cursors.Hand,
                Image = Properties.Resources.eye_closed,
                BackColor = Color.Transparent,
                TabStop = false // Não participa do fluxo de tabulação
            };

            _eyeIcon.Click += (_, _) => TogglePassword();

            Controls.Add(_eyeIcon);
            _eyeIcon.BringToFront();

            UpdateLayout();
        }

        // ==========================================================
        // LAYOUT
        // ==========================================================

        /// <summary>
        /// Atualiza o layout interno do controle.
        /// Ajusta a posição do ícone de olho considerando:
        /// - Padding
        /// - Espaço reservado para ícone de erro
        /// </summary>
        protected override void UpdateLayout()
        {
            base.UpdateLayout();

            if (_eyeIcon == null)
                return;

            int errorOffset = HasError
                ? ErrorIconSize + EyeIconSpacing
                : 0;

            _eyeIcon.Location = new Point(
                Width - Padding.Right - EyeIconSize - errorOffset,
                (Height - EyeIconSize) / 2
            );
        }

        // ==========================================================
        // COMPORTAMENTO
        // ==========================================================

        /// <summary>
        /// Alterna a visibilidade da senha.
        /// </summary>
        private void TogglePassword()
        {
            _showPassword = !_showPassword;

            if (InnerTextBox is TextBox tb)
                tb.UseSystemPasswordChar = !_showPassword;

            _eyeIcon.Image = _showPassword
                ? Properties.Resources.eye_open
                : Properties.Resources.eye_closed;
        }

        // ==========================================================
        // VALIDAÇÃO
        // ==========================================================

        /// <summary>
        /// Participa explicitamente do fluxo de validação do GS Core.
        ///
        /// Não adiciona validação própria.
        /// A regra de Required é tratada exclusivamente no GsInputBase.
        /// </summary>
        public override void ValidateInput()
        {
            base.ValidateInput();
        }
    }
}

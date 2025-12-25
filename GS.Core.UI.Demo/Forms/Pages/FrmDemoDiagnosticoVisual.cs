using System;
using System.Drawing;
using System.Windows.Forms;
using GS.Core.UI.Controls.UX;
using GS.Core.UI.Forms;

namespace GS.Core.UI.Demo.Forms.Pages
{
    /// <summary>
    /// FrmDemoDiagnosticoVisual
    ///
    /// Página de demonstração do padrão de Diagnóstico Visual
    /// do GS Core UI.
    ///
    /// OBJETIVO:
    /// - Demonstrar erro SEM diagnóstico técnico
    /// - Demonstrar erro COM diagnóstico técnico autorizado
    ///
    /// ESTE FORM:
    /// - Não captura exceções reais
    /// - Não executa regra de negócio
    /// - Não utiliza logger
    ///
    /// É exclusivamente didático.
    /// </summary>
    public sealed partial class FrmDemoDiagnosticoVisual : GsBaseForm

    {
        // =====================================================
        // CONTROLES
        // =====================================================

        private GsStateView _stateView;

        private Button _btnErroSemDiagnostico;
        private Button _btnErroComDiagnostico;
        private Button _btnLimpar;

        // =====================================================
        // LIFECYCLE GS CORE
        // =====================================================

        protected override void OnInitialize()
        {
            base.OnInitialize();

            Text = "Demo — Diagnóstico Visual";
            Width = 900;
            Height = 520;

            BuildLayout();
        }

        // =====================================================
        // LAYOUT
        // =====================================================

        private void BuildLayout()
        {
            // Painel superior com ações de demo
            var pnlActions = new FlowLayoutPanel
            {
                Dock = DockStyle.Top,
                Height = 56,
                Padding = new Padding(10),
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false
            };

            _btnErroSemDiagnostico = new Button
            {
                Text = "Erro sem diagnóstico",
                Width = 190,
                Height = 32
            };
            _btnErroSemDiagnostico.Click += OnErroSemDiagnosticoClick;

            _btnErroComDiagnostico = new Button
            {
                Text = "Erro com diagnóstico",
                Width = 190,
                Height = 32
            };
            _btnErroComDiagnostico.Click += OnErroComDiagnosticoClick;

            _btnLimpar = new Button
            {
                Text = "Limpar estado",
                Width = 140,
                Height = 32
            };
            _btnLimpar.Click += OnLimparClick;

            pnlActions.Controls.Add(_btnErroSemDiagnostico);
            pnlActions.Controls.Add(_btnErroComDiagnostico);
            pnlActions.Controls.Add(_btnLimpar);

            // StateView ocupa a área principal
            _stateView = new GsStateView
            {
                Dock = DockStyle.Fill
            };

            Controls.Add(_stateView);
            Controls.Add(pnlActions);
        }

        // =====================================================
        // AÇÕES DE DEMONSTRAÇÃO
        // =====================================================

        /// <summary>
        /// Demonstra erro SEM diagnóstico visual autorizado.
        ///
        /// Resultado esperado:
        /// - Mensagem de erro exibida
        /// - Botão "Detalhes técnicos" NÃO aparece
        /// </summary>
        private void OnErroSemDiagnosticoClick(object sender, EventArgs e)
        {
            _stateView.ShowError(new GsErrorState(
                title: "Falha ao carregar dados",
                message: "Não foi possível carregar as informações solicitadas.",
                technicalDetails: "Timeout ao acessar o serviço de clientes.",
                allowDiagnostics: false
            ));
        }

        /// <summary>
        /// Demonstra erro COM diagnóstico visual autorizado.
        ///
        /// Resultado esperado:
        /// - Mensagem de erro exibida
        /// - Botão "Detalhes técnicos" APARECE
        /// - Detalhe técnico só aparece ao clicar
        /// </summary>
        private void OnErroComDiagnosticoClick(object sender, EventArgs e)
        {
            _stateView.ShowError(new GsErrorState(
                title: "Erro de comunicação",
                message: "Ocorreu um erro ao tentar comunicar com o serviço.",
                technicalDetails:
                    "HTTP 504 - Gateway Timeout\n" +
                    "Endpoint: /api/clientes\n" +
                    "Tempo limite excedido após 30s.",
                allowDiagnostics: true
            ));
        }

        /// <summary>
        /// Remove qualquer estado visual ativo.
        /// </summary>
        private void OnLimparClick(object sender, EventArgs e)
        {
            _stateView.State = GsUxState.Hidden;
        }
    }
}

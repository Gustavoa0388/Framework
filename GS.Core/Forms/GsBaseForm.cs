using System;
using System.Collections.Generic;
using System.Windows.Forms;
using GS.Core.UI.Controls.Base;
using GS.Core.UI.Theming;
using GS.Core.UI.Forms.Navigation;
using GS.Core.UI.Controls.Debug;
using GS.Core.UI.Controls.UX;
using GS.Core.UI.Telemetry;

namespace GS.Core.UI.Forms
{
    /// <summary>
    /// GsBaseForm
    ///
    /// Form base oficial do GS Core UI.
    ///
    /// RESPONSABILIDADES:
    /// - Centralizar aplicação de tema
    /// - Oferecer validação global padronizada
    /// - Orquestrar o lifecycle oficial do GS Core UI
    ///
    /// NÃO FAZ:
    /// - Não executa lógica de negócio
    /// - Não carrega dados automaticamente fora do contrato
    /// - Não decide estados de UX
    /// </summary>
    public class GsBaseForm : Form
    {
        // =====================================================
        // TEMA
        // =====================================================

        /// <summary>
        /// Aplica o tema automaticamente ao carregar o formulário.
        /// </summary>
        protected bool AutoApplyTheme { get; set; } = true;

        /// <summary>
        /// Permite sobrescrever o tema apenas neste formulário.
        /// Se null, utiliza o tema atual do ThemeManager.
        /// </summary>
        protected GsTheme CustomTheme { get; set; }

        // =====================================================
        // CONTROLE DE LIFECYCLE
        // =====================================================

        private bool _initialized;
        private bool _dataLoaded;

        // =====================================================
        // RESULTADO DE NAVEGAÇÃO
        // =====================================================

        /// <summary>
        /// Resultado semântico da navegação deste formulário.
        /// Sempre possuirá um valor ao final do ciclo de vida.
        /// </summary>
        public GsFormResult NavigationResult { get; private set; } = GsFormResult.None();

        // =====================================================
        // UI TELEMETRIA MANUAL (OPT-IN)
        // =====================================================

        private GsUiTelemetry _uiTelemetry;

        /// <summary>
        /// Habilita ou desabilita a UI Telemetria Manual para este formulário.
        /// Desabilitada por padrão.
        /// </summary>
        protected bool EnableUiTelemetry { get; set; } = false;

        /// <summary>
        /// Acesso somente leitura à UI Telemetria Manual.
        /// Pode ser usado por Debug UX e diagnóstico.
        /// </summary>
        protected GsUiTelemetry UiTelemetry => _uiTelemetry;

        // =====================================================
        // CONSTRUTOR
        // =====================================================

        protected GsBaseForm()
        {
            StartPosition = FormStartPosition.CenterScreen;
            KeyPreview = true;
        }

        // =====================================================
        // WINFORMS → GS CORE LIFECYCLE
        // =====================================================

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (_initialized)
                return;

            ApplyThemeIfNeeded();
            OnInitialize();
            _initialized = true;
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            if (_dataLoaded)
                return;

            _dataLoaded = true;
            ExecuteLoadPipeline();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (!OnBeforeClose())
            {
                e.Cancel = true;
                return;
            }

            // =====================================================
            // GARANTIA DE RESULTADO DE NAVEGAÇÃO
            // =====================================================

            if (NavigationResult == null || NavigationResult.ResultType == GsFormResultType.None)
            {
                NavigationResult = GsFormResult.Closed();
            }

            base.OnFormClosing(e);
        }

        // =====================================================
        // PIPELINE CONTROLADO
        // =====================================================

        private void ExecuteLoadPipeline()
        {
            try
            {
                OnLoadData();
                OnAfterLoad();
            }
            catch
            {
                // Importante:
                // O GsBaseForm NÃO decide UX nem tratamento de erro.
                // Cada Form concreto é responsável por isso.
                throw;
            }
        }

        // =====================================================
        // HOOKS OFICIAIS DO LIFECYCLE GS CORE
        // =====================================================

        /// <summary>
        /// Executado uma única vez na inicialização do Form.
        /// Infraestrutura do framework é inicializada aqui.
        /// </summary>
        protected virtual void OnInitialize()
        {
            InitializeInfrastructure();
            OnInitializeInternal();
        }

        /// <summary>
        /// Inicialização de infraestrutura do GS Core UI.
        /// Blindada contra override indevido.
        /// </summary>
        private void InitializeInfrastructure()
        {
            _uiTelemetry = new GsUiTelemetry(EnableUiTelemetry);
        }

        /// <summary>
        /// Hook seguro para inicialização de formulários filhos.
        /// A infraestrutura do GS Core já estará pronta.
        /// </summary>
        protected virtual void OnInitializeInternal() { }

        /// <summary>
        /// Executado na primeira exibição do Form.
        /// Use para carregar dados.
        /// </summary>
        protected virtual void OnLoadData() { }

        /// <summary>
        /// Executado após OnLoadData().
        /// Use para ajustes finais de UX.
        /// </summary>
        protected virtual void OnAfterLoad() { }

        /// <summary>
        /// Executado antes do fechamento do Form.
        /// Retorne false para cancelar o fechamento.
        /// </summary>
        protected virtual bool OnBeforeClose() => true;

        // =====================================================
        // DEBUG UX (OBSERVABILIDADE VISUAL)
        // =====================================================

        private GsDebugUxOverlay _debugUxOverlay;
        private bool _enableDebugUx;

        /// <summary>
        /// Habilita ou desabilita o Debug UX visual.
        /// Uso explícito e opt-in.
        /// </summary>
        public bool EnableDebugUx
        {
            get => _enableDebugUx;
            set
            {
                if (_enableDebugUx == value)
                    return;

                _enableDebugUx = value;

                if (_enableDebugUx)
                    UiTelemetry?.Increment(UiTelemetryMetric.DebugUxEnabled);
                else
                    UiTelemetry?.Increment(UiTelemetryMetric.DebugUxDisabled);

                UpdateDebugUxVisibility();
            }
        }


        private void InitializeDebugUx()
        {
            _debugUxOverlay = new GsDebugUxOverlay
            {
                Anchor = AnchorStyles.Bottom | AnchorStyles.Right,
                Visible = false
            };

            Controls.Add(_debugUxOverlay);
            _debugUxOverlay.BringToFront();
        }

        private void UpdateDebugUxVisibility()
        {
            if (_debugUxOverlay == null)
                return;

            _debugUxOverlay.Visible = _enableDebugUx;
        }

        /// <summary>
        /// Sincroniza os estados internos com o Debug UX.
        /// </summary>
        protected void SyncDebugUx(
            GsUxState uxState,
            bool isBusy,
            bool isSkeletonActive,
            bool diagnosticsAllowed,
            bool hasTechnicalDetails)
        {
            if (!_enableDebugUx || _debugUxOverlay == null)
                return;

            _debugUxOverlay.UpdateState(
                uxState,
                isBusy,
                isSkeletonActive,
                NavigationResult?.ResultType ?? GsFormResultType.None,
                diagnosticsAllowed,
                hasTechnicalDetails
            );
        }

        // =====================================================
        // ENCERRAMENTO COM RESULTADO
        // =====================================================

        /// <summary>
        /// Encerra o formulário informando explicitamente
        /// um resultado semântico de navegação.
        /// </summary>
        protected void CloseWithResult(GsFormResult result)
        {
            NavigationResult = result ?? GsFormResult.None();
            Close();
        }

        // =====================================================
        // TEMA
        // =====================================================

        protected virtual void ApplyThemeIfNeeded()
        {
            if (!AutoApplyTheme)
                return;

            var theme = CustomTheme ?? ThemeManager.Current;
            if (theme == null)
                return;

            ThemeManager.ApplyTheme(this, theme);
            Invalidate(true);
        }

        // =====================================================
        // VALIDAÇÃO GLOBAL
        // =====================================================

        /// <summary>
        /// Valida todos os controles que implementam IGsValidatable.
        /// Retorna true se todos estiverem válidos.
        /// </summary>
        public virtual bool ValidateForm()
        {
            var inputs = GetAllValidatableControls(this);

            IGsValidatable firstInvalid = null;

            foreach (var input in inputs)
            {
                input.Validate();

                if (!input.IsValid && firstInvalid == null)
                    firstInvalid = input;
            }

            // UX profissional: foca no primeiro erro
            if (firstInvalid is Control ctrl && ctrl.CanFocus)
                ctrl.Focus();

            return firstInvalid == null;
        }

        /// <summary>
        /// Obtém recursivamente todos os controles IGsValidatable.
        /// </summary>
        protected virtual List<IGsValidatable> GetAllValidatableControls(Control parent)
        {
            var list = new List<IGsValidatable>();

            foreach (Control ctrl in parent.Controls)
            {
                if (ctrl is IGsValidatable validatable)
                    list.Add(validatable);

                if (ctrl.HasChildren)
                    list.AddRange(GetAllValidatableControls(ctrl));
            }

            return list;
        }

        // =====================================================
        // LEGADO (COMPATIBILIDADE)
        // =====================================================

        /// <summary>
        /// Método legado em PT-BR.
        /// Use ValidateForm().
        /// </summary>
        [Obsolete("Use ValidateForm()")]
        public bool ValidarFormulario()
        {
            return ValidateForm();
        }
    }
}

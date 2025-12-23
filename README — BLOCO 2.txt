# GS Core UI — BLOCO 2  
## Limpeza de Legado (ECTurbo)

Este documento registra o encerramento oficial do **BLOCO 2** do framework **GS Core UI**.

O foco deste bloco foi **eliminar dependências do framework legado ECTurbo**, padronizar arquitetura, aplicar boas práticas de UI/UX e preparar o núcleo do GS Core para evolução sustentável.

---

## 🎯 Objetivos do Bloco

- Refatorar controles herdados do ECTurbo
- Eliminar uso de `Tag`, `FuncoesLegacy` e cores hardcoded
- Padronizar validação, tema e estados visuais
- Centralizar enums semânticos reutilizáveis
- Isolar controles legados sem quebrar compatibilidade
- Garantir base arquitetural sólida para próximos blocos

---

## ✅ Escopo Concluído

### 🔹 Inputs e Seleção
- `GsComboBox`
- `GsCheckBox`
- Integração total com `GsInputBase`
- Validação padronizada (`Required`)
- Sem lógica oculta ou dependências legadas

### 🔹 Feedback Visual
- `GsProgressBar`
- `GsProgressLabel`
- Tokens semânticos adicionados ao `GsTheme`
- Estados reutilizáveis (`ProgressState`)

### 🔹 Layout e Navegação
- `GsSeparator` (unificado horizontal/vertical)
- `GsPaginator` (desacoplado de grids e dados)

### 🔹 Media
- `GsImageBox`
- Suporte a escala, borda e tema
- Sem lógica de edição ou upload

---

## 🧠 Arquitetura Aplicada

- **Theme First**
- **Single Responsibility**
- **Controles desacoplados de dados**
- **Enums centralizados**
- **Estados semânticos reutilizáveis**
- **Legado isolado, não misturado**

---

## 🧱 Estrutura de Estados (Enums)

Centralizados em:
GS.Core.UI
└── Controls
└── States

Enums consolidados:
- `ProgressState`
- `GridFilterType`
- `GridFilterCombineMode`

---

## 🧼 Controles Legados Isolados

Os controles abaixo permanecem funcionais, porém **não foram refatorados neste bloco** e serão tratados em fases futuras:

- `GsTitleLabel`
- `GsSideMenu`
- `GsConfiguration`
- `GsPanel`
- `GsRadioButton`
- `GsToggleButton`
- `GsChart1` → `GsChart5`

Esses controles são classificados como **LEGACY-FUNCTIONAL**.

---

## 🏁 Status do Bloco

- ✔ Compilação estável
- ✔ Demo funcional
- ✔ Arquitetura padronizada
- ✔ Base pronta para expansão

O **BLOCO 2 está oficialmente encerrado**.

